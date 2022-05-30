using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class Employee_Management_ProvinceInsert : System.Web.UI.Page
{
    #region Properties
    string PageMode
    {
        set
        {
            HiddenFieldProvince["PageMode"] = value;
        }
        get
        {
            return HiddenFieldProvince["PageMode"].ToString();
        }
    }
    int PrId
    {
        set
        {
            HiddenFieldProvince["PrId"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldProvince["PrId"]);
        }
    }
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

        SetWarningLableWarning();

        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.ProvinceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnNew.Enabled = btnNew2.Enabled = Per.CanNew;
            btnEdit2.Enabled = btnEdit.Enabled = Per.CanEdit;
            btnSave.Enabled = btnSave2.Enabled = Per.CanNew || Per.CanEdit;

            SetKey();
            SetMode();

            this.ViewState["btnNew"] = btnNew.Enabled;
            this.ViewState["btnEdit"] = btnEdit.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;

        }

        if (this.ViewState["btnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["btnNew"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
        if (this.ViewState["btnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["btnEdit"];
        
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        PageMode = "New";
        SetMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PageMode = "Edit";
        SetMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (PageMode == "New")
            Insert();
        else if (PageMode == "Edit")
        {
            Update(PrId);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Province.aspx");
    }

    #endregion

    #region Method

    private void SetWarningLableWarning()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetKey()
    {
        if (Request.QueryString["PgMd"] == null || Request.QueryString["PId"] == null)
        {
            Response.Redirect("Province.aspx");
            return;
        }
        PageMode = Utility.DecryptQS(Request.QueryString["PgMd"].ToString());
        PrId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PId"].ToString()));
    }

    private void SetMode()
    {
        switch (PageMode)
        {
            case "New":
                SetNewMode();
                break;
            case "Edit":
                SetEditMode();
                break;
            case "View":
                SetViweMode();
                break;
        }
    }

    private void SetNewMode()
    {
        RoundPanelMain.HeaderText = "جدید";
        SetControlEnable(true);
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = true;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;

        //Clear Form
        ClearForm();
    }

    private void SetEditMode()
    {
        //Set RoundPanel
        RoundPanelMain.HeaderText = "ویرایش";
        //FillForm
        FillForm();
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = true;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
        PageMode = "Edit";
        //Clear Form
        //Set ControlEnable
        SetControlEnable(true);
    }

    private void SetViweMode()
    {
        //Set RoundPanel
        RoundPanelMain.HeaderText = "مشاهده";
        //FillForm
        FillForm();
        //Set Btn Enable
        btnSave.Enabled = btnSave2.Enabled = false;
        btnNew.Enabled = btnNew2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = true;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["btnNew"] = btnNew.Enabled;
        this.ViewState["btnEdit"] = btnEdit.Enabled;
        //Clear Form
        //Set ControlEnable
        SetControlEnable(false);
    }
    private void FillForm()
    {
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        ProvinceManager.FindByCode(PrId);
        if (ProvinceManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(ProvinceManager[0]["PrName"]))
            txtTittle.Text = ProvinceManager[0]["PrName"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProvinceManager[0]["NezamCode"]))
            txtCode.Text = ProvinceManager[0]["NezamCode"].ToString();
    }
    private void SetControlEnable(Boolean Enabled)
    {
        txtTittle.Enabled = Enabled;
        txtCode.Enabled = Enabled;
    }
    private void ClearForm()
    {
        txtTittle.Text = "";
        txtCode.Text = "";
    }

    #region Insert-Update
    private void Insert()
    {
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        try
        {
            DataRow dr = ProvinceManager.NewRow();
            if (txtTittle.Text != null)
                dr["PrName"] = txtTittle.Text;
            if (txtCode.Text != null && int.Parse(txtCode.Text)<int.MaxValue)
                dr["NezamCode"] = txtCode.Text;
            dr["CounId"] = Utility.GetCurrentCounId();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            dr["EditDate"] = Utility.GetDateOfToday();
            dr["CreatDate"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;


            ProvinceManager.AddRow(dr);
            ProvinceManager.Save();
            ProvinceManager.DataTable.AcceptChanges();
            PrId = Convert.ToInt32(ProvinceManager[ProvinceManager.Count - 1]["PrId"]);


            SetEditMode();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));

        }
        catch (Exception err)
        {

            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private void Update(int PrId)
    {
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        try
        {
            ProvinceManager.FindByCode(PrId);
            if (ProvinceManager.Count != 1)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            ProvinceManager[0].BeginEdit();
            if (txtTittle.Text != null)
                ProvinceManager[0]["PrName"] = txtTittle.Text;
            if (txtCode.Text != null)
                ProvinceManager[0]["NezamCode"] = txtCode.Text;
            ProvinceManager[0]["CounId"] = Utility.GetCurrentCounId();
            ProvinceManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ProvinceManager[0]["ModifiedDate"] = DateTime.Now;
            ProvinceManager[0]["EditDate"] = Utility.GetDateOfToday();
            ProvinceManager[0]["InActive"] = 0;

            ProvinceManager[0].EndEdit();
            ProvinceManager.Save();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }

    }

    #endregion

    #endregion
}