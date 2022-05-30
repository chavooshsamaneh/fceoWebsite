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

public partial class Employee_MembersRegister_MemberGroups : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

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
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.MemberManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            CustomAspxDevGridView1.Visible = per.CanView;
            btnSave.Enabled = per.CanNew;

            Session["GrMeCheched"] = false;
            CustomAspxDevGridView2.Selection.UnselectAll();

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("Members.aspx");
                return;
            }
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"]).ToString();
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"].ToString());
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string MeId = Utility.DecryptQS(MemberId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            switch (Mode)
            {
                case "Home":
                    MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Visible = false;

                    break;
                case "Request":
                    SetMenuItem();
                    //MenuTop.Items[MenuTop.Items.IndexOfName("Member")].Visible = false;

                    break;
            }

            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(int.Parse(MeId));

            ObjectDataSource1.SelectParameters["MeId"].DefaultValue = MeId;


            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        FillMemberName();
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }

    protected void MenuTop_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string TempMe = "";
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Member":
                Response.Redirect("MemberRegister.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Activity":
                Response.Redirect("MemberActivity.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Message":
                Response.Redirect("MemberMessage.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Attach":
                Response.Redirect("MemberAttachment.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"] + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "Request":
                Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Request.QueryString["Mode"]
                    + "&TP=" + Request.QueryString["TP"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
                 + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));
                break;
            case "PollAnswer":
                Response.Redirect("ReportMemberPollAnswers.aspx?MeId=" + MemberId.Value +
                    "&PageMode=" + PgMode.Value +
                    "&MReId=" + Request.QueryString["MReId"] +
                    "&Mode=" + Request.QueryString["Mode"] +
                    "&TP=" + Request.QueryString["TP"] +
                    "&GrdFlt=" + Request.QueryString["GrdFlt"] +
                    "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
            case "AccFish":
                Response.Redirect("MembersAccounting.aspx?MeId=" + MemberId.Value + "&MReId=" + Request.QueryString["MReId"] + "&Mode=" + Utility.EncryptQS(Mode) + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + Request.QueryString["PageMode"] + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.GroupDetailManager GrdManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.GroupDetailManager DetailManager = new TSP.DataManager.GroupDetailManager();

        DetailManager.Fill();
        bool flag = false;
        try
        {
            for (int i = 0; i < CustomAspxDevGridView2.VisibleRowCount; i++)
            {

                DataRow dr = GrdManager.NewRow();

                if (CustomAspxDevGridView2.Selection.IsRowSelected(i))
                {
                    DataRow row = CustomAspxDevGridView2.GetDataRow(i);

                    DetailManager.FindByMeId(int.Parse(row["GrId"].ToString()), int.Parse(Utility.DecryptQS(MemberId.Value)), 1);
                    if (DetailManager.Count != 0)
                    {
                        flag = true;
                        continue;
                    }
                    else
                    {

                        dr["GrId"] = row["GrId"];
                        dr["MeId"] = Utility.DecryptQS(MemberId.Value);
                        dr["MeType"] = 1;
                        dr["ModifiedDate"] = DateTime.Now;
                        dr["UserId"] = Utility.GetCurrentUser_UserId();
                        GrdManager.AddRow(dr);
                        flag = true;
                    }
                }
            }
            if (flag == true)
            {
                int cnt = GrdManager.Save();
                if (cnt > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " ذخیره انجام شد";
                    CustomAspxDevGridView1.DataBind();
                    CustomAspxDevGridView2.DataBind();
                    CustomAspxDevGridView2.Selection.UnselectAll();

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "گروه انتخاب شده تکراری می باشد";
                    CustomAspxDevGridView2.DataBind();
                    CustomAspxDevGridView2.Selection.UnselectAll();


                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "گروهی انتخاب نشده است";
                CustomAspxDevGridView2.DataBind();
                CustomAspxDevGridView2.Selection.UnselectAll();
            }


        }

        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }

        }


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        int GrdId = -1;
        TSP.DataManager.GroupDetailManager GrManager = new TSP.DataManager.GroupDetailManager();

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GrdId = (int)row["GrdId"];
        }
        if (GrdId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            GrManager.FindByCode(GrdId);
            if (GrManager.Count == 1)
            {
                try
                {
                    GrManager[0].Delete();

                    int cn = GrManager.Save();
                    if (cn == 1)
                    {
                        CustomAspxDevGridView1.DataBind();

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                    }

                }
                catch (Exception err)
                {

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 547)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }
                }

            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        string TempMe = "";
        if (Mode == "Request")
            TempMe = "0";
        else if (Mode == "TempMe")
            TempMe = "1";
        if (Mode == "Home")
        {
            Response.Redirect("MemberRegister.aspx?MeId=" + MemberId.Value + "&PageMode=" + PgMode.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

        }
        else
            Response.Redirect("MemberInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + Request.QueryString["MReId"] + "&TP=" + Request.QueryString["TP"] + "&PageMode=" + PgMode.Value
                + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]
         + "&TMe=" + Utility.EncryptQS(TempMe) + "&Pt=" + Utility.EncryptQS("2"));
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        BackToManagementPage();
    }

    protected void CustomAspxDevGridView2_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {

        if (CustomAspxDevGridView2.Selection.Count == CustomAspxDevGridView2.VisibleRowCount)
        {
            Session["GrMeCheched"] = true;
            //e.Button.Text = "عدم انتخاب همه";
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                //e.Column.SelectButton.Text =
                SelectButton.Text = "عدم انتخاب همه";


        }
        else
        {
            Session["GrMeCheched"] = false;
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                //e.Column.SelectButton.Text =
                SelectButton.Text = "انتخاب همه";
            // e.Button.Text = "انتخاب همه";
        }
    }

    protected void CustomAspxDevGridView2_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (Convert.ToBoolean(Session["GrMeCheched"]))
        {
            Session["GrMeCheched"] = false;
            CustomAspxDevGridView2.Selection.UnselectAll();
            SelectButton.Text = "انتخاب همه";

        }
        else
        {
            Session["GrMeCheched"] = true;
            CustomAspxDevGridView2.Selection.SelectAll();
            SelectButton.Text = "عدم انتخاب همه";
        }
    }

    protected void SetMenuItem()
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];

            if ((int)arr[0] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[5] == 1)
            {
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Url = "~/Images/icons/Check.png";
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Width = Utility.MenuImgSize;
                MenuTop.Items[MenuTop.Items.IndexOfName("Request")].Image.Height = Utility.MenuImgSize;
            }
        }
        else
        {
            string MReId = Request.QueryString["MReId"];
            if (!string.IsNullOrEmpty(Utility.DecryptQS(MReId)))
                CheckMenuImage(int.Parse(Utility.DecryptQS(MemberId.Value)), int.Parse(Utility.DecryptQS(MReId)));

        }

    }

    protected void CheckMenuImage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();
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
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        if (ProjectJobHistoryManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }
        AttachmentsManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.MemberRequest, MReId, (short)TSP.DataManager.AttachType.Attachments);
        if (AttachmentsManager.Count > 0)
        {
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Url = "~/Images/icons/Check.png";
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Width = Utility.MenuImgSize;
            MenuTop.Items[MenuTop.Items.IndexOfName("Attach")].Image.Height = Utility.MenuImgSize;
            arr[4] = 1;
        }
        Session["MenuArrayList"] = arr;
    }

    private void FillMemberName()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        string Mode = Utility.DecryptQS(HDMode.Value);
        MemberInfoUserControl1.MeId = Convert.ToInt32(MeId);
        if (Mode == "TempMe")
        {
            MemberInfoUserControl1.IsMeTemp = true;
        }
        MemberInfoUserControl1.MReId = Convert.ToInt32(Utility.DecryptQS(MemberRequest.Value));
    }

    private void BackToManagementPage()
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        string UserName = "";
        if (Mode == "Request")
            UserName = Utility.DecryptQS(MemberId.Value);
        else if (Mode == "TempMe")
            UserName = "M" + Utility.DecryptQS(MemberId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Members.aspx?PostId=" + Utility.EncryptQS(UserName) + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt + "&MReId=" + Request.QueryString["MReId"]);
        }
        else
        {
            Response.Redirect("Members.aspx?MReId=" + Request.QueryString["MReId"]);
        }
    }
}
