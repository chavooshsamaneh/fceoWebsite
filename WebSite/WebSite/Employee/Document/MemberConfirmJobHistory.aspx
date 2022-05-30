<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberConfirmJobHistory.aspx.cs" Inherits="Employee_Document_MemberConfirmJobHistory" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <table >
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False" OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                        ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnInActive_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/delete.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
  
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                </td>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td style="vertical-align: top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت پروانه"
                                        CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="../../Images/icons/BakToManagment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div style="width: 100%" dir="rtl" align="right">
                <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server"  
                    OnItemClick="MenuMemberFile_ItemClick" >
                    <Items>
                        <dxm:MenuItem Text="مشخصات پروانه" Name="Major">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="آزمون ها" Name="Exam">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="دوره های آموزشی" Name="Periods">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="پایه - صلاحیت" Name="MeDetail">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مدارک پیوست" Name="Attachment">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="ظرفیت اشتغال" Name="Capacity">
                        </dxm:MenuItem>
                      
                    </Items>
                    
                </TSPControls:CustomAspxMenuHorizontal>

            </div>
            <br />
            <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewJobCon" KeyFieldName="JobConfId"
                runat="server" DataSourceID="ObjectDataSourceJobConfirm" Width="100%" ClientInstanceName="GridViewJobCon" OnHtmlRowPrepared="GridViewJobCon_HtmlRowPrepared">
                <Settings ShowHorizontalScrollBar="true" />
                <SettingsCookies Enabled="false" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JobConfId"
                        Name="JobConfId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MfId" Name="MfId">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InActives" Caption="وضعیت"
                        Name="Position">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Position" Caption="سمت"
                        Name="Position">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FromDate" Caption="از تاریخ"
                        Name="FromDate">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ToDate" Caption="تا تاریخ"
                        Name="ToDate">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="JobDateDiff" Caption="تعداد سال"
                                                    Name="JobDateDiff">
                                                </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ConfirmTypeName" Caption="نوع تایید کننده"
                        Name="ProjectName">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت شخص حقیقی"
                        Name="Employer">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Name" Caption="نام" Name="PrTypeName">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MFNo" Caption="شماره پروانه"
                        Name="StartCorporateDate">
                        <HeaderStyle Wrap="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="200" FieldName="Description"
                        Caption="توضیحات" Name="EndCorporateDate">
                        <HeaderStyle Wrap="True" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر فرم تاییدیه "
                        FieldName="FileURL" Caption="تصویر فرم تاییدیه" Name="FileURL">
                    </dxwgv:GridViewDataHyperLinkColumn>
                    <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="5" Width="130" PropertiesHyperLinkEdit-Text="تصویر پروانه/رتبه بندی "
                        FieldName="GrdURL" Caption="تصویر پروانه/رتبه بندی" Name="GrdURL">
                        <HeaderStyle Wrap="False" />
                        <CellStyle Wrap="False" />
                    </dxwgv:GridViewDataHyperLinkColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            </br>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit2" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False" OnClick="btnView_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                        ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnInActive_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/delete.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                               
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td style="vertical-align: top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت پروانه"
                                        CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="../../Images/icons/BakToManagment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceJobConfirm" runat="server" SelectMethod="FindByMfId"
                TypeName="TSP.DataManager.DocMemberFileJobConfirmationManager">
                <SelectParameters>
                    <asp:Parameter Name="MfId" DefaultValue="-1" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
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
