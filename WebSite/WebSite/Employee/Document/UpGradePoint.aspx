<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="UpGradePoint.aspx.cs" Inherits="Employee_Document_UpGradePoint"
    Title="مدیریت امتیازات ارتقاء پایه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       
          <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate> <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="BtnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                              <td style="width: 30px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
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
                                            <td style="width: 30px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    Width="25px" ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
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
                                             <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnInActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                     <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnActive_Click" Text=" " ToolTip="فعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/active.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                        </tr>
                                    </tbody>
                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewUpGradePoint" runat="server" DataSourceID="ObjdsUpGradePoint"
                Width="100%" 
                 ClientInstanceName="GridViewUpGradePoint"
                AutoGenerateColumns="False" KeyFieldName="UpGrdPId" OnHtmlDataCellPrepared="GridViewUpGradePoint_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="GridViewUpGradePoint_AutoFilterCellEditorInitialize">
                 
                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PointStatus" Caption="ارتقاء/تمدید">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="UpGrdId" Width="100px" Caption="ارتقاء پایه"
                        VisibleIndex="1">
                        <headerstyle font-size="X-Small"></headerstyle>
                        <cellstyle wrap="False" horizontalalign="right"></cellstyle>
                        <propertiescombobox valuetype="System.String" textfield="UpGrdName" datasourceid="ObjdsUpGrade"
                            valuefield="UpGrdId"></propertiescombobox>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته">
                        <cellstyle wrap="False"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ResName" Caption="صلاحیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MinPointPeriod" Width="100px"
                        Caption="امتیاز لازم از دوره">
                        <headerstyle wrap="True" font-size="X-Small"></headerstyle>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="MinPointSeminar" Width="100px"
                        Caption="امتیاز لازم از سمینار">
                        <headerstyle wrap="True" font-size="X-Small"></headerstyle>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="TotalPoint" Width="100px"
                        Caption="کل امتیاز مورد نیاز از دوره و سمینار">
                        <headerstyle wrap="True" font-size="X-Small"></headerstyle>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="JobDuration" Width="100px"
                        Caption="سابقه کار حرفه ای لازم">
                        <headerstyle wrap="True" font-size="X-Small"></headerstyle>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="TotalPointNeed" Width="100px"
                        Caption="کل امتیاز مورد نیاز">
                        <headerstyle wrap="True" font-size="X-Small"></headerstyle>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="MinPeriodNeed" Width="100px"
                        Caption="تعداد دوره های مورد نیاز ">
                        <headerstyle wrap="True" font-size="X-Small"></headerstyle>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                   
                    <dxwgv:GridViewDataTextColumn  VisibleIndex="8" FieldName="InActives"
                        Caption="وضعیت">
                        <headerstyle font-size="X-Small"></headerstyle>
                        <cellstyle horizontalalign="Center"></cellstyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Width="30px" VisibleIndex="11" Caption=" "  ShowClearFilterButton="true">
                         
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="BtnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                               <td style="width: 30px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
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
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    Width="25px" ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
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
                                          <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnInActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                     <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnActive_Click" Text=" " ToolTip="فعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewUpGradePoint.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/active.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                        </tr>
                                    </tbody>
                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        <asp:ObjectDataSource ID="ObjdsUpGrade" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.DocAcceptedUpGradeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsUpGradePoint" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.DocUpGradePointManager"></asp:ObjectDataSource>
        </contenttemplate> </asp:UpdatePanel>

</asp:Content>
