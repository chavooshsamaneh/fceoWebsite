<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardMemberSummary.aspx.cs" Inherits="NezamRegister_WizardMemberSummary"
    Title="عضویت حقیقی - خلاصه اطلاعات" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                    <Items>
                        <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Member" Text="مشخصات فردی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Job" Text="سوابق کاری">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Language" Text="زبان ها">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="End" Text="ثبت نهایی">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="خلاصه اطلاعات" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <fieldset>
                                <legend class="HelpUL">مشخصات عضو</legend>
                                <table runat="server" id="TABLE2" dir="rtl" width="100%">
                                    <tr runat="server" id="Tr2">
                                        <td runat="server" id="Td2" valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="عنوان" ID="Label63" Visible="False"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td3" valign="top" align="right" width="30%">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="AStitle" Visible="False" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td1" valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="تصویر" ID="Label95"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td4" valign="top" align="right" width="30%">
                                            <asp:Image runat="server" Width="75px" Height="75px" ID="Image"></asp:Image>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr3">
                                        <td runat="server" id="Td5" valign="top" align="right">
                                            <asp:Label runat="server" Text="نام" ID="Label64"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td6" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASfirstname" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td7" valign="top" align="right">
                                            <asp:Label runat="server" Text=" (انگلیسی)" ID="Label65"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td8" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASfirstnameen" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr4">
                                        <td runat="server" id="Td9" valign="top" align="right">
                                            <asp:Label runat="server" Text="نام خانوادگی" ID="Label66"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td10" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASlastname" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td11" valign="top" align="right">
                                            <asp:Label runat="server" Text=" (انگلیسی)" ID="Label1"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td12" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASlastnameen" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr1">
                                        <td runat="server" id="Td134" valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره شناسنامه" ID="Label71"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td135" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASIdno" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td136" valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره ملی" ID="Label72"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td137" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASssn" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr8">
                                        <td runat="server" id="Td14" valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ تولد" ID="Label69"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td15" dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASbirthdate" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td76" valign="top" align="right">
                                            <asp:Label runat="server" Text="محل تولد" ID="Label70"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td77" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASbirthplace" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr9">
                                        <td runat="server" id="Td16" valign="top" align="right">
                                            <asp:Label runat="server" Text="نام پدر" ID="Label68"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td17" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASPfathername" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td78" valign="top" align="right">
                                            <asp:Label runat="server" Text="محل صدور" ID="Label73"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td79" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASissueplace" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr10">
                                        <td runat="server" id="Td18" valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس محل سکونت" ID="Label74"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td138" valign="top" align="right" colspan="7">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="AShomeaddr" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr11">
                                        <td runat="server" id="Td20" valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره تلفن محل سکونت" ID="Label75"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td21" dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="AShometel" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td80" valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره تلفن همراه" ID="Label78"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td81" valign="top" align="right" colspan="1">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASmobile" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr12">
                                        <td runat="server" id="Td22" valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس محل کار" ID="Label79"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td23" valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASworkaddr" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr13">
                                        <td runat="server" id="Td24" valign="top" align="right">
                                            <asp:Label runat="server" Text="تلفن محل کار" ID="Label80"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td25" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASworktel" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td82" valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره فکس" ID="Label83"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td83" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASfax" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr14">
                                        <td runat="server" id="Td26" valign="top" align="right">
                                            <asp:Label runat="server" Text="کد پستی محل سکونت" ID="Label77"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td27" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="AShomepo" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td84" valign="top" align="right">
                                            <asp:Label runat="server" Text="کد پستی محل کار" ID="Label82"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td85" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASworkpo" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" id="Td34" valign="top" align="right">
                                            <asp:Label runat="server" Text="محل اقامت" ID="Label2"></asp:Label>
                                        </td>
                                        <td runat="server" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="AScity" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" valign="top" align="right" colspan="1"></td>
                                        <td runat="server" valign="top" align="right" colspan="1"></td>
                                    </tr>
                                    <tr runat="server" id="Tr15">
                                        <td runat="server" id="Td86" valign="top" align="right" colspan="1">
                                            <asp:Label runat="server" Text="شماره حساب " ID="Label85"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td87" valign="top" align="right" colspan="1">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASbankaccount" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td28" valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره پرونده" ID="Label91" Visible="False"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td29" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASfilenno" Visible="False" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr16">
                                        <td runat="server" id="Td30" valign="top" align="right">
                                            <asp:Label runat="server" Text="جنسیت" ID="Label86"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td31" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASsex" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td88" valign="top" align="right">
                                            <asp:Label runat="server" Text="وضعیت تأهل" ID="Label88"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td89" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASmar" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr17">
                                        <td runat="server" id="Td32" valign="top" align="right">
                                            <asp:Label runat="server" Text="وضعیت سربازی" ID="lblSo" Visible="False"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td33" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASsol" Visible="False" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td90" valign="top" align="right"></td>
                                        <td runat="server" id="Td91" valign="top" align="right"></td>
                                    </tr>
                                    <tr runat="server" id="Tr28">
                                        <td runat="server" id="Td54" valign="top" align="right">
                                            <asp:Label runat="server" Text="ملیت" ID="Label92"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td55" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASnationality" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td112" valign="top" align="right">
                                            <asp:Label runat="server" Text="مذهب" ID="Label89" Visible="False"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td113" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASRel" Visible="False" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" valign="top" align="right">
                                            <asp:Label runat="server" Text="نمایندگی" ID="Label3"></asp:Label>
                                        </td>
                                        <td runat="server" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASAgent" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" valign="top" align="right"></td>
                                        <td runat="server" valign="top" align="right"></td>
                                    </tr>
                                    <tr runat="server" id="Tr29">
                                        <td runat="server" id="Td56" valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس وب سایت" ID="Label93"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td57" valign="top" align="right" colspan="3" _>
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASwebsite" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr30">
                                        <td runat="server" id="Td58" valign="top" align="right">
                                            <asp:Label runat="server" Text="پست الکترونیکی" ID="Label94"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td59" valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASemail" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr32">
                                        <td runat="server" id="Td62" valign="top" align="right">
                                            <asp:Label runat="server" Text="توضیحات" ID="Label96"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td63" valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASdesc" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr5">
                                        <td runat="server" id="Td13" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نوع فعالیت" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td19" valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASattype" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کمیسیون ها" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="AScommission" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr34">
                                        <td runat="server" id="Td66" valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ انتقالی" ID="Label76"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td67" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASTDate" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td124" valign="top" align="right">
                                            <asp:Label runat="server" Text="استان قبلی" ID="Label81"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td125" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASTPr" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr36">
                                        <td runat="server" id="Td70" valign="top" align="right">
                                            <asp:Label runat="server" Text="کد عضویت قبلی" ID="Label84"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td71" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASTMeNo" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td128" valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره پروانه قبلی" ID="Label99"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td129" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="ASTFileNo" ForeColor="DarkBlue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr38">
                                        <td runat="server" id="Td74" valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر نامه انتقالی" ID="Label98"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td75" valign="top" align="right">
                                            <asp:Image runat="server" Width="75px" Height="75px" ID="TImage"></asp:Image>
                                        </td>
                                        <td runat="server" id="Td132" valign="top" align="right">
                                              <asp:Label runat="server" Text="تصویر نامه عدم عضویت کانون کاردان ها" ID="Label4"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td133" valign="top" align="right">
                                                <asp:Image runat="server" Width="75px" Height="75px" ID="ImgKardan"></asp:Image>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <div dir="rtl">
                                <asp:Panel runat="server" Width="100%" ScrollBars="Horizontal" ID="Panel1">
                                    <TSPControls:CustomAspxDevGridView runat="server" Width="100%" Font-Size="8pt" ID="GGrdvActivity"
                                        KeyFieldName="MasId" Caption="فعالیت ها" RightToLeft="True">
                                        <Settings ShowGroupPanel="True" ShowFilterBar="Hidden"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="MasId"
                                                Caption="کد" Name="MasId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AsName" Caption="نام فعالیت"
                                                Name="AsName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="AsPercent" Caption="درصد فعالیت"
                                                Name="AsPercent">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات"
                                                Name="Description">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="AsId" Name="AsId">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView>
                                </asp:Panel>
                                <br />
                                <asp:Panel runat="server" Width="100%" ScrollBars="Horizontal" ID="Panel2">
                                    <TSPControls:CustomAspxDevGridView runat="server" Width="100%" Font-Size="8pt" ID="GrdvLanguage"
                                        KeyFieldName="MlanId" Caption="آشنایی با دیگر زبان ها" RightToLeft="True">
                                        <Settings ShowGroupPanel="True" ShowFilterBar="Hidden"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlanId"
                                                Name="MlanId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="LanId"
                                                Name="LanId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LanName" Caption="نام زبان"
                                                Name="LanName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="LqId" Name="LqId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LqName" Caption="حد دانش"
                                                Name="LqName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView>
                                </asp:Panel>
                                <br />
                                <asp:Panel runat="server" Width="100%" ScrollBars="Horizontal" ID="Panel3" Visible="False">
                                    <TSPControls:CustomAspxDevGridView runat="server" Width="100%" ID="GrdvResearch"
                                        KeyFieldName="MraId" Caption="مقالات و تحقیقات" RightToLeft="True">
                                        <Settings ShowGroupPanel="True" ShowFilterBar="Hidden"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MraId"
                                                Name="MraId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Name" Caption="نام مقاله / فعالیت آموزشی"
                                                Name="Name">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات"
                                                Name="Description">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="RaName" Caption="نوع مقاله / فعالیت آموزشی"
                                                Name="RaName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="RaId" Name="RaId">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView>
                                </asp:Panel>
                                <br />
                                <asp:Panel runat="server" Width="100%" ScrollBars="Horizontal" ID="Panel4">
                                    <TSPControls:CustomAspxDevGridView runat="server" Width="100%" ID="GrdvMadrak" KeyFieldName="MlId"
                                        Caption="مدارک تحصیلی" ClientInstanceName="grid" OnHtmlDataCellPrepared="GrdvMadrak_HtmlDataCellPrepared"
                                        RightToLeft="True">
                                        <Settings ShowGroupPanel="True" ShowFilterBar="Hidden" ShowHorizontalScrollBar="true"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlId" Name="MlId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LiName" Caption="مدرک"
                                                Name="LiName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MajorName" Caption="رشته"
                                                Name="MajorName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="UniName" Caption="دانشگاه"
                                                Name="UniName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CityName" Caption="شهر"
                                                Name="CityName">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Avg" Caption="معدل" Name="Avg">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="LiId" Name="LiId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="MajorId"
                                                Name="MajorId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="UniId"
                                                Name="UniId">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="NumUnit" Caption="تعداد واحد"
                                                Name="NumUnit">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="11" FieldName="Description" Caption="توضیحات"
                                                Name="Description">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="StartDate" Caption="تاریخ شروع"
                                                Name="StartDate">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="EndDate" Caption="تاریخ پایان"
                                                Name="EndDate">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Thesis" Caption="موضوع پایان نامه">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CounName" Caption="کشور">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="DefaultValue" Caption="پیش فرض">
                                            </dxwgv:GridViewDataCheckColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView>
                                </asp:Panel>
                                <br />
                                <asp:Panel runat="server" Width="100%" ScrollBars="Horizontal" ID="Panel5">
                                    <TSPControls:CustomAspxDevGridView runat="server" Width="100%" ID="GrdvJob" KeyFieldName="JhId"
                                        Caption="سوابق کاری" ClientInstanceName="jgrid" OnHtmlDataCellPrepared="GrdvJob_HtmlDataCellPrepared"
                                        RightToLeft="True">
                                        <Settings ShowGroupPanel="True" ShowFilterBar="Hidden" ShowHorizontalScrollBar="true"></Settings>
                                        <Columns>
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
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="EndCorporateDate" Caption="پایان همکاری"
                                                Name="EndCorporateDate">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
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
                                </asp:Panel>
                                <br />
                                <table style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%" dir="rtl" align="right">
                                                <TSPControls:CustomASPxCheckBox runat="server" Width="245px" Font-Bold="True" Text="صحت اطلاعات فوق را تأئید می نمایم."
                                                    ID="ChbCheck"></TSPControls:CustomASPxCheckBox>
                                                <asp:Label runat="server" Text="*" Font-Bold="True" ForeColor="Red" ID="lblCheck"
                                                    Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>

                <div class="Item-center">
                    <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server" UseSubmitBehavior="False"
                      CausesValidation="False" ToolTip="بازگشت" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;"
                        EnableViewState="False" EnableTheming="False">
                    </TSPControls:CustomAspxButton>
                    <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" UseSubmitBehavior="False"
                        Text="تایید و ادامه" CausesValidation="False" ToolTip="تایید و ادامه"
                        EnableViewState="False" EnableTheming="False">
                    </TSPControls:CustomAspxButton>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
