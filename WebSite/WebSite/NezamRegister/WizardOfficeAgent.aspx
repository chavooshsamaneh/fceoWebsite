<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOfficeAgent.aspx.cs" Inherits="NezamRegister_WizardOfficeAgent"
    Title="عضویت حقوقی - شعبه ها" %>

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

        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'OagName;Responsible;Tel_;Fax_;Tel_Pre;Fax_Pre;Website;Email;Address', SetValue);
        }
        function SetValue(values) {
            Name.SetText(values[0]);
            Manager.SetText(values[1]);
            Tel.SetText(values[2]);
            Fax.SetText(values[3]);
            TelPre.SetText(values[4]);
            FaxPre.SetText(values[5]);
            Website.SetText(values[6]);
            Email.SetText(values[7]);
            Address.SetText(values[8]);

        }
        function SetEmpty() {
            Name.SetText("");
            Manager.SetText("");
            Tel.SetText("");
            Fax.SetText("");
            TelPre.SetText("");
            FaxPre.SetText("");
            Website.SetText("");
            Email.SetText("");
            Address.SetText("");

            btnAgent.SetEnabled(true);
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
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
                    <dxm:MenuItem Text="شعبه ها" Name="Agent" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="اعضای شرکت" Name="Member">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="آگهی های رسمی" Name="Letter">
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
                    href="#"><span style="color: #000000">بس</span>تن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="شعبه ها" runat="server">


                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="شعبه ها">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 28px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
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
                            <div class="col-md-3">نام شعبه *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server"  MaxLength="80" ID="txtOfAgName"
                                    ClientInstanceName="Name">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="نام شعبه را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">مدیر شعبه *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" MaxLength="60" ID="txtOfAgResponsible"
                                    ClientInstanceName="Manager">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired="True" ErrorText="نام مدیر را وارد نمایید"></RequiredField>

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تلفن *</div>
                            <div class="col-md-3">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td  style="vertical-align: top; text-align: right" width="80%">
                                                <TSPControls:CustomTextBox runat="server" MaxLength="8" ID="txtOfAgTel"
                                                    ClientInstanceName="Tel">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top"  width="5%">
                                                <asp:Label runat="server" Text="-" Width="13px" ID="Label71"></asp:Label>
                                            </td>
                                            <td  width="15%">
                                                <TSPControls:CustomTextBox runat="server" MaxLength="4" ID="txtOfAgTel_pre"
                                                    ClientInstanceName="TelPre">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-3">فکس</div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td  width="80%">
                                                <TSPControls:CustomTextBox runat="server"   MaxLength="8" ID="txtOfAgFax"
                                                    ClientInstanceName="Fax">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top"  width="5%">
                                                <asp:Label runat="server" Text="-" Width="13px" ID="Label74"></asp:Label>
                                            </td>
                                            <td  width="15%">
                                                <TSPControls:CustomTextBox runat="server" MaxLength="4" ID="txtOfAgFax_pre"
                                                    ClientInstanceName="FaxPre">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">آدرس وب سایت</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" MaxLength="60" ID="txtOfAgWebsite"
                                    ClientInstanceName="Website">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">آدرس پست الکترونیکی</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" MaxLength="60" ID="txtOfAgEmail1"
                                    ClientInstanceName="Email">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RegularExpression ErrorText="آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                    
                        <div class="row">
                            <div class="col-md-3">آدرس</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo runat="server" Height="40px" Width="100%" ID="txtOfAgAddress"
                                    ClientInstanceName="Address">
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>    <br />
                        <div class="Item-center">
                            <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;اضافه به لیست"
                                ID="btnAdd" UseSubmitBehavior="False" Wrap="False" ClientInstanceName="btnAgent"
                                OnClick="btnAdd_Click">
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
                        <TSPControls:CustomAspxDevGridView2 ID="CustomAspxDevGridView1" runat="server" Width="100%"
                            AutoGenerateColumns="False"
                            KeyFieldName="OagId" ClientInstanceName="grid" OnRowDeleting="CustomAspxDevGridView1_RowDeleting"
                            EnableCallBacks="False">
                            <ClientSideEvents RowClick="function(s, e) {
	//SetControlValues();
	//btnAgent.SetEnabled(false);
}"
                                SelectionChanged="function(s, e) {
	SetControlValues();
	btnAgent.SetEnabled(false);
}"></ClientSideEvents>
                            <Columns>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true" ShowSelectButton="true" Width="25px">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="25px">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="OagId" Name="OagId" Visible="False"
                                    VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="نام شعبه" FieldName="OagName" Name="OagName"
                                    VisibleIndex="0" Width="200px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="مدیر مسئول" FieldName="Responsible" Name="Responsible"
                                    VisibleIndex="1" Width="150px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تلفن" FieldName="Tel" Name="Tel" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="فکس" FieldName="Fax" Name="Fax" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Tel_Pre" Name="Tel_Pre" Visible="False"
                                    VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Fax_Pre" Name="Fax_Pre" Visible="False"
                                    VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Website" Name="Website" Visible="False"
                                    VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="پست الکترونیکی" FieldName="Email" Name="Email"
                                    VisibleIndex="4" Visible="False">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Address" Name="Address" Visible="False"
                                    VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Tel_" Name="Tel_" Visible="False" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Fax_" Name="Fax_" Visible="False" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView2>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <div class="Item-center">
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" UseSubmitBehavior="False"
                    CausesValidation="False" EnableTheming="False" EnableViewState="False" ToolTip="بازگشت">
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" UseSubmitBehavior="False"
                    CausesValidation="False" EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه">
                </TSPControls:CustomAspxButton>
            </div>
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
