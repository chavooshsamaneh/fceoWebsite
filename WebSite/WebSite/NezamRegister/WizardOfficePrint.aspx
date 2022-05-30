<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WizardOfficePrint.aspx.cs"
    Inherits="NezamRegister_WizardOfficePrint" Title="عضویت حقوقی - چاپ اطلاعات کاربری" %>

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
    <title>عضویت حقوقی - چاپ اطلاعات کاربری</title>
      <link rel="stylesheet" href="../StyleSheet/bootstrap.css" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
        <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="خلاصه اطلاعات" runat="server">
            <PanelCollection>
                <dxp:PanelContent>
                    <fieldset>
                        <legend class="HelpUL">مشخصات شرکت </legend>
                        <table dir="rtl" width="600" __designer:dtid="844424930133515">
                            <tbody>
                                <tr __designer:dtid="844424930133518">
                                    <td valign="top" align="right" __designer:dtid="844424930133519">
                                        <asp:Label runat="server" Text="نام شرکت" ID="Label33" __designer:dtid="844424930133520"
                                            __designer:wfdid="w319"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133521">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASOfName" __designer:dtid="844424930133522"
                                        __designer:wfdid="w320">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133523">
                                    <td valign="top" align="right" __designer:dtid="844424930133524">
                                        <asp:Label runat="server" Text="نام شرکت (English)" Width="144px" ID="Label36" __designer:dtid="844424930133525"
                                            __designer:wfdid="w216"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133526">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASOfNameEn" __designer:dtid="844424930133527"
                                        __designer:wfdid="w322">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133528">
                                    <td valign="top" align="right" __designer:dtid="844424930133529">
                                        <asp:Label runat="server" Text="نوع شرکت" ID="Label44" __designer:dtid="844424930133530"
                                            __designer:wfdid="w323"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133531">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASOfType" __designer:dtid="844424930133532"
                                        __designer:wfdid="w324">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133533">
                                    <td valign="top" align="right" __designer:dtid="844424930133534">
                                        <asp:Label runat="server" Text="موضوع شرکت" ID="Label58" __designer:dtid="844424930133535"
                                            __designer:wfdid="w325"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133536">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofsubject" __designer:dtid="844424930133537"
                                        __designer:wfdid="w326">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133538">
                                    <td valign="top" align="right" __designer:dtid="844424930133539">
                                        <asp:Label runat="server" Text="شماره ثبت شرکت" Width="128px" ID="Label92" __designer:dtid="844424930133540"
                                            __designer:wfdid="w222"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133541">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASregno" __designer:dtid="844424930133542"
                                        __designer:wfdid="w328">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133543">
                                    <td valign="top" align="right" __designer:dtid="844424930133544">
                                        <asp:Label runat="server" Text="محل ثبت شرکت" ID="Label102" __designer:dtid="844424930133545"
                                            __designer:wfdid="w329"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133546">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofregplace" __designer:dtid="844424930133547"
                                        __designer:wfdid="w330">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133548">
                                    <td valign="top" align="right" __designer:dtid="844424930133549">
                                        <asp:Label runat="server" Text="تاریخ ثبت شرکت" ID="Label103" __designer:dtid="844424930133550"
                                            __designer:wfdid="w331"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133551">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofrregdate" __designer:dtid="844424930133552"
                                        __designer:wfdid="w332">
                                    </dxe:ASPxLabel>
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133553">
                                    <td valign="top" align="right" __designer:dtid="844424930133554">
                                        <asp:Label runat="server" Text="شماره پروانه" ID="Label104" Visible="False" __designer:dtid="844424930133555"
                                            __designer:wfdid="w333"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133556">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASoffileno" Visible="False"
                                        __designer:dtid="844424930133557" __designer:wfdid="w334">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133558">
                                    <td valign="top" align="right" __designer:dtid="844424930133559">
                                        <asp:Label runat="server" Text="سرمایه شرکت" ID="Label105" __designer:dtid="844424930133560"
                                            __designer:wfdid="w335"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133561">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofvalue" __designer:dtid="844424930133562"
                                        __designer:wfdid="w336">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133563">
                                    <td valign="top" align="right" __designer:dtid="844424930133564">
                                        <asp:Label runat="server" Text="تعداد سهام شرکت" Width="134px" ID="Label106" __designer:dtid="844424930133565"
                                            __designer:wfdid="w232"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133566">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofstock" __designer:dtid="844424930133567"
                                        __designer:wfdid="w338">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133568">
                                    <td valign="top" align="right" __designer:dtid="844424930133569">
                                        <asp:Label runat="server" Text="نوع فعالیت" ID="Label107" Visible="False" __designer:dtid="844424930133570"
                                            __designer:wfdid="w339"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133571">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofattype" Visible="False"
                                        __designer:dtid="844424930133572" __designer:wfdid="w340">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133573">
                                    <td valign="top" align="right" __designer:dtid="844424930133574">
                                        <asp:Label runat="server" Text="تلفن 1" ID="Label108" __designer:dtid="844424930133575"
                                            __designer:wfdid="w341"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133576">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASoftel1" __designer:dtid="844424930133577"
                                        __designer:wfdid="w342">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133578">
                                    <td valign="top" align="right" __designer:dtid="844424930133579">
                                        <asp:Label runat="server" Text="تلفن 2" ID="Label110" __designer:dtid="844424930133580"
                                            __designer:wfdid="w343"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133581">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASoftel2" __designer:dtid="844424930133582"
                                        __designer:wfdid="w344">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133583">
                                    <td valign="top" align="right" __designer:dtid="844424930133584">
                                        <asp:Label runat="server" Text="فکس" ID="Label112" __designer:dtid="844424930133585"
                                            __designer:wfdid="w345"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133586">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASoffax" __designer:dtid="844424930133587"
                                        __designer:wfdid="w346">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133588">
                                    <td valign="top" align="right" __designer:dtid="844424930133589">
                                        <asp:Label runat="server" Text="شماره همراه" ID="Label114" __designer:dtid="844424930133590"
                                            __designer:wfdid="w347"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133591">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofmobileno" __designer:dtid="844424930133592"
                                        __designer:wfdid="w348">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133593">
                                    <td valign="top" align="right" __designer:dtid="844424930133594">
                                        <asp:Label runat="server" Text="آدرس شرکت" ID="Labe76a" __designer:dtid="844424930133595"
                                            __designer:wfdid="w349"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133596">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofaddr" __designer:dtid="844424930133597"
                                        __designer:wfdid="w350">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133598">
                                    <td valign="top" align="right" __designer:dtid="844424930133599">
                                        <asp:Label runat="server" Text="آدرس وب سایت" ID="Labe77a" __designer:dtid="844424930133600"
                                            __designer:wfdid="w351"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133601">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofwebsite" __designer:dtid="844424930133602"
                                        __designer:wfdid="w352">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133603">
                                    <td valign="top" align="right" __designer:dtid="844424930133604">
                                        <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="159px" ID="Labe82a" __designer:dtid="844424930133605"
                                            __designer:wfdid="w248"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133606">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofemail" __designer:dtid="844424930133607"
                                        __designer:wfdid="w354">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133608">
                                    <td valign="top" align="right" __designer:dtid="844424930133609">
                                        <asp:Label runat="server" Text="تصویر آرم شرکت" ID="Label79a" __designer:dtid="844424930133610"
                                            __designer:wfdid="w355"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133611">&nbsp;<asp:Image runat="server" Width="70px" ID="imgOfArma" __designer:dtid="844424930133612"
                                        __designer:wfdid="w356"></asp:Image>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133613">
                                    <td valign="top" align="right" __designer:dtid="844424930133614">
                                        <asp:Label runat="server" Text="تصویر مهر شرکت" ID="Label80a" __designer:dtid="844424930133615"
                                            __designer:wfdid="w357"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133616">&nbsp;<asp:Image runat="server" Width="70px" ID="imgOfSigna" __designer:dtid="844424930133617"
                                        __designer:wfdid="w358"></asp:Image>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133618">
                                    <td valign="top" align="right" __designer:dtid="844424930133619">
                                        <br __designer:dtid="844424930133620" />
                                        <asp:Label runat="server" Text="توضیحات" ID="Label81a" __designer:dtid="844424930133621"
                                            __designer:wfdid="w359"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133622">&nbsp;<br __designer:dtid="844424930133623" />
                                        &nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofdesc" __designer:dtid="844424930133624"
                                            __designer:wfdid="w360">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                    <fieldset>
                        <legend class="HelpUL">مشخصات کاربری</legend>                        
                            <div class="row">
                                <div class="col-md-3">نام کاربری</div>
                                <div class="col-md-3">      <dxe:ASPxLabel runat="server" ID="ASUserName" >
                                    </dxe:ASPxLabel></div>
                                <div class="col-md-3">رمز عبور</div>
                                <div class="col-md-3">     <dxe:ASPxLabel runat="server" ID="ASPassword" >
                                    </dxe:ASPxLabel></div>
                            </div>                                                
                    </fieldset>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanel>
        <br />
        <TSPControls:CustomAspxDevGridView2 ID="GvMembers" runat="server" AutoGenerateColumns="False"
            ClientInstanceName="gridMember"
            KeyFieldName="OfmId" RightToLeft="True"  Width="100%" >
            <ClientSideEvents RowDblClick="function(s, e) {
	SetGridValues();
}" />

            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="ID" Name="ID" Visible="False" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="OfmTypeId" Name="OfmTypeId" Visible="False"
                    VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="OthType" Name="OthType" Visible="False"
                    VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FullName" Name="FullName"
                    VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="OfpName" Name="OfpName" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" Name="MobileNo"
                    Visible="False" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" Name="FatherName"
                    Visible="False" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شروع همکاری" FieldName="StartDate" Name="StartDate"
                    VisibleIndex="6">
                    <HeaderStyle Wrap="True" />
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="BirthPlace" Name="BirthPlace" Visible="False"
                    VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="OfpId" Name="OfpId" Visible="False" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="HasSignRight" Name="HasSignRight" Visible="False"
                    VisibleIndex="9">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ImageUrl" Name="ImageUrl" Visible="False"
                    VisibleIndex="9">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SignUrl" Name="SignUrl" Visible="False"
                    VisibleIndex="9">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="FileNo" Name="FileNo" Visible="False" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="IsFullTime" Name="IsFullTime" Visible="False"
                    VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Visible="False"
                    VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Tel" Name="Tel" Visible="False" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Address" Name="Address" Visible="False"
                    VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="OtpCode" Name="OtpCode" Visible="False"
                    VisibleIndex="9">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نوع" FieldName="OfmTypeName" Name="OfmTypeName"
                    VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="MeId" Name="MeId" Visible="False" VisibleIndex="8">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </TSPControls:CustomAspxDevGridView2>
        <br />
        <TSPControls:CustomAspxDevGridView2 ID="GvAgents" runat="server" AutoGenerateColumns="False"
            ClientInstanceName="grid"
            EnableCallBacks="False" KeyFieldName="OagId"  Width="100%">
            <ClientSideEvents RowClick="function(s, e) {
	SetControlValues();
	btnAgent.SetEnabled(false);
}" />

            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="OagId" Name="OagId" Visible="False"
                    VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام شعبه" FieldName="OagName" Name="OagName"
                    VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="مدیر مسئول" FieldName="Responsible" Name="Responsible"
                    VisibleIndex="1">
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
                <dxwgv:GridViewDataTextColumn FieldName="Website" Name="Website" VisibleIndex="4"
                    Caption="وب سایت">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Email" Name="Email" VisibleIndex="5" Caption="پست الکترونیکی">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="آدرس" FieldName="Address" Name="Address" VisibleIndex="6">
                    <CellStyle Wrap="True">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Tel_" Name="Tel_" Visible="False" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Fax_" Name="Fax_" Visible="False" VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </TSPControls:CustomAspxDevGridView2>
        <br />
        <TSPControls:CustomAspxDevGridView2 ID="GvLetters" runat="server" AutoGenerateColumns="False"
            KeyFieldName="OlId" Width="100%">

            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="OlId" Name="OlId" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره روزنامه" FieldName="LetterNo" Name="LeNo"
                    VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره صفحه" FieldName="PageNo" Name="LePageNo"
                    VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" Name="LeDate" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" Name="LeDesc"
                    VisibleIndex="4">
                    <CellStyle Wrap="True">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </TSPControls:CustomAspxDevGridView2>
    </form>
</body>
</html>
