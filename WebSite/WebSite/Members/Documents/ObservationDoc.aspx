<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ObservationDoc.aspx.cs" Inherits="Members_Documents_ObservationDoc"
    Title="مدیریت مجوز ناظر حقیقی _ به تنهایی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table width="100%" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="BtnNew_Click" Visible="false">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnEdit_Click" Visible="false">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                            Width="25px" ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnView_Click">
                                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
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
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                                            Width="25px" ID="btnDelete" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnDelete_Click" Visible="false">
                                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/delete.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                                            CausesValidation="False" Width="25px" ID="btnReDuplicate" AutoPostBack="False"
                                                            UseSubmitBehavior="False" Visible="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnReDuplicate_Click" >
                                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/copy.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                                            CausesValidation="False" Width="25px" ID="btnRevival" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnRevival_Click" Visible="false">
                                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Revival.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ارسال به مرحله بعد"
                                                            ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" Visible="false">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	txtDescription.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/reload.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                                            CausesValidation="False" ID="btnTracing" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnTracing_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint" runat="server" AutoPostBack="False" 
                                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                            ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnExportExcel_Click">
                                                            <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/ExportExcel.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td align="left" style="width: 100%">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False" 
                                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                            Visible="true" AutoPostBack="false">
                                                            <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                                            </ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="GridViewMemberFile" runat="server" DataSourceID="ObjdsMemberFile"
                          OnCustomCallback="GridViewMemberFile_CustomCallback"
                        ClientInstanceName="GridViewMemberFile" AutoGenerateColumns="False" KeyFieldName="MfId"
                        OnAutoFilterCellEditorInitialize="GridViewMemberFile_AutoFilterCellEditorInitialize"
                        OnHtmlDataCellPrepared="GridViewMemberFile_HtmlDataCellPrepared" Width="100%"
                        OnDetailRowExpandedChanged="GridViewMemberFile_DetailRowExpandedChanged" OnFocusedRowChanged="GridViewMemberFile_FocusedRowChanged">
                  
                        <Columns>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFNo" Caption="شماره مجوز">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ تمدید">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="پایان اعتبار">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MFType" Caption="وضعیت پروانه">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActives" Caption="وضعیت">
                                <CellStyle Wrap="True">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="TaskName" Caption="وضعیت درخواست"
                                Visible="False">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                VisibleIndex="6" Width="40px">
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
                            <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " Width="30px" ShowClearFilterButton="true">
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
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" Visible="false">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnEdit_Click" Visible="false">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                        Width="25px" ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
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
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                                        Width="25px" ID="btnDelete2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnDelete_Click" Visible="false">
                                                        <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/delete.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                                        CausesValidation="False" Width="25px" ID="btnReDuplicate2" UseSubmitBehavior="False"
                                                        Visible="False" EnableViewState="False" EnableTheming="False" OnClick="btnReDuplicate_Click">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/copy.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                                        CausesValidation="False" Width="25px" ID="btnRevival2" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnRevival_Click" Visible="false">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/Revival.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ارسال به مرحله بعد"
                                                        ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" Visible="false">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	txtDescription.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/reload.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                                        CausesValidation="False" ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnTracing_Click">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint2" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                        ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnExportExcel_Click">
                                                        <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/ExportExcel.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="left" style="width: 100%">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                        Visible="true" AutoPostBack="false">
                                                        <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                                        </ClientSideEvents>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMemberFile">
                    </dxwgv:ASPxGridViewExporter>
                    <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager">
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager">
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsResponsibility" runat="server" SelectMethod="GetData"
                        TypeName="TSP.DataManager.ResponcibilityTypeManager"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsMemberFile" runat="server" UpdateMethod="Update" SelectMethod="SelectObsDocSubRequest"
                        DeleteMethod="Delete" TypeName="TSP.DataManager.DocMemberFileManager" InsertMethod="Insert"
                        OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                            <asp:Parameter Name="TaskCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
                        TypeName="TSP.DataManager.WorkFlowTaskManager">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" Width="387px" 
                         ClientInstanceName="PopupWorkFlow" 
                        PopupHorizontalAlign="WindowCenter" Modal="True" 
                        HeaderText="" >
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <TSPControls:CustomAspxCallbackPanel runat="server"  Width="100%"
                                    ID="CallbackPanelWorkFlow" ClientInstanceName="CallbackPanelWorkFlow" OnCallback="CallbackPanelWorkFlow_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxp:ASPxPanel runat="server" Width="100%" ID="PanelMain" ClientInstanceName="PanelMain">
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                                            ForeColor="Red" Visible="False">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align: top; text-align: right" valign="top" align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="ارسال به مرحله" Font-Size="X-Small" Width="77px"
                                                                            ID="lblSenBack">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: right" dir="ltr" valign="top" align="right">
                                                                        <TSPControls:CustomAspxComboBox runat="server"  Width="290px" 
                                                                            ID="cmbSendBackTask" ValueType="System.String" >
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
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align: top; width: 159px; height: 37px; text-align: right" valign="top"
                                                                        align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Font-Size="X-Small" Width="56px" ID="ASPxLabel1">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 600px; height: 37px" dir="rtl" valign="top" align="right">
                                                                        <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtDescription" 
                                                                            Width="100%" ClientInstanceName="txtDescription" >
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
                                                                    <td dir="ltr" valign="top" align="center" colspan="2">
                                                                        <TSPControls:CustomAspxButton runat="server" Text="ارسال"  Width="93px" ID="btnSendNextWorkStep"
                                                                            AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                                            >
                                                                            <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	GridViewMemberFile.PerformCallback('');
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                            <dxp:ASPxPanel runat="server" Height="100%" Width="100%" ID="PanelSaveSuccessfully"
                                                ClientInstanceName="PanelSaveSuccessfully">
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <div align="center">
                                                            <br />
                                                            <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small"
                                                                ID="lblInstitueWarning" ForeColor="Red">
                                                            </dxe:ASPxLabel>
                                                            <br />
                                                            <br />
                                                            <TSPControls:CustomAspxButton runat="server" Text="خروج"  Width="93px" ID="btnClose"
                                                                AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                                >
                                                                <ClientSideEvents Click="function(s, e) {	
	GridViewMemberFile.PerformCallback('');
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
                       
                    </TSPControls:CustomASPxPopupControl>
          
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نماييد
                            <img alt="" src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
                <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
                </dxhf:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
