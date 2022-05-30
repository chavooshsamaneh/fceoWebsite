<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherBasicInfo.aspx.cs" Inherits="Teachers_TeacherInfo_TeacherBasicInfo"
    Title="مشخصات فردی استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

         <asp:updatepanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
        <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
            <asp:label id="LabelWarning" runat="server" text="Label"></asp:label>
            [<a class="closeLink" href="#">بستن</a>]</div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server" 
              OnItemClick="ASPxMenu1_ItemClick" 
                >
                <Items>
                    <dxm:MenuItem Text="مشخصات فردی" Name="BasicInfo" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مدارک تحصیلی" Name="Madrak">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوابق آموزشی" Name="Job">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="تالیفات و تحقیقات" Name="Research">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مستندات" Name="Atachment">
                    </dxm:MenuItem>
                    <dxm:MenuItem Visible="False" Text="دروس" Name="Course">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
        <br />
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="مشخصات فردی استاد" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
        
                        <table dir="rtl" width="100%">
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel32" runat="server" Text="عنوان استاد:" Width="87px">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblTiName" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="نام:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblName" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="نام خانوادگی:" Width="75px">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblFamily" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="نام پدر:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblFatherName" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="تاریخ تولد:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblBirthDate" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="شماره شناسنامه:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblIdNo" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="کد ملی:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblSSN" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="شماره تلفن:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblTel" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="شماره همراه:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblMobileNo" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="آدرس پست الکترونیکی:" Width="123px">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblEmail" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="آخرین مدرک تحصیلی:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblLiName" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="رشته:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblMjName" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel29" runat="server" Text="آدرس:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblAddress" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel30" runat="server" Text="توضیحات:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblDesc" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <br />
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel31" runat="server" Text="وضعیت:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblInActiveName" runat="server" Text="- - -">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                </td>
                            </tr>
                        </table>
                   </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
        <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                            <table >
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False" OnClick="btnBack_Click">
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                              
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                      </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate> </asp:updatepanel>
        <asp:objectdatasource id="ObjdsTeacherLicence" runat="server" selectmethod="SelectByTeacherId"
            typename="TSP.DataManager.TeacherLicenceManger">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="TeacherId" Type="Int32" />
            </SelectParameters>
        </asp:objectdatasource>
        <asp:objectdatasource id="ObjdsTeacherJobHistory" runat="server" selectmethod="SelectByTeacherId"
            typename="TSP.DataManager.TeacherJobHistoryManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="TeacherId" Type="Int32" />
            </SelectParameters>
        </asp:objectdatasource>
        <asp:objectdatasource id="ObjdsTeacherResearch" runat="server" selectmethod="SelectByTeacher"
            typename="TSP.DataManager.TeacherResearchActivityManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="TecherId" Type="Int32" />
            </SelectParameters>
        </asp:objectdatasource>
        <asp:hiddenfield id="MeId" runat="server" visible="False" />
        <dxhf:ASPxHiddenField ID="HiddenFieldTeCertificate" runat="server">
        </dxhf:ASPxHiddenField>
</asp:Content>
