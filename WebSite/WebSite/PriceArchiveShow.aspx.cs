using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PriceArchiveShow : System.Web.UI.Page
{
    #region Properties
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
                SetKey();
                SetMode();
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PriceArchive.aspx");
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        if (String.IsNullOrEmpty(Request.QueryString["PrAId"]))
            Response.Redirect("PriceArchive.aspx", false);
        PrAId = int.Parse(Utility.DecryptQS(Request.QueryString["PrAId"]));

    }

    private void SetMode()
    {
        Load_PageData(PrAId);        
    }

    void Load_PageData(int PrAId)
    {
        #region Manager
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager ItemManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager ItemDetailManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailManager();
        TSP.DataManager.TechnicalServices.StructureSkeletonManager structureSkeletonManager = new TSP.DataManager.TechnicalServices.StructureSkeletonManager();
        #endregion

        PriceArchiveManager.FindById(PrAId);
        structureSkeletonManager.FindByStructureSkeletonId(-1);
        
        txtYear.Text = PriceArchiveManager[0]["Year"].ToString();
        txtDescription.Text = PriceArchiveManager[0]["Description"].ToString();
        txtRemark.Text = PriceArchiveManager[0]["Remark"].ToString();
        txtCreateDate.Text = PriceArchiveManager[0]["CreateDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(PriceArchiveManager[0]["FileUrl"]))
            HeyperLinkImg.NavigateUrl = PriceArchiveManager[0]["FileUrl"].ToString();
        String temp = "";
        //************* Group A1 *************************
        #region GroupA1
        ItemManager.FindByPriceArchiveId(PrAId);
        if (ItemManager.Count == 0)
            return;
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
            {
                structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
                if (structureSkeletonManager.Count != 0)
                    ComboBoxStructureSkeletonA1.Text = structureSkeletonManager[0]["Title"].ToString();
            }
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

        //************* Group A *************************
        #region GroupA
      
        ItemManager.CurrentFilter = "GroupId=" + (int)TSP.DataManager.TechnicalServices.StructureGroupsManager.Groups.A;
        ItemManager.CurrentFilter += " and No=0";
        txtStepNoAFrom.Text = ItemManager[0]["StepFrom"].ToString();
        txtStepNoATo.Text = ItemManager[0]["StepTo"].ToString();
        txtCountUnitA.Text = ItemManager[0]["MaxCountUnits"].ToString();
        txtMaxSqA.Text = ItemManager[0]["MaxInfrastructureArea"].ToString();
        txtCostA.Text = Decimal.Parse(ItemManager[0]["BuildCost"].ToString()).ToString("#,#").Replace(",", "");
        lblSumAllPercentA.Text = ItemManager[0]["SumPercents"].ToString();
        if (!Utility.IsDBNullOrNullValue(ItemManager[0]["StructureSkeletonId"]))
        {
            structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
            if (structureSkeletonManager.Count != 0)
                ComboBoxStructureSkeletonA.Text = structureSkeletonManager[0]["Title"].ToString();
        }
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
        {
            structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
            if (structureSkeletonManager.Count != 0)
                ComboBoxStructureSkeletonB.Text = structureSkeletonManager[0]["Title"].ToString();
        }
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
        {
            structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
            if (structureSkeletonManager.Count != 0)
                ComboBoxStructureSkeletonC1.Text = structureSkeletonManager[0]["Title"].ToString();
        }
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
        {
            structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
            if (structureSkeletonManager.Count != 0)
                ComboBoxStructureSkeletonC2.Text = structureSkeletonManager[0]["Title"].ToString();
        }
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
        {
            structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
            if (structureSkeletonManager.Count != 0)
                ComboBoxStructureSkeletonD1.Text = structureSkeletonManager[0]["Title"].ToString();
        }
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
        {
            structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
            if (structureSkeletonManager.Count != 0)
                ComboBoxStructureSkeletonD2.Text = structureSkeletonManager[0]["Title"].ToString();
        }
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
        {
            structureSkeletonManager.CurrentFilter = "StructureSkeletonId=" + Convert.ToInt32(ItemManager[0]["StructureSkeletonId"]);
            if (structureSkeletonManager.Count != 0)
                ComboBoxStructureSkeletonD3.Text = structureSkeletonManager[0]["Title"].ToString();
        }
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