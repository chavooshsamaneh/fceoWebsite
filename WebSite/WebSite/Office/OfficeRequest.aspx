<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="OfficeRequest.aspx.cs" Inherits="Office_OfficeRequest" Title="درخواست های پروانه شخص حقوقی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>



<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                    ToolTip="درخواست صدور پروانه"  Visible="false">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Write Document.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                    ToolTip="ویرایش">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                    ToolTip="مشاهده">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnRevival_Click" Text=" "
                                    ToolTip="درخواست تمدید پروانه" Visible="false">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnChange_Click" Text=" "
                                    ToolTip="درخواست تغییرات پروانه"  Visible="false">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReduplicate" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnReduplicate_Click"
                                    Text=" " ToolTip="درخواست المثنی"  Visible="false">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Copy2.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                    ToolTip="لغو درخواست"  Visible="false">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به لغو کردن این درخواست هستید؟');
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
    TextDesc.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                    ToolTip="پیگیری گردش کار" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                           
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
        DataSourceID="ObjectDataSource1"
        KeyFieldName="OfId" EnableViewState="False" ClientInstanceName="grid" Width="100%"
        OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="OfReId" Name="OfReId" Visible="False" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد شرکت" FieldName="OfId" VisibleIndex="0"
                Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" VisibleIndex="6"
                Visible="false">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="MFNo" VisibleIndex="4"
                Width="150px">
                <%-- <headerstyle wrap="True" />--%>
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع پروانه" FieldName="MFTypeName" VisibleIndex="5">
                <HeaderStyle Wrap="True" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور" FieldName="RegDate" VisibleIndex="7"
                Visible="false">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="ExpireDate"
                VisibleIndex="8" Visible="false">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="1">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeName" VisibleIndex="0">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" VisibleIndex="8">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت تایید" FieldName="Confirm" VisibleIndex="10"
                Visible="false">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="2">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت درخواست" FieldName="TaskName" VisibleIndex="11"
                Visible="false">
                <%-- <headerstyle wrap="True" />--%>
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="8">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کدپیگیری" FieldName="FollowCode" VisibleIndex="9">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                VisibleIndex="9">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره نامه" FieldName="LetterNo" Visible="False"
                VisibleIndex="6">
                <HeaderStyle Wrap="False" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ نامه" FieldName="LetterDate" Visible="False"
                VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="true" ShowHorizontalScrollBar="true" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                    ToolTip="درخواست صدور پروانه"  Visible="false">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Write Document.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton5" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                    ToolTip="ویرایش">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                    ToolTip="مشاهده">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival2" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnRevival_Click" Text=" "
                                    ToolTip="درخواست تمدید پروانه"  Visible="false">
                                    <ClientSideEvents Click="function(s, e) {
	
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange2" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnChange_Click" Text=" "
                                    ToolTip="درخواست تغییرات پروانه"  Visible="false">
                                    <ClientSideEvents Click="function(s, e) {
	
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReduplicate2" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnReduplicate_Click"
                                    Text=" " ToolTip="درخواست المثنی"  Visible="false">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Copy2.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton4" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                    ToolTip="لغو درخواست"  Visible="false">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به لغو کردن این درخواست هستید؟');
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendToNexStep" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
TextDesc.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                    ToolTip="پیگیری گردش کار" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" FilterExpression="OfId={0}"
        SelectMethod="FindByOfficeId" TypeName="TSP.DataManager.OfficeRequestManager"
        OldValuesParameterFormatString="original_{0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="Type" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" AllowDragging="True" ClientInstanceName="PopupWorkFlow"
        CloseAction="CloseButton"
        HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" Width="387px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">

                <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelWorkFlow" runat="server" ClientInstanceName="CallbackPanelWorkFlow"
                    OnCallback="CallbackPanelWorkFlow_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxp:ASPxPanel ID="PanelMain" runat="server" ClientInstanceName="PanelMain" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">
                                                        <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                            ForeColor="Red" Visible="False" __designer:wfdid="w34">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="ارسال به مرحله:" Font-Size="X-Small" ID="lblSenBack"
                                                            __designer:wfdid="w35">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" dir="ltr">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="230px"
                                                            ID="cmbSendBackTask" ValueType="System.String"
                                                            __designer:wfdid="w36">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 159px; height: 37px" align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات:" Font-Size="X-Small" Width="56px" ID="ASPxLabel7"
                                                            __designer:wfdid="w37">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 600px; height: 37px" align="right" dir="rtl">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="71px" Width="100%" ID="txtDescription"
                                                            ClientInstanceName="TextDesc"
                                                            __designer:wfdid="w38">
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 37px" align="center" dir="ltr" colspan="2">
                                                        <TSPControls:CustomAspxButton runat="server" Text="ارسال" Width="93px" ID="btnSendNextWorkStep"
                                                            AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep">
                                                            <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	grid.PerformCallback('');
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                            <dxp:ASPxPanel ID="PanelSaveSuccessfully" runat="server" ClientInstanceName="PanelSaveSuccessfully"
                                Height="100%" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <div align="center">
                                            <br />
                                            <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small"
                                                ID="lblInstitueWarning" ForeColor="Red" __designer:wfdid="w41">
                                            </dxe:ASPxLabel>
                                            <br />
                                            <br />
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="خروج" Width="93px" ID="btnClose"
                                                AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                __designer:wfdid="w42">
                                                <ClientSideEvents Click="function(s, e) {	
	//CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Hide();
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomAspxCallbackPanel>

            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle>
            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
        </HeaderStyle>
        <SizeGripImage Height="12px" Width="12px" />
        <CloseButtonImage Height="17px" Width="17px" />
    </TSPControls:CustomASPxPopupControl>

</asp:Content>
