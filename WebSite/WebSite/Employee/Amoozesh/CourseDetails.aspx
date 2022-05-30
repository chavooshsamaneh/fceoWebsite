<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CourseDetails.aspx.cs" Inherits="Employee_Amoozesh_CourseDetails"
    Title="واحد های درسی" %>

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
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>





                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  EnableTheming="False" ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                                            <Image  Url="~/Images/icons/view.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"  EnableTheming="False" ToolTip="حذف" ID="btnDelete" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png"></Image>
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

            <br />

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelCourseDetail" HeaderText="نام واحد" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="100%"
                            DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" KeyFieldName="CdId">
                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>


                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="CdId" Name="CdId" Caption="CdId" Visible="False"
                                    VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CrsTitle" Name="CrsTitle" Caption="نام واحد"
                                    VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="MjId" Caption="نام رشته" VisibleIndex="1">
                                    <PropertiesComboBox DataSourceID="OdbMajor" TextField="MjName" ValueField="MjId"
                                        ValueType="System.String">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="GrdId" Caption="عنوان پایه" VisibleIndex="2">
                                    <PropertiesComboBox DataSourceID="OdbGrade" TextField="GrdName" ValueField="GrdId"
                                        ValueType="System.String">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="FiId" Caption="صلاحیت" VisibleIndex="3">
                                    <PropertiesComboBox DataSourceID="OdbField" TextField="FiName" ValueField="FiId"
                                        ValueType="System.String">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewCommandColumn Caption="عملیات" VisibleIndex="4" ShowClearFilterButton="true">
                                    
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                            <Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>
                        </TSPControls:CustomAspxDevGridView>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <asp:HiddenField ID="CourseId" runat="server" Visible="False"></asp:HiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click">
                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click">
                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  EnableTheming="False" ToolTip="مشاهده" ID="btnView2" EnableViewState="False" OnClick="btnView_Click">
                                                            <Image  Url="~/Images/icons/view.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"  EnableTheming="False" ToolTip="حذف" ID="btnDelete2" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 27px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False"  EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png"></Image>
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

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OdbMajor" runat="server"
        SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbGrade" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbField" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.FieldManager"></asp:ObjectDataSource>

    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.CourseDetailManager" FilterExpression="CrsId={0}">
        <FilterParameters>
            <asp:Parameter Name="CrsId" />
        </FilterParameters>
    </asp:ObjectDataSource>

</asp:Content>
