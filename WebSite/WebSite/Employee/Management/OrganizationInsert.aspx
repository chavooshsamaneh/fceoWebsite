<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="OrganizationInsert.aspx.cs" Inherits="Employee_Management_OrganizationInsert" Title="مشخصات سازمان" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/save.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
                <br />
               	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

        
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام سازمان" ID="ASPxLabel1"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtOrgName"  Width="340px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="نام سازمان را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام سازمان(انگليسى)" Width="120px" ID="ASPxLabel4"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtOrgNameEn"  Width="340px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText=""></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام مدير سازمان" ID="ASPxLabel2"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtManagerName"  Width="170px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="نام مدير سازمان را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right"></td>
                                            <td valign="top" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تلفن" ID="ASPxLabel3"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTel"  Width="170px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="تلفن را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="شماره همراه" ID="ASPxLabel5"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMobileNo"  Width="170px" MaxLength="11" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="شماره همراه را وارد نمایید"></RequiredField>

                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{1,10}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="آدرس وب سايت" Width="107px" ID="ASPxLabel7"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtWebsite"  Width="170px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکى" Width="115px" ID="ASPxLabel8"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEmail"  Width="170px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این آدرس صحیح نیست" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="کدپستی" ID="ASPxLabel12">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtPostalCode" Style="direction: ltr"  Width="170px" MaxLength="20" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="" />
                                                        <RegularExpression ErrorText="کدپستی را با فرمت صحیح وارد نمایید" ValidationExpression="\d{10}" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="صندوق پستی" ID="ASPxLabel11">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtOrgPO" Style="direction: ltr"  Width="170px" MaxLength="20" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="" />
                                                        <RegularExpression ErrorText="" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="فكس" ID="ASPxLabel6"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFax"  Width="170px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText=""></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">&nbsp;</td>
                                            <td valign="top" align="right">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel9"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtAddress"  Width="530px" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="آدرس را وارد نماييد"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضيحات" ID="ASPxLabel10"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtDesc"  Width="530px" ></TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel> <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/save.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
                <asp:HiddenField ID="OrganizationId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
  
</asp:Content>

