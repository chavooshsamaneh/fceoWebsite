<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddPlanDesigner.aspx.cs" Inherits="Members_TechnicalServices_Project_AddPlanDesigner"
    Title="مشخصات طراح نقشه" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Src="~/UserControl/CapacityUserControl.ascx" TagPrefix="TSP" TagName="Capacity" %>

<%@ Register Src="~/UserControl/WorkRequestUserControl.ascx" TagPrefix="TSP" TagName="WorkRequestUserControl" %>
<%@ Register Src="~/UserControl/MeEngOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeEngOfficeInfoUserControl" %>
<%@ Register Src="~/UserControl/MeOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeOfficeInfoUserControlUserControl" %>
<%@ Register Src="~/UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetTaskOrderError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
                document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }


        function SetLbalesBsedOnMembershipType() {
            if (CmbMembershipType.GetSelectedIndex() == 0) {
                PanelDocumentFileNo.SetVisible(true);
                lbltxtMeIdSearchTitle.SetText('کد عضویت');
            }
            else if (CmbMembershipType.GetSelectedIndex() == 1) {
                PanelDocumentFileNo.SetVisible(false);
                lbltxtMeIdSearchTitle.SetText('كد عضويت كانون كاردان ها');
            }

        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>

                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton Visible="false" IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره" ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت نقشه ها" ToolTip="مدیریت نقشه ها"
                                            CausesValidation="False" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server" />

            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelDes" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <div class="Item-center">
                            <dxe:ASPxLabel runat="server" Text="وضعیت درخواست ثبت طراح و نقشه مربوطه: ----" Font-Bold="False" ID="lblWorkFlowState"
                                ForeColor="Red">
                            </dxe:ASPxLabel>
                        </div>
                        <ul class="HelpUL">
                            <li>اشخاص جهت طراحی نقشه بایستی عضو دفتر و یا شرکت باشند. </li>
                            <li>تنها اعضای دارای پروانه در رشته عمران می توانند بدون عضویت در دفتر و یا شرکت کار
                                                                                            طراحی معماری و زیر 600 متر انجام دهند. </li>
                        </ul>
                        <dxe:ASPxPanel runat="server" ID="RoundPanelSearch" Visible="false">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">مشخصات عضویت و آماده بکاری  طراح</legend>

                                        <fieldset>
                                            <legend class="HelpUL"></legend>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="right" colspan="4">
                                                            <dxe:ASPxLabel Font-Bold="true" ForeColor="DarkRed" Visible="false" runat="server"
                                                                Text="" Width="100%" ID="lblWarningsearchOfEngInfo">
                                                            </dxe:ASPxLabel>
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right" width="15%">نوع عضویت</td>
                                                        <td valign="top" align="right" width="35%">
                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                ID="CmbMembershipType" ClientInstanceName="CmbMembershipType" ValueType="System.String" SelectedIndex="0"
                                                                RightToLeft="True">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                                SetLbalesBsedOnMembershipType();

}"></ClientSideEvents>
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                    <RequiredField IsRequired="True" ErrorText="نوع عضویت را انتخاب نمایید"></RequiredField>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                                <Items>
                                                                    <dxe:ListEditItem Value="1" Text="عضو سازمان نظام مهندسی" Selected="True"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Value="4" Text="عضو نظام کاردان ها"></dxe:ListEditItem>
                                                                </Items>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>
                                                        </td>
                                                        <td valign="top" align="right" width="15%">
                                                            <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lbltxtMeIdSearchTitle" ClientInstanceName="lbltxtMeIdSearchTitle">
                                                            </dxe:ASPxLabel>
                                                            :
                                                        </td>
                                                        <td valign="top" align="right" width="35%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtMeIdSearch" AutoPostBack="true" Width="100%"
                                                                ClientInstanceName="txtMeIdSearch">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                    <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxcp:ASPxPanel ID="PanelDocumentFileNo" ClientInstanceName="PanelDocumentFileNo"
                                                runat="server">
                                                <PanelCollection>
                                                    <dxcp:PanelContent>
                                                        <table width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="top" align="right" width="15%">شماره پروانه:
                                                                    </td>
                                                                    <td valign="top" align="right" width="35%">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td width="5%">استان
                                                                                </td>
                                                                                <td align="right" width="17%">
                                                                                    <TSPControls:CustomTextBox runat="server" ID="txtDocProvCode" Width="100%"
                                                                                        ClientInstanceName="txtDocProvCode">
                                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                                            <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                            </ErrorFrameStyle>
                                                                                        </ValidationSettings>
                                                                                    </TSPControls:CustomTextBox>
                                                                                </td>
                                                                                <td width="5%">رشته
                                                                                </td>
                                                                                <td align="right" width="17%">
                                                                                    <TSPControls:CustomTextBox runat="server" ID="txtMjCode" Width="100%" ClientInstanceName="txtMjCode">
                                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                            </ErrorImage>
                                                                                            <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                            </ErrorFrameStyle>
                                                                                        </ValidationSettings>
                                                                                    </TSPControls:CustomTextBox>
                                                                                </td>
                                                                                <td width="5%">سریال
                                                                                </td>
                                                                                <td align="right" width="17%">
                                                                                    <TSPControls:CustomTextBox runat="server" ID="txtDocSerialNo" AutoPostBack="true" Width="100%"
                                                                                        ClientInstanceName="txtDocSerialNo">
                                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                                            <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                            </ErrorFrameStyle>
                                                                                        </ValidationSettings>


                                                                                        <ClientSideEvents TextChanged="function(s, e) {
