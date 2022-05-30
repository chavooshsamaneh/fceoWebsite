<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberJob.aspx.cs" Inherits="Members_MemberInfo_MemberJob"
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
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td  >
                                        <table  >
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="BtnNew_Click">
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                            <image url="~/Images/icons/new.png">
                                                                        </image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                            ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnEdit_Click">
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnView_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

 
}"></ClientSideEvents>
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                            <image url="~/Images/icons/view.png">
                                                                        </image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                                            ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnInActive_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
  else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');

 
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/disactive.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                            <image url="~/Images/icons/Back.png">
                                                                        </image>
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
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" CssClass="ProjectMainMenuHorizontal"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Activity" Text="فعالیت ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Language" Text="زبان ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="مشخصات عضو" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
         
                <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="100%"
                OnPageIndexChanged="CustomAspxDevGridView1_PageIndexChanged"
                AutoGenerateColumns="False" ClientInstanceName="jgrid" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
                OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
                <SettingsBehavior  ColumnResizeMode="Control"></SettingsBehavior>
              
                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JhId" Name="JhId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectName" Caption="نام پروژه"
                        Name="ProjectName">
                        <CellStyle Wrap="true"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Employer" Caption="کارفرما"
                        Name="Employer" Visible="false">
                        <CellStyle Wrap="true"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع پروژه"
                        Name="TypeName">
                        <CellStyle Wrap="true"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StartCorporateDate" Caption="تاریخ شروع همکاری"
                        Name="StartCorporateDate" Width="120px">
                        <CellStyle Wrap="False"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="EndCorporateDate" Caption="تاریخ پایان همکاری"
                        Name="EndCorporateDate" Visible="False">
                        <CellStyle Wrap="False"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Area" Caption="زیربنا"
                        Name="Area">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CorName" Caption="نحوه مشارکت"
                        Name="CorName" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="CreateDate" Caption="تاریخ ایجاد"
                        Name="CreateDate">
                        <CellStyle Wrap="False"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActiveName" Caption="وضعیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="TtName" Caption="اعلام"
                        Width="150px">
                        <CellStyle Wrap="true"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CitName" Caption="شهر">
                        <CellStyle Wrap="true"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="TableId" Visible="False" VisibleIndex="8">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
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
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                                    ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="BtnNew_Click">
                                                                  
                                                                    <image url="~/Images/icons/new.png">
                                                                        </image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                                    ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnEdit_Click">
                                                                  
                                                                    <image  url="~/Images/icons/edit.png"  />
                                                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnView_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                   
                                                                    <image url="~/Images/icons/view.png">
                                                                        </image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                                                    ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnInActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
 
}" />
                                                                   
                                                                    <image  url="~/Images/icons/disactive.png" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                                   
                                                                    <image url="~/Images/icons/Back.png">
                                                                        </image>
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
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" FilterExpression="MeId={0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.ProjectJobHistoryManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="MemberId" runat="server" />
    <asp:HiddenField ID="PgMode" runat="server" />
    <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="HDMode" runat="server" Visible="False" />

</asp:Content>
