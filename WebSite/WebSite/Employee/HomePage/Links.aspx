<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Links.aspx.cs" Inherits="Employee_HomePage_Links"
    Title="لینک های مرتبط" %>

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
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#"><span style="color: #000000">بستن</span></a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>



                            <table >
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                           
                                            <Image  Url="~/Images/icons/new.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" 
                                            UseSubmitBehavior="False">
                                            
                                            <Image  Url="~/Images/icons/edit.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                          
                                            <Image  Url="~/Images/icons/view.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                            ToolTip="حذف" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            
                                            <Image  Url="~/Images/icons/delete.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                       
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewLink" runat="server" AutoGenerateColumns="False"
         ClientInstanceName="GridViewLink"
         DataSourceID="ObjectDataSource1" OnCustomDataCallback="GridViewLink_CustomDataCallback" OnCustomCallback="GridViewLink_OnCustomCallback"
        KeyFieldName="LiId" Width="100%">
        <ClientSideEvents CustomButtonClick="function(s, e) {
    GridViewLink.cpError=0;
	e.processOnServer=false;
	GridViewLink.GetValuesOnCustomCallback(e.visibleIndex + ';'+e.buttonID);
    GridViewLink.PerformCallback();	
}"
            EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
    if(s.cpUpdate==1)
     {
        s.cpUpdate=0;
         GridViewLink.PerformCallback();
     }
}"></ClientSideEvents>
    
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="LiId" Name="LiId" Visible="False" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام لینک" Width="300px" FieldName="LiName"
                VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع لینک" Width="200px" FieldName="TypeName"
                VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت نمایش" Width="130px" FieldName="IsShow"
                VisibleIndex="2">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="اولویت نمایش" Width="70px" FieldName="OrderCode"
                VisibleIndex="2">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="آدرس لینک" Visible="false" FieldName="LinkAddress"
                VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn VisibleIndex="4" Width="80px" Caption="تغییر اولویت"
                ButtonType="Image" Name="OrderChange">
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton ID="Up" Text="اولویت بالاتر">
                        <Image Width="16px" Height="16px" Url="~/Images/icons/up-32.png">
                        </Image>
                    </dxwgv:GridViewCommandColumnCustomButton>
                    <dxwgv:GridViewCommandColumnCustomButton ID="Down" Text="اولویت پایین تر">
                        <Image Width="16px" Height="16px" Url="~/Images/icons/down-32.png">
                        </Image>
                    </dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxwgv:GridViewCommandColumn>
        </Columns>
      
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                            <table >
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                            
                                            <Image  Url="~/Images/icons/new.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" 
                                            UseSubmitBehavior="False">
                                           
                                            <Image  Url="~/Images/icons/edit.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                          
                                            <Image  Url="~/Images/icons/view.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                            ToolTip="حذف" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                           
                                            <Image  Url="~/Images/icons/delete.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.LinksManager"></asp:ObjectDataSource>
</asp:Content>
