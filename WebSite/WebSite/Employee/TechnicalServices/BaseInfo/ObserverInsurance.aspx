<%@ Page Title="مدیریت بیمه ناظران" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ObserverInsurance.aspx.cs" Inherits="Employee_TechnicalServices_BaseInfo_ObserverInsurance" %>


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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="جدید" ID="BtnNew"
                                    EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	GridViewObsInsurance.AddNewRow();
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                    Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                    ID="btnEdit" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewObsInsurance.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewObsInsurance.StartEditRow(GridViewObsInsurance.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="غیر فعال"
                                    ID="btnInActive" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewObsInsurance.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/Disactive.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="فعال"
                                    ID="btnActive" EnableViewState="False" OnClick="btnActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewObsInsurance.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/Active.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewObsInsurance" Width="100%" runat="server"
        AutoGenerateColumns="False"
        DataSourceID="ObjDataSourceObsInsurance" ClientInstanceName="GridViewObsInsurance" KeyFieldName="ObsInId"
        OnRowInserting="GridViewObsInsurance_RowInserting" OnRowUpdating="GridViewObsInsurance_RowUpdating"
        RightToLeft="True">
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="نوع ناظر" FieldName="ObserversTypeId" VisibleIndex="0"
                >
                <PropertiesComboBox TextField="Title" ValueField="ObserversTypeId" DataSourceID="ObjectDataSourceObserversType">
                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="درصد وارد نشده است"></RequiredField>
                    </ValidationSettings>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="هزینه بیمه" FieldName="Price" VisibleIndex="0"
                >
                <PropertiesTextEdit>
                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="هزینه وارد نشده است"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="درصد بیمه" FieldName="PricePercent" VisibleIndex="0"
                >
                <PropertiesTextEdit>
                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="درصد وارد نشده است"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع قرارداد" FieldName="ContractStartDate" VisibleIndex="0">
                <PropertiesTextEdit>
                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="تاریخ شروع وارد نشده است"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditItemTemplate>
                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                        ID="txtContractStartDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                        Style="text-align: right;" ShowPickerOnEvent="OnClick" Text='<%# Bind("ContractStartDate") %>'>
                    </pdc:PersianDateTextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContractStartDate" ID="RequiredFieldValidator3"
                        Display="Dynamic">تاریخ را انتخاب نمایید</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان قرارداد" FieldName="ContractEndDate" VisibleIndex="0">
                <PropertiesTextEdit>
                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="تاریخ پایان وارد نشده است"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditItemTemplate>
                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                        ID="txtContractStartDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                        Style="text-align: right;" ShowPickerOnEvent="OnClick"  Text='<%# Bind("ContractEndDate") %>'>
                    </pdc:PersianDateTextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContractStartDate" ID="ContractEndDate"
                        Display="Dynamic">تاریخ را انتخاب نمایید</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="1"
                Width="9%">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " Name=" " VisibleIndex="3" Width="6%" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="AlbumId" Visible="False" VisibleIndex="2">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents EndCallback="function(s, e) {
if(s.cpMessage!='')
{
 ShowMessage(s.cpMessage);
}
}" />
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormModal="True"
            PopupEditFormVerticalAlign="WindowCenter" />

    </TSPControls:CustomAspxDevGridView>

    <asp:ObjectDataSource ID="ObjectDataSourceObserversType" runat="server" TypeName="TSP.DataManager.TechnicalServices.ObserversTypeManager"
        SelectMethod="GetData"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjDataSourceObsInsurance" runat="server" TypeName="TSP.DataManager.TechnicalServices.ObserverInsuranceManager"
        SelectMethod="SelectTSObserverInsuranceForManagementPage"></asp:ObjectDataSource>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="جدید" ID="btnNew2"
                                    EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
		GridViewObsInsurance.AddNewRow();
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                    Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                    ID="btnEdit2" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewObsInsurance.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewObsInsurance.StartEditRow(GridViewObsInsurance.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="غیر فعال"
                                    ID="btnInActive2" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewObsInsurance.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/Disactive.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="فعال"
                                    ID="btnActive2" EnableViewState="False" OnClick="btnActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewObsInsurance.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/Active.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>



