<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddTestCondition.aspx.cs" Inherits="Employee_Document_AddTestCondition"
    Title="مشخصات شرایط آزمون" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click" CausesValidation="true">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelTestCondition" HeaderText="مشاهده"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="عنوان *" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" ID="txtTitle" runat="server" Width="100%"
                                            MaxLength="63">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="true" ErrorText="عنوان وارد نشده است" />
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="رشته آزمون *" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="MjName"
                                            ID="cmbMajor" AutoPostBack="True" DataSourceID="ObjdsMajor"
                                            RightToLeft="True" ValueType="System.String" ValueField="MjId"
                                            OnSelectedIndexChanged="cmbMajor_SelectedIndexChanged" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="true" ErrorText="رشته را انتخاب نمایید" />
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار آزمون *" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                            ID="txtExamValidDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="اعتبار آزمون را وارد نمایید." ControlToValidate="txtExamValidDate"
                                            Font-Size="XX-Small" ID="PersianDateValidator3"></pdc:PersianDateValidator>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مهلت تکمیل سوابق *" Width="100%" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td avalign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                            ID="txtExpireDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="مهلت تکمیل سوابق را وارد نمایید." ControlToValidate="txtExpireDate"
                                            Width="163px" Font-Size="XX-Small" ID="PersianDateValidator1"></pdc:PersianDateValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo ID="txtDescription" runat="server" Height="37px" Width="100%">
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
                            </tbody>
                        </table>

                        <fieldset id="RoundPanelDetails"
                            runat="server">
                            <legend class="HelpUL">زمینه آزمون</legend>
                            <asp:Panel runat="server" ID="PanelTestConditionDetails">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="زمینه آزمون *" ID="ASPxLabel7">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server" TextField="TTypeName"
                                                    ID="cmbTestType" DataSourceID="ObjdsTestType" ValueType="System.String"
                                                    ValueField="TTypeId" RightToLeft="True" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                                        ValidationGroup="TestCondition">
                                                        <RequiredField IsRequired="true" ErrorText="زمینه آزمون را انتخاب نمایید" />
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="پایه *" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server" TextField="GrdName"
                                                    ID="cmbGrade" DataSourceID="ObjdsGrade" ValueType="System.String"
                                                    ValueField="GrdId" RightToLeft="True" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                                        ValidationGroup="TestCondition">
                                                        <RequiredField IsRequired="true" ErrorText="پایه را انتخاب نمایید" />
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="حد کسری نمره *" Width="100%" ID="ASPxLabel5">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtMaxDeductionGrade">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="TestCondition">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="حد کسری نمره را وارد نمایید."></RequiredField>
                                                        <RegularExpression ErrorText="حد کسری نمره را با فرمت صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نمره قبولی *" Width="100%" ID="ASPxLabel4">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtAcceptGrade">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="TestCondition">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نمره قبولی را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="نمره قبولی را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ آزمون *" Width="100%" ID="ASPxLabel12">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                                    ShowPickerOnEvent="OnClick" ID="txtTestDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                    Style="direction: ltr"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ آزمون را وارد نمایید." ControlToValidate="txtTestDate" ID="PersianDateValidator2"
                                                    ValidationGroup="TestCondition">تاریخ آزمون را وارد نمایید</pdc:PersianDateValidator>
                                            </td>
                                            <td></td>
                                            <td>
                                                <TSPControls:CustomASPxCheckBox ID="checkboxNeedFileUpload" ClientInstanceName="checkboxNeedFileUpload" runat="server" Text="بارگذاری تصویر دوره برای عضو الزامی می باشد">
                                                </TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td valign="top" align="center" colspan="4">
                                                <div class="Item-center">
                                                    <TSPControls:CustomAspxButton runat="server" CssClass="ButtonMenue" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست"
                                                        ID="btnAddDetail" UseSubmitBehavior="False"
                                                        OnClick="btnAddDetail_Click" CausesValidation="true" ValidationGroup="TestCondition">
                                                        <ClientSideEvents Click="function(s,e){
                                                        if (ASPxClientEdit.ValidateGroup('TestCondition') == false)
                                                        e.processOnServer=false;
                                                        }" />
                                                    </TSPControls:CustomAspxButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <br />
                            <TSPControls:CustomAspxDevGridView runat="server" ID="GridViewTestCondition"
                                KeyFieldName="Id" AutoGenerateColumns="False" ClientInstanceName="GridViewTestCondition"
                                OnRowDeleting="GridViewTestCondition_RowDeleting"
                                Width="100%">
                                <Settings ShowHorizontalScrollBar="true" />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="50px"
                                        Name="Delete">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MaxDeductionGrade" Caption="حد کسر نمره"
                                        Name="MaxDeductionGrade">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="AcceptGrade" Caption="نمره قبولی"
                                        Name="AcceptGrade">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="GrdName" Caption="پایه"
                                        Name="GrdName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="TTypeName" Width="150px"
                                        Caption="نوع آزمون" Name="TTypeName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="TestDate" Width="150px"
                                        Caption="تاریخ آزمون" Name="TestDate">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataCheckColumn FieldName="NeedFileUpload" Caption="اجباری بودن بارگذاری تصویر دوره"
                                        Name="NeedFileUpload" VisibleIndex="0">
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataCheckColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="Id" Caption="Id">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>

                        <fieldset id="RoundPanelResponsibilitiesForDocument"
                            runat="server">
                            <legend class="HelpUL">بخش های الزامی آزمون جهت اخذ پروانه </legend>

                            <asp:Panel runat="server" ID="PanelInputTestConditionResponsibility">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="زمینه آزمون *" ID="ASPxLabel10">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server" TextField="TTypeName"
                                                    ClientInstanceName="cmbDocTestType" ID="cmbDocTestType" DataSourceID="ObjdsTestType"
                                                    ValueType="System.String" ValueField="TTypeId"
                                                    RightToLeft="True" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                                        ValidationGroup="TestConditionResponsibility">
                                                        <RequiredField IsRequired="true" ErrorText="زمینه آزمون را انتخاب نمایید" />
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="پایه - صلاحیت *" ID="ASPxLabel11">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server" TextField="GrdResName"
                                                    ID="cmbAcceptedGrade" DataSourceID="objDocAcceptedGrade" ValueType="System.String"
                                                    ValueField="GMRId" RightToLeft="True"
                                                    ClientInstanceName="cmbAcceptedGrade" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                                        ValidationGroup="TestConditionResponsibility">
                                                        <RequiredField IsRequired="true" ErrorText="پایه - صلاحیت را انتخاب نمایید" />
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" colspan="4">

                                                <div class="Item-center">
                                                    <TSPControls:CustomAspxButton runat="server" CssClass="ButtonMenue" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست"
                                                        ID="btnAddTestConditionResponsibility" UseSubmitBehavior="False" ClientInstanceName="btnAddTestConditionResponsibility"
                                                        AutoPostBack="false" CausesValidation="false">
                                                        <ClientSideEvents Click="function(s, e) {
    if (ASPxClientEdit.ValidateGroup('TestConditionResponsibility') == false)
        return;
    grdTestConditionResponsibility.PerformCallback('Insert$' + cmbDocTestType.GetValue() + '$' +cmbDocTestType.GetText() + '$' + cmbAcceptedGrade.GetValue() + '$' +cmbAcceptedGrade.GetText() );

}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <br />
                            <TSPControls:CustomAspxDevGridView runat="server" ID="grdTestConditionResponsibility"
                                Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" ClientInstanceName="grdTestConditionResponsibility"
                                OnCustomCallback="grdTestConditionResponsibility_CustomCallback" OnRowDeleting="grdTestConditionResponsibility_RowDeleting">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="25px"
                                        Name="Delete">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="GrdResName" Caption="پایه - صلاحیت"
                                        Name="GrdResName" Width="200px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="TTypeName" Caption="زمینه آزمون"
                                        Name="TTypeName" Width="200px">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowHorizontalScrollBar="true"></Settings>
                                <ClientSideEvents EndCallback="function(s,e){
                                        if(s.cpSaveComplete==1)
                                        {
                                         s.cpSaveMessage=0;
                                         cmbDocTestType.SetSelectedIndex(-1);
                                         cmbAcceptedGrade.SetSelectedIndex(-1);
                                        }
                                        if(s.cpMessage!='')
                                        {
                                         ShowMessage(s.cpMessage);
                                         s.cpMessage='';
                                        }
                                        }" />
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" CausesValidation="false" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click" CausesValidation="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTestCondition">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjdsGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="FindMjParents"
                TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="objDocAcceptedGrade" runat="server" SelectMethod="SelectByMajor"
                TypeName="TSP.DataManager.DocAcceptedGradeManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsTestType" runat="server" SelectMethod="SelectByMajor"
                TypeName="TSP.DataManager.DocTestTypeManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="InActiveMajorTestType" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
