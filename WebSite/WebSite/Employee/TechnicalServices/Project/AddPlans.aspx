<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddPlans.aspx.cs" Inherits="Employee_TechnicalServices_Project_AddPlans"
    Title="مشخصات نقشه" %>

<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div align="center">
        <asp:UpdatePanel ID='UpdatePanel1' runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار نقشه"
                                                ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="طراح جدید"
                                                ID="btnDesigners" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                OnClick="btnDesigners_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/Designers.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>
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
                <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" CssClass="ProjectMainMenuHorizontal"
                    OnItemClick="MainMenu_ItemClick">
                    <Items>
                        <dxm:MenuItem Text="مشخصات پروژه" Name="Project" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            <Items>
                                <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" Selected="true" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="پلاک ثبتی" Name="RegisteredNo" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="دستور نقشه" Name="PlansMethod" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="بلوک" Name="Block" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="بیمه" Name="Insurance" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                            </Items>
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مالک" Name="Owner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مالی پروژه" Name="Accounting" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            <Items>
                                <%--             <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner">
                                </dxm:MenuItem>--%>
                                <dxm:MenuItem Text="مالی ناظران" Name="AccObserver" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <%-- <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
                                </dxm:MenuItem>--%>
                            </Items>
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="نقشه" Name="Plans" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="طراح" Name="Designer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="ناظر" Name="Observers" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="قرارداد" Name="Contract" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <%-- <dxm:MenuItem Text="مجری" Name="Implementer">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="زمان بندی" Name="Timing">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement">
                            </dxm:MenuItem>--%>
                    </Items>

                </TSPControls:CustomAspxMenuHorizontal>
                <br />
                <TSPControls:CustomAspxMenuHorizontal ID="MenuPlan" runat="server"
                    OnItemClick="MenuPlan_ItemClick" CssClass="ProjectSubMenuHorizontal">
                    <ItemStyle HorizontalAlign="Right" Font-Size="X-Small" Font-Bold="true" />
                    <Items>
                        <dxm:MenuItem Name="Plan" Selected="True" Text="مشخصات نقشه" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="PlanDes" Text="طراحان نقشه" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="ControlerViewPoint" Text="بازبین نقشه" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                        </dxm:MenuItem>
                    </Items>

                </TSPControls:CustomAspxMenuHorizontal>

                <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
                <br />
                <div align="center" style="width: 100%">
                    <dxe:ASPxLabel runat="server" Text="هشدار" Width="100%" ID="lblPlanWarning" Visible="false"
                        Font-Bold="true" ForeColor="DarkRed" />
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelPlans" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <div class="Item-center">
                                <dxe:ASPxLabel runat="server" Text="وضعیت درخواست: نامشخص" Font-Bold="False" ID="lblWorkFlowState"
                                    ForeColor="Red">
                                </dxe:ASPxLabel>
                            </div>
                            <fieldset>
                                <legend class="HelpUL">اطلاعات پایه نقشه</legend>

                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 115px" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کد پیگیری" ID="ASPxLabel29">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtFollowCode"
                                                    Enabled="false">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right"></td>
                                            <td valign="top" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="نوع نقشه" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                    TextField="Title" ID="cmbPlanType" DataSourceID="ObjdsPlansType" ValueType="System.String"
                                                    ValueField="PlansTypeId" RightToLeft="True" OnSelectedIndexChanged="cmbPlanType_SelectedIndexChanged" AutoPostBack="true">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نوع نقشه را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="شماره نقشه" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtPlanNo" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="شماره نقشه را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel5">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtPlanDes" Width="100%">
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
                                            <td valign="top" align="right" colspan="4">
                                                <TSPControls:CustomASPxCheckBox runat="server" Text="نقشه بدون ثبت بازبین نقشه در سیستم تایید شده در نظرگرفته شود" ID="CheckBoxConfirmed"
                                                    ClientInstanceName="CheckBoxConfirmed">
                                                </TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right" colspan="4">

                                                <fieldset>
                                                    <legend class="HelpUL">فایل پیوست</legend>
                                                    <ul class="HelpUL" runat="server" id="ArchNotification" visible="false">
                                                        <p><b>در خصوص طراحی نقشه های معماری رعایت موارد ذیل الزامی است</b></p>
                                                        <li>كليه موارد مربوط به مقررات ملي ساختمان واصول شهرسـازي ودستورنقشه رعایت شده باشد.</li>
                                                        <li>نقشه پيوسـت شامـل سايت پلان، پلان طبقات، بـرش به تعداد موردنياز پروژه، نماهـا، پلان تيرريزي همراه با محل بادبند يا ديواربرشي باشد.</li>
                                                        <li>رعايت ضوابط ومقررات شهرسازي پروژه الزامي مي باشد و مسئوليت هرگونه مغايرت با مهندس طراح معمار مي باشد.</li>
                                                        <li>رعايت حداقل مساحت نورگيرها الزامي است.</li>
                                                        <li>رعايت مباحث مقررات ملي ساختمان الزامي است.</li>
                                                        <li>رعايت ضوابط و مقررات مناسب سازي فضا براي معلولين الزامي است.</li>
                                                        <li>ارائه نقشه برداري تاييد شده توسط مهندس نقشه بردار جهت زمين هاي شيب دار الزامي است.</li>
                                                        <li>رعايت درز انقطاع با سازه هاي مجاور الزامي است.</li>
                                                        <li>محل ديوارهاي برشي يا بادبندها مناسب باشد.</li>
                                                        <li>ابعاد ستونها براي پاركينگ مزاحمت نداشته باشد.</li>
                                                        <li>محل وابعاد داكتهاي تاسيساتي ومسير عبور كولر مناسب باشد.</li>
                                                    </ul>
                                                    <ul class="HelpUL" runat="server" id="TasisatNotification">
                                                        <p><b>در خصوص طراحی نقشه های تاسیسات برقی و مکانیکی رعایت موارد ذیل الزامی است</b></p>
                                                        <li>نوع سیستم گرمایشی و سرمایشی و نوع اسکلت و سقف و نوع کاربری ساختمان به همراه متراژ و تعداد طبقات در فرم مربوطه قید شده باشد.</li>
                                                        <li>نقشه ها با مقیاس یک صدم باشد.</li>
                                                        <li>نقشه های معماری تایید شده ضمیمه شده باشد.</li>
                                                        <li>جهت شمال و قبله در نقشه ها مشخص باشد.</li>
                                                        <li>نقشه ها با محورهای طولی وعرضی طرح شده باشد.</li>
                                                        <li>عنوان در نقشه ها مشخص باشد.</li>
                                                        <li>پلان تیرریزی سقف (سازه ای) ضمیمه شده باشد.</li>
                                                        <li>درصورت استفاده از تجهیزات و سیستم های تاسیساتی ارائه چیدمان و جانمایی با رعایت مقیاس ارائه شود.</li>
                                                        <li>پیش بینی فضای منبع آب ذخیره در مجتمع های آپارتمانی شده باشد.</li>
                                                    </ul>
                                                    <p class="HelpUL" runat="server" id="AllowedFileExt"></p>
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="4" width="100%">
                                                                    <table dir="rtl" width="100%" runat="server" id="TblTrFileType">
                                                                        <tr>
                                                                            <td valign="top" align="right" width="15%">
                                                                                <dxe:ASPxLabel runat="server" Text="نوع فایل پیوست" ID="ASPxLabel7">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td valign="top" align="right" width="35%">
                                                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                                    ID="cmbAttachType" DataSourceID="ObjdsAttachType" TextField="Title" ValueField="AttachTypeId" ValueType="System.String" RightToLeft="True">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <ValidationSettings Display="Dynamic" ValidationGroup="ValidAttach" ErrorTextPosition="bottom">
                                                                                        <RequiredField IsRequired="True" ErrorText="نوع فایل را انتخاب نمایید"></RequiredField>
                                                                                    </ValidationSettings>
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="Title" Caption="عنوان" Width="30%" />
                                                                                        <dxe:ListBoxColumn FieldName="AllowedFileExtensions" Caption="فرمت مجاز" Width="70%" />
                                                                                    </Columns>
                                                                                </TSPControls:CustomAspxComboBox>
                                                                            </td>
                                                                            <td valign="top" align="right" width="15%">
                                                                                <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel10">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td valign="top" align="right" width="35%">
                                                                                <table dir="rtl" runat="server" id="TblTrFileUplode">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td valign="top" align="right">
                                                                                                <TSPControls:CustomAspxUploadControl runat="server" Width="258px" ShowProgressPanel="True"
                                                                                                    MaxSizeForUploadFile="100000000" UploadWhenFileChoosed="True" ID="flpFile" InputType="Files"
                                                                                                    ClientInstanceName="flp" OnFileUploadComplete="flpFile_FileUploadComplete">
                                                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
  if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
	HDflp.Set('name',1);
	lbl1.SetVisible(false);
	hp.SetVisible(true);
	hp.SetNavigateUrl('../../Image/TechnicalServices/Plans/'+e.callbackData);
	}
	else{
	imgEndUploadImgClient.SetVisible(false);
	HDflp.Set('name',0);
	lbl1.SetVisible(true);
	hp.SetVisible(false);
	hp.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                                                                </TSPControls:CustomAspxUploadControl>
                                                                                                <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                                                    ID="ASPxLabel11" ForeColor="Red" ClientInstanceName="lbl1">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                                    ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                                                                </dxe:ASPxImage>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" ClientVisible="False" Target="_blank"
                                                                                    ID="ASPxHyperLink1" NavigateUrl='<%# Bind("FilePath") %>' ClientInstanceName="hp">
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="middle" align="center" colspan="4">
                                                                    <br />
                                                                    <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;اضافه به لیست"
                                                                        CausesValidation="true" ValidationGroup="ValidAttach" ID="btnSaveAttachment"
                                                                        UseSubmitBehavior="False" OnClick="btnSaveAttachment_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
       if( confirm('آیا مطمئن به افزودن این اطلاعات به لیست زیر هستید؟ در صورت افزودن اطلاعات قادر به حذف یا ویرایش نیستید'))
     {
	if(HDflp.Get('name')!=1)
	{
		lbl1.SetVisible(true);
		e.processOnServer=false;
	}
	else
	{
		HDflp.Set('name',0);
	}
 }
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxButton>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right" colspan="4">
                                                                    <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                                        ID="GridViewAttachment" KeyFieldName="AttachmentId" AutoGenerateColumns="False"
                                                                        ClientInstanceName="GridViewAttachment" OnPageIndexChanged="GridViewAttachment_PageIndexChanged"
                                                                        OnRowDeleting="GridViewAttachment_RowDeleting">
                                                                        <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                                                        <Columns>
                                                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AttachmentId"
                                                                                Caption="AttachmentId" Name="AttachmentId">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        
                                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="Title" Caption="نوع فایل">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="200px" FieldName="FileName"
                                                                                Caption="نام فایل">
                                                                            </dxwgv:GridViewDataTextColumn>                                                                         
                                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CreateDate" Caption="تاریخ ثبت">
                                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </dxwgv:GridViewDataTextColumn>

                                                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                                                FieldName="FilePath" Caption="لینک" Name="PlanFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                                                            </dxwgv:GridViewDataHyperLinkColumn>
                                                                        </Columns>
                                                                    </TSPControls:CustomAspxDevGridView2>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </fieldset>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>

                            <asp:Panel ID="RoundPanelDesigner" runat="server" Width="100%">

                                <fieldset>
                                    <legend class="HelpUL">طراحان نقشه</legend>
                                    <table dir="rtl" width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                        ID="GridViewDesigner" DataSourceID="ObjectDataSourceDesignerPlans" KeyFieldName="DesignerPlansId"
                                                        AutoGenerateColumns="False" ClientInstanceName="GridViewDesigner">
                                                        <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                                        <Columns>
                                                            <%-- <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MeType" Caption="نوع طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>--%>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="OfficeEngOId" Caption="کد دفتر/شرکت طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="DesignerMeId" Caption="کد عضویت طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="DesignerName" Caption="نام و نام خانوادگی"
                                                                Width="300px">
                                                                <CellStyle Wrap="False">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Wage" Caption="متراژ دستمزد">
                                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="YearName" Caption="تعرفه">
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Year" Caption="سال کاری">
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="DesignerCreateDate" Caption="تاریخ ثبت طراح">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Date" Caption="تاریخ ثبت نقشه">
                                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                    </TSPControls:CustomAspxDevGridView2>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceDesignerPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                                                        SelectMethod="SelectTSDesignerPlansForByPlanId" OldValuesParameterFormatString="original_{0}">
                                                        <SelectParameters>
                                                            <asp:SessionParameter SessionField="PlansId" Type="Int32" DefaultValue="-2" Name="PlansId"></asp:SessionParameter>
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </fieldset>
                            </asp:Panel>


                            <asp:Panel ID="RoundPanelControler" runat="server" Width="100%">
                                <fieldset>
                                    <legend class="HelpUL">بازبین نقشه</legend>
                                    <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                        ID="GridViewControler" KeyFieldName="PlansControlerId" AutoGenerateColumns="False"
                                        ClientInstanceName="GridViewControler">
                                        <Settings ShowHorizontalScrollBar="true"></Settings>
                                        <SettingsCookies Enabled="false" />
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FullName" Width="250px"
                                                Caption="نام و نام خانوادگی">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="InActiveName" Width="200px"
                                                Caption="وضعیت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="PlansControlerId"
                                                Caption="PlansControlerId" Name="PlansControlerId">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>
                                    <br />
                                    <TSPControls:CustomAspxDevGridView2 Caption="نظرات بازبینی نقشه" runat="server" Width="100%"
                                        ID="GridViewViewPoint" DataSourceID="ObjectDataSourcePlansControlerViewPoint"
                                        KeyFieldName="ViewPointId" AutoGenerateColumns="False" ClientInstanceName="GridViewDesigner">
                                        <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RowNo" Width="30px" Caption="ردیف">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Subject" Caption="موضوع">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="SheetNo" Caption="شماره برگ نقشه">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                FieldName="FileUrl" Caption="فایل پیوست" Name="ControlerFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                            </dxwgv:GridViewDataHyperLinkColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ViewPoint" Width="300px"
                                                Caption="توضیحات بازبینی">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ControllerName" Caption="بازبین"
                                                Name="ControllerName" Width="150px">
                                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>

                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id" Caption="Id">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>
                                    <asp:ObjectDataSource ID="ObjectDataSourcePlansControlerViewPoint" runat="server"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="FindByPlansId" TypeName="TSP.DataManager.TechnicalServices.PlansControlerViewPointManager">
                                        <SelectParameters>
                                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="PlansId"></asp:Parameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </fieldset>
                            </asp:Panel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
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
                                                Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار نقشه"
                                                ID="btnSendToNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="طراح جدید"
                                                ID="btnDesigners2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                OnClick="btnDesigners_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/Designers.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>
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

                <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewPlans" SessionName="SendBackDataTable_EmpAddPln"
                    OnCallback="CallbackPanelWorkFlow_Callback" />
                <dxhf:ASPxHiddenField ID="HD_Flp" runat="server" ClientInstanceName="HDflp">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HiddenFieldPrjDes" runat="server">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsAttachType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.AttachTypeManager"></asp:ObjectDataSource>

            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
