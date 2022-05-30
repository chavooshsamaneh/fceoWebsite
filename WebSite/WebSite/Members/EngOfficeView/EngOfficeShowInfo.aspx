<%@ Page Title="مشخصات دفتر" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeShowInfo.aspx.cs" Inherits="Members_EngOfficeView_EngOfficeShowInfo" %>

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
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

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
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
        OnItemClick="ASPxMenu1_ItemClick"
        AutoSeparators="RootOnly" RightToLeft="True">

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
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" ClientInstanceName="RoundPanel1"
        HeaderText="" runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="شماره  مشارکت نامه" ID="ASPxLabel1">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" Width="100%"
                            ID="txtLetterNo">
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="تاریخ مشارکت نامه" ID="ASPxLabel4">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">

                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                            ID="txtLetterDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="نوع دفتر" ID="ASPxLabel2">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomAspxComboBox runat="server" TextField="Name"
                            ID="ComboEOfTId" DataSourceID="OdbEOffType" ValueType="System.String"
                            ValueField="EOfTId" RightToLeft="True"
                            Width="100%">
                            <ItemStyle HorizontalAlign="Right" />
                            <ButtonStyle Width="13px">
                            </ButtonStyle>
                        </TSPControls:CustomAspxComboBox>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="شماره دفتر اسناد رسمی" ID="ASPxLabel7">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="20" ID="txtDaftarNo">
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="محل دفتر اسناد رسمی" ID="ASPxLabel9">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-9">
                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ID="txtDaftarLoc">
                        </TSPControls:CustomASPXMemo>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="تلفن" ID="ASPxLabel10">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtTel">
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="فکس" ID="ASPxLabel11">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFax">
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel3">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ID="txtAddress">
                        </TSPControls:CustomASPXMemo>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ID="txtDesc">
                        </TSPControls:CustomASPXMemo>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" Enabled="False" ID="lblFileNo">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="False" ID="txtFileNo"
                            Style="direction: ltr">
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="شماره نامه" ID="ASPxLabeldno" Visible="False">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" Width="100%" ClientEnabled="False"
                            ID="txtFileLetterNo" Visible="False">
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="تاریخ نامه" ID="ASPxLabelddate" ClientInstanceName="ASPxLabel8"
                            Visible="False">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Enabled="False"
                            Visible="False" ShowPickerOnTop="True" ID="txtFileLetterDate" PickerDirection="ToRight"
                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabeld1" Visible="False">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">

                        <TSPControls:CustomAspxComboBox runat="server" Visible="False" Width="100%" Enabled="False"
                            ID="cmbdIsTemporary" RightToLeft="True" ValueType="System.String">
                            <ItemStyle HorizontalAlign="Right" />
                            <Items>
                                <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                            </Items>
                            <ButtonStyle Width="13px">
                            </ButtonStyle>
                        </TSPControls:CustomAspxComboBox>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="شماره سریال" ID="ASPxLabeld2" Visible="False">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="False" ID="txtdSerialNo"
                            Visible="False">
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="تاریخ آخرین تمدید" ID="ASPxLabeld3" ClientInstanceName="lblDate"
                            Visible="False">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Enabled="False"
                            Visible="False" ShowPickerOnTop="True" ID="txtdLastRegDate" PickerDirection="ToRight"
                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" ID="ASPxLabeld4" ClientInstanceName="lblDate"
                            Visible="False">
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Enabled="False"
                            Visible="False" ShowPickerOnTop="True" ID="txtdExpDate" PickerDirection="ToRight"
                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="EngFileId" runat="server" Visible="False"></asp:HiddenField>
    <asp:ObjectDataSource ID="OdbEOffType" runat="server" TypeName="TSP.DataManager.EngOfficeTypeManager"
        SelectMethod="GetData" FilterExpression="EOfTId={0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td align="right">
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
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
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

</asp:Content>
