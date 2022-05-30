<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="Help.aspx.cs" Inherits="Help_Help" Title="راهنماي سايت" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxti" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxsm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <script language="javascript" type="text/javascript">
    var HeaderText='';
    
function Load_Data(Data)
{
  if(Data[1]!=''){
    tab.GetTab(1).SetVisible(true);
	tab.SetActiveTab(tab.GetTab(1));
	HeaderText=Data[0];
	lblHeader.SetText(Data[0]);
	window.frames[0].location.href='DownloadHelp.aspx?Id='+Data[2];
	window.frames[1].location.href='HtmlFiles/'+Data[1];
  }
}

function GetTabHeader()
{
  return HeaderText;
}

function OnMouseOverImageCloseTab(e)
{
    e.style.cursor='hand';
}
    </script>


        <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
              Width="100%" ClientInstanceName="tab">
            <ContentStyle>
                <Border BorderColor="#7EACB1" BorderStyle="Solid" BorderWidth="1px"></Border>
            </ContentStyle>
            <ClientSideEvents ActiveTabChanged="function(s,e){lblHeaderInActive.SetText(GetTabHeader());}" />
            <TabPages>
                <dxtc:TabPage Name="GridHelp" Text="فهرست">
                    <ContentCollection>
                        <dxw:ContentControl runat="server">
                            <div align="right" dir="rtl">
                                <dxwtl:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False"
                                    DataSourceID="XmlDataSource1" ClientInstanceName="tree" KeyFieldName="HelpId">
                                    <columns>
                                        <dxwtl:TreeListDataColumn FieldName="Title" Caption="فهرست" VisibleIndex="0" AllowSort="False" />
                                    </columns>
                                    <settings showcolumnheaders="False" showtreelines="False" />
                                    <settingsbehavior autoexpandallnodes="True" />
                                    <clientsideevents nodeclick="function(s,e){tree.GetNodeValues(e.nodeKey,'Title;FileUrl;HelpId',Load_Data);}" />
                                    <templates>
                                        <DataCell>
                                            <dxe:ASPxHyperLink ID="lnkIndex" runat="server" Text='<%# Bind("Title") %>'
                                                NavigateUrl='<%# Bind("FakeUrl") %>' ToolTip='<%# Bind("Enabled") %>' Font-Names="Tahoma"
                                                Font-Size="10pt" Font-Underline="False" OnDataBound="lnkIndex_DataBound">
                                                <DisabledStyle ForeColor="Chocolate">
                                                </DisabledStyle>
                                            </dxe:ASPxHyperLink>
                                        </DataCell>
                                    </templates>
                                </dxwtl:ASPxTreeList>
                                <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/HelpFiles.xml"></asp:XmlDataSource>
                            </div>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="Steps" Text="راهنما" ClientVisible="false" >
                    <ContentCollection>
                        <dxw:ContentControl runat="server">
                            <iframe id="DownloadHelpFrame" marginwidth="0" marginheight="0" frameborder="0" noresize
                                scrolling="auto" runat="server" style="height: 50px; width: 100%"></iframe>
                            <iframe frameborder="0" scrolling="auto" style="height: 500px; width: 100%" runat="server"
                                id="HelpFrame" name="HelpFrame"></iframe>
                        </dxw:ContentControl>
                    </ContentCollection>
                    <ActiveTabTemplate>
                        <table width="250px">
                            <tr>
                                <td align="right" width="90%">
                                    <dxe:ASPxLabel ID="lblHeader" runat="server" Font-Names="tahoma" Font-Size="8pt"
                                        ClientInstanceName="lblHeader">
                                    </dxe:ASPxLabel>
                                </td>
                                <td width="5%">
                                    &nbsp;&nbsp;</td>
                                <td align="left" width="5%">
                                    <div id="DivImgCloseTab" onmouseover="OnMouseOverImageCloseTab(this)">
                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/Images/Stop.png" Width="16px"
                                            Height="16px" ToolTip="خروج">
                                            <ClientSideEvents Click="function(s,e){tab.GetTab(1).SetVisible(false);}" />
                                        </dxe:ASPxImage>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ActiveTabTemplate>
                    <TabStyle HorizontalAlign="Right">
                    </TabStyle>
                    <TabTemplate>
                        <dxe:ASPxLabel ID="lblHeaderInActive" Width="200px" runat="server" Font-Names="tahoma"
                            Font-Size="8pt" ClientInstanceName="lblHeaderInActive">
                        </dxe:ASPxLabel>
                    </TabTemplate>
                </dxtc:TabPage>
            </TabPages>
        </dxtc:ASPxPageControl>

</asp:Content>
