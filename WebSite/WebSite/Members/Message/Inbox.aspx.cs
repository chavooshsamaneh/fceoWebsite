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


public partial class Members_Message_Inbox : System.Web.UI.Page
{
    //public static bool IsSelectAll = false;
    // public static int f=-1;
    //public static int MsgId;
    //public static int SenderId;
    //public static int SenderType;
    //public static bool IsSenderPart;

    #region Events
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        if (Context != null && Context.Request.UserAgent != null)
        {
            // If it is FIREFOX Browser
            if (Context.Request.UserAgent.ToLower().Contains("mozilla"))
            {
                Response.Cache.SetNoStore();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ObjectDataSource1.SelectParameters["ReceiverId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            ObjectDataSource1.SelectParameters["ReceiverType"].DefaultValue = Utility.GetCurrentUser_LoginType().ToString();
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        TSP.DataManager.MessageManager MesManager = new TSP.DataManager.MessageManager();
        int curRow = GridViewMsg.FocusedRowIndex;
        DataRow r = (DataRow)GridViewMsg.GetDataRow(curRow);
        if (r != null)
        {
            // ASPxMemo1.Text=r["MsgBody"].ToString();
            lblMessageContent.Text = r["MsgBody"].ToString();
            //Label2.Height = 197;
            //Label2.Visible = true;
            //Panel1.Height = 200;
            //Panel1.Visible = true;
            Session["MsgId"] = r["MsgId"].ToString();
            MsgId.Value = r["MsgId"].ToString();
            MesManager.FindByMsgId(int.Parse(MsgId.Value));

            try
            {
                MesManager[0].BeginEdit();
                MesManager[0]["IsRead"] = true;
                MesManager[0].EndEdit();
                int cnt = MesManager.Save();

                if (cnt <= 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

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
                        this.LabelWarning.Text = err.Message;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    this.LabelWarning.Text = err.Message;
                }

            }


            // SenderId = int.Parse(LoManager[0]["MeId"].ToString());
            //SenderType = int.Parse(LoManager[0]["UltId"].ToString());
            // IsSenderPart = false;//           
        }

    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        int index = -1;
        if (ASPxHiddenField1.Contains("FocInd"))
            index = int.Parse(ASPxHiddenField1.Get("FocInd").ToString());

        if (index < 0)// GridViewMsg.FocusedRowIndex <0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای پاسخ، ابتدا یک پیام را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("~/Members/Message/Compose.aspx?MsgId=" + Utility.EncryptQS(Session["MsgId"].ToString()));

        }
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {

        int index = -1;
        if (ASPxHiddenField1.Contains("FocInd"))
            index = int.Parse(ASPxHiddenField1.Get("FocInd").ToString());

        if (index < 0)// GridViewMsg.FocusedRowIndex <0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ارسال، ابتدا یک پیام را انتخاب نمائید";
        }
        else
        {
            Response.Redirect("~/Members/Message/Compose.aspx?MsgId=" + int.Parse(Session["MsgId"].ToString()) + "&F=" + Utility.EncryptQS("1"));

        }
    }

    protected void GridViewMsg_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        TSP.DataManager.MessageManager MesManager = new TSP.DataManager.MessageManager();
        TSP.DataManager.LoginManager LoManager = new TSP.DataManager.LoginManager();
        LoManager.FindByCode(Utility.GetCurrentUser_UserId());
        if (Convert.ToBoolean(e.GetValue("IsRead")))
            e.Row.BackColor = System.Drawing.Color.LightGray;

        //MesManager.FindByMsgId(int.Parse(e.GetValue("MsgId").ToString()));

        System.Web.UI.Control cn;
        //MesManager.FindByCode(int.Parse(e.KeyValue.ToString()), int.Parse(LoManager[0]["MeId"].ToString()), int.Parse(LoManager[0]["UltId"].ToString()),false,2);
        // Image img = GridViewMsg.FindRowCellTemplateControl(e.VisibleIndex, null, "IconReply") as Image;
        // if (img != null)
        // {
        //if (MesManager.Count > 0)
        //img.Visible = true;
        //string s = l.Text;
        //if (s.Length >= 26)
        //    l.Text = (s.Substring(0, 26)) + "...";
        ////int Count = MesManager.CountOfReceived(int.Parse(e.KeyValue.ToString()));
        // l.Text = Count.ToString();
        //  } 

        //if (MesManager.Count > 0)
        //{
        //    GridViewMsg.FindControl("IconReply");
        //   //  lbl = (Label)grdEquipment.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)grdEquipment.Columns["GroupName"], "cmbGroups");
        //   DevExpress.Web.ASPxLabel  lbl = (DevExpress.Web.ASPxLabel)GridViewMsg.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewMsg.Columns["reply"], "IconReply");
        //  //  cn=GridViewMsg.FindControl("IconReply");
        //  //  if(cn!=null)
        //   // lbl.Visible = true;

        //   // lbl = (Label)e.Row.Cells[1].Controls[0];
        //   // lbl.Visible = true;
        //}

        // GridViewMsg.FindControl("IconReply").Visible = true;

        //    if (int.Parse(GridViewMsg.GetDataRow(e.VisibleIndex)["MsgTypeId"].ToString()) == 2)
        //        GridViewMsg.FindControl("IconReply").Visible = true;
    }

