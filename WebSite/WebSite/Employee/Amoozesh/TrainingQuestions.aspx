<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TrainingQuestions.aspx.cs" Inherits="Employee_Amoozesh_TrainingQuestions"
    Title="آرشیو سؤالات امتحانی" %>

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
    
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]
        </div>
      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
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
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False"
                                                                Width="25px">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                                ToolTip="حذف" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
 	else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" CausesValidation="False" 
                                                                EnableTheming="False" EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="چاپ"
                                                                UseSubmitBehavior="False" Visible="true">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                                <ClientSideEvents Click="function(s,e){ grid.PerformCallback('Print'); }" />
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
        <TSPControls:CustomAspxDevGridView ID="GridViewQuestions" runat="server" AutoGenerateColumns="False"
            Width="100%"  
            OnCustomCallback="GridViewQuestions_CustomCallback" DataSourceID="ObjectDataSource1"
            KeyFieldName="TrQuId" ClientInstanceName="grid" OnHtmlDataCellPrepared="GridViewQuestions_HtmlDataCellPrepared"
            OnAutoFilterCellEditorInitialize="GridViewQuestions_AutoFilterCellEditorInitialize">
          
            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="TrQuId" Visible="False" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Width="200px" FieldName="CrsId" Caption="نام درس"
                    Name="CrsName" VisibleIndex="1">
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                    <PropertiesComboBox ValueType="System.String" TextField="CrsName" DataSourceID="ObjdsCourse"
                        ValueField="CrsId">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="طراح سؤال" FieldName="TrName" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="سؤال" Width="200px" FieldName="QuText" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نوع" FieldName="TypeName" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" VisibleIndex="3">
                    <CellStyle Wrap="True">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="4" ShowClearFilterButton="true">
                   
                </dxwgv:GridViewCommandColumn>
            </Columns>
           
            <ClientSideEvents EndCallback="function(s,e){ 
            if(s.cpReqType=='Print')
            {
              if(s.cpPrint==1)
              {
                window.open(s.cpReqValue);            
                s.cpPrint=0;
               }
               }
             }" />
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
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False"
                                                                Width="25px">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                                ToolTip="حذف" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
 	else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" runat="server" CausesValidation="False" 
                                                                EnableTheming="False" EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="چاپ"
                                                                UseSubmitBehavior="False" Visible="true">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                                <ClientSideEvents Click="function(s,e){ grid.PerformCallback('Print'); }" />
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
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.TrainingQuestionsManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsCourse" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
   
</asp:Content>
