<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master" CodeFile="AddTestAttender.aspx.cs" Inherits="Employee_Amoozesh_AddTeacher" %>


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

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                href="#">بستن</a>]
        </div>
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
                                                    EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید">
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="width: 27px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px">
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="width: 27px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnSave_Click" ToolTip="ذخیره" ValidationGroup="a">
                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="width: 27px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف">
                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="width: 27px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت">
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
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: center; height: 243px;" colspan="4">
                                    <br />
                                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="ODBPeriodAttender" KeyFieldName="PAId">
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn Caption="انتخاب" ShowSelectCheckbox="True" VisibleIndex="0">
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="کد شرکت کننده در دوره" FieldName="PAId" Visible="False"
                                                VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نام و نام خانوادگی" FieldName="AttenderName"
                                                VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="2">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="ImageUrl" VisibleIndex="3">
                                                <PropertiesImage ImageHeight="40px" ImageWidth="40px"></PropertiesImage>
                                            </dxwgv:GridViewDataImageColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                        <SettingsPager>
                                            <AllButton Text="همه رکوردها">
                                            </AllButton>
                                            <FirstPageButton Text="اولین صفحه">
                                            </FirstPageButton>
                                            <LastPageButton Text="آخرین صفحه">
                                            </LastPageButton>
                                            <NextPageButton Text="صفحه بعد">
                                            </NextPageButton>
                                            <PrevPageButton Text="صفحه قبل">
                                            </PrevPageButton>
                                            <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})" />
                                        </SettingsPager>
                                        <Settings ShowGroupPanel="True" />
                                        <SettingsText CommandCancel="انصراف" CommandClearFilter="پاک کردن فیلتر" CommandDelete="حذف"
                                            CommandEdit="ویرایش" CommandNew="جدید" CommandSelect="انتخاب" CommandUpdate="ذخیره"
                                            ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد"
                                            GroupPanel="برای گروه بندی از این قسمت استفاده کنید" />
                                        <SettingsLoadingPanel Text="در حال بارگذاری" />
                                        <Styles>
                                            <Header HorizontalAlign="Center">
                                            </Header>
                                            <SelectedRow BackColor="White" ForeColor="Black">
                                            </SelectedRow>
                                            <GroupPanel BackColor="CornflowerBlue" ForeColor="Black">
                                            </GroupPanel>
                                        </Styles>
                                    </TSPControls:CustomAspxDevGridView>
                                    <asp:ObjectDataSource ID="ODBPeriodAttender" runat="server"
                                        SelectMethod="GetData" TypeName="TSP.DataManager.PeriodAttenderManager" OldValuesParameterFormatString="original_{0}" FilterExpression="PPId={0}">
                                        <FilterParameters>
                                            <asp:Parameter DefaultValue="-1" Name="PPId" />
                                        </FilterParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
        <asp:HiddenField ID="PeriodId" runat="server" Visible="False" />
        <asp:HiddenField ID="TestID" runat="server" Visible="False" />
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
                                                   EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید">
                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px">
                                                   <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnSave_Click" ToolTip="ذخیره" ValidationGroup="a">
                                                   <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                   EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف">
                                                   <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                   <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت">
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
