<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOfficeSummary.aspx.cs" Inherits="NezamRegister_WizardOfficeSummary"
    Title="عضویت حقوقی - خلاصه اطلاعات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="false">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Office" Text="مشخصات شرکت">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Agent" Text="شعبه ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضای شرکت">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Letter" Text="آگهی های رسمی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="خلاصه اطلاعات" runat="server">

                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="خلاصه اطلاعات">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 28px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                    <image height="25px" url="~/Images/Help.png" width="25px">
                                                            </image>
                                    <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">مشخصات شرکت</legend>
                           <table> <tbody>
                                <tr __designer:dtid="844424930133518">
                                    <td valign="top" align="right" __designer:dtid="844424930133519">
                                        <asp:Label runat="server" Text="نام شرکت" ID="Label33" __designer:dtid="844424930133520"
                                            __designer:wfdid="w746"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133521">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASOfName" __designer:dtid="844424930133522"
                                        __designer:wfdid="w747">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام شرکت (انگلیسی)" Width="111px" ID="Label36" __designer:dtid="844424930133525"
                                            __designer:wfdid="w748"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASOfNameEn" __designer:dtid="844424930133527"
                                        __designer:wfdid="w749">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133528">
                                    <td valign="top" align="right" __designer:dtid="844424930133529">
                                        <asp:Label runat="server" Text="نوع شرکت" ID="Label44" __designer:dtid="844424930133530"
                                            __designer:wfdid="w750"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133531">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASOfType" __designer:dtid="844424930133532"
                                        __designer:wfdid="w751">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="موضوع شرکت" ID="Label58" __designer:dtid="844424930133535"
                                            __designer:wfdid="w752"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofsubject" __designer:dtid="844424930133537"
                                        __designer:wfdid="w753">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133538">
                                    <td valign="top" align="right" __designer:dtid="844424930133539">
                                        <asp:Label runat="server" Text="شماره ثبت" Width="55px" ID="Label92" __designer:dtid="844424930133540"
                                            __designer:wfdid="w754"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133541">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASregno" __designer:dtid="844424930133542"
                                        __designer:wfdid="w755">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="محل ثبت " ID="Label102" __designer:dtid="844424930133545"
                                            __designer:wfdid="w756"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofregplace" __designer:dtid="844424930133547"
                                        __designer:wfdid="w757">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133548">
                                    <td valign="top" align="right" __designer:dtid="844424930133549">
                                        <asp:Label runat="server" Text="تاریخ ثبت " ID="Label103" __designer:dtid="844424930133550"
                                            __designer:wfdid="w758"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133551">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofrregdate" __designer:dtid="844424930133552"
                                        __designer:wfdid="w759">
                                    </dxe:ASPxLabel>
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره پروانه" ID="Label104" Visible="False" __designer:dtid="844424930133555"
                                            __designer:wfdid="w760"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASoffileno" Visible="False"
                                        __designer:dtid="844424930133557" __designer:wfdid="w761">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133558">
                                    <td valign="top" align="right" __designer:dtid="844424930133559">
                                        <asp:Label runat="server" Text="سرمایه شرکت" ID="Label105" __designer:dtid="844424930133560"
                                            __designer:wfdid="w762"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133561">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofvalue" __designer:dtid="844424930133562"
                                        __designer:wfdid="w763">
                                    </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تعداد سهام شرکت" Width="107px" ID="Label106" __designer:dtid="844424930133565"
                                            __designer:wfdid="w764"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="- - -" ID="ASofstock" __designer:dtid="844424930133567"
                                            __designer:wfdid="w765">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133568">
                                    <td valign="top" align="right" __designer:dtid="844424930133569">
                                        <asp:Label runat="server" Text="نوع فعالیت" ID="Label107" Visible="False" __designer:dtid="844424930133570"
                                            __designer:wfdid="w766"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133571">
                                        <dxe:ASPxLabel runat="server" Text="- - -" ID="ASofattype" Visible="False" __designer:dtid="844424930133572"
                                            __designer:wfdid="w767">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr __designer:dtid="844424930133573">
                                    <td valign="top" align="right" __designer:dtid="844424930133574">
                                        <asp:Label runat="server" Text="تلفن 1" ID="Label108" __designer:dtid="844424930133575"
                                            __designer:wfdid="w768"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133576">
                                        <dxe:ASPxLabel runat="server" Text="- - -" ID="ASoftel1" __designer:dtid="844424930133577"
                                            __designer:wfdid="w769">
                                        </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تلفن 2" ID="Label110" __designer:dtid="844424930133580"
                                            __designer:wfdid="w770"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASoftel2" __designer:dtid="844424930133582"
                                        __designer:wfdid="w771">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133583">
                                    <td valign="top" align="right" __designer:dtid="844424930133584">
                                        <asp:Label runat="server" Text="فکس" ID="Label112" __designer:dtid="844424930133585"
                                            __designer:wfdid="w772"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" __designer:dtid="844424930133586">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASoffax" __designer:dtid="844424930133587"
                                        __designer:wfdid="w773">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره همراه" ID="Label114" __designer:dtid="844424930133590"
                                            __designer:wfdid="w774"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofmobileno" __designer:dtid="844424930133592"
                                        __designer:wfdid="w775">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133593">
                                    <td valign="top" align="right" __designer:dtid="844424930133594">
                                        <asp:Label runat="server" Text="آدرس شرکت" ID="Labe76a" __designer:dtid="844424930133595"
                                            __designer:wfdid="w776"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3" __designer:dtid="844424930133596">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofaddr" __designer:dtid="844424930133597"
                                        __designer:wfdid="w777">
                                    </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133598">
                                    <td valign="top" align="right" __designer:dtid="844424930133599">
                                        <asp:Label runat="server" Text="آدرس وب سایت" ID="Labe77a" __designer:dtid="844424930133600"
                                            __designer:wfdid="w778"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3" __designer:dtid="844424930133601">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofwebsite" __designer:dtid="844424930133602"
                                        __designer:wfdid="w779">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133603">
                                    <td valign="top" align="right" __designer:dtid="844424930133604">
                                        <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="119px" ID="Labe82a" __designer:dtid="844424930133605"
                                            __designer:wfdid="w780"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3" __designer:dtid="844424930133606">&nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofemail" __designer:dtid="844424930133607"
                                        __designer:wfdid="w781">
                                    </dxe:ASPxLabel>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">&nbsp;
                                    </td>
                                    <td valign="top" align="right" colspan="3"></td>
                                </tr>
                                <tr __designer:dtid="844424930133608">
                                    <td valign="top" align="right" __designer:dtid="844424930133609">
                                        <asp:Label runat="server" Text="تصویر آرم شرکت" ID="Label79a" __designer:dtid="844424930133610"
                                            __designer:wfdid="w782"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3" __designer:dtid="844424930133611">&nbsp;<asp:Image runat="server" Width="70px" ID="imgOfArma" __designer:dtid="844424930133612"
                                        __designer:wfdid="w783"></asp:Image>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر مهر شرکت" ID="Label80a" __designer:dtid="844424930133615"
                                            __designer:wfdid="w784"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">&nbsp;<asp:Image runat="server" Width="70px" ID="imgOfSigna" __designer:dtid="844424930133617"
                                        __designer:wfdid="w785"></asp:Image>
                                    </td>
                                </tr>
                                <tr __designer:dtid="844424930133618">
                                    <td valign="top" align="right" __designer:dtid="844424930133619">
                                        <br __designer:dtid="844424930133620" />
                                        <asp:Label runat="server" Text="توضیحات" ID="Label81a" __designer:dtid="844424930133621"
                                            __designer:wfdid="w786"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3" __designer:dtid="844424930133622">&nbsp;<br __designer:dtid="844424930133623" />
                                        &nbsp;<dxe:ASPxLabel runat="server" Text="- - -" ID="ASofdesc" __designer:dtid="844424930133624"
                                            __designer:wfdid="w787">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </tbody>
                            </table>
                        </fieldset>
                        <br />
                        <TSPControls:CustomAspxDevGridView ID="GvMembers" runat="server" AutoGenerateColumns="False"
                            Caption="اعضای شرکت" ClientInstanceName="gridMember"
                            KeyFieldName="ID" Width="100%">
                            <ClientSideEvents RowDblClick="function(s, e) {
	SetGridValues();
}" />
                            <Settings GridLines="None" ShowGroupPanel="True" ShowHorizontalScrollBar="true" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="ID" Name="ID" Visible="False" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="OfmTypeId" Name="OfmTypeId" Visible="False"
                                    VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="OthType" Name="OthType" Visible="False"
                                    VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                 
                                <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" Name="FirstName"
                                    VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" Name="LastName"
                                    VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" Name="IdNo"
                                    VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="کد ملی" FieldName="SSN" Name="SSN" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="BirthDate" Name="BirthDate"
                                    VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="وضعیت امتیاز" FieldName="HasEfficientGradeName" Name="HasEfficientGradeName"
                                    VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="OfPosition" Name="OfPosition"
                                    VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" Name="MobileNo"
                                    Visible="False" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" Name="FatherName"
                                    Visible="False" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شروع همکاری" FieldName="StartDate" Name="StartDate"
                                    VisibleIndex="6">
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
                                    VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " VisibleIndex="11">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="MeId" Name="MeId" Visible="False" VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                        <br />
                        <TSPControls:CustomAspxDevGridView ID="GvAgents" runat="server" AutoGenerateColumns="False"
                            Caption="شعبه ها" ClientInstanceName="grid"
                            EnableCallBacks="False" KeyFieldName="OagId" Width="100%">
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
                                <dxwgv:GridViewDataTextColumn Caption="آدرس " FieldName="Address" Name="Address"
                                    VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Tel_" Name="Tel_" Visible="False" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Fax_" Name="Fax_" Visible="False" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings GridLines="None" ShowGroupPanel="True" ShowHorizontalScrollBar="true" />
                        </TSPControls:CustomAspxDevGridView>
                        <br />
                        <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                            ID="GrdvJob" KeyFieldName="JhId" AutoGenerateColumns="False" Caption="سوابق کاری"
                            ClientInstanceName="jgrid">
                            <Settings ShowGroupPanel="True" GridLines="None" ShowHorizontalScrollBar="true"></Settings>
                            <Columns>
                                <dxwgv:GridViewCommandColumn VisibleIndex="17" Caption=" ">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JhId" Name="JhId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectName" Caption="نام پروژه"
                                    Name="ProjectName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Employer" Caption="کارفرما"
                                    Name="Employer">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="PrTypeName" Caption="نوع پروژه"
                                    Name="PrTypeName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StartCorporateDate" Caption="شروع همکاری"
                                    Name="StartCorporateDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="EndCorporateDate" Caption="پایان همکاری"
                                    Name="EndCorporateDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="13" FieldName="Area" Caption="زیربنا"
                                    Name="Area">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CorTypeName" Caption="نحوه مشارکت"
                                    Name="CorTypeName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="PrTypeId"
                                    Name="PrTypeId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="SazeTypeId"
                                    Name="SazeTypeId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="15" FieldName="SazeTypeName" Caption="نوع سازه"
                                    Name="SazeTypeName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="CitName" Caption="شهر"
                                    Name="CitName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="CounId"
                                    Name="CounId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="CounName" Caption="کشور"
                                    Name="CounName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="ProjectPosition" Caption="سمت"
                                    Name="ProjectPosition">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StartOriginalDate" Caption="شروع پروژه"
                                    Name="StartOriginalDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="10" FieldName="StatusOfStartDate" Caption="وضعیت آغازی"
                                    Name="StatusOfStartDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="11" FieldName="StatusOfEndDate" Caption="وضعیت نهایی"
                                    Name="StatusOfEndDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="ProjectVolume" Caption="حجم پروژه"
                                    Name="ProjectVolume">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="14" FieldName="Floors" Caption="طبقات"
                                    Name="Floors">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="CorTypeId"
                                    Name="CorTypeId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="16" FieldName="Description" Caption="توضیحات"
                                    Name="Description">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                        <br />
                        <TSPControls:CustomAspxDevGridView ID="GvLetters" runat="server" AutoGenerateColumns="False"
                            Caption="آگهی های رسمی"
                            KeyFieldName="OlId" Width="100%">
                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="OlId" Name="OlId" VisibleIndex="0"
                                    Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره روزنامه" FieldName="LeNo" Name="LeNo"
                                    VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره صفحه" FieldName="LePageNo" Name="LePageNo"
                                    VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="LeDate" Name="LeDate" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="LeDesc" Name="LeDesc"
                                    VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                        <div class="row">
                            <TSPControls:CustomASPxCheckBox ID="ChbCheck" runat="server" Width="245px" Font-Bold="True" Text="صحت اطلاعات فوق را تأئید می نمایم."></TSPControls:CustomASPxCheckBox>
                            <asp:Label ID="lblCheck" runat="server" Font-Bold="True" Text="*" Visible="False"
                                ForeColor="Red"></asp:Label>
                            </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>                       
              <div class="Item-center">
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server"  Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" UseSubmitBehavior="False"
                                CausesValidation="False" EnableTheming="False" EnableViewState="False" ToolTip="بازگشت">
                      
                            </TSPControls:CustomAspxButton>
                   
            
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" UseSubmitBehavior="False"
                                CausesValidation="False" EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه">
                                
                            </TSPControls:CustomAspxButton>
                     </div>
                    </tr>
        </ContentTemplate>
    </asp:UpdatePanel>
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
</asp:Content>