    protected void GridViewMsg_DataBound(object sender, EventArgs e)
    {
        GridViewMsg.FocusedRowIndex = -1;
        //for (int i = 0; i < GridViewMsg.VisibleRowCount; i++)
        //{ 
        //    if((Convert.ToBoolean(GridViewMsg.Columns["IsRead"]))==true)
        //        e.Row.BackColor = System.Drawing.Color.Blue;                

        //}

    }

    protected void GridViewMsg_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        TSP.DataManager.MessageManager MesManager = new TSP.DataManager.MessageManager();
        TSP.DataManager.LoginManager LoManager = new TSP.DataManager.LoginManager();
        LoManager.FindByCode(Utility.GetCurrentUser_UserId());
        if (Convert.ToBoolean(e.GetValue("IsRead")))
            e.Row.BackColor = System.Drawing.Color.LightGray;

        // MesManager.FindByRefMsgId(int.Parse(e.GetValue("MsgId").ToString()));

        //Image img = GridViewMsg.FindRowCellTemplateControl(e.VisibleIndex, null, "IconReply") as Image;
        //if (img != null)
        //{
        //    img.Visible = false;
        //    //string s = l.Text;
        //    //if (s.Length >= 26)
        //    //    l.Text = (s.Substring(0, 26)) + "...";
        //    ////int Count = MesManager.CountOfReceived(int.Parse(e.KeyValue.ToString()));
        //    // l.Text = Count.ToString();
        //} 

        LinkButton l = GridViewMsg.FindRowCellTemplateControl(e.VisibleIndex, null, "LinkButton1") as LinkButton;
        if (l != null)
        {
            string s = l.Text;
            if (s.Length >= 26)
                l.Text = (s.Substring(0, 26)) + "...";
            //int Count = MesManager.CountOfReceived(int.Parse(e.KeyValue.ToString()));
            // l.Text = Count.ToString();
        }


        Image img = GridViewMsg.FindRowCellTemplateControl(e.VisibleIndex, null, "IconReply") as Image;
        if (img != null)
        {
            MesManager.FindByCode(int.Parse(e.KeyValue.ToString()), int.Parse(LoManager[0]["MeId"].ToString()), int.Parse(LoManager[0]["UltId"].ToString()), false, 2);
            if (MesManager.Count > 0)
                img.Visible = true;
            //string s = l.Text;
            //if (s.Length >= 26)
            //    l.Text = (s.Substring(0, 26)) + "...";
            ////int Count = MesManager.CountOfReceived(int.Parse(e.KeyValue.ToString()));
            // l.Text = Count.ToString();
        }


        //if (MesManager.Count > 0)
        //{
        //    //  lbl = (Label)grdEquipment.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)grdEquipment.Columns["GroupName"], "cmbGroups");
        //   // lbl = (Label)GridViewMsg.FindEditRowCellTemplateControl((DevExpress.Web.GridViewDataColumn)GridViewMsg.Columns["reply"], "IconReply");
        //    //  cn=GridViewMsg.FindControl("IconReply");
        //    //  if(cn!=null)
        //   // lbl.Visible = true;

