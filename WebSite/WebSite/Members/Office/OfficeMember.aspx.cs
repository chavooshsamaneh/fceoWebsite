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
using DevExpress.Web;

public partial class Members_Office_OfficeMember : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OfReId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]) )
            {
                Response.Redirect("Office.aspx");
                return;
            }
            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);


            if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjectDataSource1.SelectParameters[0].DefaultValue = OfId;
            ObjectDataSource1.SelectParameters[1].DefaultValue = OfReId;
            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                lblOfName.Text = OfManager[0]["OfName"].ToString();
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
  
   
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int OfReId = -1;
        byte OfmType = 0;
        int PersonId = -1;


        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            OfmType = Convert.ToByte(row["OfmType"]);
            
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OfficeMemberShow.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&OfmType=" + Utility.EncryptQS(OfmType.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString()) + "&OfReId=" + OfficeRequest.Value + "&OfId=" + OfficeId.Value);
           
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeShowInfo.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int OfmId = -1;
        int PersonId = -1;
        //int OfReId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1 || PersonId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای تائید درخواست ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();

            try
            {
                if (PersonId == Utility.GetCurrentUser_MeId())
                {
                    MemManager.FindByCode(OfmId);

                    if (!Utility.IsDBNullOrNullValue(MemManager[0]["IsConfirm"]))
                    {
                        if (!Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "درخواست مورد نظر قبلاً رد شده است";
                            return;
                        }
                        if (Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "درخواست مورد نظر قبلاً تأیید شده است";
                            return;
                        }
                    }
                  

                    MemManager[0].BeginEdit();
                    MemManager[0]["IsConfirm"] = 1;//تائید درخواست
                    MemManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
                    MemManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    MemManager[0].EndEdit();

                    MemManager.Save();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    CustomAspxDevGridView1.DataBind();
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان پاسخ درخواست برای شما وجود ندارد";
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
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int PersonId = -1;


        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1 || PersonId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای تائید درخواست ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();


            try
            {
                if (PersonId == Utility.GetCurrentUser_MeId())
                {
                    MemManager.FindByCode(OfmId);
                    if (!Utility.IsDBNullOrNullValue(MemManager[0]["IsConfirm"]))
                    {
                        if (!Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "درخواست مورد نظر قبلاً رد شده است";
                            return;
                        }
                        if (Convert.ToBoolean(MemManager[0]["IsConfirm"]))
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "درخواست مورد نظر قبلاً تأیید شده است";
                            return;
                        }
                    }

                    MemManager[0].BeginEdit();
                    MemManager[0]["IsConfirm"] = 0;//رد درخواست
                    MemManager[0]["ConfirmDate"] = Utility.GetDateOfToday();
                    MemManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    MemManager[0].EndEdit();

                    MemManager.Save();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    CustomAspxDevGridView1.DataBind();
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان پاسخ درخواست برای شما وجود ندارد";
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
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
                break;
            case "Office":
                Response.Redirect("OfficeShowInfo.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
                break;
        }

    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ConfirmDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ConfirmDate")
            e.Cell.Style["direction"] = "ltr";
    }
}
