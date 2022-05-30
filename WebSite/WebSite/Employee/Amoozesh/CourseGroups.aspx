<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CourseGroups.aspx.cs" Inherits="Employee_Amoozesh_CourseGroups"
    Title="گروه بندی درس" %>

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
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" style="width: 100%; text-align: center; display: block;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	pop.Show();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                CausesValidation="False" ID="btnDelete" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <div width="100%" align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetails" runat="server"  
                        SeparatorWidth="1px" SeparatorHeight="100%" SeparatorColor="#A5A6A8" OnItemClick="MenuCourseDetails_ItemClick"
                        ItemSpacing="0px"  AutoSeparators="RootOnly"
                        RightToLeft="True">
                        <Items>
                            <dxm:MenuItem Name="Course" Text="مشخصات درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseRefrence" Text="منابع درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه بندی" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel5" HeaderText="گروه ها" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                                RightToLeft="True" ID="DevGridViewGroups" DataSourceID="OdbGrid" KeyFieldName="CrsGrId"
                                AutoGenerateColumns="False" ClientInstanceName="grid" >
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TgrId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Name" Caption="نام گروه">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	pop.Show();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                CausesValidation="False" ID="btnDelete1" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {

if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"  
                     HeaderText="جدید" PopupVerticalAlign="WindowCenter"
                    PopupHorizontalAlign="WindowCenter" PopupElementID="btnAddGroup" Modal="True"
                    CloseAction="CloseButton" ClientInstanceName="pop" RightToLeft="True" Width="300px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <table>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxDevTreeList ID="TreeListTrainingGroups" runat="server" AutoGenerateColumns="False"
                                            DataSourceID="ObjectDataSourceTrainingGroups" KeyFieldName="TgrId" ParentFieldName="ParentId" Width="100%">
                                           
                                                <SettingsSelection Enabled="True"></SettingsSelection>
                                            <Columns>
                                                <dxwtl:TreeListTextColumn Caption="نام" FieldName="Name" VisibleIndex="0" Width="150px">
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="1" Width="150px">
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn FieldName="TgrId" Visible="False" VisibleIndex="2" Caption="">
                                                </dxwtl:TreeListTextColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevTreeList>
                                    </td>
                                </tr>
                                <tr>
                                    <td c align="center" valign="top">
                                        <br />
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnAdd" runat="server" CausesValidation="False" 
                                             OnClick="btnSaveGroup_Click" Text="ذخیره">
                                            <ClientSideEvents Click="function(s, e) {
	pop.Hide();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </TSPControls:CustomASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="CourseId" runat="server" Visible="False" />
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSourceTrainingGroups" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TrainingGroupsManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbGrid" runat="server" FilterExpression="CrsId={0}" SelectMethod="GetData"
            TypeName="TSP.DataManager.CourseGroupsManager">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
