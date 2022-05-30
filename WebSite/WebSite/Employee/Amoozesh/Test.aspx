<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master" CodeFile="Test.aspx.cs" Inherits="Employee_Amoozesh_Observer" Title="آزمون های دوره" %>

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
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             
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
                               <td style="vertical-align: top; text-align: left">
                                   <table style="display: block; overflow: hidden; border-collapse: collapse">
                                       <tr>
                                           <td>
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNazerin" runat="server" CausesValidation="False"
                                                   OnClick="BtnNazerin_Click" Text="ناظرین آزمون" Width="87px"   Wrap="False" UseSubmitBehavior="False">
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td>
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTestattender" runat="server" CausesValidation="False"
                                                   OnClick="btnTestattender_Click" Text="شرکت کنندگان در آزمون" Width="149px"   Wrap="False" UseSubmitBehavior="False">
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                       </tr>
                                   </table>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                       <tr>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                   EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف" Text=" " UseSubmitBehavior="False">
                                                   <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                   <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
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
                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                        DataSourceID="OdbTest" KeyFieldName="TestId" Width="100%">
                        
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="TestId" Visible="False" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="TestName" VisibleIndex="0"
                                Width="170px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد آزمون" FieldName="TestCode" VisibleIndex="1"
                                Width="60px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ آزمون" FieldName="Date" VisibleIndex="2"
                                Width="80px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="محل برگزاری آزمون" FieldName="Place" VisibleIndex="3" Width="300px" Visible="False">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ساعت شروع" FieldName="StartTime" VisibleIndex="3" Width="60px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ساعت پایان" FieldName="EndTime" VisibleIndex="4" Width="60px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="5" Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                      
                    </TSPControls:CustomAspxDevGridView>
                    <br />
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                       <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                           width="100%">
                           <tr>
                               <td style="vertical-align: top; text-align: left">
                                   <table style="display: block; overflow: hidden; border-collapse: collapse">
                                       <tr>
                                           <td>
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNezarin" runat="server" CausesValidation="False"
                                                   OnClick="BtnNazerin_Click" Text="ناظرین آزمون"   Wrap="False" UseSubmitBehavior="False">
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td>
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTestAttender1" runat="server" CausesValidation="False"
                                                   OnClick="btnTestattender_Click" Text="شرکت کنندگان در آزمون" Width="149px"   Wrap="False" UseSubmitBehavior="False">
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                       </tr>
                                   </table>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                       <tr>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                   EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف" Text=" " UseSubmitBehavior="False">
                                                   <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                   <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                       </tr>
                                   </table>
                               </td>
                           </tr>
                       </table> </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                   
                    <asp:ObjectDataSource ID="OdbTest" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TestManger" FilterExpression="PPId={0}" OldValuesParameterFormatString="original_{0}">
                        <FilterParameters>
                            <asp:Parameter DefaultValue="-1" Name="PPId" />
                        </FilterParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="PPId" runat="server" Visible="False" />
                    <br />
                    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                        <ProgressTemplate>
                            <div class="modalPopup">
                                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                            </div>
                        </ProgressTemplate>
                    </asp:ModalUpdateProgress>
     
            </ContentTemplate>
        </asp:UpdatePanel>
 

</asp:Content>


