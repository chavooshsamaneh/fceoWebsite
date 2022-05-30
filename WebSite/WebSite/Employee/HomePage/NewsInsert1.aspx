<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="NewsInsert1.aspx.cs" Inherits="Employee_News_NewsInsert1"
    Title="مشخصات خبر" %>

<%--<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>--%>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function myImageUpload(editor) {
            var imageFileName = null;
            var imageFileTitle = null;

            function postback(doc, popup_iframe) {
                if (doc != null) // Upload clicked
                {
                    if (doc.getElementById("path").value != "") {
                        var frm = doc.getElementById("fraExecute");

                        function onLoad() {
                            setTimeout(function () {
                                if (frm.detachEvent)
                                    frm.detachEvent("onload", onLoad);
                                else
                                    if (frm.removeEventListener)
                                        frm.removeEventListener("load", onLoad, true);

                                imageFileName = frm.contentWindow.imageFileName;
                                imageFileTitle = frm.contentWindow.imageFileTitle;

                                if (imageFileName == null)
                                    alert(frm.contentWindow.imageSaved);

                                popup_iframe.contentWindow.document.getElementById("cancel").onclick();  // emulate Cancel pressed
                            }, 0);
                        }

                        if (frm.attachEvent)
                            frm.attachEvent("onload", onLoad);
                        else
                            if (frm.addEventListener)
                                frm.addEventListener("load", onLoad, true);

                        try { doc.getElementById("frmFile").submit(); }
                        catch (e) {
                            alert(e.message);

                            if (frm.detachEvent)
                                frm.detachEvent("onload", onLoad);
                            else
                                if (frm.removeEventListener)
                                    frm.removeEventListener("load", onLoad, true);

                            popup_iframe.contentWindow.document.getElementById("cancel").onclick();
                        }
                        return false;
                    }
                }
                else  // Cancel clicked or emulated
                {
                    if (imageFileName != null) {

                        editor.focusEditor();
                        editor.InsertHTML("<img src=\"" + imageFileName + "\" alt=\"" + imageFileTitle + "\" title=\"" + imageFileTitle + "\" />");
                    }
                }
            }

            function init(doc, popup_iframe) {
                if (doc != null) {
                    doc.getElementById("title").value = "";
                    if (document.all) {
                        popup_iframe.style.display = "none";
                        doc.getElementById("path").click();
                        popup_iframe.style.display = "";
                        if (doc.getElementById("path").value == "")
                            popup_iframe.contentWindow.document.getElementById("cancel").onclick();
                    }
                }
            }

            editor.customPopup("popup_image_upload", "image-upload", "__cs_myImageUpload.aspx", postback, init, false, false);
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/new.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش" UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/edit.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/save.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False" Visible="False">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />

                                            <Image Url="~/Images/icons/delete.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/Back.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="عنوان *" ID="Label4"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTitle" MaxLength="80" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="عنوان را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="موضوع *" ID="Label1"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="Name"
                                            ID="drpSub" DataSourceID="ObjectDataSourceSub"
                                            ValueType="System.String" RightToLeft="True" ValueField="SubId">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="موضوع را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="درجه اهمیت" ID="Label13"></asp:Label>
                                    </td>
                                    <td valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" ID="drpImp"
                                            ValueType="System.String" RightToLeft="True" SelectedIndex="0">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="اهمیت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="معمولی" Selected="True"></dxe:ListEditItem>
                                            <%--    <dxe:ListEditItem Value="1" Text="مهم"></dxe:ListEditItem>--%>
                                                <dxe:ListEditItem Value="2" Text="بسیار مهم"></dxe:ListEditItem>
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نمایندگی*" ID="Label3"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" TextField="Name"
                                            ID="cmbAgent" DataSourceID="ObjectDataSourceAgent"
                                            ValueType="System.String" RightToLeft="True" ValueField="AgentId">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نمایندگی را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text="سطح دسترسی*" ID="Label6"></asp:Label>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server" ID="cmbUserLoginType"
                                            ValueType="System.String" SelectedIndex="0"
                                            RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نوع نمایش را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="کاربران مهمان" Selected="True"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="اشخاص حقیقی"></dxe:ListEditItem>
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="تاریخ *" ID="Label8"></asp:Label>
                                    </td>
                                    <td>
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                            ID="txtDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                            Style="text-align: right;" Width="230px" ShowPickerOnEvent="OnClick">
                                        </pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" ID="RequiredFieldValidator3"
                                            Display="Dynamic">تاریخ را انتخاب نمایید</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text="تاریخ اعتبار" ID="Label5"></asp:Label>
                                    </td>
                                    <td>
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                            ID="txtExpireDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr; text-align: right;" Width="230px" ShowPickerOnEvent="OnClick">
                                        </pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="زمان" ID="lblTime" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <TSPControls:CustomTextBox runat="server" ReadOnly="True" Visible="False" ID="txtTime"></TSPControls:CustomTextBox>
                                    </td>
                                    <td>حوزه خبر</td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox ID="cmbExGroup" ClientInstanceName="cmbExGroup" runat="server"
                                            DataSourceID="ObjectDataExGroup"
                                            EnableIncrementalFiltering="True" TextField="ExGroupName" ValueField="ExGroupId" ValueType="System.String"
                                            Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectDataExGroup" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="SelectExGroupForNews" TypeName="TSP.DataManager.ExGroupManager"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3"><TSPControls:CustomASPxCheckBox runat="server" ID="CheckBoxIsNotification" Text="خبر از نوع اطلاعیه می باشد."></TSPControls:CustomASPxCheckBox></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="خلاصه خبر *<br>(نمایش در آرشیو)" ID="Label2"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtSummary" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="خلاصه خبر را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="فایل" ID="Label9"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td dir="rtl">
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpcAttachment" InputType="Files"
                                                            ClientInstanceName="flpcAttachment" OnFileUploadComplete="flpcAttachment_FileUploadComplete"
                                                            MaxSizeForUploadFile="10000000" UploadWhenFileChoosed="true">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
imgEndAttachment.SetVisible(true);
//	HyperLinkAttachment.SetVisible(true);
	HyperLinkAttachment.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}
