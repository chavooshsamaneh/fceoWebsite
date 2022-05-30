<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EPaymentUserControl.ascx.cs"
    Inherits="UserControl_EPaymentUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<TSPControls:CustomASPxRoundPanel ID="RoundPanelPeriodRegister" HeaderText="فاکتور پرداخت"
    runat="server" Width="100%">
    <PanelCollection>
        <dxp:PanelContent>
            <div class="row">
                <dxe:ASPxLabel runat="server" Text="" ID="lblError" ForeColor="Red"
                    Font-Bold="true" Width="100%" Visible="false" Wrap="True">
                </dxe:ASPxLabel>
            </div>
     <%--       <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-3"></div>
                <div class="col-md-3"></div>
                <div class="col-md-3"></div>
            </div>--%>

            <dxp:ASPxPanel ID="PanelPaymentInfo" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td colspan="10" align="center" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="" ID="lblEPaymentMessage" ForeColor="Green" Font-Bold="true"
                                        Width="100%" Wrap="True">
                                    </dxe:ASPxLabel>
                                    <br />
                                </td>
                                <%--<td rowspan="5" valign="middle" align="center" width="10%">
                                    <asp:Image ID="imgContact" runat="server" ImageUrl="~/Images/Icons/EPaymentLogo.png"
                                        Height="122px" Width="130px" />
                                </td>--%>
                            </tr>
                            <tr>
                                <td align="center" valign="top" colspan="4">
                                    <dxe:ASPxLabel runat="server" Text="کدرهگیری بانکی:  - - - - - -" ID="lblPaymentRefrenceId"
                                        Font-Bold="true" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="20%">
                                    <dxe:ASPxLabel runat="server" Text="مبلغ پرداخت شده:" ID="ASPxLabel4" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="20%">
                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="lblPaymentAmount" Font-Bold="true"
                                        Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="20%">
                                    <dxe:ASPxLabel runat="server" Text="بابت:" ID="ASPxLabel2" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="20%">
                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="lblPaymentType" Font-Bold="true" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="وضعیت پرداخت:" ID="ASPxLabel3" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="lblPaymentStatus" Font-Bold="true"
                                        Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="نام پرداخت کننده:" ID="ASPxLabel7" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="lblFishPayerName" Font-Bold="true"
                                        Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="تاریخ پرداخت:" ID="ASPxLabel6" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="lblPaymentDate" Font-Bold="true" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="ساعت پرداخت:" ID="ASPxLabel8" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" colspan="3">
                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="lblPaymentTime" Font-Bold="true" Width="100%">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxp:ASPxPanel>

            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                ID="GridViewAccounting" KeyFieldName="AccountingId" AutoGenerateColumns="False"
                ClientInstanceName="grid">
                <%--OnRowDeleting="GridViewAccounting_RowDeleting"--%>
                <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                </TotalSummary>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AccType"
                        Caption="بابت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت"
                        Width="350px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Description" Name="Description"
                        Visible="false" Caption="توضیحات" Width="50px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Amount" Caption="مبلغ (ريال)"
                        Width="200px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                        <PropertiesTextEdit DisplayFormatString="#,#">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ" Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView2>
            <div class="row">
                <a id="BankHelp1" runat="server" class="HelpUL">
                    <p>
                        پرداخت به صورت الکترونیکی با استفاده از کارت های اعتباری عضو شتاب انجام می شود
                    </p>
                </a><a id="BankHelp2" runat="server" class="HelpUL">
                    <p >
                        <b>توجه! بعد از انجام پرداخت الکترونیکی در صفحه بانک حتما گزینه <u>تکمیل فرایند خرید</u>
                            را انتخاب نمایید تا دوباره به سایت سازمان بازگشته در غیر اینصورت فرایند درخواست
                                                    کامل نشده ودرخواست مربوطه(صدور پروانه/ثبت نام دوره آموزشی/ثبت نام سمینار و...) شما با مشکل مواجه می شود</b>
                    </p>
                </a>
            </div>
        </dxp:PanelContent>
    </PanelCollection>
</TSPControls:CustomASPxRoundPanel>

