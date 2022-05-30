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

public partial class Employee_MembersRegister_MemberLicence : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    string _Mode
    {
        get
        {
            return HDMode.Value;
        }
        set
        {
            HDMode.Value = value;
        }
    }

    int _MeId
    {
        get
        {
            return Convert.ToInt32(MemberId.Value);
        }
        set
        {
            MemberId.Value = value.ToString();
        }
    }

    int _MReId
    {
        get
        {
            return Convert.ToInt32(MemberRequest.Value);
        }
        set
        {
            MemberRequest.Value = value.ToString();
        }
    }

    #region Events
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
            SetKey();
        }

        if (Session["FillMeLicence"] != null)
        {
            GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
            if (_Mode == "Request")
                GridViewMemberLicence.KeyFieldName = "MlId";
            else if (_Mode == "TempMe")
                GridViewMemberLicence.KeyFieldName = "TMlId";
            GridViewMemberLicence.DataBind();

        }
        else
            FillGrid();

        string MemberName = FillMemberName();
        Session["DataTable"] = GridViewMemberLicence.Columns;
        Session["DataSource"] = Session["FillMeLicence"];
        Session["Title"] = "مدارک تحصیلی";
        Session["Header"] = "نام : " + MemberName + " " + "کد عضویت : " + _MeId;


        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = btnActive.Enabled = btnActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberLicenceInsert.aspx?MlId=" + Utility.EncryptQS("-1") + "&aPageMode=" + Utility.EncryptQS("New") + "&MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode) //////+ "&TP=" + Request.QueryString["TP"]
            + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int MlId = -1;
            int MReId = -1;

            if (GridViewMemberLicence.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                    GridViewMemberLicence.DataBind();
                }
                else
                    FillGrid();

                DataRow row = GridViewMemberLicence.GetDataRow(GridViewMemberLicence.FocusedRowIndex);

                if (_Mode == "Request")
                    MlId = (int)row["MlId"];
                else if (_Mode == "TempMe")
                    MlId = (int)row["TMlId"];
                MReId = (int)row["MReId"];

            }
            if (MlId == -1)
            {
                ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");

            }
            else
            {
                if (MReId == _MReId)
                {
                    if (CheckPermitionForEdit(MReId))
                        Response.Redirect("MemberLicenceInsert.aspx?MlId=" + Utility.EncryptQS(MlId.ToString()) + "&aPageMode=" + Utility.EncryptQS("Edit") + "&MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode) //////+ "&TP=" + Request.QueryString["TP"]
                            + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                    else
                    {
                        ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد");
                    }

                }
                else
                {
                    ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
                }


            }
        }
        catch (Exception)
        {
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void btnEditInActive_Click(object sender, EventArgs e)
    {
        try
        {
            int MlId = -1;
            int MReId = -1;

            if (GridViewMemberLicence.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                    GridViewMemberLicence.DataBind();
                }
                else
                    FillGrid();

                DataRow row = GridViewMemberLicence.GetDataRow(GridViewMemberLicence.FocusedRowIndex);

                if (_Mode == "Request")
                    MlId = (int)row["MlId"];
                else if (_Mode == "TempMe")
                    MlId = (int)row["TMlId"];
                MReId = (int)row["MReId"];

            }
            if (MlId == -1)
            {
                ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");
                return;
            }
            TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
            RequestInActivesManager.FindByReqIdAndTableId(_MReId, MlId);
            if (RequestInActivesManager.Count == 1)
            {
                ShowMessage("مدرک انتخاب شده غیرفعال می باشد");
                return;
            }
            //if (MReId == _MReId)
            //{
            if (CheckPermitionForEdit(_MReId))
                Response.Redirect("MemberLicenceInsert.aspx?MlId=" + Utility.EncryptQS(MlId.ToString()) + "&aPageMode=" + Utility.EncryptQS("EditInActive") + "&MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            else
            {
                ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد");
            }

            //}
            //else
            //{
            //    ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
            //}

        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            int MlId = -1;

            if (GridViewMemberLicence.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                    GridViewMemberLicence.DataBind();
                }
                else
                    FillGrid();

                DataRow row = GridViewMemberLicence.GetDataRow(GridViewMemberLicence.FocusedRowIndex);
                if (_Mode == "Request")
                    MlId = (int)row["MlId"];
                else if (_Mode == "TempMe")
                    MlId = (int)row["TMlId"];

            }
            if (MlId == -1)
            {
                ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");

            }
            else
            {
                Response.Redirect("MemberLicenceInsert.aspx?MlId=" + Utility.EncryptQS(MlId.ToString()) + "&aPageMode=" + Utility.EncryptQS("View") + "&MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString())
                    + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

            }
        }
        catch (Exception)
        {
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        string TempMe = "";
        if (_Mode == "Request")
            TempMe = "0";
        else if (_Mode == "TempMe")
            TempMe = "1";
        if (_Mode == "Home")
        {
            Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]); //////////+ "&TP=" + Request.QueryString["TP"]);

        }
        else
            Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Request.QueryString["MReId"]
                + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
         + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));

    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();

        trans.Add(LiManager);

        try
        {
            int MlId = -1;
            int MReId = -1;
            string InActiveName = "";
            int Inactive = -1;
            if (GridViewMemberLicence.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                    GridViewMemberLicence.DataBind();
                }
                else
                    FillGrid();

                DataRow row = GridViewMemberLicence.GetDataRow(GridViewMemberLicence.FocusedRowIndex);
                if (_Mode == "Request")
                    MlId = (int)row["MlId"];
                else if (_Mode == "TempMe")
                    MlId = (int)row["TMlId"];
                MReId = (int)row["MReId"];
                InActiveName = row["InActiveName"].ToString();
                Inactive = Convert.ToInt32(row["InActive"]);
                //if (Convert.ToBoolean(row["DefaultValue"]))
                //{
                //    ShowMessage("مدرک انتخاب شده به عنوان مدرک پیش فرض انتخاب شده است.پیش از غیرفعال/حذف این مدرک  ، مدرک دیگری را به عنوان مدرک پیش فرض انتخاب نموده و سپس اقدام به غیرفعال/حذف این مدرک نمایید");
                //    return;
                //}
            }
            if (MlId == -1)
            {
                ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
                return;
            }
            if (_Mode == "Request")
            {
                #region MeLicence
                TSP.DataManager.MemberLicenceManager LiManager2 = new TSP.DataManager.MemberLicenceManager();
                TSP.DataManager.RequestInActivesManager InActivesManager = new TSP.DataManager.RequestInActivesManager();

                LiManager.FindByCode(MlId);
                if (LiManager.Count == 1)
                {

                    if (MReId == _MReId)
                    {
                        #region Delete
                        trans.BeginSave();

                        LiManager[0].Delete();
                        LiManager.Save();


                        trans.EndSave();

                        Session["FillMeLicence"] = LiManager2.SelectMemberLicence(_MeId, _MReId, -1, -1, -1);
                        GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                        GridViewMemberLicence.KeyFieldName = "MlId";
                        GridViewMemberLicence.DataBind();
                        IsPageRefresh = true;
                        ShowMessage("ذخیره انجام شد");
                        #endregion
                    }
                    else
                    {
                        #region InActive
                        if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                        {
                            ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                            return;
                        }

                        if (Inactive == 1)//RequestInActivesManager.Count > 0)
                        {
                            ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                            return;
                        }

                        InsertInActive(InActivesManager, MlId, _MReId, _MeId, LiManager2);
                        if (IsDocFileMajor(MlId))
                            ShowMessage("ذخیره انجام شد. هشدار : رشته انتخاب شده در واحد پروانه به عنوان رشته پروانه شخص استفاده شده است. لطفا اقدامات لازم را انجام دهید ");
                        else ShowMessage("ذخیره انجام شد");
                        // Session["FillMeLicence"] = LiManager2.FindByMeRequest(MeId, CurrentMReId, -1, 2);
                        Session["FillMeLicence"] = LiManager2.SelectMemberLicence(_MeId, _MReId, -1, -1, -1);
                        GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                        GridViewMemberLicence.KeyFieldName = "MlId";
                        GridViewMemberLicence.DataBind();
                        IsPageRefresh = true;
                        #endregion
                    }
                    CheckMenuImageCurrentPage(_MeId, _MReId);
                }
                else
                {
                    ShowMessage("امکان تغییر اطلاعات وجود ندارد");
                }
                #endregion
            }
            else if (_Mode == "TempMe")
            {
                #region TempMeLicence
                TSP.DataManager.TempMemberLicenceManager TeLiManager = new TSP.DataManager.TempMemberLicenceManager();
                TSP.DataManager.TempMemberLicenceManager LiManager2 = new TSP.DataManager.TempMemberLicenceManager();


                TeLiManager.FindByCode(MlId);
                if (TeLiManager.Count == 1)
                {

                    if (MReId == _MReId)
                    {
                        trans.BeginSave();

                        TeLiManager[0].Delete();
                        TeLiManager.Save();
                        trans.EndSave();
                        LiManager2.FindByRequest(_MeId, _MReId);
                        DataTable dtli = LiManager2.DataTable;
                        Session["FillMeLicence"] = dtli;
                        GridViewMemberLicence.KeyFieldName = "TMlId";
                        GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                        GridViewMemberLicence.DataBind();
                        IsPageRefresh = true;
                        ShowMessage("ذخیره انجام شد");
                    }
                    CheckMenuImageCurrentPage(_MeId, _MReId);
                }
                else
                {
                    ShowMessage("امکان تغییر اطلاعات وجود ندارد");
                }
                #endregion
            }

            int MemberRequestId = _MReId;
            TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
            MemberRequestManager.FindByCode(MemberRequestId);
            if (MemberRequestManager[0]["IsCreated"].ToString() == "1")
                TSP.DataManager.MemberManager.UpdateMeNo(_MeId, null);
            else
                TSP.DataManager.MemberRequestManager.UpdateMeNo(MemberRequestId, null);
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);

            ShowMessage("خطایی در ذخیره رخ داده است");
            if (Utility.ShowExceptionError())
            {
                this.LabelWarning.Text += err.Message;
            }

        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();
        try
        {
            int MlId = -1;
            int MReId = -1;
            string InActiveName = "";
            if (GridViewMemberLicence.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                    GridViewMemberLicence.DataBind();
                }
                else
                    FillGrid();

                DataRow row = GridViewMemberLicence.GetDataRow(GridViewMemberLicence.FocusedRowIndex);
                if (_Mode == "Request")
                    MlId = (int)row["MlId"];
                else if (_Mode == "TempMe")
                    MlId = (int)row["TMlId"];
                MReId = (int)row["MReId"];
                InActiveName = row["InActiveName"].ToString();

            }
            if (MlId == -1)
            {
                ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
                return;
            }
            if (_Mode == "Request")
            {
                if (!string.IsNullOrEmpty(InActiveName) && InActiveName == "فعال")
                {
                    ShowMessage("رکورد مورد نظر  فعال می باشد");
                    return;
                }
                int per = DeleteInActive(MlId);
                switch (per)
                {
                    case 0:
                        IsPageRefresh = true;
                        ShowMessage("ذخیره انجام شد");
                        //  Session["FillMeLicence"] = LiManager.FindByMeRequest(MeId, CurrentMReId, -1, 2);
                        Session["FillMeLicence"] = LiManager.SelectMemberLicence(_MeId, _MReId, -1, -1, -1);
                        GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                        GridViewMemberLicence.KeyFieldName = "MlId";
                        GridViewMemberLicence.DataBind();
                        break;
                    case 1:
                        ShowMessage("مدرک انتخاب شده فعال می باشد");
                        break;
                    case 2:
                        ShowMessage("خطایی در ذخیره انجام گرفته است");
                        break;
                }
                GridViewMemberLicence.DataBind();
            }

            int MemberRequestId = _MReId;
            TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
            MemberRequestManager.FindByCode(MemberRequestId);
            if (MemberRequestManager[0]["IsCreated"].ToString() == "1")
                TSP.DataManager.MemberManager.UpdateMeNo(_MeId, null);
            else
                TSP.DataManager.MemberRequestManager.UpdateMeNo(MemberRequestId, null);
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در ذخیره رخ داده است");
            if (Utility.ShowExceptionError())
            {
                this.LabelWarning.Text += err.Message;
            }

        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (_Mode == "TempMe")
            {
                ShowMessage("امکان استعلام مدرک عضو موقت وجود ندارد");
                return;
            }

            int MlId = -1;
            int MReId = -1;
            int IsInquiry = -1;
            if (GridViewMemberLicence.FocusedRowIndex > -1)
            {
                if (Session["FillMeLicence"] != null)
                {
                    GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
                    GridViewMemberLicence.DataBind();
                }
                else
                    FillGrid();

                DataRow row = GridViewMemberLicence.GetDataRow(GridViewMemberLicence.FocusedRowIndex);
                MlId = (int)row["MlId"];
                MReId = (int)row["MReId"];
                IsInquiry = Convert.ToInt32(row["IsInquiry"]);

            }
            if (MlId == -1)
            {
                ShowMessage("لطفاً ابتدا یک رکورد را انتخاب نمائید");
                return;

            }
            else
            {
                //-----------------------------------
                TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
                RequestInActivesManager.FindByReqIdAndTableId(_MReId, MlId);
                if (RequestInActivesManager.Count == 1)
                {
                    ShowMessage("مدرک انتخاب شده غیرفعال می باشد");
                    return;
                }
                //-----------------------------------
                //------------------جهت ثبت تصویر استعلام درخواست های قبل امکان تغییر آن برای کاربر ایجاد شد-----------------
                //////////if (MReId == _MReId)
                //////////{
                if (CheckPermitionForConfirming(_MReId))//////MReId))
                    Response.Redirect("MemberLicenceInsert.aspx?MlId=" + Utility.EncryptQS(MlId.ToString())
                        + "&aPageMode=" + Utility.EncryptQS("Confirming")
                        + "&MeId=" + Utility.EncryptQS(_MeId.ToString())
                        + "&PageMode=" + PgMode.Value
                        + "&MReId=" + Utility.EncryptQS(_MReId.ToString())
                        + "&Mode=" + Utility.EncryptQS(_Mode)
                        ///// + "&TP=" + Request.QueryString["TP"]
                        + "&GrdFlt=" + Request.QueryString["GrdFlt"]
                        + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                else
                {
                    ShowMessage("امکان تایید مدرک در این مرحله از گردش کار برای شما وجود ندارد");
                }

                ////////////}
                ////////////else
                ////////////{
                ////////////    if (IsInquiry == 0 && CheckIsLicenceBelongToFirstRequest(MReId))
                ////////////    {
                ////////////        if (!CheckPermitionForConfirming(_MReId))
                ////////////        {
                ////////////            ShowMessage("امکان تایید مدرک در این مرحله از گردش کار برای شما وجود ندارد");
                ////////////            return;
                ////////////        }

                ////////////        Response.Redirect("MemberLicenceInsert.aspx?MlId=" + Utility.EncryptQS(MlId.ToString())
                ////////////           + "&aPageMode=" + Utility.EncryptQS("Confirming")
                ////////////           + "&MeId=" + Utility.EncryptQS(_MeId.ToString())
                ////////////           + "&PageMode=" + PgMode.Value
                ////////////           + "&MReId=" + Utility.EncryptQS(_MReId.ToString())
                ////////////           + "&Mode=" + Utility.EncryptQS(_Mode)
                ////////////           + "&GrdFlt=" + Request.QueryString["GrdFlt"]
                ////////////           + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                ////////////        return;
                ////////////    }
                ////////////    ShowMessage("امکان تایید مدرک مربوط به درخواست های قبل وجود ندارد.");
                ////////////}


            }
        }
        catch (Exception)
        {
            ShowMessage("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TempMe = "";
        if (_Mode == "Request")
            TempMe = "0";
        else if (_Mode == "TempMe")
            TempMe = "1";
        //
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)//////// + "&TP=" + Request.QueryString["TP"]
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attach":
                Response.Redirect("MemberAttachment.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Message":
                Response.Redirect("MemberMessage.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Group":
                Response.Redirect("MemberGroups.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&PageMode=" + PgMode.Value + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Request":
                Response.Redirect("MemberInsert.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) +
                    "&PageMode=" + PgMode.Value +
                    "&MReId=" + Utility.EncryptQS(_MReId.ToString()) +
                    "&Mode=" + Utility.EncryptQS(_Mode) +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"] +
                    "&TMe=" + Utility.EncryptQS(TempMe) +
                    "&Pt=" + Utility.EncryptQS("2"));
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) +
                    "&PageMode=" + PgMode.Value +
                    "&MReId=" + Utility.EncryptQS(_MReId.ToString()) +
                    "&Mode=" + Utility.EncryptQS(_Mode) +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + Utility.EncryptQS(_MeId.ToString()) + "&MReId=" + Utility.EncryptQS(_MReId.ToString()) + "&Mode=" + Utility.EncryptQS(_Mode)
                    + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }
    //************************************Grid***************************************************
    protected void GridViewMemberLicence_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        String[] Parameter = e.Parameters.Split(';');
        GridViewMemberLicence.JSProperties["cpDoPrintRequestCard"] = 0;
        GridViewMemberLicence.JSProperties["cpPrintRequestCardPath"] = "";
        GridViewMemberLicence.JSProperties["cpDoPrintInquery"] = 0;
        GridViewMemberLicence.JSProperties["cpPrintInqueryPath"] = "";
        TSP.DataManager.MemberLicenceManager LiManager = new TSP.DataManager.MemberLicenceManager();


        switch (Parameter[0])
        {
            case "Print":
                GridViewMemberLicence.DetailRows.CollapseAllRows();
                GridViewMemberLicence.JSProperties["cpDoPrint"] = 1;
                break;
            case "PrintRequestCard":
                if (_Mode == "TempMe")
                {
                    ShowCallBackMessage("امکان درخواست صدور کارت برای عضو موقت وجود ندارد");
                    return;
                }

                int MrsId = -1;
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                DataRow row = GridViewMemberLicence.GetDataRow(Convert.ToInt32(Parameter[1]));
                int MeId = (int)row["MeId"];
                MemberManager.FindByCode(MeId);
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["MrsId"]))
                    MrsId = Convert.ToInt32(MemberManager[0]["MrsId"]);
                if (MrsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
                {
                    ShowCallBackMessage("تنها امکان درخواست صدور کارت برای اعضای تایید شده وجود دارد");
                    return;
                }
                //----------------check estelam------------------
                LiManager.FindByIsInquiry(MeId, 1, 1);
                if (LiManager.Count == 0)
                {
                    ShowCallBackMessage("امکان درخواست صدور کارت برای مدرک استعلام نشده وجود ندارد");
                    return;
                }

                //-----------card request-----------
                GridViewMemberLicence.JSProperties["cpDoPrintRequestCard"] = 1;
                GridViewMemberLicence.JSProperties["cpPrintRequestCardPath"] = "../../ReportForms/MemberCardRequestReport.aspx?MeId=" + Utility.EncryptQS(MeId.ToString());
                break;
            case "PrintInquery":
                //----------------check estelam------------------
                int MeId1 = -1;
                int MlId = -1;
                int IsMeTemp = 0;
                DataRow row1 = GridViewMemberLicence.GetDataRow(Convert.ToInt32(Parameter[1]));
                if (_Mode == "TempMe")
                {
                    MeId1 = (int)row1["TMeId"];
                    MlId = (int)row1["TMlId"];
                    IsMeTemp = 1;
                }
                else
                {
                    MeId1 = (int)row1["MeId"];
                    MlId = (int)row1["MlId"];
                    IsMeTemp = 0;
                }
                //LiManager.FindByCode(MlId);
                //if (LiManager.Count == 1)
                //{
                //    if (Convert.ToInt32(LiManager[0]["IsInquiry"]) != 0)
                //    {
                //        ShowCallBackMessage("امکان درخواست استعلام مدرک تحصیلی برای مدرک استعلام شده وجود ندارد");
                //        return;
                //    }
                //}

                //-----------card request-----------
                GridViewMemberLicence.JSProperties["cpDoPrintInquery"] = 1;
                GridViewMemberLicence.JSProperties["cpPrintInqueryPath"] = "../../ReportForms/MemberInQueryLicence.aspx?MeId="
                    + Utility.EncryptQS(MeId1.ToString()) + "&MlId=" + Utility.EncryptQS(MlId.ToString()) + "&IsMeTemp=" + Utility.EncryptQS(IsMeTemp.ToString());
                break;
        }



    }

    protected void GridViewMemberLicence_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (_Mode == "Request")
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data)
                return;
            if (_MReId != null)
            {
                //string MReId = Utility.DecryptQS(MemberRequest.Value);
                if (e.GetValue("MReId") == null)
                    return;
                int CurretnMReId = Convert.ToInt32(e.GetValue("MReId"));
                if (_MReId == CurretnMReId)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
    }

    protected void GridViewMemberLicence_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewMemberLicence_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void WFUserControl_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;

        if (Utility.IsDBNullOrNullValue(_MReId))
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }

        int MsId = -1;
        MemberRequestManager.FindByCode(_MReId);
        if (MemberRequestManager.Count == 1)
        {
            MsId = Convert.ToInt32(MemberRequestManager[0]["MsId"]);
        }
        else
        {
            WFUserControl.SetMsgText("لطفاً ابتدا یک درخواست را انتخاب نمائید");
            return;
        }

        if (!Utility.IsDBNullOrNullValue(MsId))
        {
            if (MsId == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                WFCode = (int)TSP.DataManager.WorkFlows.MemberTransferConfirming;
        }
        else
        {
            WFUserControl.SetMsgText("نوع درخواست نامشخص می باشد.");
            return;
        }


        WFUserControl.PerformCallback(_MReId, TableType, WFCode, e);
        GridViewMemberLicence.DataBind();
    }
    #endregion

    #region Methods

    private void SetKey()
    {
        try
        {
            #region CheckPermission
            TSP.DataManager.Permission per = TSP.DataManager.MemberLicenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perCardRequest = TSP.DataManager.MemberManager.GetUserPermissionForMemberCardRequest(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perInqueryRequest = TSP.DataManager.MemberManager.GetUserPermissionForMemberLicenceInqueryButton(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnPrintCardRequest.Enabled = btnPrintCardRequest2.Enabled = perCardRequest.CanView;
            btnInqueryPrint.Enabled = btnInqueryPrint2.Enabled = perInqueryRequest.CanView;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnInActive.Enabled = btnInActive2.Enabled = btnActive.Enabled = btnActive2.Enabled = per.CanEdit;

            GridViewMemberLicence.Visible = per.CanView;
            #endregion

            if (string.IsNullOrEmpty(Request.QueryString["Mode"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }

            _Mode = Utility.DecryptQS(Request.QueryString["Mode"]).ToString();
            _MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"]).ToString());
            _MReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MReId"]).ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            if (string.IsNullOrEmpty(_Mode) || _MeId == null)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            Session["FillMeLicence"] = null;
            CheckWorkFlowPermission();
            SetMenuItem();
            if (_MReId == null)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            ReqManager.FindByCode(_MReId);
            if (ReqManager.Count > 0 && (ReqManager[0]["IsConfirm"].ToString() != "0"))
            {
                btnEdit.Enabled = btnEdit2.Enabled =
                BtnNew.Enabled = BtnNew2.Enabled =
                btnInActive2.Enabled = btnInActive.Enabled =
                btnActive.Enabled = btnActive2.Enabled = false;
            }
            switch (_Mode)
            {
                case "Request":
                    #region Request
                    TSP.DataManager.MemberLicenceManager LicManager = new TSP.DataManager.MemberLicenceManager();
                    Session["FillMeLicence"] = LicManager.SelectMemberLicence(_MeId, _MReId, -1, -1, -1);
                    #endregion
                    break;
                case "TempMe":
                    #region TempMe
                    TSP.DataManager.TempMemberLicenceManager TempMemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
                    TempMemberLicenceManager.FindByRequest(_MeId, _MReId);
                    Session["FillMeLicence"] = TempMemberLicenceManager.DataTable;
                    #endregion
                    break;
            }

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    void ShowCallBackMessage(string Msg)
    {
        GridViewMemberLicence.JSProperties["cpMsg"] = Msg;
        GridViewMemberLicence.JSProperties["cpError"] = 1;
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.LabelWarning.Text = Message;
    }

    protected void Delete(int MlId, TSP.DataManager.MemberLicenceManager LiManager)
    {


    }

    protected void FillGrid()
    {

        TSP.DataManager.MemberLicenceManager LicManager = new TSP.DataManager.MemberLicenceManager();


        switch (_Mode)
        {
            case "Home":
                GridViewMemberLicence.DataSource = Session["FillMeLicence"] = LicManager.SelectMemberLicence(_MeId, -1, -1, -1, -1);//LicManager.FindByMeRequest(int.Parse(MeId), -1, 1);
                GridViewMemberLicence.DataBind();
                break;

            case "Request":
                Session["FillMeLicence"] = LicManager.SelectMemberLicence(_MeId, _MReId, -1, -1, -1);
                GridViewMemberLicence.KeyFieldName = "MlId";
                GridViewMemberLicence.DataBind();

                break;
            case "TempMe":
                TSP.DataManager.TempMemberLicenceManager TempMemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
                TempMemberLicenceManager.FindByRequest(_MeId, _MReId);
                DataTable dtLi = TempMemberLicenceManager.DataTable;
                GridViewMemberLicence.DataSource = dtLi;
                GridViewMemberLicence.KeyFieldName = "TMlId";
                GridViewMemberLicence.DataBind();

                break;

        }
        Session["FillMeLicence"] = GridViewMemberLicence.DataSource;
    }

    #region WF
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.MemberConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, _MReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPer1 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember, WFCode, _MReId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPerTransfer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo, (int)TSP.DataManager.WorkFlows.MemberTransferConfirming, _MReId, Utility.GetCurrentUser_UserId());

        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPerTransfer.BtnNew || WFPer1.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerTransfer.BtnEdit || WFPer1.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = btnActive.Enabled = btnActive2.Enabled = WFPer.BtnInactive || WFPerTransfer.BtnInactive || WFPer1.BtnInactive;
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming
                || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMemberTransferRequestInfo || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingMember)
            {
                return true;
            }
        }
        return false;
    }

    private Boolean CheckPermitionForConfirming(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.MemberLicenceInquiryAndConfirming;
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            if (CurrentTaskCode == TaskCode)
                return true;
        }
        return false;
    }
    #endregion

    private Boolean CheckIsLicenceBelongToFirstRequest(int SelectedMReId)
    {
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberRequestManager.FindByCode(SelectedMReId);
        if (MemberRequestManager.Count != 1)
        {
            return false;
        }
        if (Convert.ToBoolean(MemberRequestManager[0]["IsCreated"]))
        {
            return true;
        }
        return false;
    }

    protected void InsertInActive(TSP.DataManager.RequestInActivesManager Manager, int MlId, int MReId, int MeId, TSP.DataManager.MemberLicenceManager LicneceManager)
    {
        DataRow dr = Manager.NewRow();
        dr["TableId"] = MlId;
        dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberLicence);
        dr["ReqId"] = MReId;
        dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest);
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();

        Session["FillMeLicence"] = LicneceManager.SelectMemberLicence(MeId, -1, -1, -1, -1);
        GridViewMemberLicence.DataSource = (DataTable)Session["FillMeLicence"];
        GridViewMemberLicence.DataBind();

        //  ShowMessage("ذخیره انجام شد");
    }

    protected int DeleteInActive(int MlId)
    {
        int result = 0;  // 0 successful 1 not exist 2 error
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberLicence);
        RequestInActivesManager.FindByTableIdTableType(MlId, TableType, -1, 0); //(MReId, MlId);
        if (RequestInActivesManager.Count > 0)
        {
            RequestInActivesManager[0].Delete();
            if (RequestInActivesManager.Save() > 0)
                result = 0;
            else result = 2;
        }
        else result = 1;

        return result;
    }

    #region Menu Images
    protected void CheckMenuImage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        // TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Attachment
        arr.Add(0);//arr[5]-->Request

        MemberActivitySubjectManager.FindForDelete(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        //ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        //if (ProjectJobHistoryManager.Count > 0)
        //{
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
        //    arr[1] = 1;
        //}
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }

    protected void CheckMenuImageCurrentPage(int MeId, int MReId)
    {
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        MemberLicenceManager.FindForDelete(MeId, MReId);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberLicenceManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
                arr[0] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "";
                arr[0] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberLicenceManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
                arr[0] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "";
                arr[0] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }

    protected void SetMenuItem()
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];

            if ((int)arr[0] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(_MeId, _MReId);
        }
    }
    #endregion

    bool IsDocFileMajor(int MlId)
    {
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        DocMemberFileMajorManager.FindByMlId(MlId, 0);
        if (DocMemberFileMajorManager.Count > 0)
            return true;
        else
            return false;
    }

    private void BackToManagementPage()
    {

        string UserName = "";
        if (_Mode == "Request")
            UserName = _MeId.ToString();
        else if (_Mode == "TempMe")
            UserName = "M" + _MeId.ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Members.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + Utility.EncryptQS(_MReId.ToString()));
        }
        else
        {
            Response.Redirect("Members.aspx?MReId=" + Utility.EncryptQS(_MReId.ToString()));
        }
    }

    private string FillMemberName()
    {
        MemberInfoUserControl.MeId = _MeId;
        if (_Mode == "TempMe")
        {
            MemberInfoUserControl.IsMeTemp = true;
        }
        MemberInfoUserControl.MReId = _MReId;
        return MemberInfoUserControl.MeName;
    }
    #endregion
}
