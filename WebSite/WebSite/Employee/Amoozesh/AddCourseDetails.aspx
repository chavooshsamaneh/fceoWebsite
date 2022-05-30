<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddCourseDetails.aspx.cs" Inherits="Employee_Amoozesh_AddCourseDetails"
    Title="ارتباط با پروانه" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="Content" runat="server" style="width: 100%" dir="ltr" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <div align="right"
                 width="100%">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetails" runat="server" OnItemClick="MenuCourseDetails_ItemClick">
                        <Items>
                            <dxm:MenuItem Name="Course" Text="مشخصات درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseRefrence" Text="منابع درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه بندی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelCourseDetail" HeaderText="نام درس"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                ID="CustomAspxDevGridViewGrade" DataSourceID="ObjectDataSourceGrid" KeyFieldName="TrGrId"
                                AutoGenerateColumns="False" ClientInstanceName="grid" Width="100%">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="TrGrId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="InActiveName" Caption="وضعیت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" Width="30px" ShowClearFilterButton="true">                                    
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                </div>
                <asp:HiddenField ID="CourseId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="DetailId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table  class="TableMenu">
                                <tbody>
                                    <tr>
                                       
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="OdbMajor" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                    SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager" UpdateMethod="Update">
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbGrade" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                    SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager" UpdateMethod="Update"
                    OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbField" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.FieldManager">
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceGrid" runat="server" SelectMethod="FindByPKCode"
                    TypeName="TSP.DataManager.TrainingAcceptedGradeManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="PkId" Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="Type" Type="Byte" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
