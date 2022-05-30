<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddCourses.aspx.cs" Inherits="Employee_Amoozesh_AddCourses"
    Title="مشخصات درس" %>

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
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="Content" runat="server" style="width: 100%" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table  class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" 
                                                EnableTheming="False" ToolTip="جدید" Text=" " ID="BtnNew" EnableViewState="False" CausesValidation="false" OnClick="BtnNew_Click">
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" 
                                                Width="25px" EnableTheming="False" ToolTip="ویرایش" Text=" " ID="btnEdit" EnableViewState="False"      OnClick="btnEdit_Click" >
                                                <Image  Url="~/Images/icons/edit.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                 EnableTheming="False" Text=" " ToolTip="ذخیره"
                                                ID="btnSave" EnableViewState="False" OnClick="btnSave_Click">
                                                <Image  Url="~/Images/icons/save.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" 
                                                CausesValidation="false" EnableTheming="False" ToolTip="بازگشت" Text=" " ID="btnBack"
                                                EnableViewState="False" OnClick="btnBack_Click">
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <div align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetails" runat="server" SeparatorHeight="100%" 
                        AutoSeparators="RootOnly" OnItemClick="MenuCourseDetails_ItemClick" 
                        >
                        <Items>
                            <dxm:MenuItem Name="Course" Text="مشخصات درس" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseRefrence" Text="منابع درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه بندی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelContent" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="vertical-align: top; text-align: right" width="100%" dir="rtl">
                                <tbody>
                                    <tr>
                                        <td class="TdFirst">
                                            <dxe:ASPxLabel runat="server" Text="کد" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdSecond">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCourseId">
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="کد را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td class="TdFirst">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان اعتبار(ماه)" Width="110px" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdSecond">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtValidDiuration" MaxLength="3">
                                                <MaskSettings Mask="&lt;0..999&gt;"></MaskSettings>
                                                <ValidationSettings>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید." ValidationExpression="\d{0,3}">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TdAlignment">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdAlignment">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCourseName">
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td class="TdAlignment">
                                            <dxe:ASPxLabel runat="server" Text="طول دوره(ساعت)" Width="101px" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdAlignment">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDuration">
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="طول دوره را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TdAlignment">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان عملی(ساعت)" Width="134px" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdAlignment">
                                             <TSPControls:CustomTextBox IsMenuButton="true"  runat="server" ID="txtbPracticalDuration">
                                                <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="مدت زمان عملی را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
                                                   
                                                </ValidationSettings>
                                           </TSPControls:CustomTextBox>
                                        </td>
                                        <td class="TdAlignment">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان تئوری(ساعت)" Width="130px" ID="ASPxLabel6"
                                        >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdAlignment">
                                              <TSPControls:CustomTextBox IsMenuButton="true"  runat="server" ID="txtbNonPracticalDuration">
                                                <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="مدت زمان بازدید از کارگاه را وارد نمایید">
                                                    </RequiredField>
                                                    <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
                                                    
                                                </ValidationSettings>
                                      </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TdAlignment">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان بازدید از کارگاه(ساعت)" Width="180px"
                                                ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdAlignment">
                                           <TSPControls:CustomTextBox IsMenuButton="true"  runat="server" ID="txtbWorkroomDuration" >
                                                <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                                <ValidationSettings >
                                                    <RequiredField IsRequired="True" ErrorText="مدت زمان بازدید از کارگاه را وارد نمایید">
                                                    </RequiredField>
                                                    <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
                                                   
                                                </ValidationSettings>
                                           </TSPControls:CustomTextBox>
                                        </td>
                                        <td class="TdAlignment">
                                            <dxe:ASPxLabel runat="server" Text="امتیاز" ID="ASPxLabel3">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="TdAlignment">
                                          <TSPControls:CustomTextBox IsMenuButton="true"  runat="server" ID="txtPoint" MaxLength="6">
                                                <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="امتیاز را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید." ValidationExpression="\d{0,6}">
                                                    </RegularExpression>
                                                   
                                                </ValidationSettings>
                                           </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table  class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click"
                                                UseSubmitBehavior="False"> 
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                <Image  Url="~/Images/icons/edit.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                                UseSubmitBehavior="False">
                                                <Image  Url="~/Images/icons/save.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                EnableTheming="False" ToolTip="بازگشت" ID="btnback2" EnableViewState="False"
                                                OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="CourseId" runat="server" Visible="False"></asp:HiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
