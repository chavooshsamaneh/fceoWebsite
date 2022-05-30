<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeLetters.aspx.cs" Inherits="Office_OfficeInfo_OfficeLetters"
    Title="روزنامه های رسمی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">


        function SetTaskOrderError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
                document.getElementById('<%=LabelWarning.ClientID%>').innerText = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }

    </script>

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="جدید"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
		grid.cpError=0;
grid.AddNewRow();
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="ویرایش"
                                    UseSubmitBehavior="False" Width="25px">
                                    <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	{
    grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
  }
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                    EnableTheming="False" EnableViewState="False"
                                    OnClick="btnInActive_Click" Text=" " ToolTip="غیر فعال" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                    CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/BakToManagment.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callmenu"
        OnCallback="ASPxCallbackPanel1_Callback" Width="100%" HideContentOnCallback="False">
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" ItemImagePosition="right" runat="server"
                    SeparatorHeight="100%" SeparatorColor="#A5A6A8"
                    AutoSeparators="RootOnly" OnItemClick="ASPxMenu1_ItemClick"
                    ItemSpacing="0px" SeparatorWidth="1px" RightToLeft="True">
                    <Items>
                        <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Member" Text="اعضا">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="شعبه ها" Name="Agent">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="سوابق کاری" Name="Job">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="روزنامه های رسمی" Name="Letters" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مستندات" Name="Attach">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Group" Text="گروه ها">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>
    <ul class="HelpUL">
        <li>پس از تکمیل تمامی اطلاعات خود در سربرگ های مختلف در نهایت جهت بررسی پرونده توسط سازمان باید به صفحه ی مدیریت درخواست هاي عضويت/مدیریت درخواست هاي پروانه رفته و برروی دکمه ''گردش کار'' 
                        (
            <asp:Image ID="Image1" ImageUrl="~/Images/icons/reload.png" Width="25px" runat="server" />
            )
                         کلیک نمایید.سپس در پنجره باز شده برروی دکمه ارسال کلیک نمایید.
        </li>
    </ul>
    <div style="width: 100%" align="right">
        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
        </dxe:ASPxLabel>
        <dxe:ASPxLabel ID="lblOfName" runat="server">
        </dxe:ASPxLabel>
    </div>

    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="OdbOfLetters"
        Width="100%"
        ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="OlId" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
        OnCustomDataCallback="CustomAspxDevGridView1_CustomDataCallback" OnRowInserting="CustomAspxDevGridView1_RowInserting"
        OnRowUpdating="CustomAspxDevGridView1_RowUpdating" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared"
        OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
        <ClientSideEvents EndCallback="function(s, e) {

if(grid.cpError==2)
SetTaskOrderError(grid.cpErrorMsg);
if(grid.cpMenu==1)
	callmenu.PerformCallback('');
	
}"
            BeginCallback="function(s, e) {
	grid.cpMenu=0;
}"></ClientSideEvents>
        <Columns>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OlId" Caption="کد" Name="OlId"
                Width="30px">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LetterNo" Caption="شماره روزنامه"
                Name="LetterNo" Width="60px">
                <PropertiesTextEdit Width="100px">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="شماره روزنامه را وارد نمایید"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PageNo" Caption="شماره صفحه"
                Name="PageNo" Width="60px">
                <PropertiesTextEdit Width="100px">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="شماره صفحه را وارد نمایید"></RequiredField>
                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,3}"></RegularExpression>
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ" Name="Date"
                Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
                <PropertiesTextEdit Width="100px">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="تاریخ را وارد نمایید"></RequiredField>
                        <RegularExpression ErrorText="تاریخ را با فرمت--/--/--13 وارد نمایید" ValidationExpression="\d{4}/\d{2}/\d{2}"></RegularExpression>
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="OfReId">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="4"
                Width="320px">
                <PropertiesTextEdit Width="300px">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="5"
                Width="60px">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="true" ShowHorizontalScrollBar="true"></Settings>
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    ID="BtnNew2" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
		grid.cpError=0;
grid.AddNewRow();

}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  {
    grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
  }
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                    CausesValidation="False" ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/disactive.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBack_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/Back.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                    CausesValidation="False" ID="ASPxButton3" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/BakToManagment.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>  
    <dxhf:ASPxHiddenField ID="LetterId" runat="server">
    </dxhf:ASPxHiddenField>
    <dxhf:ASPxHiddenField ID="LetterMode" runat="server">
    </dxhf:ASPxHiddenField>
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
    </dxhf:ASPxHiddenField>

    <asp:ObjectDataSource ID="OdbOfLetters" runat="server" TypeName="TSP.DataManager.OfficialLetterManager"
        SelectMethod="FindByOffRequest" FilterExpression="OfId={0}" OldValuesParameterFormatString="original_{0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="JustActive" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