if(txtDocProvCode.GetText()=='' || txtDocProvCode.GetText()==null ||txtMjCode.GetText()=='' || txtMjCode.GetText()==null)
{
alert('کد استان و رشته را به صورت کامل وارد نمایید');
e.ProsseccOnServer=false;
}
else
e.ProsseccOnServer=true;
}"></ClientSideEvents>
                                                                                    </TSPControls:CustomTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td valign="top" align="right" width="15%">
                                                                        <b><font color="red">یا</font></b>شماره پروانه:
                                                                    </td>
                                                                    <td valign="top" align="right" width="35%">
                                                                        <TSPControls:CustomTextBox runat="server" ID="txtSearchFileNo" Width="100%"
                                                                            ClientInstanceName="txtSearchFileNo" AutoPostBack="true"
                                                                            Style="direction: ltr">
                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                                <RegularExpression ErrorText="شماره پروانه به صورت *****-***-**  می باشد" ValidationExpression="\d{2}-\d{3}-\d{1,7}" />
                                                                            </ValidationSettings>

                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxcp:PanelContent>
                                                </PanelCollection>
                                            </dxcp:ASPxPanel>
                                        </fieldset>
                                        <TSP:WorkRequestUserControl runat="server" ID="WorkRequestUserControl" />
                                        <dxcp:ASPxPanel ID="RoundPanelMemberEngOfficeInfo" ClientInstanceName="RoundPanelMemberEngOfficeInfo"
                                            runat="server">
                                            <PanelCollection>
                                                <dxcp:PanelContent>
                                                    <TSP:MeEngOfficeInfoUserControl runat="server" ID="UserControlMeEngOfficeInfoUserControl" />
                                                    <TSP:MeOfficeInfoUserControlUserControl runat="server" ID="UserControlMeOfficeInfoUserControl" />
                                                </dxcp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxPanel>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxPanel>
                        <fieldset>
                            <legend class="HelpUL" id="ASPxRoundPanel2" runat="server">مشخصات تعرفه</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع نقشه" ID="lblcmbPlanType">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox Enabled="false" runat="server" Width="100%"
                                                TextField="Title" ID="cmbPlanType" DataSourceID="ObjdsPlansType"
                                                ValueType="System.String" ValueField="PlansTypeId" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="نوع نقشه را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع طراح" ID="ASPxLabel9" Visible="false">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox Enabled="false" runat="server"
                                                TextField="Title" ID="cmbDesMeType" DataSourceID="ObjectDataSourceMemberType"
                                                ValueType="System.String" ValueField="MemberTypeId" ClientInstanceName="cmbMeType"
                                                Width="100%" RightToLeft="True" Visible="false">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نوع طراح را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSourceMemberType" runat="server" TypeName="TSP.DataManager.TechnicalServices.MemberTypeManager"
                                                SelectMethod="GetData" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                        </td>
                                    </tr>

                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="تعرفه خدمات مهندسی *" ID="lblPriceArchive" ClientInstanceName="lblPriceArchive">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="YearName" ID="cmbPriceArchive" DataSourceID="ObjectDataSource_PriceArchive"
                                            ValueType="System.String" ValueField="PriceArchiveId" ClientInstanceName="cmbPriceArchive"
                                            Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="تعرفه را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectDataSource_PriceArchive" runat="server" TypeName="TSP.DataManager.TechnicalServices.PriceArchiveManager"
                                            SelectMethod="SelectActivePriceArchive"></asp:ObjectDataSource>
                                    </td>
                                    <td>سال کاری</td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" DataSourceID="ObjectCapacityAssignment"
                                            TextField="Year" ValueField="Year" ID="comboYear"
                                            ValueType="System.String" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="سال کاری را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectCapacityAssignment" runat="server" TypeName="TSP.DataManager.TechnicalServices.CapacityAssignmentManager"
                                            SelectMethod="SelectTSCapacityAssignmentYears">
                                            <SelectParameters>
                                                <asp:Parameter DbType="Int16" DefaultValue="-1" Name="IsMainAgent" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                    </tr>
                                      <tr>
                                          <td valign="top" align="right" colspan="4">
                                              <TSPControls:CustomASPxCheckBox Visible="false" runat="server" Text="نماینده طراحان می باشد"
                                                  Wrap="False" ID="chbIsMaster">
                                              </TSPControls:CustomASPxCheckBox>
                                          </td>
                                      </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="ثبت کارکرد به دلیل اضافه اشکوب می باشد"
                                                Wrap="False" ID="ChbIsExteraFloor" Visible="false">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <TSP:Capacity ID="CapacityUserControl" runat="server" />
                        <fieldset>
                            <legend class="HelpUL">اطلاعات نقشه</legend>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 115px" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد پیگیری" ID="ASPxLabel29">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" ID="txtFollowCode">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع نقشه" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Title" ID="cmbPlanType2" DataSourceID="ObjdsPlansType" ValueType="System.String"
                                                ValueField="PlansTypeId" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نوع نقشه را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره نقشه" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPlanNo" Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره نقشه را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtPlanDes" Width="100%">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">

                                            <fieldset>
                                                <legend class="HelpUL">فایل پیوست</legend>
                                                <ul class="HelpUL" runat="server" id="ArchNotification" visible="false">
                                                    <p><b>در خصوص طراحی نقشه های معماری رعایت موارد ذیل الزامی است</b></p>
                                                    <li>كليه موارد مربوط به مقررات ملي ساختمان واصول شهرسـازي ودستورنقشه رعایت شده باشد.</li>
                                                    <li>نقشه پيوسـت شامـل سايت پلان، پلان طبقات، بـرش به تعداد موردنياز پروژه، نماهـا، پلان تيرريزي همراه با محل بادبند يا ديواربرشي باشد.</li>
                                                    <li>رعايت ضوابط ومقررات شهرسازي پروژه الزامي مي باشد و مسئوليت هرگونه مغايرت با مهندس طراح معمار مي باشد.</li>
                                                    <li>رعايت حداقل مساحت نورگيرها الزامي است.</li>
                                                    <li>رعايت مباحث مقررات ملي ساختمان الزامي است.</li>
                                                    <li>رعايت ضوابط و مقررات مناسب سازي فضا براي معلولين الزامي است.</li>
                                                    <li>ارائه نقشه برداري تاييد شده توسط مهندس نقشه بردار جهت زمين هاي شيب دار الزامي است.</li>
                                                    <li>رعايت درز انقطاع با سازه هاي مجاور الزامي است.</li>
                                                    <li>محل ديوارهاي برشي يا بادبندها مناسب باشد.</li>
                                                    <li>ابعاد ستونها براي پاركينگ مزاحمت نداشته باشد.</li>
                                                    <li>محل وابعاد داكتهاي تاسيساتي ومسير عبور كولر مناسب باشد.</li>
                                                </ul>
                                                <ul class="HelpUL" runat="server" id="TasisatNotification">
                                                    <p><b>در خصوص طراحی نقشه های تاسیسات برقی و مکانیکی رعایت موارد ذیل الزامی است</b></p>
                                                    <li>نوع سیستم گرمایشی و سرمایشی و نوع اسکلت و سقف و نوع کاربری ساختمان به همراه متراژ و تعداد طبقات در فرم مربوطه قید شده باشد.</li>
                                                    <li>نقشه ها با مقیاس یک صدم باشد.</li>
                                                    <li>نقشه های معماری تایید شده ضمیمه شده باشد.</li>
                                                    <li>جهت شمال و قبله در نقشه ها مشخص باشد.</li>
                                                    <li>نقشه ها با محورهای طولی وعرضی طرح شده باشد.</li>
                                                    <li>عنوان در نقشه ها مشخص باشد.</li>
                                                    <li>پلان تیرریزی سقف (سازه ای) ضمیمه شده باشد.</li>
                                                    <li>درصورت استفاده از تجهیزات و سیستم های تاسیساتی ارائه چیدمان و جانمایی با رعایت مقیاس ارائه شود.</li>
                                                    <li>پیش بینی فضای منبع آب ذخیره در مجتمع های آپارتمانی شده باشد.</li>
                                                </ul>
                                                <p class="HelpUL" runat="server" id="AllowedFileExt"></p>
                                                <table width="100%" runat="server" id="TblPlanAttachmentAddInfo">
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="4" width="100%">
                                                                <table dir="rtl" width="100%">
                                                                    <tr>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <dxe:ASPxLabel runat="server" Text="نوع فایل پیوست" ID="ASPxLabel7">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                                ID="cmbAttachType" DataSourceID="ObjdsAttachType" TextField="Title" ValueField="AttachTypeId" ValueType="System.String" RightToLeft="True">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <ValidationSettings Display="Dynamic" ValidationGroup="ValidAttach" ErrorTextPosition="bottom">

                                                                                    <RequiredField IsRequired="True" ErrorText="نوع فایل را انتخاب نمایید"></RequiredField>

                                                                                </ValidationSettings>
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn FieldName="Title" Caption="عنوان" Width="30%" />
                                                                                    <dxe:ListBoxColumn FieldName="AllowedFileExtensions" Caption="فرمت مجاز" Width="70%" />
                                                                                </Columns>
                                                                            </TSPControls:CustomAspxComboBox>
                                                                        </td>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel10">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <table dir="rtl" runat="server" id="TblTrFileUplode">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td valign="top" align="right">
                                                                                            <TSPControls:CustomAspxUploadControl runat="server" Width="258px" ShowProgressPanel="True"
                                                                                                MaxSizeForUploadFile="100000000" UploadWhenFileChoosed="True" ID="flpFile" InputType="Files"
                                                                                                ClientInstanceName="flp" OnFileUploadComplete="flpFile_FileUploadComplete">
                                                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
  if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
	HDflp.Set('name',1);
	lbl1.SetVisible(false);
	HyperLinkFile.SetVisible(true);
	HyperLinkFile.SetNavigateUrl('../../../Image/TechnicalServices/Plans/'+e.callbackData);
	}
	else{
	imgEndUploadImgClient.SetVisible(false);
	HDflp.Set('name',0);
	lbl1.SetVisible(true);
	HyperLinkControlerFile.SetVisible(false);
	HyperLinkFile.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                                                            </TSPControls:CustomAspxUploadControl>
                                                                                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                                                ID="ASPxLabel11" ForeColor="Red" ClientInstanceName="lbl1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                                ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" ClientVisible="False" Target="_blank"
                                                                                ID="HyperLinkFile" NavigateUrl='<%# Bind("FilePath") %>' ClientInstanceName="HyperLinkFile">
                                                                            </dxe:ASPxHyperLink>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle" align="center" colspan="4">
                                                                <div class="Item-center">
                                                                    <br />
                                                                    <TSPControls:CustomAspxButton runat="server" CssClass="ButtonMenue" Text="&nbsp;&nbsp;اضافه به لیست"
                                                                        CausesValidation="true" ValidationGroup="ValidAttach" ID="btnSaveAttachment"
                                                                        UseSubmitBehavior="False" OnClick="btnSaveAttachment_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
    if( confirm('آیا مطمئن به افزودن این اطلاعات به لیست زیر هستید؟ در صورت افزودن اطلاعات قادر به حذف یا ویرایش نیستید'))
    {
	if(HDflp.Get('name')!=1)
	{
		lbl1.SetVisible(true);
		e.processOnServer=false;
	}
	else
	{
		HDflp.Set('name',0);
	}
    }
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxButton>
                                                                    <br />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                    ID="GridViewAttachment" KeyFieldName="AttachmentId" AutoGenerateColumns="False" OnPageIndexChanged="GridViewAttachment_PageIndexChanged"
                                                    ClientInstanceName="GridViewAttachment">
                                                    <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AttachmentId"
                                                            Caption="AttachmentId" Name="AttachmentId">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="Title" Caption="نوع فایل">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="200px" FieldName="FileName"
                                                            Caption="نام فایل">
                                                        </dxwgv:GridViewDataTextColumn>

                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CreateDate" Caption="تاریخ ثبت">
                                                            <CellStyle Wrap="False" HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>


                                                        <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                            FieldName="FilePath" Caption="لینک" Name="PlanFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                                        </dxwgv:GridViewDataHyperLinkColumn>
                                                    </Columns>
                                                </TSPControls:CustomAspxDevGridView2>
                                            </fieldset>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <fieldset runat="server" id="PanelControlerVoiewPoint">
                            <legend class="HelpUL">نواقص نقشه</legend>
                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                ID="GridViewViewPoint" KeyFieldName="ViewPointId" AutoGenerateColumns="False"
                                ClientInstanceName="GridViewDesigner" DataSourceID="ObjectDataSourceViewPoint">
                                <SettingsBehavior ColumnResizeMode="Control" />
                                <Settings ShowHorizontalScrollBar="true"></Settings>
                                <SettingsCookies Enabled="false" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RowNo" Width="50px" Caption="ردیف">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Subject" Caption="موضوع"
                                        Width="150px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="SheetNo" Caption="شماره برگ نقشه"
                                        Width="100px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                        FieldName="FileUrl" Caption="لینک" Name="FileUrl" PropertiesHyperLinkEdit-Target="_blank">
                                    </dxwgv:GridViewDataHyperLinkColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ViewPoint" Width="400px"
                                        Caption="توضیحات بازبینی">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Date" Caption="تاریخ ثبت">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>


                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id" Caption="Id">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="4" Caption=" " ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>
                        <dxhf:ASPxHiddenField ID="HiddenFieldPrjDes" runat="server" ClientInstanceName="HiddenFieldPrjDes">
                        </dxhf:ASPxHiddenField>
                        <dxhf:ASPxHiddenField ID="HD_Flp" runat="server" ClientInstanceName="HDflp">
                        </dxhf:ASPxHiddenField>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TspUserControl:EPaymentUserControl ID="EPaymentUC" runat="server" Visible="false" />
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>

                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton Visible="false" IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top" dir="ltr">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره" ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                        </TSPControls:CustomAspxButton>

                                    </td>

                                    <td style="vertical-align: top">

                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت نقشه ها" ToolTip="مدیریت نقشه ها"
                                            CausesValidation="False" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
    <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsAttachType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.AttachTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceViewPoint" runat="server" SelectMethod="FindByPlansId" TypeName="TSP.DataManager.TechnicalServices.PlansControlerViewPointManager">
        <SelectParameters >
            <asp:Parameter DefaultValue="-2" Name="PlansId" DbType="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
