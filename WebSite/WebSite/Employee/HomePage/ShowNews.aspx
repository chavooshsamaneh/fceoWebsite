<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ShowNews.aspx.cs" Inherits="Employee_News_ShowNews"
    Title="مشاهده خبر" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function ShowImg(id) {
            //window.alert(id.src);
            window.open(id.src, '_blank');
        }

        function OnMouseOver(e) {
            e.style.cursor = 'hand';
        }
    </script>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="lbtnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                          
                                                            <Image  Url="~/Images/icons/Back.png"  />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <br />
            <div align="center">
                <table class="TableBorder" width="100%">
                    <tbody>
                        <tr>
                            <td class="TableTitle" align="right" colspan="2">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 20%; background-color: whitesmoke" align="center">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblSub_Imp" runat="server" Font-Size="7.5pt" Width="79px"></asp:Label>
                                                <%--<asp:label id="lblImp" runat="server" font-size="7.5pt"></asp:label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <div onmouseover="OnMouseOver(this);">
                                                    <asp:Image Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid"
                                                        ID="Image1" onclick="ShowImg(this)" runat="server"
                                                        Width="100px" Height="100px" ImageUrl="~/Images/noimage.gif"></asp:Image>
                                                </div>
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="nextButton" runat="server" Width="16px" Visible="False" EnableViewState="False"
                                                                    EnableTheming="False" Text=" " Height="16px">
                                                                    <Image Height="16px" Width="16px" Url="~/News/Image/play.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="playButton" runat="server" Width="16px" Visible="False" EnableViewState="False"
                                                                    EnableTheming="False" Text=" " Height="16px"
                                                                    CausesValidation="False">
                                                                    <Image Height="16px" Width="16px" Url="~/News/Image/Pause.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="prevButton" runat="server" Width="16px" Visible="False" EnableViewState="False"
                                                                    EnableTheming="False" Text=" " Height="16px">
                                                                    <Image Height="16px" Width="16px" Url="~/News/Image/Play2.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" align="center">
                                                <cc1:Rating ID="Rating1" runat="server" Width="69px" Direction="RightToLeft" EmptyStarCssClass="emptyRatingStar"
                                                    FilledStarCssClass="filledRatingStar" RatingDirection="RightToLeftBottomToTop"
                                                    ReadOnly="True" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                                    HorizontalAlign="Right">
                                                </cc1:Rating>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="Label14" runat="server" Text="تعداد بازدید کنندگان :" Font-Size="XX-Small"></asp:Label>
                                                <asp:Label ID="lblCountOfRead" runat="server" Font-Size="XX-Small"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="vertical-align: top; height: 150px; text-align: right">
                                <table width="100%" style="height: 100%">
                                    <tbody>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblDate" runat="server" Font-Size="7pt" ForeColor="Gray"></asp:Label>
                                                <asp:Label ID="lblTime" runat="server" Font-Size="7pt" ForeColor="Gray"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                                                align="right">نمایندگی :
                                                <asp:Label ID="lblAgent" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                                                align="right">
                                                <asp:Label ID="lblBody" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                                                valign="bottom" align="left">
                                                <dxe:ASPxHyperLink ID="HyperLinkAttachment" runat="server" Visible="False"
                                                    Text="دانلود" Target="_blank" ClientInstanceName="HyperLinkAttachment">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <cc1:SlideShowExtender ID="slideshowextend1" runat="server" AutoPlay="true" BehaviorID=""
                    Loop="true" PlayButtonText="Play" SlideShowServiceMethod="GetSlidesForViewFromManagePage"
                    SlideShowServicePath="~/WebService.asmx" StopButtonText="Stop" TargetControlID="Image1">
                </cc1:SlideShowExtender>
                <asp:Panel ID="PanelIdea" runat="server">
                    <table class="TableBorder" style="width: 100%">
                        <tbody>
                            <tr>
                                <td class="TableTitle" colspan="3" align="center">
                                    <b>نظرات</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <asp:DataList ID="DataList1" runat="server" Width="100%" BackColor="White" DataSourceID="ObjectDataSource1"
                                        CellSpacing="5" OnItemDataBound="DataList1_ItemDataBound">
                                        <SeparatorStyle Width="10px" />
                                        <ItemTemplate>
                                            <div align="center">
                                                <table class="TableBorder" style="width: 95%">
                                                    <tr>
                                                        <td style="background-color: whitesmoke" align="right">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("Name") %>' Font-Bold="true" Font-Size="7.5pt"></asp:Label>
                                                                        <asp:Label ID="Label16" runat="server" Text='<%# Eval("LastName") %>' Font-Bold="true" Font-Size="7.5pt"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <cc1:Rating ID="Rating2" runat="server" CurrentRating="0" Direction="RightToLeft"
                                                                            EmptyStarCssClass="emptyRatingStar" FilledStarCssClass="filledRatingStar" HorizontalAlign="Right"
                                                                            OnDataBinding="Rating3_DataBinding" RatingDirection="RightToLeftBottomToTop"
                                                                            ReadOnly="True" StarCssClass="ratingStar" Tag='<%# Bind("Rate") %>' WaitingStarCssClass="savedRatingStar"
                                                                            Width="69px">
                                                                        </cc1:Rating>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="left">
                                                            <asp:Label ID="Label18" runat="server" Text='<%# Eval("Date") %>' Font-Size="7pt" ForeColor="Gray"></asp:Label>

                                                            <asp:Label ID="Label20" runat="server" Text='<%# Eval("StrTime").ToString().Substring(0,5) %>' Font-Size="7pt" ForeColor="Gray"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="vertical-align: top; text-align: right">
                                                            <asp:Label ID="Label17" runat="server" Text='<%# Eval("Body").ToString().Replace("\n","<br>") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="left">
                                                            <br />
                                                            <asp:LinkButton ID="lbntDelete" OnClientClick="return confirm('آیا مطمئن از حذف این نظر هستید؟');"
                                                                OnClick="lbntDelete_Click" runat="server" CommandArgument='<%# Bind("IdeaId") %>'>حذف</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("IdeaId") %>' Visible="False"></asp:Label>
                                            </div>

                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                    </table>
                </asp:Panel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" CausesValidation="False"
                                                                EnableTheming="False" EnableViewState="False" OnClick="lbtnBack_Click" Text=" "
                                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                 
                                                                <Image  Url="~/Images/icons/Back.png"  />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                      
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.NewsIdeaManager"
        UpdateMethod="Update" FilterExpression="NewsId={0}" OldValuesParameterFormatString="original_{0}">

        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>

</asp:Content>
