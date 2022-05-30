<%@ Page Title="مدیریت شماره حساب اعضا" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MembersBankAccNo.aspx.cs" Inherits="Employee_MembersRegister_MembersBankAccNo" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییر شماره حساب"
                                            ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false" OnClick="btnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/new_Disabled.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">

                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){ 
	grid.PerformCallback('Print');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){  }" />

                                            <Image Url="~/Images/icons/ExportExcel.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td style="width: 15%">کد عضویت
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtMeId" ClientInstanceName="txtMeId">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td style="width: 15%">شماره حساب
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtBankAccNo"
                                        ClientInstanceName="txtBankAccNo" MaxLength="20">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%">نام
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtName" ClientInstanceName="txtName">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td style="width: 15%">نام خانوادگی
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFamily" ClientInstanceName="txtFamily"
                                        MaxLength="20">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td align="left" valign="top">
                                    <TSPControls:CustomAspxButton runat="server" Text="جستجو" ID="btnSearch" AutoPostBack="False"
                                        UseSubmitBehavior="False" Width="98px"
                                        ClientInstanceName="btnSearch" OnClick="OnClick_btnSearch">
                                        <ClientSideEvents Click="function(s, e) {
	if(txtFamily.GetText()=='' && txtName.GetText()=='' && txtBankAccNo.GetText()=='' && txtMeId.GetText()=='')
    {
	  alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.');
      e.processOnServer=false;
      return;
	}
    e.processOnServer=true;
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم" ID="ASPxButton1"
                                        AutoPostBack="False" UseSubmitBehavior="False"
                                        OnClick="OnClick_btnSearch">
                                        <ClientSideEvents Click="function(s, e) {
                                                                txtFamily.SetText('');
                                                                 txtName.SetText('');
                                                                 txtBankAccNo.SetText('');
                                                                 txtMeId.SetText('');
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <ul class="HelpUL" style="padding: 0;">
                <li><b>
                تنها قادر به جستجوی اعضای نمایندگی خود می باشید
                  </b>  </li></ul>
            <TSPControls:CustomAspxDevGridView ID="GridViewMember" runat="server" Width="100%"
                DataSourceID="ObjectDataSourceMember" KeyFieldName="MeId" AutoGenerateColumns="False"
                ClientInstanceName="grid" OnCustomCallback="GridViewMember_CustomCallback">
                <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" AllowFocusedRow="True"></SettingsBehavior>
                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Width="150px"
                        Caption="نام">
                        <CellStyle Wrap="true">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Width="150px"
                        Caption="نام خانوادگی">
                        <CellStyle Wrap="true">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="SSN" Width="150px" Caption="کد ملی">
                        <CellStyle Wrap="true" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="BankAccNo" Width="200px"
                        Caption="شماره حساب">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="4" Width="30px" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Templates>
                    <DetailRow>
                        <div align="center">
                            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                                ClientInstanceName="Reqgrid" AutoGenerateColumns="False" KeyFieldName="MReId"
                                Width="100%" OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                                DataSourceID="OdbRequest" OnHtmlDataCellPrepared="CustomAspxDevGridViewRequest_HtmlDataCellPrepared"
                                OnAutoFilterCellEditorInitialize="CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="MReId" Name="MReId" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" Name="MeId" Visible="False"
                                        VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" Name="CreateDate"
                                        VisibleIndex="0">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="Confirm" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" Name="AnswerDate"
                                        VisibleIndex="2">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="TypeName" VisibleIndex="4">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="5">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="6">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </div>
                    </DetailRow>
                </Templates>
                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowFilterRow="True" ShowHorizontalScrollBar="True"></Settings>
                <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" ExportMode="None" />
                <ClientSideEvents EndCallback="function(s, e) {

if(grid.cpDoPrint == 1)
{
	grid.cpDoPrint = 0;
	window.open(&quot;../../Printdt.aspx&quot;);
}

}" />
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییر شماره حساب"
                                            ID="BtnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false" OnClick="btnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/new_Disabled.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">

                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){ 
	grid.PerformCallback('Print');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){  }" />

                                            <Image Url="~/Images/icons/ExportExcel.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMember"
                ExportedRowType="All">
            </dxwgv:ASPxGridViewExporter>
            <asp:ObjectDataSource ID="ObjectDataSourceMember" runat="server" SelectMethod="SelectMemberForBanckAccNo"
                TypeName="TSP.DataManager.MemberManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="%" Name="BankAccNo" Type="String" />
                    <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                    <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="SelectMemberRequestForBankAcc"
                TypeName="TSP.DataManager.MemberManager">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MemberId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نماييد
                        <img alt="" src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
