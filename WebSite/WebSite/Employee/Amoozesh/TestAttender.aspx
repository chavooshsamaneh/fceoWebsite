<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master" CodeFile="TestAttender.aspx.cs" Inherits="Employee_Amoozesh_Observer" Title="شرکت کنندگان آزمون" %>

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
                 
                    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]
                    </div>
                    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                    </pdc:PersianDateScriptManager>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                       <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                           width="100%">
                           <tr>
                               <td style="vertical-align: top; text-align: left">
                                   <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnGrade" runat="server" OnClick="btnGrade_Click" Text="ثبت نمره"   Wrap="False" UseSubmitBehavior="False">
                                   </TSPControls:CustomAspxButton>
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
                </TSPControls:CustomASPxRoundPanelMenu>      <br />
                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                        DataSourceID="OdbTestAttender" KeyFieldName="TAId" Width="100%">
                         
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="TAId" Visible="False" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد شرکت کننده" FieldName="PAId" Visible="False"
                                VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد آزمون" FieldName="TestId" Visible="False"
                                VisibleIndex="10">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع عضویت" FieldName="TypeName" VisibleIndex="0" Width="70px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1"
                                Width="60px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2"
                                Width="100px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3"
                                Width="60px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد ملی" FieldName="SSN" VisibleIndex="4" Width="80px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره تلفن" FieldName="Tel" VisibleIndex="5" Width="80px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" VisibleIndex="6" Width="80px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع ثبت نام" FieldName="TypeOfReg" VisibleIndex="7"
                                Width="100px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نمره" FieldName="Mark" VisibleIndex="8" Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نتیحه" FieldName="PassedName" VisibleIndex="9"
                                Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                       
                    </TSPControls:CustomAspxDevGridView>
                    <br />  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                       <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                           width="100%">
                           <tr>
                               <td style="vertical-align: top; text-align: left">
                                   <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnGrade1" runat="server" OnClick="btnGrade_Click" Text="ثبت نمره"   Wrap="False" UseSubmitBehavior="False">
                                   </TSPControls:CustomAspxButton>
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
                       </table>
                 </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu> 
                    <asp:ObjectDataSource ID="OdbTestAttender" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TestAttenderManager" FilterExpression="TestId={0}">
                        <FilterParameters>
                            <asp:Parameter DefaultValue="-1" Name="TestId" />
                        </FilterParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="TestId" runat="server" Visible="False" />
                    <asp:HiddenField ID="PPId" runat="server" Visible="False" />
              
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


