<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberExam.aspx.cs" Inherits="Members_Documents_MemberExam"
    Title="مدیریت آزمون های پذیرفته شده" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="جدید" ToolTip="جدید"
                                                ID="BtnNew" Width="25px" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="BtnNew_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                                Width="25px" ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="غیرفعال" ToolTip="غیرفعال"
                                                Width="25px" ID="btnInActive" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
else
{
 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="فعال" ToolTip="فعال"
                                                ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت" ToolTip="بازگشت"
                                                ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnBack_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروانه پروانه اشتغال به کار" ToolTip="مدیریت پروانه پروانه اشتغال به کار"
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
                        OnItemClick="MenuMemberFile_ItemClick" SeparatorWidth="1px" SeparatorHeight="100%"
                        SeparatorColor="#A5A6A8" ItemSpacing="0px"
                        AutoSeparators="RootOnly">
                        <Items>
                            <dxm:MenuItem Text="مشخصات پروانه" Name="Major" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="سابقه کار" Name="JobHistory" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="آزمون ها" Name="Exam" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="دوره آموزشی" Name="Periods" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پایه - صلاحیت" Name="MeDetail" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مدارک پیوست" Name="Attachment" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="ظرفیت اشتغال" Name="Capacity" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                            </dxm:MenuItem>
                        </Items>
                
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
                <br />
                <TSPControls:CustomAspxDevGridView2 runat="server" ID="GridViewMemberExam" DataSourceID="ObjdsExam"
                    KeyFieldName="MExmId" ClientInstanceName="GridViewMemberExam" OnHtmlRowPrepared="GridViewMemberExam_HtmlRowPrepared"
                    Width="100%">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Width="80px" VisibleIndex="0" FieldName="InActiveStatus" Caption="وضعیت">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Title" Caption="آزمون"
                            Width="250px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjName" Caption="رشته"
                            Width="200px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TTypeName" Width="80px"
                            Caption="نوع آزمون">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Width="80px"
                            Caption="پایه">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Point" Width="80px" Caption="نمره آزمون">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataHyperLinkColumn Width="150px" VisibleIndex="0" PropertiesHyperLinkEdit-Text="تصویر تاییدیه آزمون"
                            FieldName="ExamConfirmImageURL" Caption="تصویر تاییدیه آزمون" Name="ExamConfirmImageURL">
                        </dxwgv:GridViewDataHyperLinkColumn>

                        <dxwgv:GridViewDataHyperLinkColumn Width="150px" VisibleIndex="0" PropertiesHyperLinkEdit-Text="تصویر کارنامه"
                            FieldName="FileURL" Caption="تصویر کارنامه" Name="FileURL">
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataHyperLinkColumn Width="150px" VisibleIndex="0" PropertiesHyperLinkEdit-Text="تصویر دوره آموزشی"
                            FieldName="PeriodImgURL" Caption="تصویر دوره آموزشی" Name="PeriodImgURL">
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="UserCode" Caption="کد کاربری"
                            Width="150px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="EntranceCode" Caption="رمز عبور"
                            Width="150px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
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
                                                ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="BtnNew_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>

                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                                Width="25px" ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="غیرفعال" ToolTip="غیرفعال"
                                                Width="25px" ID="btnInActive2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
else
{
 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>

                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="فعال" ToolTip="فعال"
                                                ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (GridViewMemberExam.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>

                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت" ToolTip="بازگشت"
                                                ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnBack_Click">
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
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldExam">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjdsExam" runat="server" TypeName="TSP.DataManager.DocMemberExamDetailManager"
                    SelectMethod="SelectDocMemberExamDetailForManagmentPage" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MFId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
