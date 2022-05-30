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

public partial class Employee_AddGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.GroupManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                GroupId.Value = Server.HtmlDecode(Request.QueryString["GrId"]).ToString();
            }
            catch (Exception)
            { }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string GrId = Utility.DecryptQS(GroupId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(GrId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;

                    FillForm(int.Parse(GrId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    lblCreateDate.Visible = false;
                    txtCreateDate.Visible = false;
                    Enable();
                    ASPxRoundPanel2.HeaderText = "جدید";
                    BtnNew.Enabled = false;
                    btnNew2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;

                    break;
                case "Edit":
                    Enable();

                    if (string.IsNullOrEmpty(GrId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    FillForm(int.Parse(GrId));
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    break;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        btnBack.PostBackUrl = "Groups.aspx";
        btnBack2.PostBackUrl = "Groups.aspx";

    }
    protected void FillForm(int GrId)
    {
        TSP.DataManager.GroupManager GrManager = new TSP.DataManager.GroupManager();
        TSP.DataManager.GroupDetailManager DetailManager = new TSP.DataManager.GroupDetailManager();

        GrManager.FindByCode(GrId);
        if (GrManager.Count == 1)
        {
            txtCreateDate.Text = GrManager[0]["CreateDate"].ToString();
            txtDescription.Text = GrManager[0]["Description"].ToString();
            txtName.Text = GrManager[0]["GrName"].ToString();

            DetailManager.FindByGrId(GrId, 1);
            DetailManager.CurrentFilter = "MeType=1";

            for (int i = 0; i < DetailManager.Count; i++)
            {
                txtMeNo.Text += DetailManager[i]["MeId"].ToString() + ";";
            }
            //txtMeNo.Text[txtMeNo.Text.Length-1] = "";
            if (!string.IsNullOrEmpty(txtMeNo.Text))
                txtMeNo.Text = txtMeNo.Text.Remove(txtMeNo.Text.Trim().Length - 1);
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";

        }

    }
    protected void ClearForm()
    {
        txtDescription.Text = "";
        txtMeNo.Text = "";
        txtName.Text = "";

    }
    protected void Disable()
    {
        txtDescription.Enabled = false;
        txtMeNo.Enabled = false;
        txtName.Enabled = false;

    }
    protected void Enable()
    {
        txtDescription.Enabled = true;
        txtMeNo.Enabled = true;
        txtName.Enabled = true;
    }
    protected void Edit(int GrId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.GroupDetailManager DetailManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.GroupManager GrManager = new TSP.DataManager.GroupManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        trans.Add(GrManager);
        trans.Add(DetailManager);
        //  trans.Add(MemberManager);
        try
        {
            string MemberNotExist = "";
            string[] arr = txtMeNo.Text.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            //TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            String InvalidMember = MemberManager.SelectInvalidMemberNo(txtMeNo.Text);
           string[] arrInvalidMember = InvalidMember.Split(';');
            if (InvalidMember != "")
            {
                txtMeNoNotExist.Text = InvalidMember;
            }

            GrManager.FindByCode(GrId);
            if (GrManager.Count == 1)
            {
                GrManager[0].BeginEdit();

                GrManager[0]["GrName"] = txtName.Text;
                GrManager[0]["Description"] = txtDescription.Text;
                GrManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                GrManager[0]["ModifiedDate"] = DateTime.Now;

                GrManager[0].EndEdit();

                trans.BeginSave();

                int cn = GrManager.Save();
                if (cn == 1)
                {
                    DetailManager.DataTable.Columns.Add("IsExist", typeof(bool));
                    DetailManager.FindByGrId(GrId, 1);


                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (!Utility.IsDBNullOrNullValue(arr[i]))
                        {
                            if (InvalidMember != "" && Array.IndexOf(arrInvalidMember, arr[i])>-1)
                            {
                                //int pos = ;
                                //MemberManager.FindByCode(Convert.ToInt32(arr[i]));
                                //if (MemberManager.Count == 0)
                                MemberNotExist += " " + arr[i];
                            }
                            else
                            {
                                DetailManager.CurrentFilter = "MeId=" + arr[i] + " and MeType=1";
                                if (DetailManager.Count == 0)
                                {
                                    DataRow drMe = DetailManager.NewRow();
                                    drMe["GrId"] = GrManager[0]["GrId"];
                                    drMe["MeId"] = arr[i];
                                    drMe["MeType"] = 1;
                                    drMe["UserId"] = Utility.GetCurrentUser_UserId();
                                    drMe["ModifiedDate"] = DateTime.Now;
                                    drMe["IsExist"] = true;

                                    DetailManager.AddRow(drMe);

                                }
                                else
                                {
                                    DetailManager[0]["IsExist"] = true;
                                    DetailManager[0].AcceptChanges();

                                }
                            }
                        }
                    }
                    DetailManager.CurrentFilter = "IsExist is null " + " and MeType=1";
                    if (DetailManager.Count > 0)
                    {
                        int c = DetailManager.Count;
                        for (int i = 0; i < c; i++)
                            DetailManager[0].Delete();

                    }

                    DetailManager.Save();

                    trans.EndSave();
                    GroupId.Value = Utility.EncryptQS(GrManager[0]["GrId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    // txtMeNoNotExist.Text = MemberNotExist;
                }

                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            }



        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
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
    protected void Insert()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.GroupDetailManager DetailManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.GroupManager GrManager = new TSP.DataManager.GroupManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        trans.Add(GrManager);
        trans.Add(DetailManager);
        //trans.Add(MemberManager);

        try
        {

            string[] arr = txtMeNo.Text.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            String InvalidMember = MemberManager.SelectInvalidMemberNo(txtMeNo.Text);
            string[] arrInvalidMember = InvalidMember.Split(';');
            if (InvalidMember != "")
            {
                txtMeNoNotExist.Text = InvalidMember;
            }
            //if (arr.Length == 1)
            //{
            for (int i = 0; i < arr.Length; i++)
            {
                int result;
                if (!int.TryParse(arr[i], out result))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد عضویت اعضای گروه با فرمت نادرست وارد شده است";
                    return;
                }
            }
            //  }

            string MemberNotExist = "";
            //string[] arr = txtMeNo.Text.Trim().Split(';',StringSplitOptions.RemoveEmptyEntries);
            DataRow row = GrManager.NewRow();
            row["GrName"] = txtName.Text;
            row["Description"] = txtDescription.Text;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            row["CreateDate"] = Utility.GetDateOfToday();

            GrManager.AddRow(row);
            trans.BeginSave();

            int cn = GrManager.Save();
            if (cn == 1)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (!Utility.IsDBNullOrNullValue(arr[i]))
                    {
                        if (InvalidMember != "" && Array.IndexOf(arrInvalidMember, arr[i]) > -1)
                        {
                            //MemberManager.FindByCode(Convert.ToInt32(arr[i]));
                            //if (MemberManager.Count == 0)
                            MemberNotExist += " " + arr[i];
                        }
                        else
                        {
                            DataRow drMe = DetailManager.NewRow();
                            drMe["GrId"] = GrManager[0]["GrId"];
                            drMe["MeId"] = arr[i];
                            drMe["MeType"] = 1;
                            drMe["UserId"] = Utility.GetCurrentUser_UserId();
                            drMe["ModifiedDate"] = DateTime.Now;
                            DetailManager.AddRow(drMe);
                        }
                    }
                }

                DetailManager.Save();
                trans.EndSave();
                GroupId.Value = Utility.EncryptQS(GrManager[0]["GrId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
              //  txtMeNoNotExist.Text = MemberNotExist;
            }

            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        lblCreateDate.Visible = false;
        txtCreateDate.Visible = false;
        GroupId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();

        TSP.DataManager.Permission per = TSP.DataManager.GroupManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string GrId = Utility.DecryptQS(GroupId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                Insert();
            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(GrId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(GrId));
                }

            }

        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        string pageMode = Utility.DecryptQS(PgMode.Value);
        string GrId = Utility.DecryptQS(GroupId.Value);

        if (string.IsNullOrEmpty(GrId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(pageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();
                ASPxRoundPanel2.HeaderText = "ویرایش";

                TSP.DataManager.Permission per = TSP.DataManager.GroupManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
            }

        }

    }
}
