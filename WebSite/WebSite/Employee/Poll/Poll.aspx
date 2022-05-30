<%@ Page Title="مدیریت نظرسنجی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Poll.aspx.cs" Inherits="Employee_Poll_Poll" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tr>
                                <td align="right">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                                   
                                                    <Image   Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                     ID="btnEdit" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewPoll.GetFocusedRowIndex()&lt;0)
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
                                                      
                                                        <Image  Url="~/Images/icons/view.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                </td>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                    ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnDelete_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewPoll.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                               <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                </td>

                                                   <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReportGrid" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" Text=" " ToolTip="نتایج نظرسنجی" UseSubmitBehavior="False" OnClick="btnReportGrid_Click">
                                                       
                                                        <Image  Url="~/Images/Statistics-32.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            </br>
            <TSPControls:CustomAspxDevGridView ID="GridViewPoll" KeyFieldName="PollId" runat="server"
                DataSourceID="ObjdsbserverPollPoll" Width="100%" ClientInstanceName="GridViewPoll">
                <Settings ShowHorizontalScrollBar="true" />
                <SettingsCookies Enabled="false" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="موضوع" FieldName="Tittle" VisibleIndex="1"
                        Width="300px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" VisibleIndex="3"
                        Width="120px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ نمایش" FieldName="StartShowDate" VisibleIndex="3"
                        Width="120px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار" FieldName="ValidDate" VisibleIndex="3"
                        Width="120px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                   
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="4"
                        Width="100px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ غیر فعال" FieldName="InActiveDate" VisibleIndex="5"
                        Width="120px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="سطح دسترسی نتایج" FieldName="IsResultPublicName"
                        VisibleIndex="2" Width="150px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع نظر سنجی" FieldName="QuestionCountTypeName"
                        VisibleIndex="2" Width="150px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            </br>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tr>
                                <td align="right">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                                    
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                     ID="btnEdit2" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewPoll.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 }"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False" OnClick="btnView_Click">
                                                        
                                                        <Image  Url="~/Images/icons/view.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                </td>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                    ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnDelete_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewPoll.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>  <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                                </td>

                                                   <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReportGrid2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" Text=" " ToolTip="نتایج نظرسنجی" UseSubmitBehavior="False" OnClick="btnReportGrid_Click">
                                                      
                                                        <Image Url="~/Images/Statistics-32.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsbserverPollPoll" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.PollPollManager"></asp:ObjectDataSource>
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
