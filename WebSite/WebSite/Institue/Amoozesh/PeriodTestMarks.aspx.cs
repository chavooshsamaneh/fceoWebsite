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

public partial class Institue_Amoozesh_PeriodTestMarks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.PeriodTestMarksManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew;
            btnSave1.Enabled = per.CanNew;


            if (string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
            {
                Response.Redirect("Period.aspx");
            }

            PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"].ToString());
            InstitueId.Value = Server.HtmlDecode(Request.QueryString["InsId"].ToString());
            string PPId = Utility.DecryptQS(PeriodId.Value);
            string InsId = Utility.DecryptQS(InstitueId.Value);

            if (string.IsNullOrEmpty(PPId) || string.IsNullOrEmpty(InsId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            ObjectDataSource1.FilterParameters[0].DefaultValue = PPId;


            TSP.DataManager.InstitueManager InsManager = new TSP.DataManager.InstitueManager();
            InsManager.FindByCode(int.Parse(InsId));
            lblInsName.Text = InsManager[0]["InsName"].ToString();
            lblManager.Text = InsManager[0]["Manager"].ToString();

            TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
            PPManager.FindByCode(int.Parse(PPId));
            txtPPTitle.Text = PPManager[0]["PeriodTitle"].ToString();


            TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
            MarkManager.SelectPeriodMarks(int.Parse(PPId));
            if (MarkManager.Count > 0)
            {
                if (MarkManager[0]["UltId"].ToString() == "6")//موسسه
                {
                    HDPageMode.Value = "Edit";
                    //btnSave.Enabled = false;
                    //btnSave1.Enabled = false;
                    txtDate.Text = MarkManager[0]["ObjectionDate"].ToString();
                    txtTotalMark.Text = MarkManager[0]["TotalMark"].ToString();
                    //CustomAspxDevGridView1.Columns[4].Visible = false;
                    //CustomAspxDevGridView1.Columns[5].Visible = false;
                    //CustomAspxDevGridView1.Columns[6].Visible = false;
                    //CustomAspxDevGridView1.Columns[7].Visible = true;
                    //CustomAspxDevGridView1.Columns[8].Visible = true;
                }
                else
                {
                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnConfirm1.Enabled = false;
                    txtDate.Text = MarkManager[0]["ObjectionDate"].ToString();
                    txtTotalMark.Text = MarkManager[0]["TotalMark"].ToString();
                    //CustomAspxDevGridView1.Columns[4].Visible = false;
                    //CustomAspxDevGridView1.Columns[5].Visible = false;
                    //CustomAspxDevGridView1.Columns[6].Visible = false;
                    //CustomAspxDevGridView1.Columns[7].Visible = true;
                    //CustomAspxDevGridView1.Columns[8].Visible = true;
                }
            }
            else
                HDPageMode.Value = "New";

            if (Convert.ToBoolean(PPManager[0]["IsConfirm"]))//تایید نهایی
            {
                btnSave.Enabled = false;
                btnSave1.Enabled = false;
                btnConfirm.Enabled = false;
                btnConfirm1.Enabled = false;
                txtDate.Text = MarkManager[0]["ObjectionDate"].ToString();
                txtTotalMark.Text = MarkManager[0]["TotalMark"].ToString();
                lblPStatus.Visible = true;
                txtPPStatus.Visible = true;
                //CustomAspxDevGridView1.Columns[4].Visible = false;
                //CustomAspxDevGridView1.Columns[5].Visible = false;
                //CustomAspxDevGridView1.Columns[6].Visible = false;
                //CustomAspxDevGridView1.Columns[7].Visible = true;
                //CustomAspxDevGridView1.Columns[8].Visible = true;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave1.Enabled = (bool)this.ViewState["BtnSave"];
    }
    protected void ButtonRet_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPeriod.aspx?PPId=" + PeriodId.Value + "&InsId=" + Request.QueryString["InsId"] + "&PageMode=" + Request.QueryString["PageMode"] + "&PPRId=" + Request.QueryString["PPRId"]);
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string InsId = Utility.DecryptQS(InstitueId.Value);
        string PPId = Utility.DecryptQS(PeriodId.Value);

        if (string.IsNullOrEmpty(InsId) || string.IsNullOrEmpty(PPId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            if (HDPageMode.Value == "New")
                Insert(int.Parse( InsId), int.Parse(PPId));
            else
                Edit(int.Parse(PPId));
        }

    }
    protected void Insert(int InsId,int PPId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodTestMarksManager TestMarkManager = new TSP.DataManager.PeriodTestMarksManager();
        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        trans.Add(PPManager);
        trans.Add(TestMarkManager);

        try
        {
            ArrayList ar = new ArrayList();
            ArrayList ard = new ArrayList();


            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
                DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)CustomAspxDevGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)CustomAspxDevGridView1.Columns["LastMark"], "txtMark");
                DevExpress.Web.ASPxMemo desc = (DevExpress.Web.ASPxMemo)CustomAspxDevGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)CustomAspxDevGridView1.Columns["Description"], "txtDesc");

                if (!string.IsNullOrEmpty(txt.Text))
                    ar.Add(txt.Text);
                else
                    ar.Add(null);

                if (!string.IsNullOrEmpty(desc.Text))
                    ard.Add(desc.Text);
                else
                    ard.Add(null);

            }
            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
                TestMarkManager.DataSet.EnforceConstraints = false;
                DataRow dr = TestMarkManager.NewRow();


                if (ar[i] != null)
                {
                    dr["FirstMark"] = Convert.ToDecimal(ar[i].ToString());
                    dr["LastMark"] = Convert.ToDecimal(ar[i].ToString());
                }
                if (ard[i] != null)
                    dr["Description"] = ard[i].ToString();
                else
                    dr["Description"] = DBNull.Value;
                 
                dr["TotalMark"] = txtTotalMark.Text;
                dr["PRId"] = CustomAspxDevGridView1.GetDataRow(i)["PRId"].ToString();
                dr["PkId"] = InsId;
                dr["UltId"] = 6;
                dr["ObjectionDate"] = txtDate.Text;
                dr["CreateDate"] = Utility.GetDateOfToday();
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                TestMarkManager.AddRow(dr);
            
            }
            trans.BeginSave();
            int cnt = TestMarkManager.Save();
            if (cnt > 0)
            {
                PPManager.FindByCode(PPId);
                PPManager[0].BeginEdit();
                PPManager[0]["Status"] = (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection;
                PPManager[0].EndEdit();
                PPManager.Save();

                trans.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                btnSave.Enabled = false;
                btnSave1.Enabled = false;


            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
            }

        }
        catch (Exception err)
        {
            trans.CancelSave();

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
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        TSP.DataManager.PeriodPresentManager Manager = new TSP.DataManager.PeriodPresentManager();
        try
        {
            string PPId = Utility.DecryptQS(PeriodId.Value);
            Manager.FindByCode(int.Parse(PPId));
            if (Manager.Count > 0)
            {

                Manager[0].BeginEdit();
                Manager[0]["IsConfirm"] = 1;
                Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                Manager[0].EndEdit();
                if (Manager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
                }
            }

        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }
    }
    protected void Edit(int PPId)
    {
        TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        MarkManager.SelectPeriodMarks(PPId);
        if (MarkManager.Count > 0)

            try
            {

                ArrayList ar = new ArrayList();
                ArrayList ard = new ArrayList();


                for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
                {
                    DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)CustomAspxDevGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)CustomAspxDevGridView1.Columns["LastMark"], "txtMark");
                    DevExpress.Web.ASPxMemo desc = (DevExpress.Web.ASPxMemo)CustomAspxDevGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)CustomAspxDevGridView1.Columns["Description"], "txtDesc");

                    if (!string.IsNullOrEmpty(txt.Text))
                        ar.Add(txt.Text);
                    else
                        ar.Add(null);

                    if (!string.IsNullOrEmpty(desc.Text))
                        ard.Add(desc.Text);
                    else
                        ard.Add(null);

                }
                for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
                {
                    MarkManager[i].BeginEdit();

                    if (ar[i] != null)
                    {
                        // MarkManager[i]["FirstMark"] = Convert.ToDecimal(ar[i].ToString());
                        MarkManager[i]["LastMark"] = Convert.ToDecimal(ar[i].ToString());
                    }
                    if (ard[i] != null)
                        MarkManager[i]["Description"] = ard[i].ToString();
                    else
                        MarkManager[i]["Description"] = DBNull.Value;

                    MarkManager[i]["TotalMark"] = txtTotalMark.Text;
                    //dr["PRId"] = CustomAspxDevGridView1.GetDataRow(i)["PRId"].ToString();
                    //dr["PkId"] = TeId;
                    // dr["UltId"] = 5;
                    MarkManager[i]["ObjectionDate"] = txtDate.Text;
                    //dr["CreateDate"] = Utility.GetDateOfToday();
                    MarkManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                    MarkManager[i]["ModifiedDate"] = DateTime.Now;
                    MarkManager[i].EndEdit();
                }
                int cnt = MarkManager.Save();
                if (cnt > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    //btnSave.Enabled = false;
                    //btnSave1.Enabled = false;
                    //Response.Redirect("Period.aspx");
                    HDPageMode.Value = "Edit";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
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
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
    }
    protected void AspxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));
        if (string.IsNullOrEmpty(PPId.ToString()))
        {
            Response.Redirect("Period.aspx");
        }


        switch (e.Item.Name)
        {
            case "Costs":
                Response.Redirect("PeriodCosts.aspx?PPId=" + PeriodId.Value + "&InsId=" + InstitueId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&PPRId=" + Request.QueryString["PPRId"]);
               
                break;
            case "Period":
                Response.Redirect("AddPeriod.aspx?PPId=" + PeriodId.Value + "&InsId=" + InstitueId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&PPRId=" + Request.QueryString["PPRId"]);


                break;
            case "InValid":
                if (!string.IsNullOrEmpty(PeriodId.Value))
                {
                    int PSCId = -1;
                    int TableType = (int)TSP.DataManager.TableCodes.PeriodPresent;

                    if (!IsInActive(PPId))
                    {
                        Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PSCId=" + Utility.DecryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("New") + "&PageMode=" + Request.QueryString["PageMode"] + "&InsId=" + InstitueId.Value + "&PPRId=" + Request.QueryString["PPRId"]);
                    }
                    else
                    {
                        TSP.DataManager.TrainingStatusChangeManager TrainingStatusChangeManager = new TSP.DataManager.TrainingStatusChangeManager();

                        TrainingStatusChangeManager.FindByTableType(TableType, PPId, 0);
                        if (TrainingStatusChangeManager.Count > 0)
                        {
                            PSCId = int.Parse(TrainingStatusChangeManager[0]["PSCId"].ToString());
                            Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PSCId=" + Utility.EncryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "&PageMode=" + Request.QueryString["PageMode"] + "&InsId=" + InstitueId.Value + "&PPRId=" + Request.QueryString["PPRId"]);
                        }
                        //this.DivReport.Visible = true;
                        //this.LabelWarning.Text = "دوره انتخاب شده لغو شده است.";
                    }
                }
                break;

        }
    }
    private Boolean IsInActive(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.InvalidPeriod)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
   
}
