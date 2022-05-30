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

public partial class Employee_Management_ContactUsSettings : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            TSP.DataManager.Permission per = TSP.DataManager.PublicMessageGroupsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;
            TreeListMemberChart.ClientVisible = per.CanView;

            //if (per.CanView == false)
            //{
            //    Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.ViewNotAllowed).ToString());
            //}

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["View"] = TreeListMemberChart.ClientVisible;


            Load_PageData();
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["View"] != null)
            TreeListMemberChart.ClientVisible = (bool)this.ViewState["View"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (TreeListMemberChart.GetSelectedNodes().Count == 0)
        {
            ShowMessage("رکوردی انتخاب نشده است");
            return;
        }

        TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PublicMessageGroupsManager PublicMessageGroups = new TSP.DataManager.PublicMessageGroupsManager();

        Transaction.Add(PublicMessageGroups);
        Transaction.BeginSave();
        try
        {
            PublicMessageGroups.Fill();
            DataTable dt = PublicMessageGroups.DataTable.Copy();
            foreach (DevExpress.Web.ASPxTreeList.TreeListNode TreeNode in TreeListMemberChart.GetAllNodes())
            {
                DataRow[] drTemp = dt.Select("NezamChartId=" + TreeNode["NcId"]);
                if (drTemp.Length > 0)
                {
                    if ((Boolean)drTemp[0]["InActive"] == TreeNode.Selected)
                    {
                        // Edit
                        PublicMessageGroups.FindByNezamChartId(int.Parse(TreeNode["NcId"].ToString()));
                        PublicMessageGroups[0].BeginEdit();
                        PublicMessageGroups[0]["InActive"] = !TreeNode.Selected;
                        PublicMessageGroups[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        PublicMessageGroups[0]["ModifiedDate"] = DateTime.Now;
                        PublicMessageGroups[0].EndEdit();
                        if (PublicMessageGroups.Save() > 0)
                            PublicMessageGroups.DataTable.AcceptChanges();
                    }
                }
                else if (TreeNode.Selected == true)
                {
                    // Insert
                    DataRow dr = PublicMessageGroups.NewRow();
                    dr["NezamChartId"] = TreeNode["NcId"].ToString();
                    dr["InActive"] = false;
                    dr["UserId"] = Utility.GetCurrentUser_UserId();
                    dr["ModifiedDate"] = DateTime.Now;
                    PublicMessageGroups.AddRow(dr);
                }
            }
            if (PublicMessageGroups.Save() > 0)
                PublicMessageGroups.DataTable.AcceptChanges();
            Transaction.EndSave();
            ShowMessage("ذخیره انجام شد");
        }
        catch (Exception err)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
            Transaction.CancelSave();
            Utility.SaveWebsiteError(err);
        }
    }
    #endregion

    #region Methods
    void Load_PageData()
    {
        TSP.DataManager.PublicMessageGroupsManager PublicMessageGroups = new TSP.DataManager.PublicMessageGroupsManager();
        PublicMessageGroups.Fill();
        PublicMessageGroups.CurrentFilter = "InActive=0";
        TreeListMemberChart.DataBind();
        for (int i = 0; i < PublicMessageGroups.Count; i++)
        {
            TreeListMemberChart.FindNodeByKeyValue(PublicMessageGroups[i]["NezamChartId"].ToString()).Selected = !(Boolean)PublicMessageGroups[i]["InActive"];
        }
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
