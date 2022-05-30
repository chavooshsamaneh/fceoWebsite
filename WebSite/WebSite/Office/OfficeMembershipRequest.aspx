<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="OfficeMembershipRequest.aspx.cs" Inherits="Office_OfficeMembershipRequest"
    Title="درخواست های عضويت" %>

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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>

                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                    ToolTip="درخواست تغییرات"  Visible="false">
                                                                    <ClientSideEvents Click="function(s, e) {
	
	
	
}" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
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
                <ul class="HelpUL">
                    <li>جهت ویرایش اطلاعات شرکت خود ابتدا درخواست مورد نظر را انتخاب نموده سپس بر روی دکمه
                        ''ویرایش''
                        ( <asp:Image ImageUrl="~/Images/icons/edit.png" Width="25px" runat="server" /> )
                        واقع در منوی بالا و یا منوی پایین کلیک نمایید.</li>

                          <li>پس از تکمیل اطلاعات خود جهت بررسی پرونده توسط سازمان برروی دکمه ''گردش کار'' 
                        ( <asp:Image ID="Image1" ImageUrl="~/Images/icons/reload.png" Width="25px" runat="server" /> )
                         کلیک نمایید.سپس در پنجره باز شده برروی دکمه ارسال کلیک نمایید.
                        </li>
                </ul>
                </div>
                <TSPControls:CustomAspxDevGridView ID="GridViewOffice" runat="server" DataSourceID="ObjectDataSourceOffice"
                    KeyFieldName="OfId" EnableViewState="False" ClientInstanceName="grid" Width="100%"
                    OnHtmlDataCellPrepared="GridViewOffice_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewOffice_AutoFilterCellEditorInitialize">
                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="OfReId" Name="OfReId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد شرکت" FieldName="OfId" VisibleIndex="0"
                            Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                            VisibleIndex="0" Width="50px">
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
                        <dxwgv:GridViewDataTextColumn Caption="نام درخواست" FieldName="TaskName" VisibleIndex="1"
                            Width="200px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="CreateDate" VisibleIndex="2">
                            <HeaderStyle Wrap="False" />
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت تایید" FieldName="Confirm" VisibleIndex="3">
                            <HeaderStyle Wrap="False" />
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeName" VisibleIndex="4"
                            Visible="false">
                            <HeaderStyle Wrap="False" />
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" VisibleIndex="5">
                            <HeaderStyle Wrap="False" />
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="6">
                            <HeaderStyle Wrap="False" />
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="7">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت ارسال کننده" FieldName="WFRequesterType"
                            VisibleIndex="8">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره نامه" FieldName="LetterNo" Visible="False"
                            VisibleIndex="9">
                            <HeaderStyle Wrap="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ نامه" FieldName="LetterDate" Visible="False"
                            VisibleIndex="10">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="11" Width="30px" ShowClearFilterButton="true">
                         
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true" />
                </TSPControls:CustomAspxDevGridView>
                <br />
                      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                    ToolTip="درخواست تغییرات"  Visible="false">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                <asp:ObjectDataSource ID="ObjectDataSourceOffice" runat="server" FilterExpression="OfId={0}"
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
                    HeaderText=""  Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="387px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <div dir="rtl">
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
                                                                            ForeColor="Red" Visible="False">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" valign="top">
                                                                        <dxe:ASPxLabel runat="server" Text="ارسال به مرحله:" Font-Size="X-Small" ID="lblSenBack">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td align="right" dir="ltr">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="230px" 
                                                                            ID="cmbSendBackTask"  ValueType="System.String" >
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
                                                                        <dxe:ASPxLabel runat="server" Text="توضیحات:" Font-Size="X-Small" Width="56px" ID="ASPxLabel7">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 600px; height: 37px" align="right" dir="rtl">
                                                                        <TSPControls:CustomASPXMemo runat="server" Height="71px"  Width="100%" ID="txtDescription"
                                                                            ClientInstanceName="TextDesc" >
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
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="ارسال"  Width="93px" ID="btnSendNextWorkStep"
                                                                            AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                                            >
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
                                                                ID="lblInstitueWarning" ForeColor="Red">
                                                            </dxe:ASPxLabel>
                                                            <br />
                                                            <br />
                                                            <TSPControls:CustomAspxButton  runat="server" Text="خروج"  Width="93px" ID="btnClose"
                                                                AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                                >
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
                            </div>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px" />
                    <CloseButtonImage Height="17px" Width="17px" />
                </TSPControls:CustomASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
