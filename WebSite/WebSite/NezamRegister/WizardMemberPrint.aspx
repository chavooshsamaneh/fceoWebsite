<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WizardMemberPrint.aspx.cs"
    Inherits="NezamRegister_WizardMemberPrint" Title="عضویت حقیقی - چاپ اطلاعات کاربری" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<html>
<head>
    <title>عضویت حقیقی - چاپ اطلاعات کاربری</title>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.css" type="text/css" />

</head>
<body>
    <form runat="server">
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="خلاصه اطلاعات" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">مشخصات عضو</legend>

                            <table runat="server" id="TABLE2" style="font-size: 9pt; font-family: Tahoma" dir="rtl"
                                width="700" __designer:dtid="2533274790397446">
                                <tr runat="server" id="Tr2" __designer:dtid="2533274790397450">
                                    <td runat="server" id="Td2" valign="top" align="right" __designer:dtid="2533274790397451">
                                        <asp:Label runat="server" Text="عنوان" ID="Label63" Visible="False" __designer:dtid="2533274790397453"
                                            __designer:wfdid="w409"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td3" valign="top" align="right" colspan="5" __designer:dtid="2533274790397454">
                                        <dxe:ASPxLabel runat="server" Text="- - -" ID="AStitle" Visible="False" __designer:dtid="2533274790397456"
                                            __designer:wfdid="w410">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td1" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="تصویر" ID="Label95" __designer:dtid="2533274790397599"
                                            __designer:wfdid="w411"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td4" valign="top" align="right" colspan="1">
                                        <asp:Image runat="server" Height="75px" Width="75px" ID="Image" __designer:dtid="2533274790397601"
                                            __designer:wfdid="w412"></asp:Image>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr3" __designer:dtid="2533274790397457">
                                    <td runat="server" id="Td5" valign="top" align="right" __designer:dtid="2533274790397458">
                                        <asp:Label runat="server" Text="نام" ID="Label64" __designer:dtid="2533274790397459"
                                            __designer:wfdid="w413"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td6" valign="top" align="right" colspan="5" __designer:dtid="2533274790397460">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASfirstname" __designer:dtid="2533274790397461"
                                        __designer:wfdid="w414">
                                    </dxe:ASPxLabel>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                    </td>
                                    <td runat="server" id="Td7" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text=" (انگلیسی)" Width="62px" ID="Label65" __designer:dtid="2533274790397469"
                                            __designer:wfdid="w415"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td8" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASfirstnameen" __designer:dtid="2533274790397471"
                                        __designer:wfdid="w416">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr4" __designer:dtid="2533274790397462">
                                    <td runat="server" id="Td9" valign="top" align="right" __designer:dtid="2533274790397463">
                                        <asp:Label runat="server" Text="نام خانوادگی" ID="Label66" __designer:dtid="2533274790397464"
                                            __designer:wfdid="w417"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td10" valign="top" align="right" colspan="5" __designer:dtid="2533274790397465">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASlastname" __designer:dtid="2533274790397466"
                                        __designer:wfdid="w418">
                                    </dxe:ASPxLabel>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                    </td>
                                    <td runat="server" id="Td11" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text=" (انگلیسی)" Width="62px" ID="Label1" __designer:dtid="2533274790397469"
                                            __designer:wfdid="w419"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td12" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASlastnameen" __designer:dtid="2533274790397476"
                                        __designer:wfdid="w420">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr1">
                                    <td runat="server" id="Td134" valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره شناسنامه" Width="119px" ID="Label71" __designer:dtid="2533274790397494"
                                            __designer:wfdid="w421"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td135" valign="top" align="right" colspan="5">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASIdno" __designer:dtid="2533274790397496"
                                        __designer:wfdid="w422">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td136" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="شماره ملی" ID="Label72" __designer:dtid="2533274790397499"
                                            __designer:wfdid="w423"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td137" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASssn" __designer:dtid="2533274790397501"
                                        __designer:wfdid="w424">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr8" __designer:dtid="2533274790397482">
                                    <td runat="server" id="Td14" valign="top" align="right" __designer:dtid="2533274790397483">
                                        <asp:Label runat="server" Text="تاریخ تولد" ID="Label69" __designer:dtid="2533274790397484"
                                            __designer:wfdid="w425"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td15" valign="top" align="right" colspan="5" __designer:dtid="2533274790397485">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASbirthdate" __designer:dtid="2533274790397486"
                                        __designer:wfdid="w426">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                    <td runat="server" id="Td76" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="محل تولد" ID="Label70" __designer:dtid="2533274790397489"
                                            __designer:wfdid="w427"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td77" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASbirthplace" __designer:dtid="2533274790397491"
                                        __designer:wfdid="w428">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr9" __designer:dtid="2533274790397487">
                                    <td runat="server" id="Td16" valign="top" align="right" __designer:dtid="2533274790397488">
                                        <asp:Label runat="server" Text="نام پدر" ID="Label68" __designer:dtid="2533274790397479"
                                            __designer:wfdid="w429"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td17" valign="top" align="right" colspan="5" __designer:dtid="2533274790397490">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASPfathername" __designer:dtid="2533274790397481"
                                        __designer:wfdid="w430">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td78" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="محل صدور" ID="Label73" __designer:dtid="2533274790397504"
                                            __designer:wfdid="w431"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td79" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASissueplace" __designer:dtid="2533274790397506"
                                        __designer:wfdid="w432">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr10" __designer:dtid="2533274790397492">
                                    <td runat="server" id="Td18" valign="top" align="right" __designer:dtid="2533274790397493">
                                        <asp:Label runat="server" Text="آدرس محل سکونت" Width="115px" ID="Label74" __designer:dtid="2533274790397509"
                                            __designer:wfdid="w433"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td138" valign="top" align="right" colspan="7">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="AShomeaddr" __designer:dtid="2533274790397511"
                                        __designer:wfdid="w434">
                                    </dxe:ASPxLabel>
                                        &nbsp;&nbsp;&nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr11" __designer:dtid="2533274790397497">
                                    <td runat="server" id="Td20" valign="top" align="right" __designer:dtid="2533274790397498">
                                        <asp:Label runat="server" Text="شماره تلفن محل سکونت" Width="136px" ID="Label75"
                                            __designer:dtid="2533274790397514" __designer:wfdid="w435"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td21" valign="top" align="right" colspan="5" __designer:dtid="2533274790397500">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="AShometel" __designer:dtid="2533274790397516"
                                        __designer:wfdid="w436">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td80" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="شماره تلفن همراه" Width="95px" ID="Label78" __designer:dtid="2533274790397524"
                                            __designer:wfdid="w437"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td81" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASmobile" __designer:dtid="2533274790397526"
                                        __designer:wfdid="w438">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr12" __designer:dtid="2533274790397502">
                                    <td runat="server" id="Td22" valign="top" align="right" __designer:dtid="2533274790397503">
                                        <asp:Label runat="server" Text="آدرس محل کار" ID="Label79" __designer:dtid="2533274790397529"
                                            __designer:wfdid="w439"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td23" valign="top" align="right" colspan="7" __designer:dtid="2533274790397505">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASworkaddr" __designer:dtid="2533274790397531"
                                        __designer:wfdid="w440">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr13" __designer:dtid="2533274790397507">
                                    <td runat="server" id="Td24" valign="top" align="right" __designer:dtid="2533274790397508">
                                        <asp:Label runat="server" Text="تلفن محل کار" ID="Label80" __designer:dtid="2533274790397534"
                                            __designer:wfdid="w441"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td25" valign="top" align="right" colspan="5" __designer:dtid="2533274790397510">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASworktel" __designer:dtid="2533274790397536"
                                        __designer:wfdid="w442">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td82" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="شماره فکس" Width="71px" ID="Label83" __designer:dtid="2533274790397544"
                                            __designer:wfdid="w443"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td83" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASfax" __designer:dtid="2533274790397546"
                                        __designer:wfdid="w444">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr14" __designer:dtid="2533274790397512">
                                    <td runat="server" id="Td26" valign="top" align="right" __designer:dtid="2533274790397513">
                                        <asp:Label runat="server" Text="کد پستی محل سکونت" Width="128px" ID="Label77" __designer:dtid="2533274790397519"
                                            __designer:wfdid="w445"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td27" valign="top" align="right" colspan="5" __designer:dtid="2533274790397515">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="AShomepo" __designer:dtid="2533274790397521"
                                        __designer:wfdid="w446">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td84" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="کد پستی محل کار" Width="127px" ID="Label82" __designer:dtid="2533274790397539"
                                            __designer:wfdid="w447"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td85" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASworkpo" __designer:dtid="2533274790397541"
                                        __designer:wfdid="w448">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" valign="top" align="right">
                                        <asp:Label runat="server" Text="محل اقامت" ID="Label2" __designer:dtid="2533274790397579"
                                            __designer:wfdid="w449"></asp:Label>
                                    </td>
                                    <td runat="server" valign="top" align="right" colspan="5">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="AScity" __designer:dtid="2533274790397521"
                                        __designer:wfdid="w450">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" valign="top" align="right" colspan="1"></td>
                                    <td runat="server" valign="top" align="right" colspan="1"></td>
                                </tr>
                                <tr runat="server" id="Tr15" __designer:dtid="2533274790397517">
                                    <td runat="server" id="Td28" valign="top" align="right" __designer:dtid="2533274790397518">
                                        <asp:Label runat="server" Text="شماره پرونده" ID="Label91" Visible="False" __designer:dtid="2533274790397579"
                                            __designer:wfdid="w451"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td29" valign="top" align="right" colspan="5" __designer:dtid="2533274790397520">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASfilenno" Visible="False" __designer:dtid="2533274790397581"
                                        __designer:wfdid="w452">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td86" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="شماره حساب " ID="Label85" Visible="False" __designer:dtid="2533274790397549"
                                            __designer:wfdid="w453"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td87" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASbankaccount" Visible="False"
                                        __designer:dtid="2533274790397551" __designer:wfdid="w454">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr16" __designer:dtid="2533274790397522">
                                    <td runat="server" id="Td30" valign="top" align="right" __designer:dtid="2533274790397523">
                                        <asp:Label runat="server" Text="جنسیت" ID="Label86" __designer:dtid="2533274790397554"
                                            __designer:wfdid="w455"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td31" valign="top" align="right" colspan="5" __designer:dtid="2533274790397525">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASsex" __designer:dtid="2533274790397556"
                                        __designer:wfdid="w456">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td88" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="وضعیت تأهل" ID="Label88" __designer:dtid="2533274790397564"
                                            __designer:wfdid="w457"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td89" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASmar" __designer:dtid="2533274790397566"
                                        __designer:wfdid="w458">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr17" __designer:dtid="2533274790397527">
                                    <td runat="server" id="Td32" valign="top" align="right" __designer:dtid="2533274790397528">
                                        <asp:Label runat="server" Text="وضعیت سربازی" Width="94px" ID="lblSo" Visible="False"
                                            __designer:dtid="2533274790397559" __designer:wfdid="w459"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td33" valign="top" align="right" colspan="5" __designer:dtid="2533274790397530">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASsol" Visible="False" __designer:dtid="2533274790397561"
                                        __designer:wfdid="w460">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td90" valign="top" align="right" colspan="1">&nbsp;
                                    </td>
                                    <td runat="server" id="Td91" valign="top" align="right" colspan="1">&nbsp;
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr28" __designer:dtid="2533274790397582">
                                    <td runat="server" id="Td54" valign="top" align="right" __designer:dtid="2533274790397583">
                                        <asp:Label runat="server" Text="ملیت" ID="Label92" __designer:dtid="2533274790397584"
                                            __designer:wfdid="w461"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td55" valign="top" align="right" colspan="5" __designer:dtid="2533274790397585">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASnationality" __designer:dtid="2533274790397586"
                                        __designer:wfdid="w462">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td112" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="مذهب" ID="Label89" Visible="False" __designer:dtid="2533274790397569"
                                            __designer:wfdid="w463"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td113" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASRel" Visible="False" __designer:dtid="2533274790397571"
                                        __designer:wfdid="w464">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" valign="top" align="right">
                                        <asp:Label runat="server" Text="نمایندگی" ID="Label3" __designer:dtid="2533274790397584"
                                            __designer:wfdid="w465"></asp:Label>
                                    </td>
                                    <td runat="server" valign="top" align="right" colspan="5">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASAgent" __designer:dtid="2533274790397586"
                                        __designer:wfdid="w466">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" valign="top" align="right" colspan="1"></td>
                                    <td runat="server" valign="top" align="right" colspan="1"></td>
                                </tr>
                                <tr runat="server" id="Tr29" __designer:dtid="2533274790397587">
                                    <td runat="server" id="Td56" valign="top" align="right" __designer:dtid="2533274790397588">
                                        <asp:Label runat="server" Text="آدرس وب سایت" Width="105px" ID="Label93" __designer:dtid="2533274790397589"
                                            __designer:wfdid="w467"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td57" valign="top" align="right" colspan="7" __designer:dtid="2533274790397590">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASwebsite" __designer:dtid="2533274790397591"
                                        __designer:wfdid="w468">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr30" __designer:dtid="2533274790397592">
                                    <td runat="server" id="Td58" valign="top" align="right" __designer:dtid="2533274790397593">
                                        <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="123px" ID="Label94" __designer:dtid="2533274790397594"
                                            __designer:wfdid="w469"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td59" valign="top" align="right" colspan="7" __designer:dtid="2533274790397595">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASemail" __designer:dtid="2533274790397596"
                                        __designer:wfdid="w470">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr32" __designer:dtid="2533274790397602">
                                    <td runat="server" id="Td62" valign="top" align="right" __designer:dtid="2533274790397603">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label96" __designer:dtid="2533274790397604"
                                            __designer:wfdid="w471"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td63" valign="top" align="right" colspan="7" __designer:dtid="2533274790397605">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASdesc" __designer:dtid="2533274790397606"
                                        __designer:wfdid="w472">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr5">
                                    <td runat="server" id="Td13" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع فعالیت" ID="ASPxLabel1" __designer:dtid="2533274790397660"
                                            __designer:wfdid="w473">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td19" valign="top" align="right" colspan="7">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASattype" __designer:dtid="2533274790397660"
                                        __designer:wfdid="w474">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کمیسیون ها" ID="ASPxLabel3" __designer:dtid="2533274790397660"
                                            __designer:wfdid="w475">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" valign="top" align="right" colspan="7">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="AScommission" __designer:dtid="2533274790397647"
                                        __designer:wfdid="w476">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr33" __designer:dtid="2533274790397607">
                                    <td runat="server" id="Td64" valign="top" align="right" __designer:dtid="2533274790397608"></td>
                                    <td runat="server" id="Td65" valign="top" align="right" colspan="5" __designer:dtid="2533274790397609"></td>
                                    <td runat="server" id="Td122" valign="top" align="right" colspan="1"></td>
                                    <td runat="server" id="Td123" valign="top" align="right" colspan="1"></td>
                                </tr>
                                <tr runat="server" id="Tr34" __designer:dtid="2533274790397610">
                                    <td runat="server" id="Td66" valign="top" align="right" __designer:dtid="2533274790397611">
                                        <asp:Label runat="server" Text="تاریخ انتقالی" ID="Label76" __designer:dtid="2533274790397612"
                                            __designer:wfdid="w477"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td67" valign="top" align="right" colspan="5" __designer:dtid="2533274790397613">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASTDate" __designer:dtid="2533274790397614"
                                        __designer:wfdid="w478">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td124" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="استان قبلی" ID="Label81" __designer:dtid="2533274790397617"
                                            __designer:wfdid="w479"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td125" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASTPr" __designer:dtid="2533274790397619"
                                        __designer:wfdid="w480">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr36" __designer:dtid="2533274790397620">
                                    <td runat="server" id="Td70" valign="top" align="right" __designer:dtid="2533274790397621">
                                        <asp:Label runat="server" Text="کد عضویت قبلی" ID="Label84" __designer:dtid="2533274790397622"
                                            __designer:wfdid="w481"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td71" valign="top" align="right" colspan="5" __designer:dtid="2533274790397623">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASTMeNo" __designer:dtid="2533274790397624"
                                        __designer:wfdid="w482">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td128" valign="top" align="right" colspan="1">
                                        <asp:Label runat="server" Text="شماره پروانه قبلی" ID="Label99" __designer:dtid="2533274790397627"
                                            __designer:wfdid="w483"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td129" valign="top" align="right" colspan="1">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASTFileNo" __designer:dtid="2533274790397629"
                                        __designer:wfdid="w484">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr38" __designer:dtid="2533274790397630">
                                    <td runat="server" id="Td74" valign="top" align="right" __designer:dtid="2533274790397631">
                                        <asp:Label runat="server" Text="تصویر نامه انتقالی" ID="Label98" __designer:dtid="2533274790397632"
                                            __designer:wfdid="w485"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td75" valign="top" align="right" colspan="5" __designer:dtid="2533274790397633">
                                        <asp:Image runat="server" Height="75px" Width="75px" ID="TImage" __designer:dtid="2533274790397634"
                                            __designer:wfdid="w486"></asp:Image>
                                    </td>
                                    <td runat="server" id="Td132" valign="top" align="right" colspan="1"></td>
                                    <td runat="server" id="Td133" valign="top" align="right" colspan="1"></td>
                                </tr>
                            </table>

                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <fieldset>
                <legend class="HelpUL">خلاصه اطلاعات</legend>

                <fieldset>
                    <legend class="HelpUL">مشخصات کاربری</legend>
                    <table runat="server" id="tblUser" dir="rtl" width="50%">
                        <tr runat="server" id="Tr6">
                            <td runat="server" id="Td34" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="نام کاربری" Width="60px" ID="ASPxLabel36" __designer:wfdid="w487">
                                </dxe:ASPxLabel>
                            </td>
                            <td runat="server" id="Td35" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" ID="ASUserName" __designer:wfdid="w488">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr runat="server" id="Tr7">
                            <td runat="server" id="Td36" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="رمز عبور" Width="60px" ID="ASPxLabel2" __designer:wfdid="w489">
                                </dxe:ASPxLabel>
                            </td>
                            <td runat="server" id="Td37" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" ID="ASPassword" __designer:wfdid="w490">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="کد پیگیری" Width="60px" ID="ASPxLabel4" __designer:wfdid="w491">
                                </dxe:ASPxLabel>
                            </td>
                            <td runat="server" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" ID="lblFollowCode" __designer:wfdid="w492">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GGrdvActivity" Caption="فعالیت ها" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="TMasId">

                    <Columns>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="3">
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="MasId" Name="MasId" Visible="False"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام فعالیت" FieldName="AsName" Name="AsName"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="درصد فعالیت" FieldName="AsPercent" Name="AsPercent"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" Name="Description"
                            VisibleIndex="2">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="AsId" Name="AsId" Visible="False" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>

                </TSPControls:CustomAspxDevGridView>

                <br />
                <TSPControls:CustomAspxDevGridView ID="GrdvLanguage" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="TMlanId" Caption="آشنایی با دیگر زبان ها">

                    <Columns>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="3">
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="MlanId" Name="MlanId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="LanId" Name="LanId" Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام زبان" FieldName="LanName" Name="LanName"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="LqId" Name="LqId" Visible="False" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="حد دانش" FieldName="LqName" Name="LqName"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="2">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <TSPControls:CustomAspxDevGridView ID="GrdvResearch" runat="server" AutoGenerateColumns="False"
                    Caption="مقالات و تحقیقات"
                    KeyFieldName="MraId" Visible="False" RightToLeft="True">

                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="MraId" Name="MraId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام مقاله / فعالیت آموزشی" FieldName="Name"
                            Name="Name" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" Name="Description"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="3">
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع مقاله / فعالیت آموزشی" FieldName="RaName"
                            Name="RaName" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="RaId" Name="RaId" Visible="False" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>

                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GrdvMadrak" runat="server" AutoGenerateColumns="False" Caption="مدارک تحصیلی"
                    ClientInstanceName="grid"
                    KeyFieldName="TMlId" OnHtmlDataCellPrepared="GrdvMadrak_HtmlDataCellPrepared">

                    <Columns>
                        <dxwgv:GridViewDataCheckColumn Caption="پیش فرض" FieldName="DefaultValue" VisibleIndex="0">
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="MlId" Name="MlId" Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="مدرک" FieldName="LiName" Name="LiName" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="رشته" FieldName="MjName" Name="MjName" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="دانشگاه" FieldName="UnName" Name="UnName"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شهر" FieldName="CitName" Name="CitName" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="معدل" FieldName="Avg" Name="Avg" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="LiId" Name="LiId" Visible="False" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="MjId" Name="MjId" Visible="False" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="UniId" Name="UniId" Visible="False" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="NumUnit" Name="NumUnit" VisibleIndex="6"
                            Caption="تعداد واحد">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" VisibleIndex="10"
                            Caption="توضیحات">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="StartDate" Name="StartDate" VisibleIndex="7"
                            Caption="تاریخ شروع">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="EndDate" Name="EndDate" VisibleIndex="8"
                            Caption="تاریخ پایان">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="موضوع پایان نامه" FieldName="Thesis" VisibleIndex="9">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GrdvJob" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="jgrid"
                    KeyFieldName="TMJhId" Caption="سوابق کاری" OnHtmlDataCellPrepared="GrdvJob_HtmlDataCellPrepared">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="JhId" Name="JhId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام پروژه" FieldName="ProjectName" Name="ProjectName"
                            VisibleIndex="0" Width="10%">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کارفرما" FieldName="Employer" Name="Employer"
                            VisibleIndex="3" Width="40%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع پروژه" FieldName="TypeName" Name="TypeName"
                            VisibleIndex="1" Width="10%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شروع همکاری" FieldName="StartCorporateDate"
                            Name="StartCorporateDate" VisibleIndex="4" Width="5%">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="پایان همکاری" FieldName="EndCorporateDate"
                            Name="EndCorporateDate" VisibleIndex="5" Width="5%">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="زیربنا" FieldName="Area" Name="Area" VisibleIndex="11"
                            Width="5%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نحوه مشارکت" FieldName="CorName" Name="CorName"
                            VisibleIndex="6" Width="10%">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PrTypeId" Name="PrTypeId" Visible="False"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SazeTypeId" Name="SazeTypeId" Visible="False"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع سازه" FieldName="SazeName" Name="SazeTypeName"
                            VisibleIndex="8" Width="5%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شهر" FieldName="CitName" Name="CitName" VisibleIndex="10"
                            Width="10%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CounId" Name="CounId" Visible="False" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کشور" FieldName="CounName" Name="CounName"
                            Visible="False" VisibleIndex="12" Width="10%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="ProjectPosition" Name="ProjectPosition"
                            VisibleIndex="7" Width="10%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شروع پروژه" FieldName="StartOriginalDate"
                            Name="StartOriginalDate" VisibleIndex="2" Width="5%">
                            <HeaderStyle Wrap="False" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="StatusOfStartDate" Name="StatusOfStartDate"
                            Visible="False" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="StatusOfEndDate" Name="StatusOfEndDate"
                            Visible="False" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="حجم پروژه" FieldName="ProjectVolume" Name="ProjectVolume"
                            VisibleIndex="9" Width="10%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="طبقات" FieldName="Floors" Name="Floors" VisibleIndex="12"
                            Width="5%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CorTypeId" Name="CorTypeId" Visible="False"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Visible="False"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PJPId" Name="PJPId" Visible="False" VisibleIndex="13">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
            </fieldset>
    </form>
</body>
</html>
