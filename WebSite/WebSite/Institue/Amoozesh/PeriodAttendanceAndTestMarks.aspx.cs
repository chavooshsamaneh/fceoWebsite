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
public partial class Institue_Amoozesh_PeriodAttendanceAndTestMarks : System.Web.UI.Page
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

    int _TaskCode
    {
        set
        {
            TaskId.Value = value.ToString();
        }
        get
        {
            return Convert.ToInt32(TaskId.Value);
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
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
        && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("Period.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Period.aspx");
        }
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
          Edit(_PPId);
        }
    }

    //in popup control btnSave
    protected void btnObjSave_Click(object sender, EventArgs e)
    {
        //int PRId = -1;
        //TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        //if (string.IsNullOrEmpty(txtPRId.Text))
        //{
          
        //    SetMessage("خطایی در دخیره اطلاعات رخ داده است");
        //    return;
        //}
        //else
        //{
        //    PRId = int.Parse(txtPRId.Text);
        //    MarkManager.FindByPRCode(PRId);
        //    if (MarkManager.Count == 1)
        //    {
        //        MarkManager[0].BeginEdit();
        //        MarkManager[0]["TeId"] = Utility.GetCurrentUser_MeId();
        //        MarkManager[0]["TeObjectionDate"] = Utility.GetDateOfToday();
        //        MarkManager[0]["TeObjectionText"] = txtTeObjText.Text;
        //        MarkManager[0]["LastMark"] = txtLastMark.Text;
        //        MarkManager[0].EndEdit();
        //        if (MarkManager.Save() > 0)
        //        {
        //            GridViewTestMarks.DataBind();
        //           SetMessage("ذخیره انجام شد");
        //        }
        //        else
        //        {
        //            SetMessage("خطایی در دخیره اطلاعات رخ داده است");
        //        }

        //    }
        //    else
        //    {
        //        SetMessage( "خطایی در دخیره اطلاعات رخ داده است");
        //    }
        //}
    }

    protected void GridViewTestMarks_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;
      
        int PRId = int.Parse(e.KeyValue.ToString());
        TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        MarkManager.FindByPRCode(PRId);
        if (MarkManager.Count != 0 && string.IsNullOrEmpty(MarkManager[0]["MeObjectionDate"].ToString()))
        {
            DevExpress.Web.ASPxHyperLink lnk = (DevExpress.Web.ASPxHyperLink)GridViewTestMarks.FindRowCellTemplateControl(e.VisibleIndex, null, "ASPxHyperLink1");
            if (lnk != null)
                lnk.Visible = false;
        }
       

    }


    #endregion

    #region Methods

    private void SetKey()
    {
        if (string.IsNullOrEmpty(Request.QueryString["PPId"]))
        {
            Response.Redirect("Period.aspx");
        }

        _InsId = Utility.GetCurrentUser_MeId();
        _PPId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PPId"].ToString()));
        _TaskCode = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["TCode"].ToString()));


        if (_PPId == null || _TaskCode == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        ObjectDataSourcePeriodRegister.SelectParameters["PPId"].DefaultValue = _PPId.ToString();

        //TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        //MarkManager.SelectPeriodMarks(_PPId);
        //if (MarkManager.Count > 0)
        //{
        //    HDPageMode.Value = "Edit";
        //    txtDate.Text = MarkManager[0]["ObjectionDate"].ToString();
        //    txtTotalMark.Text = MarkManager[0]["TotalMark"].ToString();

        //}

        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        PPManager.FindByCode(_PPId);
        if (PPManager.Count == 1)
        {
            lblPeriodTitle.Text = PPManager[0]["PeriodTitle"].ToString();
            lblPPCode.Text = PPManager[0]["PPCode"].ToString();
            lblCapacity.Text = PPManager[0]["Capacity"].ToString();
            lblStartDate.Text = PPManager[0]["StartDate"].ToString();
            lblEndDate.Text = PPManager[0]["EndDate"].ToString();
            lblStartRegisterDate.Text = PPManager[0]["StartRegisterDate"].ToString();
            lblEndRegisterDate.Text = PPManager[0]["EndRegisterDate"].ToString();
            lblPPStatus.Text = PPManager[0]["PeriodStatus"].ToString();
            lblPDuration.Text = PPManager[0]["Duration"].ToString();

           
            if (_TaskCode != (int)TSP.DataManager.WorkFlowTask.RecordAbsenteeism)
            {
                HDPageMode.Value = "Edit";
                btnObjSave.ClientVisible = false;
                btnSave.Enabled = false;
                btnSave1.Enabled = false;
           
              
                GridViewTestMarks.Columns["TotalTimePresentEdit"].Visible = false;
                GridViewTestMarks.Columns["ObjAns"].Visible = true;
                GridViewTestMarks.Columns["Description"].Visible = true;
                GridViewTestMarks.Columns["LastMark"].Visible = true;
                GridViewTestMarks.Columns["Status"].Visible = true;
                GridViewTestMarks.Columns["TotalTimePresent"].Visible = true;
            }

        }
 
    }

    //protected void Insert(int TeId, int PPId)
    //{
    //    TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();

    //    trans.Add(PeriodRegisterManager);
    //    try
    //    {
    //        trans.BeginSave();
            
    //        ArrayList arTimePresent = new ArrayList();

    //        for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
    //        {
               
    //            DevExpress.Web.ASPxComboBox txtTPer = (DevExpress.Web.ASPxComboBox)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["TotalTimePresentEdit"], "txtTotalTimePresent");
    

    //            if (!string.IsNullOrEmpty(txtTPer.Text))
    //                arTimePresent.Add(txtTPer.Value);
    //            else
    //                arTimePresent.Add(null);
    //                        }

    //        for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
    //        {
    //            // TotalTimePresent insert in tblPeriodRegister
    //            int PRId = -2;
    //            if (!Utility.IsDBNullOrNullValue((GridViewTestMarks.GetDataRow(i))["PRId"]))
    //                PRId = (int)((GridViewTestMarks.GetDataRow(i))["PRId"]);
    //            PeriodRegisterManager.FindByCode(PRId);
    //            PeriodRegisterManager[0].BeginEdit();
    //            if (arTimePresent[i] != null)
    //                PeriodRegisterManager[0]["TotalTimePresent"] = arTimePresent[i].ToString();
    //            else
    //                PeriodRegisterManager[0]["TotalTimePresent"] = DBNull.Value;

    //            PeriodRegisterManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //            PeriodRegisterManager[0]["ModifiedDate"] = DateTime.Now;
    //            PeriodRegisterManager[0].EndEdit();
    //            PeriodRegisterManager.Save();
    //            PeriodRegisterManager.DataTable.AcceptChanges();
           

    //        }

    //        if (PeriodRegisterManager.Save() > 0)
    //        {
    //            trans.EndSave();
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "ذخیره انجام شد";
    //            HDPageMode.Value = "Edit";
    //            GridViewTestMarks.DataBind();
    //                        }
    //        else
    //        {
    //            trans.CancelSave();
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
    //        }

    //    }
    //    catch (Exception err)
    //    {
    //        trans.CancelSave();
    //        Utility.SaveWebsiteError(err);
    //        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //        {
    //            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //            if (se.Number == 2601)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
    //            }

    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //        }
    //    }
    //}

    protected void Edit(int PPId)
    {
       
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
     
        trans.Add(PeriodRegisterManager);
        try
        {
            ArrayList arTimePresent = new ArrayList();

            trans.BeginSave();
            for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
            {
                DevExpress.Web.ASPxTextBox txtTPer = (DevExpress.Web.ASPxTextBox)GridViewTestMarks.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)GridViewTestMarks.Columns["TotalTimePresentEdit"], "txtTotalTimePresent");

                if (!string.IsNullOrEmpty(txtTPer.Text))
                    arTimePresent.Add(txtTPer.Value);
                else
                    arTimePresent.Add(null);

            }
       
            for (int i = 0; i < GridViewTestMarks.VisibleRowCount; i++)
            {
                // TotalTimePresent insert in tblPeriodRegister
                int PRId = -2;
                if (!Utility.IsDBNullOrNullValue((GridViewTestMarks.GetDataRow(i))["PRId"]))
                    PRId = (int)((GridViewTestMarks.GetDataRow(i))["PRId"]);
                PeriodRegisterManager.FindByCode(PRId);
                PeriodRegisterManager[0].BeginEdit();
                if (arTimePresent[i] != null)
                    PeriodRegisterManager[0]["TotalTimePresent"] = arTimePresent[i].ToString();
                else
                    PeriodRegisterManager[0]["TotalTimePresent"] = DBNull.Value;

                PeriodRegisterManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                PeriodRegisterManager[0]["ModifiedDate"] = DateTime.Now;
                PeriodRegisterManager[0].EndEdit();
                PeriodRegisterManager.Save();
                PeriodRegisterManager.DataTable.AcceptChanges();
              
            }
            trans.EndSave();
            SetMessage("ذخیره انجام شد");
            HDPageMode.Value = "Edit";
            GridViewTestMarks.DataBind();
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

    private bool CheckWorkFlowPermission(int PPRId)
    {
        int PermissionEdit = -1;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest);
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.PeriodSaveExamMinute;
        PermissionEdit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, PPRId, TaskCode, Utility.GetCurrentUser_UserId());
        if (PermissionEdit > 0)
            return true;
        else
            return false;
    }
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion

}