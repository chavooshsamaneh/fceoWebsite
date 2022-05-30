<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master"
    CodeFile="AddMemberFile.aspx.cs" Inherits="Employee_Document_AddMemberFile" Title="مشخصات پروانه اشتغال به کار" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function btnAdd_Click() {
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');

            if (txtaNumber.GetIsValid() && txtaAmount.GetIsValid()) {
                grid.PerformCallback('Add');
            }
        }

        function ClearAccounting() {
            cmbAccType.SetSelectedIndex(0);
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');
            txtDate.value = "";
            txtaNumber.SetText("");
            txtaAmount.SetText(HiddenFieldDocMemberFile.Get('FishAmount'));
            txtaDesc.SetText("");
        }

        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        function SetMeDocDefualtExpireDateJS() {
            CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());
        }
        function CheckDate() {
            var StartDate = document.getElementById('<%=txtRegDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtExpDate.ClientID%>').value;
            if (EndDate < StartDate && EndDate != "")
                return -1;
            else
                return 1;
        }
    </script>
    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                    CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                    FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                    WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                </pdc:PersianDateScriptManager>
                <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">

                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		alert('بازه زمانی اعتبار پروانه اشتباه می باشد' );
	}
	else
		e.processOnServer=true;
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>

                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">

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
                <div style="width: 100%" dir="rtl" align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server"
                        OnItemClick="MenuMemberFile_ItemClick">
                        <Items>
                            <dxm:MenuItem Text="مشخصات پروانه" Selected="True" Name="BaseInfo">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="آزمون ها" Name="Exam">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="دوره های آموزشی" Name="Periods">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پایه - صلاحیت" Name="MeDetail">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مدارک پیوست" Name="Attachment">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Capacity" Text="ظرفیت اشتغال">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <table dir="rtl" width="100%" align="center">
                    <tr>
                        <td width="10%" align="left">
                            <blink id="bkImgWarningMsg"><dxe:ASPxImage ID="ImgWarningMsg" ClientVisible="false" Width="25px" Height="25px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                        </td>
                        <td width="90%" align="right">
                            <asp:Label ID="lblWarningText" Font-Bold="true" ForeColor="DarkRed" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <blink id="bkImgWarningMsg2"><dxe:ASPxImage ID="ImgWarningMsg2" ClientVisible="false" Width="25px" Height="25px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblWarningText2" Font-Bold="true" ForeColor="DarkRed" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberFile" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <div width="100%" align="center">
                                <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ForeColor="Red"
                                    ID="lblWorkFlowState">
                                </dxe:ASPxLabel>
                                <br />

                                <span class="HelpUL" runat="server" visible="false" id="txtRequestComment">دستورالعمل درخواست</span>
                            </div>

                            <br />
                            <fieldset runat="server" id="RoundPanelMeInfo">
                                <legend class="fieldset-legend" dir="rtl"><b>مشخصات عضو</b>
                                </legend>

                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="کد عضویت*" ID="ASPxLabel2" Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" AutoPostBack="True"
                                                    ID="txtMeId" OnTextChanged="txtMeId_TextChanged">
                                                    <ClientSideEvents TextChanged="function(s, e) {
}"></ClientSideEvents>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                        <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td width="15%"></td>
                                            <td width="35%"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel16" Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                    ID="lblMeName">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel18" Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                    ID="lblMeLastName">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">تصویر عضو
                                            </td>
                                            <td valign="middle" align="right">
                                                <dxe:ASPxHyperLink runat="server" NavigateUrl="~/Images/Person.png" ImageUrl="~/Images/Person.png"
                                                    Width="75px" Height="75px" ImageWidth="75px" ImageHeight="75px" ID="ImgMember"
                                                    Target="_blank">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                            <td id="Td48" runat="server" align="right" valign="top">تصویر صفحه اول شناسنامه
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="hidno" Target="_blank"
                                                    ImageWidth="75px" ImageHeight="75px" Text="تصویر صفحه اول شناسنامه">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">تصویر صفحه دوم شناسنامه
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxHyperLink ID="HpIdNo2" runat="server" Target="_blank" ImageWidth="75px" ImageHeight="75px"
                                                    Text="تصویر صفحه دوم شناسنامه">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                            <td align="right" valign="top">تصویر صفحه توضیحات شناسنامه
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="HpIdNoDes" runat="server" Target="_blank" ImageWidth="75px"
                                                    ImageHeight="75px" Text="تصویر صفحه توضیحات شناسنامه">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right" valign="top">تصویر کارت ملی
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxHyperLink ID="HpSSN" runat="server" Target="_blank" ImageWidth="75px" ImageHeight="75px"
                                                    Text="تصویر کارت ملی">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                            <td align="right" valign="top">تصویر پشت کارت ملی
                                                       
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="HpSSN2" runat="server" Target="_blank" ImageWidth="75px" ImageHeight="75px"
                                                    Text="تصویر کارت ملی">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblSoldire" ClientVisible="true" runat="server" Text="تصویر کارت پایان خدمت"
                                                    ClientInstanceName="lblSoldire">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxHyperLink ID="HpSoldire" runat="server" Target="_blank" ImageWidth="75px"
                                                    ImageHeight="75px" Text="تصویر کارت پایان خدمت">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblSoldire2" ClientVisible="true" runat="server" Text="تصویر پشت کارت پایان خدمت"
                                                    ClientInstanceName="lblSoldire2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl">
                                                <dxe:ASPxHyperLink ID="HpSoldire2" runat="server" Target="_blank" ImageWidth="75px"
                                                    ImageHeight="75px" Text="تصویر پشت کارت پایان خدمت">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <TSPControls:CustomAspxDevGridView2 runat="server" KeyFieldName="MlId"
                                                    ID="CustomAspxDevGridView1" Caption="مدارک تحصیلی تایید شده در واحد عضویت" DataSourceID="ObjdsMemberLicence" SettingsBehavior-AllowFocusedRow="false">
                                                    <Columns>
                                                        <dxwgv:GridViewDataImageColumn FieldName="FilePath" Caption="تصویر مدرک تحصیلی" VisibleIndex="3"
                                                            Width="150px">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                            <PropertiesImage ImageHeight="50px" ImageWidth="50px">
                                                            </PropertiesImage>
                                                        </dxwgv:GridViewDataImageColumn>
                                                        <dxwgv:GridViewDataImageColumn FieldName="InquiryImageURL" Caption="تصویر استعلام"
                                                            VisibleIndex="3" Width="150px">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                                                            </PropertiesImage>
                                                        </dxwgv:GridViewDataImageColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="MajorFullNameWithMjcode" Caption="رشته" VisibleIndex="3"
                                                            Width="250px">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="UnName" Width="200px" Caption="دانشگاه"
                                                            VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="EndDate" Caption="فارغ التحصیل" VisibleIndex="3">
                                                            <EditCellStyle Wrap="False" HorizontalAlign="Center">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Avg" Caption="معدل" VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                            <PropertiesTextEdit DisplayFormatString="f2">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="CounName" Caption="کشور" VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <%-- <SettingsBehavior ColumnResizeMode="Control" >
                                                            </SettingsBehavior>--%>
                                                    <Settings ShowHorizontalScrollBar="True"></Settings>
                                                </TSPControls:CustomAspxDevGridView2>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <fieldset runat="server" id="TblTransfer">
                                    <legend class="fieldset-legend" dir="rtl"><b>اطلاعات انتقال از سایر استان ها</b>
                                    </legend>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 15%" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نوع انتقال" ClientInstanceName="lblPr" Width="100%"
                                                    ID="ASPxLabel11">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 35%" valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                    ID="txtTransferStatus">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="Tr1">

                                            <td style="width: 15%" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="استان قبلی" ClientInstanceName="lblPr" Width="100%"
                                                    ID="ASPxLabel6">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 35%" valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                    ID="lblPreProvince">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" style="width: 15%" dir="rtl" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ انتقالی" Width="100%" ID="ASPxLabel7">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 35%" valign="top" dir="ltr" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                    ID="lblTransferDate">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td runat="server" id="Td5" dir="ltr" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel8">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                    ClientInstanceName="lblFileNo" ID="lblFileNo" Style="direction: ltr" RightToLeft="True">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره عضویت" ClientInstanceName="lblMeNo" ID="ASPxLabel9">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" dir="rtl" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                    ID="lblPreMeNo">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="تصویر نامه انتقالی" ID="ASPxLabel38">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxHyperLink ID="HyperLinkTransfer" runat="server" Target="_blank" Text="تصویر نامه انتقالی">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <ul class="HelpUL">
                                                    <li>تاریخ ها مربوط به آخرین پروانه فرد در استان قبلی می باشند.همچنین استان صدور پروانه ، محل صدور اولین پروانه وی می باشد
                                                    </li>
                                                </ul>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%" align="right" valign="top">
                                                <dxe:ASPxLabel ID="lblDocPr" runat="server" ClientInstanceName="lblDocPr" Text="استان صدور پروانه"
                                                    Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 35%" align="right" valign="top">
                                                <TSPControls:CustomAspxComboBox ID="ComboDocPr" runat="server" ClientInstanceName="ComboDocPr"
                                                    DataSourceID="OdbProvince"
                                                    TextField="PrName" ValueField="PrId" ValueType="System.String"
                                                    Width="100%" RightToLeft="True">
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource ID="OdbProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                                                    SelectMethod="GetData" CacheDuration="30" FilterExpression="NezamCode<>{0}">
                                                    <FilterParameters>
                                                        <asp:Parameter Name="newparameter" />
                                                    </FilterParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td style="width: 15%"></td>
                                            <td style="width: 35%"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="تاریخ اولین صدور" ID="ASPxLabel12">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                                    ID="txtFirstDocRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                    ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="تاریخ آخرین تمدید" ID="ASPxLabel34">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                                    ID="txtCurrentDocRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                    ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار" ID="ASPxLabel37">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                                    ID="txtCurrentDocExpDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                    ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>

                                </fieldset>
                            </fieldset>
                            <br />
                            <fieldset runat="server" id="RoundPanelMajors">
                                <legend class="fieldset-legend" dir="rtl"><b>مدارک تحصیلی پروانه*</b>
                                </legend>


                                <asp:Panel runat="server" ID="PanelMajor">
                                    <div width="100%" align="right" class="HelpUL">
                                        <ul>
                                            <li>جهت ثبت پروانه با کد رشته اصلی برای افراد دارای مدرک با کد رشته مرتبط ،از لیست رشته
                                                                موضوع پروانه استفاده نمایید
                                            </li>
                                            <li>در صورتی که پیش از این شخص دارای پروانه اشتغال بوده است و در لیست زیر هیچ یک از
                                                                مدارک به عنوان رشته موضوع پروانه مشخص نشده است ، جهت جلوگیری از هرگونه تغییر در
                                                                شماره پروانه شخص پیش از ثبت هرگونه درخواست به واحد پشتیبان سریعا اطلاع رسانی نمایید.
                                            </li>
                                        </ul>
                                    </div>
                                    <fieldset>
                                        <legend class="fieldset-legend" dir="rtl"><b>مدارک تحصیلی فرد</b>
                                        </legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="15%" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مدرک تحصیلی*" ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td width="35%" valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String" DataSourceID="ObjdsMemberLicence"
                                                            TextField="MajorFullNameWithMjcode" ValueField="MlId" AutoPostBack="True"
                                                            ID="cmbMajor"
                                                            OnSelectedIndexChanged="cmbMajor_SelectedIndexChanged" RightToLeft="True">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidMajor">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField IsRequired="True" ErrorText="رشته تحصیلی را انتخاب نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td width="15%"></td>
                                                    <td width="35%"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="کشور محل تحصیل" ID="ASPxLabel29">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" Width="100%" runat="server" AutoPostBack="True" Enabled="false"
                                                            ID="txtUniCountry"
                                                            ClientInstanceName="txtUniCountry">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نام دانشگاه" ID="ASPxLabel26">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" Width="100%" runat="server" AutoPostBack="True" Enabled="false"
                                                            ID="txtUnivercity"
                                                            ClientInstanceName="txtUnivercity">
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="فارغ التحصیلی" ID="ASPxLabel27" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" Style="direction: ltr" Width="100%" runat="server" AutoPostBack="True"
                                                            Enabled="false"
                                                            ID="txtUNiEndDate" ClientInstanceName="txtUNiEndDate">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="معدل" Width="86px" ID="ASPxLabel28">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" Width="100%" runat="server" AutoPostBack="True" Enabled="false"
                                                            ID="txtUniGrade"
                                                            ClientInstanceName="txtUniGrade" DisplayFormatString="#.##">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="fieldset-legend" dir="rtl"><b>مشخصات رشته پروانه</b>
                                        </legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4" width="100%">
                                                        <dxe:ASPxLabel runat="server" Font-Bold="true" ClientInstanceName="lblWarningIsPrinted"
                                                            Text="در صورتی که رشته موضوع پروانه نمی باشد و تمایل به چاپ مقطع و گرایش تحصیلی این مدرک به صورت مجزا بر روی گواهینامه دارید گزینه ''چاپ مقطع تحصیلی در گواهینامه'' را انتخاب نمایید.انتخاب یا عدم انتخاب این گزینه در نمایش اطلاعات کامل مدرک در جدول پشت پروانه نمی شود"
                                                            ID="lblWarningIsPrinted" ForeColor="DarkRed" ClientVisible="false">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="رشته موضوع پروانه" ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td width="30%" valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String" DataSourceID="ObjdsMajor"
                                                            TextField="MjNameWithCode" ValueField="MjId"
                                                            ID="cmbFileMajor" RightToLeft="True"
                                                            OnSelectedIndexChanged="cmbFileMajor_SelectedIndexChanged" AutoPostBack="true">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right" width="20%">
                                                        <dxe:ASPxLabel runat="server" Text="وضعیت رشته" ID="ASPxLabel5">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="30%">
                                                        <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String"
                                                            ID="cmbMajorType"
                                                            ClientInstanceName="cmbMajorType" RightToLeft="True">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <Items>
                                                                <dxe:ListEditItem Text="رشته موضوع پروانه می باشد" Value="0"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Text="رشته موضوع پروانه نمی باشد" Value="1"></dxe:ListEditItem>
                                                            </Items>
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){
                                                                                                       
                                                                                                        if(cmbMajorType.GetValue()==0)
                                                                                                          {
                                                                                                            PanelSuggestMFNo.SetVisible(true);
                                                                                                            chkIsPrinted.SetVisible(false);
                                                                                                            chkIsPrinted.SetChecked(false);
                                                                                                            lblWarningIsPrinted.SetVisible(false);
                                                                                                          }
                                                                                                        else
                                                                                                          {
                                                                                                            PanelSuggestMFNo.SetVisible(false);
                                                                                                            chkIsPrinted.SetChecked(false);
                                                                                                            chkIsPrinted.SetVisible(true);
                                                                                                            lblWarningIsPrinted.SetVisible(true);
                                                                                                          }

                                                                                                        }" />
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" colspan="2" width="50%">
                                                        <dxp:ASPxPanel ID="PanelSuggestMFNo" ClientInstanceName="PanelSuggestMFNo" runat="server"
                                                            Width="100%">
                                                            <PanelCollection>
                                                                <dxp:PanelContent>
                                                                    <table width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td valign="top" align="right" style="width: 40%">
                                                                                    <dxe:ASPxLabel runat="server" Text="شماره پروانه پیشنهادی" ID="lblMfNoSuggested">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td valign="top" align="right">
                                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="false"
                                                                                        Style="direction: ltr" RightToLeft="True"
                                                                                        ID="txtMfNoSuggested" ClientInstanceName="txtMfNoSuggested">
                                                                                        <ValidationSettings>
                                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                            </ErrorImage>
                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                            </ErrorFrameStyle>
                                                                                        </ValidationSettings>
                                                                                    </TSPControls:CustomTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                    <td width="20%"></td>
                                                    <td width="30%"></td>
                                                </tr>
                                                <tr>
                                                    <td width="100%" valign="top" align="right" colspan="4">
                                                        <TSPControls:CustomASPxCheckBox ID="chkIsPrinted" runat="server" ClientInstanceName="chkIsPrinted"
                                                            Text="چاپ مقطع بر روی گواهینامه" ClientVisible="false">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                    <div dir="rtl">
                                        <table width="100%">
                                            <tr>
                                                <td style="vertical-align: top" dir="ltr" align="center" colspan="4">
                                                    <br />
                                                    <TSPControls:CustomAspxButton runat="server"
                                                        Text="اضافه به لیست" ImagePosition="Right" ValidationGroup="ValidMajor"
                                                        ID="btnAddMajor" OnClick="btnAddMajor_Click"
                                                        UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s,e){
                                                                            if(cmbMajorType.GetValue()==0 && lblMFNo.GetText()!='' &&  txtMfNoSuggested.GetText()!=lblMFNo.GetText())
                                                                            {
                                                                            e.processOnServer=confirm('با انتخاب این رشته شماره پروانه شخص تغییر می یابد آیا از انتخاب این رشته مطمئن می باشید؟');
                                                                            }
                                                                            }" />
                                                        <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td dir="rtl" align="center">
                                                <TSPControls:CustomAspxDevGridView2 ID="GridViewMajor" ClientInstanceName="GridViewMajor" runat="server" Width="100%"
                                                    KeyFieldName="Id"
                                                    OnRowDeleting="GridViewMajor_RowDeleting" EnableCallBacks="False" OnHtmlRowPrepared="GridViewMajor_HtmlRowPrepared">
                                                    <Columns>

                                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="25px" Name="clmnDelete">
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Id" Caption="Id" Visible="False" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="InActives" Caption="وضعیت" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="MfId" Caption="MfId" Visible="False" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="MlName" Width="200px" Caption="رشته تحصیلی"
                                                            VisibleIndex="0">
                                                        </dxwgv:GridViewDataTextColumn>

                                                        <dxwgv:GridViewDataTextColumn FieldName="FMjName" Width="200px" Caption="رشته موضوع پروانه"
                                                            VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="MjType" Caption="موضوع پروانه" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="UnCount" Caption="کشور" VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="UnName" Width="200px" Caption="دانشگاه"
                                                            VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="UnEndDate" Caption="فارغ التحصیل" VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="UnGrade" Caption="معدل" VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                            <PropertiesTextEdit DisplayFormatString="#,##">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="IsPrintedName" Caption="وضعیت چاپ" VisibleIndex="3">
                                                            <EditCellStyle Wrap="False">
                                                            </EditCellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </TSPControls:CustomAspxDevGridView2>


                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset runat="server" id="RoundPanelBasicInfo">
                                <legend class="fieldset-legend" dir="rtl"><b>مشخصات پروانه</b>
                                </legend>


                                <TSPControls:CustomAspxCallbackPanel runat="server"
                                    ClientInstanceName="CallbackPanelDoRegDate" Width="100%" ID="CallbackPanelDoRegDate"
                                    OnCallback="CallbackPanelDoRegDate_Callback">
                                    <SettingsLoadingPanel Text="در حال بارگذاری" />
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent11" runat="server">
                                            <table id="Table3" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100%" colspan="4" align="right">
                                                            <ul class="HelpUL">
                                                                <li runat="server" text="نکات تاریخ صدور" id="lblRegDateComment" visible="false"></li>
                                                            </ul>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="نوع درخواست انتقال" ID="lblTransferType" Width="100%"
                                                                ClientInstanceName="lblTransferType">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td width="35%">
                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                ID="cmbTransferType" ClientInstanceName="cmbTransferType"
                                                                ValueType="System.String" AutoPostBack="false"
                                                                RightToLeft="True">
                                                                <Items>
                                                                    <dxe:ListEditItem Value="5" Text="انتقال و صدور" Selected="true"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Value="10" Text="انتقال و تمدید"></dxe:ListEditItem>
                                                                </Items>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>
                                                        </td>
                                                        <td width="15%"></td>
                                                        <td width="35%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%">عنوان شخص در گواهینامه
                                                        </td>
                                                        <td width="35%">

                                                            <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String" DataSourceID="ObjdsMemberLicenceTitle"
                                                                TextField="MeLicenceName" ValueField="TitleId"
                                                                ID="cmbMeTitle"
                                                                RightToLeft="True">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField IsRequired="True" ErrorText="عنوان شخص را انتخاب نمایید"></RequiredField>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomAspxComboBox>
                                                            <asp:ObjectDataSource ID="ObjdsMemberLicenceTitle" runat="server" TypeName="TSP.DataManager.MemberLicenceManager"
                                                                SelectMethod="SelectActiveLicenceByTitle" OldValuesParameterFormatString="original_{0}">
                                                                <SelectParameters>
                                                                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>

                                                        </td>

                                                        <td width="15%"></td>
                                                        <td width="35%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabel22">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="vertical-align: top" dir="rtl" align="right">
                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                ID="cmbIsTemporary" ClientInstanceName="cmbIsTemporary" ValueType="System.String"
                                                                AutoPostBack="false" RightToLeft="True">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e){CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());}" />
                                                                <Items>
                                                                    <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                                                </Items>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="شماره سریال گواهینامه" ID="ASPxLabel1">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"
                                                                ID="txtSerialNo">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                    <RegularExpression ValidationExpression="\d*" ErrorText="شماره سریال را صحیح وارد نمایید" />
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="تاریخ صدور" ID="lblRegDate">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                                                                ID="txtRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" onchange="return SetMeDocDefualtExpireDateJS();"></pdc:PersianDateTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage ID="btnSetRegDate" ClientInstanceName="btnSetRegDate" ToolTip="تنظیم تاریخ اعتبار"
                                                                                runat="server" Text="" Height="13px" Border-BorderWidth="1px" Border-BorderColor="LightBlue"
                                                                                Width="13px" Image-Height="13px" Image-Width="13px" ImageUrl="~/Images/ResetDate.png">
                                                                                <ClientSideEvents Click="function(s, e) { SetMeDocDefualtExpireDateJS(); }" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" ID="ASPxLabel14">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                                                ID="txtExpDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel30">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td colspan="3">
                                                            <TSPControls:CustomASPXMemo runat="server" Height="34px" Width="100%"
                                                                ClientInstanceName="txtDescription" ID="txtDescription" RightToLeft="True">
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
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel21">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                                ID="txtGradeDes">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel23">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                                ID="txtGradeObs">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel24">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                                ID="txtGradeImp">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="پایه شهرسازی" ID="ASPxLabel10">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                                ID="txtGradeUrbanism">
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="پایه نقشه برداری" ID="ASPxLabel36">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                                ID="txtGradeMapping">
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="پایه ترافیک" ID="ASPxLabel39">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                                                ID="txtGradeTraffic">
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel40">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="false"
                                                                Style="direction: ltr" RightToLeft="True"
                                                                ID="lblMFNo" ClientInstanceName="lblMFNo">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="تاریخ تحویل گواهینامه" ID="lblLivertyDate">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="false"
                                                                Style="direction: ltr" RightToLeft="True"
                                                                ID="txtLivertyDate" ClientInstanceName="txtLivertyDate">
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>




                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <dxe:ASPxLabel runat="server" Text="تصویر روی پروانه پیشین" ID="lblImgFrontOldDoc" ClientVisible="true">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td align="right" valign="top">
                                                            <table dir="rtl">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <TSPControls:CustomAspxUploadControl runat="server" ID="flpFrontOldDoc" InputType="Images"
                                                                                UploadWhenFileChoosed="true" ClientInstanceName="flpFrontOldDoc" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                                ClientVisible="false">
                                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	ImgEndFrontOldDoc.SetVisible(true);
	HiddenFieldDocMemberFile.Set('ImgFrontOld',1);
	lblValidationFImgOldDoc.SetVisible(false);
    hpImgFrontOldDoc.SetVisible(true);
	hpImgFrontOldDoc.SetImageUrl('../../Image/DocMeFile/OldDoc/'+e.callbackData);
	}
	else{
    HiddenFieldDocMemberFile.Set('ImgFrontOld',0);
	ImgEndFrontOldDoc.SetVisible(false);
	lblValidationFImgOldDoc.SetVisible(true);
	}
}"></ClientSideEvents>
                                                                            </TSPControls:CustomAspxUploadControl>
                                                                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                                ID="lblValidationFImgOldDoc" ForeColor="Red" ClientInstanceName="lblValidationFImgOldDoc">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                ID="ImgEndFrontOldDoc" ClientInstanceName="ImgEndFrontOldDoc">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="hpImgFrontOldDoc" ClientInstanceName="hpImgFrontOldDoc"
                                                                Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                                <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                                </EmptyImage>
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td align="right" valign="top">
                                                            <dxe:ASPxLabel runat="server" Text="تصویر پشت پروانه پیشین" ID="lblImgBackOldDoc" ClientVisible="true">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td align="right" valign="top">
                                                            <table dir="rtl">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <TSPControls:CustomAspxUploadControl runat="server" ID="flpBackOldDoc" InputType="Images"
                                                                                UploadWhenFileChoosed="true" ClientInstanceName="flpBackOldDoc" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                                ClientVisible="False">
                                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldDocMemberFile.Set('ImgBackOld',1);   
	ImgEndBackOldDoc.SetVisible(true);
	lblValidationBImgOldDoc.SetVisible(false);
    hpImgBackOldDoc.SetVisible(true);
    hpImgBackOldDoc.SetImageUrl('../../Image/DocMeFile/OldDoc/'+e.callbackData);
	}
	else{
    HiddenFieldDocMemberFile.Set('ImgBackOld',0);
	ImgEndBackOldDoc.SetVisible(false);
	lblValidationBImgOldDoc.SetVisible(true);
	}
}"></ClientSideEvents>
                                                                            </TSPControls:CustomAspxUploadControl>
                                                                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                                ID="lblValidationBImgOldDoc" ForeColor="Red" ClientInstanceName="lblValidationBImgOldDoc">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                ID="ImgEndBackOldDoc" ClientInstanceName="ImgEndBackOldDoc">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر پشت پروانه پیشین"
                                                                ID="hpImgBackOldDoc" ClientInstanceName="hpImgBackOldDoc" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                                <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                                </EmptyImage>
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <dxe:ASPxLabel runat="server" Text="تصویر استعلام از اداره امور مالیاتی" ID="lblImgTaxOfficeLetter" ClientVisible="true">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td align="right" valign="top">
                                                            <table dir="rtl">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <TSPControls:CustomAspxUploadControl runat="server" ID="flpTaxOfficeLetter" InputType="Images"
                                                                                UploadWhenFileChoosed="true" ClientInstanceName="flpTaxOfficeLetter" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                                ClientVisible="False">
                                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldDocMemberFile.Set('ImgTaxOfficeLetter',1);   
	ImgEndTaxOfficeLetter.SetVisible(true);
	lblValidationTaxOfficeLetter.SetVisible(false);
    hpImgTaxOfficeLetter.SetVisible(true);
    hpImgTaxOfficeLetter.SetImageUrl('../../Image/DocMeFile/TaxOffice/'+e.callbackData);
	}
	else{
    HiddenFieldDocMemberFile.Set('ImgTaxOfficeLetter',0);
	ImgEndTaxOfficeLetter.SetVisible(false);
	lblValidationTaxOfficeLetter.SetVisible(true);
	}
}"></ClientSideEvents>
                                                                            </TSPControls:CustomAspxUploadControl>
                                                                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                                ID="lblValidationTaxOfficeLetter" ForeColor="Red" ClientInstanceName="lblValidationTaxOfficeLetter">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                ID="ImgEndTaxOfficeLetter" ClientInstanceName="ImgEndTaxOfficeLetter">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر استعلام اداره امور مالیاتی"
                                                                ID="hpImgTaxOfficeLetter" ClientInstanceName="hpImgTaxOfficeLetter" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                                <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                                </EmptyImage>
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td>تصویر اثر انگشت</td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر اثر انگشت"
                                                                ID="ImgFingerPrint" ClientInstanceName="ImgFingerPrint" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                                <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                                </EmptyImage>
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <dxe:ASPxLabel runat="server" Text="تصویر گواهینامه دوره جوش" ID="ASPxLabel13" ClientVisible="true">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td align="right" valign="top">
                                                            <table dir="rtl">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <TSPControls:CustomAspxUploadControl runat="server" ID="flpJooshPeriod" InputType="Images"
                                                                                UploadWhenFileChoosed="true" ClientInstanceName="flpJooshPeriod" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                                ClientVisible="False">
                                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldDocMemberFile.Set('JooshPeriod',1);                                                                                                             
	ImgEndJooshPeriod.SetVisible(true);  
	lblValidationJooshPeriod.SetVisible(false);  
    hpImgJooshPeriod.SetVisible(true);  
    hpImgJooshPeriod.SetImageUrl('../../Image/DocMeFile/JooshPeriod/'+e.callbackData);  

	}
	else{
    HiddenFieldDocMemberFile.Set('JooshPeriod',0);
	ImgEndJooshPeriod.SetVisible(false);
	lblValidationJooshPeriod.SetVisible(true);
	}
           
}"></ClientSideEvents>
                                                                            </TSPControls:CustomAspxUploadControl>
                                                                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                                ID="lblValidationJooshPeriod" ForeColor="Red" ClientInstanceName="lblValidationJooshPeriod">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                ID="ImgEndJooshPeriod" ClientInstanceName="ImgEndJooshPeriod">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر گواهینامه دوره جوش"
                                                                ID="hpImgJooshPeriod" ClientInstanceName="hpImgJooshPeriod" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                                <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                                </EmptyImage>
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="تصویر  نامه عدم عضویت در سازمان نظام کاردانی" ID="lblKardanAttach"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <table>
                                                                <tbody>                                                                   
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <a id="AImageKardan" target="_blank">
                                                                                <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImageKardan" ClientInstanceName="ImageKardan"
                                                                                    Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                                                    <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                                                    </EmptyImage>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                                                               <td align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="تصویر HSE" ID="lblHse" ClientVisible="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3" align="right" valign="top">
                                                        <table dir="rtl">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpHse" InputType="Images"
                                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpHse" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                            ClientVisible="False">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldDocMemberFile.Set('Hse',1);                                                                                                             
	ImgEndHse.SetVisible(true);  
	lblValidationHse.SetVisible(false);  
    hpImgHse.SetVisible(true);  
    hpImgHse.SetImageUrl('../../Image/DocMeFile/Hse/'+e.callbackData);  

	}
	else{
    HiddenFieldDocMemberFile.Set('Hse',0);
	ImgEndHse.SetVisible(false);
	lblValidationHse.SetVisible(true);
	}
           
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblValidationHse" ForeColor="Red" ClientInstanceName="lblValidationHse">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="ImgEndHse" ClientInstanceName="ImgEndHse">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر HSE"
                                                            ID="hpImgHse" ClientInstanceName="hpImgHse" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                            </EmptyImage>
                                                        </dxe:ASPxImage>
                                                    </td>


                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </TSPControls:CustomAspxCallbackPanel>
                            </fieldset>

                            <br />
                            <fieldset runat="server" id="RoundPanelJobConfirm">
                                <legend class="fieldset-legend" dir="rtl"><b>تایید کنندگان سابقه کار</b>
                                </legend>

                                <TSPControls:CustomAspxDevGridView2 ID="GrdvJobCon" runat="server" Width="100%" EnableCallBacks="True"
                                    KeyFieldName="JobConfId" AutoGenerateColumns="False"
                                    DataSourceID="ObjectDataSourceJobConfirm">
                                    <Settings ShowHorizontalScrollBar="true"></Settings>
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JobConfId"
                                            Name="JobConfId">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Position" Caption="سمت"
                                            Name="Position">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FromDate" Caption="از تاریخ"
                                            Name="FromDate">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ToDate" Caption="تا تاریخ"
                                            Name="ToDate">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ConfirmTypeName" Caption="نوع تایید کننده"
                                            Name="ProjectName">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت شخص حقیقی"
                                            Name="Employer">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Name" Caption="نام" Name="PrTypeName">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MFNo" Caption="شماره پروانه">
                                            <HeaderStyle Wrap="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="200" FieldName="Description"
                                            Caption="توضیحات">
                                            <HeaderStyle Wrap="True" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر فرم تاییدیه "
                                            FieldName="FileURL" Caption="تصویر فرم تاییدیه" Name="FileURL">
                                        </dxwgv:GridViewDataHyperLinkColumn>
                                        <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="5" Width="130" PropertiesHyperLinkEdit-Text="تصویر پروانه/رتبه بندی "
                                            FieldName="GrdURL" Caption="تصویر پروانه/رتبه بندی" Name="GradeURL">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False" />
                                        </dxwgv:GridViewDataHyperLinkColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView2>
                                <asp:ObjectDataSource ID="ObjectDataSourceJobConfirm" runat="server" SelectMethod="FindByMfId"
                                    TypeName="TSP.DataManager.DocMemberFileJobConfirmationManager">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="-2" DbType="Int32" Name="MfId" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </fieldset>


                            <br />
                            <fieldset runat="server" id="RoundPanelAccounting">
                                <legend class="fieldset-legend" dir="rtl"><b>ثبت فیش</b>
                                </legend>
                                <asp:Panel ID="PanelAccountingInserting" runat="server">
                                    <div align="center">
                                        <table id="tableAccounting" dir="rtl" runat="server" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4" valign="top" align="center">
                                                        <dxe:ASPxLabel runat="server" ID="lblRegEnter" ClientInstanceName="lblRegEnter" ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel runat="server" ID="lblReg" ClientInstanceName="lblReg" ClientVisible="false"
                                                            ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="بابت" ID="ASPxLabel17">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="rtl" valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbAccType"
                                                            ValueType="System.Int32" SelectedIndex="0"
                                                            DataSourceID="ObjectDataSourceAccType"
                                                            TextField="TypeName" ValueField="AccTypeId" ClientInstanceName="cmbAccType">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                            <LoadingPanelImage Url="~/App_Themes/Glass/Editors/Loading.gif">
                                                            </LoadingPanelImage>
                                                        </TSPControls:CustomAspxComboBox>
                                                        <asp:ObjectDataSource ID="ObjectDataSourceAccType" runat="server" SelectMethod="GetData"
                                                            TypeName="TSP.DataManager.TechnicalServices.AccTypeManager"></asp:ObjectDataSource>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="مبلغ" ID="ASPxLabel33">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtaAmount" Width="100%"
                                                            ClientInstanceName="txtaAmount">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="نامعتبر" ValidationExpression="[1-9]\d*"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره" ID="ASPxLabel31">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtaNumber" Width="100%"
                                                            Style="direction: ltr" ClientInstanceName="txtaNumber">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel32">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            ShowPickerOnTop="True" ValidationGroup="Acc" ID="txtaDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtaDate" ValidationGroup="Acc"
                                                            ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtaDesc" Width="100%"
                                                            ClientInstanceName="txtaDesc">
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
                                                    <td valign="middle" align="center" colspan="4">
                                                        <br />
                                                        <TSPControls:CustomAspxButton runat="server" ImagePosition="left" Text="اضافه به لیست"
                                                            ID="btnAddAccounting" ValidationGroup="Acc"
                                                            AutoPostBack="False" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) { btnAdd_Click(); }" />
                                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                        </TSPControls:CustomAspxButton>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%" ID="GridViewAccounting" SettingsResizing-ColumnResizeMode="Control"
                                    KeyFieldName="AccountingId" OnHtmlRowPrepared="GridViewAccounting_HtmlRowPrepared"
                                    OnRowDeleting="GridViewAccounting_RowDeleting" ClientInstanceName="grid" OnCustomCallback="GridViewAccounting_CustomCallback">

                                    <Columns>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Name="Delete"
                                            Width="25px" ShowDeleteButton="true">
                                            <%--<DeleteButton Visible="True" Image-Url="~/Images/DeleteFromGrid.png">
                                </DeleteButton>--%>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AccType"
                                            Caption="بابت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StatusName" Caption="وضعیت پرداخت"
                                            Width="80px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ReferenceId" Caption="شماره رهگیری بانک"
                                            Width="150px">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت"
                                            Width="250px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره"
                                            Width="200px">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ" Width="90px">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Amount" Caption="مبلغ"
                                            Width="100px">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                            <PropertiesTextEdit DisplayFormatString="#,#">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Description" Caption="توضیحات"
                                            Width="150px">
                                            <CellStyle Wrap="True">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <ClientSideEvents EndCallback="function(s,e){
                                        if(s.cpSaveComplete=='1'){
                                         ClearAccounting();
                                         s.cpSaveComplete='0';
                                         }
                                        else if(s.cpMessage!='')
                                        {
                                         ShowMessage(s.cpMessage);
                                         s.cpMessage='';
                                        }
                                        }" />
                                </TSPControls:CustomAspxDevGridView2>
                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>


                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                CausesValidation="false" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">

                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td dir="ltr">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">

                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		alert('بازه زمانی اعتبار پروانه اشتباه می باشد' );
	}
	else
		e.processOnServer=true;
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                        </td>


                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" OnClick="btnBack_Click" runat="server" Text=" " EnableTheming="False"
                                                EnableViewState="False" UseSubmitBehavior="False" CausesValidation="False" ToolTip="بازگشت">

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
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile" ClientInstanceName="HiddenFieldDocMemberFile">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjdsMajor" runat="server" TypeName="TSP.DataManager.MajorManager"
                    SelectMethod="FindMajorParent">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" TypeName="TSP.DataManager.MemberLicenceManager"
                    SelectMethod="SelectMemberLicence" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="MReId" Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="InActive" Type="Int16" />
                        <asp:Parameter DefaultValue="1" Name="IsConfirm" Type="Int16" />
                        <asp:Parameter DefaultValue="-1" Name="JustThisRequestMembers" Type="int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
