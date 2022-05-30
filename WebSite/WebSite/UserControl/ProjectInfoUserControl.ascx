<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProjectInfoUserControl.ascx.cs"
    Inherits="UserControl_ProjectInfoUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="مشخصات پروژه"
    runat="server" Width="100%" AllowCollapsingByHeaderClick="true">
    <PanelCollection>
        <dxp:PanelContent>
            <table cellpadding="3" width="100%">
                <tbody>
                    <tr>
                        <td align="center" valign="top" colspan="4" dir="ltr">
                            <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                ForeColor="Red">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="15%">
                            <dxe:ASPxLabel runat="server" Text="کد پروژه:" ID="ASPxLabel14" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top" width="35%">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrProjectId" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top" width="15%">
                            <dxe:ASPxLabel runat="server" Text="نام پروژه:" ID="ASPxLabel16" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top" width="35%">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrProjectName" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شماره پرونده:" ID="ASPxLabel4" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" dir="ltr" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrFileNo" Font-Bold="true" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شماره پلاک ثبتی:" ID="ASPxLabel5" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" dir="ltr" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrRegisteredNo" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شماره پروانه ساخت:" ID="ASPxLabel8" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="ltr" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrLicenceNo" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="نماینده مالکین:" ID="ASPxLabel6" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrOwnerName" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="نوع اسکلت:" ID="ASPxLabel12" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrStructure" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="گروه ساختمانی:" ID="ASPxLabel7" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrGroup" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="زیربنا(مترمربع):" ID="ASPxLabel1" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtFoundation" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="بیشترین تعداد طبقات:" ID="ASPxLabel2" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtStage" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>

                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شهر:" ID="ASPxLabel9" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrCitName" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شهرداری:" ID="ASPxLabel11" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtPrMunName" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="کد بایگانی:" ID="ASPxLabel3" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtarchiveNo" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>

                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="نوع پروژه:" ID="ASPxLabel10" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtDiscountPercentTitle" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>

                    </tr>

                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شناسه دسترسی طراح:" ID="ASPxLabel15" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtTraceCode" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="افزایش زیر بنا نسب به آخرین درخواست:" ID="ASPxLabel13" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtFundationDifference" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>


                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="وضعیت پروژه:" ID="ASPxLabel19" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="lblProjectStatusName" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="نوع درخواست:" ID="ASPxLabel21" Width="100%" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td dir="rtl" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="lblPrjReTypeName" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>


                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="تعداد ناظران فعال:" ID="ASPxLabel17" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="" ID="txtCountProjectObserver" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel runat="server" Text="جمعیت شهر پروژه:" ID="ASPxLabel18" Width="100%"
                                Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel runat="server" Text="" ID="lblCityPopulation" Font-Bold="true" Width="100%"
                                RightToLeft="True" Wrap="False">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</TSPControls:CustomASPxRoundPanel>
