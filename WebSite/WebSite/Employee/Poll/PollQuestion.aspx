<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PollQuestion.aspx.cs" Inherits="Employee_Poll_PollQuestion" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                        
                                        <image url="~/Images/icons/new.png">
                                        </image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                          ID="btnEdit" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewPollQuestion.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 }"></ClientSideEvents>
                                        
                                        <image url="~/Images/icons/edit.png">
                                        </image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False" OnClick="btnView_Click">
                                            
                                            <image   url="~/Images/icons/view.png"   />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                        ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnDelete_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewPollQuestion.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                        
                                        <image url="~/Images/icons/delete.png">
                                        </image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                </td>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                        
                                        <image   url="~/Images/icons/back.png"   />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuDetail" runat="server"
                OnItemClick="MenuDetail_ItemClick">
                <Items>
                    <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" Selected="false">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوالات" Name="Questions" Selected="true">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            </br>
            <TSPControls:CustomAspxDevGridView ID="GridViewPollQuestion" KeyFieldName="QuestionId"
                runat="server" DataSourceID="ObjdsbserverPollQuestion" Width="100%" ClientInstanceName="GridViewPollQuestion">
                <Settings ShowHorizontalScrollBar="true" />
                <SettingsCookies Enabled="false" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="عنوان سوال" FieldName="Question" VisibleIndex="1"
                        Width="600px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            </br>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                        <table  >
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                        
                                        <image url="~/Images/icons/new.png">
                                        </image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                          ID="btnEdit2" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewPollQuestion.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 }"></ClientSideEvents>
                                        
                                        <image url="~/Images/icons/edit.png">
                                        </image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False" OnClick="btnView_Click">
                                       
                                        <image   url="~/Images/icons/view.png"   />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                        ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnDelete_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (GridViewPollQuestion.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                        
                                        <image url="~/Images/icons/delete.png">
                                        </image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                       
                                        <image   url="~/Images/icons/back.png"   />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsbserverPollQuestion" runat="server" SelectMethod="FindByPollId"
                TypeName="TSP.DataManager.PollQuestionManager">
                <SelectParameters>
                    <asp:Parameter Name="PollId" DefaultValue="-1" Type="Int32" />
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
