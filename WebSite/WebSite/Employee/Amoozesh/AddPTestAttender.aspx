<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master" CodeFile="AddPTestAttender.aspx.cs" Inherits="Employee_Amoozesh_AddTeacher" %>


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
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
       <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
           CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
           FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
           WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
       </pdc:PersianDateScriptManager>
       <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
           <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
               href="#">بستن</a>]</div>
           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                       <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                           width="100%">
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                       <tr>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnSave_Click" ToolTip="ذخیره" ValidationGroup="a" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                   EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف" UseSubmitBehavior="False">
                                                   <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                   <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                       </tr>
                                   </table>
                               </td>
                           </tr>
                       </table>
                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
       <br />
       	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

 
                   <table style="vertical-align: top; text-align: right" cellpadding="1" dir="rtl">
                       <tr>
                           <td style="vertical-align: top; text-align: right" colspan="4">
                               <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="لطفآ افرادی را که در آزمون شرکت کرده اند انتخاب نمائید و سپس بر روی دکمه ذخیره کلیک نمائید." Visible="False">
                               </dxe:ASPxLabel>
                               <br />
                           </td>
                       </tr>
                       <tr>
                           <td style="vertical-align: top; text-align: right" colspan="4">
                               <div dir="ltr" style="width: 100px; height: 100px; text-align: left">
                                   <TSPControls:CustomASPxListBox ID="ListBox" runat="server" 
                                        DataSourceID="ODBPeriodAttender" Height="500px" 
                                       ImageUrlField="ImageUrl"  Rows="10" TextField="AttenderName;FatherName"
                                       ValueField="PAId">
                                       <Columns>
                                           <dxe:ListBoxColumn Caption="نام پدر" FieldName="FatherName" Width="100px" />
                                           <dxe:ListBoxColumn Caption="نام و نام خانوادگی" FieldName="AttenderName" Width="150px" />
                                       </Columns>
                                       <ItemImage Height="40px" Width="40px" />
                                       <ItemStyle HorizontalAlign="Center" />
                                       <ValidationSettings>
                                           
                                           <ErrorFrameStyle ImageSpacing="4px">
                                               <ErrorTextPaddings PaddingLeft="4px" />
                                           </ErrorFrameStyle>
                                       </ValidationSettings>
                                   </TSPControls:CustomASPxListBox>
                               </div>
                               <asp:ObjectDataSource ID="ODBPeriodAttender" runat="server"
                                   SelectMethod="GetData" TypeName="TSP.DataManager.PeriodAttenderManager" OldValuesParameterFormatString="original_{0}">
                               </asp:ObjectDataSource>
                           </td>
                       </tr>
                   </table>
                     </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
       <br />
       <asp:HiddenField ID="TestAtId" runat="server" Visible="False" />
       <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                       <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                           width="100%">
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                       <tr>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnSave_Click" ToolTip="ذخیره" ValidationGroup="a" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                   EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف" UseSubmitBehavior="False">
                                                   <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                   <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                       </tr>
                                   </table>
                               </td>
                           </tr>
                       </table>
                  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

</asp:Content>
