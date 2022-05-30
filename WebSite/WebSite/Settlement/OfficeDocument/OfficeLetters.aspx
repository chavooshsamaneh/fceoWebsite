<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeLetters.aspx.cs" Inherits="Settlement_OfficeDocument_OfficeLetters"
    Title="روزنامه های رسمی" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                    ToolTip="بازگشت" UseSubmitBehavior="False">

                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
         OnItemClick="ASPxMenu1_ItemClick">
        <Items>
            <dxm:MenuItem Name="Office" Text="مشخصات شرکت">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Member" Text="اعضای شرکت">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Agent" Text="شعبه ها">
            </dxm:MenuItem>
            <%--   <dxm:MenuItem Name="Job" Text="سوابق کاری">
            </dxm:MenuItem>--%>
            <dxm:MenuItem Name="Letters" Text="روزنامه های رسمی" Selected="true">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
            </dxm:MenuItem>
        </Items>

    </TSPControls:CustomAspxMenuHorizontal>

    <br />
    <div style="width: 100%; text-align: right">
        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
        </dxe:ASPxLabel>
        <dxe:ASPxLabel ID="lblOfName" runat="server">
        </dxe:ASPxLabel>
    </div>
    <br />
    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="OdbOfLetters"
        ClientInstanceName="grid"
        AutoGenerateColumns="False" KeyFieldName="OlId" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
        Width="100%" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
        OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">

        <Columns>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OlId" Caption="کد" Name="OlId"
                Width="30px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LetterNo" Caption="شماره روزنامه"
                Name="LetterNo" Width="60px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PageNo" Caption="شماره صفحه"
                Name="PageNo" Width="60px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ" Name="Date"
                Width="80px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="InActiveName" Caption="وضعیت"
                Width="60px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات"
                Name="Description">
                <EditFormSettings Visible="True"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="50px" ShowClearFilterButton="true">
      
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OfReId" Visible="False" VisibleIndex="5">
            </dxwgv:GridViewDataTextColumn>
        </Columns>

    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
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
    <dxhf:ASPxHiddenField ID="LetterId" runat="server">
    </dxhf:ASPxHiddenField>
    <dxhf:ASPxHiddenField ID="LetterMode" runat="server">
    </dxhf:ASPxHiddenField>
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>

    <asp:ObjectDataSource ID="OdbOfLetters" runat="server" TypeName="TSP.DataManager.OfficialLetterManager"
        SelectMethod="FindByOffRequest" FilterExpression="OfId={0}" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>

        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="JustActive" Type="Int16" />
        </SelectParameters>

    </asp:ObjectDataSource>
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />

</asp:Content>
