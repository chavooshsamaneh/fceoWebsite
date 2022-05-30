using System;
using System.Data;

public partial class Members_TechnicalServices_Project_Contract : System.Web.UI.Page
{
    #region Properties
    int _PrjReId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["PrjReId"]);
        }
        set
        {
            HiddenFieldPage["PrjReId"] = value;
        }
    }
    int _ProjectId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ProjectId"]);
        }
        set
        {
            HiddenFieldPage["ProjectId"] = value;
        }
    }
    int _PrjImpObsDsgnId
    {

        get
        {
            return Convert.ToInt32(HiddenFieldPage["PrjImpObsDsgnId"]);
        }
        set
        {
            HiddenFieldPage["PrjImpObsDsgnId"] = value;
        }
    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Utility.IsDBNullOrNullValue(Utility.DecryptQS(Request.QueryString["prObsId"].ToString())) || Utility.IsDBNullOrNullValue(Utility.DecryptQS(Request.QueryString["PrId"].ToString())))
            {
                Response.Redirect("Project.aspx");
            }
            _ProjectId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrId"].ToString()));
            _PrjImpObsDsgnId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["prObsId"].ToString()));
            ObjdContract.SelectParameters["PrjImpObsDsgnId"].DefaultValue = _PrjImpObsDsgnId.ToString();
            ObjdContract.SelectParameters["ProjectIngridientTypeId"].DefaultValue =( (int)TSP.DataManager.TSProjectIngridientType.Observer).ToString();
            GridViewContract.DataBind();
            TSP.DataManager.TechnicalServices.ProjectRequestManager projectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            DataTable dtReIdLastConf = projectRequestManager.SelectTSProjectReIdLastConf(_ProjectId);
            if (dtReIdLastConf.Rows.Count == 0)
            {
                SetMessage("خطا در بازخوانی اطلاعات پروژه ایجاد شده است");
                return;
            }
            _PrjReId = Convert.ToInt32(dtReIdLastConf.Rows[0]["PrjReId"]);
            prjInfo.Fill(_PrjReId);
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ContractInsert.aspx?PrId=" +Utility.EncryptQS( _ProjectId.ToString() )+ "&CoId=" + Utility.EncryptQS("-2") + "&PMo=" + Utility.EncryptQS("New") + "&PrObsId=" + Utility.EncryptQS(_PrjImpObsDsgnId.ToString()));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int ContractId = -1;

        if (GridViewContract.FocusedRowIndex > -1)
        {
            DataRow row = GridViewContract.GetDataRow(GridViewContract.FocusedRowIndex);
            ContractId = (int)row["ContractId"];
        }
        if (ContractId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            Response.Redirect("ContractInsert.aspx?PrId=" + Utility.EncryptQS(_ProjectId.ToString())+ "&CoId=" + Utility.EncryptQS(ContractId.ToString()) + "&PMo=" + Utility.EncryptQS("View") + "&PrObsId=" + Utility.EncryptQS(_PrjImpObsDsgnId.ToString()));
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Project.aspx");
    }
    #endregion

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion

}
