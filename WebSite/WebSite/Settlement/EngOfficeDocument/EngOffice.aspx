<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOffice.aspx.cs" Inherits="Settlement_EngOfficeDocument_EngOffice"
    Title="مدیریت دفاتر" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px"
                                Text=" " UseSubmitBehavior="False">
                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnShow_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{		
    ShowWf();
}
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                ToolTip="پیگیری" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                ID="btnChooseCln1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                AutoPostBack="False" Visible="true">
                                <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
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
                        <td align="right" width="15%">
                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="کد دفتر" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" width="35%">
                            <TSPControls:CustomTextBox ID="txtEngOffId" runat="server" ClientInstanceName="txtEngOffId"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد دفتر را صحیح وارد نمایید" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" width="15%"></td>
                        <td align="right" width="35%"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نام دفتر" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox ID="txtEngOffName" runat="server" ClientInstanceName="txtEngOffName"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                          <%--  <dxe:ASPxLabel ID="ASPxLabel5" Wrap="False" runat="server" Text="کدعضویت شرکا" Width="100%">
                            </dxe:ASPxLabel>--%>
                        </td>
                        <td align="right">
                            <%--<TSPControls:CustomTextBox ID="txtMeId" runat="server" ClientInstanceName="txtMeId"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel3" Wrap="False" runat="server" Text="نام مدیر مسئول"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox ID="txtManagerName" runat="server" ClientInstanceName="txtManagerName"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel4" Wrap="False" runat="server" Text="نام خانوادگی مدیر مسئول"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox ID="txtManagerfamily" runat="server" ClientInstanceName="txtManagerfamily"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel10" Wrap="False" runat="server" Text="تاریخ اعتبار از"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtEndDateFrom" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" Width="225px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel9" Wrap="False" runat="server" Text="تاریخ اعتبار تا"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtEndDateTo" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" Width="225px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel13" Wrap="False" runat="server" Text="تاریخ درخواست از"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateFrom" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" Width="225px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel14" Wrap="False" runat="server" Text="تاریخ درخواست تا"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateTo" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" Width="225px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel7" Wrap="False" runat="server" Text="تاریخ آخرین بررسی از"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtWFFrom" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" Width="225px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel23" Wrap="False" runat="server" Text="تاریخ آخرین بررسی تا"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtWFTo" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" Width="225px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel8" Wrap="False" runat="server" Text="نوع درخواست" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox ID="CmbReqType" runat="server"
                                ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="CmbReqType" HorizontalAlign="Right"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <Items>
                                    <dxe:ListEditItem Text="همه موارد" Selected="true" Value="-1" />
                                    <dxe:ListEditItem Text="صدور پروانه" Value="0" />
                                    <dxe:ListEditItem Text="تمدید" Value="1" />
                                    <dxe:ListEditItem Text="تغییرات" Value="2" />
                                    <dxe:ListEditItem Text="المثنی" Value="3" />
                                    <dxe:ListEditItem Text="ابطال" Value="4" />
                                    <dxe:ListEditItem Text="صدور سیستم قدیم" Value="5" />
                                    <dxe:ListEditItem Text="تغییرات اطلاعات پایه" Value="6" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="کد پیگیری" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox ID="txtFollowCode" runat="server" ClientInstanceName="txtFollowCode"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>مرحله
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="CmbTask" runat="server"
                                ValueType="System.String"
                                TextField="TaskName" ValueField="TaskId" RightToLeft="True" ClientInstanceName="CmbTask"
                                DataSourceID="ObjdsWorkFlowTask" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td>آخرین بررسی کننده
                        </td>
                        <td>
                            <TSPControls:CustomTextBox ID="txtEndAuditor" runat="server" ClientInstanceName="txtEndAuditor"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" valign="top">
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="btnSearch" OnClick="btnSearch_OnClick" runat="server"
                                            Text="جستجو"
                                            ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false" CausesValidation="true">
                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer=false;
 if (ASPxClientEdit.ValidateGroup('SearchValid'))
{

	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                        <td>
                                            <TSPControls:CustomAspxButton ID="btnClearSearch" runat="server" OnClick="btnSearch_OnClick"
                                                Text="پاک کردن فرم"
                                                UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s, e) {

        ClearSearch();
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <div align="right">
        <ul class="HelpUL">
            <li>در صورتی که در هنگام چاپ گواهینامه دفتر شماره سریال کارت گواهینامه توسط کاربران
                    ثبت نشده باشد. پیش از تایید دفتر در گردش کار بایستی پس از زدن دکمه ویرایش "شماره
                    سریال" را در صفحه مشخصات دفتر ثبت نمایید. </li>
        </ul>
    </div>
    <TSPControls:CustomAspxDevGridView ID="GridViewEngOffice" runat="server" AutoGenerateColumns="False"
        KeyFieldName="EngOfId" Width="100%"
        DataSourceID="OdbEngOffice" ClientInstanceName="grid" OnHtmlRowPrepared="GridViewEngOffice_HtmlRowPrepared">
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
        <SettingsCookies Enabled="false" />
        <SettingsCustomizationWindow Enabled="True" />
        <Columns>

            <dxwgv:GridViewCommandColumn Caption=" " ShowSelectCheckbox="true" Name="CheckBox" VisibleIndex="0"
                Width="30px">
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="NotConfirmOfReId" Visible="false" Name="NotConfirmOfReId" ShowInCustomizationForm="false"
                VisibleIndex="0" Width="50px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                VisibleIndex="0" Width="40px">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("TaskName") %>'>
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد دفتر" FieldName="EngOfId" Name="EngOfId"
                VisibleIndex="0" Width="50px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع دفتر" FieldName="OfTName" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" VisibleIndex="2"
                Width="150px">
                <CellStyle Wrap="false" HorizontalAlign="Center" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مدیر مسئول" FieldName="MeName" Name="MeName"
                VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره مشارکت نامه" FieldName="ParticipateLetterNo"
                VisibleIndex="3">
                <CellStyle Wrap="false" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ مشارکت نامه" FieldName="ParticipateLetterDate"
                VisibleIndex="4">
                <CellStyle Wrap="false" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره دفتر اسناد رسمی" FieldName="EngOffNo"
                VisibleIndex="5">
                <CellStyle Wrap="false" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="آخرین بررسی کننده" FieldName="WFDoerName"
                VisibleIndex="5">
                <CellStyle Wrap="false" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ آخرین بررسی" FieldName="WFDate"
                VisibleIndex="5">
                <CellStyle Wrap="false" />
            </dxwgv:GridViewDataTextColumn>

<%--            <dxwgv:GridViewDataTextColumn Caption="تعداد اعضا" FieldName="MeCount" Name="MeCount"
                Width="65px" VisibleIndex="6">
            </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" Width="65px"
                VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Templates>
            <DetailRow>
                <div align="center">
                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                        AutoGenerateColumns="False"
                        DataSourceID="OdbRequest" KeyFieldName="EOfId" OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                        Width="100%">
                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                        <Columns>
                            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                VisibleIndex="0" Width="40px">
                                <DataItemTemplate>
                                    <div align="center">
                                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("TaskName") %>'>
                                        </dxe:ASPxImage>
                                    </div>
                                </DataItemTemplate>
                                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                    ValueType="System.String">
                                </PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>

                            <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="6">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="Confirm" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" Visible="False"
                                VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeName" VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="3">
                                <CellStyle Wrap="True">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" VisibleIndex="0"
                                Visible="false">
                                <HeaderStyle Wrap="True" />
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه اشتغال" FieldName="FileNo" VisibleIndex="1">
                                <HeaderStyle Wrap="True" />
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور" FieldName="RegDate" Visible="False"
                                VisibleIndex="2">
                                <HeaderStyle Wrap="True" />
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="ExpireDate"
                                Visible="False" VisibleIndex="3">
                                <HeaderStyle Wrap="True" />
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت درخواست" FieldName="TaskName" VisibleIndex="4"
                                Visible="false">
                                <CellStyle Wrap="True">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>

                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="11" Width="30px" ShowClearFilterButton="true">
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="EngOfId" Visible="False" VisibleIndex="9">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
                </div>
            </DetailRow>
        </Templates>
        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
        <Settings ShowHorizontalScrollBar="true" />
        <ClientSideEvents FocusedRowChanged="function(s, e) {
			
}"
            DetailRowExpanding="function(s, e) {
	if(grid.cpIsReturn!=1)
	{
		grid.cpSelectedIndex=grid.GetFocusedRowIndex();
			
	}
	else
	{
		grid.cpIsReturn=0;	
	}				
		grid.SetFocusedRowIndex(grid.cpSelectedIndex);

}" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <fieldset width="98%">
        <legend>راهنما</legend>
        <table width="100%">
            <tbody>
                <tr>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="صدور: فونت مشکی" ForeColor="Black">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Text="تمدید: فونت سبز" ForeColor="Green">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel43" runat="server" Text="تغییرات: فونت آبی" ForeColor="DarkBlue">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="المثنی: فونت بنفش" ForeColor="DarkMagenta"
                            Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="ابطال: فونت قرمز" ForeColor="Red">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Text="باطل شده: فونت قهوه ای" ForeColor="Brown"
                            Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_WFStart.png" />
                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Text="درخواست صدور پروانه دفتر طراحی"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image3" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_DocumentUnitEmployee.png" />
                        <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Text="بررسی و تایید درخواست توسط مسئول واحد پروانه"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image4" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_settlementAgentConfirming.png" />
                        <dxe:ASPxLabel ID="ASPxLabel17" runat="server" Text="تایید کارشناس مسکن" ForeColor="Black"
                            Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image5" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_ NezamEmployeeInSettlement.png" />
                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server" Text="تایید رئیس اداره توسعه مهندسی و نظارت بر مقررات ملی و کیفیت ساخت"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image6" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_RoadAndurbanism.png" />
                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="تایید مدیر کل اداره راه و شهرسازی استان فارس"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image7" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_PrintDoc.png" />
                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" Text="چاپ گواهینامه توسط کارشناس واحد پروانه"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image11" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_PrintAndWaitForConfirm.png" />
                        <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Text="چاپ شده و منتظر تایید نهایی"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image8" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFConfirmAndEnd.png" />
                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="تایید و پایان بررسی صدور پروانه دفتر"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Image ID="Image9" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFREjectAndEnd.png" />
                        <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Text="عدم تایید و پایان بررسی صدور پروانه دفتر"
                            ForeColor="Black" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </tbody>
        </table>
    </fieldset>

    <br />

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px"
                                Text=" " UseSubmitBehavior="False">
                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnShow_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep2" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="'گردش کار" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{		
    ShowWf();
}
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                ToolTip="پیگیری" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                ID="btnChooseCln" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                AutoPostBack="False" Visible="true">
                                <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="OdbEngOffice" runat="server" SelectMethod="SelectEngOfficeForManagmentPage"
        TypeName="TSP.DataManager.EngOfficeManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
      <%--      <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />--%>
            <asp:Parameter DefaultValue="%" Name="ManagerName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="Managerfamily" Type="String" />
            <asp:Parameter DefaultValue="%" Name="EngOffName" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="EngOfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="ReqType" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="CreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="CreateDateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="WFDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="WFDateTo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="WFDoerName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="SelectForManagmentPage"
        TypeName="TSP.DataManager.EngOffFileManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="EngOfId" SessionField="EngOfficeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:WFUserControl ID="WFUserControl" runat="server" OnCallback="WFUserControl_Callback"
        GridName="grid" SessionName="SendBackDataTable_EngOffConfStl" />
</asp:Content>
