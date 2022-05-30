<%@ Page Title="مدیریت تاریخ سابقه کار" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="MemberConfirmJobEditDate.aspx.cs" Inherits="Employee_Document_MemberConfirmJobEditDate" %>

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
                                    Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                    ID="btnEdit" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewJobConfirm.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewJobConfirm.StartEditRow(GridViewJobConfirm.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/edit.png">
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
    <TSPControls:CustomAspxDevGridView ID="GridViewJobConfirm" Width="100%" runat="server"
        AutoGenerateColumns="False"
        DataSourceID="ObjectDataSourceJobConfirm" ClientInstanceName="GridViewJobConfirm" KeyFieldName="JobConfId"
        OnRowUpdating="GridViewJobConfirm_RowUpdating"
        RightToLeft="True">
   

        <Columns>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MemberId" Caption="کد عضویت" Name="MemberId">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeFullName" Caption="نام و نام خانوادگی" Name="MeFullName">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JobConfId"
                Name="JobConfId">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InActives" Caption="وضعیت"
                Name="Position">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Position" Caption="سمت"
                Name="Position">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="از تاریخ" FieldName="FromDate" VisibleIndex="0">
                <PropertiesTextEdit>
                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="تاریخ شروع وارد نشده است"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditItemTemplate>
                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                        ID="txtFromDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                        Style="text-align: right;" ShowPickerOnEvent="OnClick" Text='<%# Bind("FromDate") %>'>
                    </pdc:PersianDateTextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFromDate" ID="RequiredFieldValidator3"
                        Display="Dynamic">تاریخ را انتخاب نمایید</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="تا تاریخ" FieldName="ToDate" VisibleIndex="0">
                <PropertiesTextEdit>
                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="تاریخ شروع وارد نشده است"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditItemTemplate>
                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                        ID="txtToDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                        Style="text-align: right;" ShowPickerOnEvent="OnClick" Text='<%# Bind("ToDate") %>'>
                    </pdc:PersianDateTextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtToDate" ID="RequiredFieldValidator3"
                        Display="Dynamic">تاریخ را انتخاب نمایید</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataImageColumn VisibleIndex="0"
                FieldName="FileURL" Caption="تصویر فرم تاییدیه" Name="FileURL" >
            <PropertiesImage ImageHeight="300px" ImageWidth="300px"></PropertiesImage>
            </dxwgv:GridViewDataImageColumn>
            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="0" PropertiesHyperLinkEdit-Text="تصویر فرم تاییدیه"
                FieldName="FileURL" Caption="تصویر فرم تاییدیه" Name="FileURL" PropertiesHyperLinkEdit-Target="_blank">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataHyperLinkColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ConfirmTypeName" Caption="نوع تایید کننده"
                Name="ProjectName">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت تایید کننده"
                Name="Employer">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Name" Caption="نام" Name="PrTypeName">
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

    <asp:ObjectDataSource ID="ObjectDataSourceJobConfirm" runat="server" SelectMethod="SelctJobConfirmationWithoutDate"
        TypeName="TSP.DataManager.DocMemberFileJobConfirmationManager"></asp:ObjectDataSource>
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
                                    Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                    ID="btnEdit2" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewJobConfirm.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewJobConfirm.StartEditRow(GridViewJobConfirm.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/edit.png">
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




