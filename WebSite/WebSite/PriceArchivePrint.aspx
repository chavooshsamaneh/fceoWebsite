<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriceArchivePrint.aspx.cs" Inherits="PriceArchivePrint" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>چاپ تعرفه</title>
     <script language="javascript" type="text/javascript">
    function PrintPage()
    {
    window.print();
    }
    </script>
</head>
<body dir="rtl" align="center" style="font-family: Tahoma, 'Microsoft Sans Serif';
    font-size: 9pt;" onload="PrintPage()">
    <form id="form1" runat="server">
           <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="false">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
                    <dxhf:ASPxHiddenField ID="hiddenSaveID" runat="server" ClientInstanceName="hiddenSaveID">
            </dxhf:ASPxHiddenField>
    <div>  

               <div align="right" style="width: 100%; font-family: Tahoma; font-size: 8pt">
                                <table width="100%" border="1px" bordercolor="#7EACB1">
                                    <tr>
                                        <td style="width: 11.11%;" colspan="2">
                                            <b>گروه ساختمانی</b>
                                        </td>
                                        <td style="width: 22.22%;" align="center" colspan="4">
                                            <b>الف</b>
                                        </td>
                                        <td style="width: 11.11%;" align="center" colspan="2">
                                            <b>ب</b>
                                        </td>
                                        <td style="width: 22.22%;" align="center" colspan="4">
                                            <b>ج</b>
                                        </td>
                                        <td style="width: 33.33%;" align="center" colspan="6">
                                            <b>د</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <b>تعداد طبقات</b>
                                        </td>
                                                  <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoA1From" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoA1To" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoAFrom" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoATo" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoBFrom" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>


                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoBTo" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>



                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoC1From" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoC1To" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoC2From" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoC2To" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoD1From" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoD1To" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>از
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoD2From" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تا
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoD2To" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtStepNoD3From" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                    <td>طبقه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="5">و بالاتر
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="5">از روی شالوده
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>


                                        <tr>
                                    <td style="font-size: 8pt" colspan="2">
                                            <b>نوع اسکلت</b>
                                        </td>
                                    <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                       
                                         <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                         <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                      <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                      <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                      <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                      <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                      <td colspan="2">
                                             <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="---------" ID="ComboBoxStructureSkeletonD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel></td>
                                     </tr>
                                    <tr>
                                        <td style="font-size: 8pt" colspan="2">
                                            <b>حداکثر زیربنا (متر)</b>
                                        </td>
                                        <td colspan="2">
                                          
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqB" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqC1" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqC2" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqD1" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqD2" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtMaxSqD3" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 8pt" colspan="2">
                                            <b>حداکثر تعداد واحد</b>
                                        </td>
                                             <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitA1" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitA" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 10%" align="center"></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 10%" align="center"></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 10%" align="center">&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 10%" align="center">&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 10%" align="center"></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCountUnitD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                    <td style="width: 10%" align="center">&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 8pt" colspan="2">
                                            <b>هزینه ساخت هر مترمربع بنا (ریال)</b>
                                        </td>
                                            <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostA1" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostA" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostB" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostC1" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostC2" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostD1" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostD2" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtCostD3" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                    </tr>
                                    <%--*******************************************************************************--%>
                                    <tr>
                                        <td rowspan="2">
                                           <spin  style="font-weight:bold"  Class="VerticalText">طراحی</spin>
                                        </td>
                                        <td style="font-size: 7.5pt">مجموع درصد حق الزحمه طراحی
                                        </td>
                                                    <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentA1" ClientInstanceName="lblDesignSumPercentA1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentA" ClientInstanceName="lblDesignSumPercentA"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentB" ClientInstanceName="lblDesignSumPercentB"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentC1" ClientInstanceName="lblDesignSumPercentC1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentC2" ClientInstanceName="lblDesignSumPercentC2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentD1" ClientInstanceName="lblDesignSumPercentD1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentD2" ClientInstanceName="lblDesignSumPercentD2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblDesignSumPercentD3" ClientInstanceName="lblDesignSumPercentD3"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignArchPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSazePercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignTasisatPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignShahrPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignMapPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>حق الزحمه طراحی (ریال)
                                        </td>
                                                 <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumArchD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumSazeD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumTasisatD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumShahrD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtDesignSumMapD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--*************************************************************************************--%>
                                    <tr>
                                        <td rowspan="2">
                                                               <spin  style="font-weight:bold"  Class="VerticalText">نظارت</spin>
                                        </td>
                                        <td style="font-size: 7.5pt">مجموع درصد حق الزحمه نظارت
                                        </td>
                                           <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentA1" ClientInstanceName="lblSupervisionSumPercentA1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentA" ClientInstanceName="lblSupervisionSumPercentA"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentB" ClientInstanceName="lblSupervisionSumPercentB"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentC1" ClientInstanceName="lblSupervisionSumPercentC1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentC2" ClientInstanceName="lblSupervisionSumPercentC2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentD1" ClientInstanceName="lblSupervisionSumPercentD1"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentD2" ClientInstanceName="lblSupervisionSumPercentD2"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; width: 30px">جمع<br />
                                            <dxe:ASPxLabel ID="lblSupervisionSumPercentD3" ClientInstanceName="lblSupervisionSumPercentD3"
                                                runat="server" Text="0" Font-Size="6.5pt">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="center" style="vertical-align: top; font-size: 7.5pt; padding: 0px 0px 0px 0px">
                                            <table style="padding: 0px 0px 0px 0px">
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionArchPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSazePercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionTasisatPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionShahrPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 7pt">نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionMapPercentD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>حق الزحمه نظارت (ریال)
                                        </td>
                                              <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                    <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapA1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                              <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapA" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                                 <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapB" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                                 <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapC1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                                 <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapC2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                                 <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapD1" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                                 <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapD2" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; font-size: 7pt" align="center" colspan="2">
                                            <table width="100%">
                                                                 <tr>
                                                    <td>ناظر هماهنگ کننده
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumCordD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>معماری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumArchD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>سازه
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumSazeD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تاسیسات
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumTasisatD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>شهرسازی
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumShahrD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نقشه برداری
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel Font-Size="8pt" runat="server" Text="0" ID="txtSupervisionSumMapD3" Font-Bold="true" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">مجموع درصد حق الزحمه های طراحی و نظارت
                                        </td>
                                          <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentA1" ClientInstanceName="lblSumAllPercentA1" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentA" ClientInstanceName="lblSumAllPercentA" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentB" ClientInstanceName="lblSumAllPercentB" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentC1" ClientInstanceName="lblSumAllPercentC1" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentC2" ClientInstanceName="lblSumAllPercentC2" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentD1" ClientInstanceName="lblSumAllPercentD1" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentD2" ClientInstanceName="lblSumAllPercentD2" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="2" align="center">
                                            <dxe:ASPxLabel Font-Size="8pt" ID="lblSumAllPercentD3" ClientInstanceName="lblSumAllPercentD3" runat="server"
                                                Text="0">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
    </div>
    </form>
</body>
</html>
