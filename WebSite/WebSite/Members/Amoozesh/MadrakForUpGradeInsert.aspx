<%@ Page Title="مشخصات دوره/همایش خارج از سازمان" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="MadrakForUpGradeInsert.aspx.cs" Inherits="Members_Amoozesh_MadrakForUpGradeInsert" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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

    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div runat="server" id="DivReport" style="text-align: right" class="DivErrors">
                <asp:Label runat="server" Text="Label" ID="LabelWarning"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="BtnNew" CssClass="ButtonMenue" CausesValidation="false" OnClick="BtnNew_Click" runat="server">مدرک جدید</asp:LinkButton>

                                    </td>

                                    <td>

                                        <TSPControls:CustomAspxButton ID="btnSave" CssClass="ButtonMenue" runat="server" UseSubmitBehavior="False" Text="ذخیره"
                                            EnableTheming="False" ToolTip="ذخیره" EnableViewState="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s,e){
 if (HiddenFieldImg.Get('ImgPeriod')==0)
 {
  alert(&quot;بارگذاری تصویر گواهینامه الزامی می باشد&quot;); 
  e.processOnServer= false;     
  return;
}
   if (ASPxClientEdit.ValidateGroup() == false)
   {
        return false;                                       
    }                                 
	 e.processOnServer= confirm('بعد از ذخیره اطلاعات قادر به اعمال تغییرات نیستید.آیا مطمئن به ذخیره اطلاعات می باشید؟');
  
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="btnBack" CssClass="ButtonMenue" runat="server" UseSubmitBehavior="False" Text="مدیریت مدارک دوره/همایش خارج از سازمان"
                                            EnableTheming="False" ToolTip="ذخیره" EnableViewState="False" CausesValidation="false" OnClick="btnBack_Click">
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
                                    <td valign="top" align="right" width="20%">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="کد عضویت *" ClientInstanceName="lblMe"
                                            ID="lblMeId">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="30%">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="ID" ID="txtMeNo" ClientEnabled="false" AutoPostBack="true">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" width="20%"></td>
                                    <td valign="top" align="right" width="30%"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ClientInstanceName="lblMname" ID="lblMeFirstName">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox ClientEnabled="false" runat="server" Width="100%" ReadOnly="True"
                                            ClientInstanceName="mFirstName" ID="txtMeFirstName">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="نام خانوادگی" ClientInstanceName="lblMfamily"
                                            ID="lblMeLastName">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox ClientEnabled="false" runat="server" Width="100%" ReadOnly="True"
                                            ClientInstanceName="mLastName" ID="txtMeLastName">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" ClientEnabled="false" IncrementalFilteringMode="StartsWith"
                                            SelectedIndex="0" ValueType="System.String"
                                            Width="100%"
                                            ClientInstanceName="cmb" ID="ComboType" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />

                                            <Items>
                                                <dxe:ListEditItem Text="دوره/همایش" Value="0" Selected="True"></dxe:ListEditItem>
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="شماره گواهینامه *" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="LiNo" ID="txtLiNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="vgUpdate">
                                                <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ گواهینامه *" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtLiDate"
                                            Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLiDate" ErrorMessage="تاریخ را وارد نمایید"
                                            Display="Dynamic" ID="RequiredFieldValidator4">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="عنوان *" ID="ASPxLabel7">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" DropDownWidth="100%" EnableIncrementalFiltering="True"
                                            IncrementalFilteringMode="Contains" ValueType="System.Int32" DataSourceID="odbCourseName"
                                            TextField="CrsTitle" ValueField="CrsId" TextFormatString="{2}" Width="100%"
                                            ClientInstanceName="crsId" ID="ComboCrsName" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="NonPracticalDuration" Width="80px" Caption="تعداد ساعات">
                                                </dxe:ListBoxColumn>
                                                <dxe:ListBoxColumn FieldName="CrsCode" Width="80px" Caption="کد درس"></dxe:ListBoxColumn>
                                                <dxe:ListBoxColumn FieldName="CrsTitle" Width="420px" Caption="عنوان"></dxe:ListBoxColumn>
                                            </Columns>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="عنوان مدرک را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد دوره" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="PPCode" ID="txtPPCode">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <%--  <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="عنوان دوره/همایش *" ID="ASPxLabel21">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox  runat="server" Width="100%"
                                            ClientInstanceName="SeName" ID="txtSeName">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="عنوان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="ارائه دهنده *" ID="ASPxLabel23">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="SeTeName" ID="txtSeTeName">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ارائه دهنده را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ شروع *" ID="ASPxLabel10">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtStartDate"
                                            Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStartDate" ErrorMessage="تاریخ را وارد نمایید"
                                            Display="Dynamic" ID="RequiredFieldValidator2">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ پایان *" ID="ASPxLabel12">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtEndDate"
                                            Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEndDate" ErrorMessage="تاریخ را وارد نمایید"
                                            Display="Dynamic" ID="RequiredFieldValidator3">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="امتیاز اخذ شده(ساعت)" ID="ASPxLabel24">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="TimeMark" ID="txtTimeMark">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="امتیاز کل *" ID="ASPxLabel25">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="TotalMark" ID="txtTotalMark">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="امتیاز کل را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="مدت زمان دوره/همایش(ساعت) *" ID="ASPxLabel11">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="6"
                                            ClientInstanceName="PPDuration" ID="txtPPDuration">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مدت زمان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ آزمون*" ID="ASPxLabel20">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtTestDate"
                                            Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTestDate" ErrorMessage="تاریخ را وارد نمایید"
                                            Display="Dynamic" ID="RequiredFieldValidator1">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام مؤسسه/ارگان برگزار کننده *" ID="ASPxLabel13">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="InsName" ID="txtInsName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مؤسسه/ارگان برگزار کننده را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="استان" ID="ASPxLabel16">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                            ValueType="System.String" DataSourceID="OdbProvince" TextField="PrName" ValueField="PrId"
                                            Width="100%"
                                            ClientInstanceName="PrId"
                                            ID="ComboPrId" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings Display="Dynamic" CausesValidation="true" ErrorText="استان محل برگزاری را انتخاب نمایید" ErrorTextPosition="Bottom">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="" IsRequired="true"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="شماره مجوز فعالیت" ID="ASPxLabel14">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="InsRegNo" ID="txtInsRegNo" Style="direction: ltr">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="مکان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ اخذ مجوز" ID="ASPxLabel15">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                            Width="230px" ID="txtInsRegDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مدرس دوره *" ID="ASPxLabel17">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="PPTeName" ID="txtPPTeName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مدرس را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Wrap="False" Text="پروانه اشتغال به کار" ID="ASPxLabel18">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ClientInstanceName="TeFileNo" ID="txtTeFileNo" Style="direction: ltr">
                                            <ValidationSettings ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="مکان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>قصد استفاده از این گواهینامه جهت ارتقاء کدام صلاحیت را داردید؟</td>
                                    <td>  <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                            ValueType="System.String" DataSourceID="ObjectDataSourceResponsibility" TextField="ResName" ValueField="ResId"
                                            Width="100%"
                                            ClientInstanceName="comboRes"
                                            ID="comboRes" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings Display="Dynamic" CausesValidation="true" ErrorText="صلاحیت را انتخاب نمایید" ErrorTextPosition="Bottom">

                                                <ErrorFrameStyle ImageSpacing="4px" >
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="" IsRequired="true"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        
            <asp:ObjectDataSource runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetAllData" TypeName="TSP.DataManager.ResponcibilityTypeManager" ID="ObjectDataSourceResponsibility">
                <FilterParameters>
                    <asp:Parameter Name="EpId"></asp:Parameter>
                </FilterParameters>
            </asp:ObjectDataSource>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label3"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="32px" Width="100%"
                                            ClientInstanceName="Desc" ID="txtDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر گواهینامه" ID="Label1"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right">


                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true" InputType="Files"
                                                            ClientInstanceName="flpc" ID="flpImage" OnFileUploadComplete="flpImage_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
	if(e.isValid)
    {                                                                    
    imgEndUploadImgClient.SetVisible(true);                                                                       
	hp.SetVisible(true);                                                                    
	hp.SetNavigateUrl('../../Image/Amoozesh/Madrak/'+e.callbackData);   
                                                                
	HiddenFieldImg.Set('ImgPeriod',1);
    }
    else {
                                                                
	HiddenFieldImg.Set('ImgPeriod',0);
    imgEndUploadImgClient.SetVisible(false);
                                                                }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td valign="middle" align="right">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClient" ClientVisible="False" ID="imgEndUploadImg">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" Target="_blank" ClientInstanceName="hp"
                                            ClientVisible="False" ID="HpFile">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <asp:HiddenField runat="server" ID="MadrakId" Visible="False"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="PgMode" Visible="False"></asp:HiddenField>

            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldImg" ClientInstanceName="HiddenFieldImg">
            </dxhf:ASPxHiddenField>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="BtnNew2" CssClass="ButtonMenue" runat="server" UseSubmitBehavior="False" Text="مدرک جدید"
                                            EnableTheming="False" ToolTip="ذخیره" EnableViewState="False" CausesValidation="false" OnClick="BtnNew_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td>

                                        <TSPControls:CustomAspxButton ID="btnSave2" CssClass="ButtonMenue" runat="server" UseSubmitBehavior="False" Text="ذخیره"
                                            EnableTheming="False" ToolTip="ذخیره" EnableViewState="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s,e){
 if (HiddenFieldImg.Get('ImgPeriod')==0)
 {
  alert(&quot;بارگذاری تصویر گواهینامه الزامی می باشد&quot;); 
  e.processOnServer= false;                                                         
  } else
  {                                  
	 e.processOnServer= confirm('بعد از ذخیره اطلاعات قادر به اعمال تغییرات نیستید.آیا مطمئن به ذخیره اطلاعات می باشید؟');
  }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="btnBack2" CssClass="ButtonMenue" runat="server" UseSubmitBehavior="False" Text="مدیریت مدارک دوره/همایش خارج از سازمان"
                                            EnableTheming="False" ToolTip="ذخیره" EnableViewState="False" CausesValidation="false" OnClick="btnBack_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="SelectCourseByTrainingAcceptedGrade" TypeName="TSP.DataManager.CourseManager" ID="odbCourseName">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="InActiveUpGradePoint" Type="Int32"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.ProvinceManager" ID="OdbProvince">
                <FilterParameters>
                    <asp:Parameter Name="EpId"></asp:Parameter>
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource runat="server" CacheDuration="30" DeleteMethod="Delete" EnableCaching="True"
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectMemberByName"
                TypeName="TSP.DataManager.MemberManager" UpdateMethod="Update" ID="OdbLastName">

                <SelectParameters>
                    <asp:Parameter DefaultValue="%" Name="FirstName" Type="String"></asp:Parameter>
                    <asp:Parameter DefaultValue="" Name="LastName" Type="String"></asp:Parameter>
                </SelectParameters>

            </asp:ObjectDataSource>

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
