<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddTeacherSalary.aspx.cs" Inherits="Employee_Amoozesh_AddTeacherSalary"
    Title="مشخصات حق الزحمه اساتید" %>

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
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]<br />
                </div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
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
                	<TSPControls:CustomASPxRoundPanel ID="RoundPanelTeSalary" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                <table width="100%">
                                    <tbody>
                                        <tr>   <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ" Width="81px" ID="ASPxLabel5"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" Width="170px" ID="txtDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ClientValidationFunction="PersianDateValidator" ValidateEmptyText="True" ControlToValidate="txtDate" ErrorMessage="تاریخ را وارد نمایید" ID="PersianDateValidator1"></pdc:PersianDateValidator>
                                            </td> <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="مدرک تحصیلی" Width="81px" ID="ASPxLabel1"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsLicence" TextField="LiName" ValueField="LiId"    ID="cmbLicence">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                      
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                         
                                           
                                        </tr>
                                        <tr>  <td dir="rtl" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="حق الزحمه تئوری(ریال)" Width="91px" ID="ASPxLabel3"></dxe:ASPxLabel>
                                            </td>
                                            <td dir="rtl" valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="170px"   ID="txtNonPractical">
                                                    <MaskSettings Mask="&lt;0..999999999999g&gt;" ErrorText="حق الزحمه را با فرمت صحیح وارد نمایید"></MaskSettings>

                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>

                                                        <RequiredField IsRequired="True" ErrorText="فیلد را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                                <br />
                                            </td>  <td dir="rtl" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="حق الزحمه عملی(ریال)" Width="96px" ID="ASPxLabel4"></dxe:ASPxLabel>
                                            </td>
                                            <td dir="rtl" valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="170px"   ID="txtPractical">
                                                    <MaskSettings Mask="&lt;0..999999999999g&gt;" ErrorText="حق الزحمه را با فرمت صحیح وارد نمایید"></MaskSettings>

                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>

                                                        <RequiredField IsRequired="True" ErrorText="فیلد را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                          
                                          
                                        </tr>
                                        <tr>
                                           
                                            <td dir="rtl" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="حق الزحمه بازدید از کارگاه(ریال)" Width="106px" ID="ASPxLabel2"></dxe:ASPxLabel>
                                            </td> <td dir="rtl" valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="170px"   ID="txtWorkRoom">
                                                    <MaskSettings Mask="&lt;0..999999999999g&gt;" ErrorText="حق الزحمه را با فرمت صحیح وارد نمایید"></MaskSettings>

                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>

                                                        <RequiredField IsRequired="True" ErrorText="فیلد را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right"></td>
                                            <td valign="top" align="right"></td>
                                        </tr>
                                    </tbody>
                                </table>
                     
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>    
                <br />
                                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeSalary">
                                                    </dxhf:ASPxHiddenField>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                
                                </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
             
                <asp:ObjectDataSource ID="ObjdsLicence" runat="server" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager">
                   
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>

</asp:Content>
