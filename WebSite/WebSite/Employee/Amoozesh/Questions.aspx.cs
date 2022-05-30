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

public partial class Employee_Amoozesh_Questions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.QuestionSetManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;          
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDisactive.Enabled = per.CanEdit;
            btnDisactive2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;
        }
    }
  
  
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddQuestion.aspx?QuSetId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int ID = -1;
      
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ID = (int)row["QuSetId"];
        }
        if (ID == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.QuestionSetManager QuManager = new TSP.DataManager.QuestionSetManager();
            QuManager.FindByCode(ID);
            if (Convert.ToBoolean(QuManager[0]["InActive"]) == true)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش برای سری سؤالات غیر فعال وجود ندارد";
                return;
            }
            else
                Response.Redirect("AddQuestion.aspx?QuSetId=" + Utility.EncryptQS(ID.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int ID = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ID = (int)row["QuSetId"];
        }
        if (ID == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddQuestion.aspx?QuSetId=" + Utility.EncryptQS(ID.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        int ID = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ID = (int)row["QuSetId"];
        }
        if (ID == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "برای غیر فعال کردن یک سری از سؤالات را انتخاب نمایید";
            return;
        }

        TSP.DataManager.QuestionSetManager SetManager = new TSP.DataManager.QuestionSetManager();
        try
        {
            SetManager.FindByCode(ID);
            if (SetManager.Count > 0)
            {
                if (Convert.ToBoolean(SetManager[0]["InActive"]) == true)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "سری سؤالات انتخاب شده غیر فعال می باشد";
                    return;
                }
                SetManager[0].BeginEdit();
                SetManager[0]["InActive"] = 1;
                SetManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                SetManager[0].EndEdit();
                if (SetManager.Save() > 0)
                {
                    CustomAspxDevGridView1.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
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
                this.LabelWarning.Text = "برای غیر فعال کردن یک سری از سؤالات را انتخاب نمایید";
            }

         
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
        
    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "LetterNo" || e.DataColumn.FieldName == "LetterDate")
            e.Cell.Style["direction"] = "ltr";

    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "LetterNo" || e.Column.FieldName == "LetterDate")
            e.Editor.Style["direction"] = "ltr";
    }

}
