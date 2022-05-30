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

public partial class Teachers_Amoozesh_PeriodTestMarks : System.Web.UI.Page
{
    int _PPId
    {
        set
        {
            PeriodId.Value = value.ToString();
        }
        get
        {
            return Convert.ToInt32(PeriodId.Value);
        }
    }

    int _InsId
    {
        set
        {
            InstitueId.Value = value.ToString();
        }
        get
        {
            return Convert.ToInt32(InstitueId.Value);
        }
    }

    string _PgMode
    {
        set
        {
            PgMode.Value = value;
        }
        get
        {
            return PgMode.Value;
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetKey();
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave1.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void ButtonRet_Click(object sender, EventArgs e)
    {
        Response.Redirect("Period.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int TeId = Utility.GetCurrentUser_MeId();
       

        if (_PPId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
        {
            if (HDPageMode.Value == "New")
                Insert(TeId, _PPId);
            else
                Edit(_PPId);
        }
    }

    protected void btnObjSave_Click(object sender, EventArgs e)
    {
        int PRId = -1;
        TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        if (string.IsNullOrEmpty(txtPRId.Text))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در دخیره اطلاعات رخ داده است";
            return;
        }
        else
        {
            PRId = int.Parse(txtPRId.Text);
            MarkManager.FindByPRCode(PRId);
            if (MarkManager.Count == 1)
            {
                MarkManager[0].BeginEdit();
                MarkManager[0]["TeId"] = Utility.GetCurrentUser_MeId();
                MarkManager[0]["TeObjectionDate"] = Utility.GetDateOfToday();
                MarkManager[0]["TeObjectionText"] = txtTeObjText.Text;
                MarkManager[0]["LastMark"] = txtLastMark.Text;
                MarkManager[0].EndEdit();
                if (MarkManager.Save() > 0)
                {
                    GridViewTestMarks.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در دخیره اطلاعات رخ داده است";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در دخیره اطلاعات رخ داده است";
            }
        }
    }

    protected void GridViewTestMarks_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;

        string Mode = Utility.DecryptQS(PgMode.Value);
        //  if (Mode == "Objection")
        // {
        int PRId = int.Parse(e.KeyValue.ToString());
        TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        MarkManager.FindByPRCode(PRId);
        if (MarkManager.Count != 0 && string.IsNullOrEmpty(MarkManager[0]["MeObjectionDate"].ToString()))
        {
            DevExpress.Web.ASPxHyperLink lnk = (DevExpress.Web.ASPxHyperLink)GridViewTestMarks.FindRowCellTemplateControl(e.VisibleIndex, null, "ASPxHyperLink1");
            if (lnk != null)
                lnk.Visible = false;
        }
        // }

    }

    //protected void btnConfirm_Click(object sender, EventArgs e)
    //{
    //    TSP.DataManager.PeriodPresentManager Manager = new TSP.DataManager.PeriodPresentManager();
    //    try
    //    {          
    //        Manager.FindByCode(_PPId);
    //        if (Manager.Count > 0)
    //        {
    //            Manager[0].BeginEdit();
    //            Manager[0]["IsConfirm"] = 1;
    //            Manager[0]["Status"] = (int)TSP.DataManager.PeriodPresentStatus.EndObjection;
    //            Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //            Manager[0].EndEdit();
    //            if (Manager.Save() > 0)
    //            {
    //                //  lblPStatus.Visible = true;
    //                //lblPPStatus.Visible = true;
    //                btnObjSave.ClientVisible = false;
    //                btnSave.Enabled = false;
    //                btnSave1.Enabled = false;
    //                btnConfirm.Enabled = false;
    //                btnConfirm1.Enabled = false;
    //                txtMeObjDate.Enabled = false;
    //                txtTotalMark.Enabled = false;
    //                txtDate.Enabled = false;
    //                //PanelGradDetail.Enabled = false;
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام شد";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
    //            }
    //        }

    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
    //    }
    //}
    #endregion

    #region Methods

    private void SetKey()
    {
        if (string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
        {
            Response.Redirect("Period.aspx");
        }

        _InsId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["InsId"].ToString()));
        _PPId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PPId"].ToString()));
        _PgMode = Utility.DecryptQS(Request.QueryString["Mode"].ToString());


        if (_PPId == null || string.IsNullOrEmpty(_PgMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        ObjectDataSourcePeriodRegister.SelectParameters["PPId"].DefaultValue = _PPId.ToString();

        TSP.DataManager.InstitueManager InsManager = new TSP.DataManager.InstitueManager();
        InsManager.FindByCode(_InsId);
        if (InsManager.Count == 1)
        {
            lblInsName.Text = InsManager[0]["InsName"].ToString();
            lblManager.Text = InsManager[0]["Manager"].ToString();
        }

        TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        MarkManager.SelectPeriodMarks(_PPId);
        if (MarkManager.Count > 0)
        {
                HDPageMode.Value = "Edit";
                txtDate.Text = MarkManager[0]["ObjectionDate"].ToString();
                txtTotalMark.Text = MarkManager[0]["TotalMark"].ToString();

            }
            else
            HDPageMode.Value = "New";

        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        PPManager.FindByCode(_PPId);
        lblPPTitle.Text = PPManager[0]["PeriodTitle"].ToString();
        lblPriodCode.Text = PPManager[0]["PPCode"].ToString();
        lblPPStatus.Text = PPManager[0]["PeriodStatus"].ToString();
        lblCapacity.Text = PPManager[0]["Capacity"].ToString();
        lblStartDate.Text = PPManager[0]["StartDate"].ToString();
        lblEndDate.Text = PPManager[0]["EndDate"].ToString();
        lblPDuration.Text = PPManager[0]["Duration"].ToString();

        int Status = Convert.ToInt32(PPManager[0]["Status"]);
        if (Status != (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection)
        {
            btnObjSave.ClientVisible = false;
            btnSave.Enabled = false;
            btnSave1.Enabled = false;
            txtMeObjDate.Enabled = false;
            txtTotalMark.Enabled = false;
            txtDate.ReadOnly = true;
            if (MarkManager.Count == 1)
            {
                txtDate.Text = MarkManager[0]["ObjectionDate"].ToString();
                txtTotalMark.Text = MarkManager[0]["TotalMark"].ToString();
            }
            GridViewTestMarks.Columns["DescriptionEdit"].Visible = false;
            GridViewTestMarks.Columns["LastMarkEdit"].Visible = false;
            GridViewTestMarks.Columns["StatusEdit"].Visible = false;
            GridViewTestMarks.Columns["ObjAns"].Visible = true;
            GridViewTestMarks.Columns["Description"].Visible = true;
            GridViewTestMarks.Columns["LastMark"].Visible = true;
            GridViewTestMarks.Columns["Status"].Visible = true;
        }


        ////////switch (_PgMode)
        ////////{
        ////////    case "Mark":

        ////////        break;
        ////////    case "Objection":
        ////////        GridViewTestMarks.Columns["DescriptionEdit"].Visible = false;
        ////////        GridViewTestMarks.Columns["LastMarkEdit"].Visible = false;
        ////////        GridViewTestMarks.Columns["StatusEdit"].Visible = false;
        ////////        GridViewTestMarks.Columns["ObjAns"].Visible = true;
        ////////        GridViewTestMarks.Columns["Description"].Visible = true;
        ////////        GridViewTestMarks.Columns["LastMark"].Visible = true;
        ////////        GridViewTestMarks.Columns["Status"].Visible = true;


        ////////        break;
        ////////}
    }

    protected void Insert(int TeId, int PPId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodTestMarksManager TestMarkManager = new TSP.DataManager.PeriodTestMarksManager();
        //TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        //trans.Add(PPManager);
        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        TestMarkManager.SelectPeriodMarks(_PPId);
        if (TestMarkManager.Count > 0 && TestMarkManager[0]["UltId"].ToString() == ((int)TSP.DataManager.UserType.Teacher).ToString())
        {
            btnSave.Enabled = false;
            btnSave1.Enabled = false;
            //btnConfirm.Enabled = false;
            //btnConfirm1.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما قبلا لیست نمرات را ثبت کرده اید در صورت نیاز به ویرایش دوباره به این صفحه وارد شوید";
            return;
        }
        trans.Add(TestMarkManager);
        try
        {
            trans.BeginSave();
            ArrayList ar = new ArrayList();
            ArrayList ard = new ArrayList();
            ArrayList st = new ArrayList();

            for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
            {
                DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["LastMarkEdit"], "txtMark");
                DevExpress.Web.ASPxMemo desc = (DevExpress.Web.ASPxMemo)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["DescriptionEdit"], "txtDesc");
                DevExpress.Web.ASPxComboBox sts = (DevExpress.Web.ASPxComboBox)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["StatusEdit"], "CmbStatus");

                if (!string.IsNullOrEmpty(txt.Text))
                    ar.Add(txt.Text);
                else
                    ar.Add(null);

                if (!string.IsNullOrEmpty(desc.Text))
                    ard.Add(desc.Text);
                else
                    ard.Add(null);

                if (!string.IsNullOrEmpty(sts.Text))
                    st.Add(sts.Value);
                else
                    st.Add(null);

            }
            for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
            {
                TestMarkManager.DataSet.EnforceConstraints = false;
                DataRow dr = TestMarkManager.NewRow();


                if (ar[i] != null)
                {                   
                    dr["FirstMark"] = dr["LastMark"] = Convert.ToDecimal(ar[i].ToString());
                }
                if (ard[i] != null)
                    dr["Description"] = ard[i].ToString();
                else
                    dr["Description"] = DBNull.Value;

                if (st[i] != null)
                    dr["Status"] = st[i].ToString();
                else
                    dr["Status"] = DBNull.Value;

                dr["TotalMark"] = txtTotalMark.Text;
                dr["PRId"] = GridViewTestMarks.GetDataRow(i)["PRId"].ToString();
                dr["PkId"] = TeId;
                dr["UltId"] = (int)TSP.DataManager.UserType.Teacher;
                dr["ObjectionDate"] = txtDate.Text;
                dr["CreateDate"] = Utility.GetDateOfToday();
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                TestMarkManager.AddRow(dr);

            }

            if (TestMarkManager.Save() > 0)
            {
                //PPManager.FindByCode(PPId);
                //PPManager[0].BeginEdit();
                //PPManager[0]["Status"] = (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection;
                //PPManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                //PPManager[0].EndEdit();
                //PPManager.Save();

                trans.EndSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
                HDPageMode.Value = "Edit";
                GridViewTestMarks.DataBind();
              
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
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

    protected void Edit(int PPId)
    {
            TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(MarkManager);
        try
            {

            //MarkManager.SelectPeriodMarks(PPId);
            //if (MarkManager.Count <= 0)
            //{
            //    SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            //    return;
            //}
            ArrayList ar = new ArrayList();
            ArrayList ard = new ArrayList();
            ArrayList st = new ArrayList();
            trans.BeginSave();
            for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
            {
                DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["LastMarkEdit"], "txtMark");
                DevExpress.Web.ASPxMemo desc = (DevExpress.Web.ASPxMemo)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["DescriptionEdit"], "txtDesc");
                DevExpress.Web.ASPxComboBox sts = (DevExpress.Web.ASPxComboBox)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["StatusEdit"], "CmbStatus");

                if (txt != null && !string.IsNullOrEmpty(txt.Text))
                    ar.Add(txt.Text);
                else
                    ar.Add(null);

                if (desc != null && !string.IsNullOrEmpty(desc.Text))
                    ard.Add(desc.Text);
                else
                    ard.Add(null);
                if (!string.IsNullOrEmpty(sts.Text))
                    st.Add(sts.Value);
                else
                    st.Add(null);

            }
            int PtmId = -2;
            for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
            {
                if (!Utility.IsDBNullOrNullValue( (GridViewTestMarks.GetDataRow(i))["PtmId"]))
                    PtmId = (int)((GridViewTestMarks.GetDataRow(i))["PtmId"]);
                else
                    PtmId = -2;
                MarkManager.FindByCode(PtmId);
                if (MarkManager.Count == 0)
                {
                    DataRow dr = MarkManager.NewRow();

                if (ar[i] != null)
                {
                        dr["FirstMark"] = dr["LastMark"] = Convert.ToDecimal(ar[i].ToString());
                }
                if (ard[i] != null)
                        dr["Description"] = ard[i].ToString();
                else
                        dr["Description"] = DBNull.Value;

                if (st[i] != null)
                        dr["Status"] = st[i].ToString();
                else
                        dr["Status"] = DBNull.Value;

                    dr["TotalMark"] = txtTotalMark.Text;
                    dr["PRId"] = GridViewTestMarks.GetDataRow(i)["PRId"].ToString();
                    dr["PkId"] = Utility.GetCurrentUser_MeId();
                    dr["UltId"] = (int)TSP.DataManager.UserType.Teacher;
                    dr["ObjectionDate"] = txtDate.Text;
                    dr["CreateDate"] = Utility.GetDateOfToday();
                    dr["UserId"] = Utility.GetCurrentUser_UserId();
                    dr["ModifiedDate"] = DateTime.Now;
                    MarkManager.AddRow(dr);
                    MarkManager.Save();
                    MarkManager.DataTable.AcceptChanges();
            }
            else
            {
                    MarkManager[0].BeginEdit();

                    if (ar[i] != null)
                    {
                        MarkManager[0]["FirstMark"] = MarkManager[0]["LastMark"] = Convert.ToDecimal(ar[i].ToString());
            }
                    if (ard[i] != null)
                        MarkManager[0]["Description"] = ard[i].ToString();
                    else
                        MarkManager[0]["Description"] = DBNull.Value;

                    if (st[i] != null)
                        MarkManager[0]["Status"] = st[i].ToString();
                    else
                        MarkManager[0]["Status"] = DBNull.Value;

                    MarkManager[0]["TotalMark"] = txtTotalMark.Text;
                    MarkManager[0]["ObjectionDate"] = txtDate.Text;
                    MarkManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    MarkManager[0]["ModifiedDate"] = DateTime.Now;
                    MarkManager[0].EndEdit();
                    MarkManager.Save();
                    MarkManager.DataTable.AcceptChanges();
                }
            }
            trans.EndSave();
            SetMessage("ذخیره انجام شد");
            HDPageMode.Value = "Edit";
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
                    SetMessage("اطلاعات تکراری می باشد");
                }

                else
                {
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion


}

