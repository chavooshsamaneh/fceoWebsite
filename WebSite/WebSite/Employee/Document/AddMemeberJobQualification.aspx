<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddMemeberJobQualification.aspx.cs" Inherits="Employee_Document_AddMemeberJobQualification"
    Title="مشخصات مطلوبیت سابقه کار" %>

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
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                     
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="BtnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnSave_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) {
if(flpme.Get('name')!=1)
{
lblf.SetVisible(true);
e.processOnServer=false;
}
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
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

            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table runat="server" id="tbl" dir="rtl" width="100%">
                            <tr runat="server" id="Tr1">
                                <td runat="server" id="Td1" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="نوع مطلوبیت کار *" Width="100px" ID="ASPxLabel2">
                                    </dxe:ASPxLabel>
                                </td>
                                <td runat="server" id="Td2" dir="ltr" valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="250px" 
                                        TextField="Name" ID="CmbName"  DataSourceID="OdbFactorDocuments"
                                        ValueType="System.String" ValueField="OfdId" 
                                        ClientInstanceName="CmbName">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="نوع مطلوبیت را انتخاب نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="Name" Caption="نام" Width="290px"></dxe:ListBoxColumn>
                                            <dxe:ListBoxColumn FieldName="Value" Caption="حداکثر نمره"></dxe:ListBoxColumn>
                                        </Columns>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	txtCmbName.SetText(CmbName.GetSelectedIndex());
}" />
                                    </TSPControls:CustomAspxComboBox>
                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtCmbName" runat="server" ClientInstanceName="txtCmbName" Width="170px"
                                        ClientVisible="False">
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="Tr2">
                                <td runat="server" id="Td3" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel3">
                                    </dxe:ASPxLabel>
                                </td>
                                <td runat="server" id="Td4" valign="top" align="right">
                                    <TSPControls:CustomASPXMemo runat="server" Height="33px"   ID="txtJhDesc"
                                        >
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr runat="server" id="Tr3">
                                <td runat="server" id="Td5" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="فایل *" ID="ASPxLabel5">
                                    </dxe:ASPxLabel>
                                </td>
                                <td runat="server" id="Td6" valign="top" align="right">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                        ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                        <ClientSideEvents  
                                                            FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
	flpme.Set('name',1);
	lblf.SetVisible(false);
	hpFilePath.SetVisible(true);
	hpFilePath.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}"></ClientSideEvents>
                                                        
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientVisible="False"
                                                        ID="ASPxLabel48" ForeColor="Red" ClientInstanceName="lblf">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                        ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" Target="_blank" ID="hpFilePath"
                                        ClientVisible="False" ClientInstanceName="hpFilePath">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelJudge" HeaderText="نظر کارشناسی" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>

                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="4">
                                                    <dxe:ASPxRadioButtonList runat="server"   ID="rdbtnIsConfirm"
                                                        >
                                                        <Border BorderWidth="0px"></Border>
                                                        <Items>
                                                            <dxe:ListEditItem Value="0" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="1" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                        </Items>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="وضعیت را مشخص نمایید"></RequiredField>
                                                        </ValidationSettings>
                                                    </dxe:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="ltr" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server"  Width="76px" ID="txtMeetingId"
                                                        >
                                                        <ValidationSettings>
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="شماره جلسه" ID="ASPxLabel6">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="167px" ShowPickerOnTop="True"
                                                        ID="txtMeetingDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تاریخ جلسه" ID="ASPxLabel7">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox  runat="server"  Width="170px" ID="txtGrade" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="امتیاز را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="امتیاز" ID="ASPxLabel4">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px"    ID="txtViewPoint"
                                                        >
                                                        <ValidationSettings>
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                                <td style="vertical-align: top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="نظر کارشناسی" ID="ASPxLabel8">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldQualification">
            </dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table >
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(flpme.Get('name')!=1)
{
lblf.SetVisible(true);
e.processOnServer=false;
}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
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
    <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="OdbFactorDocuments" runat="server" FilterExpression="Type={0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="JhQualityId" runat="server" Visible="False" />
    <asp:HiddenField ID="HDJudgeId" runat="server" Visible="False" />

</asp:Content>
