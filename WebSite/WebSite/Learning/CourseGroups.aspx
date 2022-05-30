<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="CourseGroups.aspx.cs" Inherits="Members_Amoozesh_CourseGroups" Title="گروه بندی درس" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                    <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                    <image height="25px" url="~/Images/icons/Back.png" width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetails" runat="server" OnItemClick="MenuCourseDetails_ItemClick">
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
   
                <br />
  
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                Width="100%"
                KeyFieldName="CrsGrId" DataSourceID="OdbGrid" ClientInstanceName="grid">

                <Settings ShowHorizontalScrollBar="True" />

                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="نام گروه" FieldName="Name" VisibleIndex="0" Width="300px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="1" Width="300px">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
      
        <br />
          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False"
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                <hoverstyle backcolor="#FFE0C0">
                                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                            </hoverstyle>
                                                                <image height="25px" url="~/Images/icons/Back.png" width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        <asp:HiddenField ID="CourseId" runat="server" Visible="False" />   
    <asp:ObjectDataSource ID="OdbGrid" runat="server" FilterExpression="CrsId={0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.CourseGroupsManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>

</asp:Content>
