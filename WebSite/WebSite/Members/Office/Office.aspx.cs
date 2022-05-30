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

public partial class Members_Office_Office : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int MeId = Utility.GetCurrentUser_MeId();
            if (string.IsNullOrEmpty(MeId.ToString()))
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            else
                ObjectDataSource1.SelectParameters[0].DefaultValue = MeId.ToString();

            ODBMrsId.FilterParameters[0].DefaultValue = "4";

            //TSP.DataManager.MemberManager MeManager = Session["MemberManager"] as TSP.DataManager.MemberManager;
            //if (MeManager == null)
            //{
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            //    return;
            //}
            //if (MeManager.Count > 0)
            //{
            //    lblSex.Text = MeManager[0]["SexName1"].ToString();
            //    lblT.Text = MeManager[0]["TiName"].ToString();
            //    lblOfName.Text = MeManager[0]["MeName"].ToString();
            //}

        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
  
    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["OfficeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();

    }
    protected void CustomAspxDevGridView1_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        CustomAspxDevGridView1.FocusedRowIndex = e.VisibleIndex;
    }
    protected void CustomAspxDevGridView1_FocusedRowChanged(object sender, EventArgs e)
    {
        if (CustomAspxDevGridView1.FocusedRowIndex != -1 && CustomAspxDevGridView1.SettingsDetail.ShowDetailRow)
            CustomAspxDevGridView1.DetailRows.ExpandRow(CustomAspxDevGridView1.FocusedRowIndex);
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OfId = -1;
        int OfReId = -1;
       
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OfId = (int)row["OfId"];

        }
        if (OfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)CustomAspxDevGridView1.FindDetailRowTemplateControl(CustomAspxDevGridView1.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest != null)
            {
                if (GridRequest.VisibleRowCount > 0)
                {
                    int index0 = GridRequest.FocusedRowIndex;
                    if (index0 != -1)
                    {
                        OfReId = int.Parse(GridRequest.GetDataRow(index0)["OfReId"].ToString());
                        //ReType = short.Parse(GridRequest.GetDataRow(index0)["Type"].ToString());
                       // string GrdFlt = GridViewOffice.FilterExpression;
                       
                        Response.Redirect("OfficeShowInfo.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&OfReId=" + Utility.EncryptQS(OfReId.ToString()));
                        //Response.Redirect("MemberRequestShow.aspx?MeId=" + Utility.EncryptQS(MeId.ToString()) + "&MReId=" + Utility.EncryptQS(MReId.ToString()) + "&TP=" + Utility.EncryptQS("1") + "&PageMode=" + Utility.EncryptQS("View"));
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";
                }


            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید";

            }
        }
        //else
        //{
        //    TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        //    ReqManager.FindByOfficeId(OfId, -1, -1);
        //    if (ReqManager.Count > 0)
        //    {
        //        OfReId = ReqManager[0]["OfReId"].ToString();
        //    }
        //    else
        //    {
        //        ReqManager.FindByOfficeId(OfId, -1, -1);
        //        if (ReqManager.Count > 0)
        //        {
        //            OfReId = ReqManager[0]["OfReId"].ToString();
                   
        //        }
        //    }

        //    Response.Redirect("OfficeShowInfo.aspx?OfId=" + Utility.EncryptQS(OfId.ToString()) + "&OfReId=" + Utility.EncryptQS(OfReId));
            
        //}
    }
 
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";
    }

    protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "MFNo" || e.Column.FieldName == "AnswerDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "MFNo" || e.DataColumn.FieldName == "AnswerDate")
            e.Cell.Style["direction"] = "ltr";
    }
}