else {
 imgEndAttachment.SetVisible(false); 
 HyperLinkAttachment.SetNavigateUrl('');
 }                                     
}"></ClientSideEvents>
                                                            <BrowseButton Text=" انتخاب فایل">
                                                                <Image Height="16px" Url="~/Images/Icons/file-upload.png" Width="16px">
                                                                </Image>
                                                            </BrowseButton>
                                                            <CancelButton Text="انصراف">
                                                            </CancelButton>
                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td align="right">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRemoveFile" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnRemoveFile_Click" Text=" "
                                                            ToolTip="حذف فایل" UseSubmitBehavior="False">

                                                            <Image Url="~/Images/icons/DeleteFile.png" />
                                                            <ClientSideEvents Click="function(s,e){	 e.processOnServer= confirm('آیا مطمئن به حذف فایل هستید؟');}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ID="imgEndAttachment" ToolTip="تصویر انتخاب شد" ClientVisible="False"
                                                            ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndAttachment">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HyperLinkAttachment" runat="server" ClientInstanceName="HyperLinkAttachment"
                                            Target="_blank" Text="آدرس فایل">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                    <td valign="top" align="right"></td>
                                </tr>

                            </tbody>
                        </table>
                        <fieldset runat="server" id="RoundPanel1">
                            <legend class="HelpUL">سایر فایل های پیوست</legend>

                            <table width="100%">
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <asp:Label runat="server" Text="فایل" ID="Label10"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpcOtherAttachment" InputType="Files"
                                                            ClientInstanceName="flpcOtherAttachment" OnFileUploadComplete="flpcOtherAttachment_FileUploadComplete"
                                                            MaxSizeForUploadFile="10000000" UploadWhenFileChoosed="true">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
