<%@ Page Title="مدیریت امضاکنندگان گواهینامه ها" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PrintSetting.aspx.cs" Inherits="Employee_HomePage_PrintSetting" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#"><span style="color: #000000">بستن</span></a>]</div>
     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


 
                            <table cellpadding="0">
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                            <Image Height="25px" Url="~/Images/new.png" Width="25px">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به حذف کردن این ردیف هستید؟'))
  grid.PerformCallback('inactive');
  }
}" />
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                AutoPostBack="false" CausesValidation="False" Text=" " 
                                                EnableTheming="False" ToolTip="غیر فعال" ID="btninactive" EnableViewState="False"
                                                OnClick="btnInActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(!confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
   e.processOnServer=false;
  }

}"></ClientSideEvents>
                                                <Image  Url="~/Images/icons/disactive.png">
                                                </Image>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </td>                                  
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            ID="btnprint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
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
                                </tr>
                            </table> </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                   
                <br />
                <TSPControls:CustomAspxDevGridView ID="GridViewPrintSetting" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="grid"  
                    DataSourceID="ObjectDataSourcePrintSetting" EnableViewState="False" Width="100%"
                    OnAutoFilterCellEditorInitialize="GridViewPrintSetting_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="GridViewPrintSetting_HtmlDataCellPrepared" RightToLeft="True"
                    KeyFieldName="PrtsId" OnCustomCallback="GridViewPrintSetting_CustomCallback">
                    <ClientSideEvents EndCallback="function(s, e) {
       if(s.cpDoPrint==1)
            {
               s.cpDoPrint = 0;
	           window.open('../../Print.aspx');
            }
}" />
                    
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="نوع گواهینامه" Width="200px" FieldName="PrtTypeName"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امضاکننده اول" Width="130px" FieldName="FirstAssigner"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امضاکننده دوم" Width="130px" FieldName="SecondAssigner"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" Width="200px" FieldName="Description"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" Width="70px" FieldName="InActiveName"
                            VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" Width="80px" Name="Date" FieldName="CreateDate"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" Width="80px" Name="Date" FieldName="ExpireDate"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ غیرفعال" Width="80px" Name="Date" FieldName="InActiveDate"
                            VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="50px" ShowClearFilterButton="true">
                         
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <br />
           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


  
                            <table cellpadding="0">
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                            <Image Height="25px" Url="~/Images/new.png" Width="25px">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به حذف کردن این ردیف هستید؟'))
  grid.PerformCallback('inactive');
  }
}" />
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            AutoPostBack="false" CausesValidation="False" Text=" " 
                                            EnableTheming="False" ToolTip="غیر فعال" ID="btninactive2" EnableViewState="False"
                                            OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(!confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
   e.processOnServer=false;
  }

}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/disactive.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            ID="btnprint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
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
                                </tr>
                            </table>
                      </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjectDataSourcePrintSetting" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.PrintSettingManager" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
    <dx:ASPxHiddenField ID="HiddenFieldClnID" runat="server">
    </dx:ASPxHiddenField>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPrintSetting">
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
