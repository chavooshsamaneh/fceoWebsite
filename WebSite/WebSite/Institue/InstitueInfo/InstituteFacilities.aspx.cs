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

public partial class Institue_InstitueInfo_InstituteFacilities : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Request.QueryString["InsCId"]))
        {
            Response.Redirect("InstitueCertificates.aspx");
            return;
        }

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.InstitueFacilityManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            GridViewFacility.Visible = per.CanView;

            HiddenFieldFacility["InsCId"] = Request.QueryString["InsCId"].ToString();
            HiddenFieldFacility["InsId"] = Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
            string InsId = Utility.DecryptQS(HiddenFieldFacility["InsId"].ToString());
            string InsCId = Utility.DecryptQS(HiddenFieldFacility["InsCId"].ToString());
            ObjdsInstitueFacility.SelectParameters[0].DefaultValue = InsCId;
            ObjdsInstitueFacility.SelectParameters[1].DefaultValue = InsId;

            TSP.DataManager.InstitueManager InstitueManager = new TSP.DataManager.InstitueManager();
            InstitueManager.FindByCode(int.Parse(InsId));

            if (InstitueManager.Count > 0)
            {
                RoundPanelFacility.HeaderText = "امکانات و تجهیزات موسسه: " + InstitueManager[0]["InsName"].ToString();
            }

        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldFacility["InsCId"].ToString());
        
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Manager":
                Response.Redirect("InstitueManager.aspx?InsCId=" + HiddenFieldFacility["InsCId"].ToString());
                break;
            case "BasicInfo":
                Response.Redirect("InstitueBasicInfo.aspx?InsCId=" + HiddenFieldFacility["InsCId"].ToString());
                break;
            case "Activity":
                Response.Redirect("InstitueActivity.aspx?InsCId=" + HiddenFieldFacility["InsCId"].ToString());
                break;
            case "InsTeacher":
                Response.Redirect("InstitueTeacher.aspx?InsCId=" + HiddenFieldFacility["InsCId"].ToString());
                break;
        }
    }

    protected void GridViewFacility_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.KeyValue != null)
        {
            TSP.DataManager.InstitueFacilityManager InstitueFacilityManager = new TSP.DataManager.InstitueFacilityManager();
            InstitueFacilityManager.FindByCode(int.Parse(e.KeyValue.ToString()));
            if (InstitueFacilityManager.Count == 1)
            {
                Boolean IsEquipment = Boolean.Parse(InstitueFacilityManager[0]["IsEquipment"].ToString());
                DevExpress.Web.ASPxLabel lblFacilityType = GridViewFacility.FindRowCellTemplateControl(e.VisibleIndex, null, "lblFacilityType") as DevExpress.Web.ASPxLabel;
                if (lblFacilityType != null)
                {
                    if (IsEquipment)
                    {
                        lblFacilityType.Text = "تجهیزات";
                    }
                    else
                    {
                        lblFacilityType.Text = "فضای آموزشی";
                    }
                }
            }
        }
    }

    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int InsFacilityId = -1;
        int focucedIndex = GridViewFacility.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewFacility.GetDataRow(focucedIndex);
            InsFacilityId = (int)row["InsFacilityId"];
        }
        if (InsFacilityId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                InsFacilityId = -1;
                Response.Redirect("AddInstituteFacilities.aspx?InsFacilityId=" + Utility.EncryptQS(InsFacilityId.ToString()) + "&InsId=" + HiddenFieldFacility["InsId"] + "&PrePageMode=" + HiddenFieldFacility["PageMode"] + "&PageMode=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddInstituteFacilities.aspx?InsFacilityId=" + Utility.EncryptQS(InsFacilityId.ToString()) + "&InsId=" + HiddenFieldFacility["InsId"].ToString() + "&PrePageMode=" + HiddenFieldFacility["PageMode"] + "&PageMode=" + Utility.EncryptQS(Mode));
            }
        }
    }   

    #endregion       
}
