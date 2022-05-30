<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOfficeLetters.aspx.cs" Inherits="NezamRegister_WizardOfficeLetters"
    Title="عضویت حقوقی - آگهی های رسمی درج شده در روزنامه رسمی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript">

        function SetEmpty() {
            TextLeNo.SetText("");
            TextPageNo.SetText("");
            TextDesc.SetText("");
            document.getElementById('<%=txtDate.ClientID%>').value = "";

        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="شعبه ها" Name="Agent">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="اعضای شرکت" Name="Member">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="آگهی های رسمی" Name="Letter" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="خلاصه اطلاعات" Name="Summary">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ثبت نهایی" Name="End">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="آگهی های رسمی" runat="server">
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="آگهی های رسمی">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 28px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px">
                                    </Image>
                                    <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <PanelCollection>
                    <dxp:PanelContent>

                        <div class="row">
                            <p style="color: Red; text-align: justify">
                                * تنها آگهی های درج شده در روزنامه رسمی جمهوری اسلامی مدنظر هستند و باید شماره روزنامه
                                                                    و شماره صفحه روزنامه رسمی درج شود، بنابراین آگهی های درج شده در روزنامه های کثیرالانتشار
                                                                    قابل پذیرش <span style="text-decoration: underline">نمی باشند</span>
                            </p>
                            <p style="color: Red">
                                * حداقل آگهی رسمی تاسیس شرکت باید گذاشته شود
                            </p>
                            <p style="color: Red">
                                * تاریخ آخرین روزنامه رسمی باید سال جاری باشد
                            </p>
                        </div>
                        <div class="row">
                            <div class="col-md-3">شماره روزنامه *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtLeNo" runat="server" ClientInstanceName="TextLeNo"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="a">

                                        <RequiredField ErrorText="شماره روزنامه را وارد نمایید" IsRequired="True" />
                                        <RegularExpression ErrorText="شماره روزنامه صحیح نیست" ValidationExpression="\d{1,8}" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">شماره صفحه *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtLePageNo" runat="server" ClientInstanceName="TextPageNo"
                                    Width="100%">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="a">
                                        <RequiredField ErrorText="شماره صفحه را وارد نمایید" IsRequired="True" />
                                        <RegularExpression ErrorText="شماره صفحه صحیح نیست" ValidationExpression="\d{1,3}" />
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-3">تاریخ</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox ID="txtDate" ShowPickerOnEvent="OnClick" runat="server" DefaultDate=""
                                    IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True"
                                    Width="245px"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">توضیحات(حداکثر255کاراکتر)</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo ID="txtLeDesc" runat="server" ClientInstanceName="TextDesc"
                                    Height="45px" Width="100%">
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>

                        <br />
                        <div class="Item-center">
                            <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnSave" runat="server"
                                OnClick="btnSave_Click" Text="&nbsp;&nbsp;اضافه به لیست" ValidationGroup="a"
                                UseSubmitBehavior="False" ClientInstanceName="btnle">
                                <ClientSideEvents Click="function(s, e) {

}" />
                            </TSPControls:CustomAspxButton>

                            <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
                                CausesValidation="False" AutoPostBack="False" ID="btnRefresh" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
}"></ClientSideEvents>
                            </TSPControls:CustomAspxButton>
                        </div>
                        <br />
                        <TSPControls:CustomAspxDevGridView2 ID="CustomAspxDevGridView1" ClientInstanceName="grid"
                            runat="server" Width="100%"
                            AutoGenerateColumns="False" KeyFieldName="OlId" OnRowDeleting="CustomAspxDevGridView1_RowDeleting"
                            EnableCallBacks="False">
                            <Settings ShowHorizontalScrollBar="true" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="40px">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="OlId" Name="OlId" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره روزنامه" FieldName="LeNo" Name="LeNo"
                                    VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره صفحه" FieldName="LePageNo" Name="LePageNo"
                                    VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="LeDate" Name="LeDate" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="LeDesc" Name="LeDesc"
                                    VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView2>
                        <div class="Item-center">
                            <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server"  Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" CausesValidation="False"
                                UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="بازگشت">
                            </TSPControls:CustomAspxButton>
                            <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" CausesValidation="False"
                                UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه">
                                <ClientSideEvents Click="function(s, e) {
	if(grid.GetVisibleRowsOnPage()==0)
	{
	   alert(&quot;ثبت حداقل یک آگهی رسمی الزامی می باشد&quot;)
	   e.processOnServer=false;
	}
}" />
                            </TSPControls:CustomAspxButton>
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img align="middle" src="../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
