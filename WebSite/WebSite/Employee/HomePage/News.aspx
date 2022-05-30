<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="Employee_News_News"
    Title="مدیریت اخبار" %>

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

            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                    <table >
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" 
                                                    UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/view.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                             <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                                    ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnInActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewNews.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/disactive.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" Text=" " ToolTip="فعال"
                                                    ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewNews.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/active.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewNews" ClientInstanceName="GridViewNews" runat="server" AutoGenerateColumns="False"
                  DataSourceID="ObjectDataSourceNews"
                EnableViewState="False" KeyFieldName="NewsId" Width="100%" OnAutoFilterCellEditorInitialize="GridViewNews_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="GridViewNews_HtmlDataCellPrepared">

                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="NewsId" Name="NewsId" Visible="False" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="Title" VisibleIndex="1"
                        Width="200px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" VisibleIndex="2"
                        Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" Name="Date" VisibleIndex="3">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="موضوع" FieldName="SubjectId" VisibleIndex="4">
                        <PropertiesComboBox DataSourceID="ObjectDataSourceSub" TextField="Name" ValueField="SubId"
                            ValueType="System.String">
                        </PropertiesComboBox>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                      <dxwgv:GridViewDataTextColumn Caption="اطلاعیه" FieldName="IsNotificationStatus" Name="IsNotificationStatus" VisibleIndex="3">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="درجه اهمیت" FieldName="Importance" VisibleIndex="5">
                        <PropertiesComboBox ValueType="System.String">
                            <Items>
                                <dxe:ListEditItem Value="0" Text="معمولی"></dxe:ListEditItem>
                                <dxe:ListEditItem Value="1" Text="مهم"></dxe:ListEditItem>
                                <dxe:ListEditItem Value="2" Text="بسیار مهم"></dxe:ListEditItem>
                            </Items>
                        </PropertiesComboBox>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار" FieldName="ExpireDate" VisibleIndex="6">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کاربر" FieldName="ModifiedUser" VisibleIndex="7"
                        Width="130px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="سطح دسترسی" FieldName="UserLoginTypeName" VisibleIndex="8"
                        Width="100px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="8"
                        Width="100px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                    <table>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                    
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" 
                                                    UseSubmitBehavior="False">
                                                   
                                                    <Image  Url="~/Images/icons/edit.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                   
                                                    <Image  Url="~/Images/icons/view.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                    
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                                <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                                    ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnInActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewNews.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/disactive.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" Text=" " ToolTip="فعال"
                                                    ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewNews.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/active.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ObjectDataSourceSub" runat="server"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.NewsSubjectManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceNews" runat="server" SelectMethod="SelectForManagmentPage"
        TypeName="TSP.DataManager.NewsManager">
        <SelectParameters>
            <asp:Parameter Name="AgentId" Type="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
