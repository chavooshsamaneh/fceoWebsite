using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Accounting_Users_AgentAttachments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        //SetNavBar(3);

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.AccountingAgentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnSave.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["AgentId"]))
            {
                Response.Redirect("Agent.aspx");
                return;
            }
            try
            {
                HDAgentId.Value = Server.HtmlDecode(Request.QueryString["AgentId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string AgentId = Utility.DecryptQS(HDAgentId.Value);

            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            CustomAspxDevGridView1.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Agent, int.Parse(AgentId));
            CustomAspxDevGridView1.DataBind();


            this.ViewState["BtnView"] = btnView.Enabled;
           
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
       
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        
    }

    void SetNavBar(int i)
    {
        DevExpress.Web.ASPxNavBar ASPxNavBar1 = (DevExpress.Web.ASPxNavBar)Master.FindControl("ASPxNavBar1");
        ASPxNavBar1.DataBind();
        ASPxNavBar1.Groups[i].Expanded = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!flp.HasFile)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " فایل مورد نظر را انتخاب نمایید";
            return;
        }

        string fileNameImg = "", pathAx = "", extension = "";
        // byte[] img = null;
        bool AxImg = false;
        try
        {
            TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

            DataRow dr = attManager.NewRow();
            dr["TtId"] = (int)TSP.DataManager.TableCodes.Agent;
            dr["RefTable"] = Utility.DecryptQS(HDAgentId.Value);
            dr["AttId"] = 1;
            dr["IsValid"] = 1;
            dr["Description"] = txtDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModfiedDate"] = DateTime.Now;

            try
            {
                extension = Path.GetExtension(flp.FileName);
                extension = extension.ToLower();

                // if (extension == ".jpg" || extension == ".gif")
                // {
                if (flp.HasFile)
                {
                    //img = flp.FileBytes;
                    fileNameImg = Utility.GenerateName(Path.GetExtension(flp.FileName));
                    pathAx = Server.MapPath("~/image/Temp/");
                    flp.SaveAs(pathAx + fileNameImg);
                    // dr["AtContent"] = img;
                    dr["AtContent"] = DBNull.Value;
                    dr["FilePath"] = "~/Image/Office/Attachments/" + fileNameImg;
                    AxImg = true;
                }
                // }


            }
            catch
            {
            }

            attManager.AddRow(dr);
            int cnt = attManager.Save();
            if (cnt == 1)
            {
                if (AxImg == true)
                {
                    try
                    {
                        string ImgSoource = Server.MapPath("~/image/Temp/") + fileNameImg;
                        string ImgTarget = Server.MapPath("~/image/Office/Attachments/") + fileNameImg;
                        File.Copy(ImgSoource, ImgTarget,true);
                    }
                    catch
                    {
                    }
                }
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " ذخیره انجام شد";
                txtDesc.Text = "";
                CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Agent, int.Parse(Utility.DecryptQS(HDAgentId.Value)));
                CustomAspxDevGridView1.DataBind();

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    txtDesc.Text = "";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                txtDesc.Text = "";
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int AttachId = -1;
        TSP.DataManager.AttachmentsManager attManager = new TSP.DataManager.AttachmentsManager();

        CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Agent, int.Parse(Utility.DecryptQS(HDAgentId.Value)));
        CustomAspxDevGridView1.DataBind();

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            AttachId = (int)row["AttachId"];
        }
        if (AttachId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            attManager.FindByCode(AttachId);
            if (attManager.Count == 1)
            {
                try
                {
                    attManager[0].Delete();

                    int cn = attManager.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataSource = attManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Agent, int.Parse(Utility.DecryptQS(HDAgentId.Value)));
                        CustomAspxDevGridView1.DataBind();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }

                }
                catch (Exception err)
                {

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 547)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }
                }

            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Agent.aspx");
    }
}