        //    // lbl = (Label)e.Row.Cells[1].Controls[0];
        //    // lbl.Visible = true;
        //}    



    }

    protected void GridViewMsg_PageIndexChanged(object sender, EventArgs e)
    {
        GridViewMsg.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int curRow = GridViewMsg.FocusedRowIndex;
        DataRow r = (DataRow)GridViewMsg.GetDataRow(curRow);
        if (r != null)
        {
            TSP.DataManager.MessageManager MessageManager = new TSP.DataManager.MessageManager();
            TSP.DataManager.MessageReceiverManager MessageReceiverManager = new TSP.DataManager.MessageReceiverManager();
            TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
            TransactionManager.Add(MessageReceiverManager);
            TransactionManager.Add(MessageManager);
            try
            {
                int MsgrId = (int)r["MsgrId"];
                int MsgId = (int)r["MsgId"];
                Boolean MsgInActive = false;
                TransactionManager.BeginSave();
                MessageManager.FindByMsgId(MsgId);
                if (MessageManager.Count == 1)
                {
                    MsgInActive = Convert.ToBoolean(MessageManager[0]["Inactive"]);
                }

                MessageReceiverManager.FindByCode(-1, -1,-1, MsgId);
                MessageReceiverManager.CurrentFilter = "InActive = 0";
                if (MessageReceiverManager.Count == 1 && MsgInActive == true)
                {
                    MessageReceiverManager.FindByCode(-1, -1,-1, MsgId);
                    if (MessageReceiverManager.Count > 0)
                    {
                        int CountRe = MessageReceiverManager.Count;
                        for (int i = 0; i < CountRe; i++)
                        {
                            MessageReceiverManager[i].Delete();
                        }
                        if (MessageReceiverManager.Save() > 0)
                        {
                            // MessageManager.FindByMsgId(MsgId);
                            if (MessageManager.Count == 1)
                            {
                                MessageManager[0].Delete();
                                if (MessageManager.Save() > 0)
                                {
                                    TransactionManager.EndSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "حذف انجام شد.";
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                            }
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                    }
                }
                else
                {
                    MessageReceiverManager.FindByMsgrId(MsgrId);
                    if (MessageReceiverManager.Count == 1)
                    {
                        MessageReceiverManager[0].BeginEdit();

                        MessageReceiverManager[0]["InActive"] = 1;
                        MessageReceiverManager[0].EndEdit();

                        if (MessageReceiverManager.Save() > 0)
                        {
                            TransactionManager.EndSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "حذف انجام شد.";
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                GridViewMsg.DataBind();
            }
            catch (Exception err)
            {
                TransactionManager.CancelSave();
                SetDeleteError(err);
            }
        }
        //TSP.DataManager.MessageReceiverManager MessRe = new TSP.DataManager.MessageReceiverManager();
        //TSP.DataManager.MessageManager MesManager = new TSP.DataManager.MessageManager();
        //TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();

        //int curRow = GridViewMsg.FocusedRowIndex;
        //DataRow r = (DataRow)GridViewMsg.GetDataRow(curRow);
        //if (r != null)
        //{
        //    MessRe.FindByCode(int.Parse(r["MsgrId"].ToString()), -1, -1);
        //    if (MessRe.Count > 0)
        //    {
        //        MessRe[0].BeginEdit();
        //        MessRe[0]["InActive"] = true;
        //        MessRe[0].EndEdit();
        //        int cn = MessRe.Save();
        //        if (cn > 0)
        //        {
        //            GridViewMsg.DataBind();
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "حذف با موفقیت انجام شد.";
        //        }
        //        else
        //        {
        //            this.DivReport.Visible = true;
        //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        //        }
        //    }
        //}
    }

    protected void GridViewMsg_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        TSP.DataManager.MessageReceiverManager MessRe = new TSP.DataManager.MessageReceiverManager();
        TSP.DataManager.MessageManager MesManager = new TSP.DataManager.MessageManager();
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        // int curRow = ;
        DataRow r = GridViewMsg.GetDataRow(GridViewMsg.FocusedRowIndex);
        if (r != null)
        {
            lblMessageContent.Text = r["MsgBody"].ToString();
            lblMessageContent.Visible = true;
            Panel1.Visible = true;
            Session["MsgId"] = r["MsgId"].ToString();
            MsgId.Value = r["MsgId"].ToString();
            MesManager.FindByMsgId(int.Parse(MsgId.Value));
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            int MeId = (int)(LoginManager[0]["MeId"]);
            //MessRe.FindByCode(int.Parse(Session["Login"].ToString())); ;
            MessRe.FindByCode(int.Parse(r["MsgrId"].ToString()), MeId,Utility.GetCurrentUser_LoginType(), -1);
            if (MessRe.Count > 0)
            {
                try
                {
                    MessRe[0].BeginEdit();
                    MessRe[0]["IsRead"] = true;
                    MessRe[0].EndEdit();
                    int cnt = MessRe.Save();

                    if (cnt <= 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

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
                            this.LabelWarning.Text = err.Message;
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        this.LabelWarning.Text = err.Message;
                    }

                }
            }
        }
    }

    protected void CallbackPanelMsg_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewMsg.FocusedRowIndex > -1)
        {
            int curRow = GridViewMsg.FocusedRowIndex;
            DataRow r = (DataRow)GridViewMsg.GetDataRow(curRow);
            if (r != null)
            {
                int MsgId = (int)r["MsgId"];
                lblMessageContent.Text = r["MsgBody"].ToString();
                //txtSubject.Text = r["MsgSubject"].ToString();
            }
        }
    }

    protected void GridViewMsg_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewMsg_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    private void SetDeleteError(Exception err)
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
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
    }
    #endregion
}
