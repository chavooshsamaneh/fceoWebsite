<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddImplementDoc.aspx.cs" Inherits="Employee_ImplementDoc_AddImplementDoc"
    Title="مشخصات مجوز فعالیت مجری حقیقی" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SetMeDocDefualtExpireDateJS() {
            CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());
        }
    </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                    CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                    FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                    WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                </pdc:PersianDateScriptManager>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                               
                                                <Image  Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">
                                              
                                                <Image  Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>                                    
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                              
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
                <div style="width: 100%" dir="rtl" align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server"  
                        OnItemClick="MenuMemberFile_ItemClick"
                        AutoSeparators="RootOnly">
                        <Items>
                            <dxm:MenuItem Selected="True" Text="مشخصات مجوز">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="سابقه کار" Name="JobHistory" Visible="false">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="توان مالی" Name="Financial">
                            </dxm:MenuItem>
                        </Items>
                     
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberFile" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" dir="rtl" align="center">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                                ForeColor="Red">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelTransfer" HeaderText="مشخصات عضو"
                                runat="server" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table id="Table4" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="15%" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel2" ClientInstanceName="lblPr">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td width="35%" valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeId"  Width="100%" AutoPostBack="True"
                                                             OnTextChanged="txtMeId_TextChanged">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*">
                                                                </RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: middle" dir="rtl" align="center" colspan="2" rowspan="4">
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ImageUrl="~/Images/Person.png"
                                                            ID="ImgMember">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel16">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" dir="rtl" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="lblMeName"  Width="100%" ReadOnly="True"
                                                            >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel18">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" dir="rtl" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="lblMeLastName"  Width="100%"
                                                            ReadOnly="True" >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel10" ClientInstanceName="lblDate">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="lblMFNo"  Width="100%" ReadOnly="True"
                                                             Style="direction: ltr">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="رشته موضوع پروانه" ID="ASPxLabel11">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMemberFileMajor"  Width="100%"
                                                            ReadOnly="True" >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                    </td>
                                                    <td valign="top" align="right">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" width="15%" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel6">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" width="35%" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtObsName"  Width="100%" ReadOnly="True"
                                                            >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" width="15%" align="right">
                                                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="تاریخ اعتبار پروانه" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" width="35%" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Style="direction: ltr" Width="100%" ReadOnly="true"
                                                              ID="txtExpireDateMember">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtImpName"  Width="100%" ReadOnly="True"
                                                            >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel9">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDesName"  Width="100%" ReadOnly="True"
                                                            >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table runat="server" id="TblTransfer" width="100%" dir="rtl">
                                            <tr>
                                                <td style="width: 15%" valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="استان قبلی" ClientInstanceName="lblPr" Width="100%"
                                                        ID="ASPxLabel12">
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
                                                <td valign="top" style="width: 15%" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تاریخ انتقالی" Width="100%" ID="ASPxLabel13">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 35%" valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"  
                                                        ID="lblTransferDate" Style="direction: ltr">
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
                                                    <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel14">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"  
                                                        ClientInstanceName="lblFileNo" ID="lblFileNo" Style="direction: ltr" RightToLeft="True">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="شماره عضویت" ClientInstanceName="lblMeNo" ID="ASPxLabel20">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"  
                                                        ID="lblPreMeNo">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="lblDocPr" runat="server" ClientInstanceName="lblDocPr" Text="استان صدور پروانه"
                                                        Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxComboBox ID="ComboDocPr" runat="server" ClientInstanceName="ComboDocPr"
                                                          DataSourceID="OdbProvince"
                                                         TextField="PrName" ValueField="PrId" ValueType="System.String"
                                                        Width="100%" RightToLeft="True" ReadOnly="true">
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
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelImpDoc" HeaderText="مشخصات مجوز مجری حقیقی"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <TSPControls:CustomAspxCallbackPanel runat="server" 
                                ClientInstanceName="CallbackPanelDoRegDate" Width="100%" ID="CallbackPanelDoRegDate"
                                OnCallback="CallbackPanelDoRegDate_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent11" runat="server">
                                        <table id="Table1" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="شماره مجوز" ID="ASPxLabel3" ClientInstanceName="lblDate">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMfNoImp"  Width="100%" ReadOnly="True"
                                                             Style="direction: ltr">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="پایه" ID="ASPxLabel19" ClientInstanceName="lblFileNo">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <%--<TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtGradeImp"  Width="100%"
                                                            ReadOnly="True" >
                                                        </TSPControls:CustomTextBox>--%>
                                                        
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbGradeImp" ClientInstanceName="cmbGradeImp" ValueType="System.String"
                                                                       EnableIncrementalFiltering="True"
                                                                        IncrementalFilteringMode="StartsWith" RightToLeft="True" DataSourceID="ObjectDataSourceGrade"
                                                                        TextField="GrdName" ValueField="GrdId">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        
                                                                            <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                        <ButtonStyle Width="13px">
                                                                        </ButtonStyle>
                                                                    </TSPControls:CustomAspxComboBox>
                                                                    <asp:ObjectDataSource ID="ObjectDataSourceGrade" runat="server" SelectMethod="GetData"
                                                                        TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="محل صدور مجوز" ID="ASPxLabel4" ClientInstanceName="lblFileNo">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtProvinceNameImp"  Width="100%"
                                                            ReadOnly="True" >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Visible="false" Text="تاریخ صدور" ID="ASPxLabel5" ClientInstanceName="lblFileNo">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtRegDateImp"  Width="100%"
                                                            ReadOnly="True" Visible="false" >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="15%" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="کد پیگیری" ID="lblFollowCode">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td width="35%" valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFollowCode"  Width="100%"
                                                            ReadOnly="True" >
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td width="15%" valign="top" align="right">
                                                    </td>
                                                    <td width="35%" valign="top" align="right">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabel7">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                            ID="cmbIsTemporary" ClientInstanceName="cmbIsTemporary" ValueType="System.String"
                                                            >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="نوع مجوز را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <Items>
                                                                <dxe:ListEditItem Value="0" Selected="true" Text="پروانه دائم"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                                            </Items>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره سریال" ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSerialNoImp"  Width="100%"
                                                             Style="direction: ltr">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ صدور" ID="ASPxLabel15">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <table width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                                                            ID="txtLastRegDateImp" PickerDirection="ToRight" ShowPickerOnEvent="OnClick"
                                                                            IconUrl="~/Image/Calendar.gif" onchange="return SetMeDocDefualtExpireDateJS();"></pdc:PersianDateTextBox>
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
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" ID="ASPxLabel17">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            ShowPickerOnEvent="OnClick" PickerDirection="ToRight" ShowPickerOnTop="True"
                                                            Width="230px" ID="txtExpDateImp"></pdc:PersianDateTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="15%" valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel22">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3" valign="top" align="right">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="34px" Width="100%"  
                                                            ClientInstanceName="txtDescription" ID="txtDescription" RightToLeft="True">
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomAspxCallbackPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
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
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td dir="ltr">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                        Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnSave_Click">
                                                        <Image  Url="~/Images/icons/save.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" OnClick="btnBack_Click" runat="server" Text=" " EnableTheming="False"
                                                        EnableViewState="False" UseSubmitBehavior="False" CausesValidation="False" ToolTip="بازگشت"
                                                        >
                                                        <Image  Url="~/Images/icons/Back.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile">
                                    </dxhf:ASPxHiddenField>
                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

                <asp:ObjectDataSource ID="ObjdsMajor" runat="server" UpdateMethod="Update" TypeName="TSP.DataManager.MajorManager"
                    SelectMethod="FindMajorParent" >
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                    SelectMethod="GetData"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" UpdateMethod="Update"
                    TypeName="TSP.DataManager.MemberLicenceManager" SelectMethod="SelectByMemberId"
                   OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MemberId"></asp:Parameter>
                        <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
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
</asp:Content>
