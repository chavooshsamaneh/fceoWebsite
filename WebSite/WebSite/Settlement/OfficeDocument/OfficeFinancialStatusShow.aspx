<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeFinancialStatusShow.aspx.cs" Inherits="Settlement_OfficeDocument_OfficeFinancialStatusShow"
    Title="مشخصات وضعیت مالی" %>

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

                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0">
                    <tbody>
                        <tr>
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
    <div style="width: 100%; text-align: right">
        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
        </dxe:ASPxLabel>
        <dxe:ASPxLabel ID="lblOfName" runat="server">
        </dxe:ASPxLabel>
    </div>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table width="100%">
                    <tr>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نوع وضعیت مالی" Width="130px">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" colspan="3" dir="ltr" valign="top">
                            <TSPControls:CustomAspxComboBox ID="CmbName" runat="server"
                                DataSourceID="ObjectDataSource1"
                                TextField="Name" ValueField="OfdId" ValueType="System.String" Width="300px" ReadOnly="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RequiredField ErrorText="نوع وضعیت مالی را انتخاب نمایید" IsRequired="True" />
                                </ValidationSettings>
                                <Columns>
                                    <dxe:ListBoxColumn Caption="نام" FieldName="Name" Width="290px" />
                                    <dxe:ListBoxColumn Caption="ضریب" FieldName="Value" />
                                </Columns>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="مبلغ(ریال)">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" colspan="3" valign="top">
                            <TSPControls:CustomTextBox ID="txtValue" runat="server"
                                Width="170px" ReadOnly="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                    <RequiredField ErrorText="مبلغ را وارد نمایید" IsRequired="True" />
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="توضیحات" Width="87px">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" colspan="3" valign="top">
                            <TSPControls:CustomASPXMemo ID="txtDesc" runat="server"
                                Height="35px" Width="550px" ReadOnly="True">
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomASPXMemo>
                        </td>
                    </tr>
                </table>
                <fieldset>
                    <legend class="HelpUL">مدارک پیوست</legend>
                    <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                        Width="379px" ID="AspxGridFlp" KeyFieldName="AttachId" AutoGenerateColumns="False">

                        <Columns>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل"
                                Name="FilePath">
                                <DataItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" Target="_blank" NavigateUrl='<%# Bind("FilePath") %>'></asp:HyperLink>
                                </DataItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                </EditItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                                Name="Description">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
                </fieldset>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBack_Click">
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
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="FinancialId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager" FilterExpression="Type={0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>

</asp:Content>
