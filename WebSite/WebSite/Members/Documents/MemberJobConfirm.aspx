<%@ Page Title="مدیریت تایید کنندگان سابقه کار" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberJobConfirm.aspx.cs" Inherits="Members_Documents_MemberJobConfirm" %>

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
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="جدید" ToolTip="جدید"
                                    ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="BtnNew_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                    Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="غیر فعال" ToolTip="غیر فعال"
                                    ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnPrint" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" Text="چاپ" ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت" ToolTip="بازگشت"
                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBack_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروانه اشتغال به کار" ToolTip="مدیریت پروانه اشتغال به کار"
                                    CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <div style="width: 100%" dir="rtl" align="right">
        <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server"
            OnItemClick="MenuMemberFile_ItemClick" CssClass="ProjectMainMenuHorizontal">
            <Items>
                <dxm:MenuItem Text="مشخصات پروانه" Name="Major" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
                <dxm:MenuItem Text="سابقه کار" Name="JobHistory" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
                <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
                <dxm:MenuItem Text="آزمون ها" Name="Exam" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
                <dxm:MenuItem Text="دوره آموزشی" Name="Periods" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
                <dxm:MenuItem Text="پایه - صلاحیت" Name="MeDetail" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
                <dxm:MenuItem Text="مدارک پیوست" Name="Attachment"  ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
                <dxm:MenuItem Text="ظرفیت اشتغال" Name="Capacity" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                </dxm:MenuItem>
            </Items>
     
        </TSPControls:CustomAspxMenuHorizontal>
    </div>
    <br />
    <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
    <br />
    <TSPControls:CustomAspxDevGridView2 ID="GridViewJobCon" ClientInstanceName="GridViewJobCon" runat="server" DataSourceID="ObjectDataSourceJobConfirm"
        Width="100%" EnableCallBacks="True" KeyFieldName="JobConfId" AutoGenerateColumns="False">
        <Settings ShowHorizontalScrollBar="true"></Settings>
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
    </TSPControls:CustomAspxDevGridView2>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="جدید" ToolTip="جدید"
                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="BtnNew_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="غیر فعال" ToolTip="غیر فعال"
                                    ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewJobCon.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnPrint2" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" Text="چاپ" ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت" ToolTip="بازگشت"
                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBack_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروانه اشتغال به کار" ToolTip="مدیریت پروانه اشتغال به کار"
                                    CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="ObjectDataSourceJobConfirm" runat="server" SelectMethod="FindByMfId"
        TypeName="TSP.DataManager.DocMemberFileJobConfirmationManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-2" DbType="Int32" Name="MfId" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
