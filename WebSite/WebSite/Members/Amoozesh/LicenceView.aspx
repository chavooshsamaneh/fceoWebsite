<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="LicenceView.aspx.cs" Inherits="Members_Amoozesh_LicenceView" Title="مشخصات مدرک" %>


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

    <script language="javascript">
        function contentPageLoad(sender, args) {
            if (cmb.GetSelectedIndex() == 0)
                SetPeriod();
            else
                SetSeminar();
        }

        function SetPeriod() {

            document.getElementById('tr6').style.display = 'none';
            document.getElementById('tr7').style.display = 'none';
            document.getElementById('tr9').style.display = 'none';

            document.getElementById('tr5').style.display = 'inline';
            document.getElementById('tr10').style.display = 'inline';
            document.getElementById('tr11').style.display = 'inline';
            document.getElementById('tr13').style.display = 'inline';
            document.getElementById('tr14').style.display = 'inline';

        }
        function SetSeminar() {

            document.getElementById('tr6').style.display = 'inline';
            document.getElementById('tr7').style.display = 'inline';
            document.getElementById('tr9').style.display = 'inline';

            document.getElementById('tr5').style.display = 'none';
            document.getElementById('tr10').style.display = 'none';
            document.getElementById('tr11').style.display = 'none';
            document.getElementById('tr13').style.display = 'none';
            document.getElementById('tr14').style.display = 'none';
        }

    </script>

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table >
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </hoverstyle>

                                            <image url="~/Images/icons/Back.png"></image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table id="tbl" dir="rtl" width="100%">
                            <tbody>
                                <tr id="tr1">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeId" ClientInstanceName="lblMe"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" AutoPostBack="True" ID="txtMeNo" ClientInstanceName="ID" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText=" کد عضویت را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr id="tr2">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="lblMeFirstName" ClientInstanceName="lblMname"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ReadOnly="True" ID="txtMeFirstName" ClientInstanceName="mFirstName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblMeLastName" ClientInstanceName="lblMfamily"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ReadOnly="True" ID="txtMeLastName" ClientInstanceName="mLastName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="tr3">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع" ID="ASPxLabel5"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="150px" ID="ComboType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="cmb" ReadOnly="True">
                                            <Items>
                                                <dxe:ListEditItem Text="دوره" Value="0" Selected="True" />
                                                <dxe:ListEditItem Text="سمینار" Value="1" />
                                            </Items>

                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="نوع مدرک را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    
}" />
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع مدرک" ID="ASPxLabel2"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="150px" ID="ComboMdType" ValueType="System.String" ClientInstanceName="mdType" ReadOnly="True">
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="اولیه"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="انتقالی"></dxe:ListEditItem>
                                            </Items>

                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="نوع مدرک را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr id="tr4">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره گواهینامه" Width="95px" ID="ASPxLabel8"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" Style="direction: ltr" ID="txtLiNo" ClientInstanceName="LiNo" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ گواهینامه" ID="ASPxLabel6"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="165px" ShowPickerOnTop="True" ID="txtLiDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr id="tr5">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="عنوان" ID="ASPxLabel7"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="CrsTitle" ID="ComboCrsName" DataSourceID="odbCourseName" ValueType="System.Int32" ValueField="CrsId" Width="150px" ClientInstanceName="crsId" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">


                                                <RequiredField IsRequired="True" ErrorText="عنوان مدرک را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد دوره" ID="ASPxLabel9"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ID="txtPPCode" ClientInstanceName="PPCode" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="tr6">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="عنوان سمینار" ID="ASPxLabel21"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox runat="server" Width="413px" ID="txtSeName" ClientInstanceName="SeName" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="عنوان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="tr7">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="ارائه دهنده" ID="ASPxLabel23"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox runat="server" Width="413px" ID="txtSeTeName" ClientInstanceName="SeTeName" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ارائه دهنده را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="tr8">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel10"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="165px" ShowPickerOnTop="True" ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان" ID="ASPxLabel12"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="165px" ShowPickerOnTop="True" ID="txtEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr id="tr9">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مدت زمان سمینار(ساعت)" ID="ASPxLabel22" Width="136px"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ID="txtSeDuration" ClientInstanceName="SeDuration" MaxLength="6" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مدت زمان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr id="tr10">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="امتیاز اخذ شده(ساعت)" Width="122px" ID="ASPxLabel24"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ID="txtTimeMark" ClientInstanceName="TimeMark" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="امتیاز کل" ID="ASPxLabel25"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ID="txtTotalMark" ClientInstanceName="TotalMark" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="امتیاز کل را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="tr11">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مدت زمان دوره(ساعت)" ID="ASPxLabel11"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ID="txtPPDuration" ClientInstanceName="PPDuration" MaxLength="6" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مدت زمان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ آزمون" ID="ASPxLabel20"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="165px" ShowPickerOnTop="True" ID="txtTestDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr id="tr12">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام مؤسسه" ID="ASPxLabel13"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ID="txtInsName" ClientInstanceName="InsName" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مؤسسه را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="استان" ID="ASPxLabel16"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="150px" ID="ComboPrId" ValueType="System.String" DataSourceID="OdbProvince" TextField="PrName" ValueField="PrId" ClientInstanceName="PrId">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField ErrorText=""></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr id="tr13">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره مجوز فعالیت" Width="109px" ID="ASPxLabel14"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" Width="150px" ID="txtInsRegNo" ClientInstanceName="InsRegNo" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="مکان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ اخذ مجوز" ID="ASPxLabel15"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="165px" ShowPickerOnTop="True" ID="txtInsRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr id="tr14">
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مدرس دوره" ID="ASPxLabel17"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="150px" ID="txtPPTeName" ClientInstanceName="PPTeName" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مدرس را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="پروانه اشتغال به کار" Width="101px" ID="ASPxLabel18"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" Width="150px" ID="txtTeFileNo" ClientInstanceName="TeFileNo" ReadOnly="True">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="مکان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="tr15">
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label3"></asp:Label>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="32px" Width="500px" ID="txtDesc" ClientInstanceName="Desc" ReadOnly="True"></TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label ID="Label1" runat="server" Text="فایل"></asp:Label>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <dxe:ASPxHyperLink ID="HpFile" runat="server" ClientInstanceName="hp"
                                            Target="_blank" Text="آدرس فایل">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <asp:HiddenField ID="MadrakId" runat="server" Visible="False"></asp:HiddenField>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </hoverstyle>

                                            <image url="~/Images/icons/Back.png"></image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
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
    <asp:ObjectDataSource ID="odbCourseName" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbProvince" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.ProvinceManager" CacheDuration="3600">
        <FilterParameters>
            <asp:Parameter Name="EpId" />
        </FilterParameters>
    </asp:ObjectDataSource>
</asp:Content>


