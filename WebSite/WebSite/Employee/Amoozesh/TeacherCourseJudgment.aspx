<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="TeacherCourseJudgment.aspx.cs" Inherits="Employee_Amoozesh_AddTeacherCourse" Title="کارشناسی دروس مورد تقاضای استاد" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div id="DivReport" runat="server" class="DivErrors" dir="rtl" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#"><span style="color: #000000">ب</span>ستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; width: 100%; text-align: right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>

                                                                <Image  Url="~/Images/icons/save.png"></Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>

                                                                <Image  Url="~/Images/icons/Back.png"></Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherCourse" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; width: 100px; text-align: right">
                                            <dxe:ASPxLabel runat="server" Text="درس:" ID="ASPxLabel1"></dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; width: 100px; text-align: right">
                                            <div dir="ltr">
                                                <TSPControls:CustomAspxComboBox runat="server"  ID="cmbTeacherCourse"  DataSourceID="ObjdsCourse" ValueType="System.String"  Enabled="False" TextField="CrsName" ValueField="CrsId">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>

                                                    <ButtonStyle Width="13px"></ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </div>
                                        </td>
                                        <td style="vertical-align: top; width: 100px; text-align: right" dir="ltr"></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; width: 100px; text-align: right" dir="ltr">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel2"></dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; text-align: right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="477px" ID="txtTCrsDescription"  Enabled="False">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>


                
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right" dir="rtl" colspan="2">
                                                <dxe:ASPxRadioButtonList runat="server" ID="rdbtnIsConfirm">
                                                    <Items>
                                                        <dxe:ListEditItem Value="0" Text="مورد تایید نمی باشد"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                    </Items>
                                                    <Border BorderWidth="0px" />
                                                </dxe:ASPxRadioButtonList>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 100px; text-align: right" dir="rtl">
                                                <dxe:ASPxLabel runat="server" Text="شماره نامه:" ID="ASPxLabel3"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="115px" ID="txtMailNo" >
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
                                            <td style="vertical-align: top; width: 100px; text-align: right" dir="rtl">
                                                <dxe:ASPxLabel runat="server" Text="دلایل:" ID="ASPxLabel4"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="477px" ID="txtDescription" >
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
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



                                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; width: 100%; text-align: right">
                                                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeacherCourse"></dxhf:ASPxHiddenField>
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                    </HoverStyle>

                                                                    <Image  Url="~/Images/icons/save.png"></Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                    </HoverStyle>

                                                                    <Image  Url="~/Images/icons/Back.png"></Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>   
                <asp:ObjectDataSource ID="ObjdsCourse" runat="server" TypeName="TSP.DataManager.CourseManager" SelectMethod="GetData"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
 
</asp:Content>

