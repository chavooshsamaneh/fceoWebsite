using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class Employee_HomePage_AddPrintSetting : System.Web.UI.Page
{
    #region Properties
    public string PageMode
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldModeID["PgMode"].ToString());
        }
        set
        {
            HiddenFieldModeID["PgMode"] = Utility.EncryptQS(value.ToString());
        }
    }
    public int ID
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldModeID["Id"].ToString()));
        }
        set
        {
            HiddenFieldModeID["Id"] = Utility.EncryptQS(value.ToString());
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                SetKeys();
                SetPermission();
                SetMode();
               // cmbSecontAssigner.DataBind();
                cmbFirstAssigner.Items.Insert(0, new ListEditItem("----------------------------", null));
                cmbSecontAssigner.Items.Insert(0, new ListEditItem("----------------------------", null));
            }
            catch (Exception err)
            {
                ShowMessage("خطایی در مشاهده اطلاعات بوجود آمده است");
                Utility.SaveWebsiteError(err);
            }
        }
        
        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (this.ViewState["btnnew"] != null)
            this.btnnew.Enabled = this.btnnew2.Enabled = (bool)this.ViewState["btnnew"];
        if (this.ViewState["btnedit"] != null)
            this.btnedit.Enabled = this.btnedit2.Enabled = (bool)this.ViewState["btnedit"];
        if (this.ViewState["btnsave"] != null)
            this.btnsave.Enabled = this.btnsave2.Enabled = (bool)this.ViewState["btnsave"];
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.PrintSettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
        {
            PageMode = "insert";
            SetMode();
        }
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.PrintSettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
           (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanEdit)
        {
            //------------check if this printsetting used or inactive-------
            TSP.DataManager.PrintSettingManager PrintSettingManager = new TSP.DataManager.PrintSettingManager();
            PrintSettingManager.FindByCode(ID);
            if (Convert.ToBoolean(PrintSettingManager[0]["InActive"]))
            {
                ShowMessage("رکورد موردنظر غیرفعال می باشد");
                return;
            }
            if (Convert.ToBoolean(PrintSettingManager[0]["IsUsed"]))
            {
                ShowMessage("این تنظیمات استفاده شده و قابل ویرایش نمی باشد");
                return;
            }
            //---------------------------------------------------------
            PageMode = "edit";
            SetMode();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (PageMode == "insert")
            Insert();
        else if (PageMode == "edit")
            Edit(ID);
    }

    protected void CallbackPanelPrintSetting_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parameter = e.Parameter.Split(';');
        TSP.DataManager.GovManagerNameManager GovManagerNameManager = new TSP.DataManager.GovManagerNameManager();
        if (parameter[1] != "null")
        {
            GovManagerNameManager.FindByTitle(Convert.ToInt32(parameter[1]));
            if (GovManagerNameManager.Count != 0)
            {
                if (Convert.ToBoolean(GovManagerNameManager[0]["InActive"]))
                {
                    ShowCallBackMessage("مدیر مربوطه غیرفعال می باشد");
                    if (parameter[0] == "first")
                        txtFirstAssigner.Text = string.Empty;
                    else
                        txtSecontAssigner.Text = string.Empty;
                    return;
                }
                switch (parameter[0])
                {
                    case "first":
                        if (!Utility.IsDBNullOrNullValue(GovManagerNameManager[0]["GmnName"].ToString()))
                            txtFirstAssigner.Text = GovManagerNameManager[0]["GmnName"].ToString();
                        else txtFirstAssigner.Text = string.Empty;
                        break;
                    case "second":
                        if (!Utility.IsDBNullOrNullValue(GovManagerNameManager[0]["GmnName"].ToString()))
                            txtSecontAssigner.Text = GovManagerNameManager[0]["GmnName"].ToString();
                        else txtSecontAssigner.Text = string.Empty;
                        break;
                }
            }
            else
            {
                if (parameter[0] == "first")
                    txtFirstAssigner.Text = string.Empty;
                else
                    txtSecontAssigner.Text = string.Empty;
            }
        }

    }
    #endregion

    #region Methods
    void SetKeys()
    {
        try
        {
            if (Request.QueryString.Count != 0)
            {
                ID = int.Parse(Utility.DecryptQS(Request.QueryString["id"].ToString()));
                PageMode = Utility.DecryptQS(Request.QueryString["mode"].ToString());
            }
            else
            {
                Response.Redirect("PrintSetting.aspx");
            }
        }
        catch
        {
            Response.Redirect("PrintSetting.aspx");
        }
    }

    void SetMode()
    {
        switch (PageMode)
        {
            case "view":
                DisableControls();
                FillControls(ID);
                PageMode = "view";
                RoundPanelMain.HeaderText = "مشاهده";
                SetButtoms(true, true, false);
                break;
            case "edit":
                PageMode = "edit";
                EnableControls();
                FillControls(ID);
                RoundPanelMain.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
                break;
            case "insert":
                PageMode = "insert";
                EnableControls();
                ResetForm();
                RoundPanelMain.HeaderText = "جدید";
                SetButtoms(true, false, true);
                txtCreateDate.Text = Utility.GetDateOfToday();
                break;
        }
        this.ViewState["btnnew"] = btnnew.Enabled;
        this.ViewState["btnedit"] = btnedit.Enabled;
        this.ViewState["btnsave"] = btnsave.Enabled;
    }

    void SetPermission()
    {
        TSP.DataManager.Permission per = TSP.DataManager.PrintSettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
            (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnsave.Enabled = per.CanEdit || per.CanNew;
        btnsave2.Enabled = per.CanEdit || per.CanNew;
        btnnew.Enabled = per.CanNew;
        btnedit.Enabled = per.CanEdit;
        btnnew2.Enabled = per.CanNew;
        btnedit2.Enabled = per.CanEdit;
    }

    void ResetForm()
    {
        txtFirstAssigner.Text = string.Empty;
        txtSecontAssigner.Text = string.Empty;
        MemoDescription.Text = string.Empty;
        cmbPrintType.SelectedIndex = -1;
        cmbFirstAssigner.SelectedIndex = -1;
        cmbSecontAssigner.SelectedIndex = -1;
        ID = -1;
        RoundPanelMain.HeaderText = "";
        PageMode = "insert";
    }

    void DisableControls()
    {
        RoundPanelMain.Enabled = false;
    }

    void EnableControls()
    {
        RoundPanelMain.Enabled = true;
    }

    void FillControls(int id)
    {
        TSP.DataManager.PrintSettingManager PrintSettingManager = new TSP.DataManager.PrintSettingManager();
        TSP.DataManager.PrintAssignerSettingManager pasManager = new TSP.DataManager.PrintAssignerSettingManager();
        TSP.DataManager.GovManagerNameManager GovManagerNameManager = new TSP.DataManager.GovManagerNameManager();
        //---------------fill main data
        PrintSettingManager.FindByCode(id);
        if (PrintSettingManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(PrintSettingManager[0]["PrtTypeId"]))
            {
                cmbPrintType.DataBind();
                cmbPrintType.SelectedIndex = cmbPrintType.Items.FindByValue(PrintSettingManager[0]["PrtTypeId"].ToString()).Index;
            }
            txtCreateDate.Text = PrintSettingManager[0]["CreateDate"].ToString();
            txtExpireDate.Text = PrintSettingManager[0]["ExpireDate"].ToString();
            MemoDescription.Text = PrintSettingManager[0]["Description"].ToString();

            pasManager.FindByPrintSettingCode(id, 1);
            if (pasManager.Count == 1)
            {

                if (!Utility.IsDBNullOrNullValue(pasManager[0]["GmtId"]))
                {
                    cmbFirstAssigner.DataBind();
                    cmbFirstAssigner.SelectedIndex = cmbFirstAssigner.Items.FindByValue(pasManager[0]["GmtId"].ToString()).Index;
                    GovManagerNameManager.FindByTitle(Convert.ToInt32(pasManager[0]["GmtId"]));
                    txtFirstAssigner.Text = GovManagerNameManager[0]["GmnName"].ToString();
                }
            }

            pasManager.FindByPrintSettingCode(id, 2);
            if (pasManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(pasManager[0]["GmtId"]))
                {
                    cmbSecontAssigner.DataBind();
                    cmbSecontAssigner.SelectedIndex = cmbSecontAssigner.Items.FindByValue(pasManager[0]["GmtId"].ToString()).Index;
                    GovManagerNameManager.FindByTitle(Convert.ToInt32(pasManager[0]["GmtId"]));
                    txtSecontAssigner.Text = GovManagerNameManager[0]["GmnName"].ToString();
                }
            }

        }
    }

    void SetButtoms(bool newb, bool editb, bool saveb)
    {
        TSP.DataManager.Permission per = TSP.DataManager.PrintSettingManager.GetUserPermission(Utility.GetCurrentUser_UserId(),
          (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if ((!per.CanNew) && (newb))
            newb = false;
        if ((!per.CanEdit) && (editb))
            editb = false;

        btnnew.Enabled = newb;
        btnnew2.Enabled = newb;
        btnedit.Enabled = editb;
        btnedit2.Enabled = editb;
        btnsave.Enabled = saveb;
        btnsave2.Enabled = saveb;
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("Style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackPanelPrintSetting.JSProperties["cpMsg"] = Msg;
        CallbackPanelPrintSetting.JSProperties["cpError"] = 1;
    }

    public void Insert()
    {
        TSP.DataManager.TransactionManager TransactManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PrintSettingManager PrintSettingManager = new TSP.DataManager.PrintSettingManager();
        TSP.DataManager.PrintSettingManager PrintSettingManager2 = new TSP.DataManager.PrintSettingManager();
        TSP.DataManager.PrintAssignerSettingManager pasManager = new TSP.DataManager.PrintAssignerSettingManager();
        TransactManager.Add(PrintSettingManager);
        TransactManager.Add(pasManager);

        try
        {
            //--------------check unique setting per printtype------
            //DataTable dt = PrintSettingManager2.SelectByPrintType(Convert.ToInt32(cmbPrintType.Value));
            //if (dt.Rows.Count != 0)
            //{
            //    ShowMessage("پیش از این امضاکنندگان گواهینامه انتخاب شده تعریف شده است");
            //    return;
            //}
            //------------------------------------------------------

            TransactManager.BeginSave();
            DataRow row = PrintSettingManager.NewRow();
            row["InActive"] = 0;
            if (txtCreateDate.Text != string.Empty)
                row["CreateDate"] = txtCreateDate.Text.Trim();

            if (txtExpireDate.Text != string.Empty)
                row["ExpireDate"] = txtExpireDate.Text.Trim();            

            row["Description"] = MemoDescription.Text.Trim();
            row["IsUsed"] = 0;
            row["PrtTypeId"] = cmbPrintType.Value != null ? cmbPrintType.SelectedItem.Value : DBNull.Value;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            PrintSettingManager.AddRow(row);
            if (PrintSettingManager.Save() > 0)
            {
                ID = Convert.ToInt32(PrintSettingManager[0]["PrtsId"].ToString());

                //------------save first assigner 
                DataRow pasrow1 = pasManager.NewRow();
                pasrow1["PrtsId"] = ID;
                pasrow1["AssignerOrder"] = 1;
                pasrow1["GmtId"] = cmbFirstAssigner.Value != null ? cmbFirstAssigner.SelectedItem.Value : DBNull.Value;
                pasrow1["UserId"] = Utility.GetCurrentUser_UserId();
                pasrow1["ModifiedDate"] = DateTime.Now;
                pasManager.AddRow(pasrow1);
                if (pasManager.Save() > 0)
                {
                    pasManager.DataTable.AcceptChanges();
                }
                else
                {
                    TransactManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }

                //------------save second assigner 
                if (cmbSecontAssigner.Value != null)
                {
                    DataRow pasrow2 = pasManager.NewRow();
                    pasrow2["PrtsId"] = ID;
                    pasrow2["AssignerOrder"] = 2;
                    pasrow2["GmtId"] = cmbSecontAssigner.Value != null ? cmbSecontAssigner.SelectedItem.Value : DBNull.Value;
                    pasrow2["UserId"] = Utility.GetCurrentUser_UserId();
                    pasrow2["ModifiedDate"] = DateTime.Now;
                    pasManager.AddRow(pasrow2);
                    if (pasManager.Save() > 0)
                    {
                        pasManager.DataTable.AcceptChanges();
                    }
                    else
                    {
                        TransactManager.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                    }
                }
                //---------------------------------------------------------

                PrintSettingManager.DataTable.AcceptChanges();
                TransactManager.EndSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                PageMode = "edit";
                RoundPanelMain.HeaderText = "ویرایش";
                SetButtoms(true, false, true);
            }
        }
        catch (Exception err)
        {
            TransactManager.CancelSave();
            SetError(err);
        }
    }

    public void Edit(int id)
    {
        TSP.DataManager.TransactionManager TransactManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PrintSettingManager PrintSettingManager = new TSP.DataManager.PrintSettingManager();
        TSP.DataManager.PrintSettingManager PrintSettingManager2 = new TSP.DataManager.PrintSettingManager();
        TSP.DataManager.PrintAssignerSettingManager pasManager = new TSP.DataManager.PrintAssignerSettingManager();
        TransactManager.Add(PrintSettingManager);
        TransactManager.Add(pasManager);

        try
        {
            PrintSettingManager.FindByCode(id);
            //------------check if this printsetting used or inactive-------          
            if (Convert.ToBoolean(PrintSettingManager[0]["InActive"]))
            {
                ShowMessage("رکورد موردنظر غیرفعال می باشد");
                return;
            }
            if (Convert.ToBoolean(PrintSettingManager[0]["IsUsed"]))
            {
                ShowMessage("این تنظیمات استفاده شده و قابل ویرایش نمی باشد");
                return;
            }
            //---------------------------------------------------------

            //--------------check unique setting per printtype------
            //if (Convert.ToInt32(PrintSettingManager[0]["PrtTypeId"].ToString()) != Convert.ToInt32(cmbPrintType.Value))
            //{
            //    PrintSettingManager2.FindByCode(id);
            //    DataTable dt = PrintSettingManager2.SelectByPrintType(Convert.ToInt32(cmbPrintType.Value));
            //    if (dt.Rows.Count != 0)
            //    {
            //        ShowMessage("پیش از این امضاکنندگان گواهینامه انتخاب شده تعریف شده است");
            //        return;
            //    }
            //}
            //------------------------------------------------------

            TransactManager.BeginSave();
            if (PrintSettingManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                TransactManager.CancelSave();
                return;
            }
            PrintSettingManager[0].BeginEdit();
            if (txtCreateDate.Text != string.Empty)
                PrintSettingManager[0]["CreateDate"] = txtCreateDate.Text.Trim();
            if (txtExpireDate.Text != string.Empty)
                PrintSettingManager[0]["ExpireDate"] = txtExpireDate.Text.Trim();

            //  PrintSettingManager[0]["InActive"] = 0;
            // PrintSettingManager[0]["InActiveDate"] = Utility.GetDateOfToday();
            PrintSettingManager[0]["Description"] = MemoDescription.Text.Trim();
            //   PrintSettingManager[0]["IsUsed"] = 0;
            PrintSettingManager[0]["PrtTypeId"] = cmbPrintType.Value != null ? cmbPrintType.SelectedItem.Value : DBNull.Value;
            PrintSettingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PrintSettingManager[0]["ModifiedDate"] = DateTime.Now;
            PrintSettingManager[0].EndEdit();
            if (PrintSettingManager.Save() <= 0)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                TransactManager.CancelSave();
                return;
            }

            #region ------------save first assigner
            pasManager.FindByPrintSettingCode(id, 1);
            if (pasManager.Count == 1)
            {
                pasManager[0].BeginEdit();
                pasManager[0]["PrtsId"] = ID;
                pasManager[0]["AssignerOrder"] = 1;
                pasManager[0]["GmtId"] = cmbFirstAssigner.Value != null ? cmbFirstAssigner.SelectedItem.Value : DBNull.Value;
                pasManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                pasManager[0]["ModifiedDate"] = DateTime.Now;
                pasManager[0].EndEdit();
                if (pasManager.Save() > 0)
                {
                    pasManager.DataTable.AcceptChanges();
                }
                else
                {
                    TransactManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                //------------insert first assigner 
                if (cmbSecontAssigner.Value != null)
                {
                    DataRow pasrow2 = pasManager.NewRow();
                    pasrow2["PrtsId"] = ID;
                    pasrow2["AssignerOrder"] = 1;
                    pasrow2["GmtId"] = cmbFirstAssigner.Value != null ? cmbFirstAssigner.SelectedItem.Value : DBNull.Value;
                    pasrow2["UserId"] = Utility.GetCurrentUser_UserId();
                    pasrow2["ModifiedDate"] = DateTime.Now;
                    pasManager.AddRow(pasrow2);
                    if (pasManager.Save() > 0)
                    {
                        pasManager.DataTable.AcceptChanges();
                    }
                    else
                    {
                        TransactManager.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                    }
                }
            }
            #endregion 

            #region Second Assigner
            pasManager.FindByPrintSettingCode(id, 2);
            if (pasManager.Count == 1)
            {
                //------------Edit second assigner 
                if (cmbSecontAssigner.Value != null)
                {
                    pasManager[0].BeginEdit();
                    pasManager[0]["PrtsId"] = ID;
                    pasManager[0]["AssignerOrder"] = 2;
                    pasManager[0]["GmtId"] = cmbSecontAssigner.Value != null ? cmbSecontAssigner.SelectedItem.Value : DBNull.Value;
                    pasManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    pasManager[0]["ModifiedDate"] = DateTime.Now;
                    pasManager[0].EndEdit();
                    if (pasManager.Save() > 0)
                    {
                        pasManager.DataTable.AcceptChanges();
                    }
                    else
                    {
                        TransactManager.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                    }
                }
                else
                {
                    pasManager[0].Delete();
                    pasManager.Save();

                }
            }
            else
            {
                //------------insert second assigner 
                if (cmbSecontAssigner.Value != null)
                {
                    DataRow pasrow2 = pasManager.NewRow();
                    pasrow2["PrtsId"] = ID;
                    pasrow2["AssignerOrder"] = 2;
                    pasrow2["GmtId"] = cmbSecontAssigner.Value != null ? cmbSecontAssigner.SelectedItem.Value : DBNull.Value;
                    pasrow2["UserId"] = Utility.GetCurrentUser_UserId();
                    pasrow2["ModifiedDate"] = DateTime.Now;
                    pasManager.AddRow(pasrow2);
                    if (pasManager.Save() > 0)
                    {
                        pasManager.DataTable.AcceptChanges();
                    }
                    else
                    {
                        TransactManager.CancelSave();
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                    }
                }
            }
            #endregion
            //---------------------------------------------------------

            PrintSettingManager.DataTable.AcceptChanges();
            TransactManager.EndSave();
            PageMode = "edit";
            RoundPanelMain.HeaderText = "ویرایش";
            SetButtoms(true, false, true);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));


        }
        catch (Exception err)
        {
            TransactManager.CancelSave();
            SetError(err);
        }
    }

    private void SetError(Exception err)
    {
        Utility.SaveWebsiteError(err);
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                ShowMessage("شماره پرونده تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                ShowMessage("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }
    #endregion
}