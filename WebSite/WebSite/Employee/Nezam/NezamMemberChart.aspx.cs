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
using DevExpress.Web.ASPxTreeList;

public partial class Employee_Nezam_NezamChartMember : System.Web.UI.Page
{
    public bool found = false;
    public int CurrentNcId;
    public int CurrentNmcId;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Attributes.Add("Style", "display:block");
        this.DivReport.Attributes.Add("Style", "display:none");
        // Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.NezamMemberChartManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {

    }

    protected void TreeListNmChart_NodeExpanding(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeCancelEventArgs e)
    {
        TreeListNmChart.DataBind();
    }

    protected void TreeListNmChart_NodeExpanded(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeEventArgs e)
    {
        TreeListNmChart.DataBind();

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnNewMemberChart_Click(object sender, EventArgs e)
    {
        NextPage("New");
        // Response.Redirect("AddNezamMemberChart.aspx");
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "New")
        {
            int ParentId = -1;
            if (TreeListNmChart.FocusedNode != null)
            {
                ParentId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
            }
            InsertNezamChart(ParentId);
        }
        else if (e.Parameter == "View")
        {
            int NcId = -1;
            if (TreeListNmChart.FocusedNode != null)
            {
                NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
                TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                NezamChartManager.SelectById(NcId);
                if (NezamChartManager.Count > 0)
                {
                    txtNcName.Text = NezamChartManager[0]["NcName"].ToString();
                }
                else
                {
                    //PanelSaveSuccessfully.Visible = true;
                    //PanelMain.Visible = false;
                    TreeListNmChart.JSProperties["cpMsg"] = true;
                    lblChartWarning.ForeColor = System.Drawing.Color.Red;
                    lblChartWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
        else if (e.Parameter == "EditChart")
        {
            int NcId = -1;
            if (TreeListNmChart.FocusedNode != null)
            {
                NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
            }
            EditNezamChart(NcId);
        }
        TreeListNmChart.DataBind();
    }

    protected void TreeListNmChart_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
    {
        cmbParents.DataBind();
        if (e.Argument == "New")
        {
            int ParentId = -1;
            if (TreeListNmChart.FocusedNode != null)
            {
                ParentId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
            }
            InsertNezamChart(ParentId);
            TreeListNmChart.DataBind();
            if (TreeListNmChart.Nodes.Count != 0)
                TreeListNmChart.Nodes[0].Focus();
        }
        else if (e.Argument == "View")
        {
            int NcId = -1;
            if (TreeListNmChart.FocusedNode != null)
            {
                NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
                TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                NezamChartManager.SelectById(NcId);
                if (NezamChartManager.Count > 0)
                {
                    txtNcName.Text = NezamChartManager[0]["NcName"].ToString();
                    TreeListNmChart.JSProperties["cptxtNcName"] = NezamChartManager[0]["NcName"].ToString();
                    cmbParents.DataBind();
                    if (Utility.IsDBNullOrNullValue(NezamChartManager[0]["ParentId"]))
                        cmbParents.SelectedIndex = -1;
                    else
                    {
                        cmbParents.SelectedIndex = cmbParents.Items.FindByValue(NezamChartManager[0]["ParentId"].ToString()).Index;

                    }
                    TreeListNmChart.JSProperties["cpcmbParent"] = cmbParents.SelectedIndex;
                }
                else
                {
                    //PanelSaveSuccessfully.Visible = true;
                    //PanelMain.Visible = false
                    TreeListNmChart.JSProperties["cpMsg"] = true;
                    lblChartWarning.ForeColor = System.Drawing.Color.Red;
                    TreeListNmChart.JSProperties["cpMsgContent"] = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    // lblChartWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
        else if (e.Argument == "EditChart")
        {
            int NcId = -1;
            if (TreeListNmChart.FocusedNode != null)
            {
                NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
                txtName.Text = ((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcName"].ToString();
            }
            TSP.DataManager.NezamChartManager NezamChartManager1 = new TSP.DataManager.NezamChartManager();
            //NezamChartManager1.Fill();
            //NezamChartManager1.CurrentFilter = "NcName='" + txtNcName.Text.Trim() + "'";
            NezamChartManager1.FindByNcName(txtNcName.Text.Trim());
            if (NezamChartManager1.Count == 1 && txtName.Text != txtNcName.Text)
            {
                TreeListNmChart.JSProperties["cpMsg"] = true;
                lblChartWarning.ForeColor = System.Drawing.Color.Red;
                TreeListNmChart.JSProperties["cpMsgContent"] = "اطلاعات تکراری می باشد.";
            }
            else
            {
                EditNezamChart(NcId);
            }
            TreeListNmChart.DataBind();
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int NmcId = -1;
        int NcId = -1;
        NmcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NmcId"];
        NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
        if (txtPgMd.Text == "InActiveNc")
        {
            InActiveNezamChart(NcId);
        }
        else if (txtPgMd.Text == "DeleteNmc")
        {
            //  DeleteNezamMemberChart(NmcId);
            InActiveNezamMemberChart(NmcId);
        }
        if (TreeListNmChart.Nodes.Count != 0)
            TreeListNmChart.Nodes[0].Focus();
    }

    protected void btnDeleteNC_Click(object sender, EventArgs e)
    {
        int NmcId = -1;
        int NcId = -1;
        NmcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NmcId"];
        NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
        if (txtPgMd.Text == "DeleteNc")
        {
            DeleteNezamChart(NcId);
        }
        if (TreeListNmChart.Nodes.Count != 0)
            TreeListNmChart.Nodes[0].Focus();
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int NmcId = -1;
        int NcId = -1;
        NmcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NmcId"];
        NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
        if (txtPgMd.Text == "ActiveNmc")
        {
            ActiveNezamMemberChart(NmcId, NcId);

        }
        else if (txtPgMd.Text == "ActiveNc")
        {
            ActiveNezamChartChart(NcId);

        }
        if (TreeListNmChart.Nodes.Count != 0)
            TreeListNmChart.Nodes[0].Focus();
    }

    protected void TreeListNmChart_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        if (txtPgMd.Text == "New")
        {
            lblParent.ClientVisible = false;//Convert.ToBoolean(TreeListNmChart.JSProperties["cpcmbVisible"]);
            cmbParents.ClientVisible = false;// Convert.ToBoolean(TreeListNmChart.JSProperties["cpcmbVisible"]);        
        }
        else if (txtPgMd.Text == "Edit")
        {
            if (Utility.IsDBNullOrNullValue(e.GetValue("ParentId")))
            {
                lblParent.ClientVisible = false;
                cmbParents.ClientVisible = false;
            }
            lblParent.ClientVisible = true;//Convert.ToBoolean(TreeListNmChart.JSProperties["cpcmbVisible"]);
            cmbParents.ClientVisible = true;//Convert.ToBoolean(TreeListNmChart.JSProperties["cpcmbVisible"]);
        }
        if (e.GetValue("FullName") != null)
        {
            if (string.IsNullOrEmpty(e.GetValue("FullName").ToString()))
            {
                e.Row.Font.Bold = true;
                if (!string.IsNullOrEmpty(e.GetValue("InActive").ToString()) && Convert.ToInt16( e.GetValue("InActive"))==1)
                {
                    e.Row.ForeColor = System.Drawing.Color.Orange;
                }
               
            }
            else
            {
                if (e.GetValue("IsMaster") != null)
                {
                    if (Convert.ToBoolean(e.GetValue("IsMaster")))
                    {
                        e.Row.BackColor = System.Drawing.Color.Wheat;
                    }
                }
                if (e.GetValue("IsMasterPosition") != null)
                {
                    if (Convert.ToBoolean(e.GetValue("IsMasterPosition")))
                    {
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                if (e.GetValue("InActive") != null)
                {
                    if (Convert.ToBoolean(e.GetValue("InActive")))
                    {

                        e.Row.ForeColor = System.Drawing.Color.LightSlateGray;
                    }
                }
            }
        }
    }
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int NmcId = -1;
        int NcId = -1;
        int InActive = -1;
        NcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NcId"];
        // InActive = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["InActive"];
        if (Mode != "New")
            NmcId = (int)((System.Data.DataRowView)(TreeListNmChart.FocusedNode.DataItem)).Row["NmcId"];
        if (NmcId == -1 && Mode != "New")
        {
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                if (FindNmcCount(NcId) > 0)
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "به هر پست سازماني تنها يك فرد را مي توان انتساب داد.";
                    return;
                }
                NmcId = -1;
                Response.Redirect("AddNezamMemberChart.aspx?NmcId=" + Utility.EncryptQS(NmcId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&NcId=" + Utility.EncryptQS(NcId.ToString()));
            }
            else
            {
                TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
                NezamMemberChartManager.FindByNmcId(NmcId);
                if (NezamMemberChartManager.Count == 1 && Mode == "Edit")
                {
                    if (Convert.ToBoolean(NezamMemberChartManager[0]["InActive"]))
                    {
                        this.DivReport.Attributes.Add("Style", "display:block");
                        this.LabelWarning.Text = "امكان ويريش اطلاعات عضو غير فعل شده وجود ندارد.";
                        return;
                    }
                }
                else if (NezamMemberChartManager.Count != 1)
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطايي در بازيابي اطلاعات صورت گرفته است.";
                    return;
                }
                Response.Redirect("AddNezamMemberChart.aspx?NmcId=" + Utility.EncryptQS(NmcId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode) + "&NcId=" + Utility.EncryptQS(NcId.ToString()));
            }
        }
    }

    private void InsertNezamChart(int ParentId)
    {
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.NezamChartManager NezamChartManager1 = new TSP.DataManager.NezamChartManager();
        try
        {
            // NezamChartManager1.Fill();
            //NezamChartManager1.CurrentFilter = "NcName='" + txtNcName.Text.Trim() + "'";
            NezamChartManager1.FindByNcName(txtNcName.Text.Trim());
            if (NezamChartManager1.Count == 1)
            {
                TreeListNmChart.JSProperties["cpMsg"] = true;
                lblChartWarning.ForeColor = System.Drawing.Color.Red;
                TreeListNmChart.JSProperties["cpMsgContent"] = "اطلاعات تکراری می باشد.";
                return;
            }
            DataRow NcRow = NezamChartManager.NewRow();
            NcRow["NcName"] = txtNcName.Text.Trim();
            if (ParentId > 0)
                NcRow["ParentId"] = ParentId;
            else
                NcRow["ParentId"] = DBNull.Value;
            NcRow["IsExternal"] = 0;
            NcRow["IsDepartment"] = 0;
            NcRow["UserId"] = Utility.GetCurrentUser_UserId();
            NcRow["ModifiedDate"] = DateTime.Now;

            NezamChartManager.AddRow(NcRow);
            int cn = NezamChartManager.Save();
            if (cn > 0)
            {
                lblChartWarning.ForeColor = System.Drawing.Color.Green;
                TreeListNmChart.JSProperties["cpMsgContent"] = "ذخیره انجام شد.";
                TreeListNmChart.JSProperties["cpMsg"] = true;
                //  PanelMain.ClientVisible = false;
                //  PanelSaveSuccessfully.ClientVisible = true;         
            }
            else
            {
                //PanelSaveSuccessfully.Visible = true;
                // PanelMain.Visible = false;
                TreeListNmChart.JSProperties["cpMsg"] = true;
                lblChartWarning.ForeColor = System.Drawing.Color.Red;
                TreeListNmChart.JSProperties["cpMsgContent"] = "خطایی در ذخیره انجام گرفت.";
            }

        }
        catch (Exception err)
        {
            TreeListNmChart.JSProperties["cpMsg"] = true;
            lblChartWarning.ForeColor = System.Drawing.Color.Red;
            TreeListNmChart.JSProperties["cpMsgContent"] = "خطایی در ذخیره انجام گرفت.";
            Utility.SaveWebsiteError(err);
        }
    }

    private void EditNezamChart(int NcId)
    {

        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        try
        {

            // else
            {
                NezamChartManager.SelectById(NcId);
                if (NezamChartManager.Count == 1)
                {
                    NezamChartManager[0].BeginEdit();
                    NezamChartManager[0]["NcName"] = txtNcName.Text;
                    cmbParents.DataBind();
                    NezamChartManager[0]["ParentId"] = cmbParents.SelectedItem.Value.ToString();
                    NezamChartManager[0].EndEdit();
                    int cn = NezamChartManager.Save();
                    if (cn > 0)
                    {
                        lblChartWarning.ForeColor = System.Drawing.Color.Green;
                        TreeListNmChart.JSProperties["cpMsgContent"] = "ذخیره انجام شد.";
                        TreeListNmChart.JSProperties["cpMsg"] = true;
                        //  PanelMain.ClientVisible = false;
                        //  PanelSaveSuccessfully.ClientVisible = true;
                    }
                    else
                    {
                        //PanelSaveSuccessfully.Visible = true;
                        //PanelMain.Visible = false;
                        TreeListNmChart.JSProperties["cpMsg"] = true;
                        lblChartWarning.ForeColor = System.Drawing.Color.Red;
                        TreeListNmChart.JSProperties["cpMsgContent"] = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    //PanelSaveSuccessfully.Visible = true;
                    //PanelMain.Visible = false;
                    TreeListNmChart.JSProperties["cpMsg"] = true;
                    lblChartWarning.ForeColor = System.Drawing.Color.Red;
                    TreeListNmChart.JSProperties["cpMsgContent"] = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                }
            }
        }
        catch (Exception err)
        {
            TreeListNmChart.JSProperties["cpMsg"] = true;
            lblChartWarning.ForeColor = System.Drawing.Color.Red;
            TreeListNmChart.JSProperties["cpMsgContent"] = "خطایی در ذخیره انجام گرفته است.";
        }

    }

    private void DeleteNezamChart(int NcId)
    {
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        try
        {
            NezamChartManager.SelectById(NcId);
            if (NezamChartManager.Count > 0)
            {
                NezamChartManager[0].Delete();
                int cn = NezamChartManager.Save();
                if (cn > 0)
                {
                    TreeListNmChart.DataBind();
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "حذف انجام شد.";
                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }

            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    private void InActiveNezamChart(int NcId)
    {
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        try
        {
            NezamMemberChartManager.FindByNcId(NcId, 0);
            if (NezamMemberChartManager.Count > 0)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "این سمت به فرد انتساب داده شده.ابتدا آن را غیرفعال نمایید.";
                return;
            }
            DataTable dtChild= NezamChartManager.SelectAllPosition(0, NcId);
            if (dtChild.Rows.Count > 0)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "این سمت دارای زیر شاخه های فعال می باشد.ابتدا آنها راغیرفعال نمایید.";
                return;
            }
            NezamChartManager.SelectById(NcId);
            if (NezamChartManager.Count > 0)
            {
                NezamChartManager[0].BeginEdit();
                NezamChartManager[0]["InActive"] = 1;
                NezamChartManager[0].EndEdit();
                if (NezamChartManager.Save() > 0)
                {
                    TreeListNmChart.DataBind();
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
    }

    private void ActiveNezamChartChart(int NcId)
    {
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();        
        try
        {
            NezamChartManager.SelectById(NcId);
            if (NezamChartManager.Count > 0)
            {
                NezamChartManager[0].BeginEdit();
                NezamChartManager[0]["InActive"] = 0;
                NezamChartManager[0].EndEdit();
                if (NezamChartManager.Save() > 0)
                {
                    TreeListNmChart.DataBind();
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
    }

    private void DeleteNezamMemberChart(int NmcId)
    {
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        try
        {
            NezamMemberChartManager.FindByNmcId(NmcId);
            if (NezamMemberChartManager.Count > 0)
            {
                NezamMemberChartManager[0].Delete();
                int cn = NezamMemberChartManager.Save();
                if (cn > 0)
                {
                    TreeListNmChart.DataBind();
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "حذف انجام شد.";

                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
                }

            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    private void InActiveNezamMemberChart(int NmcId)
    {
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        try
        {
            NezamMemberChartManager.FindByNmcId(NmcId);
            if (NezamMemberChartManager.Count == 1)
            {
                int NcId = int.Parse(NezamMemberChartManager[0]["NcId"].ToString());
                TaskDoerManager.FindByNcId(NcId);
                if (TaskDoerManager.Count > 0)
                {
                    string WFTaskResponsiblity = FindWFTaskOfPerson(TaskDoerManager.DataTable);
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "کارمند انتخاب شده مسئولیت انجام عملیات: " + WFTaskResponsiblity + "می باشد.";
                    return;
                }
                NezamMemberChartManager[0].BeginEdit();
                NezamMemberChartManager[0]["InActive"] = 1;
                NezamMemberChartManager[0]["EndDate"] = Utility.GetDateOfToday();
                NezamMemberChartManager[0].EndEdit();
                if (NezamMemberChartManager.Save() > 0)
                {
                    TreeListNmChart.DataBind();
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }


            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void ActiveNezamMemberChart(int NmcId, int NcId)
    {
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        try
        {

            if (FindNmcCount(NcId) > 0)
            {
                SetMessage("به هر پست سازماني تنها يك فرد را مي توان انتساب داد.ابتدا فرد دیگر را غیر فعال و مجددا جهت فعال سازی تلاش نمایید.");
                return;
            }
            NezamMemberChartManager.FindByNmcId(NmcId);
            if (NezamMemberChartManager.Count == 1)
            {
                //  int NcId = int.Parse(NezamMemberChartManager[0]["NcId"].ToString());
                //TaskDoerManager.FindByNcId(NcId);
                //if (TaskDoerManager.Count > 0)
                //{
                //    string WFTaskResponsiblity = FindWFTaskOfPerson(TaskDoerManager.DataTable);
                //    this.DivReport.Attributes.Add("Style", "display:block");
                //    this.LabelWarning.Text = "کارمند انتخاب شده مسئولیت انجام عملیات: " + WFTaskResponsiblity + "می باشد.";
                //    return;
                //}
                NezamMemberChartManager[0].BeginEdit();
                NezamMemberChartManager[0]["InActive"] = 0;
                NezamMemberChartManager[0]["EndDate"] = Utility.GetDateOfToday();
                NezamMemberChartManager[0].EndEdit();
                if (NezamMemberChartManager.Save() > 0)
                {
                    TreeListNmChart.DataBind();
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }


            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    private void SetDeleteError(Exception err)
    {

        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 547)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Attributes.Add("Style", "display:block");
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
        }
    }

    private string FindWFTaskOfPerson(DataTable dtTaskDoer)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        string WFTask = "";
        for (int i = 0; i < dtTaskDoer.Rows.Count; i++)
        {
            WorkFlowTaskManager.FindByCode(int.Parse(dtTaskDoer.Rows[i]["TaskId"].ToString()));
            if (WorkFlowTaskManager.Count == 1)
            {
                WFTask += WorkFlowTaskManager[0]["WorkFlowName"].ToString() + ":" + WorkFlowTaskManager[0]["TaskName"].ToString() + " " + ";";
            }
        }
        WFTask = WFTask.Remove(WFTask.Length - 1);
        return WFTask;
    }

    private int FindNmcCount(int NcId)
    {
        int NmcCount = 0;
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        //NezamMemberChartManager.Fill();
        NezamMemberChartManager.FindByNcId(NcId, 0);
        //  NezamMemberChartManager.CurrentFilter = "NcId=" + NcId.ToString() + " and " + "InActive=" + "0";
        NmcCount = NezamMemberChartManager.Count;
        return NmcCount;
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Message;
    }
    #endregion
}
