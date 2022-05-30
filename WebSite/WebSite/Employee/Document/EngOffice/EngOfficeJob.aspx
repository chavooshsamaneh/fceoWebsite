<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeJob.aspx.cs" Inherits="Employee_Document_EngOffice_EngOfficeJob"
    Title="سوابق کاری" %>

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
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnView_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/view.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" AutoPostBack="False" 
                                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../../Print.aspx&quot;);	
}" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                        </td>
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
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت دفاتر"
                                                                CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/BakToManagment.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                      
                         </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu> 
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  OnItemClick="ASPxMenu1_ItemClick" >
                        <Items>
                            <dxm:MenuItem Name="EngOffice" Text="مشخصات دفتر">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضای دفتر">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attach" Text="مستندات">
                            </dxm:MenuItem>
                        </Items>
                       
                    </TSPControls:CustomAspxMenuHorizontal>
        
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="100%"
                      ClientInstanceName="jgrid"
                    AutoGenerateColumns="False" KeyFieldName="JhId" DataSourceID="OdbGrid" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="JhId" Name="JhId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام پروژه" FieldName="ProjectName" Name="ProjectName"
                            Width="180px" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کارفرما" FieldName="Employer" Name="Employer"
                            Visible="False" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع پروژه" FieldName="TypeName" Name="TypeName"
                            Visible="false" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع همکاری" FieldName="StartCorporateDate"
                            Name="StartCorporateDate" VisibleIndex="5">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نحوه مشارکت" FieldName="CorName" Name="CorName"
                            Width="150px" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" Name="CreateDate"
                            VisibleIndex="8">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="9"
                            Width="70px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TableId" Visible="False" VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0"
                            Width="70px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TtName" VisibleIndex="7"
                            Width="150px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">
                         
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnView_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/view.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton3" runat="server" AutoPostBack="False" 
                                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../../Print.aspx&quot;);	
}" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت دفاتر"
                                                                CausesValidation="False" ID="ASPxButton4" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/BakToManagment.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                      
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindByMeRequest"
            TypeName="TSP.DataManager.ProjectJobHistoryManager" OldValuesParameterFormatString="original_{0}">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MReId" Type="Int32" />
                <asp:Parameter DefaultValue="1" Name="IsConfirm" Type="Int16" />
                <asp:Parameter DefaultValue="0" Name="Type" Type="Int16" />
                <asp:Parameter DefaultValue="-1" Name="TableType" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" />
        <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
        <asp:ObjectDataSource ID="OdbGrid" runat="server" SelectMethod="SelectEngOfficeMeJobHistoryByOfId"
            TypeName="TSP.DataManager.EngOfficeManager" OldValuesParameterFormatString="original_{0}">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
   
</asp:Content>
