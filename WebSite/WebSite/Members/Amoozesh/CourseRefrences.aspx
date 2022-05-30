<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CourseRefrences.aspx.cs" Inherits="Members_Amoozesh_CourseRefrences"
    Title="منابع درس" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"
                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                            <image url="~/Images/icons/Back.png">
                                                                    </image>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                    </hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetail" runat="server"
                OnItemClick="MenuCourseDetail_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Course" Text="مشخصات درس">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="CourseRefrence" Text="منابع درس" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="پیشنیاز ها" Name="Prerequisite">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه بندی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <br />

            <TSPControls:CustomAspxDevGridView runat="server" ClientInstanceName="GridViewCourseRefrences"
                KeyFieldName="RefrenceId"
                AutoGenerateColumns="False" DataSourceID="ObjdsCourseRefrences" ID="GridViewCourseRefrences"
                Width="100%">
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="RefName" Width="200px" Caption="نام کتاب"
                        VisibleIndex="0">
                        <PropertiesTextEdit Width="200px">
                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="نام کتاب را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="RefAuthor" Width="200px" Caption="مولف"
                        VisibleIndex="1">
                        <PropertiesTextEdit Width="200px">
                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="نام مولف را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="RefTopics" Width="300px" Caption="سرفصل ها"
                        VisibleIndex="2">
                        <PropertiesMemoEdit Width="200px" Height="70px"></PropertiesMemoEdit>
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="Description" Width="300px" Caption="توضیحات"
                        VisibleIndex="4">
                        <PropertiesMemoEdit Width="200px" Height="70px"></PropertiesMemoEdit>
                    </dxwgv:GridViewDataMemoColumn>
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"
                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False"
                                            OnClick="btnBack_Click">
                                            <image url="~/Images/icons/Back.png">
                                                                    </image>
                                            <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                    </hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource runat="server" SelectMethod="SelectByCourseId" TypeName="TSP.DataManager.CourseRefrenceManager"
                ID="ObjdsCourseRefrences">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="CourseId" Type="Int32"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:HiddenField ID="CourseId" runat="server" Visible="False" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
