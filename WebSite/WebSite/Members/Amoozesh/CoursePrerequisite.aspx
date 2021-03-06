<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CoursePrerequisite.aspx.cs" Inherits="Members_Amoozesh_CoursePrerequisite"
    Title="پیشنیاز دروس" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                            <Image Url="~/Images/icons/Back.png"></Image>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetail" runat="server" OnItemClick="MenuCourseDetail_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Course" Text="مشخصات درس">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Refrences" Text="منابع درس">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه بندی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>
            <TSPControls:CustomAspxDevGridView ID="GridViewPrerequisite" runat="server" Width="100%"
                AutoGenerateColumns="False" DataSourceID="ObjdsPrerequisite" ClientInstanceName="GridViewPrerequisite" KeyFieldName="PrerequisiteId">

                <Columns>
                    <dxwgv:GridViewDataComboBoxColumn Caption="پیشنیاز" FieldName="ParentCrsId" VisibleIndex="0"
                        Width="300px">
                        <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                        <PropertiesComboBox DataSourceID="ObjdsCourse" TextField="CrsName" ValueField="CrsId"
                            ValueType="System.String" Width="150px">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="Description" VisibleIndex="1"
                        Width="350px">
                        <PropertiesMemoEdit Height="70px" Width="300px"></PropertiesMemoEdit>
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " Name=" " VisibleIndex="2" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
             <asp:ObjectDataSource ID="ObjdsPrerequisite" runat="server" TypeName="TSP.DataManager.CoursePrerequisiteManager" SelectMethod="SelectByCourseId">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="-1" Name="CourseId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="ObjdsCourse" runat="server" TypeName="TSP.DataManager.CourseManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False" OnClick="btnBack_Click">
                                            <Image Url="~/Images/icons/Back.png"></Image>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="CourseId" runat="server" Visible="False" />
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
