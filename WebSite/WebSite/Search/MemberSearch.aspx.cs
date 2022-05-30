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

public partial class Search_MemberSearch : System.Web.UI.Page
{
    public int Envelope_AddressType
    {
        get { return Convert.ToInt32(comboAddress.SelectedItem.Value); }
    }

    public bool Envelope_PageBreak
    {
        get
        {
            int PrintType = Convert.ToInt32(comboPrintType.SelectedItem.Value.ToString());
            if (PrintType == 0)
                return false;
            else
                return true;
        }
    }

    public int Envelope_SId
    {
        get { return Convert.ToInt32(comboSecretariat.SelectedItem.Value.ToString()); }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Utility.GetCurrentUser_AgentId() != Utility.GetMainAgentId())
            {         
                ASPxListBox AgentListBox = (ASPxListBox)drdAgent.FindControl("ListBoxAgent");     
                AgentListBox.Items.FindByValue(Utility.GetCurrentUser_AgentId().ToString()).Selected = true;
                drdAgent.Text = AgentListBox.Items.FindByValue(Utility.GetCurrentUser_AgentId().ToString()).Text;
                drdAgent.ClientEnabled = false;
            }

            ObjectDataSourceSecretariat.SelectParameters["EmpId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            comboSecretariat.DataBind();
            Session["MeIdParametersForEnvPrint"] = null;

            drdMajor.DataBind();
            ((ASPxListBox)(drdMajor.FindControl("ListBoxMajor"))).Items.Insert(0, new ListEditItem("<همه>", null));

            drdLicense.DataBind();
            ((ASPxListBox)(drdLicense.FindControl("ListBoxLicense"))).Items.Insert(0, new ListEditItem("<همه>", null));

            drdDocGrade.DataBind();
            ((ASPxListBox)(drdDocGrade.FindControl("ListBoxDocGrade"))).Items.Insert(0, new ListEditItem("<فاقد پروانه>", 0));
            ((ASPxListBox)(drdDocGrade.FindControl("ListBoxDocGrade"))).Items.Insert(1, new ListEditItem("<همه>", null));

            drdRegistrationStatus.DataBind();
            ((ASPxListBox)(drdRegistrationStatus.FindControl("ListBoxRegistrationStatus"))).Items.Insert(0, new ListEditItem("<همه>", null));
            ((ASPxListBox)(drdRegistrationStatus.FindControl("ListBoxRegistrationStatus"))).Items.FindByValue(((int)TSP.DataManager.MembershipRegistrationStatus.Confirmed).ToString()).Selected = true;
            ((ASPxListBox)(drdRegistrationStatus.FindControl("ListBoxRegistrationStatus"))).Items.FindByValue(((int)TSP.DataManager.MembershipRegistrationStatus.Pending).ToString()).Selected = true;
            drdRegistrationStatus.Text = ((ASPxListBox)(drdRegistrationStatus.FindControl("ListBoxRegistrationStatus"))).Items.FindByValue(((int)TSP.DataManager.MembershipRegistrationStatus.Confirmed).ToString()).Text + "," +
            ((ASPxListBox)(drdRegistrationStatus.FindControl("ListBoxRegistrationStatus"))).Items.FindByValue(((int)TSP.DataManager.MembershipRegistrationStatus.Pending).ToString()).Text;

            drdAgent.DataBind();
            ((ASPxListBox)(drdAgent.FindControl("ListBoxAgent"))).Items.Insert(0, new ListEditItem("<همه>", null));

            drdParentFileMajor.DataBind();
            drdParentFileMajor.Items.Insert(0, new DevExpress.Web.ListEditItem("<همه>", null));
            TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
            ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).DataSource = MajorManager.GetData();
            ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).DataBind();
            ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).Items.Insert(0, new ListEditItem("<همه>", null));

            drdGroup.DataBind();
            drdGroup.Items.Insert(0, new DevExpress.Web.ListEditItem("<همه>", null));
            

            drdCommision.DataBind();
            drdCommision.Items.Insert(0, new DevExpress.Web.ListEditItem("<همه>", null));
        }

        #region Members_SenderComment
        //if (String.IsNullOrEmpty(txtMeId.Text.Trim()) == false && int.Parse(txtMeId.Text.Trim()) > 0)
        //    ObjectDataSource1.SelectParameters["MeId"].DefaultValue = txtMeId.Text.Trim();
        //else if (IsPostBack == false)
        //    ObjectDataSource1.SelectParameters["MeId"].DefaultValue = "-2";
        //else
        //    ObjectDataSource1.SelectParameters["MeId"].DefaultValue = "-1";
        //if (cmbMajor.SelectedIndex > 0)
        //    ObjectDataSource1.SelectParameters["MjId"].DefaultValue = cmbMajor.Value.ToString();
        //ObjectDataSource1.SelectParameters["FirstName"].DefaultValue = txtFName.Text.Trim();
        //ObjectDataSource1.SelectParameters["LastName"].DefaultValue = txtLName.Text.Trim();
        //if (cmbGroup.SelectedIndex > 0)
        //    ObjectDataSource1.SelectParameters["GrId"].DefaultValue = cmbGroup.Value.ToString();
        //grdMembers.DataBind();

        #endregion

        //if (!IsCallback)
        //{
        //    #region Members_Sender
        //Search();
        //    #endregion
        //}

        #region Script
        string script = @"<SCRIPT language='javascript'>
                function CheckFirstDocRegDate()
                {
                var StartDate = document.getElementById('" + txtFirstDocRegDateFrom.ClientID + @"').value;
                var EndDate = document.getElementById('" + txtFirstDocRegDateTo.ClientID + @"').value;
                return CheckDate(StartDate,EndDate);
                }
                function CheckRevivalDocRegDate()
                {
                var StartDate = document.getElementById('" + txtRevivalDocRegDateFrom.ClientID + @"').value;
                var EndDate = document.getElementById('" + txtRevivalDocRegDateTo.ClientID + @"').value;
                return CheckDate(StartDate,EndDate);
                }
                function CheckBirthDate()
                {
                var StartDate = document.getElementById('" + txtBirthDateFrom.ClientID + @"').value;
                var EndDate = document.getElementById('" + txtBirthDateTo.ClientID + @"').value;
                return CheckDate(StartDate,EndDate);
                }
                function CheckCreateDate()
                {
                var StartDate = document.getElementById('" + txtCreateDateFrom.ClientID + @"').value;
                var EndDate = document.getElementById('" + txtCreateDateTo.ClientID + @"').value;
                return CheckDate(StartDate,EndDate);
                }
                function CheckFileDate()
                {
                var StartDate = document.getElementById('" + txtFileDateFrom.ClientID + @"').value;
                var EndDate = document.getElementById('" + txtFileDateTo.ClientID + @"').value;
                return CheckDate(StartDate,EndDate);
                }
                function CheckMembershipDate()
                {
                var StartDate = document.getElementById('" + txtMembershipDateFrom.ClientID + @"').value;
                var EndDate = document.getElementById('" + txtMembershipDateTo.ClientID + @"').value;
                return CheckDate(StartDate,EndDate);
                }
                function CheckDate(StartDate,EndDate)
                {
                  if(EndDate<StartDate && EndDate!='')
                      return -1;
                  else
                      return 1;
                }
                function SetEmpty()
                {
                cmbLicenseInquiryStatus.SetSelectedIndex(-1);
                TextMeIdFrom.SetText('');
                TextMeIdTo.SetText('');
                TextFName.SetText('');
                TextLName.SetText('');
                drdMj.SetText('');
                ListMj.UnselectAll();
                drdLicense.SetText('');
                ListBoxLicense.UnselectAll();
                drdRegistrationStatus.SetText('');
                ListBoxRegistrationStatus.UnselectAll();
                var ItemIndex1=ListBoxRegistrationStatus.FindItemByValue(" + (int)TSP.DataManager.MembershipRegistrationStatus.Pending + @").index;
                var ItemIndex2=ListBoxRegistrationStatus.FindItemByValue(" + (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed + @").index;
                ListBoxRegistrationStatus.SelectIndices([ItemIndex1,ItemIndex2]);
                UpdateText(ListBoxRegistrationStatus,drdRegistrationStatus,0);
                drdFileMjName.SetText('');
                ListBoxFileMjName.UnselectAll();             
                drdParentFileMajor.SetText('');
                drdParentFileMajor.SetSelectedIndex(0);
                //ListBoxParentFileMajor.UnselectAll();
                drdDocGrade.SetText('');
                ListBoxDocGrade.UnselectAll();
                if(drdAgent.GetEnabled())
                {
                    drdAgent.SetText('');
                    ListBoxAgent.UnselectAll();
                }
                drdGr.SetSelectedIndex(0);
         
                drdCom.SetSelectedIndex(0);
               
                document.getElementById('" + txtBirthDateFrom.ClientID + @"').value = '';
                document.getElementById('" + txtBirthDateTo.ClientID + @"').value='';
                document.getElementById('" + txtCreateDateFrom.ClientID + @"').value = '';
                document.getElementById('" + txtCreateDateTo.ClientID + @"').value='';
                document.getElementById('" + txtFileDateFrom.ClientID + @"').value='';
                document.getElementById('" + txtFileDateTo.ClientID + @"').value='';
                document.getElementById('" + txtMembershipDateFrom.ClientID + @"').value = '';
                document.getElementById('" + txtMembershipDateTo.ClientID + @"').value = '';
                document.getElementById('" + txtFirstDocRegDateFrom.ClientID + @"').value = '';
                document.getElementById('" + txtFirstDocRegDateTo.ClientID + @"').value='';
                document.getElementById('" + txtRevivalDocRegDateFrom.ClientID + @"').value = '';
                document.getElementById('" + txtRevivalDocRegDateTo.ClientID + @"').value='';
                }
                function CheckSearch() 
                {
               var   txtBirthDateFrom=   document.getElementById('" + txtBirthDateFrom.ClientID + @"').value;
               var   txtBirthDateTo=    document.getElementById('" + txtBirthDateTo.ClientID + @"').value;
               var  txtCreateDateFrom =   document.getElementById('" + txtCreateDateFrom.ClientID + @"').value;
               var   txtCreateDateTo=  document.getElementById('" + txtCreateDateTo.ClientID + @"').value;
               var  txtFileDateFrom =  document.getElementById('" + txtFileDateFrom.ClientID + @"').value;
               var   txtFileDateTo=    document.getElementById('" + txtFileDateTo.ClientID + @"').value;
               var   txtMembershipDateFrom=    document.getElementById('" + txtMembershipDateFrom.ClientID + @"').value;
               var   txtMembershipDateTo=    document.getElementById('" + txtMembershipDateTo.ClientID + @"').value;
               var   txtFirstDocRegDateFrom=    document.getElementById('" + txtFirstDocRegDateFrom.ClientID + @"').value;
               var  txtFirstDocRegDateTo =    document.getElementById('" + txtFirstDocRegDateTo.ClientID + @"').value;
               var  txtRevivalDocRegDateFrom =    document.getElementById('" + txtRevivalDocRegDateFrom.ClientID + @"').value;
               var   txtRevivalDocRegDateTo=    document.getElementById('" + txtRevivalDocRegDateTo.ClientID + @"').value;
                if ( txtBirthDateFrom=='' && txtBirthDateTo=='' && txtCreateDateFrom=='' && txtCreateDateTo==''&& txtFileDateFrom=='' && txtFileDateTo=='' && txtMembershipDateFrom=='' && txtMembershipDateTo=='' && txtFirstDocRegDateFrom=='' && txtFirstDocRegDateTo=='' && txtRevivalDocRegDateFrom=='' && txtRevivalDocRegDateTo==''&& 
              cmbLicenseInquiryStatus.GetSelectedIndex()==-1 && 
                TextMeIdFrom.GetText()=='' && 
                TextMeIdTo.GetText()=='' && 
                TextFName.GetText()=='' && 
                TextLName.GetText()=='' && 
                drdMj.GetText()=='' && 
                drdLicense.GetText()=='' && 
                drdFileMjName.GetText()=='' &&           
                drdParentFileMajor.GetSelectedIndex()==0 && 
                drdDocGrade.GetText()=='' &&
                (drdAgent.GetEnabled()!=false && drdAgent.GetText()=='')   && 
             drdGr.GetSelectedIndex()==0
) return 0; else return 1;
                }
                </SCRIPT>";

          //
          //     // ListMj.UnselectAll() && 
          //     && 
          //    //  ListBoxLicense.UnselectAll() && 
          //     && 
          //     // ListBoxRegistrationStatus.UnselectAll() && 
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);
        #endregion

       
            TSP.DataManager.Permission reportDocMe = TSP.DataManager.MemberManager.GetUserPermissionForReportDocMemberBySparateMajor(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perMe = TSP.DataManager.MemberManager.GetUserPermissionForSearchMember(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perMeExport = TSP.DataManager.MemberManager.GetUserPermissionForExportExcelMemberSearch(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perMePrint = TSP.DataManager.MemberManager.GetUserPermissionForPrintMemberSearch(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            TSP.DataManager.Permission perMeEnvelopPrint = TSP.DataManager.MemberManager.GetUserPermissionForPrintMeEnvelope(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnExportExcel.Visible = perMeExport.CanView;
            btnEnvelopePrint.Visible = perMeEnvelopPrint.CanView;
            btnPrint.Visible = perMePrint.CanView;
            btnPrintMeDocReport.Visible = reportDocMe.CanView;
      

        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("SelectMember");

        Session["DeletedColumnsName"] = DeletedColumnsName;

        Session["DataTable"] = grdMembers.Columns;
        Session["DataSource"] = ObjectDataSourceGrid;
        Session["Title"] = "ليست اشخاص حقیقی";
    }

    protected void grdMembers_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;

        Int32 start = grid.VisibleStartIndex;
        Int32 end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
        Int32 selectNumbers = 0;
        end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);

        for (int i = start; i < end; i++)
            if (grid.Selection.IsRowSelected(i))
                selectNumbers++;

        e.Properties["cpSelectedRowsOnPage"] = selectNumbers;
        e.Properties["cpVisibleRowCount"] = grid.VisibleRowCount;
    }

    protected void grdMembers_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (String.IsNullOrEmpty(e.Parameters) == false)
        {
            switch (e.Parameters)
            {
                //case "Search":
                //    Search();
                //    break;
                case "Print":
                    #region Print
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    string SelectedMeId = "(-1)";
                    if (!string.IsNullOrEmpty(txtSelectedMeId.Text))
                        SelectedMeId = txtSelectedMeId.Text;
                    DataTable dtSelectedMe = MemberManager.SelectMemberForEnvelopePrint(SelectedMeId);
                    grdMembers.JSProperties["cpDoPrint"] = 1;
                    Session["DataTable"] = grdMembers.Columns;
                    Session["DataSource"] = ObjectDataSourceGrid;
                    Session["Title"] = "ليست اشخاص حقیقی";
                    #endregion
                    break;
                case "EnvelopePrint":
                    #region EnvelopePrint
                    int ReceiverType = (int)TSP.DataManager.AutomationLetterRecieverTypes.Member;
                    int AddressType = Envelope_AddressType;
                    string MeIdParameters = "(0)";
                    if (!string.IsNullOrEmpty(txtSelectedMeId.Text))
                        MeIdParameters = txtSelectedMeId.Text;
                    grdMembers.JSProperties["cpPrint"] = 1;
                    Session["MeIdParametersForEnvPrint"] = MeIdParameters;
                    grdMembers.JSProperties["cpURL"] = "../ReportForms/EnvelopeReport.aspx?FromMeSearch=" + Utility.EncryptQS("1")//MeIdParameters=" + MeIdParameters// Utility.EncryptQS(MeIdParameters)
                        + "&SId=" + Utility.EncryptQS(Envelope_SId.ToString())
                        + "&PageBreak=" + Utility.EncryptQS(Envelope_PageBreak.ToString())
                        + "&AddressType=" + Utility.EncryptQS(AddressType.ToString())
                        + "&ReceiverType=" + Utility.EncryptQS(ReceiverType.ToString());
                    #endregion
                    break;
                case "PrintMeDocReport":
                    #region PrintMeDocReport
                    grdMembers.JSProperties["cpPrint"] = 1;
                    grdMembers.JSProperties["cpURL"] = "../ReportForms/DocumentMemberCountByMajor.aspx";
                    #endregion
                    break;
            }
        }
    }

    protected void grdMembers_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "BirhtDate" || e.Column.FieldName == "MeNo" || e.Column.FieldName == "CreateDate" || e.Column.FieldName == "MembershipDate" || e.Column.FieldName == "FileNo" || e.Column.FieldName == "FileDate" || e.Column.FieldName == "LastLiEndDate" || e.Column.FieldName == "HomeTel" || e.Column.FieldName == "WorkTel")
            e.Editor.Style["direction"] = "ltr";

    }

    protected void grdMembers_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "BirhtDate" || e.DataColumn.FieldName == "MeNo" || e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "MembershipDate" || e.DataColumn.FieldName == "FileNo" || e.DataColumn.FieldName == "FileDate" || e.DataColumn.FieldName == "LastLiEndDate" || e.DataColumn.FieldName == "HomeTel" || e.DataColumn.FieldName == "WorkTel")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void grdMembers_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("MrsId") != null)
        {
            switch (Convert.ToInt32(e.GetValue("MrsId")))
            {
                case (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed:
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    break;
                default:
                    e.Row.ForeColor = System.Drawing.Color.Brown;
                    break;
            }
        }
    }

    protected void CallbackPanelParvane_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parameter = e.Parameter.Split(';');
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        if (parameter[0] == "cmbChange")
        {
            if (drdParentFileMajor.SelectedIndex == 0)
            {
                ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).DataSource = MajorManager.GetData();
                ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).DataBind();
                ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).Items.Insert(0, new ListEditItem("<همه>", null));
                ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).UnselectAll();
                drdFileMjName.Text = "";
                return;
            }
            int ParentId = Convert.ToInt32(parameter[1]);

            DataTable dt = MajorManager.FindMajorAndChilds(ParentId);
            ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).DataSource = dt;
            ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).DataBind();

            // drdFileMjName.DataBind();
            ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).Items.Insert(0, new ListEditItem("<همه>", null));
            ((ASPxListBox)(drdFileMjName.FindControl("ListBoxFileMjName"))).SelectAll();
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Member";
        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void btnSearchMember_Click(object sender, EventArgs e)
    {
        Search();
    }
    #endregion

    #region Methods
    String GetSelectedInDxDropDown(ASPxDropDownEdit DropDown, String ListBoxName)
    {
        string Param = "";
        bool flag = false;

        ASPxListBox ListBox = (ASPxListBox)DropDown.FindControl(ListBoxName);
        if (ListBox == null)
            return "";

        for (int i = 0; i < ListBox.SelectedItems.Count; i++)
        {
            if (ListBox.SelectedItems[i].Value != null)
            {
                if (Param != "")
                    Param += "," + ListBox.SelectedItems[i].Value.ToString();
                else
                    Param += ListBox.SelectedItems[i].Value.ToString();

                flag = true;
            }
        }

        if (flag)
        {
            Param += "";
            return Param;
        }
        return "";
    }

    protected void cbSelectAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
    }

    private void Search()
    {
        if (String.IsNullOrEmpty(txtMeIdFrom.Text.Trim()) == false && int.Parse(txtMeIdFrom.Text.Trim()) > 0)
            ObjectDataSourceGrid.SelectParameters["MeIdFrom"].DefaultValue = txtMeIdFrom.Text.Trim();

        else if (IsPostBack == false)
            ObjectDataSourceGrid.SelectParameters["MeIdFrom"].DefaultValue = "-2";
        else
            ObjectDataSourceGrid.SelectParameters["MeIdFrom"].DefaultValue = "-1";

        if (String.IsNullOrEmpty(txtMeIdTo.Text.Trim()) == false && int.Parse(txtMeIdTo.Text.Trim()) > 0)
            ObjectDataSourceGrid.SelectParameters["MeIdTo"].DefaultValue = txtMeIdTo.Text.Trim();
        else if (IsPostBack == false)
            ObjectDataSourceGrid.SelectParameters["MeIdTo"].DefaultValue = "-2";
        else
            ObjectDataSourceGrid.SelectParameters["MeIdTo"].DefaultValue = "-1";
        ObjectDataSourceGrid.SelectParameters["FirstName"].DefaultValue = txtFName.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["LastName"].DefaultValue = txtLName.Text.Trim();


        string MjParam = GetSelectedInDxDropDown(drdMajor, "ListBoxMajor");
        if (String.IsNullOrWhiteSpace(MjParam) == false)
            ObjectDataSourceGrid.SelectParameters["MjParam"].DefaultValue = MjParam;
        else
            ObjectDataSourceGrid.SelectParameters["MjParam"].DefaultValue = "0";

     if(drdGroup.SelectedIndex>0)
            ObjectDataSourceGrid.SelectParameters["GrParam"].DefaultValue = drdGroup.SelectedItem.Value.ToString();
        else
            ObjectDataSourceGrid.SelectParameters["GrParam"].DefaultValue = "-1";

        string FileMjIdParam = GetSelectedInDxDropDown(drdFileMjName, "ListBoxFileMjName");
        if (String.IsNullOrWhiteSpace(FileMjIdParam) == false)
            ObjectDataSourceGrid.SelectParameters["FileMjIdParam"].DefaultValue = FileMjIdParam;
        else
            ObjectDataSourceGrid.SelectParameters["FileMjIdParam"].DefaultValue = "0";

        string DocGradeParam = GetSelectedInDxDropDown(drdDocGrade, "ListBoxDocGrade");
        if (String.IsNullOrWhiteSpace(DocGradeParam) == false)
            ObjectDataSourceGrid.SelectParameters["DocGradeParam"].DefaultValue = DocGradeParam;
        else
            ObjectDataSourceGrid.SelectParameters["DocGradeParam"].DefaultValue = "-1";

        string LicenseParam = GetSelectedInDxDropDown(drdLicense, "ListBoxLicense");
        if (String.IsNullOrWhiteSpace(LicenseParam) == false)
            ObjectDataSourceGrid.SelectParameters["LicenseParam"].DefaultValue = LicenseParam;
        else
            ObjectDataSourceGrid.SelectParameters["LicenseParam"].DefaultValue = "0";

        string RegistrationStatusParam = GetSelectedInDxDropDown(drdRegistrationStatus, "ListBoxRegistrationStatus");
        if (String.IsNullOrWhiteSpace(RegistrationStatusParam) == false)
            ObjectDataSourceGrid.SelectParameters["RegistrationStatusParam"].DefaultValue = RegistrationStatusParam;
        else
            ObjectDataSourceGrid.SelectParameters["RegistrationStatusParam"].DefaultValue = "0";
      
        if (Utility.GetCurrentUser_AgentId() != Utility.GetMainAgentId())
        {          
            ASPxListBox AgentListBox = (ASPxListBox)drdAgent.FindControl("ListBoxAgent");
            AgentListBox.Items.FindByValue(Utility.GetCurrentUser_AgentId().ToString()).Selected = true;
            drdAgent.Text = AgentListBox.Items.FindByValue(Utility.GetCurrentUser_AgentId().ToString()).Text;
            drdAgent.ClientEnabled = false;
            ObjectDataSourceGrid.SelectParameters["AgentParam"].DefaultValue =  Utility.GetCurrentUser_AgentId().ToString() ;
        }
        else
        {
            string AgentParam = GetSelectedInDxDropDown(drdAgent, "ListBoxAgent");
            if (String.IsNullOrWhiteSpace(AgentParam) == false)
                ObjectDataSourceGrid.SelectParameters["AgentParam"].DefaultValue = AgentParam;
            else
                ObjectDataSourceGrid.SelectParameters["AgentParam"].DefaultValue = "0";
        }


    

        if (cmbLicenseInquiryStatus.SelectedIndex > 0)
            ObjectDataSourceGrid.SelectParameters["LicenseInquiryStatus"].DefaultValue = cmbLicenseInquiryStatus.SelectedItem.Value.ToString();
        else
            ObjectDataSourceGrid.SelectParameters["LicenseInquiryStatus"].DefaultValue = "-1";

        ObjectDataSourceGrid.SelectParameters["BirthDateFrom"].DefaultValue = txtBirthDateFrom.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["BirthDateTo"].DefaultValue = txtBirthDateTo.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["CreateDateFrom"].DefaultValue = txtCreateDateFrom.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["CreateDateTo"].DefaultValue = txtCreateDateTo.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["FileDateFrom"].DefaultValue = txtFileDateFrom.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["FileDateTo"].DefaultValue = txtFileDateTo.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["MembershipDateFrom"].DefaultValue = txtMembershipDateFrom.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["MembershipDateTo"].DefaultValue = txtMembershipDateTo.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["FirstDocRegDateFrom"].DefaultValue = txtFirstDocRegDateFrom.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["FirstDocRegDateTo"].DefaultValue = txtFirstDocRegDateTo.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["RevivalDocRegDateFrom"].DefaultValue = txtRevivalDocRegDateFrom.Text.Trim();
        ObjectDataSourceGrid.SelectParameters["RevivalDocRegDateTo"].DefaultValue = txtRevivalDocRegDateTo.Text.Trim();
        if (drdCommision.SelectedIndex > 0)
            ObjectDataSourceGrid.SelectParameters["ComId"].DefaultValue = drdCommision.SelectedItem.Value.ToString();
        else
            ObjectDataSourceGrid.SelectParameters["ComId"].DefaultValue = "-1";

        grdMembers.DataBind();
    }
    #endregion
}