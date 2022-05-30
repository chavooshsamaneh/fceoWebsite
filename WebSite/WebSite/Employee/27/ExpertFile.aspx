<%@ Page Title="مدیریت کارشناسان ماده 27" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ExpertFile.aspx.cs" Inherits="Employee_27_ExpertFile" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="true" OnClick="btnReqEdit_Click">
                                          
                                            <Image  Url="~/Images/icons/edit.png"  />
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            ID="btnReqView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="true" OnClick="btnReqView_Click">
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                            ID="btnReqNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="true" OnClick="btnReqNew_Click">
                                            
                                            <Image  Url="~/Images/icons/new_Disabled.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInvalid2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="درخواست ابطال"
                                            UseSubmitBehavior="False" OnClick="btnInvalid_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	e.processOnServer=confirm('آیا مطمئن به باطل کردن این مجوز هستید؟');
}" />
                                            
                                            <Image  Url="~/Images/icons/button_cancel.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator ID="MenuSeprator11" runat="server"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep2" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {                                                          
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 
 {
 
 ShowWf();
 }
}" />
                                           
                                            <Image  Url="~/Images/icons/reload.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" Text=" " AutoPostBack="true" ToolTip="پیگیری" UseSubmitBehavior="False" OnClick="btnTracing_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                           
                                            <Image  Url="~/Images/icons/Cheque Status ReChange.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                           
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){ 
	GridViewExpertFile.PerformCallback('Print');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){  }" />
                                           
                                            <Image  Url="~/Images/icons/ExportExcel.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="انتخاب ستون ها"
                                            ID="ChooseColumn2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="False" Visible="true">
                                            <ClientSideEvents Click="function(s, e) {
	if(!GridViewExpertFile.IsCustomizationWindowVisible())
		GridViewExpertFile.ShowCustomizationWindow();
	else
		GridViewExpertFile.HideCustomizationWindow();
}" />
                                           
                                            <Image  Url="~/Images/icons/cursor-hand.png"  />
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
                <ContentPaddings PaddingBottom="10px" PaddingLeft="4px" PaddingTop="10px" />
                <HeaderStyle Height="23px" HorizontalAlign="Right">
                <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
                </HeaderStyle>
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td style="width: 15%">مرحله
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomAspxComboBox ID="CmbTask" runat="server" 
                                          ValueType="System.String"
                                        TextField="TaskName" ValueField="TaskId" RightToLeft="True" ClientInstanceName="CmbTask"
                                        DataSourceID="ObjdsWorkFlowTask" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td style="width: 15%">کد عضویت
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtMeId" ClientInstanceName="txtMeId">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 15%">نام
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtName" ClientInstanceName="txtName" Theme="Mulberry">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td style="width: 15%">نام خانوادگی
                                </td>
                                <td style="width: 35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtFamily" ClientInstanceName="txtFamily"
                                        MaxLength="20">
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>

                                <td align="left" valign="top" colspan="2">
                                    <TSPControls:CustomAspxButton  runat="server" Text="جستجو"  ID="btnSearch" AutoPostBack="False"
                                        UseSubmitBehavior="False"  Width="98px"
                                        ClientInstanceName="btnSearch" OnClick="btnSearch_Click" CausesValidation="False" ToolTip="جستجو">
                                        <ClientSideEvents Click="function(s, e) {
	if(txtFamily.GetText()=='' && txtName.GetText()=='' && txtMeId.GetText()=='' &&   CmbTask.GetSelectedIndex()==-1)
    {
	  alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.');
      e.processOnServer=false;
      return;
	}
    e.processOnServer=true;
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top" colspan="2">
                                    <TSPControls:CustomAspxButton  runat="server" Text="پاک کردن فرم"  ID="ASPxButton1"
                                        AutoPostBack="False" UseSubmitBehavior="False" 
                                         OnClick="btnSearch_Click" CausesValidation="False" ToolTip="پاک کردن فرم">
                                        <ClientSideEvents Click="function(s, e) {
                                                                txtFamily.SetText('');
                                                                 txtName.SetText('');
                                                                 txtMeId.SetText('');
                                                                 CmbTask.SetSelectedIndex(-1);
                                          //  alert( CmbTask.GetSelectedIndex());
}" />
                                        
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewExpertFile" runat="server" Width="100%"
                DataSourceID="ObjectDataSourceexpertFile" KeyFieldName="EfId" 
                AutoGenerateColumns="False" ClientInstanceName="GridViewExpertFile" OnCustomCallback="GridViewExpertFile_CustomCallback" Theme="Metropolis">
                <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" AllowFocusedRow="True"></SettingsBehavior>
                <SettingsCustomizationWindow Enabled="True" />
                <Columns>
                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                        VisibleIndex="0" Width="40px">
                        <DataItemTemplate>
                            <div align="center">
                                <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                </dxe:ASPxImage>
                            </div>
                        </DataItemTemplate>
                        <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                            ValueType="System.String">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
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
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FileNo" Width="150px" Caption="شماره پروانه">
                        <CellStyle Wrap="true" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="ImageUrl" Visible="False"
                        VisibleIndex="0">
                        <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                        </PropertiesImage>
                    </dxwgv:GridViewDataImageColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="4" Width="30px" Caption=" " ShowClearFilterButton="true">
              
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Templates>
                    <DetailRow>
                        <div align="center">
                            <TSPControls:CustomAspxDevGridView ID="GridViewExpertFileRequest" runat="server"
                                ClientInstanceName="Reqgrid" AutoGenerateColumns="False" 
                               KeyFieldName="EfReqId" Width="100%" OnBeforePerformDataSelect="GridViewExpertFileRequest_BeforePerformDataSelect"
                                DataSourceID="OdbRequest">
                                <Columns>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                        VisibleIndex="0" Width="40px">
                                        <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                            ValueType="System.String">
                                        </PropertiesComboBox>
                                        <DataItemTemplate>
                                            <div align="center">
                                                <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                                </dxe:ASPxImage>
                                            </div>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" Name="CreateDate"
                                        VisibleIndex="0">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeName" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>

                                    <dxwgv:GridViewDataTextColumn Caption="وضعیت تأیید" FieldName="ConfirmName" VisibleIndex="1">
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
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="true" OnClick="btnReqEdit_Click">
                                            <Image  Url="~/Images/icons/edit.png"  />
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            ID="btnReqView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="true" OnClick="btnReqView_Click">
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                            ID="btnReqNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="true" OnClick="btnReqNew_Click">
                                            <Image  Url="~/Images/icons/new_Disabled.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInvalid" runat="server"  EnableTheming="False"
                                            EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="درخواست ابطال"
                                            UseSubmitBehavior="False" OnClick="btnInvalid_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	e.processOnServer=confirm('آیا مطمئن به باطل کردن این مجوز هستید؟');
}" />
                                            <Image  Url="~/Images/icons/button_cancel.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator ID="MenuSeprator3" runat="server"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {                                                          
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 
 {
 
 ShowWf();
 }
}" />
                                            <Image  Url="~/Images/icons/reload.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server"  EnableTheming="False"
                                            EnableViewState="False" Text=" " AutoPostBack="true" ToolTip="پیگیری" UseSubmitBehavior="False" OnClick="btnTracing_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewExpertFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Cheque Status ReChange.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){ 
	GridViewExpertFile.PerformCallback('Print');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){  }" />
                                            <Image  Url="~/Images/icons/ExportExcel.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="انتخاب ستون ها"
                                            ID="ChooseColumn" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="False" Visible="true">
                                            <ClientSideEvents Click="function(s, e) {
	if(!GridViewExpertFile.IsCustomizationWindowVisible())
		GridViewExpertFile.ShowCustomizationWindow();
	else
		GridViewExpertFile.HideCustomizationWindow();
}" />
                                            <Image  Url="~/Images/icons/cursor-hand.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewExpertFile"
                SessionName="SendBackDataTable_EmpMeFile" OnCallback="WFUserControl_Callback" />
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewExpertFile"
                ExportedRowType="All">
            </dxwgv:ASPxGridViewExporter>
            <asp:ObjectDataSource ID="ObjectDataSourceexpertFile" runat="server" SelectMethod="SelectExpertFileForManagmantpage"
                TypeName="TSP.DataManager.ExpertFileManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="EfId" Type="Int32" />
                    <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                    <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
                    <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
                TypeName="TSP.DataManager.WorkFlowTaskManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="SelectExpertFileRequestForManagmantpage"
                TypeName="TSP.DataManager.ExpertFileRequestManager">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="EfId" SessionField="EfId" Type="Int32" />
                    <asp:SessionParameter DefaultValue="-1" Name="EfReqId" />
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


