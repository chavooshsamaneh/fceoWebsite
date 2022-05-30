<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddGrades.aspx.cs" Inherits="Employee_Amoozesh_AddGrades" Title="نمرات" %>

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
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
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
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" CausesValidation="False">
                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                   </HoverStyle>
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
           	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="لیست شرکت کنندگان در آزمون" runat="server"
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
                           <td style="vertical-align: top; text-align: center;" colspan="4">
                               <br />
                               <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                                   DataSourceID="ODBTestAttender" KeyFieldName="TAId" OnRowUpdating="CustomAspxDevGridView1_RowUpdating">
                                   <Columns>
                                       <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="TAId" ReadOnly="True" Visible="False"
                                           VisibleIndex="0">
                                       </dxwgv:GridViewDataTextColumn>
                                       <dxwgv:GridViewDataTextColumn Caption="کد شرکت کننده در آزمون" FieldName="PAId" Visible="False"
                                           VisibleIndex="1">
                                       </dxwgv:GridViewDataTextColumn>
                                       <dxwgv:GridViewDataTextColumn Caption="نام و نام خانوادگی" FieldName="AttenderName"
                                           ReadOnly="True" VisibleIndex="0" Width="150px">
                                       </dxwgv:GridViewDataTextColumn>
                                       <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" ReadOnly="True"
                                           VisibleIndex="1" Width="80px">
                                       </dxwgv:GridViewDataTextColumn>
                                       <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="ImageUrl" VisibleIndex="2">
                                           <propertiesimage imageheight="40px" imagewidth="40px"></propertiesimage>
                                       </dxwgv:GridViewDataImageColumn>
                                       <dxwgv:GridViewDataTextColumn Caption="نمره" FieldName="Mark" VisibleIndex="3" Width="80px">
                                           <cellstyle font-bold="True"></cellstyle>
                                       </dxwgv:GridViewDataTextColumn>
                                       <dxwgv:GridViewDataCheckColumn Caption="نتیجه" FieldName="Passed" VisibleIndex="4">
                                           <propertiescheckedit displaytextchecked="قبول" displaytextunchecked="مردود"></propertiescheckedit>
                                       </dxwgv:GridViewDataCheckColumn>
                                       <dxwgv:GridViewDataTextColumn Caption="قبول شده" FieldName="Passed" Visible="False"
                                           VisibleIndex="4">
                                       </dxwgv:GridViewDataTextColumn>
                                       <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="Description" ReadOnly="True"
                                           VisibleIndex="5" Width="150px">
                                           <dataitemtemplate>
&nbsp;
</dataitemtemplate>
                                       </dxwgv:GridViewDataMemoColumn>
                                       <dxwgv:GridViewCommandColumn Caption="عملیات" VisibleIndex="6" Width="80px"  ShowEditButton="true" ShowCancelButton="true" ShowUpdateButton="true">
                                          <%-- <editbutton text="ثبت نمره" visible="True"></editbutton>
                                           <cancelbutton text="انصراف" visible="True"></cancelbutton>
                                           <updatebutton text="ذخیره" visible="True"></updatebutton>
                                           <cellstyle wrap="False"></cellstyle>--%>
                                       </dxwgv:GridViewCommandColumn>
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
                                       <GroupPanel BackColor="CornflowerBlue" ForeColor="White">
                                       </GroupPanel>
                                   </Styles>
                                   <SettingsEditing Mode="Inline" />
                               </TSPControls:CustomAspxDevGridView>
                               <asp:ObjectDataSource ID="ODBTestAttender" runat="server"
                                   SelectMethod="GetData" TypeName="TSP.DataManager.TestAttenderManager" OldValuesParameterFormatString="original_{0}" FilterExpression="TestId={0}">
                                   <FilterParameters>
                                       <asp:Parameter DefaultValue="-1" Name="TestId" />
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
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" CausesValidation="False">
                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                   </HoverStyle>
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
       <asp:ModalUpdateProgress id="ModalUpdateProgress2" runat="server" 
           BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
           <progresstemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </progresstemplate>
       </asp:ModalUpdateProgress>

       </ContentTemplate>
       </asp:UpdatePanel>


</asp:Content>
