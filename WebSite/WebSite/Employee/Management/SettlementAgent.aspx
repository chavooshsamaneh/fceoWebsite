<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="SettlementAgent.aspx.cs" Inherits="Employee_Employee_SettlementAgent"
    Title="مدیریت اعضاء مسکن" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
            <div id="DivReport" runat="server" class="DivErrors" dir="rtl" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False"
                                                            Width="25px">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive" runat="server" EnableClientSideAPI="True"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnDisActive_Click" Text=" "
                                                            ToolTip="غیرفعال" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
 }
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>

                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                            EnableTheming="False" EnableViewState="False"
                                                            OnClick="btnActive_Click" Text=" " ToolTip="فعال" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');

}" />
                                                            <Image Height="25px" Url="~/Images/icons/active.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6">
                                                        </TSPControls:MenuSeprator>
                                                    </td>


                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                            Visible="true" ToolTip="رمز یکبار عبور فعال" CausesValidation="False" ID="btnActiveTempPass"
                                                            EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 
if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن رمز یکبار عبور این ردیف هستید؟');
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/ActiveTempPass.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                            Visible="true" ToolTip="رمز یکبار عبور غیر فعال" CausesValidation="False" ID="btnInActiveTempPass"
                                                            EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 
if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن رمز یکبار عبوراین ردیف هستید؟');
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/InActiveTempPass.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>


                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                        </TSPControls:MenuSeprator>
                                                    </td>



                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                            CausesValidation="False" ID="btnReset2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnResetSave_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/ChangePassword.png">
                                                            </Image>
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
            <TSPControls:CustomAspxDevGridView ID="GridViewSettlementAgent" ClientInstanceName="GridViewSettlementAgent" runat="server" AutoGenerateColumns="False" DataSourceID="ObjdsSettlementAgent" Width="100%" KeyFieldName="StlAgentId">
                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="StlAgentId" Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="Name" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Family" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="Father" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="رمز یکبار عبور" FieldName="TempPass" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False"
                                                            Width="25px">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive2" runat="server" EnableClientSideAPI="True"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnDisActive_Click" Text=" "
                                                            ToolTip="غیرفعال" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
     e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
 }
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>


                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                            EnableTheming="False" EnableViewState="False"
                                                            OnClick="btnActive_Click" Text=" " ToolTip="فعال" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');

}" />
                                                            <Image Height="25px" Url="~/Images/icons/active.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                            Visible="true" ToolTip="رمز یکبار عبور فعال" CausesValidation="False" ID="btnActiveTempPass2"
                                                            EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن رمز یکبار عبور این ردیف هستید؟');
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/ActiveTempPass.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                            Visible="true" ToolTip="رمز یکبار عبور غیر فعال" CausesValidation="False" ID="btnInActiveTempPass2"
                                                            EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن رمز یکبار عبوراین ردیف هستید؟');
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/InActiveTempPass.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                            CausesValidation="False" ID="btnReset" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnResetSave_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSettlementAgent.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/ChangePassword.png">
                                                            </Image>
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
            <asp:ObjectDataSource ID="ObjdsSettlementAgent" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.SettlementAgentManager"></asp:ObjectDataSource>


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

</asp:Content>