imgEndOtherAttachment.SetVisible(true);
HyperLinkOtherAttachment.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}
else {
 imgEndOtherAttachment.SetVisible(false); 
 HyperLinkOtherAttachment.SetNavigateUrl('');
 }                                     
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ID="imgEndOtherAttachment" ToolTip="فایل انتخاب شد"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndOtherAttachment">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </td>

                                </tr>
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <asp:Label runat="server" Text="عنوان فایل پیوست" ID="Label11"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOthAttTitle" MaxLength="80"
                                            Width="100%">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td dir="ltr" align="center" colspan="2">
                                        <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;اضافه به ليست"
                                            ID="btnAddOtherAttachments" UseSubmitBehavior="False" ValidationGroup="b"
                                            OnClick="btnAddOtherAttachments_Click">
                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" colspan="2">
                                        <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                            Width="100%" RightToLeft="True" EnableCallBacks="False" ID="GridViewOtherAttachment"
                                            KeyFieldName="ImgId" AutoGenerateColumns="False"
                                            OnRowDeleting="GridViewOtherAttachment_RowDeleting">
                                            <Styles>
                                                <GroupPanel ForeColor="Black">
                                                </GroupPanel>
                                                <Header HorizontalAlign="Center">
                                                </Header>
                                            </Styles>
                                            <Settings ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ImgId"
                                                    Name="ImgId">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="1" FieldName="TempImgUrl" Caption="فایل"
                                                    Name="TempImgUrl">
                                                    <PropertiesHyperLinkEdit Target="_blank" Text="فایل">
                                                    </PropertiesHyperLinkEdit>
                                                </dxwgv:GridViewDataHyperLinkColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Desc" Caption="عنوان فایل"
                                                    Name="Desc">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="2" FieldName="fileName"
                                                    Name="fileName">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="2" Width="50px" Caption=" " ShowDeleteButton="true">
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>
                                            <StylesPager>
                                                <PageNumber HorizontalAlign="Center" VerticalAlign="Middle">
                                                </PageNumber>
                                            </StylesPager>
                                        </TSPControls:CustomAspxDevGridView2>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <div class="row">متن خبر *</div>
                        <div class="row">
                            <TSPControls:CustomASPxHtmlEditor ID="txtNewsBody" runat="server" Width="100%"  >                          
                     
                  
                                <%--     <SettingsHtmlEditing AllowIFrames="true" />--%>
                            </TSPControls:CustomASPxHtmlEditor>
                        </div>
                        <br />
                        <fieldset runat="server" id="ASPxRoundPanel4">
                            <legend class="HelpUL">انتخاب تصویر</legend>
                               <ul class="HelpUL">
                                <li>اندازه تصویر جهت اخبار با درجه اهمیت <b>بسیار مهم</b> 325*510 می باشد</li>
                                <li>اندازه تصویر جهت اخبار با درجه اهمیت <b>معمولی</b>و یا<b>اطلاعیه</b> 190*332 می باشد</li>
                            </ul>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <asp:Label runat="server" Text="تصویر" ID="Label14"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" ID="flpImg" InputType="Images"
                                                                UploadWhenFileChoosed="true" ClientInstanceName="flpc" OnFileUploadComplete="flpImg_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
  if(e.isValid)
  {
	imgEndUploadImgClient.SetVisible(true);
     }
    else
    {
	imgEndUploadImgClient.SetVisible(false);
     }
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ID="imgEndUploadImg" ToolTip="تصویر انتخاب شد" ClientVisible="False"
                                                                ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndUploadImgClient">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <asp:Label runat="server" Text="توضیحات" ID="Label15"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDesc" Width="100%">
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
                                        <td dir="ltr" align="center" colspan="2">
                                            <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;اضافه به ليست"
                                                ID="btnAddImg" UseSubmitBehavior="False" ValidationGroup="b"
                                                OnClick="btnAddImg_Click">
                                                <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="2">
                                            <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                                Width="100%" RightToLeft="True" EnableCallBacks="False" ID="CustomAspxDevGridView1"
                                                KeyFieldName="ImgId" AutoGenerateColumns="False"
                                                OnRowDeleting="CustomAspxDevGridView1_RowDeleting">

                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ImgId"
                                                        Name="ImgId">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="0" FieldName="TempImgUrl" Caption="تصویر"
                                                        Name="TempImgUrl">
                                                        <PropertiesHyperLinkEdit Target="_blank" Text="تصویر">
                                                        </PropertiesHyperLinkEdit>
                                                    </dxwgv:GridViewDataHyperLinkColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Desc" Caption="توضیحات"
                                                        Name="Desc">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="2" FieldName="fileName"
                                                        Name="fileName">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="2" Width="50px" Caption=" " ShowDeleteButton="true">
                                                    </dxwgv:GridViewCommandColumn>
                                                </Columns>
                                                <StylesPager>
                                                    <PageNumber HorizontalAlign="Center" VerticalAlign="Middle">
                                                    </PageNumber>
                                                </StylesPager>
                                            </TSPControls:CustomAspxDevGridView2>
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                         
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
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False" Visible="False">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/delete.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjectDataSourceSub" runat="server" CacheDuration="30"
                EnableCaching="True" UpdateMethod="Update" TypeName="TSP.DataManager.NewsSubjectManager"
                SelectMethod="GetData" OldValuesParameterFormatString="original_{0}" InsertMethod="Insert"
                DeleteMethod="Delete"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" CacheDuration="30"
                EnableCaching="True" TypeName="TSP.DataManager.AccountingAgentManager" SelectMethod="GetData"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:HiddenField ID="HDNewsId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server"></asp:HiddenField>

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
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
