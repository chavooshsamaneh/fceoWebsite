<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ObserverInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_ObserverInsert"
    Title="مشخصات ناظر" %>

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
<%@ Register Src="~/UserControl/CapacityUserControl.ascx" TagPrefix="TSP" TagName="Capacity" %>
<%@ Register Src="~/UserControl/WorkRequestUserControl.ascx" TagPrefix="TSP" TagName="WorkRequestUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script lang="javascript">

        function CheckSaveConditions(value) {
            if (ASPxClientEdit.ValidateGroup() == false) {
                value.processOnServer = false;
                return;
            }
            if (HiddenFieldObserver.Get('ShowAlert') == 1) {
                value.processOnServer = confirm(HiddenFieldObserver.Get('AlertMsg') + 'آیا با ذخیره اطلاعات موافق می باشید؟');
            }
        }

        function SetLbalesBsedOnMembershipType() {
            if (CmbType.GetSelectedIndex() == 0) {
                PanelDocumentFileNo.SetVisible(true);
                lbltxtMeIdSearchTitle.SetText('کد عضویت');
            }
            else if (CmbType.GetSelectedIndex() == 1) {
                PanelDocumentFileNo.SetVisible(false);
                lbltxtMeIdSearchTitle.SetText('كد عضويت كانون كاردان ها');
            }

        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>[<a class="closeLink"
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
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s,e){
                                                                       CheckSaveConditions(e);
                                                                            }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                        </TSPControls:MenuSeprator>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی ناظران"
                                            ID="btnObsAcc" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnObsAcc_Click" CausesValidation="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSImpAcc.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
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
            <TSP:ProjectInfo ID="prjInfo" runat="server" />
            <div align="right">
                <ul class="HelpUL">
                    <li><b>جهت جستجو بر اساس شماره پروانه می توانید ااز یکی از دو فرمت زیر استفاده نمایید.</b></li>
                    <li><b>در صورتی که از فرمت تفکیک شده شماره پروانه استفاده می نمایید نیازی به وارد کردن
                        صفر پیش از عدد در قسمت شماره سریال نمی باشد.</b></li>
                    <li><b>در صورتی که از فرمت پیوسته شماره پروانه استفاده می نمایید بایستی شماره پروانه
                        به صورت کامل و دقیق مطابق با آنچه در واحد پروانه اشتغال ثبت شده است وارد نمایید.(وارد
                        نمودن صفرهای پیش از عدد در قسمت شماره سریال شماره پروانه الزامی می باشد.)</b></li>
                </ul>
            </div>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset id="RoundPanelMeInfo"
                            runat="server">
                            <legend class="HelpUL">انتخاب ناظر</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="با آگاهی کامل نسبت به قوانین سازمان، قصد ثبت اطلاعات بدون در نظر گرفتن کلیه پیش شرط ها را دارم"
                                                Wrap="False" ID="CheckBoxSaveWithOutCondition">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع ناظر" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                ID="CmbType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="CmbType"
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                                SetLbalesBsedOnMembershipType();

}"></ClientSideEvents>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="نوع ناظر را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <Items>
                                                    <dxe:ListEditItem Value="1" Text="شخص حقیقی" Selected="True"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="4" Text="کاردان"></dxe:ListEditItem>

                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت *" ID="lbltxtMeIdSearchTitle" ClientInstanceName="lbltxtMeIdSearchTitle">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtID" Width="100%" AutoPostBack="True"
                                                ClientInstanceName="txtID" OnTextChanged="txtID_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>

                                    </tr>


                                </tbody>
                            </table>

                            <dxcp:ASPxPanel ID="PanelDocumentFileNo" ClientInstanceName="PanelDocumentFileNo"
                                runat="server">
                                <PanelCollection>
                                    <dxcp:PanelContent>
                                        <table width="100%">
                                            <tbody>

                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="lblMeFileNoSeprated">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="5%">
                                                                    <dxe:ASPxLabel runat="server" Text="استان" ID="lblMeDocProvCode">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" width="15%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtMeDocProvCode" Width="100%"
                                                                        ClientInstanceName="txtMeDocProvCode" MaxLength="2">
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d{2}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td width="5%">
                                                                    <dxe:ASPxLabel runat="server" Text="رشته" ID="lblMeMjCode">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" width="15%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtMeMjCode" Width="100%"
                                                                        ClientInstanceName="txtMeMjCode" MaxLength="3">
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d{3}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>

                                                                <td width="5%">
                                                                    <dxe:ASPxLabel runat="server" Text="سریال" ID="lblMeDocSerialNo">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" width="21%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtMeDocSerialNo" Width="100%"
                                                                        ClientInstanceName="txtMeDocSerialNo" OnTextChanged="txtMeDocSerialNo_TextChanged"
                                                                        AutoPostBack="true" MaxLength="6">
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage>
                                                                            </ErrorImage>
                                                                            <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                        <ClientSideEvents TextChanged="function(s, e) {
