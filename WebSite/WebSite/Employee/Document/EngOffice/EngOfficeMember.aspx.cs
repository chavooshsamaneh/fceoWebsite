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

public partial class Employee_Document_EngOffice_EngOfficeMember : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForEngOffice(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;//per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;//per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnActive.Enabled = per.CanEdit;
            btnActive1.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive1.Enabled = per.CanEdit;
            GridViewEngOffMember.Visible = per.CanView;


            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("EngOffice.aspx");
                return;
            }
            try
            {
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);


            if (string.IsNullOrEmpty(EngOfId) || string.IsNullOrEmpty(EOfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            ObjectDataSourceEngOffMember.SelectParameters["EOfId"].DefaultValue = EOfId;
            ObjectDataSourceEngOffMember.SelectParameters["EngOfId"].DefaultValue = EngOfId;
            
            CheckWorkFlowPermission();

            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            FileManager.FindByCode(int.Parse(EOfId));
            if (FileManager.Count > 0)
            {
                if (!Convert.ToBoolean(FileManager[0]["Requester"]))//FromMember
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnActive.Enabled = false;
                    btnActive1.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive1.Enabled = false;
                }
                if (FileManager[0]["IsConfirm"].ToString() != "0") //answered
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                }
                if (Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.ChangeBaseInfo)
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnActive.Enabled = false;
                    btnActive1.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive1.Enabled = false;
                }
            }

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnActive"] = btnActive.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;

        }
        if (this.ViewState["BtnActive"] != null)
            this.btnActive1.Enabled = this.btnActive.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive1.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];



        Session["DataTable"] = GridViewEngOffMember.Columns;
        Session["DataSource"] = ObjectDataSourceEngOffMember;
        Session["Title"] = "اعضای دفتر";
        //Session["Header"] = "مدیر مسئول : " + lblOfName.Text;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS("-1") + "&PersonId=" + Utility.EncryptQS("-1") + "&aPageMode=" + Utility.EncryptQS("New") + "&EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int PersonId = -1;
        int EOfId = -1;


        if (GridViewEngOffMember.FocusedRowIndex > -1)
        {

            DataRow row = GridViewEngOffMember.GetDataRow(GridViewEngOffMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];
            EOfId = (int)row["OfReId"];

        }
        if (OfmId == -1)
        {
            ShowMessage("لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید");

        }
        else
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            OfMeManager.selectEngOfficeMember(OfmId);
            if (OfMeManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            TSP.DataManager.EngOffFileManager EngOffFileManager = new TSP.DataManager.EngOffFileManager();
            EngOffFileManager.FindByCode(EOfId);
            if (EngOffFileManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }

            int CurrentEOfId = int.Parse(Utility.DecryptQS(EngFileId.Value));
            if (EOfId == CurrentEOfId || CheckIfCanEditOldInformations(EngOffFileManager))
            {

                if (CheckPermitionForEdit(CurrentEOfId))
                    Response.Redirect("EngOfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString()) + "&aPageMode=" + Utility.EncryptQS("Edit") + "&EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);

                else
                {
                    ShowMessage("امکان ویرایش اطلاعات در این مرحله از گردش کار برای شما وجود ندارد.");
                }
            }
            else
            {
                ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int PersonId = -1;
        if (GridViewEngOffMember.FocusedRowIndex > -1)
        {


            DataRow row = GridViewEngOffMember.GetDataRow(GridViewEngOffMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("EngOfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString()) + "&aPageMode=" + Utility.EncryptQS("View") + "&EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value);


        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeRegister.aspx?PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(EngOfId) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("EngOffice.aspx?PostId=" + EngOfficeId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("EngOffice.aspx");
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int EOfId = -1;
        int EngOffId = -1;
        int PersonId = -1;
        string Active = "";
        Boolean ShouldInActive = true;
        string EngOfId = Utility.DecryptQS(EngOfficeId.Value);

        if (GridViewEngOffMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffMember.GetDataRow(GridViewEngOffMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            EOfId = (int)row["OfReId"];
            PersonId = (int)row["PersonId"];
            Active = row["Active"].ToString();
            EngOffId = (int)row["OfId"];
        }
        if (OfmId == -1)
        {
            ShowMessage("لطفاً برای غیر فعال کردن ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        try
        {
            OfMeManager.FindEngOfficeMemberByOfmCode(OfmId);
            if (OfMeManager.Count != 1)
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
                return;
            }
            int CurrentOfReId = int.Parse(Utility.DecryptQS(EngFileId.Value));

            if (EOfId == CurrentOfReId)
            {
                DataTable dt = OfMeManager.FindEngOfficeMembers(EOfId, PersonId, 1);//***آیا غیرفعال شده سیستمی برای این کد عضویت وجود دارد؟
                if (dt.Rows.Count <= 0)
                {
                    ShouldInActive = false;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Active) && Active != "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                    return;
                }

            }
            if (ShouldInActive)
            {
                RequestInActivesManager.FindByTableIdTableType(OfmId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember), -1, 0);
                if (RequestInActivesManager.Count > 0)
                {
                    if (Convert.ToInt32(RequestInActivesManager[0]["ReqId"]) == CurrentOfReId)
                        ShowMessage("رکورد مورد نظر غیر فعال می باشد");
                    //  else ShowMessage("عضو مورد نظر در درخواست های قبلی غیرفعال شده است");//-------------در صورتی که در یک درخواست رد شده غیرفعال شده باشد
                    return;
                }

                InsertInActive(OfmId, CurrentOfReId, PersonId, EngOffId);
            }
            else
            {

                Delete(OfmId, PersonId, EOfId, EngOffId);

            }
            GridViewEngOffMember.DataBind();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }


    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        int EOfId = -1;
        int PersonId = -1;
        string Active = "";

        string EngOfId = Utility.DecryptQS(EngOfficeId.Value);


        if (GridViewEngOffMember.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEngOffMember.GetDataRow(GridViewEngOffMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            EOfId = (int)row["OfReId"];
            PersonId = (int)row["PersonId"];
            Active = row["Active"].ToString();
        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای فعال کردن ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        try
        {
            OfMeManager.FindEngOfficeMemberByPersonId(PersonId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
            if (OfMeManager.Count > 0)
            {
                int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                if (EngOfIdMember != int.Parse(EngOfId))
                {
                    DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, PersonId);
                    if (dtEngOffReq.Rows.Count > 0)
                    {
                        string str = "امکان فعال کردن این عضو وجود ندارد.عضو مورد نظر در دفتر ";
                        str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                        str += " مشغول به کار می باشد";
                        ShowMessage(str);
                        return;
                    }
                }
            }
            OfMeManager.FindOffMemberByPersonId(PersonId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
            if (OfMeManager.Count > 0)
            {
                int OfId = Convert.ToInt32(OfMeManager[0]["OfId"]);
                DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfId, 0, PersonId);
                if (dtOffReq.Rows.Count > 0)
                {
                    string str = "امکان فعال کردن این عضو وجود ندارد.عضو مورد نظر در شرکت ";
                    str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
                    str += " مشغول به کار می باشد";
                    ShowMessage(str);
                    return;
                }
            }

            OfMeManager.FindEngOfficeMemberByOfmCode(OfmId);
            if (OfMeManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
                return;
            }
            int CurrentOfReId = int.Parse(Utility.DecryptQS(EngFileId.Value));
            if (!string.IsNullOrEmpty(Active) && Active != "غیر فعال")
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رکورد مورد نظر فعال می باشد";
                return;
            }
            
            int per = 2;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(PersonId, 0);
            if (dtMeFile.Rows.Count <= 0)
            {
                ShowMessage("خطایی در جستجوی پروانه عضو مورد نظر رخ داده است");
                return;
            }
            int LastMemberFileId = Convert.ToInt32(dtMeFile.Rows[0]["MfId"]);
            int MemberFileId = Convert.ToInt32(OfMeManager[0]["MfId"]);
            if (LastMemberFileId != MemberFileId)//**عضو غیرفعال است و اطلاعات پروانه در دفتر با آخرین پروانه یکی نیست یعنی غیرفعال شده اما در بازه زمانی غیرفعال بودن پروانه شخص حقیقی جدید ثبت شده است و باید یک غیرفعال سیستمی برای عضو شرکت ثبت شود و سپس رکورد جدید با اطلاعات پروانه جدید شخص حقیقی ثبت شود 
            {
                per = EditInActiveAndInsert(OfmId, CurrentOfReId, PersonId, int.Parse(EngOfId), LastMemberFileId);
            }
            else//**عضو غیرفعال است و اطلاعات پروانه در دفتر با آخرین پروانه حقیقی عضو یکی است یعنی بابت این عضویت یک رکورد جدید ثبت شده و یک رکورد غیرفعال هم ثبت شده 
                per = DeleteInActive(OfmId, CurrentOfReId, PersonId, int.Parse(EngOfId));

            switch (per)
            {
                case 0:
                    ShowMessage("ذخیره انجام شد");
                    GridViewEngOffMember.DataBind();
                    break;
                case 1:
                    ShowMessage("عضو انتخاب شده فعال می باشد");
                    break;
                case 2:
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    break;
            }
            GridViewEngOffMember.DataBind();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

        switch (e.Item.Name)
        {

            case "Attach":
                Response.Redirect("EngOfficeAttachment.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "EngOffice":
                Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;

            case "Job":
                Response.Redirect("EngOfficeJob.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void GridViewEngOffMember_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (EngFileId.Value != null)
        {
            string EOfId = Utility.DecryptQS(EngFileId.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnEOfId = e.GetValue("OfReId").ToString();
            if (EOfId == CurretnEOfId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void GridViewEngOffMember_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "FileNo" || e.DataColumn.FieldName == "FileDate")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void GridViewEngOffMember_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "StartDate" || e.Column.FieldName == "FileNo" || e.Column.FieldName == "FileDate")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    protected void Delete(int OfmId, int PersonId, int EOfId, int EngOffId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        trans.Add(OfMeManager);
        trans.Add(FileManager);
        try
        {
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }
            trans.BeginSave();
            OfMeManager.selectEngOfficeMember(OfmId);
            OfMeManager[0].Delete();
            OfMeManager.Save();

            #region SetMFNo
            TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
            string MFNo = SetOfficeMfNo(EngOffId, EOfId, PrCode, MFCode, FileManager, MeMjManager, OfMeManager);
            if (string.IsNullOrWhiteSpace(MFNo))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #endregion

            trans.EndSave();
            GridViewEngOffMember.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }
    }

    protected void InsertInActive(int OfmId, int EOfId, int PersonId, int EngOfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        trans.Add(OfMeManager);
        trans.Add(FileManager);
        trans.Add(Manager);
        trans.Add(MeMjManager);
        try
        {
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return;
            }

            trans.BeginSave();
            DataRow dr = Manager.NewRow();
            dr["TableId"] = OfmId;
            dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);
            dr["ReqId"] = EOfId;
            dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
            dr["InActive"] = 1;
            dr["InActiveRow"] = 0;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            Manager.AddRow(dr);
            Manager.Save();

            #region SetMFNo
            string MFNo = SetOfficeMfNo(EngOfId, EOfId, PrCode, MFCode, FileManager, MeMjManager, OfMeManager);
            if (string.IsNullOrWhiteSpace(MFNo))
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }          
            #endregion
            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
        }

    }

    protected int DeleteInActive(int OfmId, int EOfId, int PersonId, int EngOffId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        trans.Add(OfMeManager);
        trans.Add(FileManager);
        trans.Add(RequestInActivesManager);
        trans.Add(MeMjManager);
        try
        {
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return 2;
            }
            trans.BeginSave();
            int result = 0;  // 0 successful 1 not exist 2 error
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);
            RequestInActivesManager.FindByTableIdTableType(OfmId, TableType, -1, 0); //(MReId, MlId);
            if (RequestInActivesManager.Count != 1) return 1;
            RequestInActivesManager[0].Delete();
            if (RequestInActivesManager.Save() <= 0) return 2;

            #region SetMFNo
            SetOfficeMfNo(EngOffId, EOfId, PrCode, MFCode, FileManager, MeMjManager, OfMeManager);        
            #endregion
            trans.EndSave();
            result = 0;



            return result;
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
            return 2;
        }
    }


    /// <summary>
    /// writen by nima zadeh EditInActiveAndInsert
    /// </summary>
    /// <param name="OfmId"></param>
    /// <param name="EOfId"></param>
    /// <param name="PersonId"></param>
    /// <param name="EngOffId"></param>
    /// <param name="MemberFileId"></param>
    /// <returns></returns>
    protected int EditInActiveAndInsert(int OfmId, int EOfId, int PersonId, int EngOffId, int MemberFileId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        trans.Add(OfMeManager);
        trans.Add(FileManager);
        trans.Add(RequestInActivesManager);
        trans.Add(MeMjManager);
        try
        {
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";
            string MFCode = TSP.DataManager.EngOfficeManager.MFType.ToString();
            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInLoadingData));
                return 2;
            }
            trans.BeginSave();

            int result = 0;  // 0 successful 1 not exist 2 error

            #region EditRequestInActives
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember);
            RequestInActivesManager.FindByTableIdTableType(OfmId, TableType, -1, 0); //(MReId, MlId);
            if (RequestInActivesManager.Count != 1) return 1;
            RequestInActivesManager[0].BeginEdit();
            RequestInActivesManager[0]["SysInActive"] = 1;
            RequestInActivesManager[0]["InActive"] = 1;
            RequestInActivesManager[0]["InActiveRow"] = 0;
            RequestInActivesManager[0]["CreateDate"] = Utility.GetDateOfToday();
            RequestInActivesManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            RequestInActivesManager[0]["ModifiedDate"] = DateTime.Now;
            RequestInActivesManager[0].EndEdit();
            if (RequestInActivesManager.Save() <= 0) return 2;
            #endregion

            #region Insert
            DataTable dtOldEngOfficeMember = OfMeManager.FindEngOfficeMemberByOfmCode(OfmId);
            if (dtOldEngOfficeMember.Rows.Count != 1) return 1;

            DataRow drNewEngOfficeMembers = OfMeManager.NewRow();
            drNewEngOfficeMembers["OfReId"] = EOfId.ToString();
            drNewEngOfficeMembers["MfId"] = MemberFileId;
            drNewEngOfficeMembers["OfId"] = dtOldEngOfficeMember.Rows[0]["OfId"];
            drNewEngOfficeMembers["OfKind"] = 1;
            drNewEngOfficeMembers["OfmType"] = 1;
            drNewEngOfficeMembers["PersonId"] = dtOldEngOfficeMember.Rows[0]["PersonId"];

            drNewEngOfficeMembers["SignUrl"] = dtOldEngOfficeMember.Rows[0]["SignUrl"];

            drNewEngOfficeMembers["SelfreportedImageURL"] = dtOldEngOfficeMember.Rows[0]["SelfreportedImageURL"];

            drNewEngOfficeMembers["OfpId"] = dtOldEngOfficeMember.Rows[0]["OfpId"];

            drNewEngOfficeMembers["StartDate"] = dtOldEngOfficeMember.Rows[0]["StartDate"];
            //drMembers["EndDate"] = txtMeEndDate.Text;
            drNewEngOfficeMembers["HasSignRight"] = dtOldEngOfficeMember.Rows[0]["HasSignRight"];

            drNewEngOfficeMembers["HasGasCert"] = dtOldEngOfficeMember.Rows[0]["HasGasCert"];

            drNewEngOfficeMembers["IsFullTime"] = dtOldEngOfficeMember.Rows[0]["IsFullTime"];

            drNewEngOfficeMembers["IsConfirm"] = 1;
            drNewEngOfficeMembers["ConfirmDate"] = Utility.GetDateOfToday();

            drNewEngOfficeMembers["Description"] = dtOldEngOfficeMember.Rows[0]["Description"];
            drNewEngOfficeMembers["UserId"] = Utility.GetCurrentUser_UserId();
            drNewEngOfficeMembers["ModifiedDate"] = DateTime.Now;
            OfMeManager.AddRow(drNewEngOfficeMembers);

            if (OfMeManager.Save() != 1)
            {
                trans.CancelSave();
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return 1;
            }
            OfMeManager.DataTable.AcceptChanges();

            #endregion

            #region SetMFNo
            SetOfficeMfNo(EngOffId, EOfId, PrCode, MFCode, FileManager, MeMjManager, OfMeManager);
            #endregion
            trans.EndSave();
            result = 0;



            return result;
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
            return 2;
        }
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == 0)
                                {
                                    int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                    if (Permission > 0)
                                        return true;
                                    else
                                        return false;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string EOfId = Utility.DecryptQS(EngFileId.Value);
        //****TableType
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile);

        int WFCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo, WFCode, int.Parse(EOfId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPerEmpDoc = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice, WFCode, int.Parse(EOfId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPerEmpDoc.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPerEmpDoc.BtnEdit;
        this.ViewState["btnActive"] = btnActive.Enabled = btnActive1.Enabled = btnInActive.Enabled = btnInActive1.Enabled = WFPer.BtnInactive || WFPerEmpDoc.BtnInactive;
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private Boolean CheckIfCanEditOldInformations(TSP.DataManager.EngOffFileManager EngOffFileManager)
    {
        if (Convert.ToInt32(EngOffFileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.OldDocument)
        {
            int EngOfId = Convert.ToInt32(EngOffFileManager[0]["EngOfId"]);
            EngOffFileManager.FindByEngOffCode(EngOfId, 1, -1);
            if (EngOffFileManager.Count > 1)
                return false;
            else
                return true;

        }
        return false;
    }

    private string SetOfficeMfNo(int EngOfId, int EOfId, string PrCode, string MFCode, TSP.DataManager.EngOffFileManager fileManager, TSP.DataManager.DocMemberFileMajorManager MeMjManager, TSP.DataManager.OfficeMemberManager OffMemberManager)
    {
        string MFNo = "";
        string MFMjCode = "0000000";
        #region SetMFNo
        DataTable dtOfMe = OffMemberManager.selectActiveEngOfficeMember(EOfId, EngOfId);//return member
        if (dtOfMe.Rows.Count > 0)
        {
            for (int m = 0; m < dtOfMe.Rows.Count; m++)
            {
                DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(dtOfMe.Rows[m]["PersonId"].ToString()));
                if (dtMj.Rows.Count > 0)
                {
                    //int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
                    int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
                    int i = -1;
                    char[] Code = MFMjCode.ToCharArray();

                    switch (ParentId)
                    {
                        case (int)TSP.DataManager.MainMajors.Architecture:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Civil:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Electronic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mapping:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Mechanic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Traffic:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        case (int)TSP.DataManager.MainMajors.Urbanism:
                            i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
                            Code[i] = ParentId.ToString()[0];
                            break;
                        default:
                            i = -1;
                            break;
                    }
                    if (i != -1)
                    {
                        MFMjCode = new string(Code);
                    }
                }
                dtMj.Clear();
            }
        }
        #endregion
        fileManager.FindByCode(EOfId);
        if (fileManager.Count != 1)
            return "";
        string MFSerialNo = fileManager[0]["MFSerialNo"].ToString();
        while (MFSerialNo.Length < 5)
        {
            MFSerialNo = "0" + MFSerialNo;
        }
        fileManager[0]["FileNo"] = MFNo = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
        fileManager.Save();
        return MFNo;
    }
    #endregion
}
