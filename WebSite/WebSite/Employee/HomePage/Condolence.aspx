<%@ Page Title="مدیریت تبریک و تسلیت" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Condolence.aspx.cs" Inherits="Employee_HomePage_Condolence" %>

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
                    href="#"><span style="color: #000000">بستن</span></a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>
                                    <table >
                                        <tr>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                                    <Image Url="~/Images/new.png" >
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                                    <Image Url="~/Images/icons/edit.png" >
                                                    </Image>
                                                  
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" ToolTip="نمایش" OnClick="btnview_Click">
                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                    
                                                </TSPControls:CustomAspxButton>
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
                                                  
                                                </TSPControls:CustomAspxButton>
                                            </td>
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
                                                 
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                        </tr>
                                    </table>
                            </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                
                        <br />
                        <TSPControls:CustomAspxDevGridView ID="GridViewCondolence" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="grid"  
                            DataSourceID="ObjectDataSourceCondolence" EnableViewState="False" Width="100%"
                            RightToLeft="True" KeyFieldName="CdlId">

                           
                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" />
                            
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="نوع" Width="50px" FieldName="TypeName" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="خلاصه متن" Width="400px" FieldName="Summary" VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع نمایش" Width="100px" FieldName="StartDate" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان نمایش" Width="100px" FieldName="EndDate" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="وضعیت" Width="50px" FieldName="InActiveName" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" Width="50px" ShowClearFilterButton="true">
                             
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                        <br />
                            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>
                                    <table>
                                        <tr>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                                    <Image Url="~/Images/new.png" >
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                                    <Image Url="~/Images/icons/edit.png" >
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" ToolTip="نمایش" OnClick="btnview_Click">
                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
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
                                                  
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                        </tr>
                                    </table>
                                           </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                 
            <asp:ObjectDataSource ID="ObjectDataSourceCondolence" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.CondolenceManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldClnID" runat="server">
            </dx:ASPxHiddenField>

</asp:Content>
