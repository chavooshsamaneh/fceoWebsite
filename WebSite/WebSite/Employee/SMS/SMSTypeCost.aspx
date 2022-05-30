<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SMSTypeCost.aspx.cs" Inherits="Employee_SMS_SMSTypeCost" Title="مدیریت هزینه نوع پیام" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                <table >
                                    <tbody>
                                        <tr>  <td align="right"  style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    ClientInstanceName="btnViewClient">
                                                    <ClientSideEvents Click="function(s, e) {
  			 e.processOnServer=false;
	DevGridViewTypeCost.AddNewRow();
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right"  style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                    CausesValidation="False" ID="BtnDelete" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="BtnDelete_Click">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
}" Click="function(s, e) {
		if (DevGridViewTypeCost.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                    CausesValidation="False" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                   
                                                    <Image  Url="~/Images/icons/Back.png">
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
        	<TSPControls:CustomASPxRoundPanel ID="RoundPanelSmsType" HeaderText="نوع پیام" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                         
                                    
                                    <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                        ID="GridViewTypeCost" DataSourceID="ObjdsSmsTypeModified" KeyFieldName="TypeModifiedId"
                                        AutoGenerateColumns="False" ClientInstanceName="DevGridViewTypeCost" 
                                        OnRowInserting="DevGridViewTypeCost_RowInserting">
                                        <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter"
                                            PopupEditFormModal="True" Mode="PopupEditForm"></SettingsEditing>
                                        
                                        <Columns>
                                            <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="HasCost" Caption="هزینه برای گیرنده(دارد)"
                                                Width="150px">
                                            </dxwgv:GridViewDataCheckColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="1px">
                                                <editformsettings visible="False"></editformsettings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ModifiedDate" Caption="تاریخ"
                                                Width="300px">
                                                <cellstyle wrap="False"></cellstyle>
                                                <editformsettings visible="False"></editformsettings>
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                      
                                    </TSPControls:CustomAspxDevGridView>
                               </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                </br>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                <table >
                                    <tbody>
                                        <tr>
                                          
                                            <td align="right"  style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    ClientInstanceName="btnViewClient">
                                                    <ClientSideEvents Click="function(s, e) {
  			 e.processOnServer=false;
	DevGridViewTypeCost.AddNewRow();
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>  <td align="right"  style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                    CausesValidation="False" ID="btnDelete2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="BtnDelete_Click">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
}" Click="function(s, e) {
		if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right"  style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                    CausesValidation="False" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                   
                                                    <Image  Url="~/Images/icons/Back.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsSmsTypeModified" runat="server" SelectMethod="FindByTypeId"
                    TypeName="TSP.DataManager.SmsTypeModifiedManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="TypeId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
