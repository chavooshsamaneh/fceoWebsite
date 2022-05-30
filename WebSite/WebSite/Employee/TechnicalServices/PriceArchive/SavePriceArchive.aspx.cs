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
using DevExpress.Web;

public partial class TechnicalServices_SavePriceArchive : System.Web.UI.Page
{
    #region Properties
    public string PageMode
    {
        get
        {
            return Utility.DecryptQS(hiddenSaveID["PgMode"].ToString());
        }
        set
        {
            //  int PageType = Convert.ToInt32(value);
            switch (value)
            {
                case "1":
                    hiddenSaveID["PgMode"] = Utility.EncryptQS("New");
                    break;
                case "2":
                    hiddenSaveID["PgMode"] = Utility.EncryptQS("Edit");
                    break;
                case "3":
                    hiddenSaveID["PgMode"] = Utility.EncryptQS("View");
                    break;
                default:
                    hiddenSaveID["PgMode"] = Utility.EncryptQS(value.ToString());
                    break;
            }

        }
    }

    public int PrAId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(hiddenSaveID["PrAId"].ToString()));
        }
        set
        {
            hiddenSaveID["PrAId"] = Utility.EncryptQS(value.ToString());
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["PriceArchiveFileAddress"] = null;

                TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PriceArchiveManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanNew || per.CanEdit;
                btnSave2.Enabled = per.CanNew || per.CanEdit;

                if (per.CanView == false)
                {
                    Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString(), false);
                }
                if (per.CanEdit == false)
                {
                    btnEdit.Enabled = btnEdit2.Enabled = false;
                }
                if (per.CanNew == false)
                {
                    btnNew.Enabled = btnNew2.Enabled = false;
                }

                SetKey();
                SetMode(PageMode, per);

                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                this.ViewState["BtnNew"] = btnNew.Enabled;
                this.ViewState["BtnSave"] = btnSave.Enabled;
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");        
        if (Session["PriceArchiveFileAddress"] != null)
        {
            HeyperLinkImg.ImageUrl = Session["PriceArchiveFileAddress"].ToString();
        }
    }


    protected void UploadControlImgURL_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveFile(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("SavePriceArchive.aspx?PT=" + Utility.EncryptQS("1"));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("SavePriceArchive.aspx?PT=" + Utility.EncryptQS("2") + "&PrAId=" + hiddenSaveID["PrAId"]);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PriceArchive.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckInputs())
        {
            // int? PrAId = null;
            // if (hiddenSaveID["PrAId"] != null)
            //     PrAId = Int32.Parse(Utility.DecryptQS(hiddenSaveID["PrAId"].ToString()));
            // else
            //     return;
            //if (PrAId == 0)
            //    PriceArchiveInsert();
            //else
            //    PriceArchiveUpdate();
            switch (PageMode)
            {
                case "New":
                    PriceArchiveInsert();
                    break;
                case "Edit":
                    PriceArchiveUpdate();
                    break;
            }
        }
    }
    #endregion

    #region Methods
    protected string SaveFile(UploadedFile uploadedFile)
    {
        string ret = "";
        string imgName = "";

        if (uploadedFile.IsValid)
        {
                do
                {
                    System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                    ret = "PriceArch" + imgName + Path.GetRandomFileName() + ImageType.Extension;
                } while (File.Exists(MapPath("~/image/TechnicalServices/PriceArchive/") + ret) == true);
                string tempFileName = MapPath("~/Image/TechnicalServices/PriceArchive/") + ret;
                uploadedFile.SaveAs(tempFileName, true);
                Session["PriceArchiveFileAddress"] = "~/Image/TechnicalServices/PriceArchive/" + ret;           
        }
        return ret;
    }

    private void SetKey()
    {
        //  int PageType = 0;//, PrAId = 0;

        if (String.IsNullOrEmpty(Request.QueryString["PT"]) || String.IsNullOrEmpty(Request.QueryString["PrAId"]))
            Response.Redirect("PriceArchive.aspx", false);
        //************ 1: Insert , 2: Update , 3 : View
        PageMode = Utility.DecryptQS(Request.QueryString["PT"]).ToString();
        PrAId = int.Parse(Utility.DecryptQS(Request.QueryString["PrAId"]));

    }

    private void SetMode(string PageMode, TSP.DataManager.Permission per)
    {
        switch (PageMode)
        {
            case "New":
                if (per.CanNew == false)
                    Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.NewNotAllowed).ToString(), false);
                btnEdit.Enabled = btnEdit2.Enabled = false;
                txtYear.Text = Utility.GetDateOfToday().Substring(0, 4);
                txtCreateDate.Text = Utility.GetDateOfToday();
                SetComboBoxStructure();
                RoundPanelContent.HeaderText = "جدید";
                break;
            case "Edit":
                if (per.CanNew == false)
                    Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.EditNotAlowed).ToString(), false);
                btnEdit.Enabled = btnEdit2.Enabled = false;
                Load_PageData(PrAId);
                RoundPanelContent.HeaderText = "ویرایش";
                break;
            case "View":
                if (per.CanNew == false)
                    Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString(), false);
                btnSave.Enabled = btnSave2.Enabled = false;                
                PanelData.Enabled = false;
                SetEnabled(false);
                Load_PageData(PrAId);
                RoundPanelContent.HeaderText = "مشاهده";
                break;
            default:
                Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString(), false);
                break;
        }
    }
   void SetEnabled(bool Enabled)
    {
        txtYear.Enabled = txtCreateDate.Enabled = txtCreateDate.Enabled = txtDescription.Enabled= txtRemark.Enabled = UploadControlImgURL.Enabled = Enabled;
    }
    void SetComboBoxStructure()
    {
        ComboBoxStructureSkeletonA1.DataBind();
        ComboBoxStructureSkeletonA1.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ComboBoxStructureSkeletonA.DataBind();
        ComboBoxStructureSkeletonA.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ComboBoxStructureSkeletonB.DataBind();
        ComboBoxStructureSkeletonB.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ComboBoxStructureSkeletonC1.DataBind();
        ComboBoxStructureSkeletonC1.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ComboBoxStructureSkeletonC2.DataBind();
        ComboBoxStructureSkeletonC2.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ComboBoxStructureSkeletonD1.DataBind();
        ComboBoxStructureSkeletonD1.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ComboBoxStructureSkeletonD2.DataBind();
        ComboBoxStructureSkeletonD2.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
        ComboBoxStructureSkeletonD3.DataBind();
        ComboBoxStructureSkeletonD3.Items.Insert(0, new DevExpress.Web.ListEditItem("--------------------------", null));
    }
    void Load_PageData(int PrAId)
    {
        #region Manager
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager();
        #endregion
        SetComboBoxStructure();
        PriceArchiveManager.FindById(PrAId);
        txtYear.Text = PriceArchiveManager[0]["Year"].ToString();
        txtDescription.Text = PriceArchiveManager[0]["Description"].ToString();
        txtRemark.Text = PriceArchiveManager[0]["Remark"].ToString();
        txtCreateDate.Text = PriceArchiveManager[0]["CreateDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(PriceArchiveManager[0]["FileUrl"]))
            HeyperLinkImg.NavigateUrl = PriceArchiveManager[0]["FileUrl"].ToString();
        String temp = "";
        ItemManager.FindByPriceArchiveId(PrAId);
        if (ItemManager.Count == 0)
            return;
        //************* Group A1 *************************
        #region GroupA1
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.A;
        ItemManager.CurrentFilter += " and No=1";
        if (ItemManager.Count != 0)
        {
            txtStepNoA1From.Text = ItemManager[0]["StepFrom"].ToString();
            txtStepNoA1To.Text = ItemManager[0]["StepTo"].ToString();
            txtCountUnitA1.Text = ItemManager[0]["MaxCountUnits"].ToString();
            txtMaxSqA1.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
            txtCostA1.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
            lblSumAllPercentA1.Text = ItemManager[0]["SumPercents"].ToString();
            if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
                ComboBoxStructureSkeletonA1.SelectedIndex = ComboBoxStructureSkeletonA1.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
            if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
                txtSupervisionSumCordA1.Text = ItemManager[0]["CoordinatorPrice"].ToString();

                ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));
            if (ItemDetailManager.Count == 0)
                return;
            #region Design
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtDesignArchPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtDesignSumArchA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtDesignSazePercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtDesignSumSazeA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtDesignTasisatPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtDesignSumTasisatA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtDesignShahrPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtDesignSumShahrA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtDesignMapPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtDesignSumMapA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            lblDesignSumPercentA1.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentA1.Text) + float.Parse(txtDesignSazePercentA1.Text) + float.Parse(txtDesignTasisatPercentA1.Text) + float.Parse(txtDesignShahrPercentA1.Text) + float.Parse(txtDesignMapPercentA1.Text));
            #endregion

            #region Supervision
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtSupervisionArchPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtSupervisionSumArchA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtSupervisionSazePercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtSupervisionSumSazeA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtSupervisionTasisatPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtSupervisionSumTasisatA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtSupervisionShahrPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtSupervisionSumShahrA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
            ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
            if (ItemDetailManager.DataTable.DefaultView.Count > 0)
            {
                txtSupervisionMapPercentA1.Text = ItemDetailManager[0]["Percent"].ToString();
                temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
                txtSupervisionSumMapA1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
            }
            lblSupervisionSumPercentA1.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentA1.Text) + float.Parse(txtSupervisionSazePercentA1.Text) + float.Parse(txtSupervisionTasisatPercentA1.Text) + float.Parse(txtSupervisionShahrPercentA1.Text) + float.Parse(txtSupervisionMapPercentA1.Text));

            #endregion

            lblSumAllPercentA1.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentA1.Text) + float.Parse(lblSupervisionSumPercentA1.Text));
        }
        #endregion

        //************* Group A0 *************************
        #region GroupA0
    
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.A;
        ItemManager.CurrentFilter += " and No=0";
        txtStepNoAFrom.Text = ItemManager[0]["StepFrom"].ToString();
        txtStepNoATo.Text = ItemManager[0]["StepTo"].ToString();
        txtCountUnitA.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqA.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostA.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentA.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeletonA.SelectedIndex = ComboBoxStructureSkeletonA.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
            txtSupervisionSumCordA.Text = ItemManager[0]["CoordinatorPrice"].ToString();

        ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));
        if (ItemDetailManager.Count == 0)
            return;
        #region Design
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignArchPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumArchA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignSazePercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumSazeA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignTasisatPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumTasisatA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignShahrPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumShahrA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignMapPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumMapA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        lblDesignSumPercentA.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentA.Text) + float.Parse(txtDesignSazePercentA.Text) + float.Parse(txtDesignTasisatPercentA.Text) + float.Parse(txtDesignShahrPercentA.Text) + float.Parse(txtDesignMapPercentA.Text));
        #endregion

        #region Supervision
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionArchPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumArchA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionSazePercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumSazeA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionTasisatPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumTasisatA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionShahrPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumShahrA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionMapPercentA.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumMapA.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        lblSupervisionSumPercentA.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentA.Text) + float.Parse(txtSupervisionSazePercentA.Text) + float.Parse(txtSupervisionTasisatPercentA.Text) + float.Parse(txtSupervisionShahrPercentA.Text) + float.Parse(txtSupervisionMapPercentA.Text));

        #endregion

        lblSumAllPercentA.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentA.Text) + float.Parse(lblSupervisionSumPercentA.Text));
        #endregion

        //************* Group B *************************
        #region GroupB
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.B;
        ItemManager.CurrentFilter += " and No=0";
        txtStepNoBFrom.Text = ItemManager[0]["StepFrom"].ToString();
        txtStepNoBTo.Text = ItemManager[0]["StepTo"].ToString();
        txtCountUnitB.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqB.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostB.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentB.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeletonB.SelectedIndex = ComboBoxStructureSkeletonB.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
            txtSupervisionSumCordB.Text = ItemManager[0]["CoordinatorPrice"].ToString();

        ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));

        #region Design
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignArchPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumArchB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignSazePercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumSazeB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignTasisatPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumTasisatB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignShahrPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumShahrB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignMapPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumMapB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        lblDesignSumPercentB.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentB.Text) + float.Parse(txtDesignSazePercentB.Text) + float.Parse(txtDesignTasisatPercentB.Text) + float.Parse(txtDesignShahrPercentB.Text) + float.Parse(txtDesignMapPercentB.Text));
        #endregion

        #region Supervision
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionArchPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumArchB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionSazePercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumSazeB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionTasisatPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumTasisatB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionShahrPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumShahrB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionMapPercentB.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumMapB.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        lblSupervisionSumPercentB.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentB.Text) + float.Parse(txtSupervisionSazePercentB.Text) + float.Parse(txtSupervisionTasisatPercentB.Text) + float.Parse(txtSupervisionShahrPercentB.Text) + float.Parse(txtSupervisionMapPercentB.Text));
        #endregion

        lblSumAllPercentB.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentB.Text) + float.Parse(lblSupervisionSumPercentB.Text));
        #endregion

        //************* Group C1 *************************
        #region GroupC1
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.C;
        ItemManager.CurrentFilter += " and No=1";
        txtStepNoC1From.Text = ItemManager[0]["StepFrom"].ToString();
        txtStepNoC1To.Text = ItemManager[0]["StepTo"].ToString();
        txtCountUnitC1.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqC1.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostC1.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentC1.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeletonC1.SelectedIndex = ComboBoxStructureSkeletonC1.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
            txtSupervisionSumCordC1.Text = ItemManager[0]["CoordinatorPrice"].ToString();

        ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));

        #region Design
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignArchPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumArchC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignSazePercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumSazeC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignTasisatPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumTasisatC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignShahrPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumShahrC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignMapPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumMapC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        lblDesignSumPercentC1.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentC1.Text) + float.Parse(txtDesignSazePercentC1.Text) + float.Parse(txtDesignTasisatPercentC1.Text) + float.Parse(txtDesignShahrPercentC1.Text) + float.Parse(txtDesignMapPercentC1.Text));

        #endregion

        #region Supervision
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionArchPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumArchC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionSazePercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumSazeC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionTasisatPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumTasisatC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionShahrPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumShahrC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionMapPercentC1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumMapC1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        lblSupervisionSumPercentC1.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentC1.Text) + float.Parse(txtSupervisionSazePercentC1.Text) + float.Parse(txtSupervisionTasisatPercentC1.Text) + float.Parse(txtSupervisionShahrPercentC1.Text) + float.Parse(txtSupervisionMapPercentC1.Text));
        #endregion

        lblSumAllPercentC1.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentC1.Text) + float.Parse(lblSupervisionSumPercentC1.Text));
        #endregion

        //************* Group C2 *************************
        #region GroupC2
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.C;
        ItemManager.CurrentFilter += " and No=2";
        txtStepNoC2From.Text = ItemManager[0]["StepFrom"].ToString();
        txtStepNoC2To.Text = ItemManager[0]["StepTo"].ToString();
        txtCountUnitC2.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqC2.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostC2.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentC2.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeletonC2.SelectedIndex = ComboBoxStructureSkeletonC2.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
            txtSupervisionSumCordC2.Text = ItemManager[0]["CoordinatorPrice"].ToString();

        ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));

        #region Design
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignArchPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumArchC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignSazePercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumSazeC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignTasisatPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumTasisatC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignShahrPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumShahrC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignMapPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumMapC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        lblDesignSumPercentC2.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentC2.Text) + float.Parse(txtDesignSazePercentC2.Text) + float.Parse(txtDesignTasisatPercentC2.Text) + float.Parse(txtDesignShahrPercentC2.Text) + float.Parse(txtDesignMapPercentC2.Text));
        #endregion

        #region Supervision
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionArchPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumArchC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionSazePercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumSazeC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionTasisatPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumTasisatC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionShahrPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumShahrC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionMapPercentC2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumMapC2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        lblSupervisionSumPercentC2.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentC2.Text) + float.Parse(txtSupervisionSazePercentC2.Text) + float.Parse(txtSupervisionTasisatPercentC2.Text) + float.Parse(txtSupervisionShahrPercentC2.Text) + float.Parse(txtSupervisionMapPercentC2.Text));
        #endregion

        lblSumAllPercentC2.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentC2.Text) + float.Parse(lblSupervisionSumPercentC2.Text));

        #endregion

        //************* Group D1 *************************
        #region GroupD1
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D;
        ItemManager.CurrentFilter += " and No=1";
        txtStepNoD1From.Text = ItemManager[0]["StepFrom"].ToString();
        txtStepNoD1To.Text = ItemManager[0]["StepTo"].ToString();
        txtCountUnitD1.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqD1.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostD1.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentD1.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeletonD1.SelectedIndex = ComboBoxStructureSkeletonD1.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
            txtSupervisionSumCordD1.Text = ItemManager[0]["CoordinatorPrice"].ToString();

        ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));

        #region Design
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignArchPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumArchD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignSazePercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumSazeD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignTasisatPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumTasisatD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignShahrPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumShahrD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignMapPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumMapD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        lblDesignSumPercentD1.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentD1.Text) + float.Parse(txtDesignSazePercentD1.Text) + float.Parse(txtDesignTasisatPercentD1.Text) + float.Parse(txtDesignShahrPercentD1.Text) + float.Parse(txtDesignMapPercentD1.Text));
        #endregion

        #region Supervision
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionArchPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumArchD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionSazePercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumSazeD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionTasisatPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumTasisatD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionShahrPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumShahrD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionMapPercentD1.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumMapD1.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        lblSupervisionSumPercentD1.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentD1.Text) + float.Parse(txtSupervisionSazePercentD1.Text) + float.Parse(txtSupervisionTasisatPercentD1.Text) + float.Parse(txtSupervisionShahrPercentD1.Text) + float.Parse(txtSupervisionMapPercentD1.Text));
        #endregion

        lblSumAllPercentD1.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentD1.Text) + float.Parse(lblSupervisionSumPercentD1.Text));
        #endregion

        //************* Group D2 *************************
        #region GroupD2
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D;
        ItemManager.CurrentFilter += " and No=2";
        txtStepNoD2From.Text = ItemManager[0]["StepFrom"].ToString();
        txtStepNoD2To.Text = ItemManager[0]["StepTo"].ToString();
        txtCountUnitD2.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqD2.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostD2.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentD2.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeletonD2.SelectedIndex = ComboBoxStructureSkeletonD2.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
            txtSupervisionSumCordD2.Text = ItemManager[0]["CoordinatorPrice"].ToString();

        ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));

        #region Design
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignArchPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumArchD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignSazePercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumSazeD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignTasisatPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumTasisatD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignShahrPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumShahrD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignMapPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumMapD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        lblDesignSumPercentD2.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentD2.Text) + float.Parse(txtDesignSazePercentD2.Text) + float.Parse(txtDesignTasisatPercentD2.Text) + float.Parse(txtDesignShahrPercentD2.Text) + float.Parse(txtDesignMapPercentD2.Text));
        #endregion

        #region Supervision
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionArchPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumArchD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionSazePercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumSazeD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionTasisatPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumTasisatD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionShahrPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumShahrD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionMapPercentD2.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumMapD2.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        lblSupervisionSumPercentD2.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentD2.Text) + float.Parse(txtSupervisionSazePercentD2.Text) + float.Parse(txtSupervisionTasisatPercentD2.Text) + float.Parse(txtSupervisionShahrPercentD2.Text) + float.Parse(txtSupervisionMapPercentD2.Text));
        #endregion

        lblSumAllPercentD2.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentD2.Text) + float.Parse(lblSupervisionSumPercentD2.Text));
        #endregion

        //************* Group D3 *************************
        #region GroupD3
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D;
        ItemManager.CurrentFilter += " and No=3";
        txtStepNoD3From.Text = ItemManager[0]["StepFrom"].ToString();
        txtCountUnitD3.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqD3.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostD3.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentD3.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
            ComboBoxStructureSkeletonD3.SelectedIndex = ComboBoxStructureSkeletonD3.Items.FindByValue(ItemManager[0]["StructureSkeletonId"]).Index;
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["CoordinatorPrice"]))
            txtSupervisionSumCordD3.Text = ItemManager[0]["CoordinatorPrice"].ToString();

        ItemDetailManager.FindByItemId(int.Parse(ItemManager[0]["ItemId"].ToString()));

        #region Design
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignArchPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumArchD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignSazePercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumSazeD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignTasisatPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumTasisatD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignShahrPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumShahrD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtDesignMapPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtDesignSumMapD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        lblDesignSumPercentD3.Text = RoundFloatNumber(float.Parse(txtDesignArchPercentD3.Text) + float.Parse(txtDesignSazePercentD3.Text) + float.Parse(txtDesignTasisatPercentD3.Text) + float.Parse(txtDesignShahrPercentD3.Text) + float.Parse(txtDesignMapPercentD3.Text));
        #endregion

        #region Supervision
        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionArchPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumArchD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionSazePercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumSazeD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionTasisatPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumTasisatD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionShahrPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumShahrD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        ItemDetailManager.CurrentFilter = "TypeId=" + (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision;
        ItemDetailManager.CurrentFilter += " and MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            txtSupervisionMapPercentD3.Text = ItemDetailManager[0]["Percent"].ToString();
            temp = Decimal.Parse(ItemDetailManager[0]["Price"].ToString()).ToString("#,#").Replace(",", "");
            txtSupervisionSumMapD3.Text = (String.IsNullOrEmpty(temp)) ? "0" : temp;
        }

        lblSupervisionSumPercentD3.Text = RoundFloatNumber(float.Parse(txtSupervisionArchPercentD3.Text) + float.Parse(txtSupervisionSazePercentD3.Text) + float.Parse(txtSupervisionTasisatPercentD3.Text) + float.Parse(txtSupervisionShahrPercentD3.Text) + float.Parse(txtSupervisionMapPercentD3.Text));
        #endregion

        lblSumAllPercentD3.Text = RoundFloatNumber(float.Parse(lblDesignSumPercentD3.Text) + float.Parse(lblSupervisionSumPercentD3.Text));
        #endregion
    }

    String RoundFloatNumber(float Num)
    {
        return Math.Round(double.Parse(Num.ToString()), 2).ToString();
    }


    Boolean CheckInputs()
    {
        Boolean Check = true;
        String Error = "";

        #region MaxInfrastructureArea
        if (String.IsNullOrEmpty(txtMaxSqA.Text))
        {
            Check = false;
            Error = "تعدادی از موارد موجود در 'حداکثر زیربنا' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtMaxSqB.Text))
        {
            Check = false;
            Error = "تعدادی از موارد موجود در 'حداکثر زیربنا' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtMaxSqC1.Text))
        {
            Check = false;
            Error = "تعدادی از موارد موجود در 'حداکثر زیربنا' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtMaxSqC2.Text))
        {
            Check = false;
            Error = "تعدادی از موارد موجود در 'حداکثر زیربنا' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtMaxSqD1.Text))
        {
            Check = false;
            Error = "تعدادی از موارد موجود در 'حداکثر زیربنا' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtMaxSqD2.Text))
        {
            Check = false;
            Error = "تعدادی از موارد موجود در 'حداکثر زیربنا' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtMaxSqD3.Text))
        {
            Check = false;
            Error = "تعدادی از موارد موجود در 'حداکثر زیربنا' وارد نشده است.";
        }
        #endregion

        #region MaxCountUnits
        if (String.IsNullOrEmpty(txtCountUnitA.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'حداکثر تعداد واحد' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCountUnitB.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'حداکثر تعداد واحد' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCountUnitC1.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'حداکثر تعداد واحد' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCountUnitC2.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'حداکثر تعداد واحد' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCountUnitD1.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'حداکثر تعداد واحد' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCountUnitD2.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'حداکثر تعداد واحد' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCountUnitD3.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'حداکثر تعداد واحد' وارد نشده است.";
        }
        #endregion

        #region BuildCost
        if (String.IsNullOrEmpty(txtCostA.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'هزینه ساخت هر مترمربع بنا ' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCostB.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'هزینه ساخت هر مترمربع بنا ' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCostC1.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'هزینه ساخت هر مترمربع بنا ' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCostC2.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'هزینه ساخت هر مترمربع بنا ' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCostD1.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'هزینه ساخت هر مترمربع بنا ' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCostD2.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'هزینه ساخت هر مترمربع بنا ' وارد نشده است.";
        }
        else if (String.IsNullOrEmpty(txtCostD3.Text))
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error += "تعدادی از موارد موجود در 'هزینه ساخت هر مترمربع بنا ' وارد نشده است.";
        }
        #endregion

        #region CheckPercents
        float sum = float.Parse(txtDesignArchPercentA.Text) + float.Parse(txtDesignSazePercentA.Text) + float.Parse(txtDesignTasisatPercentA.Text) + float.Parse(txtDesignShahrPercentA.Text) + float.Parse(txtDesignMapPercentA.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        sum = float.Parse(txtSupervisionArchPercentA.Text) + float.Parse(txtSupervisionSazePercentA.Text) + float.Parse(txtSupervisionTasisatPercentA.Text) + float.Parse(txtSupervisionShahrPercentA.Text) + float.Parse(txtSupervisionMapPercentA.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }

        sum = float.Parse(txtDesignArchPercentB.Text) + float.Parse(txtDesignSazePercentB.Text) + float.Parse(txtDesignTasisatPercentB.Text) + float.Parse(txtDesignShahrPercentB.Text) + float.Parse(txtDesignMapPercentB.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        sum = float.Parse(txtSupervisionArchPercentB.Text) + float.Parse(txtSupervisionSazePercentB.Text) + float.Parse(txtSupervisionTasisatPercentB.Text) + float.Parse(txtSupervisionShahrPercentB.Text) + float.Parse(txtSupervisionMapPercentB.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }

        sum = float.Parse(txtDesignArchPercentC1.Text) + float.Parse(txtDesignSazePercentC1.Text) + float.Parse(txtDesignTasisatPercentC1.Text) + float.Parse(txtDesignShahrPercentC1.Text) + float.Parse(txtDesignMapPercentC1.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        sum = float.Parse(txtSupervisionArchPercentC1.Text) + float.Parse(txtSupervisionSazePercentC1.Text) + float.Parse(txtSupervisionTasisatPercentC1.Text) + float.Parse(txtSupervisionShahrPercentC1.Text) + float.Parse(txtSupervisionMapPercentC1.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }

        sum = float.Parse(txtDesignArchPercentC2.Text) + float.Parse(txtDesignSazePercentC2.Text) + float.Parse(txtDesignTasisatPercentC2.Text) + float.Parse(txtDesignShahrPercentC2.Text) + float.Parse(txtDesignMapPercentC2.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        sum = float.Parse(txtSupervisionArchPercentC2.Text) + float.Parse(txtSupervisionSazePercentC2.Text) + float.Parse(txtSupervisionTasisatPercentC2.Text) + float.Parse(txtSupervisionShahrPercentC2.Text) + float.Parse(txtSupervisionMapPercentC2.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }

        sum = float.Parse(txtDesignArchPercentD1.Text) + float.Parse(txtDesignSazePercentD1.Text) + float.Parse(txtDesignTasisatPercentD1.Text) + float.Parse(txtDesignShahrPercentD1.Text) + float.Parse(txtDesignMapPercentD1.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        sum = float.Parse(txtSupervisionArchPercentD1.Text) + float.Parse(txtSupervisionSazePercentD1.Text) + float.Parse(txtSupervisionTasisatPercentD1.Text) + float.Parse(txtSupervisionShahrPercentD1.Text) + float.Parse(txtSupervisionMapPercentD1.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }

        sum = float.Parse(txtDesignArchPercentD2.Text) + float.Parse(txtDesignSazePercentD2.Text) + float.Parse(txtDesignTasisatPercentD2.Text) + float.Parse(txtDesignShahrPercentD2.Text) + float.Parse(txtDesignMapPercentD2.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        sum = float.Parse(txtSupervisionArchPercentD2.Text) + float.Parse(txtSupervisionSazePercentD2.Text) + float.Parse(txtSupervisionTasisatPercentD2.Text) + float.Parse(txtSupervisionShahrPercentD2.Text) + float.Parse(txtSupervisionMapPercentD2.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }

        sum = float.Parse(txtDesignArchPercentD3.Text) + float.Parse(txtDesignSazePercentD3.Text) + float.Parse(txtDesignTasisatPercentD3.Text) + float.Parse(txtDesignShahrPercentD3.Text) + float.Parse(txtDesignMapPercentD3.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        sum = float.Parse(txtSupervisionArchPercentD3.Text) + float.Parse(txtSupervisionSazePercentD3.Text) + float.Parse(txtSupervisionTasisatPercentD3.Text) + float.Parse(txtSupervisionShahrPercentD3.Text) + float.Parse(txtSupervisionMapPercentD3.Text);
        if (sum > 100)
        {
            Check = false;
            Error += (String.IsNullOrEmpty(Error)) ? "" : "<br>";
            Error = "جمع درصد ها نمی تواند بیشتر از 100 باشد";
        }
        #endregion

        if (Check == false)
            ShowMessage(Error);
        return Check;
    }

    #region Insert-Update
    #region Insert
    void PriceArchiveInsert()
    {

        #region Define Managers-Transaction
        TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemsManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();
        TSP.DataManager.TableTypeManager TableTypeManager = new TSP.DataManager.TableTypeManager();

        Transaction.Add(PriceArchiveManager);
        Transaction.Add(ItemDetailManager);
        Transaction.Add(ItemsManager);
        #endregion
        try
        {
            Transaction.BeginSave();
            #region Insert PriceArchive
            DataRow drPriceArchive = PriceArchiveManager.NewRow();
            drPriceArchive["DocumentDate"] = "";
            drPriceArchive["DocumentNo"] = "";
            drPriceArchive["Year"] = txtYear.Text;
            drPriceArchive["Description"] = txtDescription.Text;
            drPriceArchive["Remark"] = txtRemark.Text;
            if (Session["PriceArchiveFileAddress"] != null && String.IsNullOrEmpty(Session["PriceArchiveFileAddress"].ToString()) == false)
            {
                drPriceArchive["FileUrl"] = Session["PriceArchiveFileAddress"].ToString();// "~/Image/TechnicalServices/PriceArchive/" + System.IO.Path.GetFileName(Session["PriceArchiveFileAddress"].ToString());
            }
            drPriceArchive["CreateDate"] = txtCreateDate.Text;
            drPriceArchive["InActive"] = false;
            drPriceArchive["UserId"] = Utility.GetCurrentUser_UserId();
            drPriceArchive["ModifiedDate"] = DateTime.Now;
            PriceArchiveManager.AddRow(drPriceArchive);
            if (PriceArchiveManager.Save() <= 0)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                Transaction.CancelSave();
                return;
            }
            PriceArchiveManager.DataTable.AcceptChanges();
            #endregion
            string PriceArchiveId = PriceArchiveManager[0]["PriceArchiveId"].ToString();          
            if (!InsertArchieveItemForAllGroup(ItemsManager, ItemDetailManager, PriceArchiveId))
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                Transaction.CancelSave();
                return;
            }

            Transaction.EndSave();
            hiddenSaveID["PrAId"] = Utility.EncryptQS(PriceArchiveManager[0]["PriceArchiveId"].ToString());
            RoundPanelContent.HeaderText = "ویرایش";
            PageMode = "Edit";
            ShowMessage("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            Transaction.CancelSave();
            ShowMessage("خطایی در ذخیره انجام گرفته است");
            if (Utility.ShowExceptionError())
                ShowMessage(err.Message);
            Utility.SaveWebsiteError(err);
            SetError(err);
            return;
        }
        //try
        //{
        //    if (Session["PriceArchiveFileAddress"] != null && String.IsNullOrEmpty(Session["PriceArchiveFileAddress"].ToString()) == false)
        //    {
        //        if (System.IO.File.Exists(Session["PriceArchiveFileAddress"].ToString()) == true)
        //        {
        //            System.IO.File.Move(Session["PriceArchiveFileAddress"].ToString(), MapPath("~/Image/TechnicalServices/PriceArchive/") + System.IO.Path.GetFileName(Session["PriceArchiveFileAddress"].ToString()));
        //        }
        //    }
        //    Session["PriceArchiveFileAddress"] = null;
        //}
        //catch (Exception ex)
        //{
        //    Utility.SaveWebsiteError(ex);
        //}
    }

    private Boolean InsertArchieveItemForAllGroup(TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemsManager, TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager
          , string PriceArchiveId)
    {
        //************ Group A1 گروه ساختمانی الف***********************
        #region GroupA1
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 1, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.A, PriceArchiveId 
            ,txtStepNoA1From.Text, txtStepNoA1To.Text, txtCountUnitA1.Text
            , txtMaxSqA1.Text, txtCostA1.Text, lblSumAllPercentA1.Text
            , txtDesignArchPercentA1.Text, txtDesignSazePercentA1.Text, txtDesignTasisatPercentA1.Text, txtDesignTasisatPercentA1.Text, txtDesignShahrPercentA1.Text, txtDesignMapPercentA1.Text
            , txtDesignSumArchA1.Text, txtDesignSumSazeA1.Text, txtDesignSumTasisatA1.Text, txtDesignSumTasisatA1.Text, txtDesignSumShahrA1.Text, txtDesignSumMapA1.Text
            , txtSupervisionArchPercentA1.Text, txtSupervisionSazePercentA1.Text, txtSupervisionTasisatPercentA1.Text, txtSupervisionTasisatPercentA1.Text, txtSupervisionShahrPercentA1.Text, txtSupervisionMapPercentA1.Text
            , txtSupervisionSumArchA1.Text, txtSupervisionSumSazeA1.Text, txtSupervisionSumTasisatA1.Text, txtSupervisionSumTasisatA1.Text, txtSupervisionSumShahrA1.Text, txtSupervisionSumMapA1.Text, (ComboBoxStructureSkeletonA1.SelectedItem.Value != null) ? ComboBoxStructureSkeletonA1.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordA1.Text)
             )
        {
            return false;
        }
        #endregion

        //************ Group A گروه ساختمانی الف***********************
        #region GroupA
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 0, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.A, PriceArchiveId, txtStepNoAFrom.Text, txtStepNoATo.Text, txtCountUnitA.Text
            , txtMaxSqA.Text, txtCostA.Text, lblSumAllPercentA.Text
            , txtDesignArchPercentA.Text, txtDesignSazePercentA.Text, txtDesignTasisatPercentA.Text, txtDesignTasisatPercentA.Text, txtDesignShahrPercentA.Text, txtDesignMapPercentA.Text
            , txtDesignSumArchA.Text, txtDesignSumSazeA.Text, txtDesignSumTasisatA.Text, txtDesignSumTasisatA.Text, txtDesignSumShahrA.Text, txtDesignSumMapA.Text
            , txtSupervisionArchPercentA.Text, txtSupervisionSazePercentA.Text, txtSupervisionTasisatPercentA.Text, txtSupervisionTasisatPercentA.Text, txtSupervisionShahrPercentA.Text, txtSupervisionMapPercentA.Text
            , txtSupervisionSumArchA.Text, txtSupervisionSumSazeA.Text, txtSupervisionSumTasisatA.Text, txtSupervisionSumTasisatA.Text, txtSupervisionSumShahrA.Text, txtSupervisionSumMapA.Text, (ComboBoxStructureSkeletonA.SelectedItem.Value != null) ? ComboBoxStructureSkeletonA.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordA.Text)
             )
        {
            return false;
        }
        #endregion

        //************ Group B گروه ساختمانی ب***********************
        #region GroupB
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 0, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.B, PriceArchiveId, txtStepNoBFrom.Text, txtStepNoBTo.Text, txtCountUnitB.Text,
             txtMaxSqB.Text, txtCostB.Text, lblSumAllPercentB.Text
            , txtDesignArchPercentB.Text, txtDesignSazePercentB.Text, txtDesignTasisatPercentB.Text, txtDesignTasisatPercentB.Text, txtDesignShahrPercentB.Text, txtDesignMapPercentB.Text
            , txtDesignSumArchB.Text, txtDesignSumSazeB.Text, txtDesignSumTasisatB.Text, txtDesignSumTasisatB.Text, txtDesignSumShahrB.Text, txtDesignSumMapB.Text
            , txtSupervisionArchPercentB.Text, txtSupervisionSazePercentB.Text, txtSupervisionTasisatPercentB.Text, txtSupervisionTasisatPercentB.Text, txtSupervisionShahrPercentB.Text, txtSupervisionMapPercentB.Text
            , txtSupervisionSumArchB.Text, txtSupervisionSumSazeB.Text, txtSupervisionSumTasisatB.Text, txtSupervisionSumTasisatB.Text, txtSupervisionSumShahrB.Text, txtSupervisionSumMapB.Text, (ComboBoxStructureSkeletonB.SelectedItem.Value != null) ? ComboBoxStructureSkeletonB.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordB.Text))
        {
            return false;
        }
        #endregion

        //************ Group C1 گروه ساختمانی ج1***********************
        #region GroupC1
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 1, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.C, PriceArchiveId, txtStepNoC1From.Text, txtStepNoC1To.Text, txtCountUnitC1.Text
            , txtMaxSqC1.Text, txtCostC1.Text, lblSumAllPercentC1.Text
              , txtDesignArchPercentC1.Text, txtDesignSazePercentC1.Text, txtDesignTasisatPercentC1.Text, txtDesignTasisatPercentC1.Text, txtDesignShahrPercentC1.Text, txtDesignMapPercentC1.Text
            , txtDesignSumArchC1.Text, txtDesignSumSazeC1.Text, txtDesignSumTasisatC1.Text, txtDesignSumTasisatC1.Text, txtDesignSumShahrC1.Text, txtDesignSumMapC1.Text
            , txtSupervisionArchPercentC1.Text, txtSupervisionSazePercentC1.Text, txtSupervisionTasisatPercentC1.Text, txtSupervisionTasisatPercentC1.Text, txtSupervisionShahrPercentC1.Text, txtSupervisionMapPercentC1.Text
            , txtSupervisionSumArchC1.Text, txtSupervisionSumSazeC1.Text, txtSupervisionSumTasisatC1.Text, txtSupervisionSumTasisatC1.Text, txtSupervisionSumShahrC1.Text, txtSupervisionSumMapC1.Text, (ComboBoxStructureSkeletonC1.SelectedItem.Value != null) ? ComboBoxStructureSkeletonC1.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordC1.Text))
        {
            return false;
        }
        #endregion

        //************ Group C2 گروه ساختمانی ج2***********************
        #region GroupC2
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 2, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.C, PriceArchiveId, txtStepNoC2From.Text, txtStepNoC2To.Text, txtCountUnitC2.Text
            , txtMaxSqC2.Text, txtCostC2.Text, lblSumAllPercentC2.Text
            , txtDesignArchPercentC2.Text, txtDesignSazePercentC2.Text, txtDesignTasisatPercentC2.Text, txtDesignTasisatPercentC2.Text, txtDesignShahrPercentC2.Text, txtDesignMapPercentC2.Text
            , txtDesignSumArchC2.Text, txtDesignSumSazeC2.Text, txtDesignSumTasisatC2.Text, txtDesignSumTasisatC2.Text, txtDesignSumShahrC2.Text, txtDesignSumMapC2.Text
            , txtSupervisionArchPercentC2.Text, txtSupervisionSazePercentC2.Text, txtSupervisionTasisatPercentC2.Text, txtSupervisionTasisatPercentC2.Text, txtSupervisionShahrPercentC2.Text, txtSupervisionMapPercentC2.Text
            , txtSupervisionSumArchC2.Text, txtSupervisionSumSazeC2.Text, txtSupervisionSumTasisatC2.Text, txtSupervisionSumTasisatC2.Text, txtSupervisionSumShahrC2.Text, txtSupervisionSumMapC2.Text, (ComboBoxStructureSkeletonC2.SelectedItem.Value != null) ? ComboBoxStructureSkeletonC2.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordC2.Text))
        {
            return false;
        }
        #endregion

        //************ Group D1 گروه ساختمانی د1***********************
        #region GroupD1
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 1, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D, PriceArchiveId, txtStepNoD1From.Text, txtStepNoD1To.Text, txtCountUnitD1.Text
            , txtMaxSqD1.Text, txtCostD1.Text, lblSumAllPercentD1.Text
            , txtDesignArchPercentD1.Text, txtDesignSazePercentD1.Text, txtDesignTasisatPercentD1.Text, txtDesignTasisatPercentD1.Text, txtDesignShahrPercentD1.Text, txtDesignMapPercentD1.Text
            , txtDesignSumArchD1.Text, txtDesignSumSazeD1.Text, txtDesignSumTasisatD1.Text, txtDesignSumTasisatD1.Text, txtDesignSumShahrD1.Text, txtDesignSumMapD1.Text
            , txtSupervisionArchPercentD1.Text, txtSupervisionSazePercentD1.Text, txtSupervisionTasisatPercentD1.Text, txtSupervisionTasisatPercentD1.Text, txtSupervisionShahrPercentD1.Text, txtSupervisionMapPercentD1.Text
            , txtSupervisionSumArchD1.Text, txtSupervisionSumSazeD1.Text, txtSupervisionSumTasisatD1.Text, txtSupervisionSumTasisatD1.Text, txtSupervisionSumShahrD1.Text, txtSupervisionSumMapD1.Text, (ComboBoxStructureSkeletonD1.SelectedItem.Value != null) ? ComboBoxStructureSkeletonD1.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordD1.Text))
        {
            return false;
        }
        #endregion

        //************ Group D2 گروه ساختمانی د2***********************
        #region GroupD2
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 2, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D, PriceArchiveId, txtStepNoD2From.Text, txtStepNoD2To.Text, txtCountUnitD2.Text
            , txtMaxSqD2.Text, txtCostD2.Text, lblSumAllPercentD2.Text
            , txtDesignArchPercentD2.Text, txtDesignSazePercentD2.Text, txtDesignTasisatPercentD2.Text, txtDesignTasisatPercentD2.Text, txtDesignShahrPercentD2.Text, txtDesignMapPercentD2.Text
            , txtDesignSumArchD2.Text, txtDesignSumSazeD2.Text, txtDesignSumTasisatD2.Text, txtDesignSumTasisatD2.Text, txtDesignSumShahrD2.Text, txtDesignSumMapD2.Text
            , txtSupervisionArchPercentD2.Text, txtSupervisionSazePercentD2.Text, txtSupervisionTasisatPercentD2.Text, txtSupervisionTasisatPercentD2.Text, txtSupervisionShahrPercentD2.Text, txtSupervisionMapPercentD2.Text
            , txtSupervisionSumArchD2.Text, txtSupervisionSumSazeD2.Text, txtSupervisionSumTasisatD2.Text, txtSupervisionSumTasisatD2.Text, txtSupervisionSumShahrD2.Text, txtSupervisionSumMapD2.Text, (ComboBoxStructureSkeletonD2.SelectedItem.Value != null) ? ComboBoxStructureSkeletonD2.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordD2.Text))
        {
            return false;
        }
        #endregion

        //************ Group D3 گروه ساختمانی د3***********************
        #region GroupD3
        if (!InsertArchieveItem(ItemsManager, ItemDetailManager, 3, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D, PriceArchiveId, txtStepNoD3From.Text, "NULL", txtCountUnitD3.Text
            , txtMaxSqD3.Text, txtCostD3.Text, lblSumAllPercentD3.Text
            , txtDesignArchPercentD3.Text, txtDesignSazePercentD3.Text, txtDesignTasisatPercentD3.Text, txtDesignTasisatPercentD3.Text, txtDesignShahrPercentD3.Text, txtDesignMapPercentD3.Text
            , txtDesignSumArchD3.Text, txtDesignSumSazeD3.Text, txtDesignSumTasisatD3.Text, txtDesignSumTasisatD3.Text, txtDesignSumShahrD3.Text, txtDesignSumMapD3.Text
            , txtSupervisionArchPercentD3.Text, txtSupervisionSazePercentD3.Text, txtSupervisionTasisatPercentD3.Text, txtSupervisionTasisatPercentD3.Text, txtSupervisionShahrPercentD3.Text, txtSupervisionMapPercentD3.Text
            , txtSupervisionSumArchD3.Text, txtSupervisionSumSazeD3.Text, txtSupervisionSumTasisatD3.Text, txtSupervisionSumTasisatD3.Text, txtSupervisionSumShahrD3.Text, txtSupervisionSumMapD3.Text, (ComboBoxStructureSkeletonD3.SelectedItem.Value != null) ? ComboBoxStructureSkeletonD3.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordD3.Text))
        {
            return false;
        }
        #endregion

        ItemDetailManager.Save();
        //ItemDetailManager.DataTable.AcceptChanges();
        return true;
    }

    private Boolean InsertArchieveItem(TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemsManager, TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager
        , int No, int GroupId, String PriceArchiveId, string StepFrom, string StepTo, string MaxCountUnits, string MaxInfrastructureArea
        , string BuildCost, string SumPercents
        , string DesignPercentMemari, string DesignPercentOmran, string DesignPercentTasisat_Bargh, string DesignPercentTasisat_Mechanic, string DesignPercentShahrSazi, string DesignPercentNaghsheBardari
        , string DesignSumMemari, string DesignSumOmran, string DesignSumTasisat_Bargh, string DesignSumTasisat_Mechanic, string DesignSumShahrSazi, string DesignSumNaghsheBardari
        , string SupervisionPercentMemari, string SupervisionPercentOmran, string SupervisionPercentTasisat_Bargh, string SupervisionPercentTasisat_Mechanic, string SupervisionPercentShahrSazi, string SupervisionPercentNaghsheBardari
        , string SupervisionSumMemari, string SupervisionSumOmran, string SupervisionSumTasisat_Bargh, string SupervisionSumTasisat_Mechanic, string SupervisionSumShahrSazi, string SupervisionSumNaghsheBardari,object StructureSkeletonId
        , string CoordinatorPrice
        )
    {
        DataRow drGrpAItem = ItemsManager.NewRow();
        drGrpAItem["No"] = No;
        drGrpAItem["GroupId"] = GroupId;
        drGrpAItem["PriceArchiveId"] = PriceArchiveId;
        drGrpAItem["StepFrom"] = StepFrom;
        if (StepTo == "NULL")
            drGrpAItem["StepTo"] = DBNull.Value;
        else
            drGrpAItem["StepTo"] = StepTo;
        drGrpAItem["MaxCountUnits"] = MaxCountUnits;
        drGrpAItem["MaxInfrastructureArea"] = MaxInfrastructureArea;
        drGrpAItem["BuildCost"] = BuildCost;
        drGrpAItem["SumPercents"] = SumPercents;
        drGrpAItem["StructureSkeletonId"] = StructureSkeletonId;
        drGrpAItem["CoordinatorPrice"] = CoordinatorPrice;
        drGrpAItem["UserId"] = Utility.GetCurrentUser_UserId();
        drGrpAItem["ModifiedDate"] = DateTime.Now;
        ItemsManager.AddRow(drGrpAItem);
        if (ItemsManager.Save() <= 0)
            return false;
        ItemsManager.DataTable.AcceptChanges();
        int ItemId = int.Parse(ItemsManager[ItemsManager.Count - 1]["ItemId"].ToString());

        #region Design

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
        , DesignPercentMemari, (int)TSP.DataManager.MajorManager.ParentMajors.Memari, DesignSumMemari);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
        , DesignPercentOmran, (int)TSP.DataManager.MajorManager.ParentMajors.Omran, DesignSumOmran);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
        , DesignPercentTasisat_Bargh, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh, DesignSumTasisat_Bargh);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
        , DesignPercentTasisat_Mechanic, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Mechanic, DesignSumTasisat_Mechanic);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
        , DesignPercentShahrSazi, (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi, DesignSumShahrSazi);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
        , DesignPercentNaghsheBardari, (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari, DesignSumNaghsheBardari);
        #endregion

        #region Supervision
        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
      , SupervisionPercentMemari, (int)TSP.DataManager.MajorManager.ParentMajors.Memari, SupervisionSumMemari);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
          , SupervisionPercentOmran, (int)TSP.DataManager.MajorManager.ParentMajors.Omran, SupervisionSumOmran);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
         , SupervisionPercentTasisat_Bargh, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh, SupervisionSumTasisat_Bargh);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
         , SupervisionPercentTasisat_Mechanic, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Mechanic, SupervisionSumTasisat_Mechanic);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
         , SupervisionPercentShahrSazi, (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi, SupervisionSumShahrSazi);

        InsertArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
         , SupervisionPercentNaghsheBardari, (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari, SupervisionSumNaghsheBardari);
        #endregion}

        return true;

    }

    private void InsertArchiveItemDetail(TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager
        , int ItemId, int TypeId, string Percent, int MajorId, string Price)
    {
        DataRow drGrpAItemDetail1 = ItemDetailManager.NewRow();
        drGrpAItemDetail1["ItemId"] = ItemId;
        drGrpAItemDetail1["TypeId"] = TypeId;
        drGrpAItemDetail1["Percent"] = Percent;
        drGrpAItemDetail1["MajorId"] = MajorId;
        drGrpAItemDetail1["Price"] = Price;
        drGrpAItemDetail1["UserId"] = Utility.GetCurrentUser_UserId();
        drGrpAItemDetail1["ModifiedDate"] = DateTime.Now;
        ItemDetailManager.AddRow(drGrpAItemDetail1);
        ItemDetailManager.Save();
        ItemDetailManager.DataTable.AcceptChanges();
    }

    #endregion

    #region Update
    void PriceArchiveUpdate()
    {
        #region Define Manager-Transaction
        TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemsManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();

        Transaction.Add(PriceArchiveManager);
        Transaction.Add(ItemDetailManager);
        Transaction.Add(ItemsManager);
        #endregion

        String OldImageUrl = "";

        try
        {
            Transaction.BeginSave();

            #region Update PriceArchieve
            PriceArchiveManager.FindById(int.Parse(Utility.DecryptQS(hiddenSaveID["PrAId"].ToString())));
            if (PriceArchiveManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                Transaction.CancelSave();
                return;
            }
            PriceArchiveManager[0].BeginEdit();
            PriceArchiveManager[0]["DocumentDate"] = "";
            PriceArchiveManager[0]["DocumentNo"] ="";
            PriceArchiveManager[0]["Year"] = txtYear.Text;
            PriceArchiveManager[0]["Description"] = txtDescription.Text;
            PriceArchiveManager[0]["Remark"] = txtRemark.Text;
            if (Session["PriceArchiveFileAddress"] != null && String.IsNullOrEmpty(Session["PriceArchiveFileAddress"].ToString()) == false)
            {
                OldImageUrl = PriceArchiveManager[0]["FileUrl"].ToString();
                PriceArchiveManager[0]["FileUrl"] = Session["PriceArchiveFileAddress"].ToString();// "~/Image/TechnicalServices/PriceArchive/" + System.IO.Path.GetFileName(Session["PriceArchiveFileAddress"].ToString());
            }
            PriceArchiveManager[0]["CreateDate"] = txtCreateDate.Text;
            PriceArchiveManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PriceArchiveManager[0]["ModifiedDate"] = DateTime.Now;
            PriceArchiveManager[0].EndEdit();
            if (PriceArchiveManager.Save() <= 0)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                Transaction.CancelSave();
                return;
            }
            PriceArchiveManager.DataTable.AcceptChanges();
            #endregion

            string PriceArchiveId = Utility.DecryptQS(hiddenSaveID["PrAId"].ToString());
            ItemsManager.FindByPriceArchiveId(int.Parse(PriceArchiveId));
            if (ItemsManager.Count == 0)
                InsertArchieveItemForAllGroup(ItemsManager, ItemDetailManager, PriceArchiveId);
            else
                UpdateArchiveItemsForAllGroup(ItemsManager, ItemDetailManager, PriceArchiveId);

            Transaction.EndSave();
            ShowMessage("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            Transaction.CancelSave();
            ShowMessage("خطایی در ذخیره انجام گرفته است");
            if (Utility.ShowExceptionError())
                ShowMessage(err.Message);
            Utility.SaveWebsiteError(err);
            SetError(err);
            return;
        }
        //try
        //{
        //    if (Session["PriceArchiveFileAddress"] != null && String.IsNullOrEmpty(Session["PriceArchiveFileAddress"].ToString()) == false)
        //    {
        //        if (System.IO.File.Exists(Session["PriceArchiveFileAddress"].ToString()) == true)
        //        {
        //            System.IO.File.Move(Session["PriceArchiveFileAddress"].ToString(), MapPath("~/Image/TechnicalServices/PriceArchive/") + System.IO.Path.GetFileName(Session["PriceArchiveFileAddress"].ToString()));
        //        }
        //        if (String.IsNullOrEmpty(OldImageUrl) == false)
        //            if (System.IO.File.Exists(MapPath(OldImageUrl)))
        //                System.IO.File.Delete(MapPath(OldImageUrl));
        //    }
        //    Session["PriceArchiveFileAddress"] = null;
        //}
        //catch (Exception ex)
        //{
        //    Utility.SaveWebsiteError(ex);
        //}
    }

    private void UpdateArchiveItemsForAllGroup(TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemsManager, TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager
        , String PriceArchiveId)
    {
        //************ Group A1 ***********************
        #region GroupA1
        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.A, 1
            , txtStepNoA1From.Text, txtStepNoA1To.Text, txtCountUnitA1.Text, txtMaxSqA1.Text, txtCostA1.Text, lblSumAllPercentA1.Text
            , txtDesignArchPercentA1.Text, txtDesignSazePercentA1.Text, txtDesignTasisatPercentA1.Text, txtDesignTasisatPercentA1.Text, txtDesignShahrPercentA1.Text, txtDesignMapPercentA1.Text
            , txtDesignSumArchA1.Text, txtDesignSumSazeA1.Text, txtDesignSumTasisatA1.Text, txtDesignSumTasisatA1.Text, txtDesignSumShahrA1.Text, txtDesignSumMapA1.Text
            , txtSupervisionArchPercentA1.Text, txtSupervisionSazePercentA1.Text, txtSupervisionTasisatPercentA1.Text, txtSupervisionTasisatPercentA1.Text, txtSupervisionShahrPercentA1.Text, txtSupervisionMapPercentA1.Text
            , txtSupervisionSumArchA1.Text, txtSupervisionSumSazeA1.Text, txtSupervisionSumTasisatA1.Text, txtSupervisionSumTasisatA1.Text, txtSupervisionSumShahrA1.Text, txtSupervisionSumMapA1.Text, (ComboBoxStructureSkeletonA1.SelectedItem.Value != null) ? ComboBoxStructureSkeletonA1.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordA1.Text);

        #endregion

        //************ Group A0 ***********************
        #region GroupA0
        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.A, 0
            , txtStepNoAFrom.Text, txtStepNoATo.Text, txtCountUnitA.Text, txtMaxSqA.Text, txtCostA.Text, lblSumAllPercentA.Text
            , txtDesignArchPercentA.Text, txtDesignSazePercentA.Text, txtDesignTasisatPercentA.Text, txtDesignTasisatPercentA.Text, txtDesignShahrPercentA.Text, txtDesignMapPercentA.Text
            , txtDesignSumArchA.Text, txtDesignSumSazeA.Text, txtDesignSumTasisatA.Text, txtDesignSumTasisatA.Text, txtDesignSumShahrA.Text, txtDesignSumMapA.Text
            , txtSupervisionArchPercentA.Text, txtSupervisionSazePercentA.Text, txtSupervisionTasisatPercentA.Text, txtSupervisionTasisatPercentA.Text, txtSupervisionShahrPercentA.Text, txtSupervisionMapPercentA.Text
            , txtSupervisionSumArchA.Text, txtSupervisionSumSazeA.Text, txtSupervisionSumTasisatA.Text, txtSupervisionSumTasisatA.Text, txtSupervisionSumShahrA.Text, txtSupervisionSumMapA.Text, (ComboBoxStructureSkeletonA.SelectedItem.Value != null) ? ComboBoxStructureSkeletonA.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordA.Text);

        #endregion

        //************ Group B ***********************
        #region GroupB
        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.B, 0
           , txtStepNoBFrom.Text, txtStepNoBTo.Text, txtCountUnitB.Text, txtMaxSqB.Text, txtCostB.Text, lblSumAllPercentB.Text
           , txtDesignArchPercentB.Text, txtDesignSazePercentB.Text, txtDesignTasisatPercentB.Text, txtDesignTasisatPercentB.Text, txtDesignShahrPercentB.Text, txtDesignMapPercentB.Text
           , txtDesignSumArchB.Text, txtDesignSumSazeB.Text, txtDesignSumTasisatB.Text, txtDesignSumTasisatB.Text, txtDesignSumShahrB.Text, txtDesignSumMapB.Text
           , txtSupervisionArchPercentB.Text, txtSupervisionSazePercentB.Text, txtSupervisionTasisatPercentB.Text, txtSupervisionTasisatPercentB.Text, txtSupervisionShahrPercentB.Text, txtSupervisionMapPercentB.Text
           , txtSupervisionSumArchB.Text, txtSupervisionSumSazeB.Text, txtSupervisionSumTasisatB.Text, txtSupervisionSumTasisatB.Text, txtSupervisionSumShahrB.Text, txtSupervisionSumMapB.Text, (ComboBoxStructureSkeletonB.SelectedItem.Value != null) ? ComboBoxStructureSkeletonB.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordB.Text);
        #endregion

        //************ Group C1 ***********************
        #region GroupC1
        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.C, 1
          , txtStepNoC1From.Text, txtStepNoC1To.Text, txtCountUnitC1.Text, txtMaxSqC1.Text, txtCostC1.Text, lblSumAllPercentC1.Text
          , txtDesignArchPercentC1.Text, txtDesignSazePercentC1.Text, txtDesignTasisatPercentC1.Text, txtDesignTasisatPercentC1.Text, txtDesignShahrPercentC1.Text, txtDesignMapPercentC1.Text
          , txtDesignSumArchC1.Text, txtDesignSumSazeC1.Text, txtDesignSumTasisatC1.Text, txtDesignSumTasisatC1.Text, txtDesignSumShahrC1.Text, txtDesignSumMapC1.Text
          , txtSupervisionArchPercentC1.Text, txtSupervisionSazePercentC1.Text, txtSupervisionTasisatPercentC1.Text, txtSupervisionTasisatPercentC1.Text, txtSupervisionShahrPercentC1.Text, txtSupervisionMapPercentC1.Text
          , txtSupervisionSumArchC1.Text, txtSupervisionSumSazeC1.Text, txtSupervisionSumTasisatC1.Text, txtSupervisionSumTasisatC1.Text, txtSupervisionSumShahrC1.Text, txtSupervisionSumMapC1.Text, (ComboBoxStructureSkeletonC1.SelectedItem.Value != null) ? ComboBoxStructureSkeletonC1.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordC1.Text);

        #endregion

        //************ Group C2 ***********************
        #region GroupC2
        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.C, 2
        , txtStepNoC2From.Text, txtStepNoC2To.Text, txtCountUnitC2.Text, txtMaxSqC2.Text, txtCostC2.Text, lblSumAllPercentC2.Text
        , txtDesignArchPercentC2.Text, txtDesignSazePercentC2.Text, txtDesignTasisatPercentC2.Text, txtDesignTasisatPercentC2.Text, txtDesignShahrPercentC2.Text, txtDesignMapPercentC2.Text
        , txtDesignSumArchC2.Text, txtDesignSumSazeC2.Text, txtDesignSumTasisatC2.Text, txtDesignSumTasisatC2.Text, txtDesignSumShahrC2.Text, txtDesignSumMapC2.Text
        , txtSupervisionArchPercentC2.Text, txtSupervisionSazePercentC2.Text, txtSupervisionTasisatPercentC2.Text, txtSupervisionTasisatPercentC2.Text, txtSupervisionShahrPercentC2.Text, txtSupervisionMapPercentC2.Text
        , txtSupervisionSumArchC2.Text, txtSupervisionSumSazeC2.Text, txtSupervisionSumTasisatC2.Text, txtSupervisionSumTasisatC2.Text, txtSupervisionSumShahrC2.Text, txtSupervisionSumMapC2.Text, (ComboBoxStructureSkeletonC2.SelectedItem.Value != null) ? ComboBoxStructureSkeletonC2.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordC2.Text);
        #endregion

        //************ Group D1 ***********************
        #region GroupD1
        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D, 1
        , txtStepNoD1From.Text, txtStepNoD1To.Text, txtCountUnitD1.Text, txtMaxSqD1.Text, txtCostD1.Text, lblSumAllPercentD1.Text
        , txtDesignArchPercentD1.Text, txtDesignSazePercentD1.Text, txtDesignTasisatPercentD1.Text, txtDesignTasisatPercentD1.Text, txtDesignShahrPercentD1.Text, txtDesignMapPercentD1.Text
        , txtDesignSumArchD1.Text, txtDesignSumSazeD1.Text, txtDesignSumTasisatD1.Text, txtDesignSumTasisatD1.Text, txtDesignSumShahrD1.Text, txtDesignSumMapD1.Text
        , txtSupervisionArchPercentD1.Text, txtSupervisionSazePercentD1.Text, txtSupervisionTasisatPercentD1.Text, txtSupervisionTasisatPercentD1.Text, txtSupervisionShahrPercentD1.Text, txtSupervisionMapPercentD1.Text
        , txtSupervisionSumArchD1.Text, txtSupervisionSumSazeD1.Text, txtSupervisionSumTasisatD1.Text, txtSupervisionSumTasisatD1.Text, txtSupervisionSumShahrD1.Text, txtSupervisionSumMapD1.Text, (ComboBoxStructureSkeletonD1.SelectedItem.Value != null) ? ComboBoxStructureSkeletonD1.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordD1.Text);
        #endregion

        //************ Group D2 ***********************
        #region GroupD2
        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D, 2
        , txtStepNoD2From.Text, txtStepNoD2To.Text, txtCountUnitD2.Text, txtMaxSqD2.Text, txtCostD2.Text, lblSumAllPercentD2.Text
        , txtDesignArchPercentD2.Text, txtDesignSazePercentD2.Text, txtDesignTasisatPercentD2.Text, txtDesignTasisatPercentD2.Text, txtDesignShahrPercentD2.Text, txtDesignMapPercentD2.Text
        , txtDesignSumArchD2.Text, txtDesignSumSazeD2.Text, txtDesignSumTasisatD2.Text, txtDesignSumTasisatD2.Text, txtDesignSumShahrD2.Text, txtDesignSumMapD2.Text
        , txtSupervisionArchPercentD2.Text, txtSupervisionSazePercentD2.Text, txtSupervisionTasisatPercentD2.Text, txtSupervisionTasisatPercentD2.Text, txtSupervisionShahrPercentD2.Text, txtSupervisionMapPercentD2.Text
        , txtSupervisionSumArchD2.Text, txtSupervisionSumSazeD2.Text, txtSupervisionSumTasisatD2.Text, txtSupervisionSumTasisatD2.Text, txtSupervisionSumShahrD2.Text, txtSupervisionSumMapD2.Text, (ComboBoxStructureSkeletonD2.SelectedItem.Value != null) ? ComboBoxStructureSkeletonD2.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordD2.Text);
        #endregion

        //************ Group D3 ***********************
        #region GroupD3

        UpdateArchiveItems(ItemsManager, ItemDetailManager, PriceArchiveId, (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.D, 3
        , txtStepNoD3From.Text, "NULL", txtCountUnitD3.Text, txtMaxSqD3.Text, txtCostD3.Text, lblSumAllPercentD3.Text
        , txtDesignArchPercentD3.Text, txtDesignSazePercentD3.Text, txtDesignTasisatPercentD3.Text, txtDesignTasisatPercentD3.Text, txtDesignShahrPercentD3.Text, txtDesignMapPercentD3.Text
        , txtDesignSumArchD3.Text, txtDesignSumSazeD3.Text, txtDesignSumTasisatD3.Text, txtDesignSumTasisatD3.Text, txtDesignSumShahrD3.Text, txtDesignSumMapD3.Text
        , txtSupervisionArchPercentD3.Text, txtSupervisionSazePercentD3.Text, txtSupervisionTasisatPercentD3.Text, txtSupervisionTasisatPercentD3.Text, txtSupervisionShahrPercentD3.Text, txtSupervisionMapPercentD3.Text
        , txtSupervisionSumArchD3.Text, txtSupervisionSumSazeD3.Text, txtSupervisionSumTasisatD3.Text, txtSupervisionSumTasisatD3.Text, txtSupervisionSumShahrD3.Text, txtSupervisionSumMapD3.Text, (ComboBoxStructureSkeletonD3.SelectedItem.Value != null) ? ComboBoxStructureSkeletonD3.SelectedItem.Value : DBNull.Value, txtSupervisionSumCordD3.Text);
        #endregion
        //ItemDetailManager.Save();
        //ItemDetailManager.DataTable.AcceptChanges();
    }

    private void UpdateArchiveItems(TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemsManager, TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager
        , String PriceArchiveId, int GroupId, int No, string StepFrom, string StepTo, string MaxCountUnits, string MaxInfrastructureArea
        , string BuildCost, string SumPercents
        , string DesignPercentMemari, string DesignPercentOmran, string DesignPercentTasisat_Bargh, string DesignPercentTasisat_Mechanic, string DesignPercentShahrSazi, string DesignPercentNaghsheBardari
        , string DesignSumMemari, string DesignSumOmran, string DesignSumTasisat_Bargh, string DesignSumTasisat_Mechanic, string DesignSumShahrSazi, string DesignSumNaghsheBardari
        , string SupervisionPercentMemari, string SupervisionPercentOmran, string SupervisionPercentTasisat_Bargh, string SupervisionPercentTasisat_Mechanic, string SupervisionPercentShahrSazi, string SupervisionPercentNaghsheBardari
        , string SupervisionSumMemari, string SupervisionSumOmran, string SupervisionSumTasisat_Bargh, string SupervisionSumTasisat_Mechanic, string SupervisionSumShahrSazi, string SupervisionSumNaghsheBardari, object StructureSkeletonId
        , string CoordinatorPrice
         )
    {
        ItemsManager.CurrentFilter = "GroupId=" + GroupId;
        ItemsManager.CurrentFilter += " and No=" + No.ToString();
        if (ItemsManager.DataTable.DefaultView.Count > 0)
        {
            ItemsManager[0].BeginEdit();
            ItemsManager[0]["StepFrom"] = StepFrom;
            if (StepTo == "NULL")
                ItemsManager[0]["StepTo"] = DBNull.Value;
            else
                ItemsManager[0]["StepTo"] = StepTo;
            ItemsManager[0]["MaxCountUnits"] = MaxCountUnits;
            ItemsManager[0]["MaxInfrastructureArea"] = MaxInfrastructureArea;
            ItemsManager[0]["BuildCost"] = BuildCost;
            ItemsManager[0]["SumPercents"] = SumPercents;
            ItemsManager[0]["StructureSkeletonId"] = StructureSkeletonId;
            ItemsManager[0]["CoordinatorPrice"] = CoordinatorPrice;
            ItemsManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ItemsManager[0]["ModifiedDate"] = DateTime.Now;
            ItemsManager[0].EndEdit();
            ItemsManager.Save();
            ItemsManager.DataTable.AcceptChanges();

            int ItemId = int.Parse(ItemsManager[0]["ItemId"].ToString());
            ItemDetailManager.FindByItemId(int.Parse(ItemsManager[0]["ItemId"].ToString()));

            #region Design

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
            , DesignPercentMemari, (int)TSP.DataManager.MajorManager.ParentMajors.Memari, DesignSumMemari);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
            , DesignPercentOmran, (int)TSP.DataManager.MajorManager.ParentMajors.Omran, DesignSumOmran);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
            , DesignPercentTasisat_Bargh, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh, DesignSumTasisat_Bargh);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
            , DesignPercentTasisat_Mechanic, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Mechanic, DesignSumTasisat_Mechanic);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
            , DesignPercentShahrSazi, (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi, DesignSumShahrSazi);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Design
            , DesignPercentNaghsheBardari, (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari, DesignSumNaghsheBardari);
            #endregion

            #region Supervision
            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
            , SupervisionPercentMemari, (int)TSP.DataManager.MajorManager.ParentMajors.Memari, SupervisionSumMemari);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
              , SupervisionPercentOmran, (int)TSP.DataManager.MajorManager.ParentMajors.Omran, SupervisionSumOmran);       

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
             , SupervisionPercentTasisat_Bargh, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Bargh, SupervisionSumTasisat_Bargh);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
             , SupervisionPercentTasisat_Mechanic, (int)TSP.DataManager.MajorManager.ParentMajors.Tasisat_Mechanic, SupervisionSumTasisat_Mechanic);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
             , SupervisionPercentShahrSazi, (int)TSP.DataManager.MajorManager.ParentMajors.ShahrSazi, SupervisionSumShahrSazi);

            UpdateArchiveItemDetail(ItemDetailManager, ItemId, (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision
             , SupervisionPercentNaghsheBardari, (int)TSP.DataManager.MajorManager.ParentMajors.NaghsheBardari, SupervisionSumNaghsheBardari);
            #endregion}
        }
        else //*****Insert Item
        {
            InsertArchieveItem(ItemsManager, ItemDetailManager, No, GroupId, PriceArchiveId, StepFrom, StepTo, MaxCountUnits, MaxInfrastructureArea, BuildCost, SumPercents
                                , DesignPercentMemari, DesignPercentOmran, DesignPercentTasisat_Bargh, DesignPercentTasisat_Mechanic, DesignPercentShahrSazi, DesignPercentNaghsheBardari
                                , DesignSumMemari, DesignSumOmran, DesignSumTasisat_Bargh, DesignSumTasisat_Mechanic, DesignSumShahrSazi, DesignSumNaghsheBardari
                                , SupervisionPercentMemari, SupervisionPercentOmran, SupervisionPercentTasisat_Bargh, SupervisionPercentTasisat_Mechanic, SupervisionPercentShahrSazi, SupervisionPercentNaghsheBardari
                                , SupervisionSumMemari, SupervisionSumOmran, SupervisionSumTasisat_Bargh, SupervisionSumTasisat_Mechanic, SupervisionSumShahrSazi, SupervisionSumNaghsheBardari,  StructureSkeletonId
        ,  CoordinatorPrice);
        }
    }

    private void UpdateArchiveItemDetail(TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager
                                        , int ItemId, int TypeId, string Percent, int MajorId, string Price)
    {
        ItemDetailManager.CurrentFilter = "TypeId=" + TypeId;
        ItemDetailManager.CurrentFilter += " and MajorId=" + MajorId;
        if (ItemDetailManager.DataTable.DefaultView.Count > 0)
        {
            ItemDetailManager[0].BeginEdit();
            ItemDetailManager[0]["Percent"] = Percent;
            ItemDetailManager[0]["Price"] = Price;
            ItemDetailManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ItemDetailManager[0]["ModifiedDate"] = DateTime.Now;
            ItemDetailManager[0].EndEdit();
            ItemDetailManager.Save();
            ItemDetailManager.DataTable.AcceptChanges();
        }
        else
        {
            InsertArchiveItemDetail(ItemDetailManager, ItemId, TypeId, Percent, MajorId, Price);
        }
    }

    #endregion
    #endregion

    #region Set Error
    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601 || se.Number == 2627)
            {
                if (se.Message.Contains("IX_TS.PriceArchive_Year"))
                    ShowMessage("قبلا برای این سال، نرخ خدمات مهندسی ثبت شده است");
                else
                    ShowMessage("اطلاعات تکراری می باشد");
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

    void ShowMessage(String Message)
    {
        LabelWarning.Text = Message;
        DivReport.Visible = true;
    }
    #endregion
    #endregion
}
