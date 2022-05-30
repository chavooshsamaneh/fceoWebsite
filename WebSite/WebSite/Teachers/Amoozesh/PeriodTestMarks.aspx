<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodTestMarks.aspx.cs" Inherits="Teachers_Amoozesh_PeriodTestMarks"
    Title="ثبت نمرات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>


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
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'PRId;MeObjectionText;MeObjectionDate;TeObjectionText;TeObjectionDate;LastMark;FirstMark', SetValue);
        }
        function SetValue(values) {
            PRId.SetText(values[0]);
            MeDate.SetText(values[2]);
            MeText.SetText(values[1]);

            if (values[4] != null) {
                lbl.SetVisible(true);
                ////txtDate.SetVisible(true);
                lblMark.SetVisible(true);
                FirstMark.SetVisible(true);

                btn.SetVisible(false);
                /////txtDate.SetText(values[4]);
                TeAnswer.SetText(values[3]);
                TotalMark.SetText(values[5]);
                FirstMark.SetText(values[6]);

            }
            else {
                lbl.SetVisible(false);
                ////txtDate.SetVisible(false);
                btn.SetVisible(true);
            }
        }
        function HasError() {
            if (TotalMark.GetIsValid() && TeAnswer.GetIsValid())
                return false;
            return true;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave1" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/save.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ButtonRet1" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="ButtonRet_Click" Text=" "
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

            <ul class="HelpUL">
                <li>در صورتی که پس از ثبت نمرات قصد تغییر آنها را دارید از دکمه ذخیره استفاده نمایید.
                </li>
                <li>ثبت نهایی به معنای اعلام پایان اعتراضات می باشد و ذخیره نمرات را در بر ندارد.
                </li>
                <li>در صورتی که شرکت کنندگان دوره امکان اعتراض به نمرات را نداشته و همچنین از صحت کلیه
                            نمرات وارد شده مطمئن می باشید برروی دکمه ثبت نهایی کلیک نمایید. </li>
                <li>در صورت ثبت نهایی نمرات امکان اعمال هیچ گونه تغییر در نمرات وجود ندارد. </li>
            </ul>

            <fieldset>
                <legend class="HelpUL" dir="rtl"><b>اطلاعات دوره</b></legend>
                <table width="100%">
                    <tr>
                        <td valign="top" align="right" style="width: 15%">عنوان دوره :
                        </td>
                        <td valign="top" align="right" colspan="3" style="width: 35%">
                            <dxe:ASPxLabel ID="lblPPTitle" Width="100%" runat="server" Font-Bold="true">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" style="width: 15%">وضعیت دوره:
                        </td>
                        <td valign="top" align="right" style="width: 35%">
                            <dxe:ASPxLabel ID="lblPPStatus" Font-Bold="true"
                                runat="server">
                            </dxe:ASPxLabel>
                            <td valign="top" align="right" style="width: 15%">کد دوره :
                            </td>
                            <td valign="top" align="right" style="width: 35%">
                                <dxe:ASPxLabel ID="lblPriodCode" runat="server" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" width="15%">طول دوره(ساعت):
                    <td valign="top" align="right" width="35%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblPDuration">
                        </dxe:ASPxLabel>
                    </td>
                            <td valign="top" align="right" width="15%">ظرفیت دوره:
                            </td>
                            <td valign="top" align="right" width="35%">
                                <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblCapacity">
                                </dxe:ASPxLabel>
                            </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" width="15%">تاریخ شروع:
                        </td>
                        <td valign="top" align="right" width="35%">
                            <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblStartDate">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right" width="15%">تاریخ پایان:
                        </td>
                        <td valign="top" align="right" width="35%">
                            <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblEndDate">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" style="width: 15%">نام مؤسسه :
                        </td>
                        <td align="right" valign="top" style="width: 35%">
                            <dxe:ASPxLabel ID="lblInsName" runat="server" Font-Bold="true">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top" style="width: 15%">مدیر مؤسسه :
                        </td>
                        <td align="right" valign="top" style="width: 35%">
                            <dxe:ASPxLabel ID="lblManager" runat="server" Font-Bold="true">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelGread" ClientInstanceName="RoundPanelGread"
                HeaderText="ثبت نمرات" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent runat="server">

                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right" style="width: 15%">تاریخ اعتراض*
                                    </td>
                                    <td valign="top" align="right" style="width: 35%">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="245px" ID="txtDate" Style="direction: ltr; text-align: right;"
                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" ErrorMessage="تاریخ را وارد نمایید"
                                            ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td valign="top" align="right">نمره کل*
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                            ID="txtTotalMark">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <RequiredField IsRequired="True" ErrorText="نمره کل را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <TSPControls:CustomAspxDevGridView runat="server" ClientInstanceName="grid" KeyFieldName="PRId"
                            AutoGenerateColumns="False" RightToLeft="True" DataSourceID="ObjectDataSourcePeriodRegister"
                            Width="100%" ID="GridViewTestMarks" OnHtmlRowPrepared="GridViewTestMarks_HtmlRowPrepared">
                            <Settings ShowHorizontalScrollBar="true" ShowVerticalScrollBar="false" />
                            <SettingsCookies Enabled="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Name="ObjAns" Width="80px" Caption=" "
                                    VisibleIndex="0">
                                    <DataItemTemplate>
                                        <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="جواب اعتراض" NavigateUrl="#">
                                            <ClientSideEvents Click="function(s, e) {
		SetControlValues();
	pop.Show();

}"></ClientSideEvents>
                                        </dxe:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="PRId" Visible="False" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="PtmId" Name="PtmId" Visible="False" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="InActiveName" Caption="وضعیت" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="MeId" Caption="کد عضویت" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="FirstName" Caption="نام" VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="LastName" Caption="نام خانوادگی" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="RgstType" Caption="نوع ثبت نام" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="TotalTimePresent" Name="TotalTimePresent" Caption="ساعات غیبت"
                                    VisibleIndex="2">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="FirstMark" Name="FirstMark" Caption="نمره"
                                    VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="LastMark" Name="LastMarkEdit" Caption="نمره نهایی*"
                                    VisibleIndex="4">
                                    <DataItemTemplate>
                                        <TSPControls:CustomTextBox ID="txtMark" runat="server" Width="70px" Text='<%# Bind("LastMark") %>'>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نمره را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Status" Name="StatusEdit" Caption="نتیجه پایانی*"
                                    VisibleIndex="4">
                                    <DataItemTemplate>
                                        <TSPControls:CustomAspxComboBox ID="CmbStatus" runat="server" Width="70px" SelectedIndex='<%# Bind("statusMark") %>'
                                            RightToLeft="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نتیجه پایانی را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="قبول"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="مردود"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="2" Text="غیبت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="3" Text="مردود به علت غیبت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="4" Text="تخلف"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="5" Text="قبول مشروط"></dxe:ListEditItem>
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Description" Name="DescriptionEdit" Caption="توضیحات"
                                    VisibleIndex="5" Width="200px">
                                    <DataItemTemplate>
                                        <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" Width="170px" Text='<%# Bind("Description") %>'
                                            Height="22px">
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="LastMark" Name="LastMark" Caption="نمره نهایی"
                                    Visible="False" VisibleIndex="7">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="statusName" Name="Status" Caption="نتیجه پایانی"
                                    Visible="False" VisibleIndex="7">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Caption="توضیحات"
                                    Visible="False" VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>

                                <dxwgv:GridViewDataTextColumn FieldName="MeObjectionText" Visible="False" VisibleIndex="9">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="MeObjectionDate" Visible="False" VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="TeObjectionText" Visible="False" VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="TeObjectionDate" Visible="False" VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="FirstMark" Visible="False" VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <asp:ObjectDataSource ID="ObjectDataSourcePeriodRegister" runat="server" TypeName="TSP.DataManager.PeriodRegisterManager"
                SelectMethod="SelectPeriodRegisterForTeachers" FilterExpression="PPId={0}" OldValuesParameterFormatString="original_{0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter"></asp:Parameter>
                </FilterParameters>
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="PRId" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="-1" Name="PPId" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="0" Name="IsSeminar" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="1" Name="IsConfirm" Type="Int32"></asp:Parameter>
                    <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="PeriodId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="InstitueId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDPageMode" runat="server" Visible="False"></asp:HiddenField>
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"
                HeaderText="اعتراض" ClientInstanceName="pop" PopupHorizontalAlign="Center"
                PopupElementID="btnSearch1" CloseAction="CloseButton"
                Modal="True" AllowDragging="True">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="تاریخ اعتراض" Width="78px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox ID="txtMeObjDate" Style="direction: ltr" runat="server"
                                            Width="100px" ReadOnly="True" ClientInstanceName="MeDate">
                                            <ValidationSettings>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                        <TSPControls:CustomTextBox ID="txtPRId" Style="direction: ltr" runat="server"
                                            Width="100px" ClientInstanceName="PRId" ClientVisible="False">
                                            <ValidationSettings>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="متن اعتراض">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomASPXMemo ID="txtMeObjText" runat="server"
                                            Height="43px" ReadOnly="True" Width="328px" ClientInstanceName="MeText">
                                            <ValidationSettings>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="نمره اولیه" Width="78px" ClientInstanceName="lblMark"
                                            ClientVisible="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox ID="ASPxTextBox2" runat="server"
                                            Width="100px" ClientInstanceName="FirstMark" ClientVisible="False">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="نمره نهایی را وارد نمایید" IsRequired="True" />
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
                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="نمره نهایی" Width="78px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox ID="txtLastMark" runat="server"
                                            Width="100px" ClientInstanceName="TotalMark">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="نمره نهایی را وارد نمایید" IsRequired="True" />
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
                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="lbl" Text="تاریخ جواب اعتراض"
                                            Width="94px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox ID="ASPxTextBox1" Style="direction: ltr" runat="server"
                                            Width="100px" ClientInstanceName="txtDate" ClientVisible="False">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="نمره نهایی را وارد نمایید" IsRequired="True" />
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
                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="جواب اعتراض">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomASPXMemo ID="txtTeObjText" runat="server"
                                            Height="43px" Width="328px" ClientInstanceName="TeAnswer">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="جواب اعنراض را وارد نمایید" IsRequired="True" />
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br />
                                        <TSPControls:CustomAspxButton ID="btnObjSave" runat="server"
                                            OnClick="btnObjSave_Click" Text="ذخیره" UseSubmitBehavior="False"
                                            ClientInstanceName="btn">
                                            <ClientSideEvents Click="function(s, e) {
	if(!HasError())
{
pop.Hide();

}
else
  e.processOnServer=false;
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px" />
                <CloseButtonImage Height="17px" Width="17px" />
            </TSPControls:CustomASPxPopupControl>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/save.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton3" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="ButtonRet_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/Back.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </td>
                                    </tr>
                                </tbody>
                            </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
