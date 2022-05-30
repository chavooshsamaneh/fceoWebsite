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

public partial class Members_Amoozesh_MadrakForUpGrade : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.display='none'; </script>");
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            ClearMessageCP();
            Session["SendBackDataTable_PeriodReg"] = "";
            OdbMadrak.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("MadrakForUpGradeInsert.aspx?MdId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        int ID = -1;
        int MdId = -1;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            ID = MdId  = (int)row["MdId"];
        }
        if (ID == -1 )
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            Response.Redirect("MadrakForUpGradeInsert.aspx?MdId=" + Utility.EncryptQS(ID.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }

    #endregion

    #region Methods
    private void ClearMessageCP()
    {
        GridViewPeriodRegister.JSProperties["cpError"] = 0;
        GridViewPeriodRegister.JSProperties["cpMsg"] = "";
    }

    private void ShowErrorMessage(string Message)
    {
        GridViewPeriodRegister.JSProperties["cpError"] = 1;
        GridViewPeriodRegister.JSProperties["cpMsg"] = Message;
    }
    #endregion
}