<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="TraceAutomationLetter.aspx.cs" Inherits="TraceDocuments_TraceAutomationLetter"
    Title="پیگیری سند" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpg" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dxtc:ASPxPageControl ID="PageControl1" runat="server" ActiveTabIndex="0" 
          TabSpacing="0px" Width="100%">
        <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
        <TabPages>
            <dxtc:TabPage Text="مشخصات عمومی سند">
                <ContentCollection>
                    <dxw:contentcontrol runat="server">
                        <div dir="rtl" align="right" style="font-family: Tahoma; font-size: 8pt">
                            <table width="100%" cellspacing="15px">
                                <tr>
                                    <td style="width: 15%" align="right">
                                        کد رهگیری :&nbsp;</td>
                                    <td style="width: 30%" align="right">
                                        <asp:Label runat="server" ID="lblSerialNo" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                    </td>
                                    <td style="width: 15%" align="right">
                                        شماره سند :&nbsp;</td>
                                    <td style="width: 30%" align="right">
                                        <asp:Label runat="server" ID="lblDocumentNo" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        دبیرخانه :&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblSecretariat" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        گروه :&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblGroup" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        نوع سند :&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblDocumentType" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="font-size: 7.5pt">
                                        نحوه ارسال/دریافت :&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblSendRecieveType" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        گروه موضوعی :&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTitleType" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        تاریخ سند :&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDocumentDate" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        موضوع سند :&nbsp;
                                    </td>
                                    <td colspan="4">
                                        <asp:Label runat="server" ID="lblTittle" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        فرستنده :&nbsp;
                                    </td>
                                    <td colspan="4">
                                        <asp:Label runat="server" ID="lblSender" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        گیرندگان :&nbsp;
                                    </td>
                                    <td colspan="4">
                                        <asp:Label runat="server" ID="lblRecievers" Text="" ForeColor="darkblue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxw:contentcontrol>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="ارجاعات سند">
                <ContentCollection>
                    <dxw:contentcontrol runat="server">
                        <div align="right">
                            <dxwtl:ASPxTreeList ID="ASPxTreeList1" runat="server" 
                                 DataSourceID="ObjectDataSourceLetterReferenceTrace" KeyFieldName="RrId"
                                Border-BorderWidth="1px" ParentFieldName="ParentId" AutoGenerateColumns="False"
                                Width="100%">
                                <Styles  >
                                </Styles>
                                <SettingsBehavior AutoExpandAllNodes="true" />
                                <Images >
                                    <CollapsedButton Height="11px" Width="11px" Url="~/App_Themes/Glass/TreeList/CollapsedButton.png">
                                    </CollapsedButton>
                                    <ExpandedButton Height="11px" Width="11px" Url="~/App_Themes/Glass/TreeList/ExpandedButton.png">
                                    </ExpandedButton>
                                    <CustomizationWindowClose Height="17px" Width="17px"></CustomizationWindowClose>
                                </Images>
                                <Columns>
                                    <dxwtl:TreeListTextColumn Caption="اطلاعات ارجاع" VisibleIndex="0">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <DataCellTemplate>
                                            <div align="right">
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblRecieverInfo" runat="server" Text='<%# Eval("RecieverName") %>'
                                                                Font-Bold="true" Font-Size="9pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="5%">
                                                        </td>
                                                        <td width="95%">
                                                            <asp:Label ID="lblReferenceDescription" runat="server" Font-Size="8pt" ForeColor="DarkBlue"
                                                                Text='<%# (String.IsNullOrEmpty(Eval("ReferenceDescription").ToString().Trim()))?"":"<br><table><tr><td style=\"vertical-align:top\" align=right>شرح ارجاع : </td><td align=right>"+Eval("ReferenceDescription")+"</td></tr></table><br>" %>'></asp:Label>
                                                            <asp:Label ID="lblActionDescription" runat="server" Text='<%# (String.IsNullOrEmpty(Eval("ActionDescription").ToString().Trim()))?"":"<br><table><tr><td style=\"vertical-align:top\" align=right>شرح اقدام : </td><td align=right>"+Eval("ActionDescription")+"</td></tr></table><br>" %>'
                                                                Font-Size="8pt" ForeColor="DarkGreen"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </DataCellTemplate>
                                    </dxwtl:TreeListTextColumn>
                                </Columns>
                                <Settings ShowColumnHeaders="false" />
                            </dxwtl:ASPxTreeList>
                            <asp:ObjectDataSource ID="ObjectDataSourceLetterReferenceTrace" runat="server" SelectMethod="SelectAutomationLetterReferenceTrace"
                                TypeName="TSP.DataManager.Automation.LetterReferencesManager">
                                <SelectParameters>
                                    <asp:Parameter Name="LetterId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <br />
                        </div>
                    </dxw:contentcontrol>
                </ContentCollection>
            </dxtc:TabPage>
        </TabPages>
        <Paddings PaddingLeft="0px" />
        <TabStyle HorizontalAlign="Center">
        </TabStyle>
        <ContentStyle>
            <Border BorderColor="#4986A2" />
        </ContentStyle>
    </dxtc:ASPxPageControl>
</asp:Content>
