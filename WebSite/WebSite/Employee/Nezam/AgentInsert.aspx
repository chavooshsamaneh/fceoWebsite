<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AgentInsert.aspx.cs" Inherits="Accounting_Users_AgentInsert" Title="مشخصات نمایندگی" %>

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

    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
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
                <div style="width: 100%" dir="ltr">

                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <div dir="rtl">
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" align="right">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                        EnableTheming="False" ToolTip="جدید" ID="btnNew" EnableViewState="False" OnClick="btnNew_Click"
                                                                        UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                        Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                                                        OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                                        ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                                                        UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Visible="false" EnableClientSideAPI="True" CausesValidation="False"
                                                                        Text=" "  EnableTheming="False" ToolTip="حذف"
                                                                        ID="btnDelete" EnableViewState="False" OnClick="btnDelete_Click" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                                        <Image  Url="~/Images/icons/delete.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                        EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                                        UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>


                </div>
                <br />
                <div style="display: block" dir="ltr">

                    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr width="100%">
                                            <td align="right" valign="top" Width="15%">
                                              نام نمایندگی*
                                            </td>
                                            <td align="right" valign="top"  Width="35%">
                                                <TSPControls:CustomTextBox runat="server"  ID="txtName"  Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نام نمایندگی را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText=""></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top" Width="15%">
                                      کد نمایندگی*
                                            </td>
                                            <td align="right" valign="top"  Width="35%">
                                                <TSPControls:CustomTextBox runat="server" Width="100%"   ID="txtAgentCode"
                                                    >
                                                    <MaskSettings Mask="&lt;0..100000000000&gt;" />
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="کد نمایندگی را وارد نمایید" IsRequired="True" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                               شماره تلفن
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%"  ID="txtTel" 
                                                    MaxLength="12">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="تلفن را وارد نمایید" />
                                                        <RegularExpression ErrorText="شماره را با کد وارد نمایید" ValidationExpression="\d{11,12}" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">
                                               نمابر
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%"  ID="txtFax" 
                                                    MaxLength="12">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="فکس را وارد نمایید" />
                                                        <RegularExpression ErrorText="شماره را با کد وارد نمایید" ValidationExpression="\d{11,12}" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                               شماره همراه
                                            </td>
                                            <td colspan="3" align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtMobileNo"
                                                     MaxLength="11">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="شماره همراه را وارد نمایید" />
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{11,12}" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                               نشانی
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomASPXMemo runat="server" Height="30px" Width="100%"   ID="txtAddress"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="آدرس را وارد نمایید" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                               آدرس پست الکترونیکی
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomTextBox runat="server"   Width="100%" ID="txtEmail" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید" />
                                                        <RegularExpression ErrorText="این آدرس صحیح نیست" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">
                                              آدرس وب سایت
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtWebsite"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="" />
                                                        <RegularExpression ErrorText="این آدرس صحیح نیست" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td align="right" valign="top">
                                               تاریخ صدور مجوز
                                            </td>
                                            <td align="right" valign="top">
                                                <pdc:PersianDateTextBox ID="txtDate" runat="server"   DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                    PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator ID="PersianDateValidator1" runat="server" ClientValidationFunction="PersianDateValidator"
                                                    ControlToValidate="txtDate" ErrorMessage="تاریخ نامعتبر"></pdc:PersianDateValidator>
                                            </td>
                                            <td align="right" valign="top">
                                               شماره مجوز
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server"   Width="100%" ID="txtPerNo" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="شماره مجوز را وارد نمایید" />
                                                        <RegularExpression ErrorText="" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>

                                           <tr>
                                            <td align="right" valign="top">
                                               نام اداره امور مالیاتی
                                            </td>
                                            <td align="right" valign="top">
                                                   <TSPControls:CustomTextBox runat="server"   Width="100%" ID="txtTaxOfficeName" >
                                                   
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">
                                               شماره تلفن اداره امور مالیاتی
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server"   Width="100%" ID="txtTaxOfficeTell" >
                                                
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>


                                              <tr>
                                            <td align="right" valign="top">
                                               نشانی اداره امور مالیاتی
                                            </td>
                                            <td align="right" valign="top" colspan="3" >
                                                   <TSPControls:CustomTextBox runat="server"   Width="100%" ID="txtTaxOfficeAdress" >
                                                   
                                                </TSPControls:CustomTextBox>
                                            </td>
                                    
                                        </tr>


                                        <tr>
                                            <td align="right" valign="top">
                                              توضیحات
                                            </td>
                                            <td colspan="3" align="right" dir="rtl" valign="top">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" dir="rtl" valign="top">&nbsp;
                                            

  <TSPControls:CustomASPxHtmlEditor ID="txtDescription" runat="server" Width="100%">                                                                                                   
                                                    <settings allowhtmlview="False" />
                                                  
                                                </TSPControls:CustomASPxHtmlEditor>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanel>




                </div>
                <div style="width: 100%" dir="ltr">
                    <br />

                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <div dir="rtl">
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" align="right">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                        EnableTheming="False" ToolTip="جدید" ID="btnNew2" EnableViewState="False" OnClick="btnNew_Click"
                                                                        UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                        Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                                        OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                                        ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                                                        UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableClientSideAPI="True" CausesValidation="False"
                                                                        Text=" " Visible="false"  EnableTheming="False"
                                                                        ToolTip="حذف" ID="btnDelete2" EnableViewState="False" OnClick="btnDelete_Click"
                                                                        UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                                        <Image  Url="~/Images/icons/delete.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                        EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False"
                                                                        OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>



                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                    <img id="IMG1" src="../../Image/indicator.gif" align="middle" />
                    لطفا صبر نمایید ...
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
    <asp:HiddenField ID="PkAgentId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
</asp:Content>
