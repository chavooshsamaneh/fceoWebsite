<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="CourseAttachments.aspx.cs" Inherits="Members_Amoozesh_CourseAttachments"
    Title="فایل های پیوست" %>

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
    <script language="javascript">

        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'FilePath;Description', SetValue);
        }
        function SetValue(values) {
            var d = values[0];
            if (d != null && d != '') {
                d = d.replace('~/', '');
                d = '../' + d;
            }

            img.SetImageUrl(d);

            desc.SetText(values[1]);

        }
    </script>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tr>
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
                                                </table>
                                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetail" runat="server"  OnItemClick="MenuCourseDetail_ItemClick" >
                        <Items>
                            <dxm:MenuItem Name="Course" Text="مشخصات درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Refrences" Text="منابع درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه بندی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attachment" Text="فایل های پیوست" Selected="true">
                            </dxm:MenuItem>
                        </Items>
                        
                    </TSPControls:CustomAspxMenuHorizontal>
                 
                <br />
           
                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" 
                         OnPageIndexChanged="CustomAspxDevGridView1_PageIndexChanged"
                        AutoGenerateColumns="False" KeyFieldName="AttachId" ClientInstanceName="grid"
                        Width="100%">
                    
                       
                        <Columns>
                         
                            <dxwgv:GridViewDataHyperLinkColumn Visible="False" VisibleIndex="0" FieldName="FilePath"
                                Caption="فایل" Name="FilePath" Width="150px">
                                <PropertiesHyperLinkEdit Target="_blank">
                                </PropertiesHyperLinkEdit>
                            </dxwgv:GridViewDataHyperLinkColumn>
                            <dxwgv:GridViewDataImageColumn Visible="False" VisibleIndex="0" FieldName="FilePath"
                                Caption="تصویر" Name="FilePath">
                                <PropertiesImage ImageWidth="75px" ImageHeight="75px">
                                </PropertiesImage>
                            </dxwgv:GridViewDataImageColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Caption="فایل">
                                <DataItemTemplate>
                                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" OnDataBinding="ASPxHyperLink1_DataBinding"
                                        NavigateUrl='<%# Bind("FilePath") %>' Target="_blank">
                                    </dxe:ASPxHyperLink>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Width="400px"
                                Caption="توضیحات" Name="Description">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="2" Width="30px" ShowClearFilterButton="true">
                           
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
                </div>
                <br />
                <asp:HiddenField ID="CourseId" runat="server" Visible="False"></asp:HiddenField>
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                <table >
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                         </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
 
</asp:Content>
