<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PreRegister.aspx.cs" Inherits="Employee_Amoozesh_PreRegister"
    Title="پیش ثبت نام دروس" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
     
     
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             EnableTheming="False" ToolTip="جدید" ID="btnNew"
                                            EnableViewState="False" OnClick="btnNew_Click">
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                            ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             Width="25px" EnableTheming="False" ToolTip="مشاهده"
                                            ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" "  EnableTheming="False" ToolTip="حذف"
                                            ID="btnDelete" EnableViewState="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             EnableTheming="False" ToolTip="چاپ" ID="btnPrint"
                                            EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
GridViewPreRegister.PerformCallback('Print');
}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td />
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                        EnableTheming="False" ToolTip="خروجی Excel" ID="btnExportExcel" EnableViewState="False"
                                        OnClick="btnExportExcel_Click">
                                        <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                        <Image  Url="~/Images/icons/ExportExcel.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td align="right" valign="top" width="15%">تاریخ از
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <pdc:PersianDateTextBox ID="txtDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                        PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                        Width="256px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                                </td>
                                <td align="right" valign="top" width="15%">تاریخ تا
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <pdc:PersianDateTextBox ID="txtDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                        PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                        Width="256px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>درس</td>
                                <td>
                                    <TSPControls:CustomAspxComboBox Width="100%" runat="server"  TextField="CrsName"
                                        ID="cmbCourse" ClientInstanceName="cmbCourse"  DataSourceID="ObjdsCourse" ValueType="System.String"
                                        ValueField="CrsId"  RightToLeft="True">
                                    </TSPControls:CustomAspxComboBox>
                                    <asp:ObjectDataSource ID="ObjdsCourse" runat="server" TypeName="TSP.DataManager.CourseManager"
                                        SelectMethod="GetData"></asp:ObjectDataSource>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="vertical-align: top">
                                    <TSPControls:CustomAspxButton  ID="btnSearch" runat="server" 
                                         Text="جستجو" Width="100px" AutoPostBack="true" OnClick="btnSearch_Click">
                                        <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" colspan="2" style="vertical-align: top">
                                    <TSPControls:CustomAspxButton  ID="btnClear" runat="server" AutoPostBack="true" OnClick="btnSearch_Click" 
                                         Width="100px" Text="پاک کردن فرم" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {	

   	 ClearSearch(); 
}"></ClientSideEvents>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewPreRegister" runat="server" DataSourceID="ObjdsPreRegister"
                Width="100%"  
                OnAutoFilterCellEditorInitialize="GridViewPreRegister_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="GridViewPreRegister_HtmlDataCellPrepared" AutoGenerateColumns="False"
                KeyFieldName="PRegisterId" OnCustomCallback="GridViewPreRegister_CustomCallback"
                EnableViewState="False" ClientInstanceName="GridViewPreRegister" RightToLeft="True">
                <ClientSideEvents EndCallback="function(s,e){
                 
	                if(GridViewPreRegister.cpPrint == 1)
                    {// alert(1);
                        GridViewPreRegister.cpPrint = 0;
                        window.open('../../Print.aspx');
                    }
                }" />
                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0"
                        Width="30px">
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="MeName" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="MeFamily" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="واحد درسی" FieldName="CrsName" VisibleIndex="3"
                        Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="مؤسسه" FieldName="InsName" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="استاد" FieldName="TeFullName" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="RegisteringDate" VisibleIndex="4"
                        Width="100px">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="True"></Settings>
            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjdsPreRegister" runat="server" SelectMethod="SelectForManagmentPage"
                TypeName="TSP.DataManager.PreRegisterManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Name="RegisteringDateFrom" DbType="String" DefaultValue="1" />
                    <asp:Parameter Name="RegisteringDateTo" DbType="String" DefaultValue="2" />
                    <asp:Parameter Name="CrsId" DbType="Int32" DefaultValue="-1" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <dx:ASPxGridViewExporter ID="GridViewExporterPreReg" runat="server" GridViewID="GridViewPreRegister">
            </dx:ASPxGridViewExporter>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             EnableTheming="False" ToolTip="جدید" ID="btnNew2"
                                            EnableViewState="False" OnClick="btnNew_Click">
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                            ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             Width="25px" EnableTheming="False" ToolTip="مشاهده"
                                            ID="btnView2" EnableViewState="False" OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" "  EnableTheming="False" ToolTip="حذف"
                                            ID="btnDelete2" EnableViewState="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td dir="ltr">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                             EnableTheming="False" ToolTip="چاپ" ID="btnPrint2"
                                            EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td dir="ltr" />
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                        EnableTheming="False" ToolTip="خروجی Excel" ID="btnExportExcel2" EnableViewState="False"
                                        OnClick="btnExportExcel_Click">
                                        <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                        <Image  Url="~/Images/icons/ExportExcel.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

</asp:Content>
