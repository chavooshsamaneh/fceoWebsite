<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="OfficeJobShow.aspx.cs" Inherits="Members_Office_OfficeJobShow" Title="مشخصات سابقه کاری" %>


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
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>

        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
        </div>
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
            <PanelCollection>
                <dxp:PanelContent>

                    <table>
                        <tbody>
                            <tr>

                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                        

                                        <image url="~/Images/icons/Back.png"></image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" AutoSeparators="RootOnly" ItemSpacing="0px" OnItemClick="ASPxMenu1_ItemClick">
            <Items>

                <dxm:MenuItem Name="JobQuality" Text="مطلوبیت کار">
                </dxm:MenuItem>
                <dxm:MenuItem Name="Job" Text="مشخصات سابقه کاری" Selected="True">
                </dxm:MenuItem>
            </Items>

        </TSPControls:CustomAspxMenuHorizontal>



        <div style="width: 100%; text-align: right">
            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
            </dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lblOfName" runat="server">
            </dxe:ASPxLabel>
        </div>
        <br />
        <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
       
                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام پروژه" ID="ASPxLabel9"></dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="282px" ID="txtjPrName" ClientInstanceName="TextPrName" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="نام پروژه را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام کارفرما" ID="ASPxLabel11"></dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtjEmployer" ClientInstanceName="TextEmployer" ReadOnly="True">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="نام کارفرما را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نوع پروژه" ID="ASPxLabel8"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" L TextField="Name" ID="CombojPrType" DataSourceID="OdbPrType" ValueType="System.String" ValueField="PrtId" ClientInstanceName="CmbPrType" ReadOnly="True">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
   if(CmbPrType.GetValue() == '1')
	{
	TextArea.SetVisible(true);
	TextFloor.SetVisible(true);
	lbl1.SetVisible(true);
	lbl2.SetVisible(true);
	CmbSazeType.SetVisible(true);
	lbl3.SetVisible(true);
	}
	else
	{
	TextArea.SetVisible(false);
	TextFloor.SetVisible(false);
	lbl1.SetVisible(false);
	lbl2.SetVisible(false);
	CmbSazeType.SetVisible(false);
	lbl3.SetVisible(false);
	}
}"></ClientSideEvents>

                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="نوع پروژه را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top">&nbsp;<dxe:ASPxLabel runat="server" Text="نوع سازه" ID="ASPxLabel10" ClientInstanceName="lbl3" ClientVisible="False"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="Name" ID="CombojSazeType" DataSourceID="OdbSazeType" ValueType="System.String" ValueField="SztId" ClientInstanceName="CmbSazeType" ClientVisible="False" ReadOnly="True">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="نوع سازه را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="سمت" ID="ASPxLabel14"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="PName" ID="ComboPosition" DataSourceID="OdbJobPosition" ValueType="System.String" ValueField="PJPId" ClientInstanceName="CmbPosition" ReadOnly="True">

                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {

   if(CmbPosition.GetValue()=='8' || CmbPosition.GetValue()=='9')
		rdpJob.SetVisible(true);
	else
		rdpJob.SetVisible(false);
}"></ClientSideEvents>
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">


                                                <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top">&nbsp;<dxe:ASPxLabel runat="server" Text="نحوه مشارکت" ID="ASPxLabel24"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="CorName" ID="CombojIsCorporate" DataSourceID="OdbCorType" ValueType="System.String" ValueField="CortId" ClientInstanceName="CmbCorporate" ReadOnly="True">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="نحوه مشارکت را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Columns>
                                                <dxe:ListBoxColumn Caption="نام" FieldName="CorName" Width="200px" />
                                                <dxe:ListBoxColumn Caption="ضریب" FieldName="Rate" />
                                            </Columns>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="کشور" ID="ASPxLabel12"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="CounName" ID="CombojCountry" DataSourceID="ODBJobCountry" ValueType="System.String" ValueField="CounId" ClientInstanceName="CmbCountry" ReadOnly="True">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top">&nbsp;<dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel13"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtjCity" ClientInstanceName="TextCity" ReadOnly="True">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع پروژه" ID="ASPxLabel16"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="185px" ShowPickerOnTop="True" ID="txtjStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">&nbsp;<dxe:ASPxLabel runat="server" Text="حجم پروژه" ID="ASPxLabel21"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtjPrVolume" ClientInstanceName="TextVolume" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع همکاری" ID="ASPxLabel17"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="185px" ShowPickerOnTop="True" ID="txtjCoStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">&nbsp;<dxe:ASPxLabel runat="server" Text="تاریخ پایان همکاری" ID="ASPxLabel19"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="185px" ShowPickerOnTop="True" ID="txtjCoEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان شروع همکاری" Width="110px" ID="ASPxLabel18"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtjStartStatus" ClientInstanceName="TextSStatus" ReadOnly="True"></TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان پایان همکاری" Width="110px" ID="ASPxLabel20"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtjEndStatus" ClientInstanceName="TextEStatus" ReadOnly="True"></TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="زیربنا" ClientVisible="False" ID="ASPxLabel22" ClientInstanceName="lbl1"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ClientVisible="False" Width="170px" ID="txtjArea" ClientInstanceName="TextArea" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="زبر بنا را وارد نمایید"></RequiredField>

                                                <RegularExpression ErrorText="مقدار زیربنا را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">&nbsp;<dxe:ASPxLabel runat="server" Text="تعداد طبقات" ClientVisible="False" ID="ASPxLabel23" ClientInstanceName="lbl2"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ClientVisible="False" Width="170px" ID="txtjFloor" ClientInstanceName="TextFloor" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="تعداد طبقات را وارد نمایید"></RequiredField>

                                                <RegularExpression ErrorText="تعداد طبقات را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25"></dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="33px" Width="530px" ID="txtjDesc" ClientInstanceName="TextDesc" ReadOnly="True"></TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                   
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">


                                            <image url="~/Images/icons/Back.png"></image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
       
      
        <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
            <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
            <asp:HiddenField ID="JobId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:ObjectDataSource ID="ODBJobCountry" runat="server" DeleteMethod="Delete" CacheExpirationPolicy="Sliding" SqlCacheDependency="NezamFars:tblCountry"
                EnableCaching="True" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager" UpdateMethod="Update"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbPrType" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.ProjectTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbSazeType" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.SazeTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbCorType" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.CorporationTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbJobPosition" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.ProjectJobPositionManager"></asp:ObjectDataSource>
</asp:Content>




