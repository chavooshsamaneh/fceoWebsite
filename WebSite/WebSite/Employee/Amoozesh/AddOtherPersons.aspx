<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddOtherPersons.aspx.cs" Inherits="Employee_Amoozesh_AddOtherPersons" Title="مشخصات شخص" %>


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
    <div id="Content" runat="server" style="width: 100%; text-align: center;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 600px">
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
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnSave_Click" ToolTip="ذخیره" Text=" " UseSubmitBehavior="False">
                                                                <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                 EnableTheming="False" EnableViewState="False"
                                                                OnClick="btnDisActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	// e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td style="width: 27px; height: 27px;">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" Text=" " UseSubmitBehavior="False" CausesValidation="False">
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
                    	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                       <table style="vertical-align: top; text-align: right; display: block; overflow: hidden;" cellpadding="1" dir="rtl">
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="نام">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right;">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtName" runat="server" Width="150px"  >
                                       <ValidationSettings ErrorText="" ErrorTextPosition="Bottom" Display="Dynamic">
                                           <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید" />
                                       </ValidationSettings>
                                   </TSPControls:CustomTextBox>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="نام خانوادگی">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFamily" runat="server" Width="150px"  >
                                       <ValidationSettings ErrorText="" ErrorTextPosition="Bottom" Display="Dynamic">
                                           <RequiredField ErrorText="لطفاً این فیلد را تکمیل نمائید." IsRequired="True" />
                                       </ValidationSettings>
                                   </TSPControls:CustomTextBox>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="نام پدر">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFatherName" runat="server" Width="150px"  >
                                   </TSPControls:CustomTextBox>
                               </td>
                               <td style="vertical-align: top; text-align: right">&nbsp;</td>
                               <td style="vertical-align: top; text-align: right">&nbsp;
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ تولد">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <pdc:PersianDateTextBox ID="txtBrithDate" runat="server" AutoCompleteType="Disabled"
                                       DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" SetDefaultDateOnEvent="None"
                                       ShowPickerOnEvent="OnRightClick" ShowPickerOnTop="True" Width="145px"></pdc:PersianDateTextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBrithDate"
                                       ErrorMessage="تاریخ تولد" ToolTip="لطفاً این فیلد را پر کنید." Display="Dynamic">لطفاً این فیلد را پر کنید.</asp:RequiredFieldValidator>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="محل تولد">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtBirthPlace" runat="server" Width="150px"  >
                                   </TSPControls:CustomTextBox>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="شماره شناسنامه">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtIdNo" runat="server" Width="150px"  >
                                       <MaskSettings Mask="&lt;1..9999999999&gt;" />
                                       <ValidationSettings ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom" Display="Dynamic">
                                           <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{0,10}" />
                                       </ValidationSettings>
                                   </TSPControls:CustomTextBox>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="کد ملی">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtSSN" runat="server" Width="150px" MaxLength="10"  >
                                       <MaskSettings Mask="&lt;0..9999999999&gt;" IncludeLiterals="None" />
                                       <ValidationSettings ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom" Display="Dynamic">
                                           <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{10}" />
                                       </ValidationSettings>
                                   </TSPControls:CustomTextBox>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="شماره تلفن">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtTel" runat="server" Width="150px" MaxLength="12"  >
                                       <ValidationSettings ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom" Display="Dynamic">
                                           <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{8,11}" />
                                       </ValidationSettings>
                                   </TSPControls:CustomTextBox>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="شماره تلفن همراه" Width="100px">
                                   </dxe:ASPxLabel>
                               </td>
                               <td style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMobileNo" runat="server" Width="150px" MaxLength="11"  >
                                       <ValidationSettings ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom" Display="Dynamic">
                                           <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}" />
                                       </ValidationSettings>
                                   </TSPControls:CustomTextBox>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; height: 24px; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="آدرس">
                                   </dxe:ASPxLabel>
                               </td>
                               <td colspan="3" style="vertical-align: top; height: 24px; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtAddress" runat="server" Width="420px"  >
                                   </TSPControls:CustomTextBox>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Text="آدرس پست الکترونیکی" Width="120px">
                                   </dxe:ASPxLabel>
                               </td>
                               <td colspan="3" style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomTextBox IsMenuButton="true" ID="txtEmail" runat="server" Width="420px"  >
                                       <ValidationSettings ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom" Display="Dynamic">
                                           <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                       </ValidationSettings>
                                   </TSPControls:CustomTextBox>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Text="توضیحات" Width="120px">
                                   </dxe:ASPxLabel>
                               </td>
                               <td colspan="3" style="vertical-align: top; text-align: right">
                                   <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" Height="71px" Width="420px"  >
                                   </TSPControls:CustomASPXMemo>
                               </td>
                           </tr>
                           <tr>
                               <td style="vertical-align: top; text-align: right">
                                   <dxe:ASPxLabel ID="lbImage" runat="server" Text="تصویر">
                                   </dxe:ASPxLabel>
                               </td>
                               <td colspan="3" style="vertical-align: top; text-align: right">
                                   <div style="float: right">
                                       <dxe:ASPxImage ID="Image" runat="server" Height="70px" Width="70px">
                                       </dxe:ASPxImage>
                                       &nbsp;
                                   </div>
                                   <TSPControls:CustomAspxUploadControl ID="flpImage" runat="server" InputType="Images"
                                       MaxSizeForUploadFile="0" ShowProgressPanel="True">
                                   </TSPControls:CustomAspxUploadControl>
                               </td>
                           </tr>
                       </table>
                 
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                    </div>
                    <asp:HiddenField ID="OtherPersonId" runat="server" Visible="False" />
                    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
                   <br />
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
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px" CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnSave_Click" ToolTip="ذخیره" Text=" " UseSubmitBehavior="False">
                                                   <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td >
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                    EnableTheming="False" EnableViewState="False"
                                                   OnClick="btnDisActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                                   <ClientSideEvents Click="function(s, e) {
	// e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                                   <HoverStyle BackColor="#FFE0C0">
                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                   </HoverStyle>
                                                   <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                               </TSPControls:CustomAspxButton>
                                           </td>
                                           <td style="width: 27px; height: 27px;">
                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server"  EnableTheming="False"
                                                   EnableViewState="False" OnClick="btnBack_Click" ToolTip="بازگشت" CausesValidation="False" Text=" " UseSubmitBehavior="False">
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
                    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server"
                        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                        <ProgressTemplate>
                            <div class="modalPopup">
                                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                            </div>
                        </ProgressTemplate>
                    </asp:ModalUpdateProgress>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