if(txtMeDocProvCode.GetText()=='' || txtMeDocProvCode.GetText()==null ||txtMeMjCode.GetText()=='' || txtMeMjCode.GetText()==null)
{
alert('کد استان و رشته را به صورت کامل وارد نمایید');
e.ProsseccOnServer=false;
return;
}
else
e.ProsseccOnServer=true;

}"></ClientSideEvents>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel Font-Bold="true" ForeColor="Red" runat="server" Text="یا" ID="lblMeDocOR">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel13">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtFileNo" OnTextChanged="txtFileNo_TextChanged"
                                                            Width="100%" ClientInstanceName="txtFileNo"
                                                            Style="direction: ltr" AutoPostBack="True">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" >
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ErrorText="شماره پروانه به صورت *******-***-**  می باشد" ValidationExpression="\d{2}-\d{3}-\d{1,7}" />
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </dxcp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxPanel>
                        </fieldset>
                        <TSP:WorkRequestUserControl runat="server" ID="WorkRequestUserControl" />
                        <fieldset id="Fieldset1"
                            runat="server">
                            <legend class="HelpUL">مشخصات ناظر</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <dxe:ASPxLabel runat="server" Text="زمینه نظارت *" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Title" ID="cmbObsType" DataSourceID="ObjectDataSourceObserverType"
                                                ValueType="System.String" RightToLeft="True" ValueField="ObserversTypeId">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="زمینه نظارت را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد نظارت شهرسازی *" ID="ASPxLabel3" ClientInstanceName="lblLastName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtArchitectorCode" Width="100%"
                                                ClientInstanceName="txtArchitectorCode">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="کد نظارت شهرسازی را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تعرفه خدمات مهندسی *" ID="lblPricearchive" ClientInstanceName="lblPricearchive">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                TextField="YearName" ID="cmbPriceArchive" DataSourceID="ObjectDataSource_PriceArchive"
                                                ValueType="System.String" ValueField="PriceArchiveId" ClientInstanceName="cmbPriceArchive"
                                                Width="100%" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="تعرفه را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSource_PriceArchive" runat="server" TypeName="TSP.DataManager.TechnicalServices.PriceArchiveManager"
                                                SelectMethod="SelectActivePriceArchive"></asp:ObjectDataSource>
                                        </td>
                                        <td>سال کاری</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Year" ID="comboYear" DataSourceID="ObjectCapacityAssignment"
                                                ValueType="System.String" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="سال کاری را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectCapacityAssignment" runat="server" TypeName="TSP.DataManager.TechnicalServices.CapacityAssignmentManager"
                                                SelectMethod="SelectTSCapacityAssignmentYears">
                                                <SelectParameters>
                                                    <asp:Parameter DbType="Int16" DefaultValue="-1" Name="IsMainAgent" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="ناظر هماهنگ کننده می باشد" ID="ChbMother" ClientInstanceName="ChbMother">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="تنها پنج درصد سهم سازمان پرداخت شود" ID="CheckBoxFivePercentPayment" ClientInstanceName="CheckBoxFivePercentPayment">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="ثبت کارکرد به دلیل اضافه اشکوب می باشد"
                                                Wrap="False" ID="ChbIsExteraFloor">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <TSP:Capacity ID="CapacityUserControl" runat="server" />


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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
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
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s,e){
                                                                       CheckSaveConditions(e);
                                                                            }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                        </TSPControls:MenuSeprator>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی ناظران"
                                            ID="btnObsAcc2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnObsAcc_Click" CausesValidation="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSImpAcc.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDObsId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDCitId" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HiddenFieldObserver" ClientInstanceName="HiddenFieldObserver"
                runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceObserverType" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.ObserversTypeManager"></asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                        <img alt="" id="IMG2" src="../../Image/indicator.gif" align="middle" />
                        لطفا صبر نمایید ...
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
