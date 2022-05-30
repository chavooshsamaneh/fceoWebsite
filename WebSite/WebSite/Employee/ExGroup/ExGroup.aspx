<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ExGroup.aspx.cs" Inherits="Employee_ExGroup_ExGroup" Title="مدیریت انواع تشکل ها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    CausesValidation="False" ID="btnNew" AutoPostBack="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	gridViewExGroup.AddNewRow();
}"></ClientSideEvents>
                                                                  
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td style="width: 28px">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
gridViewExGroup.StartEditRow(gridViewExGroup.GetFocusedRowIndex());
}
"></ClientSideEvents>
                                                                
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel" runat="server"  EnableTheming="False"
                                                                    AutoPostBack="false" EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
  if (gridViewExGroup.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
	 if(confirm('آیا مطمئن به حذف این ردیف هستید؟'))
       gridViewExGroup.PerformCallback('delete');
     else {e.processOnServer=false;}
 }
}" />
                                                                    <Image Url="~/Images/icons/delete.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                                    ToolTip="غیر فعال" CausesValidation="False" ID="btnDisActive" EnableClientSideAPI="True"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		 
if (gridViewExGroup.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
      gridViewExGroup.PerformCallback('inactive');
    else {e.processOnServer=false;}
  }

}"></ClientSideEvents>
                                                             
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                                    ToolTip="فعال" CausesValidation="False" ID="btnactive" EnableClientSideAPI="True"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		 
if (gridViewExGroup.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به فعال کردن این ردیف هستید؟'))
    gridViewExGroup.PerformCallback('inactive');
  else {e.processOnServer=false;}
  }

}"></ClientSideEvents>
                                                             
                                                                    <Image  Url="~/Images/icons/Active.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                                    ID="btnprint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {	
	gridViewExGroup.PerformCallback('print');
}"></ClientSideEvents>
                                                                
                                                                    <Image  Url="~/Images/icons/printers.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    AutoPostBack="false">
                                                                    <ClientSideEvents Click="function(s,e){btnTempExport.DoClick();  }"></ClientSideEvents>
                                                                 
                                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView runat="server"  ID="gridViewExGroup"
                    Width="100%" DataSourceID="ObjectDataSourceExGroup" KeyFieldName="ExGroupId"
                    AutoGenerateColumns="False" ClientInstanceName="gridViewExGroup" 
                    OnRowValidating="gridViewExGroup_RowValidating" OnRowUpdating="gridViewExGroup_RowUpdating"
                    OnRowInserting="gridViewExGroup_RowInserting" OnCustomCallback="GridViewExGroup_CustomCallback">
                    <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
       if(s.cpDoPrint==1)
            {
               s.cpDoPrint = 0;
	           window.open('../../Print.aspx');
            }
}" />
                    <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormHorizontalAlign="WindowCenter"
                        PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" />
                    
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="نام گروه" FieldName="ExGroupName" VisibleIndex="1">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>                            
                            <PropertiesTextEdit Width="200px">
                                <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="Description" VisibleIndex="2"
                            Width="300px">
                            <PropertiesMemoEdit Width="300px" Height="37px">
                            </PropertiesMemoEdit>
                        </dxwgv:GridViewDataMemoColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="IsActive" VisibleIndex="3">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="4" ShowClearFilterButton="true">
                    
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                   
                </TSPControls:CustomAspxDevGridView>
                <asp:ObjectDataSource runat="server" SelectMethod="GetData" ID="ObjectDataSourceExGroup"
                    TypeName="TSP.DataManager.ExGroupManager"></asp:ObjectDataSource>
                <br />
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    CausesValidation="False" ID="btnNew2" AutoPostBack="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	gridViewExGroup.AddNewRow();
}"></ClientSideEvents>
                                                                  
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td style="width: 28px">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
gridViewExGroup.StartEditRow(gridViewExGroup.GetFocusedRowIndex());
}
"></ClientSideEvents>
                                                                  
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel2" runat="server"  EnableTheming="False"
                                                                    AutoPostBack="false" EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
                                                if (gridViewExGroup.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
	 if(confirm('آیا مطمئن به حذف این ردیف هستید؟'))
       gridViewExGroup.PerformCallback('delete');
       else {e.processOnServer=false;}
 }
}" />
                                                                    <Image Url="~/Images/icons/delete.png">
                                                                    </Image>
                                                                
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="" Height="25px" 
                                                                    ToolTip="غیر فعال" CausesValidation="False" ID="btnDisActive2" EnableClientSideAPI="True"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		 
if (gridViewExGroup.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
  gridViewExGroup.PerformCallback('inactive');
  else {e.processOnServer=false;}
  }

}"></ClientSideEvents>
                                                                
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                                    ToolTip="فعال" CausesValidation="False" ID="btnactive2" EnableClientSideAPI="True"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		 
if (gridViewExGroup.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به فعال کردن این ردیف هستید؟'))
  gridViewExGroup.PerformCallback('inactive');
  else {e.processOnServer=false;}
  }

}"></ClientSideEvents>
                                                              
                                                                    <Image  Url="~/Images/icons/Active.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                                    ID="btnprint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {	
	gridViewExGroup.PerformCallback('print');
}"></ClientSideEvents>
                                                              
                                                                    <Image  Url="~/Images/icons/printers.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                                    ID="btnExportExcel2" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s,e){btnTempExport.DoClick(); }"></ClientSideEvents>
                                                               
                                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="gridViewExGroup">
    </dxwgv:ASPxGridViewExporter>
    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
        runat="server" OnClick="btntemp_Click">
    </TSPControls:CustomAspxButton>
</asp:Content>
