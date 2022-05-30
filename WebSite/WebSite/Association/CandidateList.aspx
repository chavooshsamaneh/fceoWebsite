<%@ Page Title="اعضای دوره" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="CandidateList.aspx.cs" Inherits="CandidateList" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divcontent" style="width: 100%" align="center" title="کاندیدهای دوره انتخاباتی">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                       <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel3" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="بازگشت" OnClick="btnback_Click" CausesValidation="False">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                     
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />

                <TSPControls:CustomASPxRoundPanel ID="RoundPanelPeriod" HeaderText="مشخصات تشکل"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="نوع تشکل" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtExGroupName"  Width="100%"
                                                Enabled="false" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                        </td>
                                        <td align="left" valign="top" width="35%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ شروع تبلیغات" Width="100%" Visible="false">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" dir="ltr" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStartDatePropagation"  Width="100%"
                                                Enabled="false" RightToLeft="False"  Visible="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="تاریخ پایان تبلیغات" Width="100%"  Visible="false">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="left" dir="ltr" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndDatePropagation"  Width="100%"
                                                Enabled="false" RightToLeft="False"   Visible="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ شروع" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="right" dir="ltr" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStartDate"  Width="100%"
                                                Enabled="false" RightToLeft="False" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ پایان" Width="100%">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td align="left" dir="ltr" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndDate"  Width="100%" Enabled="false"
                                                RightToLeft="False" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <TSPControls:CustomAspxMenuHorizontal ID="MenueMajors" runat="server" 
                    Width="100%" OnItemClick="MenueMajors_ItemClick">                   
                </TSPControls:CustomAspxMenuHorizontal>
                <br />
                <TSPControls:CustomAspxDevDataView ID="DataViewCandidate" runat="server" ColumnCount="1"
                    RowPerPage="10" Width="100%" EmptyDataText="هیچ کاندیدی در این رشته ثبت نشده است"
                    DataSourceID="OdbCandid" PagerSettings-EndlessPagingMode="OnScroll">                    
                    <ItemTemplate>
                        <table width="100%" cellpadding="5" class="TableBorder">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top;" align="right" class="TableTitle" colspan="2">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("TiName") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("MemberName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;" align="center" width="20%">
                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" Width="100px" ImageUrl='<%# Bind("ImageUrl") %>'
                                            Height="100px">
                                            <EmptyImage Height="100px" Width="100px" Url="~/Images/person.png">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                    <td style="vertical-align: top;" align="right" width="80%">
                                        <table width="100%" cellpadding="5">
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label ID="Label17" runat="server" Text="کد عضویت:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="25%">
                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("MeId") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label ID="Label12" runat="server" Text="مقطع تحصیلی:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="25%">
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("LiNames") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label15" runat="server" Text="رشته تحصیلی:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("MjNames") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="دانشگاه:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("UnName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label8" runat="server" Text="پایه طراحی/محاسبه:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("DesGrdName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label10" runat="server" Text="پایه نظارت:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("ObsGrdName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label16" runat="server" Text="پایه اجرا:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label20" runat="server" Text='<%# Bind("ImpGrdName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label21" runat="server" Text="پایه ترافیک:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label22" runat="server" Text='<%# Bind("TrafficGrdName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label23" runat="server" Text="پایه شهرسازی:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label24" runat="server" Text='<%# Bind("UrbanismGrdName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label25" runat="server" Text="پایه نقشه برداری:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label26" runat="server" Text='<%# Bind("MappingGrdName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label1" runat="server" Text="وضعیت:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StatusName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label27" runat="server" Text="سمت:" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label28" runat="server" Text='<%# Bind("PositionName") %>' Font-Size="8pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label3" runat="server" Text="تعداد آرا:" Font-Size="8pt" Visible="false"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("VoteCount") %>' Font-Size="8pt"
                                                        Font-Bold="true"  Visible="false"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                </td>
                                                <td valign="top" align="right">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ItemTemplate>
                </TSPControls:CustomAspxDevDataView>
                <br />
                   <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="بازگشت" OnClick="btnback_Click" CausesValidation="False">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image> 
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div style="font-size: 9pt; font-family: Tahoma" class="modalPopup">
                    <img alt="" id="IMG1" src="../Images/indicator.gif" align="middle" />
                    لطفا صبر نمایید ...</div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="OdbCandid" runat="server" SelectMethod="SelectAcceptCandidate"
            TypeName="TSP.DataManager.CandidateManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="ExGroupPeriodId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="InActive" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
