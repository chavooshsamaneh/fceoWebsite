<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SavePriceArchive.aspx.cs" Inherits="TechnicalServices_SavePriceArchive"
    Title="مشخصات تعرفه خدمات مهندسی طراحی و نظارت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function SetSumPercentValue(LabelSum, AddValue1, AddValue2, AddValue3, AddValue4, AddValue5) {
            var value = parseFloat(AddValue1) + parseFloat(AddValue2) + parseFloat(AddValue3) + parseFloat(AddValue4) + parseFloat(AddValue5);
            if (value > 100) alert('جمع درصد ها نمی تواند بیشتر از 100 باشد');
            else LabelSum.SetText(Math.round(value * 1000) / 1000);
        }
        function SetCostValue(TextBoxCost, PriceValue, Percent) {
            var Price = 0;
            if (PriceValue != '')
                Price = parseFloat(PriceValue);
            var value = parseFloat(Percent) / 100 * Price;
            TextBoxCost.SetText(Math.ceil(value));
        }
        function SetSumAllPercent(LabelSumAll, Sum1, Sum2) {
            var value = parseFloat(Sum1) + parseFloat(Sum2);
            LabelSumAll.SetText(Math.round(value * 1000) / 1000);
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="false">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tr>
                                <td style="vertical-align: top;" align="right">
                                    <table dir="rtl" cellpadding="0">
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                    EnableTheming="False" ToolTip="جدید" ID="btnNew" EnableViewState="False" OnClick="btnNew_Click"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                    Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                                    OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " EnableTheming="False"
                                                    ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click" CausesValidation="true"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/save.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                    EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/Back.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <ul class="HelpUL">
                <li>در صورت وارد نکردن <u>حداکثر زیربنا</u> برای هر گروه ساختمانی ، محاسبه دستمزد های عوامل پروژه با مشکل مواجه خواهد شد.</li>
            </ul>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelContent" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>       
                        <fieldset><legend class="HelpUL">مشخصات تعرفه</legend>
                                    <table width="100%">
                                        <tr>
                                            <td align="right" style="width: 15%">سال
                                            </td>
                                            <td align="right" style="width: 35%">
                                                <dxe:ASPxSpinEdit ID="txtYear" runat="server" Height="21px" Width="100%" AllowNull="False"
                                                    NumberType="Integer" MinValue="1300" MaxValue="3000">
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td valign="top" align="right" width="15%">تاریخ ثبت
                                            </td>
                                            <td dir="ltr" valign="top" align="right" width="35%">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" Enabled="False"
                                                    ShowPickerOnTop="True" ID="txtCreateDate" PickerDirection="ToRight" RightToLeft="False"
                                                    IconUrl="~/Image/Calendar.gif" ShowPickerOnEvent="OnClick" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                <%--   <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCreateDate" ID="RequiredFieldValidatorRegDate">تاریخ ثبت  را وارد نمایید</asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">توضیحات
                                            </td>
                                            <td colspan="4" dir="rtl" align="right">
                                                <TSPControls:CustomASPXMemo ID="txtDescription" runat="server"
                                                    Height="51px" Width="100%">
                                                    <ValidationSettings>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">تبصره
                                            </td>
                                            <td colspan="4" dir="rtl" align="right">
                                                <TSPControls:CustomASPXMemo ID="txtRemark" runat="server"
                                                    Height="101px" Width="100%">
                                                    <ValidationSettings>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تصویر*" ID="lblImage" ClientInstanceName="lblImage"></dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" rowspan="3">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl ID="UploadControlImgURL" runat="server" ClientInstanceName="UploadControlImgURL" InputType="Files" MaxSizeForUploadFile="10000000"
                                                                    UploadWhenFileChoosed="true" OnFileUploadComplete="UploadControlImgURL_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgURL.SetVisible(true);
  	 //HiddenFieldExam.Set('Img',1);
	lblImageWarning.SetVisible(false);
	HeyperLinkImg.SetVisible(true);
	HeyperLinkImg.SetNavigateUrl('../../../Image/TechnicalServices/PriceArchive/'+e.callbackData);
	}
	else{
	imgEndUploadImgURL.SetVisible(false);
	lblImageWarning.SetVisible(true);
	HeyperLinkImg.SetVisible(false);
	HeyperLinkImg.SetNavigateUrl('');    
 // 	HiddenFieldExam.Set('Img',0);
	}
}" />
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel ID="lblImageWarning" runat="server" ClientInstanceName="lblImageWarning"
                                                                    ClientVisible="False" ForeColor="Red" Text="فایل راانتخاب نمایید">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage ID="imgEndUploadImgURL" runat="server" ClientInstanceName="imgEndUploadImgURL"
                                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="فایل انتخاب شد">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxHyperLink ID="HeyperLinkImg" runat="server" ClientInstanceName="HeyperLinkImg"
                                                    Target="_blank" Text="آدرس فایل ">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                        <br />
                        <asp:Panel runat="server" ID="PanelData">
                            <div align="right" style="width: 100%; font-family: Tahoma; font-size: 8pt">
                                <table width="100%" border="1px" bordercolor="#7EACB1">
                                    <tr>
                                        <td style="width: 11.11%;" colspan="2">
                                            <b>گروه ساختمانی</b>
                                        </td>
                                        <td style="width: 22.22%;" align="center" colspan="4">
                                            <b>الف</b>
                                        </td>
                                        <td style="width: 11.11%;" align="center" colspan="2">
                                            <b>ب</b>
                                        </td>
                                        <td style="width: 22.22%;" align="center" colspan="4">
                                            <b>ج</b>
                                        </td>
                                        <td style="width: 33.33%;" align="center" colspan="6">
                                            <b>د</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <b>تعداد طبقات</b>
                                        </td>
                                      <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoA1From" runat="server" Height="21px" Number="1" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoA1To" runat="server" Height="21px" Number="2" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                      <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoAFrom" runat="server" Height="21px" Number="1" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoATo" runat="server" Height="21px" Number="2" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoBFrom" runat="server" Height="21px" Number="3" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoBTo" runat="server" Height="21px" Number="5" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoC1From" runat="server" Height="21px" Number="6" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoC1To" runat="server" Height="21px" Number="7" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoC2From" runat="server" Height="21px" Number="8" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoC2To" runat="server" Height="21px" Number="10" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoD1From" runat="server" Height="21px" Number="11" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoD1To" runat="server" Height="21px" Number="12" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoD2From" runat="server" Height="21px" Number="13" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoD2To" runat="server" Height="21px" Number="15" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" class="tdCol2SavePriceArchive">
                                            <table width="100%">
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtStepNoD3From" runat="server" Height="21px" Number="16" Width="40px"
                                                            AllowNull="False"
                                                            NumberType="Integer" MinValue="1" MaxValue="99">
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="5">و بالاتر
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="5">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                   
                                    <td style="font-size: 8pt" colspan="2">
                                            <b>نوع اسکلت</b>
                                                <asp:ObjectDataSource ID="ObjectdatasourceStructureSkeleton" runat="server" SelectMethod="GetData"
                                                                    TypeName="TSP.DataManager.TechnicalServices.StructureSkeletonManager"></asp:ObjectDataSource>
                                        </td>
                                    <td colspan="2">
                                          <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonA1" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonA1" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                                    <td colspan="2">
                                              <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonA" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonA" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                                    <td colspan="2">
                                             <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonB" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonB" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                                    <td colspan="2">
                                           <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonC1" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonC1" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                                    <td colspan="2">
                                              <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonC2" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonC2" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                                     <td colspan="2">
                                              <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonD1" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonD1" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                                     <td colspan="2">
                                           <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonD2" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonD2" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                                     <td colspan="2">
                                                 <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="Title" ID="ComboBoxStructureSkeletonD3" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                    ValueType="System.Int32" ValueField="StructureSkeletonId"
                                                                    EnableIncrementalFiltering="True"
                                                                    ClientInstanceName="ComboBoxStructureSkeletonD3" RightToLeft="True">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </TSPControls:CustomAspxComboBox>
                                         </td>
                          </tr>
                                    <tr>
                                        <td style="font-size: 8pt" colspan="2">
                                            <b>حداکثر زیربنا (متر)</b>
                                        </td>
                                               <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqA1" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqA" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqB" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqC1" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqC2" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqD1" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqD2" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtMaxSqD3" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 8pt" colspan="2">
                                            <b>حداکثر تعداد واحد</b>
                                        </td>
                                           <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitA1" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                               </td>
                                               
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitA" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitB" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitC1" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitC2" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitD1" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitD2" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCountUnitD3" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 8pt" colspan="2">
                                            <b>هزینه ساخت هر مترمربع بنا (ریال)</b>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCostA1" ClientInstanceName="txtCostA1" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchA1,txtCostA1.GetText(),txtDesignArchPercentA1.GetText());
                        SetCostValue(txtDesignSumSazeA1,txtCostA1.GetText(),txtDesignSazePercentA1.GetText());
                        SetCostValue(txtDesignSumTasisatA1,txtCostA1.GetText(),txtDesignTasisatPercentA1.GetText());
                        SetCostValue(txtDesignSumShahrA1,txtCostA1.GetText(),txtDesignShahrPercentA1.GetText());
                        SetCostValue(txtDesignSumMapA1,txtCostA1.GetText(),txtDesignMapPercentA1.GetText());
                        
                        SetCostValue(txtSupervisionSumArchA1,txtCostA1.GetText(),txtSupervisionArchPercentA1.GetText());
                        SetCostValue(txtSupervisionSumSazeA1,txtCostA1.GetText(),txtSupervisionSazePercentA1.GetText());
                        SetCostValue(txtSupervisionSumTasisatA1,txtCostA1.GetText(),txtSupervisionTasisatPercentA1.GetText());
                        SetCostValue(txtSupervisionSumShahrA1,txtCostA1.GetText(),txtSupervisionShahrPercentA1.GetText());
                        SetCostValue(txtSupervisionSumMapA1,txtCostA1.GetText(),txtSupervisionMapPercentA1.GetText());                                     
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCostA" ClientInstanceName="txtCostA" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchA,txtCostA.GetText(),txtDesignArchPercentA.GetText());
                        SetCostValue(txtDesignSumSazeA,txtCostA.GetText(),txtDesignSazePercentA.GetText());
                        SetCostValue(txtDesignSumTasisatA,txtCostA.GetText(),txtDesignTasisatPercentA.GetText());
                        SetCostValue(txtDesignSumShahrA,txtCostA.GetText(),txtDesignShahrPercentA.GetText());
                        SetCostValue(txtDesignSumMapA,txtCostA.GetText(),txtDesignMapPercentA.GetText());
                        
                        SetCostValue(txtSupervisionSumArchA,txtCostA.GetText(),txtSupervisionArchPercentA.GetText());
                        SetCostValue(txtSupervisionSumSazeA,txtCostA.GetText(),txtSupervisionSazePercentA.GetText());
                        SetCostValue(txtSupervisionSumTasisatA,txtCostA.GetText(),txtSupervisionTasisatPercentA.GetText());
                        SetCostValue(txtSupervisionSumShahrA,txtCostA.GetText(),txtSupervisionShahrPercentA.GetText());
                        SetCostValue(txtSupervisionSumMapA,txtCostA.GetText(),txtSupervisionMapPercentA.GetText());                                     
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCostB" ClientInstanceName="txtCostB" runat="server"
                                                Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchB,txtCostB.GetText(),txtDesignArchPercentB.GetText());
                        SetCostValue(txtDesignSumSazeB,txtCostB.GetText(),txtDesignSazePercentB.GetText());
                        SetCostValue(txtDesignSumTasisatB,txtCostB.GetText(),txtDesignTasisatPercentB.GetText());
                        SetCostValue(txtDesignSumShahrB,txtCostB.GetText(),txtDesignShahrPercentB.GetText());
                        SetCostValue(txtDesignSumMapB,txtCostB.GetText(),txtDesignMapPercentB.GetText()); 
                        
                        SetCostValue(txtSupervisionSumArchB,txtCostB.GetText(),txtSupervisionArchPercentB.GetText());
                        SetCostValue(txtSupervisionSumSazeB,txtCostB.GetText(),txtSupervisionSazePercentB.GetText());
                        SetCostValue(txtSupervisionSumTasisatB,txtCostB.GetText(),txtSupervisionTasisatPercentB.GetText());
                        SetCostValue(txtSupervisionSumShahrB,txtCostB.GetText(),txtSupervisionShahrPercentB.GetText());
                        SetCostValue(txtSupervisionSumMapB,txtCostB.GetText(),txtSupervisionMapPercentB.GetText());                                   
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCostC1" runat="server"
                                                ClientInstanceName="txtCostC1" Width="100%">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchC1,txtCostC1.GetText(),txtDesignArchPercentC1.GetText());
                        SetCostValue(txtDesignSumSazeC1,txtCostC1.GetText(),txtDesignSazePercentC1.GetText());
                        SetCostValue(txtDesignSumTasisatC1,txtCostC1.GetText(),txtDesignTasisatPercentC1.GetText());
                        SetCostValue(txtDesignSumShahrC1,txtCostC1.GetText(),txtDesignShahrPercentC1.GetText());
                        SetCostValue(txtDesignSumMapC1,txtCostC1.GetText(),txtDesignMapPercentC1.GetText());  
                        
                        SetCostValue(txtSupervisionSumArchC1,txtCostC1.GetText(),txtSupervisionArchPercentC1.GetText());
                        SetCostValue(txtSupervisionSumSazeC1,txtCostC1.GetText(),txtSupervisionSazePercentC1.GetText());
                        SetCostValue(txtSupervisionSumTasisatC1,txtCostC1.GetText(),txtSupervisionTasisatPercentC1.GetText());
                        SetCostValue(txtSupervisionSumShahrC1,txtCostC1.GetText(),txtSupervisionShahrPercentC1.GetText());
                        SetCostValue(txtSupervisionSumMapC1,txtCostC1.GetText(),txtSupervisionMapPercentC1.GetText());                                    
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">

                                            <TSPControls:CustomTextBox Text="0" ID="txtCostC2" runat="server"
                                                Width="100%" ClientInstanceName="txtCostC2">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchC2,txtCostC2.GetText(),txtDesignArchPercentC2.GetText());
                        SetCostValue(txtDesignSumSazeC2,txtCostC2.GetText(),txtDesignSazePercentC2.GetText());
                        SetCostValue(txtDesignSumTasisatC2,txtCostC2.GetText(),txtDesignTasisatPercentC2.GetText());
                        SetCostValue(txtDesignSumShahrC2,txtCostC2.GetText(),txtDesignShahrPercentC2.GetText());
                        SetCostValue(txtDesignSumMapC2,txtCostC2.GetText(),txtDesignMapPercentC2.GetText());   
                        
                        SetCostValue(txtSupervisionSumArchC2,txtCostC2.GetText(),txtSupervisionArchPercentC2.GetText());
                        SetCostValue(txtSupervisionSumSazeC2,txtCostC2.GetText(),txtSupervisionSazePercentC2.GetText());
                        SetCostValue(txtSupervisionSumTasisatC2,txtCostC2.GetText(),txtSupervisionTasisatPercentC2.GetText());
                        SetCostValue(txtSupervisionSumShahrC2,txtCostC2.GetText(),txtSupervisionShahrPercentC2.GetText());
                        SetCostValue(txtSupervisionSumMapC2,txtCostC2.GetText(),txtSupervisionMapPercentC2.GetText());                                 
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCostD1" runat="server"
                                                Width="100%" ClientInstanceName="txtCostD1">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchD1,txtCostD1.GetText(),txtDesignArchPercentD1.GetText());
                        SetCostValue(txtDesignSumSazeD1,txtCostD1.GetText(),txtDesignSazePercentD1.GetText());
                        SetCostValue(txtDesignSumTasisatD1,txtCostD1.GetText(),txtDesignTasisatPercentD1.GetText());
                        SetCostValue(txtDesignSumShahrD1,txtCostD1.GetText(),txtDesignShahrPercentD1.GetText());
                        SetCostValue(txtDesignSumMapD1,txtCostD1.GetText(),txtDesignMapPercentD1.GetText()); 
                        
                        SetCostValue(txtSupervisionSumArchD1,txtCostD1.GetText(),txtSupervisionArchPercentD1.GetText());
                        SetCostValue(txtSupervisionSumSazeD1,txtCostD1.GetText(),txtSupervisionSazePercentD1.GetText());
                        SetCostValue(txtSupervisionSumTasisatD1,txtCostD1.GetText(),txtSupervisionTasisatPercentD1.GetText());
                        SetCostValue(txtSupervisionSumShahrD1,txtCostD1.GetText(),txtSupervisionShahrPercentD1.GetText());
                        SetCostValue(txtSupervisionSumMapD1,txtCostD1.GetText(),txtSupervisionMapPercentD1.GetText());                                     
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCostD2" runat="server"
                                                Width="100%" ClientInstanceName="txtCostD2">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchD2,txtCostD2.GetText(),txtDesignArchPercentD2.GetText());
                        SetCostValue(txtDesignSumSazeD2,txtCostD2.GetText(),txtDesignSazePercentD2.GetText());
                        SetCostValue(txtDesignSumTasisatD2,txtCostD2.GetText(),txtDesignTasisatPercentD2.GetText());
                        SetCostValue(txtDesignSumShahrD2,txtCostD2.GetText(),txtDesignShahrPercentD2.GetText());
                        SetCostValue(txtDesignSumMapD2,txtCostD2.GetText(),txtDesignMapPercentD2.GetText()); 
                        
                        SetCostValue(txtSupervisionSumArchD2,txtCostD2.GetText(),txtSupervisionArchPercentD2.GetText());
                        SetCostValue(txtSupervisionSumSazeD2,txtCostD2.GetText(),txtSupervisionSazePercentD2.GetText());
                        SetCostValue(txtSupervisionSumTasisatD2,txtCostD2.GetText(),txtSupervisionTasisatPercentD2.GetText());
                        SetCostValue(txtSupervisionSumShahrD2,txtCostD2.GetText(),txtSupervisionShahrPercentD2.GetText());
                        SetCostValue(txtSupervisionSumMapD2,txtCostD2.GetText(),txtSupervisionMapPercentD2.GetText());                                   
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td colspan="2">
                                            <TSPControls:CustomTextBox Text="0" ID="txtCostD3" runat="server"
                                                Width="100%" ClientInstanceName="txtCostD3">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e){
                        SetCostValue(txtDesignSumArchD3,txtCostD3.GetText(),txtDesignArchPercentD3.GetText());
                        SetCostValue(txtDesignSumSazeD3,txtCostD3.GetText(),txtDesignSazePercentD3.GetText());
                        SetCostValue(txtDesignSumTasisatD3,txtCostD3.GetText(),txtDesignTasisatPercentD3.GetText());
                        SetCostValue(txtDesignSumShahrD3,txtCostD3.GetText(),txtDesignShahrPercentD3.GetText());
                        SetCostValue(txtDesignSumMapD3,txtCostD3.GetText(),txtDesignMapPercentD3.GetText()); 
                        
                        SetCostValue(txtSupervisionSumArchD3,txtCostD3.GetText(),txtSupervisionArchPercentD3.GetText());
                        SetCostValue(txtSupervisionSumSazeD3,txtCostD3.GetText(),txtSupervisionSazePercentD3.GetText());
                        SetCostValue(txtSupervisionSumTasisatD3,txtCostD3.GetText(),txtSupervisionTasisatPercentD3.GetText());
                        SetCostValue(txtSupervisionSumShahrD3,txtCostD3.GetText(),txtSupervisionShahrPercentD3.GetText());
                        SetCostValue(txtSupervisionSumMapD3,txtCostD3.GetText(),txtSupervisionMapPercentD3.GetText());                                     
                                    }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <%--*******************************************************************************--%>

                                    <%--*******************************************************************************--%>
                                    <tr>
                                        <td rowspan="2">
                                            <spin  style="font-weight:bold"  Class="VerticalText">طراحی</spin>
                                        </td>
                                        <td style="font-size: 7.5pt">مجموع درصد حق الزحمه طراحی
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; " class="SumSavePriceArchive">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentA1" ClientInstanceName="lblDesignSumPercentA1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA1,txtDesignArchPercentA1.GetText(),txtDesignSazePercentA1.GetText(),txtDesignTasisatPercentA1.GetText(),txtDesignShahrPercentA1.GetText(),txtDesignMapPercentA1.GetText());
                        SetCostValue(txtDesignSumArchA1,txtCostA1.GetText(),txtDesignArchPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentA1" Increment="0.1"
                                                            Font-Size="7pt" NumberType="Float" DecimalPlaces="3"
                                                            MinValue="0.0" MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA1,txtDesignArchPercentA1.GetText(),txtDesignSazePercentA1.GetText(),txtDesignTasisatPercentA1.GetText(),txtDesignShahrPercentA1.GetText(),txtDesignMapPercentA1.GetText());
                        SetCostValue(txtDesignSumSazeA1,txtCostA1.GetText(),txtDesignSazePercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA1,txtDesignArchPercentA1.GetText(),txtDesignSazePercentA1.GetText(),txtDesignTasisatPercentA1.GetText(),txtDesignShahrPercentA1.GetText(),txtDesignMapPercentA1.GetText());
                        SetCostValue(txtDesignSumTasisatA1,txtCostA1.GetText(),txtDesignTasisatPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            Font-Size="7pt" ClientInstanceName="txtDesignShahrPercentA1"
                                                            Increment="0.1" NumberType="Float" DecimalPlaces="3"
                                                            MinValue="0.0" MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA1,txtDesignArchPercentA1.GetText(),txtDesignSazePercentA1.GetText(),txtDesignTasisatPercentA1.GetText(),txtDesignShahrPercentA1.GetText(),txtDesignMapPercentA1.GetText());
                        SetCostValue(txtDesignSumShahrA1,txtCostA1.GetText(),txtDesignShahrPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA1,txtDesignArchPercentA1.GetText(),txtDesignSazePercentA1.GetText(),txtDesignTasisatPercentA1.GetText(),txtDesignShahrPercentA1.GetText(),txtDesignMapPercentA1.GetText());
                        SetCostValue(txtDesignSumMapA1,txtCostA1.GetText(),txtDesignMapPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; " class="SumSavePriceArchive">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentA" ClientInstanceName="lblDesignSumPercentA"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA,txtDesignArchPercentA.GetText(),txtDesignSazePercentA.GetText(),txtDesignTasisatPercentA.GetText(),txtDesignShahrPercentA.GetText(),txtDesignMapPercentA.GetText());
                        SetCostValue(txtDesignSumArchA,txtCostA.GetText(),txtDesignArchPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentA" Increment="0.1"
                                                            Font-Size="7pt" NumberType="Float" DecimalPlaces="3"
                                                            MinValue="0.0" MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA,txtDesignArchPercentA.GetText(),txtDesignSazePercentA.GetText(),txtDesignTasisatPercentA.GetText(),txtDesignShahrPercentA.GetText(),txtDesignMapPercentA.GetText());
                        SetCostValue(txtDesignSumSazeA,txtCostA.GetText(),txtDesignSazePercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA,txtDesignArchPercentA.GetText(),txtDesignSazePercentA.GetText(),txtDesignTasisatPercentA.GetText(),txtDesignShahrPercentA.GetText(),txtDesignMapPercentA.GetText());
                        SetCostValue(txtDesignSumTasisatA,txtCostA.GetText(),txtDesignTasisatPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            Font-Size="7pt" ClientInstanceName="txtDesignShahrPercentA"
                                                            Increment="0.1" NumberType="Float" DecimalPlaces="3"
                                                            MinValue="0.0" MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA,txtDesignArchPercentA.GetText(),txtDesignSazePercentA.GetText(),txtDesignTasisatPercentA.GetText(),txtDesignShahrPercentA.GetText(),txtDesignMapPercentA.GetText());
                        SetCostValue(txtDesignSumShahrA,txtCostA.GetText(),txtDesignShahrPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentA,txtDesignArchPercentA.GetText(),txtDesignSazePercentA.GetText(),txtDesignTasisatPercentA.GetText(),txtDesignShahrPercentA.GetText(),txtDesignMapPercentA.GetText());
                        SetCostValue(txtDesignSumMapA,txtCostA.GetText(),txtDesignMapPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; " class="SumSavePriceArchive">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentB" ClientInstanceName="lblDesignSumPercentB"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentB,txtDesignArchPercentB.GetText(),txtDesignSazePercentB.GetText(),txtDesignTasisatPercentB.GetText(),txtDesignShahrPercentB.GetText(),txtDesignMapPercentB.GetText());
                        SetCostValue(txtDesignSumArchB,txtCostB.GetText(),txtDesignArchPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentB,txtDesignArchPercentB.GetText(),txtDesignSazePercentB.GetText(),txtDesignTasisatPercentB.GetText(),txtDesignShahrPercentB.GetText(),txtDesignMapPercentB.GetText());
                        SetCostValue(txtDesignSumSazeB,txtCostB.GetText(),txtDesignSazePercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentB,txtDesignArchPercentB.GetText(),txtDesignSazePercentB.GetText(),txtDesignTasisatPercentB.GetText(),txtDesignShahrPercentB.GetText(),txtDesignMapPercentB.GetText());
                        SetCostValue(txtDesignSumTasisatB,txtCostB.GetText(),txtDesignTasisatPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignShahrPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentB,txtDesignArchPercentB.GetText(),txtDesignSazePercentB.GetText(),txtDesignTasisatPercentB.GetText(),txtDesignShahrPercentB.GetText(),txtDesignMapPercentB.GetText());
                        SetCostValue(txtDesignSumShahrB,txtCostB.GetText(),txtDesignShahrPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentB,txtDesignArchPercentB.GetText(),txtDesignSazePercentB.GetText(),txtDesignTasisatPercentB.GetText(),txtDesignShahrPercentB.GetText(),txtDesignMapPercentB.GetText());
                        SetCostValue(txtDesignSumMapB,txtCostB.GetText(),txtDesignMapPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentC1" ClientInstanceName="lblDesignSumPercentC1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC1,txtDesignArchPercentC1.GetText(),txtDesignSazePercentC1.GetText(),txtDesignTasisatPercentC1.GetText(),txtDesignShahrPercentC1.GetText(),txtDesignMapPercentC1.GetText());
                        SetCostValue(txtDesignSumArchC1,txtCostC1.GetText(),txtDesignArchPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC1,txtDesignArchPercentC1.GetText(),txtDesignSazePercentC1.GetText(),txtDesignTasisatPercentC1.GetText(),txtDesignShahrPercentC1.GetText(),txtDesignMapPercentC1.GetText());
                        SetCostValue(txtDesignSumSazeC1,txtCostC1.GetText(),txtDesignSazePercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC1,txtDesignArchPercentC1.GetText(),txtDesignSazePercentC1.GetText(),txtDesignTasisatPercentC1.GetText(),txtDesignShahrPercentC1.GetText(),txtDesignMapPercentC1.GetText());
                        SetCostValue(txtDesignSumTasisatC1,txtCostC1.GetText(),txtDesignTasisatPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignShahrPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC1,txtDesignArchPercentC1.GetText(),txtDesignSazePercentC1.GetText(),txtDesignTasisatPercentC1.GetText(),txtDesignShahrPercentC1.GetText(),txtDesignMapPercentC1.GetText());
                        SetCostValue(txtDesignSumShahrC1,txtCostC1.GetText(),txtDesignShahrPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC1,txtDesignArchPercentC1.GetText(),txtDesignSazePercentC1.GetText(),txtDesignTasisatPercentC1.GetText(),txtDesignShahrPercentC1.GetText(),txtDesignMapPercentC1.GetText());
                        SetCostValue(txtDesignSumMapC1,txtCostC1.GetText(),txtDesignMapPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentC2" ClientInstanceName="lblDesignSumPercentC2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC2,txtDesignArchPercentC2.GetText(),txtDesignSazePercentC2.GetText(),txtDesignTasisatPercentC2.GetText(),txtDesignShahrPercentC2.GetText(),txtDesignMapPercentC2.GetText());
                        SetCostValue(txtDesignSumArchC2,txtCostC2.GetText(),txtDesignArchPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC2,txtDesignArchPercentC2.GetText(),txtDesignSazePercentC2.GetText(),txtDesignTasisatPercentC2.GetText(),txtDesignShahrPercentC2.GetText(),txtDesignMapPercentC2.GetText());
                        SetCostValue(txtDesignSumSazeC2,txtCostC2.GetText(),txtDesignSazePercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC2,txtDesignArchPercentC2.GetText(),txtDesignSazePercentC2.GetText(),txtDesignTasisatPercentC2.GetText(),txtDesignShahrPercentC2.GetText(),txtDesignMapPercentC2.GetText());
                        SetCostValue(txtDesignSumTasisatC2,txtCostC2.GetText(),txtDesignTasisatPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignShahrPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC2,txtDesignArchPercentC2.GetText(),txtDesignSazePercentC2.GetText(),txtDesignTasisatPercentC2.GetText(),txtDesignShahrPercentC2.GetText(),txtDesignMapPercentC2.GetText());
                        SetCostValue(txtDesignSumShahrC2,txtCostC2.GetText(),txtDesignShahrPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" cu AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentC2,txtDesignArchPercentC2.GetText(),txtDesignSazePercentC2.GetText(),txtDesignTasisatPercentC2.GetText(),txtDesignShahrPercentC2.GetText(),txtDesignMapPercentC2.GetText());
                        SetCostValue(txtDesignSumMapC2,txtCostC2.GetText(),txtDesignMapPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentD1" ClientInstanceName="lblDesignSumPercentD1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD1,txtDesignArchPercentD1.GetText(),txtDesignSazePercentD1.GetText(),txtDesignTasisatPercentD1.GetText(),txtDesignShahrPercentD1.GetText(),txtDesignMapPercentD1.GetText());
                        SetCostValue(txtDesignSumArchD1,txtCostD1.GetText(),txtDesignArchPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD1,txtDesignArchPercentD1.GetText(),txtDesignSazePercentD1.GetText(),txtDesignTasisatPercentD1.GetText(),txtDesignShahrPercentD1.GetText(),txtDesignMapPercentD1.GetText());
                        SetCostValue(txtDesignSumSazeD1,txtCostD1.GetText(),txtDesignSazePercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD1,txtDesignArchPercentD1.GetText(),txtDesignSazePercentD1.GetText(),txtDesignTasisatPercentD1.GetText(),txtDesignShahrPercentD1.GetText(),txtDesignMapPercentD1.GetText());
                        SetCostValue(txtDesignSumTasisatD1,txtCostD1.GetText(),txtDesignTasisatPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignShahrPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD1,txtDesignArchPercentD1.GetText(),txtDesignSazePercentD1.GetText(),txtDesignTasisatPercentD1.GetText(),txtDesignShahrPercentD1.GetText(),txtDesignMapPercentD1.GetText());
                        SetCostValue(txtDesignSumShahrD1,txtCostD1.GetText(),txtDesignShahrPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD1,txtDesignArchPercentD1.GetText(),txtDesignSazePercentD1.GetText(),txtDesignTasisatPercentD1.GetText(),txtDesignShahrPercentD1.GetText(),txtDesignMapPercentD1.GetText());
                        SetCostValue(txtDesignSumMapD1,txtCostD1.GetText(),txtDesignMapPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentD2" ClientInstanceName="lblDesignSumPercentD2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD2,txtDesignArchPercentD2.GetText(),txtDesignSazePercentD2.GetText(),txtDesignTasisatPercentD2.GetText(),txtDesignShahrPercentD2.GetText(),txtDesignMapPercentD2.GetText());
                        SetCostValue(txtDesignSumArchD2,txtCostD2.GetText(),txtDesignArchPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD2,txtDesignArchPercentD2.GetText(),txtDesignSazePercentD2.GetText(),txtDesignTasisatPercentD2.GetText(),txtDesignShahrPercentD2.GetText(),txtDesignMapPercentD2.GetText());
                        SetCostValue(txtDesignSumSazeD2,txtCostD2.GetText(),txtDesignSazePercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD2,txtDesignArchPercentD2.GetText(),txtDesignSazePercentD2.GetText(),txtDesignTasisatPercentD2.GetText(),txtDesignShahrPercentD2.GetText(),txtDesignMapPercentD2.GetText());
                        SetCostValue(txtDesignSumTasisatD2,txtCostD2.GetText(),txtDesignTasisatPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignShahrPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD2,txtDesignArchPercentD2.GetText(),txtDesignSazePercentD2.GetText(),txtDesignTasisatPercentD2.GetText(),txtDesignShahrPercentD2.GetText(),txtDesignMapPercentD2.GetText());
                        SetCostValue(txtDesignSumShahrD2,txtCostD2.GetText(),txtDesignShahrPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD2,txtDesignArchPercentD2.GetText(),txtDesignSazePercentD2.GetText(),txtDesignTasisatPercentD2.GetText(),txtDesignShahrPercentD2.GetText(),txtDesignMapPercentD2.GetText());
                        SetCostValue(txtDesignSumMapD2,txtCostD2.GetText(),txtDesignMapPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentD3" ClientInstanceName="lblDesignSumPercentD3"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignArchPercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignArchPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD3,txtDesignArchPercentD3.GetText(),txtDesignSazePercentD3.GetText(),txtDesignTasisatPercentD3.GetText(),txtDesignShahrPercentD3.GetText(),txtDesignMapPercentD3.GetText());
                        SetCostValue(txtDesignSumArchD3,txtCostD3.GetText(),txtDesignArchPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignSazePercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignSazePercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD3,txtDesignArchPercentD3.GetText(),txtDesignSazePercentD3.GetText(),txtDesignTasisatPercentD3.GetText(),txtDesignShahrPercentD3.GetText(),txtDesignMapPercentD3.GetText());
                        SetCostValue(txtDesignSumSazeD3,txtCostD3.GetText(),txtDesignSazePercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignTasisatPercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignTasisatPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD3,txtDesignArchPercentD3.GetText(),txtDesignSazePercentD3.GetText(),txtDesignTasisatPercentD3.GetText(),txtDesignShahrPercentD3.GetText(),txtDesignMapPercentD3.GetText());
                        SetCostValue(txtDesignSumTasisatD3,txtCostD3.GetText(),txtDesignTasisatPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignShahrPercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px"  AllowNull="False"
                                                            ClientInstanceName="txtDesignShahrPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD3,txtDesignArchPercentD3.GetText(),txtDesignSazePercentD3.GetText(),txtDesignTasisatPercentD3.GetText(),txtDesignShahrPercentD3.GetText(),txtDesignMapPercentD3.GetText());
                        SetCostValue(txtDesignSumShahrD3,txtCostD3.GetText(),txtDesignShahrPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtDesignMapPercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtDesignMapPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblDesignSumPercentD3,txtDesignArchPercentD3.GetText(),txtDesignSazePercentD3.GetText(),txtDesignTasisatPercentD3.GetText(),txtDesignShahrPercentD3.GetText(),txtDesignMapPercentD3.GetText());
                        SetCostValue(txtDesignSumMapD3,txtCostD3.GetText(),txtDesignMapPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>حق الزحمه طراحی (ریال)
                                        </td>
                                            <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchA" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeA" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatA" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrA" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapA" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchB" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeB" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatB" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrB" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapB" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumArchD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumArchD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumSazeD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumSazeD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumTasisatD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumTasisatD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumShahrD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumShahrD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtDesignSumMapD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtDesignSumMapD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--*************************************************************************************--%>
                                    <tr>
                                        <td rowspan="2">
                                            <spin  style="font-weight:bold"  Class="VerticalText">نظارت</spin>
                                   
                                        </td>
                                        <td style="font-size: 7.5pt">مجموع درصد حق الزحمه نظارت
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentA1" ClientInstanceName="lblSupervisionSumPercentA1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA1,txtSupervisionArchPercentA1.GetText(),txtSupervisionSazePercentA1.GetText(),txtSupervisionTasisatPercentA1.GetText(),txtSupervisionShahrPercentA1.GetText(),txtSupervisionMapPercentA1.GetText());
                        SetCostValue(txtSupervisionSumArchA1,txtCostA1.GetText(),txtSupervisionArchPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA1,txtSupervisionArchPercentA1.GetText(),txtSupervisionSazePercentA1.GetText(),txtSupervisionTasisatPercentA1.GetText(),txtSupervisionShahrPercentA1.GetText(),txtSupervisionMapPercentA1.GetText());
                        SetCostValue(txtSupervisionSumSazeA1,txtCostA1.GetText(),txtSupervisionSazePercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentA1" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA1,txtSupervisionArchPercentA1.GetText(),txtSupervisionSazePercentA1.GetText(),txtSupervisionTasisatPercentA1.GetText(),txtSupervisionShahrPercentA1.GetText(),txtSupervisionMapPercentA1.GetText());
                        SetCostValue(txtSupervisionSumTasisatA1,txtCostA1.GetText(),txtSupervisionTasisatPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA1,txtSupervisionArchPercentA1.GetText(),txtSupervisionSazePercentA1.GetText(),txtSupervisionTasisatPercentA1.GetText(),txtSupervisionShahrPercentA1.GetText(),txtSupervisionMapPercentA1.GetText());
                        SetCostValue(txtSupervisionSumShahrA1,txtCostA1.GetText(),txtSupervisionShahrPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentA1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentA1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA1,txtSupervisionArchPercentA1.GetText(),txtSupervisionSazePercentA1.GetText(),txtSupervisionTasisatPercentA1.GetText(),txtSupervisionShahrPercentA1.GetText(),txtSupervisionMapPercentA1.GetText());
                        SetCostValue(txtSupervisionSumMapA1,txtCostA1.GetText(),txtSupervisionMapPercentA1.GetText());
                        SetSumAllPercent(lblSumAllPercentA1,lblDesignSumPercentA1.GetText(),lblSupervisionSumPercentA1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentA" ClientInstanceName="lblSupervisionSumPercentA"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" Font-Size="7pt" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA,txtSupervisionArchPercentA.GetText(),txtSupervisionSazePercentA.GetText(),txtSupervisionTasisatPercentA.GetText(),txtSupervisionShahrPercentA.GetText(),txtSupervisionMapPercentA.GetText());
                        SetCostValue(txtSupervisionSumArchA,txtCostA.GetText(),txtSupervisionArchPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA,txtSupervisionArchPercentA.GetText(),txtSupervisionSazePercentA.GetText(),txtSupervisionTasisatPercentA.GetText(),txtSupervisionShahrPercentA.GetText(),txtSupervisionMapPercentA.GetText());
                        SetCostValue(txtSupervisionSumSazeA,txtCostA.GetText(),txtSupervisionSazePercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentA" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA,txtSupervisionArchPercentA.GetText(),txtSupervisionSazePercentA.GetText(),txtSupervisionTasisatPercentA.GetText(),txtSupervisionShahrPercentA.GetText(),txtSupervisionMapPercentA.GetText());
                        SetCostValue(txtSupervisionSumTasisatA,txtCostA.GetText(),txtSupervisionTasisatPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA,txtSupervisionArchPercentA.GetText(),txtSupervisionSazePercentA.GetText(),txtSupervisionTasisatPercentA.GetText(),txtSupervisionShahrPercentA.GetText(),txtSupervisionMapPercentA.GetText());
                        SetCostValue(txtSupervisionSumShahrA,txtCostA.GetText(),txtSupervisionShahrPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentA" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentA" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentA,txtSupervisionArchPercentA.GetText(),txtSupervisionSazePercentA.GetText(),txtSupervisionTasisatPercentA.GetText(),txtSupervisionShahrPercentA.GetText(),txtSupervisionMapPercentA.GetText());
                        SetCostValue(txtSupervisionSumMapA,txtCostA.GetText(),txtSupervisionMapPercentA.GetText());
                        SetSumAllPercent(lblSumAllPercentA,lblDesignSumPercentA.GetText(),lblSupervisionSumPercentA.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentB" ClientInstanceName="lblSupervisionSumPercentB"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentB,txtSupervisionArchPercentB.GetText(),txtSupervisionSazePercentB.GetText(),txtSupervisionTasisatPercentB.GetText(),txtSupervisionShahrPercentB.GetText(),txtSupervisionMapPercentB.GetText());
                        SetCostValue(txtSupervisionSumArchB,txtCostB.GetText(),txtSupervisionArchPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentB,txtSupervisionArchPercentB.GetText(),txtSupervisionSazePercentB.GetText(),txtSupervisionTasisatPercentB.GetText(),txtSupervisionShahrPercentB.GetText(),txtSupervisionMapPercentB.GetText());
                        SetCostValue(txtSupervisionSumSazeB,txtCostB.GetText(),txtSupervisionSazePercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentB" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentB,txtSupervisionArchPercentB.GetText(),txtSupervisionSazePercentB.GetText(),txtSupervisionTasisatPercentB.GetText(),txtSupervisionShahrPercentB.GetText(),txtSupervisionMapPercentB.GetText());
                        SetCostValue(txtSupervisionSumTasisatB,txtCostB.GetText(),txtSupervisionTasisatPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentB,txtSupervisionArchPercentB.GetText(),txtSupervisionSazePercentB.GetText(),txtSupervisionTasisatPercentB.GetText(),txtSupervisionShahrPercentB.GetText(),txtSupervisionMapPercentB.GetText());
                        SetCostValue(txtSupervisionSumShahrB,txtCostB.GetText(),txtSupervisionShahrPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentB" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentB" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentB,txtSupervisionArchPercentB.GetText(),txtSupervisionSazePercentB.GetText(),txtSupervisionTasisatPercentB.GetText(),txtSupervisionShahrPercentB.GetText(),txtSupervisionMapPercentB.GetText());
                        SetCostValue(txtSupervisionSumMapB,txtCostB.GetText(),txtSupervisionMapPercentB.GetText());
                        SetSumAllPercent(lblSumAllPercentB,lblDesignSumPercentB.GetText(),lblSupervisionSumPercentB.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentC1" ClientInstanceName="lblSupervisionSumPercentC1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC1,txtSupervisionArchPercentC1.GetText(),txtSupervisionSazePercentC1.GetText(),txtSupervisionTasisatPercentC1.GetText(),txtSupervisionShahrPercentC1.GetText(),txtSupervisionMapPercentC1.GetText());
                        SetCostValue(txtSupervisionSumArchC1,txtCostC1.GetText(),txtSupervisionArchPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC1,txtSupervisionArchPercentC1.GetText(),txtSupervisionSazePercentC1.GetText(),txtSupervisionTasisatPercentC1.GetText(),txtSupervisionShahrPercentC1.GetText(),txtSupervisionMapPercentC1.GetText());
                        SetCostValue(txtSupervisionSumSazeC1,txtCostC1.GetText(),txtSupervisionSazePercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentC1" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC1,txtSupervisionArchPercentC1.GetText(),txtSupervisionSazePercentC1.GetText(),txtSupervisionTasisatPercentC1.GetText(),txtSupervisionShahrPercentC1.GetText(),txtSupervisionMapPercentC1.GetText());
                        SetCostValue(txtSupervisionSumTasisatC1,txtCostC1.GetText(),txtSupervisionTasisatPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentC1" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC1,txtSupervisionArchPercentC1.GetText(),txtSupervisionSazePercentC1.GetText(),txtSupervisionTasisatPercentC1.GetText(),txtSupervisionShahrPercentC1.GetText(),txtSupervisionMapPercentC1.GetText());
                        SetCostValue(txtSupervisionSumShahrC1,txtCostC1.GetText(),txtSupervisionShahrPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentC1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentC1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC1,txtSupervisionArchPercentC1.GetText(),txtSupervisionSazePercentC1.GetText(),txtSupervisionTasisatPercentC1.GetText(),txtSupervisionShahrPercentC1.GetText(),txtSupervisionMapPercentC1.GetText());
                        SetCostValue(txtSupervisionSumMapC1,txtCostC1.GetText(),txtSupervisionMapPercentC1.GetText());
                        SetSumAllPercent(lblSumAllPercentC1,lblDesignSumPercentC1.GetText(),lblSupervisionSumPercentC1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentC2" ClientInstanceName="lblSupervisionSumPercentC2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC2,txtSupervisionArchPercentC2.GetText(),txtSupervisionSazePercentC2.GetText(),txtSupervisionTasisatPercentC2.GetText(),txtSupervisionShahrPercentC2.GetText(),txtSupervisionMapPercentC2.GetText());
                        SetCostValue(txtSupervisionSumArchC2,txtCostC2.GetText(),txtSupervisionArchPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC2,txtSupervisionArchPercentC2.GetText(),txtSupervisionSazePercentC2.GetText(),txtSupervisionTasisatPercentC2.GetText(),txtSupervisionShahrPercentC2.GetText(),txtSupervisionMapPercentC2.GetText());
                        SetCostValue(txtSupervisionSumSazeC2,txtCostC2.GetText(),txtSupervisionSazePercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentC2" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC2,txtSupervisionArchPercentC2.GetText(),txtSupervisionSazePercentC2.GetText(),txtSupervisionTasisatPercentC2.GetText(),txtSupervisionShahrPercentC2.GetText(),txtSupervisionMapPercentC2.GetText());
                        SetCostValue(txtSupervisionSumTasisatC2,txtCostC2.GetText(),txtSupervisionTasisatPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentC2" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC2,txtSupervisionArchPercentC2.GetText(),txtSupervisionSazePercentC2.GetText(),txtSupervisionTasisatPercentC2.GetText(),txtSupervisionShahrPercentC2.GetText(),txtSupervisionMapPercentC2.GetText());
                        SetCostValue(txtSupervisionSumShahrC2,txtCostC2.GetText(),txtSupervisionShahrPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentC2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentC2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentC2,txtSupervisionArchPercentC2.GetText(),txtSupervisionSazePercentC2.GetText(),txtSupervisionTasisatPercentC2.GetText(),txtSupervisionShahrPercentC2.GetText(),txtSupervisionMapPercentC2.GetText());
                        SetCostValue(txtSupervisionSumMapC2,txtCostC2.GetText(),txtSupervisionMapPercentC2.GetText());
                        SetSumAllPercent(lblSumAllPercentC2,lblDesignSumPercentC2.GetText(),lblSupervisionSumPercentC2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentD1" ClientInstanceName="lblSupervisionSumPercentD1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD1,txtSupervisionArchPercentD1.GetText(),txtSupervisionSazePercentD1.GetText(),txtSupervisionTasisatPercentD1.GetText(),txtSupervisionShahrPercentD1.GetText(),txtSupervisionMapPercentD1.GetText());
                        SetCostValue(txtSupervisionSumArchD1,txtCostD1.GetText(),txtSupervisionArchPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD1,txtSupervisionArchPercentD1.GetText(),txtSupervisionSazePercentD1.GetText(),txtSupervisionTasisatPercentD1.GetText(),txtSupervisionShahrPercentD1.GetText(),txtSupervisionMapPercentD1.GetText());
                        SetCostValue(txtSupervisionSumSazeD1,txtCostD1.GetText(),txtSupervisionSazePercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentD1" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD1,txtSupervisionArchPercentD1.GetText(),txtSupervisionSazePercentD1.GetText(),txtSupervisionTasisatPercentD1.GetText(),txtSupervisionShahrPercentD1.GetText(),txtSupervisionMapPercentD1.GetText());
                        SetCostValue(txtSupervisionSumTasisatD1,txtCostD1.GetText(),txtSupervisionTasisatPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentD1" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD1,txtSupervisionArchPercentD1.GetText(),txtSupervisionSazePercentD1.GetText(),txtSupervisionTasisatPercentD1.GetText(),txtSupervisionShahrPercentD1.GetText(),txtSupervisionMapPercentD1.GetText());
                        SetCostValue(txtSupervisionSumShahrD1,txtCostD1.GetText(),txtSupervisionShahrPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentD1" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentD1" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD1,txtSupervisionArchPercentD1.GetText(),txtSupervisionSazePercentD1.GetText(),txtSupervisionTasisatPercentD1.GetText(),txtSupervisionShahrPercentD1.GetText(),txtSupervisionMapPercentD1.GetText());
                        SetCostValue(txtSupervisionSumMapD1,txtCostD1.GetText(),txtSupervisionMapPercentD1.GetText());
                        SetSumAllPercent(lblSumAllPercentD1,lblDesignSumPercentD1.GetText(),lblSupervisionSumPercentD1.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentD2" ClientInstanceName="lblSupervisionSumPercentD2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD2,txtSupervisionArchPercentD2.GetText(),txtSupervisionSazePercentD2.GetText(),txtSupervisionTasisatPercentD2.GetText(),txtSupervisionShahrPercentD2.GetText(),txtSupervisionMapPercentD2.GetText());
                        SetCostValue(txtSupervisionSumArchD2,txtCostD2.GetText(),txtSupervisionArchPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD2,txtSupervisionArchPercentD2.GetText(),txtSupervisionSazePercentD2.GetText(),txtSupervisionTasisatPercentD2.GetText(),txtSupervisionShahrPercentD2.GetText(),txtSupervisionMapPercentD2.GetText());
                        SetCostValue(txtSupervisionSumSazeD2,txtCostD2.GetText(),txtSupervisionSazePercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentD2" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD2,txtSupervisionArchPercentD2.GetText(),txtSupervisionSazePercentD2.GetText(),txtSupervisionTasisatPercentD2.GetText(),txtSupervisionShahrPercentD2.GetText(),txtSupervisionMapPercentD2.GetText());
                        SetCostValue(txtSupervisionSumTasisatD2,txtCostD2.GetText(),txtSupervisionTasisatPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentD2" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD2,txtSupervisionArchPercentD2.GetText(),txtSupervisionSazePercentD2.GetText(),txtSupervisionTasisatPercentD2.GetText(),txtSupervisionShahrPercentD2.GetText(),txtSupervisionMapPercentD2.GetText());
                        SetCostValue(txtSupervisionSumShahrD2,txtCostD2.GetText(),txtSupervisionShahrPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentD2" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentD2" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD2,txtSupervisionArchPercentD2.GetText(),txtSupervisionSazePercentD2.GetText(),txtSupervisionTasisatPercentD2.GetText(),txtSupervisionShahrPercentD2.GetText(),txtSupervisionMapPercentD2.GetText());
                        SetCostValue(txtSupervisionSumMapD2,txtCostD2.GetText(),txtSupervisionMapPercentD2.GetText());
                        SetSumAllPercent(lblSumAllPercentD2,lblDesignSumPercentD2.GetText(),lblSupervisionSumPercentD2.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentD3" ClientInstanceName="lblSupervisionSumPercentD3"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionArchPercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionArchPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD3,txtSupervisionArchPercentD3.GetText(),txtSupervisionSazePercentD3.GetText(),txtSupervisionTasisatPercentD3.GetText(),txtSupervisionShahrPercentD3.GetText(),txtSupervisionMapPercentD3.GetText());
                        SetCostValue(txtSupervisionSumArchD3,txtCostD3.GetText(),txtSupervisionArchPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionSazePercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionSazePercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD3,txtSupervisionArchPercentD3.GetText(),txtSupervisionSazePercentD3.GetText(),txtSupervisionTasisatPercentD3.GetText(),txtSupervisionShahrPercentD3.GetText(),txtSupervisionMapPercentD3.GetText());
                        SetCostValue(txtSupervisionSumSazeD3,txtCostD3.GetText(),txtSupervisionSazePercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionTasisatPercentD3" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionTasisatPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD3,txtSupervisionArchPercentD3.GetText(),txtSupervisionSazePercentD3.GetText(),txtSupervisionTasisatPercentD3.GetText(),txtSupervisionShahrPercentD3.GetText(),txtSupervisionMapPercentD3.GetText());
                        SetCostValue(txtSupervisionSumTasisatD3,txtCostD3.GetText(),txtSupervisionTasisatPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionShahrPercentD3" runat="server" Height="21px"
                                                            Number="0.0" Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionShahrPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD3,txtSupervisionArchPercentD3.GetText(),txtSupervisionSazePercentD3.GetText(),txtSupervisionTasisatPercentD3.GetText(),txtSupervisionShahrPercentD3.GetText(),txtSupervisionMapPercentD3.GetText());
                        SetCostValue(txtSupervisionSumShahrD3,txtCostD3.GetText(),txtSupervisionShahrPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxSpinEdit ID="txtSupervisionMapPercentD3" runat="server" Height="21px" Number="0.0"
                                                            Width="50px" AllowNull="False"
                                                            ClientInstanceName="txtSupervisionMapPercentD3" Increment="0.1"
                                                            NumberType="Float" DecimalPlaces="3" MinValue="0.0"
                                                            MaxValue="100.00" Font-Size="7pt">
                                                            <ClientSideEvents NumberChanged="function(s,e){
                        SetSumPercentValue(lblSupervisionSumPercentD3,txtSupervisionArchPercentD3.GetText(),txtSupervisionSazePercentD3.GetText(),txtSupervisionTasisatPercentD3.GetText(),txtSupervisionShahrPercentD3.GetText(),txtSupervisionMapPercentD3.GetText());
                        SetCostValue(txtSupervisionSumMapD3,txtCostD3.GetText(),txtSupervisionMapPercentD3.GetText());
                        SetSumAllPercent(lblSumAllPercentD3,lblDesignSumPercentD3.GetText(),lblSupervisionSumPercentD3.GetText());
                        }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>حق الزحمه نظارت (ریال)
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                       <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapA1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapA1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                         <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordA" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchA" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeA" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatA" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrA" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapA" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapA">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                               <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordB" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchB" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeB" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatB" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrB" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapB" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapB">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                            <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapC1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapC1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                               <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapC2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapC2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                      <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapD1" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapD1">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                           <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapD2" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapD2">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                                       <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumCordD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumCordD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumArchD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumArchD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumSazeD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumSazeD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumTasisatD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumTasisatD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumShahrD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumShahrD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomTextBox Text="0" ID="txtSupervisionSumMapD3" runat="server"
                                                            Width="100%" ClientInstanceName="txtSupervisionSumMapD3">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">مجموع درصد حق الزحمه های طراحی و نظارت
                                        </td>
                                               <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentA1" ClientInstanceName="lblSumAllPercentA1" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentA" ClientInstanceName="lblSumAllPercentA" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentB" ClientInstanceName="lblSumAllPercentB" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentC1" ClientInstanceName="lblSumAllPercentC1" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentC2" ClientInstanceName="lblSumAllPercentC2" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentD1" ClientInstanceName="lblSumAllPercentD1" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentD2" ClientInstanceName="lblSumAllPercentD2" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel ID="lblSumAllPercentD3" ClientInstanceName="lblSumAllPercentD3" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tr>
                                <td style="vertical-align: top;" align="right">
                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                        cellpadding="0">
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                    EnableTheming="False" ToolTip="جدید" ID="btnNew2" EnableViewState="False" OnClick="btnNew_Click"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                    Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                    OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " EnableTheming="False"
                                                    ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click" CausesValidation="true"
                                                    UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/save.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                    EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False"
                                                    OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                    <Image Url="~/Images/icons/Back.png">
                                                    </Image>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                    </HoverStyle>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="hiddenSaveID" runat="server">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG2" src="../../../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
