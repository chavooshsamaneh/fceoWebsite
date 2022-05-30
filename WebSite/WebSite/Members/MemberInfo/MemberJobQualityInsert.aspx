<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberJobQualityInsert.aspx.cs" Inherits="Employee_MembersRegister_MemberJobQualityInsert"
    Title="مشخصات مطلوبیت کار" %>

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
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                            <image url="~/Images/icons/new.png"></image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                            <image url="~/Images/icons/edit.png"></image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                            <image url="~/Images/icons/save.png"></image>
                                            <ClientSideEvents Click="function(s, e) {
if(flpme.Get('name')!=1)
{
lblf.SetVisible(true);
e.processOnServer=false;
}
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                            <image url="~/Images/icons/Back.png"></image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <br />
            <div style="width: 100%; text-align: right">
                <dxe:ASPxLabel ID="lblSex" runat="server"></dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblT" runat="server"></dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblOfName" runat="server"></dxe:ASPxLabel>
            </div>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table id="tbl" runat="server" dir="rtl" width="100%">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server" align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نوع مطلوبیت کار" Width="100px">
                                    </dxe:ASPxLabel>
                                </td>
                                <td id="Td2" runat="server" align="right" dir="ltr" valign="top">
                                    <TSPControls:CustomAspxComboBox ID="CmbName" runat="server"
                                        DataSourceID="OdbFactorDocuments"
                                        TextField="Name" ValueField="OfdId" ValueType="System.String" Width="250px" EnableIncrementalFiltering="true">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            
                                            <RequiredField ErrorText="نوع مطلوبیت کار را انتخاب نمایید" IsRequired="True" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <Columns>
                                            <dxe:ListBoxColumn Caption="نام" FieldName="Name" Width="290px" />
                                            <dxe:ListBoxColumn Caption="حداکثر امتیاز" FieldName="Value" />
                                        </Columns>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td id="Td3" runat="server" align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="توضیحات">
                                    </dxe:ASPxLabel>
                                </td>
                                <td id="Td4" runat="server" align="right" valign="top">
                                    <TSPControls:CustomASPXMemo ID="txtJhDesc" runat="server"
                                        Height="33px" Width="570px">
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td5" runat="server" align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="فایل">
                                    </dxe:ASPxLabel>
                                </td>
                                <td id="Td6" runat="server" align="right" valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl ID="flp" runat="server" ClientInstanceName="flpc"
                                                    InputType="Images" OnFileUploadComplete="flp_FileUploadComplete"
                                                    UploadWhenFileChoosed="true">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
	if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
	flpme.Set('name',1);
	lblf.SetVisible(false);
	}
	else
	{
	imgEndUploadImgClient.SetVisible(false);
	flpme.Set('name',0);
	lblf.SetVisible(true);
	}
}" />

                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel ID="ASPxLabel48" runat="server" ClientInstanceName="lblf" ClientVisible="False"
                                                    ForeColor="Red" Text="تصویر را انتخاب نمایید">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="imgEndUploadImg" runat="server" ClientInstanceName="imgEndUploadImgClient"
                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </table>
                                    <dxe:ASPxHyperLink ID="hpFilePath" runat="server" Target="_blank" Text="آدرس فایل"
                                        Visible="False">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelJudge" HeaderText="نظر کارشناسی" runat="server" Visible="False">
                            <PanelCollection>
                                <dxp:PanelContent>


                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="4">
                                                    <dxe:ASPxRadioButtonList runat="server" ID="rdbtnIsConfirm">
                                                        <Border BorderWidth="0px"></Border>
                                                        <Items>
                                                            <dxe:ListEditItem Value="0" Text="مورد تایید نمی باشد"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="1" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                        </Items>

                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="وضعیت را مشخص نمایید"></RequiredField>
                                                        </ValidationSettings>
                                                    </dxe:ASPxRadioButtonList>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" Width="76px" ID="txtMeetingId">
                                                        <ValidationSettings>
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="شماره جلسه" ID="ASPxLabel6"></dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="167px" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick" ID="txtMeetingDate" Style="direction: ltr" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>

                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تاریخ جلسه" ID="ASPxLabel7"></dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" Width="170px" ID="txtGrade">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                            <RequiredField IsRequired="True" ErrorText="امتیاز مورد قبول کارشناس را وارد نمایید"></RequiredField>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="امتیاز" ID="ASPxLabel4"></dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="400px" ID="txtViewPoint">
                                                        <ValidationSettings>
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>

                                                </td>
                                                <td style="vertical-align: top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="نظر کارشناسی" ID="ASPxLabel8"></dxe:ASPxLabel>

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
                        <br />
                        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                            <PanelCollection>
                                <dxp:PanelContent>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                        <image url="~/Images/icons/new.png"></image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                        <image url="~/Images/icons/edit.png"></image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                        <image url="~/Images/icons/save.png"></image>
                                                        <ClientSideEvents Click="function(s, e) {
	if(flpme.Get('name')!=1)
{
lblf.SetVisible(true);
e.processOnServer=false;
}
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                        <image url="~/Images/icons/Back.png"></image>
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
    <asp:HiddenField ID="MemberId" runat="server" Visible="False" />
    <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="JobId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
    </dxhf:ASPxHiddenField>
    <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="OdbFactorDocuments" runat="server" FilterExpression="Type={0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="JhQualityId" runat="server" Visible="False" />
    <asp:HiddenField ID="HDJudgeId" runat="server" Visible="False" />
    <asp:HiddenField ID="HDComboName" runat="server" Visible="False" />

</asp:Content>
