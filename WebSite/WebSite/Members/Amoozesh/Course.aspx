<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Members_Amoozesh_Course"
    Title="واحدهای درسی مورد تایید نظام مهندسی" %>

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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewCourse" Width="100%" runat="server"
                KeyFieldName="CrsId"
                DataSourceID="OdbCourse" AutoGenerateColumns="False" ClientInstanceName="grid">

                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="CrsId" Visible="False" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد " FieldName="CrsCode" VisibleIndex="0"
                        Width="60px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام " FieldName="CrsName" VisibleIndex="1"
                        Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="Point" VisibleIndex="2"
                        Width="40px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="مدت زمان اعتبار" FieldName="ValidDuration"
                        VisibleIndex="3" Width="60px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="مدت زمان دوره" FieldName="Duration" VisibleIndex="4"
                        Width="60px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Templates>
                    <DetailRow>
                    </DetailRow>
                    <GroupRow>
                        <TSPControls:CustomAspxDevGridView ID="GridViewCourseRefrences" runat="server" AutoGenerateColumns="False"
                            DataSourceID="ObjdsCourseRefrense"
                            Width="100%">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="نام کتاب" FieldName="RefName" VisibleIndex="0"
                                    Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="مولف" FieldName="RefAuthor" VisibleIndex="1"
                                    Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="2"
                                    Width="150px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="1px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="سرفصل ها" FieldName="RefTopics" VisibleIndex="4"
                                    Width="150px">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                    </GroupRow>
                </Templates>
                <SettingsDetail AllowOnlyOneMasterRowExpanded="True" />
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton  IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="مشاهده" ID="btnView2" EnableViewState="False"
                                            OnClick="btnView_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="OdbCourse" runat="server" TypeName="TSP.DataManager.CourseManager"
                SelectMethod="GetData" FilterExpression="InActive={0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
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
