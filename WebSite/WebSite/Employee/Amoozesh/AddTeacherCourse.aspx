<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddTeacherCourse.aspx.cs" Inherits="Employee_Amoozesh_AddTeacherCourse" Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true"
                dir="rtl">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" UseSubmitBehavior="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/save.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Back.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <table style="width: 100%">
                <tbody>
                    <tr>
                        <td style="width: 100%" dir="rtl" valign="top" align="right">
                            <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server" 
                                 ItemSpacing="0px" SeparatorWidth="1px" Enabled="False" 
                                OnItemClick="MenuTeacherInfo_ItemClick" AutoSeparators="RootOnly" SeparatorColor="#A5A6A8"
                                SeparatorHeight="100%">
                                <Items>
                                    <dxm:MenuItem Text="مشخصات فردی" Name="BasicInfo">
                                    </dxm:MenuItem>
                                    <dxm:MenuItem Text="سوابق آموزشی" Name="Job">
                                    </dxm:MenuItem>
                                    <dxm:MenuItem Text="تالیفات و تحقیقات" Name="Research">
                                    </dxm:MenuItem>
                                </Items>
                                <RootItemSubMenuOffset X="-1" LastItemY="-2" LastItemX="-1" FirstItemY="-2" FirstItemX="-1"
                                    Y="-2"></RootItemSubMenuOffset>
                                <Border BorderWidth="1px" BorderStyle="Solid" BorderColor="#A5A6A8"></Border>
                                <VerticalPopOutImage Height="8px" Width="4px"></VerticalPopOutImage>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" ImageSpacing="5px" PopOutImageSpacing="7px">
                                    <CheckedStyle BackColor="#80FFFF" ForeColor="#400040">
                                    </CheckedStyle>
                                </ItemStyle>
                                <SubMenuItemStyle ImageSpacing="7px">
                                </SubMenuItemStyle>
                                <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1"></SubMenuStyle>
                                <HorizontalPopOutImage Height="7px" Width="7px"></HorizontalPopOutImage>
                            </TSPControls:CustomAspxMenuHorizontal>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherLicence" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; width: 64px;" align="right">
                                        <asp:Label runat="server" Text="درس" ID="Label1"></asp:Label>
                                    </td>
                                    <td align="right" colspan="3" style="vertical-align: top">
                                        <TSPControls:CustomAspxComboBox ID="cmbCourse" runat="server" 
                                             DataSourceID="ObjdsCourse" 
                                            ValueType="System.String">
                                            <ValidationSettings>
                                                
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 64px;" align="right">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label41"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top;" colspan="3" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="352px" ID="txtDescription"
                                            ClientInstanceName="txtDescription" >
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanel Visible="False" ID="RoundPanelJudge" HeaderText="نظر کارشناسی" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top;" align="right">
                                        <asp:Label runat="server" Text="شماره جلسه" Width="78px" ID="Label9"></asp:Label>
                                    </td>
                                    <td>
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="68px" ID="txtMeeting" >
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;" align="right">
                                        <asp:Label runat="server" Text="نظر کارشناسی" Width="90px" ID="Label8"></asp:Label>
                                    </td>
                                    <td>
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="352px" ID="txtJudgeView"
                                            >
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" dir="rtl" style="vertical-align: top" align="right">
                                        <dxe:ASPxRadioButtonList runat="server" Width="172px" ID="rbtnGrade">
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="مورد تایید نمی باشد"></dxe:ListEditItem>
                                            </Items>
                                            <Border BorderStyle="None" BorderWidth="0px" />
                                            <BorderRight BorderStyle="None" BorderWidth="0px" />
                                            <BorderLeft BorderStyle="None" BorderWidth="0px" />
                                            <BorderTop BorderStyle="None" BorderWidth="0px" />
                                            <BorderBottom BorderStyle="None" BorderWidth="0px" />
                                        </dxe:ASPxRadioButtonList>
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



                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/save.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/Back.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTecherLicence" ClientInstanceName="HiddenFieldTecherLicence">
            </dxhf:ASPxHiddenField>

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjdsCourse" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>

</asp:Content>

