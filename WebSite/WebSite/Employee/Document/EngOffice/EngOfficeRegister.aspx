<%@ Page Title="مشخصات دفتر" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeRegister.aspx.cs" Inherits="Employee_Document_EngOffice_EngOfficeRegister" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function btnAdd_Click() {
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');

            if (txtaNumber.GetIsValid() && txtaAmount.GetIsValid()) {
                grid.PerformCallback('Add');
            }
        }

        function ClearAccounting() {
            cmbAccType.SetSelectedIndex(0);
            CallbackAccFish.PerformCallback();
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');
            txtDate.value = "";
            txtaNumber.SetText("");
            txtaAmount.SetText(HiddenFieldEngOffice.Get('FishAmount'));
            txtaDesc.SetText("");
        }

        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        function SetMeDocDefualtExpireDateJS(s) {
            CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table align="right">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                        ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click" CausesValidation="False"
                                        Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/new.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Width="25px" EnableTheming="False"
                                        ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click"
                                        CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                        ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                        Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                        ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                        CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>


            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="EngOffice" Text="مشخصات دفتر" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضای دفتر">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attach" Text="مستندات">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <table dir="rtl" width="100%" align="center">
                <tr>
                    <td width="5%" align="right">
                        <blink id="bkImgWarningMsg"><dxe:ASPxImage ID="ImgWarningMsg" ClientVisible="false" Width="25px" Height="25px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                    </td>
                    <td width="95%" align="right">
                        <asp:Label ID="lblWarningText" Font-Bold="true" ForeColor="DarkRed" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelRequest" ClientInstanceName="RoundPanelRequest"
                HeaderText="مشاهده" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table dir="rtl" width="100%">
                            <tr>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel runat="server" Text="کد دفتر" ID="ASPxLabel53" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" style="width: 35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtEngOffId"
                                        Enabled="false">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td style="width: 15%"></td>
                                <td style="width: 35%"></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel runat="server" Text="نام دفتر" ID="ASPxLabel5" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" align="right" valign="top" style="width: 85%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtEngOffName">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField ErrorText="نام دفتر را وارد نمایید" IsRequired="True" />
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel runat="server" Text="شماره مشارکت نامه" ID="ASPxLabel100" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" style="width: 35%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" Style="direction: ltr" ID="txtLetterNo">
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" style="width: 15%" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="تاریخ مشارکت نامه" ID="ASPxLabel4" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" style="width: 35%" valign="top">
                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                        ShowPickerOnEvent="OnClick" PickerDirection="ToRight" ShowPickerOnTop="True"
                                        Width="225px" ID="txtLetterDate"></pdc:PersianDateTextBox>
                                    <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                        ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtLetterDate" ValidationGroup="Acc"
                                        ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="نوع دفتر *" ID="ASPxLabel2" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="OdbEOffType"
                                        TextField="Name" ValueField="EOfTId"
                                        ID="ComboEOfTId" RightToLeft="True"
                                        Width="100%">
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField ErrorText="نوع دفتر را انتخاب نمایید" IsRequired="True" />
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="شماره دفتر اسناد رسمی" ID="ASPxLabel7" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" dir="ltr" valign="top">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtDaftarNo"
                                        MaxLength="21">
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="تلفن *" ID="ASPxLabel10" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtTel">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="False" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="فکس" ID="ASPxLabel1" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtFax">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RegularExpression ErrorText="شماره فکس را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="شماره همراه *" ID="Label30" Width="100%"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMobileNo" Width="100%"
                                        MaxLength="11">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText="شماره همراه را صحیح وارد نمایید" ValidationExpression="0\d{1,10}"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td valign="top" align="right">
                                    <asp:Label ID="Label1" runat="server" Text="آدرس پست الکترونیکی" Width="100%"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEmail" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText="آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="محل دفتر اسناد رسمی" ID="ASPxLabel9" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <TSPControls:CustomASPXMemo ID="txtDaftarLoc" runat="server"
                                        Height="40px" Width="100%">
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="آدرس *" ID="ASPxLabel3" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <TSPControls:CustomASPXMemo ID="txtAddress" runat="server"
                                        Height="40px" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RequiredField ErrorText="آدرس  را وارد نمایید"></RequiredField>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <TSPControls:CustomASPXMemo ID="txtDesc" runat="server"
                                        Height="40px" Width="100%">
                                        <ValidationSettings>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td align="right" valign="top">سند مالکیت
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" InputType="Files" UploadWhenFileChoosed="True"
                                                        ClientInstanceName="FileUploadOwnership" ID="FileUploadOwnership" OnFileUploadComplete="FileUploadOwnership_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientOwnership.SetVisible(true);
    HiddenFieldEngOffice.Set('OwnershipImage',1);
	lblImageWarningOwnership.SetVisible(false);
	ImageOwnership.SetVisible(true);
	ImageOwnership.SetNavigateUrl('../../../image/EngOffice/Ownership/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientOwnership.SetVisible(false);
    HiddenFieldEngOffice.Set('OwnershipImage',0);
	lblImageWarningOwnership.SetVisible(true);
	ImageOwnership.SetVisible(false);
	ImageOwnership.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel runat="server" Text="سند مالکیت را انتخاب نمایید" ClientInstanceName="lblImageWarningOwnership"
                                                        ClientVisible="False" ForeColor="Red" ID="lblImageWarningOwnership">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="سند مالکیت انتخاب شد"
                                                        ClientInstanceName="imgEndUploadImgClientOwnership" ClientVisible="False" ID="imgEndUploadImgOwnership">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="divLicense" onclick="GetImage(ImageOwnership)">
                                        <dxe:ASPxHyperLink runat="server" Text="سند مالکیت" Width="150px"
                                            ID="ImageOwnership" ClientInstanceName="ImageOwnership" Target="_blank">
                                        </dxe:ASPxHyperLink>
                                    </div>
                                </td>
                            </tr>
                            <tr id="Tr4" runat="server">
                                <td align="right" valign="top">مشارکت مدنی
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" InputType="Files" UploadWhenFileChoosed="True"
                                                        ClientInstanceName="FileUploadImagepartnership" ID="FileUploadImagepartnership" OnFileUploadComplete="FileUploadpartnership_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientpartnership.SetVisible(true);
    HiddenFieldEngOffice.Set('partnershipImage',1);
	lblImageWarningpartnership.SetVisible(false);
	Imagepartnership.SetVisible(true);
	Imagepartnership.SetNavigateUrl('../../../image/EngOffice/partnership/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientpartnership.SetVisible(false);
    HiddenFieldEngOffice.Set('partnershipImage',0);
	lblImageWarningpartnership.SetVisible(true);
	Imagepartnership.SetVisible(false);
	Imagepartnership.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel runat="server" Text="مشارکت مدنی را انتخاب نمایید" ClientInstanceName="lblImageWarningpartnership"
                                                        ClientVisible="False" ForeColor="Red" ID="ASPxLabel11">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="مشارکت مدنی انتخاب شد"
                                                        ClientInstanceName="imgEndUploadImgClientpartnership" ClientVisible="False" ID="ASPxImage1">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="div1" onclick="GetImage(Imagepartnership)">
                                        <dxe:ASPxHyperLink runat="server" Text="مشارکت مدنی" Width="150px"
                                            ID="Imagepartnership" ClientInstanceName="Imagepartnership" Target="_blank">
                                        </dxe:ASPxHyperLink>
                                    </div>
                                </td>
                            </tr>
                            <tr id="Tr5" runat="server">
                                <td align="right" valign="top">تعهد نامه شرکاء
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" InputType="Files" UploadWhenFileChoosed="True"
                                                        ClientInstanceName="FileUploadImagePartnerDisclaimer" ID="FileUploadPartnerDisclaimer" OnFileUploadComplete="FileUploadPartnerDisclaimer_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientPartnerDisclaimer.SetVisible(true);
    HiddenFieldEngOffice.Set('PartnerDisclaimerImage',1);
	lblImageWarningPartnerDisclaimer.SetVisible(false);
	ImagePartnerDisclaimer.SetVisible(true);
	ImagePartnerDisclaimer.SetNavigateUrl('../../../image/EngOffice/PartnerDisclaimer/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientPartnerDisclaimer.SetVisible(false);
    HiddenFieldEngOffice.Set('PartnerDisclaimerImage',0);
	lblImageWarningPartnerDisclaimer.SetVisible(true);
	ImagePartnerDisclaimer.SetVisible(false);
	ImagePartnerDisclaimer.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel runat="server" Text="تعهد نامه شرکاء را انتخاب نمایید" ClientInstanceName="lblImageWarningPartnerDisclaimer"
                                                        ClientVisible="False" ForeColor="Red" ID="ASPxLabel12">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تعهد نامه شرکاء انتخاب شد"
                                                        ClientInstanceName="imgEndUploadImgClientPartnerDisclaimer" ClientVisible="False" ID="ASPxImage2">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="div2" onclick="GetImage(ImagePartnerDisclaimer)">
                                        <dxe:ASPxHyperLink runat="server" Text="تعهد نامه شرکاء" Width="150px"
                                            ID="ImagePartnerDisclaimer" ClientInstanceName="ImagePartnerDisclaimer" Target="_blank">
                                        </dxe:ASPxHyperLink>
                                    </div>
                                </td>
                            </tr>
                            <tr id="Tr6" runat="server">
                                <td align="right" valign="top">استعلام از شورای انتظامی
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" InputType="Files" UploadWhenFileChoosed="True"
                                                        ClientInstanceName="FileUploadImageInqueryMembers" ID="FileUploadImageInqueryMembers" OnFileUploadComplete="FileUploadInqueryMembers_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientInqueryMembers.SetVisible(true);
    HiddenFieldEngOffice.Set('InqueryMembersImage',1);
	lblImageWarningInqueryMembers.SetVisible(false);
	ImageInqueryMembers.SetVisible(true);
	ImageInqueryMembers.SetNavigateUrl('../../../image/EngOffice/InqueryMembers/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientInqueryMembers.SetVisible(false);
    HiddenFieldEngOffice.Set('InqueryMembersImage',0);
	lblImageWarningInqueryMembers.SetVisible(true);
	ImageInqueryMembers.SetVisible(false);
	ImageInqueryMembers.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel runat="server" Text="استعلام را انتخاب نمایید" ClientInstanceName="lblImageWarningInqueryMembers"
                                                        ClientVisible="False" ForeColor="Red" ID="ASPxLabel14">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="استعلام انتخاب شد"
                                                        ClientInstanceName="imgEndUploadImgClientInqueryMembers" ClientVisible="False" ID="ASPxImage3">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="div3" onclick="GetImage(ImageInqueryMembers)">
                                        <dxe:ASPxHyperLink runat="server" Text="استعلام از شورای انتظامی" Width="150px"
                                            ID="ImageInqueryMembers" ClientInstanceName="ImageInqueryMembers" Target="_blank">
                                        </dxe:ASPxHyperLink>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <fieldset runat="server">
                                        <legend class="fieldset-legend" dir="rtl"><b>مشخصات مجوز</b>
                                        </legend>

                                        <TSPControls:CustomAspxCallbackPanel runat="server"
                                            ClientInstanceName="CallbackPanelDoRegDate" Width="100%" ID="CallbackPanelDoRegDate"
                                            OnCallback="CallbackPanelDoRegDate_Callback">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent11" runat="server">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 100%" colspan="4" align="justify" align="center">
                                                                    <dxe:ASPxLabel runat="server" Font-Bold="true" Text="نکات تاریخ صدور" ID="lblRegDateComment"
                                                                        ForeColor="DarkRed" Visible="false">
                                                                    </dxe:ASPxLabel>
                                                                    <br />
                                                                    <br />
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top" style="width: 15%">
                                                                    <dxe:ASPxLabel ID="lblFileNo" runat="server" Text="شماره پروانه">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" valign="top" dir="rtl" style="width: 35%">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFileNo" runat="server"
                                                                        Enabled="False" Width="100%" Style="direction: ltr" RightToLeft="True">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td align="right" valign="top" style="width: 15%">
                                                                    <dxe:ASPxLabel ID="ASPxLabeld1" runat="server" Text="موقت/دائم" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td id="Td2" runat="server" align="right" valign="top" style="width: 35%">
                                                                    <TSPControls:CustomAspxComboBox ID="cmbdIsTemporary" ClientInstanceName="cmbIsTemporary" runat="server"
                                                                        ValueType="System.String" Width="100%" RightToLeft="True">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());}" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="پروانه دائم" Value="0" />
                                                                            <dxe:ListEditItem Text="پروانه موقت" Value="1" />
                                                                        </Items>
                                                                        <ButtonStyle Width="13px">
                                                                        </ButtonStyle>
                                                                    </TSPControls:CustomAspxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="Td3" runat="server" align="right" dir="rtl" valign="top">
                                                                    <dxe:ASPxLabel ID="ASPxLabeld2" runat="server" Text="شماره سریال" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td id="Td4" runat="server" align="right" dir="rtl" style="vertical-align: top">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtdSerialNo" runat="server"
                                                                        Width="100%">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td id="Td1" runat="server" align="right" dir="rtl" valign="top">
                                                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="تاریخ اولین صدور" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td id="Td9" runat="server" align="right" dir="rtl" style="vertical-align: top">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFirstRegDate" runat="server"
                                                                        Width="100%" Enabled="false" Style="direction: ltr" RightToLeft="True">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr2" runat="server">
                                                                <td id="Td5" runat="server" align="right" valign="top">
                                                                    <dxe:ASPxLabel ID="lblRegDate" runat="server" ClientInstanceName="lblDate" Text="تاریخ صدور"
                                                                        Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td id="Td6" runat="server" align="right" dir="rtl" valign="top">
                                                                    <table Width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <pdc:PersianDateTextBox ID="txtLastRegDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                                                        PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                                                                        Width="100%" onchange="return SetMeDocDefualtExpireDateJS(event);"></pdc:PersianDateTextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxImage ID="btnSetRegDate" ClientInstanceName="btnSetRegDate" ToolTip="تنظیم تاریخ اعتبار"
                                                                                        runat="server" Text="" Height="13px" Border-BorderWidth="1px" Border-BorderColor="LightBlue"
                                                                                        Width="13px" Image-Height="13px" Image-Width="13px" ImageUrl="~/Images/ResetDate.png">
                                                                                        <ClientSideEvents Click="function(s, e) {                                                                                                                
                                                                                                                 SetMeDocDefualtExpireDateJS(this); }" />
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td id="Td7" runat="server" align="right" dir="rtl" valign="top">
                                                                    <dxe:ASPxLabel ID="ASPxLabeld4" runat="server" ClientInstanceName="lblDate" Text="تاریخ پایان اعتبار"
                                                                        Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td id="Td8" runat="server" align="right" dir="rtl" valign="top">
                                                                    <pdc:PersianDateTextBox ID="txtExpDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                                        PickerDirection="ToRight" ShowPickerOnTop="True" Width="100%"></pdc:PersianDateTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </TSPControls:CustomAspxCallbackPanel>
                                    </fieldset>
                                </td>
                            </tr>

                            <tr>
                                <td valign="top" align="right">شرح درخواست
                                </td>
                                <td dir="rtl" valign="top" align="right" colspan="3">
                                    <TSPControls:CustomASPXMemo runat="server" Height="34px" Width="100%"
                                        ClientInstanceName="txtRequestDesc" ID="txtRequestDesc">
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
                        </table>
                        <fieldset id="RoundPanelAccounting" runat="server">
                            <legend class="HelpUL">ثبت فیش</legend>
                            <asp:Panel ID="PanelAccountingInserting" runat="server">
                                <TSPControls:CustomAspxCallbackPanel ID="CallbackAccFish" runat="server" ClientInstanceName="CallbackAccFish"
                                    HideContentOnCallback="False"
                                    OnCallback="CallbackAccFish_Callback" Width="100%">
                                    <LoadingPanelImage Url="~/Image/indicator.gif" />
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent9" runat="server">
                                            <div align="center">
                                                <table id="tableAccounting" dir="rtl" runat="server" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="4" valign="top" align="center">
                                                                <dxe:ASPxLabel runat="server" ID="lblRegEnter" ClientInstanceName="lblRegEnter" ForeColor="Blue">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel runat="server" ID="lblReg" ClientInstanceName="lblReg" ClientVisible="false"
                                                                    ForeColor="Blue">
                                                                </dxe:ASPxLabel>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="right" style="width: 15%">
                                                                <dxe:ASPxLabel runat="server" Text="بابت" ID="ASPxLabel17" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td dir="ltr" valign="top" align="right" style="width: 35%">
                                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbAccType"
                                                                    ValueType="System.Int32" SelectedIndex="0"
                                                                    DataSourceID="ObjectDataSourceAccType"
                                                                    TextField="TypeName" ValueField="AccTypeId" ClientInstanceName="cmbAccType" RightToLeft="True">
                                                                    <ClientSideEvents SelectedIndexChanged="function(e,s){CallbackAccFish.PerformCallback();}" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ValidationSettings Display="Dynamic">
                                                                        <RequiredField ErrorText=""></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                    <ButtonStyle Width="13px">
                                                                    </ButtonStyle>
                                                                    <LoadingPanelImage Url="~/App_Themes/Glass/Editors/Loading.gif">
                                                                    </LoadingPanelImage>
                                                                </TSPControls:CustomAspxComboBox>
                                                                <asp:ObjectDataSource ID="ObjectDataSourceAccType" runat="server" SelectMethod="GetData"
                                                                    TypeName="TSP.DataManager.TechnicalServices.AccTypeManager"></asp:ObjectDataSource>
                                                            </td>
                                                            <td valign="top" align="right" style="width: 15%">
                                                                <dxe:ASPxLabel runat="server" Text="مبلغ" ID="ASPxLabel33" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td dir="ltr" valign="top" align="right" style="width: 35%">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtaAmount" Width="100%"
                                                                    ClientInstanceName="txtaAmount">
                                                                    <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                                        <RegularExpression ErrorText="نامعتبر" ValidationExpression="[1-9]\d*"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="شماره" ID="ASPxLabel31" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtaNumber" Width="100%"
                                                                    Style="direction: ltr" ClientInstanceName="txtaNumber">
                                                                    <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel32" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                                    Width="100%" ShowPickerOnTop="True" ValidationGroup="Acc" ID="txtaDate" PickerDirection="ToRight"
                                                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtaDate" ValidationGroup="Acc"
                                                                    ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td valign="top" align="right" colspan="3">
                                                                <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtaDesc" Width="100%"
                                                                    ClientInstanceName="txtaDesc">
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
                                                            <td valign="middle" align="center" colspan="4">
                                                                <br />
                                                                <TSPControls:CustomAspxButton runat="server" Text="اضافه به لیست" ID="btnAddAccounting"
                                                                    ValidationGroup="Acc" AutoPostBack="False"
                                                                    UseSubmitBehavior="False">
                                                                    <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                                    <ClientSideEvents Click="function(s, e) { btnAdd_Click(); }" />
                                                                </TSPControls:CustomAspxButton>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="center" colspan="4">
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                    <LoadingPanelStyle>
                                        <border borderstyle="Double" />
                                    </LoadingPanelStyle>
                                    <ClientSideEvents EndCallback="function(s, e) { }" />
                                </TSPControls:CustomAspxCallbackPanel>
                            </asp:Panel>
                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%" ID="GridViewAccounting"
                                KeyFieldName="AccountingId" AutoGenerateColumns="False" OnHtmlRowPrepared="GridViewAccounting_HtmlRowPrepared"
                                OnRowDeleting="GridViewAccounting_RowDeleting" ClientInstanceName="grid" OnCustomCallback="GridViewAccounting_CustomCallback">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Name="Delete"
                                        Width="25px">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AccType"
                                        Caption="بابت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Amount" Caption="مبلغ">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                        <PropertiesTextEdit DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <ClientSideEvents EndCallback="function(s,e){
                                        if(s.cpSaveComplete=='1'){
                                         ClearAccounting();
                                         s.cpSaveComplete='0';
                                         }
                                        else if(s.cpMessage!='')
                                        {
                                         ShowMessage(s.cpMessage);
                                         s.cpMessage='';
                                        }
                                        }" />
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table align="right">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                        ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click" CausesValidation="False"
                                        Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/new.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Width="25px" EnableTheming="False"
                                        ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click"
                                        CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                        ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                        Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                        ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click"
                                        CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldEngOffice" ClientInstanceName="HiddenFieldEngOffice">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="OdbEOffType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.EngOfficeTypeManager"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
