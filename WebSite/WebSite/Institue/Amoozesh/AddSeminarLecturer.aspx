<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddSeminarLecturer.aspx.cs" Inherits="Institue_Amoozesh_AddSeminarLecturer"
    Title="مشخصات سخنران" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                        ToolTip="جدید" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                        ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                        <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="جدید" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">مشخصات سخنران</legend>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام *" ClientInstanceName="lblNameClient" Width="41px"
                                                ID="lblName" >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="150px"
                                                ClientInstanceName="TextName" EnableClientSideAPI="True" ID="txtTeName" >
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی *" ClientInstanceName="lblFamilyClient"
                                                Width="88px" ID="lblFamily">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="150px"
                                                ClientInstanceName="TextFamily" EnableClientSideAPI="True" ID="txtTeFamily">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام پدر *" RightToLeft="True" Width="54px" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="150px"
                                                ClientInstanceName="TextFather" EnableClientSideAPI="True" ID="txtTeFatherName"
                                                __designer:wfdid="w42">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ تولد *" Width="69px" ID="ASPxLabel11" >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="165px" ID="txtTeDate"
                                                Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ClientValidationFunction="PersianDateValidator"
                                                ValidateEmptyText="True" ControlToValidate="txtTeDate" ErrorMessage="تاریخ را وارد نمایید"
                                                Display="Dynamic" ID="PersianDateValidator2" ></pdc:PersianDateValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" Width="98px" ID="ASPxLabel8"
                                                __designer:wfdid="w46">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="150px"
                                                ClientInstanceName="TextIdNo" EnableClientSideAPI="True" ID="txtTeIdNo" >
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{0,10}"></RegularExpression>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد ملی *" Width="64px" ID="ASPxLabel12" >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="150px" MaxLength="10"
                                                ClientInstanceName="TextSSN" EnableClientSideAPI="True" ID="txtTeSSN" >
                                                <MaskSettings IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آخرین مدرک تحصیلی *" Width="119px" ID="lblicence"
                                              >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ODBLicence"
                                                TextField="LiName" ValueField="LiId" HorizontalAlign="Right"
                                                Width="150px" Height="21px"
                                                ClientInstanceName="cmbLicence"
                                                ID="cmbTeLicence">
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="مدرک را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="رشته *" Width="48px" ID="ASPxLabel3" __designer:wfdid="w52">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ODBMajor"
                                                TextField="MjName" ValueField="MjId" HorizontalAlign="Right"
                                                Width="150px" Height="21px" ClientInstanceName="cmbMajor"
                                                EnableClientSideAPI="True" ID="cmbTeMajor" __designer:wfdid="w53">
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن" ID="ASPxLabel9" __designer:wfdid="w54">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="150px" MaxLength="12"
                                                ClientInstanceName="TextTel" EnableClientSideAPI="True" ID="txtTeTel" __designer:wfdid="w55">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorFrameStyle Wrap="True">
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره همراه *" ID="ASPxLabel13" __designer:wfdid="w56">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="150px" MaxLength="11"
                                                ClientInstanceName="TextMobileNo" EnableClientSideAPI="True" ID="txtTeMobileNo"
                                                __designer:wfdid="w57">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="0\d{10}"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="120px" ID="ASPxLabel16"
                                                __designer:wfdid="w58">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" Width="246px"
                                                ClientInstanceName="TextEmail" EnableClientSideAPI="True" ID="txtTeEmail" __designer:wfdid="w59">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="این آدرس صحیح نیست" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                    <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel2" __designer:wfdid="w60">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" Width="463px"
                                                ID="txtTeAddress" __designer:wfdid="w61">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel4" __designer:wfdid="w62">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" Width="463px"
                                                ID="txtTeDesc" __designer:wfdid="w63">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend class="HelpUL">سوابق آموزشی و علمی</legend>

                            <table runat="server" id="tblTeacherFile" dir="rtl" width="100%">
                                <tr runat="server" id="Tr6">
                                    <td runat="server" id="TD11" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel23" __designer:wfdid="w30">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD12" valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" MaxSizeForUploadFile="0" InputType="Files"
                                                            ClientInstanceName="flpTe" FileUploadMode="OnPageLoad" ShowProgressPanel="True"
                                                            ID="flpTeacher" __designer:wfdid="w31" OnFileUploadComplete="flpTeacher_FileUploadComplete">

                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
	imgTeClient.SetVisible(true);
}"></ClientSideEvents>

                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgTeClient" ClientVisible="False" ID="ImgFlpTeacher" __designer:wfdid="w32">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr7">
                                    <td runat="server" id="TD13" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel24" __designer:wfdid="w33">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD14" valign="top" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" Width="552px"
                                            ID="txtDescTeImg" __designer:wfdid="w34">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr8">
                                    <td runat="server" id="TD15" align="center" colspan="2">
                                        <br />
                                        <TSPControls:CustomTextBox runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                            Text="اضافه"
                                            Width="70px" ID="btnAddTeFlp" __designer:wfdid="w35" OnClick="btnAddTeFlp_Click">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr1">
                                    <td runat="server" id="TD1" dir="rtl" align="center" colspan="2">
                                        <br />
                                        <TSPControls:CustomAspxDevGridView runat="server" ClientInstanceName="gridfile"
                                            KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                            Width="100%" ID="AspxGridFlpTeacher" EnableViewState="False" __designer:wfdid="w36"
                                            OnRowDeleting="AspxGridFlpTeacher_RowDeleting">
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="FilePath" Name="FilePath" Caption="فایل"
                                                    VisibleIndex="0">
                                                    <DataItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" Target="_blank" NavigateUrl='<%# Bind("TempImgUrl") %>'></asp:HyperLink>
                                                    </DataItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                                    </EditItemTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Caption="توضیحات"
                                                    VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="2" ShowDeleteButton="true">
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>
                                        
                                        </TSPControls:CustomAspxDevGridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table  >
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                        ToolTip="جدید" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                        ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                        <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>

                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton4" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ODBMajor" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBLicence" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:HiddenField ID="TeacherId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
</asp:Content>
