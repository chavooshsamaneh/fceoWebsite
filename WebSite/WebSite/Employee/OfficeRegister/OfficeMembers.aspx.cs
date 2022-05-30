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

public partial class Employee_OfficeRegister_OfficeMembers : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    int _ReqType
    {
        get { return Convert.ToInt32(HiddenFieldOffice["ReqType"]); }
        set { HiddenFieldOffice["ReqType"] = value.ToString(); }
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
            if (string.IsNullOrEmpty(Request.QueryString["Dprt"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            HiddenFieldOffice["Department"] = Request.QueryString["Dprt"];

            Session["FillGrid"] = null;

            CheckPermission();

            if (string.IsNullOrEmpty(Request.QueryString["OfReId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Office.aspx");
                return;
            }
            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);


            if (string.IsNullOrEmpty(OfId) || string.IsNullOrEmpty(OfReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            //ObjdsOfficeMember.SelectParameters[0].DefaultValue = OfId;
            //ObjdsOfficeMember.SelectParameters[1].DefaultValue = OfReId;

            Session["FillGrid"] = FillGrid(int.Parse(OfId), int.Parse(OfReId));

            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                Session["Header"] = "شرکت : " + OfManager[0]["OfName"].ToString();
            OfficeInfoUserControl.OfReId = int.Parse(OfReId);
            string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
            if (Department == "MemberShip")
            {
                CheckWorkFlowPermissionForOffice();
            }
            else if (Department == "Document")
            {
                CheckWorkFlowPermissionForDoc();
            }

            TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
            ReqManager.FindByCode(int.Parse(OfReId));
            if (ReqManager.Count > 0)
            {
                _ReqType = Convert.ToInt32(ReqManager[0]["Type"]);
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"
                    || _ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeBaseInfo
                    || (Department == "MemberShip"
                            && Convert.ToInt32(ReqManager[0]["DocumentStatus"]) == (int)TSP.DataManager.OfficeDocumentStatus.Confirmed))&&(_ReqType!= (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo))
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

            #region CheckModeComment
            //switch (Mode)
            //{

            //    case "Home":

            //        if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            //        {
            //            Response.Redirect("Office1.aspx");
            //            return;
            //        }
            //        try
            //        {
            //            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());


            //            ReqManager.FindByOfficeId(int.Parse(OfId), -1, 0);
            //            if (ReqManager.Count > 0)
            //            {
            //                if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
            //                {
            //                    //btnEdit.Enabled = false;
            //                    //btnEdit2.Enabled = false;
            //                    //BtnNew.Enabled = false;
            //                    //BtnNew2.Enabled = false;
            //                    //btnActive1.Enabled = false;
            //                    //btnActive.Enabled = false;
            //                }
            //            }

            //            if (OfManager[0]["MrsId"].ToString() == "1")//تایید شده
            //            {
            //                //btnActive.Enabled = false;
            //                //btnActive1.Enabled = false;
            //                //btnEdit.Enabled = false;
            //                //btnEdit2.Enabled = false;
            //                //BtnNew.Enabled = false;
            //                //BtnNew2.Enabled = false;

            //                Session["FillOfMember"] = MemManager.FindByOffRequest(int.Parse(OfId), -1, 1);

            //            }
            //            else
            //            {
            //                Session["FillOfMember"] = MemManager.FindByOffRequest(int.Parse(OfId), -1, -1);

            //            }
            //        }
            //        catch (Exception)
            //        { }


            //        break;

            //    case "Request":
            //        try
            //        {
            //            ASPxMenu1.Items[0].Visible = false;
            //            ASPxMenu1.Items[1].Visible = false;
            //            ASPxMenu1.Items[6].Visible = false;


            //            string ReqestMode = Server.HtmlDecode(Request.QueryString["TP"]).ToString();
            //            string TPType = Utility.DecryptQS(ReqestMode);
            //            if (!string.IsNullOrEmpty(TPType))
            //            {
            //                if (TPType == "0")//Menu
            //                {
            //                    btnActive1.Enabled = false;
            //                    btnActive.Enabled = false;
            //                    BtnNew.Enabled = false;
            //                    BtnNew2.Enabled = false;
            //                    btnEdit.Enabled = false;
            //                    btnEdit2.Enabled = false;
            //                }
            //                else
            //                {
            //                    btnActive1.Enabled = false;
            //                    btnActive.Enabled = false;
            //                    btnEdit.Enabled = false;
            //                    btnEdit2.Enabled = false;


            //                    ReqManager.FindByCode(int.Parse(OfReId));
            //                    if (ReqManager.Count > 0)
            //                    {
            //                        if (!Convert.ToBoolean(ReqManager[0]["Requester"]))//FromMember
            //                        {
            //                            BtnNew.Enabled = false;
            //                            BtnNew2.Enabled = false;

            //                        }
            //                    }


            //                }
            //            }

            //            Session["FillOfMember"] = MemManager.FindByOffRequest(int.Parse(OfId), int.Parse(OfReId), -1);

            //            //RequestMode.Value = Server.HtmlDecode(Request.QueryString["ReqMode"]).ToString();
            //            //string ReqMode = Utility.DecryptQS(RequestMode.Value);
            //            //if (string.IsNullOrEmpty(RequestMode))
            //            //{
            //            //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            //            //}


            //        }
            //        catch (Exception err)
            //        {
            //        }


            //        break;

            //}
            #endregion

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnActive"] = btnActive.Enabled;

            SetMenuItem();
        }
        if (this.ViewState["BtnActive"] != null)
            this.btnActive1.Enabled = this.btnActive.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        Session["DataTable"] = DevGridViewOfficeMember.Columns;
        Session["DataSource"] = (DataTable)Session["FillGrid"];
        Session["Title"] = "اعضای شرکت";

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (Session["FillGrid"] == null)
        {
            Session["FillGrid"] = FillGrid(Convert.ToInt32(Utility.DecryptQS(OfficeId.Value)), Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value)));
        }
        DevGridViewOfficeMember.DataSource = (DataTable)Session["FillGrid"];
        DevGridViewOfficeMember.DataBind();


    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS("-1")
            //+ "&OfmType=" + Utility.EncryptQS("")
            //+ "&PersonId=" + Utility.EncryptQS("") 
            + "&aPageMode=" + Utility.EncryptQS("New")
            + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value
            + "&OfReId=" + OfficeRequest.Value
            + "&GrdFlt=" + Request.QueryString["GrdFlt"]
            + "&SrchFlt=" + Request.QueryString["SrchFlt"]
            + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        byte OfmType = 0;
        int PersonId = -1;
        int OfReId = -1;


        if (DevGridViewOfficeMember.FocusedRowIndex > -1)
        {
            DataRow row = DevGridViewOfficeMember.GetDataRow(DevGridViewOfficeMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            OfmType = Convert.ToByte(row["OfmType"]);
            PersonId = (int)row["PersonId"];
            OfReId = (int)row["OfReId"];

        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            OfMeManager.FindByCode(OfmId);
            if (OfMeManager.Count == 1)
            {
                int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                if (OfReId == CurrentOfReId)
                {
                    string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());

                    if ((Department == "Document" && CheckPermitionForEditForDoc(OfReId)) || (Department == "MemberShip" && CheckPermitionForEditForOffice(OfReId)))
                        Response.Redirect("OfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&OfmType=" + Utility.EncryptQS(OfmType.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString()) + "&aPageMode=" + Utility.EncryptQS("Edit") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                            + "&Dprt=" + HiddenFieldOffice["Department"].ToString());

                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                    }


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                }


            }
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfmId = -1;
        byte OfmType = 0;
        int PersonId = -1;
        if (DevGridViewOfficeMember.FocusedRowIndex > -1)
        {
            DataRow row = DevGridViewOfficeMember.GetDataRow(DevGridViewOfficeMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            OfmType = Convert.ToByte(row["OfmType"]);
            PersonId = (int)row["PersonId"];

        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OfficeMemberInsert.aspx?OfmId=" + Utility.EncryptQS(OfmId.ToString()) + "&OfmType=" + Utility.EncryptQS(OfmType.ToString()) + "&PersonId=" + Utility.EncryptQS(PersonId.ToString()) + "&aPageMode=" + Utility.EncryptQS("View") + "&OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                + "&Dprt=" + HiddenFieldOffice["Department"].ToString());


        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Page = "";
        switch (Utility.DecryptQS(HiddenFieldOffice["Department"].ToString()))
        {
            case "MemberShip":
                Page = "OfficeInsert.aspx";
                break;
            default:
                Page = "OfficeDocumentInsert.aspx";
                break;
        }

        Response.Redirect(Page + "?PageMode=" + PgMode.Value + "&OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                  + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        Session["TblOfReImg"] = null;
        Session["MeReqUpload"] = null;
        Session["FileOfArm2"] = null;
        Session["FileOfSign2"] = null;
        string Dprt = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        string PageName = "Office.aspx";
        switch (Dprt)
        {
            case "MemberShip":
                PageName = "Office.aspx";
                break;
            case "Document":
                PageName = "OfficeDocument.aspx";
                break;
        }
        string OfId = Utility.DecryptQS(OfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(OfId) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect(PageName + "?PostId=" + OfficeId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {

            Response.Redirect(PageName);
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        int OfmId = -1;
        int OfReId = -1;
        int PersonId = -1;
        string Active = "";
        int OfpId = -1;
        Boolean ShouldInActive = true;
        int OfId = Convert.ToInt32(Utility.DecryptQS(OfficeId.Value));

        if (DevGridViewOfficeMember.FocusedRowIndex > -1)
        {
            DataRow row = DevGridViewOfficeMember.GetDataRow(DevGridViewOfficeMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            OfReId = (int)row["OfReId"];
            PersonId = (int)row["PersonId"];
            Active = row["Active"].ToString();
            OfpId = (int)row["OfpId"];
        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای غیر فعال کردن ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        if (_ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo && OfpId != (int)TSP.DataManager.OfficePosition.ShareHolder)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "در درخواست ''تغییرات اطلاعات پایه و سهامداران'' تنها قادر به تغییر سهامداران شرکت می باشید ";
            return;
        }
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();

        try
        {
            OfMeManager.FindByCode(OfmId);
            if (OfMeManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
                return;
            }
            int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));

            if (OfReId == CurrentOfReId)
            {
                OfMeManager.FindOfficeMembers(OfId, PersonId, 1);//***آیا غیرفعال شده سیستمی برای این کد عضویت وجود دارد؟
                if (OfMeManager.Count <= 0)
                {
                    ShouldInActive = false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Active) && Active != "فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "عضو مورد نظر غیر فعال می باشد";
                    return;
                }

            }
            if (ShouldInActive)
            {
                TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
                RequestInActivesManager.FindByTableIdTableType(OfmId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember), -1, 0);
                if (RequestInActivesManager.Count > 0)
                {
                    if (Convert.ToInt32(RequestInActivesManager[0]["ReqId"]) == CurrentOfReId)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("عضو مورد نظر غیر فعال می باشد");
                        return;
                    }
                }

                if (!InsertInActive(OfmId, CurrentOfReId, OfId, PersonId))
                    return;
            }
            else
            {
                Delete(OfmId, PersonId, OfReId, OfId);
            }
            CheckMenuImageCurrentPage(CurrentOfReId);
            DevGridViewOfficeMember.DataBind();

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
        int OfId = -1;
        int OfmId = -1;
        int PersonId = -1;
        string Active = "";
        int OfpId=-1;
        if (DevGridViewOfficeMember.FocusedRowIndex > -1)
        {
            DataRow row = DevGridViewOfficeMember.GetDataRow(DevGridViewOfficeMember.FocusedRowIndex);
            OfmId = (int)row["OfmId"];
            PersonId = (int)row["PersonId"];
            OfId = (int)row["OfId"];
            Active = row["Active"].ToString();
            OfpId = (int)row["OfpId"];
        }
        if (OfmId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای فعال کردن ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        if (_ReqType == (int)TSP.DataManager.OfficeRequestType.ChangeShareHolderAndBaseInfo && OfpId != (int)TSP.DataManager.OfficePosition.ShareHolder)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "در درخواست ''تغییرات اطلاعات پایه و سهامداران'' تنها قادر به تغییر سهامداران شرکت می باشید ";
            return;
        }
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        try
        {
            OfMeManager.FindEngOfficeMemberByPersonId(PersonId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
            if (OfMeManager.Count > 0)
            {
                int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, PersonId);
                if (dtEngOffReq.Rows.Count > 0)
                {
                    string str = "امکان فعال کردن این عضو وجود ندارد.عضو مورد نظر در دفتر ";
                    str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                    str += " مشغول به کار می باشد";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = (str);
                    return;
                }
            }
            OfMeManager.FindOffMemberByPersonId(PersonId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed);
            if (OfMeManager.Count > 0)
            {
                int OfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                if (OfIdMember != OfId)
                {
                    DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfId, 0, PersonId);
                    if (dtOffReq.Rows.Count > 0)
                    {
                        string str = "امکان فعال کردن این عضو وجود ندارد.عضو مورد نظر در شرکت ";
                        str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
                        str += " مشغول به کار می باشد";
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = (str);
                        return;
                    }
                }
            }

            OfMeManager.FindByCode(OfmId);
            if (OfMeManager.Count == 1)
            {
                int CurrentOfReId = int.Parse(Utility.DecryptQS(OfficeRequest.Value));
                if (!string.IsNullOrEmpty(Active) && Active != "غیر فعال")
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "رکورد مورد نظر فعال می باشد";
                    return;
                }

                int per = DeleteInActive(OfmId, CurrentOfReId, OfId, PersonId);
                switch (per)
                {
                    case 0:
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("ذخیره انجام شد");
                        DevGridViewOfficeMember.DataBind();
                        break;
                    case 1:
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("عضو انتخاب شده فعال می باشد");
                        break;
                    case 2:
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = ("خطایی در ذخیره انجام گرفته است");
                        break;
                }
                DevGridViewOfficeMember.DataBind();
            }
            else
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
            }
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
            case "Agent":
                Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());//+ "&Mode=" + HDMode.Value + "&TP=" + Request.QueryString["TP"]
                break;
            case "Office":
                string Page = "";
                switch (Utility.DecryptQS(HiddenFieldOffice["Department"].ToString()))
                {
                    case "MemberShip":
                        Page = "OfficeInsert.aspx";
                        break;
                    default:
                        Page = "OfficeDocumentInsert.aspx";
                        break;
                }
                Response.Redirect(Page + "?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Letters":
                Response.Redirect("OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Attach":
                Response.Redirect("OfficeAttachment.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Group":
                Response.Redirect("OfficeGroups.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Financial":
                Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                    + "&Dprt=" + HiddenFieldOffice["Department"].ToString());
                break;
        }
    }

    protected void DevGridViewOfficeMember_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data)
            return;
        if (OfficeRequest.Value != null)
        {
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            if (e.GetValue("OfReId") == null)
                return;
            string CurretnOfReId = e.GetValue("OfReId").ToString();
            if (OfReId == CurretnOfReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void DevGridViewOfficeMember_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";

    }

    protected void DevGridViewOfficeMember_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void DevGridViewOfficeMember_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Print")
        {
            DevGridViewOfficeMember.DetailRows.CollapseAllRows();
            DevGridViewOfficeMember.JSProperties["cpDoPrint"] = 1;
        }
    }
    #endregion

    #region Methods

    #region WF
    private void CheckWorkFlowPermissionForOffice()
    {
        CheckWorkFlowPermissionForSaveForOffice();
    }

    private void CheckWorkFlowPermissionForSaveForOffice()
    {
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPer2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage((int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPer2.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPer2.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive1.Enabled = btnActive.Enabled = btnActive1.Enabled = WFPer.BtnInactive || WFPer2.BtnInactive;
    }

    private Boolean CheckPermitionForEditForOffice(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveOfficeInfo;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeMembershipConfirming;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
           
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            if (CurrentTaskCode == TaskCode || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                    if (FirstTaskCode == TaskCode)
                    {
                        
                            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                            int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, (int)TSP.DataManager.WorkFlowTask.MembershipUnitConfirmingOffice, Utility.GetCurrentUser_UserId());
                            if (Permission > 0 || Permission2 > 0)
                                return true;
                        
                    }
                }
            }
        }
        return false;

    }

    //**************Office Doc*********************

    private void CheckWorkFlowPermissionForDoc()
    {
        CheckWorkFlowPermissionForSaveForDoc();
    }

    private void CheckWorkFlowPermissionForSaveForDoc()
    {
        //****TableId
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TaskCode2 = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission WFPer2 = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode2, WfCode, int.Parse(OfReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnNew"] = BtnNew.Enabled = BtnNew2.Enabled = WFPer.BtnNew || WFPer2.BtnNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || WFPer2.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive1.Enabled = btnActive.Enabled = btnActive1.Enabled = WFPer.BtnInactive || WFPer2.BtnInactive;
    }

    private Boolean CheckPermitionForEditForDoc(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfOfficeConfirmingSaveInfo;
        int TaskCode2 = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentOff;
        int TableType = (int)TSP.DataManager.TableCodes.OfficeRequest;
        int WfCode = (int)TSP.DataManager.WorkFlows.OfficeConfirming;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            //int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

            if (CurrentTaskCode == TaskCode || CurrentTaskCode == TaskCode2)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                    if (FirstTaskCode == TaskCode)
                    {
                            int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                            int Permission2 = TaskDoerManager.CheckWorkFlowPermissionForEditInfoByWfCode(WfCode, TableId, TaskCode2, Utility.GetCurrentUser_UserId());
                            if (Permission > 0 || Permission2 > 0)
                                return true;
                        
                    }
                }
            }
        }
        return false;

    }

    #endregion

    protected void Delete(int OfmId, int PersonId, int OfReId, int OfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();

        trans.Add(OfMeManager);
        trans.Add(ReqManager);
        trans.Add(MeMjManager);

        try
        {
            trans.BeginSave();
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                return;
            }
            OfMeManager.FindByCode(OfmId);

            OfMeManager[0].Delete();
            OfMeManager.Save();

            #region SetMFNo
            if (!SetOfficeMfNo(OfId, OfReId, PrCode, ReqManager, OfMeManager, MeMjManager))
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            #region Comment
            //ReqManager.FindByCode(OfReId);
            //if (ReqManager.Count == 1)
            //{
            //    #region SaveImpGrade
            //    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
            //    {
            //        int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
            //        if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
            //        {
            //            ReqManager[0].BeginEdit();
            //            ReqManager[0]["GrdId"] = OfMeManager.FindOffImpGrade(OfId, OfReId);
            //            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            //            ReqManager[0]["ModifiedDate"] = DateTime.Now;
            //            ReqManager[0].EndEdit();
            //            ReqManager.Save();
            //            ReqManager.DataTable.AcceptChanges();
            //        }
            //    }
            //    #endregion

            //    DataTable dtMj = MeMjManager.SelectMemberMasterMajor(PersonId);
            //    if (dtMj.Rows.Count > 0)
            //    {
            //        int Del = 0;
            //        //int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
            //        int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
            //        //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
            //        int i = -1;
            //        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFNo"]))
            //        {
            //            string MFNo = ReqManager[0]["MFNo"].ToString();
            //            if (!string.IsNullOrEmpty(MFNo))
            //            {
            //                string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            //                char[] Code = MFNoMajor[2].ToCharArray();

            //                switch (ParentId)
            //                {
            //                    case (int)TSP.DataManager.MainMajors.Architecture:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Civil:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Electronic:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Mapping:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Mechanic:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Traffic:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Urbanism:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    default:
            //                        i = -1;
            //                        break;

            //                }
            //                if (i != -1)
            //                {
            //                    // Code[i] = '1';

            //                    MFNoMajor[2] = new string(Code);//Code.ToString();
            //                    ReqManager[0].BeginEdit();
            //                    ReqManager[0]["MFNo"] = string.Join("-", MFNoMajor);
            //                    ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            //                    ReqManager[0].EndEdit();
            //                    ReqManager.Save();
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion
            #endregion

            trans.EndSave();
            Session["FillGrid"] = FillGrid(Convert.ToInt32(Utility.DecryptQS(OfficeId.Value)), Convert.ToInt32(Utility.DecryptQS(OfficeRequest.Value)));
            DevGridViewOfficeMember.DataSource = (DataTable)Session["FillGrid"];
            DevGridViewOfficeMember.DataBind();
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

    protected DataTable FillGrid(int OfId, int OfReId)
    {
        //bool IsDsgnObs = false;
        //bool IsImp = false;

        //TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        //OfficeManager.FindByCode(OfId);
        //if (!Utility.IsDBNullOrNullValue(OfficeManager[0]["MFType"]))
        //{
        //    if (Convert.ToInt32(OfficeManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)
        //        IsDsgnObs = true;
        //    else if (Convert.ToInt32(OfficeManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
        //        IsImp = true;
        //    else if (Convert.ToInt32(OfficeManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement)
        //    {
        //        IsDsgnObs = true;
        //        IsImp = true;
        //    }
        //}

        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        //TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        //ReqManager.FindByCode(OfReId);

        DataTable dt = new DataTable();
        //if (ReqManager[0]["IsConfirm"].ToString() == "0") //Not Answered
        //    dt = OfficeMemberManager.FindByOffId(OfId, OfReId, -1);
        //else
        dt = OfficeMemberManager.FindByOffRequest(OfId, OfReId, -1);

        dt.DefaultView.RowFilter = "SysInActive=0";

        ////  ArrayList arr = new ArrayList();
        //Capacity Capacity = new Capacity();
        //Capacity.OfficeCapacity OfficeCapacity = new Capacity.OfficeCapacity();
        //int PersonId = 0;
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    //************************************************************
        //    //موقتا برداشته شده که بعد با سی ال آر اس پی این توابع درون اس کیو ال برود و همین گرید را داشته باشیم
        //    //************************************************************
        //    PersonId = Convert.ToInt32(dt.Rows[i]["PersonId"]);
        //    // OfficeCapacity = Capacity.GetOfficeDsgCapacity(OfId, (int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.DocOffIncreaseJobCapacityType.Office, PersonId);
        //    if (IsDsgnObs)
        //        dt.Rows[i]["DsgnCapacity"] = "---"; //OfficeCapacity.OfficeMemberMaxJobCapacity;
        //    else
        //        dt.Rows[i]["DsgnCapacity"] = "---";
        //    //?????? if (IsImp)
        //    //??????  {
        //    //??????  ArrayList ImpCapacity = (ArrayList)Capacity.GetMembersImpCapacity(Convert.ToInt32(dt.Rows[j]["PersonId"]));
        //    //?????? if (ImpCapacity.Count != 0)
        //    //??????   dt.Rows[j]["ImpCapacity"] = ImpCapacity[1];
        //    //??????  }
        //    //??????  else
        //    dt.Rows[i]["ImpCapacity"] = "---";
        //    //?????dt.Rows[j]["DsgnFactor"] = Convert.ToDouble(((ArrayList)arr[i])[10]) / Convert.ToDouble(((ArrayList)arr[i])[1]); 
        //    if (IsDsgnObs)
        //        dt.Rows[i]["ObsCapacity"] = "---"; //OfficeCapacity.OfficeMemberMaxJobCapacity;
        //    else
        //        dt.Rows[i]["ObsCapacity"] = "---";
        //    //???????dt.Rows[j]["ObsFactor"] = Convert.ToDouble(((ArrayList)arr[i])[11]) / Convert.ToDouble(((ArrayList)arr[i])[1]); //(Convert.ToInt32(((ArrayList)arr[i])[7]) / 100) + 1 + (Convert.ToInt32(((ArrayList)arr[i])[8]) / 100) + 1 + (Convert.ToInt32(((ArrayList)arr[i])[9]) / 100) + 1;
        //}

        return dt;
    }

    protected bool InsertInActive(int OfmId, int OfReId, int OfId, int PersonId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TransactionManager.Add(ReqManager);
        TransactionManager.Add(Manager);
        TransactionManager.Add(MeMjManager);
        TransactionManager.Add(OfMeManager);
        try
        {
            TransactionManager.BeginSave();
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                return false;
            }

            DataRow dr = Manager.NewRow();
            dr["TableId"] = OfmId;
            dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            dr["ReqId"] = OfReId;
            dr["ReqType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeRequest);
            dr["InActive"] = 1;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            Manager.AddRow(dr);
            Manager.Save();
            Manager.DataTable.AcceptChanges();
            #region SetMFNo
            if (!SetOfficeMfNo(OfId, OfReId, PrCode, ReqManager, OfMeManager, MeMjManager))
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return false;
            }
            #region Comment
            //ReqManager.FindByCode(OfReId);
            //if (ReqManager.Count == 1)
            //{
            //    #region SaveImpGrade
            //    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
            //    {
            //        int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
            //        if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
            //        {
            //            ReqManager[0].BeginEdit();
            //            ReqManager[0]["GrdId"] = OfMeManager.FindOffImpGrade(OfId, OfReId);
            //            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            //            ReqManager[0]["ModifiedDate"] = DateTime.Now;
            //            ReqManager[0].EndEdit();
            //            ReqManager.Save();
            //            ReqManager.DataTable.AcceptChanges();
            //        }
            //    }
            //    #endregion

            //    DataTable dtMj = MeMjManager.SelectMemberMasterMajor(PersonId);
            //    if (dtMj.Rows.Count > 0)
            //    {
            //        int Del = 0;
            //        int MjId = int.Parse(dtMj.Rows[0]["MjId"].ToString());
            //        int ParentId = int.Parse(dtMj.Rows[0]["FMjParentId"].ToString());
            //        //string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
            //        int i = -1;
            //        if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFNo"]))
            //        {
            //            string MFNo = ReqManager[0]["MFNo"].ToString();
            //            if (!string.IsNullOrEmpty(MFNo))
            //            {
            //                string[] MFNoMajor = MFNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            //                char[] Code = MFNoMajor[2].ToCharArray();

            //                switch (ParentId)
            //                {
            //                    case (int)TSP.DataManager.MainMajors.Architecture:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Architecture;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Civil:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Civil;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Electronic:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Electronic;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Mapping:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mapping;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Mechanic:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Mechanic;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Traffic:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Traffic;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    case (int)TSP.DataManager.MainMajors.Urbanism:
            //                        i = (int)TSP.DataManager.DocumentOfficeMeMajor.Urbanism;
            //                        Code[i] = Del.ToString()[0];
            //                        break;
            //                    default:
            //                        i = -1;
            //                        break;

            //                }
            //                if (i != -1)
            //                {
            //                    MFNoMajor[2] = new string(Code);//Code.ToString();
            //                    ReqManager[0].BeginEdit();
            //                    ReqManager[0]["MFNo"] = string.Join("-", MFNoMajor);
            //                    ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            //                    ReqManager[0].EndEdit();
            //                    ReqManager.Save();
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion
            #endregion

            TransactionManager.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            Session["FillGrid"] = FillGrid(OfId, OfReId);
            DevGridViewOfficeMember.DataSource = (DataTable)Session["FillGrid"];
            DevGridViewOfficeMember.DataBind();
            return true;
        }
        catch (Exception err)
        {

            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
            return false;
        }
    }

    // 0 successful 1 not exist 2 error
    protected int DeleteInActive(int OfmId, int OfReId, int OfId, int PersonId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TSP.DataManager.DocMemberFileMajorManager MeMjManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TransactionManager.Add(ReqManager);
        TransactionManager.Add(RequestInActivesManager);
        TransactionManager.Add(MeMjManager);
        TransactionManager.Add(OfMeManager);
        try
        {
            TransactionManager.BeginSave();
            int PrId = Utility.GetCurrentProvinceId();
            ProvinceManager.FindByCode(PrId);
            string PrCode = "";

            if (ProvinceManager.Count > 0)
            {
                PrCode = ProvinceManager[0]["NezamCode"].ToString();
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است";
                return 2;
            }
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.OfficeMember);
            RequestInActivesManager.FindByTableIdTableType(OfmId, TableType, -1, 0, 0); //(MReId, MlId);
            if (RequestInActivesManager.Count != 1) return 1;
            RequestInActivesManager[0].Delete();
            if (RequestInActivesManager.Save() <= 0) return 2;

            #region SetMFNo
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count == 1)
            {
                #region SaveImpGrade
                if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
                {
                    int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
                    if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
                    {
                        ReqManager[0].BeginEdit();
                        ReqManager[0]["GrdId"] = OfMeManager.FindOffImpGrade(OfId, OfReId);
                        ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        ReqManager[0]["ModifiedDate"] = DateTime.Now;
                        ReqManager[0].EndEdit();
                        ReqManager.Save();
                        ReqManager.DataTable.AcceptChanges();
                    }
                }
                #endregion
                if (!SetOfficeMfNo(OfId, OfReId, PrCode, ReqManager, OfMeManager, MeMjManager))
                {
                    TransactionManager.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    return 2;
                }
            }

            #endregion

            TransactionManager.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            Session["FillGrid"] = FillGrid(OfId, OfReId);
            DevGridViewOfficeMember.DataSource = (DataTable)Session["FillGrid"];
            DevGridViewOfficeMember.DataBind();

            return 0;
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
            return 2;
        }
    }

    protected void CheckMenuImage(int OfReId)
    {
        TSP.DataManager.OfficeAgentManager OffAgentManager = new TSP.DataManager.OfficeAgentManager();
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficialLetterManager OffLetterManager = new TSP.DataManager.OfficialLetterManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
        TSP.DataManager.AttachmentsManager AttachmentsManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.DocOffOfficeFinancialStatusManager OffFinancialManager = new TSP.DataManager.DocOffOfficeFinancialStatusManager();
        TSP.DataManager.GroupDetailManager GrdManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.OfficeManager officeManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager officeRequestManager = new TSP.DataManager.OfficeRequestManager();



        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Agent
        arr.Add(0);//arr[1]-->Member
        arr.Add(0);//arr[2]-->Letters
        arr.Add(0);//arr[3]-->Job
        arr.Add(0);//arr[4]-->Attach
        arr.Add(0);//arr[5]-->Financial
        arr.Add(0);//arr[6]-->Office
        arr.Add(0);//arr[7]-->Group


        officeRequestManager.FindByCode(OfReId);
        if (officeRequestManager.Count > 0)
        {
            int OfId = Convert.ToInt32(officeRequestManager[0]["OfId"]);
            officeManager.FindByCode(OfId);
            if (officeManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
                arr[6] = 1;
            }
        }


        OffAgentManager.FindForDelete(OfReId);
        if (OffAgentManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        OffMemberManager.FindForDelete(OfReId, 0);
        if (OffMemberManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        OffLetterManager.FindForDelete(OfReId);
        if (OffLetterManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(1, OfReId, (int)TSP.DataManager.TableCodes.OfficeRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        OffFinancialManager.FindForDelete(OfReId);
        if (OffFinancialManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            arr[5] = 1;
        }

        Session["OffMenuArrayList"] = arr;
    }

    protected void CheckMenuImageCurrentPage(int OfReId)
    {
        TSP.DataManager.OfficeMemberManager OffMemberManager = new TSP.DataManager.OfficeMemberManager();
        OffMemberManager.FindForDelete(OfReId, 0);

        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffMemberManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "";
                arr[1] = 0;

            }
            Session["OffMenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(OfReId);
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];
            if (OffMemberManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
                arr[1] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "";
                arr[1] = 0;

            }
            Session["OffMenuArrayList"] = arr;

        }

    }

    protected void SetMenuItem()
    {
        if (Session["OffMenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["OffMenuArrayList"];

            if ((int)arr[0] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Agent")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Letters")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Financial")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[6] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Office")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            CheckMenuImage(int.Parse(Utility.DecryptQS(OfficeRequest.Value)));

        }
    }

    private void CheckPermission()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string Department = Utility.DecryptQS(HiddenFieldOffice["Department"].ToString());
        if (Department == "MemberShip")
        {
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnActive.Enabled = per.CanEdit;
            btnActive1.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive1.Enabled = per.CanEdit;
            DevGridViewOfficeMember.Visible = per.CanView;
        }
        else if (Department == "Document")
        {
            TSP.DataManager.Permission per = TSP.DataManager.OfficeMemberManager.GetUserPermissionForOffDoc(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnActive.Enabled = per.CanEdit;
            btnActive1.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive1.Enabled = per.CanEdit;
            DevGridViewOfficeMember.Visible = per.CanView;
        }

    }

    private Boolean SetOfficeMfNo(int OfId, int OfReId, string PrCode, TSP.DataManager.OfficeRequestManager ReqManager, TSP.DataManager.OfficeMemberManager OffMemManager, TSP.DataManager.DocMemberFileMajorManager MeMjManager)
    {
        string MFNo = "";
        string MFMjCode = "0000000";
        string MFCode = "";
        ReqManager.FindByCode(OfReId);
        if (ReqManager.Count != 1)
        {
            return false;
        }
        if (Convert.ToInt32(ReqManager[0]["DocumentStatus"]) == (int)TSP.DataManager.OfficeDocumentStatus.DoNotHaveDocument
            || Convert.ToInt32(ReqManager[0]["DocumentStatus"]) == (int)TSP.DataManager.OfficeDocumentStatus.DocumentCancel)
            return true;
        #region SaveImpGrade
        if (Utility.IsDBNullOrNullValue(ReqManager[0]["MFType"]))
        {
            return true;
        }
        int MFType = Convert.ToInt32(ReqManager[0]["MFType"]);
        if (MFType == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)
        {
            ReqManager[0].BeginEdit();
            ReqManager[0]["GrdId"] = OffMemManager.FindOffImpGrade(OfId, OfReId);
            ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ReqManager[0]["ModifiedDate"] = DateTime.Now;
            ReqManager[0].EndEdit();
            ReqManager.Save();
            ReqManager.DataTable.AcceptChanges();
        }

        #endregion

        //MFMjCode = Convert.ToInt32(ReqManager["MFType"]);
        if (Convert.ToInt32(ReqManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign)//طراح و ناظر
        {
            MFCode = TSP.DataManager.OfficeManager.ObservationAndDesignMFType.ToString();

        }
        else if (Convert.ToInt32(ReqManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement)//مجری
        {
            MFCode = TSP.DataManager.OfficeManager.ImplementMFType.ToString();
        }
        //****************

        //   #region SetMFNo
        DataTable dtOfMe = OffMemManager.SelectOfficeMember(OfId, 1, -1, 0);//return member
        for (int m = 0; m < dtOfMe.Rows.Count; m++)
        {
            DataTable dtMj = MeMjManager.SelectMemberMasterMajor(int.Parse(dtOfMe.Rows[m]["PersonId"].ToString()));
            if (dtMj.Rows.Count > 0)
            {
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

        ReqManager.FindByCode(OfReId);
        if (ReqManager.Count != 1)
            return false;
        string MFSerialNo = ReqManager[0]["MFSerialNo"].ToString();
        while (MFSerialNo.Length < 5)
        {
            MFSerialNo = "0" + MFSerialNo;
        }
        ReqManager[0]["MFNo"] = MFNo = MFCode + "-" + PrCode + "-" + MFMjCode + "-" + MFSerialNo;
        ReqManager.Save();
        return true;

        return true;
    }
    #endregion
}
