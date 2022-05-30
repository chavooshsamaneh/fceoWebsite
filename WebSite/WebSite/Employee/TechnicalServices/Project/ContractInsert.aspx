<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ContractInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_ContractInsert"
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
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="BtnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSave_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if(HD.Get('name')!=1)
{
lbl.SetVisible(true);
e.processOnServer=false;
}
}"></ClientSideEvents>
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
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
                                            Enabled="false" RightToLeft="True"
                                            OnSelectedIndexChanged="CmbType_SelectedIndexChanged">
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
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="نوع طراح" ID="ASPxLabelMj" ClientInstanceName="lblMj">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="Title" ID="CmbMajor"  DataSourceID="ObjectDataSourceDesignerType"
                                            ValueType="System.String" ValueField="DesignerTypeId" ClientInstanceName="cmbMj"
                                            EnableIncrementalFiltering="True"
                                            RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نوع طراح را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                            <%--    <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع" ID="lblType" ClientInstanceName="lblType">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtType" ClientInstanceName="txtType" Width="100%" ReadOnly="True">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="lblName" ClientInstanceName="lblName">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName" ClientInstanceName="txtName" Width="100%" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="طرف قرارداد را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>--%>
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
                                        <dxe:ASPxLabel runat="server" Text="درصد دستمزد" ID="ASPxLabelObs" ClientInstanceName="lblObs">
                                        </dxe:ASPxLabel>
                                        <br />
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtWage" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                              
                                                <RequiredField IsRequired="True" ErrorText="دستمزد را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این مقدار صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ انعقاد" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                            Width="230px" ShowPickerOnTop="True" ID="txtContractDate" PickerDirection="ToRight"
                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContractDate" ID="RequiredFieldValidator31"
                                            Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomASPxCheckBox runat="server" Text="قرارداد متمم قرارداد دیگری از پروژه می باشد"
                                            ID="ChbMaster" ClientInstanceName="chb">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
	if(chb.GetChecked() == true)
	{
		lblParent.SetVisible(true);	
		ParentId.SetVisible(true);	
	}
	else
	{
		lblParent.SetVisible(false);	
		ParentId.SetVisible(false);	
	}
}"></ClientSideEvents>
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد قرارداد اصلی" ClientVisible="False" ID="ASPxLabelParent"
                                            ClientInstanceName="lblParent">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtParentId" ClientVisible="False"
                                            Width="100%" AutoPostBack="True" ClientInstanceName="ParentId"
                                            OnTextChanged="txtParentId_TextChanged">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="کد قرارداد را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="قرارداد" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpContract" InputType="Images"
                                                            UploadWhenFileChoosed="true" ClientInstanceName="flp" OnFileUploadComplete="flpContract_FileUploadComplete"
                                                            ClientVisible="False">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
  if(e.isValid){
	img.SetVisible(true);
	HD.Set('name',1);
	lbl.SetVisible(false);
	hp.SetVisible(true);
	hp.SetNavigateUrl('../../../Image/TechnicalServices/Contract/'+e.callbackData);
    }
 else{
 	img.SetVisible(false);
	HD.Set('name',0);
	lbl.SetVisible(true);
	hp.SetVisible(false);
	hp.SetNavigateUrl('');
    }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="فایل قرارداد را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel3" ForeColor="Red" ClientInstanceName="lbl">
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
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelJudgmentGroup" HeaderText="هیئت حل اختلاف"
                runat="server" Width="100%" ClientVisible="False" ClientInstanceName="rdp">
                <PanelCollection>
                    <dx:PanelContent>
                        <table runat="server" id="tbl1" dir="rtl" width="100%">
                            <tr>
                                <td width="15%" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="عنوان نماینده" ID="ASPxLabel13">
                                    </dxe:ASPxLabel>
                                </td>
                                <td width="35%" valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                        ID="cmbAgentType" RightToLeft="True" ValueType="System.String" ClientInstanceName="cmbAgent">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="عنوان را انتخاب نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <Items>
                                            <dxe:ListEditItem Value="1" Text="نماینده صاحب کار"></dxe:ListEditItem>
                                            <dxe:ListEditItem Value="2" Text="نماینده مجری"></dxe:ListEditItem>
                                            <dxe:ListEditItem Value="3" Text="نماینده مرضی الطرفین"></dxe:ListEditItem>
                                        </Items>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td width="15%" valign="top" align="right"></td>
                                <td width="35%" valign="top" align="right"></td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxlbloFirstName" ClientInstanceName="lbloFirstName">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoFirstName" Width="100%"
                                        ClientInstanceName="oFirstName">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxlbloLastName" ClientInstanceName="lbloLastName">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoLastName" Width="100%"
                                        ClientInstanceName="oLastName">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxlbloFatherName" ClientInstanceName="lbloFatherName">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoFatherName" Width="100%"
                                        ClientInstanceName="oFatherName">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxlbloSSN" ClientInstanceName="lbloSSN">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoSSN" Width="100%" MaxLength="10"
                                        ClientInstanceName="oSSN">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" ID="ASPxlbloIdNo" ClientInstanceName="lbloIdNo">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoIdNo" Width="100%" MaxLength="10"
                                        ClientInstanceName="oIdNo">
                                        <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره شناسنامه را وارد نمایید"
                                            ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField ErrorText=""></RequiredField>
                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="محل صدور" ID="ASPxlbloBirthPlace" ClientInstanceName="lbloBirthPlace">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoBirthPlace" Width="100%"
                                        ClientInstanceName="oBirthPlace">
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="تلفن" ID="ASPxLabel9">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoTel" Width="100%" ClientInstanceName="oTel"
                                        MaxLength="12">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="تلفن را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="شماره همراه" ID="ASPxLabel11">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtoMobileNo" Width="100%"
                                        MaxLength="11" ClientInstanceName="oMobileNo">
                                        <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره همراه را وارد نمایید"
                                            ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{10}"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel12">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right" colspan="3">
                                    <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtoAddress" Width="100%"
                                        ClientInstanceName="oAddress">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="True" ErrorText="آدرس را وارد نمایید"></RequiredField>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" colspan="4">
                                    <br />
                                    <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;اضافه به ليست"
                                        ValidationGroup="j" ID="btnAddJudg" ClientInstanceName="btn"
                                        OnClick="btnAddJudg_Click">
                                        <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                            <tr id="Tr8" runat="server">
                                <td id="TD24" runat="server" valign="top" align="left" colspan="4">
                                    <TSPControls:CustomAspxButton runat="server" Text=" " ToolTip="پاک کردن فرم"
                                        CausesValidation="False" ID="btnRefresh" AutoPostBack="False" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	Clear();
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Height="30px" Width="30px" Url="~/Images/icons/Clear-Form.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                            ID="CustomAspxDevGridView1" KeyFieldName="Id" AutoGenerateColumns="False"
                            ClientInstanceName="grid" OnRowDeleting="CustomAspxDevGridView1_RowDeleting">
                            <ClientSideEvents RowClick="function(s, e) {
	SetControlValues();
	btn.SetEnabled(false);
}"></ClientSideEvents>
                            <Styles>
                                <GroupPanel ForeColor="Black">
                                </GroupPanel>
                                <Header HorizontalAlign="Center">
                                </Header>
                            </Styles>
                            <Settings ShowGroupPanel="True"></Settings>
                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AgentTypeName" Caption="نوع نماینده">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Tel" Caption="تلفن">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MobileNo" Caption="شماره همراه">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn VisibleIndex="5" Caption=" " ShowDeleteButton="true">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Address" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="FatherName" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="IdNo" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="SSN" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="BirthPlace" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="TypeId" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="BtnNew_Click">

                                        <Image Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                        ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSave_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if(HD.Get('name')!=1)
{
lbl.SetVisible(true);
e.processOnServer=false;
}
}"></ClientSideEvents>

                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>

                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                        CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBack_Click">

                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDContractId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDPrjImpObsDsgnId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpContract" runat="server" ClientInstanceName="HD">
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
