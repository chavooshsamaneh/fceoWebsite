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
using System.IO;

public partial class Members_TechnicalServices_Project_ContractInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Properties
    string _PageMode
    {
        get
        {
            return HiddenFieldPage["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPage["PageMode"] = value;
        }
    }
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
    int _ContractId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ContractId"]);
        }
        set
        {
            HiddenFieldPage["ContractId"] = value;
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

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        _PageMode = "Edit";
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (CmbType.Value == null)
        {
            SetLabelWarning("نوع قرارداد را انتخاب نمایید");
            return;
        }

        switch (_PageMode)
        {
            case "New":
                Insert();
                break;            
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Contract.aspx?PrId=" + Utility.EncryptQS(_ProjectId.ToString())
            + "&prObsId=" + Utility.EncryptQS(_PrjImpObsDsgnId.ToString()));
    }

    protected void flpContract_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Set&Get
    private void SetKeys()
    {
        try
        {
            Session["AttachContract"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["PrId"]) || string.IsNullOrEmpty(Request.QueryString["PrObsId"]) || string.IsNullOrEmpty(Request.QueryString["PMo"]) || string.IsNullOrEmpty(Request.QueryString["CoId"]))
            {
                Response.Redirect("Project.aspx");
            }

            _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PMo"].ToString()));
            _ProjectId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PrId"]).ToString()));
            _ContractId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["CoId"]).ToString()));
            _PrjImpObsDsgnId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PrObsId"]).ToString()));
            TSP.DataManager.TechnicalServices.ProjectRequestManager projectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            DataTable dtReIdLastConf = projectRequestManager.SelectTSProjectReIdLastConf(_ProjectId);
            if (dtReIdLastConf.Rows.Count == 0)
            {
                SetMessage("خطا در بازخوانی اطلاعات پروژه ایجاد شده است");
                return;
            }
            _PrjReId = Convert.ToInt32(dtReIdLastConf.Rows[0]["PrjReId"]);
            FillProjectInfo(_PrjReId);
            ObjectDataSourceType.FilterExpression = "ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString();
            ObjectDataSourceObserver.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
            comboProjectObserver.DataBind();
            comboProjectObserver.SelectedIndex = -1;
            comboProjectObserver.SelectedIndex = comboProjectObserver.Items.FindByValue(_PrjImpObsDsgnId.ToString()).Index;
             SetMode(_PageMode);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage("خطا در بازخوانی اطلاعات ایجاد شده است");
        }
    }

    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetNewModeKeys()
    {
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        ClearForm();
        SetEnable(true);

        ASPxRoundPanel2.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        FillForm();
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        SetEnable(true);
        CmbType.Enabled = false;

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        SetEnable(false);
        ASPxRoundPanel2.HeaderText = "مشاهده";
        FillForm();
    }

    private void SetEnable(bool Enable)
    {
        CmbType.Enabled = Enable;
        txtDuration.Enabled = Enable;
        txtAmount.Enabled = Enable;
        txtContractDate.Enabled = Enable;
        flpContract.ClientVisible = Enable;
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #endregion

    #region Fill & Clear
    private void FillForm()
    {
        try
        {
            TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();

            ContractManager.FindByContractId(_ContractId);
            if (ContractManager.Count > 0)
            {
                txtAmount.Text = Convert.ToDecimal(ContractManager[0]["Amount"]).ToString("0");
                txtContractDate.Text = ContractManager[0]["ContractDate"].ToString();
                txtDuration.Text = ContractManager[0]["Duration"].ToString();


                if (!Utility.IsDBNullOrNullValue(ContractManager[0]["FileUrl"]))
                {
                    HpContract.ClientVisible = true;
                    HpContract.NavigateUrl = ContractManager[0]["FileUrl"].ToString();
                    HiddenFieldPage["name"] = 1;
                }
                else
                    HiddenFieldPage["name"] = 0;

                if (!Utility.IsDBNullOrNullValue(ContractManager[0]["ProjectIngridientTypeId"]))
                {
                    CmbType.DataBind();
                    CmbType.SelectedIndex = CmbType.Items.IndexOfValue(ContractManager[0]["ProjectIngridientTypeId"].ToString());
                }
            }
            else
            {
                SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception)
        {
            SetLabelWarning("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    private void ClearForm()
    {
        CmbType.Value = ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString();
        txtDuration.Text = "";
        txtAmount.Text = "";
        txtContractDate.Text = "";

        HpContract.NavigateUrl = "";
        HpContract.ClientVisible = false;
        HiddenFieldPage["name"]= 0;
        Session["AttachContract"] = null;
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }
    #endregion

    #region Insert&Update
    private void Insert()
    {
        if (IsPageRefresh)
            return;
        
        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        

        try
        {         

            if (Session["AttachContract"] == null)
            {
                SetLabelWarning("فایل قرارداد را انتخاب نمایید");
                return;
            }

            DataRow ContracRow = ContractManager.NewRow();

            ContracRow["PrjImpObsDsgnId"] = _PrjImpObsDsgnId;
            ContracRow["ProjectIngridientTypeId"] = int.Parse(CmbType.Value.ToString());
            ContracRow["PrjReId"] = _PrjReId;
            ContracRow["MjId"] = DBNull.Value;
            ContracRow["Duration"] = txtDuration.Text;
            ContracRow["Amount"] = txtAmount.Text;
            ContracRow["PercentWage"] = 0;
            ContracRow["ContractDate"] = txtContractDate.Text;
            ContracRow["HaveMaster"] = false;
            ContracRow["ParentId"] = DBNull.Value;
            if (Session["AttachContract"] != null)
            {
                ContracRow["FileUrl"] = "~/Image/TechnicalServices/Contract/" + Path.GetFileName(Session["AttachContract"].ToString());
            }
            ContracRow["InActive"] = 0;
            ContracRow["CreateDate"] = Utility.GetDateOfToday();
            ContracRow["UserId"] = Utility.GetCurrentUser_UserId();
            ContracRow["ModifiedDate"] = DateTime.Now;

            ContractManager.AddRow(ContracRow);
            ContractManager.Save();

            ContractManager.DataTable.AcceptChanges();
            _ContractId = Convert.ToInt32(ContractManager[0]["ContractId"]);

            SetLabelWarning("ذخیره انجام شد");

            _PageMode = "view";
            SetViewModeKeys();
            Session["AttachContract"] = null;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }

    }    

    #endregion

    private string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/Contract/") + ret) == true);
            string tempFileName = MapPath("~/Image/TechnicalServices/Contract/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["AttachContract"] = tempFileName;

        }
        return ret;
    }


}
