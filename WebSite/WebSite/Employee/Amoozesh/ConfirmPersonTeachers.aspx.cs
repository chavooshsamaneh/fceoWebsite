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

public partial class Employee_Amoozesh_ConfirmPerson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.ConfirmedPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            

           ObjdsConfPerson.SelectParameters["TableType"].DefaultValue= ((int)(TSP.DataManager.TableCodes.Teachers)).ToString();


           this.ViewState["BtnView"] = btnView.Enabled;
           this.ViewState["BtnEdit"] = btnEdit.Enabled;
           this.ViewState["BtnDelete"] = btnDelete.Enabled;
           this.ViewState["BtnNew"] = btnNew.Enabled;


       }
       if (this.ViewState["BtnView"] != null)
           this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
       if (this.ViewState["BtnEdit"] != null)
           this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
       if (this.ViewState["BtnDelete"] != null)
           this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
       if (this.ViewState["BtnNew"] != null)
           this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
      
    
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int confPerId=-1;
        if(GridViewMajor.FocusedRowIndex>-1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewConfirmPerson = (TSP.WebControls.CustomAspxDevGridView)GridViewMajor.FindDetailRowTemplateControl(GridViewMajor.FocusedRowIndex, "GridViewConfirmPerson");
            if (GridViewConfirmPerson != null)
            {
                confPerId = GridViewConfirmPerson.FocusedRowIndex;
                if (confPerId > -1)
                {
                    Delete(confPerId);
                    GridViewMajor.DataBind();
                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }       
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }

    protected void GridViewMajor_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }

    #region Methods
    private void NextPage(string Mode)
    {
        int ConfPerId = -1;
        int MjId = -1;   

        if (GridViewMajor.FocusedRowIndex > -1)
        {
            DataRow MajorRow = GridViewMajor.GetDataRow(GridViewMajor.FocusedRowIndex);
            MjId = (int)(MajorRow["MjId"]);
            TSP.WebControls.CustomAspxDevGridView GridViewConfirmPerson = (TSP.WebControls.CustomAspxDevGridView)GridViewMajor.FindDetailRowTemplateControl(GridViewMajor.FocusedRowIndex, "GridViewConfirmPerson");
            if (GridViewConfirmPerson != null)
            {              
               if (GridViewConfirmPerson.FocusedRowIndex > -1)
               {
                   DataRow confPerRow = GridViewConfirmPerson.GetDataRow(GridViewConfirmPerson.FocusedRowIndex);
                   ConfPerId = (int)confPerRow["ConfPerId"];
               }
            }
        }
        if (ConfPerId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                ConfPerId = -1;
                Response.Redirect("AddConfPersonTeachers.aspx?ConfPerId=" + Utility.EncryptQS(ConfPerId.ToString())+"&MjId="+Utility.EncryptQS(MjId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddConfPersonTeachers.aspx?ConfPerId=" + Utility.EncryptQS(ConfPerId.ToString()) + "&MjId=" +Utility.EncryptQS(MjId.ToString()) + "&PageMode=" + Utility.EncryptQS(Mode));
            }
        }
    }

    private void Delete(int ConfPerId)
    {
        TSP.DataManager.ConfirmManager ConfirmManager = new TSP.DataManager.ConfirmManager();
        TSP.DataManager.ConfirmPersonManager ConfirmPersonManager = new TSP.DataManager.ConfirmPersonManager();
        ConfirmPersonManager.FindByCode(ConfPerId);

        if (ConfirmPersonManager.Count > 0)
        {
            int ConfId = int.Parse(ConfirmPersonManager[0]["ConfId"].ToString());
            ConfirmPersonManager[0].Delete();            
            int cn = ConfirmPersonManager.Save();
            if (cn < 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "خطایی در حذف انجام شد.";
                return;
            }
            else
            {
                ConfirmPersonManager.ClearBeforeFill = true;
                ConfirmPersonManager.FindByConfId(ConfId);
                if (ConfirmPersonManager.Count == 0)
                {
                    ConfirmManager.FindByCode(ConfId);
                    if (ConfirmManager.Count > 0)
                    {
                        ConfirmManager[0].Delete();
                        cn = ConfirmManager.Save();
                        if (cn > 0)
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "حذف با موفقیت انجام شد.";
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "خطایی در حذف انجام شد.";
                        }
                    }
                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "حذف با موفقیت انجام شد.";
                }
            }
        }
    }
    #endregion
    protected void GridViewConfirmPerson_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();

    }
}
