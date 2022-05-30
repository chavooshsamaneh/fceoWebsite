<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeShowInfo.aspx.cs" Inherits="Members_Office_OfficeShowInfo"
    Title="مشخصات شرکت" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
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
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" 
                         AutoSeparators="RootOnly" 
                        ItemSpacing="0px" OnItemClick="ASPxMenu1_ItemClick" >
                       
                        <Items>
                            <dxm:MenuItem Name="Office" Text="مشخصات شرکت" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضای شرکت">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                        </Items>
                       
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" ClientInstanceName="RoundPanel1"
                    HeaderText="" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table style="text-align: right" dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <asp:Label runat="server" Text="نام شرکت" ID="Labe59"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" colspan="3">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfName" 
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام شرکت را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="نام شرکت(انگلیسی)" ID="Label67"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" colspan="3">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfNameEn"
                                                ClientInstanceName="txt1"  ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام شرکت را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="موضوع شرکت" ID="Label61"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" colspan="3">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfSubject"
                                                 ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="موضوع شرکت را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <asp:Label runat="server" Text="نوع شرکت" ID="Label60"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                TextField="OtName" ID="drdOfType"  DataSourceID="OdbOfType"
                                                ValueType="System.String" ValueField="OtId"  
                                                ReadOnly="True" RightToLeft="True" Width="100%">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نوع شرکت را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <asp:Label runat="server" Text="شماره ثبت شرکت" ID="Label62"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfRegNo" 
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="شماره ثبت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="محل ثبت شرکت" ID="Label63"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfRegPlace"
                                                 ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="محل ثبت شرکت را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="تاریخ ثبت شرکت" ID="Label64"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtOfRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="سرمایه شرکت" ID="Label65"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfValue" 
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="سرمایه شرکت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,11}">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="تعداد سهام شرکت" ID="Label66"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfStock" 
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="تعداد سهام شرکت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,11}">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="نوع فعالیت " ID="Label68" Visible="False"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxComboBox runat="server" Visible="False"   TextField="OatName" ID="aspxcmbAttype"
                                                 DataSourceID="OdbOfAtType" Width="100%" ValueType="System.String"
                                                ValueField="OatId" 
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                 
                                                    <RequiredField IsRequired="True" ErrorText="نوع فعالیت را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="آدرس شرکت" ID="Labe76"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="100%" ID="txtOfAddress"
                                                 ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="آدرس شرکت را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="شماره تلفن 1" ID="Labe69"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td align="right" valign="top" width="70%">
                                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="8" ID="txtOfTel1"
                                                                 ReadOnly="True">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}">
                                                                    </RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td align="right" valign="top" width="5%">
                                                            <asp:Label runat="server" Text="-" ID="Labe71"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top" width="25%">
                                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="4" ID="txtOfTel1_pre"
                                                                 ReadOnly="True">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RequiredField ErrorText="پیش شماره تلفن را وارد نمایید"></RequiredField>
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}">
                                                                    </RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="شماره تلفن 2" ID="Label70"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td align="right" valign="top" width="70%">
                                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="8" ID="txtOfTel2"
                                                                 ReadOnly="True">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}">
                                                                    </RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td align="right" valign="top" width="5%">
                                                            <asp:Label runat="server" Text="-" ID="Label72"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top" width="25%">
                                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="4" ID="txtOfTel2_pre"
                                                                 ReadOnly="True">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}">
                                                                    </RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="شماره فکس" ID="Labe73"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td align="right" valign="top" width="70%">
                                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="8" ID="txtOfFax"
                                                                 ReadOnly="True">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}">
                                                                    </RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td align="right" valign="top" width="5%">
                                                            <asp:Label runat="server" Text="-" ID="Labe74"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top" width="25%">
                                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="4" ID="txtOfFax_pre"
                                                                 ReadOnly="True">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}">
                                                                    </RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="شماره همراه" ID="Label75"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="11" ID="txtOfMobile"
                                                 ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{1,10}">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="آدرس وب سایت" ID="Labe77"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfWebsite"
                                                 ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="آدرس پست الکترونیکی" ID="Labe82"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtOfEmail" 
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="False" ErrorText="آدرس پست الکترونیکی را وارد نمایید">
                                                    </RequiredField>
                                                    <RegularExpression ErrorText=" آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="توضیحات" ID="Label81"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="100%" ID="txtOfDesc"
                                                 ReadOnly="True">
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="تصویر آرم شرکت" ID="Label79"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            &nbsp;<dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgOfArm" ClientInstanceName="ImageArm">
                                                <EmptyImage Url="~/Images/noimage.gif" />
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="تصویر مهر شرکت" ID="Label80"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgOfSign" ClientInstanceName="ImageSign">
                                                <EmptyImage Url="~/Images/noimage.gif" />
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
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
                <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
                <asp:ObjectDataSource ID="OdbOfType" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="TSP.DataManager.OfficeTypeManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbOfAtType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.OfficeActivityTypeManager">
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBMrsId" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MembershipRegistrationStatusManager"
                    CacheDuration="30" EnableCaching="True"></asp:ObjectDataSource>
                </div>
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
    </div>
</asp:Content>
