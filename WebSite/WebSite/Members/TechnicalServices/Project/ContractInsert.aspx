<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ContractInsert.aspx.cs" Inherits="Members_TechnicalServices_Project_ContractInsert"
    Title="مشخصات قرارداد" %>

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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <script language="javascript">
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'TypeId;FirstName;LastName;FatherName;SSN;IdNo;BirthPlace;Tel;MobileNo;Address', SetValue);
        }
        function SetValue(values) {
            cmbAgent.SetValue(values[0]);
            oFirstName.SetText(values[1]);
            oLastName.SetText(values[2]);
            oFatherName.SetText(values[3]);
            oSSN.SetText(values[4]);
            oIdNo.SetText(values[5]);
            oBirthPlace.SetText(values[6]);
            oTel.SetText(values[7]);
            oMobileNo.SetText(values[8]);
            oAddress.SetText(values[9]);

        }
        function Clear() {
            cmbAgent.SetSelectedIndex(-1);
            oFirstName.SetText("");
            oLastName.SetText("");
            oFatherName.SetText("");
            oSSN.SetText("");
            oIdNo.SetText("");
            oBirthPlace.SetText("");
            oTel.SetText("");
            oMobileNo.SetText("");
            oAddress.SetText("");
            btn.SetEnabled(true);
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tr>

                                <td>
                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click" Visible="false">
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره" ToolTip="ذخیره"
                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSave_Click">
                                        <ClientSideEvents Click="function(s, e) {
if(HiddenFieldPage.Get('name')!=1)
{
    lblFileValidation.SetVisible(true);
    e.processOnServer=false;
}
}"></ClientSideEvents>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت قراردادهای پروژها" ToolTip="مدیریت قراردادهای پروژها"
                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBack_Click">
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" ClientInstanceName="RoundPanel1"
                HeaderText="مشاهده" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="نوع قرارداد" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="Title" ID="CmbType" AutoPostBack="True" DataSourceID="ObjectDataSourceType"
                                            ValueType="System.String" ValueField="ProjectIngridientTypeId" ClientInstanceName="cmb"
                                            Enabled="false" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نوع قرارداد را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="ناظر" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" Enabled="false"
                                            ID="comboProjectObserver" ValueType="System.String" SelectedIndex="0" DataSourceID="ObjectDataSourceObserver" ClientInstanceName="comboProjectObserver"
                                            RightToLeft="True" AutoPostBack="true" TextField="NameAndWage" ValueField="ProjectObserversId">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="ناظر را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectDataSourceObserver" runat="server" SelectMethod="FindByProjectIdAndRequestId"
                                            TypeName="TSP.DataManager.TechnicalServices.Project_ObserversManager" OldValuesParameterFormatString="original_{0}">
                                            <SelectParameters>
                                                <asp:Parameter Type="Int32" DefaultValue="-2" Name="ProjectId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjReId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="0" Name="InActive"></asp:Parameter>
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>

                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مدت زمان(ماه)" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDuration" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="مدت زمان را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این مقدار صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مبلغ(ریال)" ID="ASPxLabel7">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAmount" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این مقدار صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ انعقاد" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                            Width="100%" ShowPickerOnTop="True" ID="txtContractDate" PickerDirection="ToRight"
                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContractDate" ID="RequiredFieldValidator31"
                                            Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="فایل قرارداد" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpContract" InputType="Files"
                                                            UploadWhenFileChoosed="true" ClientInstanceName="flp" OnFileUploadComplete="flpContract_FileUploadComplete"
                                                            ClientVisible="False" MaxSizeForUploadFile="600000">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
  if(e.isValid){
	img.SetVisible(true);
	HiddenFieldPage.Set('name',1);
	lblFileValidation.SetVisible(false);
	hp.SetVisible(true);
	hp.SetNavigateUrl('../../../Image/TechnicalServices/Contract/'+e.callbackData);
    }
 else{
 	img.SetVisible(false);
	HiddenFieldPage.Set('name',0);
	lblFileValidation.SetVisible(true);
	hp.SetVisible(false);
	hp.SetNavigateUrl('');
    }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="فایل قرارداد را انتخاب نمایید" ClientVisible="False"
                                                            ID="lblFileValidation" ForeColor="Red" ClientInstanceName="lblFileValidation">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImg" ClientInstanceName="img">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" ClientVisible="False" Target="_blank"
                                            ID="HpContract" ClientInstanceName="hp">
                                        </dxe:ASPxHyperLink>
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
                        <table>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click" Visible="false">
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره" ToolTip="ذخیره"
                                        ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSave_Click">
                                        <ClientSideEvents Click="function(s, e) {
if(HiddenFieldPage.Get('name')!=1)
{
    lblFileValidation.SetVisible(true);
    e.processOnServer=false;
}
}"></ClientSideEvents>
                                    </TSPControls:CustomAspxButton>
                                </td>

                                <td>
                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت قراردادهای پروژها" ToolTip="مدیریت قراردادهای پروژها"
                                        CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBack_Click">
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldPage" runat="server" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceType" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.ProjectIngridientTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceDesignerType" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.DesignerTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceMajor" runat="server" SelectMethod="FindMjParents"
                TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img alt="" src="../../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
