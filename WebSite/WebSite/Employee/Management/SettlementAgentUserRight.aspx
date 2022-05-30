﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="SettlementAgentUserRight.aspx.cs" Inherits="Employee_Management_SettlementAgentUserRight" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxsm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function SetStateNew(Node, ch) {

            //tree.ExpandNode(Node);
            if (Node < 0)
                Node = -Node;
            var ar = document.getElementsByName('n' + Node);

            for (i = 0; i < ar.length; i++) {


                ar.item(i).checked = ch;
                if (ch == true)
                    hnew.Set(ar.item(i).id, ch);
                else if (hnew.Contains(ar.item(i).id))
                    hnew.Remove(ar.item(i).id);

                var ar2 = document.getElementsByName(ar.item(i).id);

                for (j = 0; j < ar2.length; j++) {
                    ar2.item(j).checked = ch;
                    if (ch == true)
                        hnew.Set(ar2.item(j).id, ch);
                    else if (hnew.Contains(ar2.item(j).id))
                        hnew.Remove(ar2.item(j).id);

                }

            }
            if (ch == true)
                hnew.Set('n' + Node, ch);
            else if (hnew.Contains('n' + Node))
                hnew.Remove('n' + Node);
            //window.alert(tree.GetVisibleNodeKeys().length);
        }

        function SetStateEdit(Node, ch) {
            if (Node < 0)
                Node = -Node;
            var ar = document.getElementsByName('e' + Node);

            for (i = 0; i < ar.length; i++) {
                ar.item(i).checked = ch;
                if (ch == true)
                    hedit.Set(ar.item(i).id, ch);
                else if (hnew.Contains(ar.item(i).id))
                    hedit.Remove(ar.item(i).id);

                var ar2 = document.getElementsByName(ar.item(i).id);

                for (j = 0; j < ar2.length; j++) {
                    ar2.item(j).checked = ch;
                    if (ch == true)
                        hedit.Set(ar2.item(j).id, ch);
                    else if (hedit.Contains(ar2.item(j).id))
                        hedit.Remove(ar2.item(j).id);

                }
            }

            if (ch == true)
                hedit.Set('e' + Node, ch);
            else if (hedit.Contains('e' + Node))
                hedit.Remove('e' + Node);

        }
        function SetStateView(Node, ch) {
            if (Node < 0)
                Node = -Node;

            var ar = document.getElementsByName('v' + Node);

            for (i = 0; i < ar.length; i++) {

                ar.item(i).checked = ch;
                if (ch == true)
                    hview.Set(ar.item(i).id, ch);
                else if (hnew.Contains(ar.item(i).id))
                    hview.Remove(ar.item(i).id);

                var ar2 = document.getElementsByName(ar.item(i).id);

                for (j = 0; j < ar2.length; j++) {
                    ar2.item(j).checked = ch;
                    if (ch == true)
                        hview.Set(ar2.item(j).id, ch);
                    else if (hview.Contains(ar2.item(j).id))
                        hview.Remove(ar2.item(j).id);

                }

            }
            if (ch == true)
                hview.Set('v' + Node, ch);
            else if (hview.Contains('v' + Node))
                hview.Remove('v' + Node);
        }
        function SetStateDel(Node, ch) {
            if (Node < 0)
                Node = -Node;

            var ar = document.getElementsByName('d' + Node);

            for (i = 0; i < ar.length; i++) {

                ar.item(i).checked = ch;
                if (ch == true)
                    hdel.Set(ar.item(i).id, ch);
                else if (hnew.Contains(ar.item(i).id))
                    hdel.Remove(ar.item(i).id);

                var ar2 = document.getElementsByName(ar.item(i).id);

                for (j = 0; j < ar2.length; j++) {
                    ar2.item(j).checked = ch;
                    if (ch == true)
                        hdel.Set(ar2.item(j).id, ch);
                    else if (hdel.Contains(ar2.item(j).id))
                        hdel.Remove(ar2.item(j).id);

                }
            }
            if (ch == true)
                hdel.Set('d' + Node, ch);
            else if (hdel.Contains('d' + Node))
                hdel.Remove('d' + Node);
        }
        /*function SetAllNodes()
        {

        var n=tree.GetVisibleNodeKeys();
        for( i=0;i<n.length;i++)
        {
        if(hnew.Contains('n'+n[i]))
        { 

        document.getElementById(n[i]).checked=hnew.Get('n'+n[i]);
        }
        }*/

    </script> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" align="right" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                 
  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


                   
                                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" 
                        ItemImagePosition="Left"  SeparatorWidth="1px" SeparatorHeight="100%"
                        SeparatorColor="#A5A6A8" OnItemClick="ASPxMenu1_ItemClick" ItemSpacing="0px"
                        RightToLeft="True"  AutoSeparators="RootOnly" >
                        <Items>
                            <dxm:MenuItem Name="Pages" Text="صفحات" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Automation" Text="اتوماسیون اداری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Session" Text="جلسات">
                            </dxm:MenuItem>
                        </Items>
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-2" LastItemX="-1" LastItemY="-2"
                            X="-1" Y="-2" />
                        <Border BorderColor="#A5A6A8" BorderStyle="Solid" BorderWidth="1px" />
                        <VerticalPopOutImage Height="8px" Width="4px" />
                        <ItemStyle ImageSpacing="5px" PopOutImageSpacing="7px" VerticalAlign="Middle" />
                        <SubMenuItemStyle ImageSpacing="7px">
                        </SubMenuItemStyle>
                        <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1" />
                        <HorizontalPopOutImage Height="7px" Width="7px" />
                    </TSPControls:CustomAspxMenuHorizontal>
               
                <br />
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelTree" HeaderText="تعيين سطح دسترسی"  runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

   
                                    <dxwtl:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False"
                                        RightToLeft="True" Width="100%" DataSourceID="ObjectDataSourceTree" KeyFieldName="TtId"
                                        ParentFieldName="ParentId" OnNodeCollapsing="ASPxTreeList1_NodeCollapsing" OnNodeExpanding="ASPxTreeList1_NodeExpanding"
                                        ClientInstanceName="tree" 
                                        OnHtmlRowPrepared="ASPxTreeList1_HtmlRowPrepared">
                                     
                                        <SettingsBehavior AllowFocusedNode="True" />
                                        <Columns>
                                            <dxwtl:TreeListTextColumn FieldName="TtId" Name="TtId" Visible="False" VisibleIndex="0">
                                            </dxwtl:TreeListTextColumn>
                                            <dxwtl:TreeListTextColumn Caption="نام جدول" FieldName="TtName" VisibleIndex="0">
                                            </dxwtl:TreeListTextColumn>
                                            <dxwtl:TreeListTextColumn Caption="امکان ایجاد" Name="CanNew" VisibleIndex="1">
                                                <DataCellTemplate>
                                                    &nbsp;&nbsp;
                                                    <%--<%#String.IsNullOrEmpty(Eval("ParentId").ToString())?"style='visibility:hidden'":"" %>--%>
                                                    <input type="checkbox" name='n<%#Eval("ParentId")%>' id="n<%# Container.NodeKey %>"
                                                        <%# HDnew.Contains("n"+Container.NodeKey)?"checked=\"checked\"":"" %> onclick="SetStateNew('<%# Container.NodeKey %>',checked)" />&nbsp;
                                                </DataCellTemplate>
                                            </dxwtl:TreeListTextColumn>
                                            <dxwtl:TreeListTextColumn Caption="امکان ویرایش" Name="CanEdit" VisibleIndex="2">
                                                <DataCellTemplate>
                                                    <input type="checkbox" name='e<%#Eval("ParentId")%>' id="e<%# Container.NodeKey %>"
                                                        <%# HDedit.Contains("e"+Container.NodeKey)?"checked=\"checked\"":"" %> onclick="SetStateEdit('<%# Container.NodeKey %>',checked)" />&nbsp;
                                                </DataCellTemplate>
                                            </dxwtl:TreeListTextColumn>
                                            <dxwtl:TreeListTextColumn Caption="امکان مشاهده" Name="CanView" VisibleIndex="3">
                                                <DataCellTemplate>
                                                    <input type="checkbox" name='v<%#Eval("ParentId")%>' id="v<%# Container.NodeKey %>"
                                                        <%# HDview.Contains("v"+Container.NodeKey)?"checked=\"checked\"":"" %> onclick="SetStateView('<%# Container.NodeKey %>',checked)" />&nbsp;
                                                </DataCellTemplate>
                                            </dxwtl:TreeListTextColumn>
                                            <dxwtl:TreeListTextColumn Caption="امکان حذف" Name="CanDelete" VisibleIndex="4">
                                                <DataCellTemplate>
                                                    <input type="checkbox" name='d<%#Eval("ParentId")%>' id="d<%# Container.NodeKey %>"
                                                        <%# HDdelete.Contains("d"+Container.NodeKey)?"checked=\"checked\"":"" %> onclick="SetStateDel('<%# Container.NodeKey %>',checked)" />&nbsp;
                                                </DataCellTemplate>
                                            </dxwtl:TreeListTextColumn>
                                        </Columns>
                                        <SettingsCookies StoreExpandedNodes="True" StorePaging="True" />
                                        <Images >
                                            <LoadingPanel Url="~/App_Themes/Glass/TreeList/Loading.gif">
                                            </LoadingPanel>
                                        </Images>
                                        <Styles  >
                                        </Styles>
                                        <ClientSideEvents FocusedNodeChanged="function(s, e) {
	//if(s.GetNodeState(s.GetFocusedNodeKey())=='Collapsed')
	  tree.ExpandNode(s.GetFocusedNodeKey());
    //else
	 // tree.CollapseNode(s.GetFocusedNodeKey());
}" />
                                    </dxwtl:ASPxTreeList>
                                     </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <br />
               
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView2" runat="server" AutoGenerateColumns="False"
                      DataSourceID="ObjectDataSource1"
                    EnableViewState="False" KeyFieldName="TtId" Visible="False">
                    <Templates>
                        <DetailRow>
                            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView2" runat="server" AutoGenerateColumns="False"
                                  DataSourceID="ObjectDataSource2"
                                KeyFieldName="TtId" OnBeforePerformDataSelect="CustomAspxDevGridView2_BeforePerformDataSelect"
                                Width="100%" EnableViewState="False">
                                
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="نام جدول" FieldName="TtName" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="امکان ایجاد" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox4" runat="server" __designer:wfdid="w59">
                                            </TSPControls:CustomASPxCheckBox>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="امکان ویرایش" VisibleIndex="2">
                                        <DataItemTemplate>
                                            <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox4" runat="server" __designer:wfdid="w60">
                                            </TSPControls:CustomASPxCheckBox>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="امکان مشاهده" VisibleIndex="3">
                                        <DataItemTemplate>
                                            <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox4" runat="server" __designer:wfdid="w61">
                                            </TSPControls:CustomASPxCheckBox>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="امکان حذف" VisibleIndex="4">
                                        <DataItemTemplate>
                                            <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox4" runat="server" __designer:wfdid="w62">
                                            </TSPControls:CustomASPxCheckBox>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                               
                                <SettingsDetail IsDetailGrid="True" />
                            </TSPControls:CustomAspxDevGridView>
                        </DetailRow>
                    </Templates>
                     
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="نام جدول" FieldName="TtName" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امکان ایجاد" VisibleIndex="1">
                            <DataItemTemplate>
                                <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox3" runat="server" __designer:wfdid="w64">
                                </TSPControls:CustomASPxCheckBox>
                                &nbsp;
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امکان ویرایش" VisibleIndex="2">
                            <DataItemTemplate>
                                <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox3" runat="server" __designer:wfdid="w55">
                                </TSPControls:CustomASPxCheckBox>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امکان مشاهده" VisibleIndex="3">
                            <DataItemTemplate>
                                <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox3" runat="server" __designer:wfdid="w56">
                                </TSPControls:CustomASPxCheckBox>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امکان حذف" VisibleIndex="4">
                            <DataItemTemplate>
                                <TSPControls:CustomASPxCheckBox ID="ASPxCheckBox3" runat="server" __designer:wfdid="w57">
                                </TSPControls:CustomASPxCheckBox>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                     
                </TSPControls:CustomAspxDevGridView>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                               </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnableCaching="True"
                    FilterExpression="TtId<>1" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectUserRightByParentId"
                    TypeName="TSP.DataManager.UserRightManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="ParentId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnableCaching="True"
                    FilterExpression="TtId<>1" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectUserRightByParentId"
                    TypeName="TSP.DataManager.UserRightManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="ParentId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceTree" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="TSP.DataManager.UserRightManager"></asp:ObjectDataSource>
                <dxhf:ASPxHiddenField ID="HDnew" runat="server" ClientInstanceName="hnew">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDedit" runat="server" ClientInstanceName="hedit">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDview" runat="server" ClientInstanceName="hview">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDdelete" runat="server" ClientInstanceName="hdel">
                </dxhf:ASPxHiddenField>
                <asp:HiddenField ID="HDUserId" runat="server" Visible="False" />
                <asp:HiddenField ID="HDEmpId" runat="server" Visible="False" />
            </ContentTemplate>
        </asp:UpdatePanel>
 
</asp:Content>

