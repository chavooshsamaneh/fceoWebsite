<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberGroups.aspx.cs" Inherits="Employee_MembersRegister_MemberGroups"
    Title="گروه ها" %>

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
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s, e) {
	pop.Show();
}"></ClientSideEvents>
                                                  
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                        CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                      
                                                        <Image  Url="~/Images/icons/delete.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                      
                                                        <Image  Url="~/Images/icons/Back.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                                        CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                      
                                                        <Image  Url="../../Images/icons/BakToManagment.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuTop" runat="server"  OnItemClick="MenuTop_ItemClick" >
                        <Items>
                            <dxm:MenuItem Name="Request" Text="مشخصات عضو">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Language" Text="زبان ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attach" Text="مستندات">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه ها" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مالی" Name="AccFish">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Message" Text="پیام ها" Visible="false">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="گزارش تنظیمات" Name="PollAnswer">
                            </dxm:MenuItem>
                        </Items>
                      
                    </TSPControls:CustomAspxMenuHorizontal>
             
                <br />
                <div style="width: 100%; text-align: right; display: none">
                    <dxe:ASPxLabel ID="lblSex" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblT" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblOfName" runat="server">
                    </dxe:ASPxLabel>
                </div>
                <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSource1"
                    Width="100%"  
                    EnableViewState="False" KeyFieldName="GrdId" AutoGenerateColumns="False" ClientInstanceName="grid">
                   
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="GrdId"
                            Name="GrdId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrName" Caption="نام گروه"
                            Name="GrName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                            Name="Description">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="2" FieldName="MeType">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                        
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                   
                </TSPControls:CustomAspxDevGridView>
       
                <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"  
                     PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
                    PopupElementID="btnSearch1" HeaderText="جدید" CloseAction="CloseButton" Modal="true"
                    Width="500px" ClientInstanceName="pop">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table>
                                <tr>
                                    <td colspan="2" align="right">
                                        <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView2" runat="server" AutoGenerateColumns="False"
                                              DataSourceID="ObjectDataSource2"
                                            Width="500px" KeyFieldName="GrId" OnCommandButtonInitialize="CustomAspxDevGridView2_CommandButtonInitialize"
                                            OnCustomButtonCallback="CustomAspxDevGridView2_CustomButtonCallback" EnableViewState="False">
                                            
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="GrId" Name="GrId" Visible="False" VisibleIndex="0">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewCommandColumn Caption=" انتخاب" ShowSelectCheckbox="True" VisibleIndex="0" ShowClearFilterButton="true">
                                       
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton Visibility="FilterRow" ID="SelectButton"
                                                            Text="انتخاب همه">
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="نام گروه" FieldName="GrName" Name="GrName"
                                                    VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns> 
                                        </TSPControls:CustomAspxDevGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br />
                                        <TSPControls:CustomAspxButton ID="btnSave" runat="server" 
                                             OnClick="btnSave_Click" Text="ذخیره" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	pop.Hide();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px" />
                    <CloseButtonImage Height="17px" Width="17px" />
                </TSPControls:CustomASPxPopupControl>
                <br />
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s, e) {
	pop.Show();
}"></ClientSideEvents>
                                                      
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                        CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                       
                                                        <Image  Url="~/Images/icons/delete.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                       
                                                        <Image  Url="~/Images/icons/Back.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                                        CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                       
                                                        <Image  Url="../../Images/icons/BakToManagment.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    UpdateMethod="Update" TypeName="TSP.DataManager.GroupDetailManager" SelectMethod="FindByMeId"
                    InsertMethod="Insert" FilterExpression="MeId={0} and MeType={1}" DeleteMethod="Delete">
                    
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                    
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="GrId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                        <asp:Parameter DefaultValue="1" Name="MeType" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="TSP.DataManager.GroupManager"
                    SelectMethod="GetData" FilterExpression="MeId={0}">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="MemberId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="MemberRequest" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDMode" runat="server" Visible="False"></asp:HiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
 
</asp:Content>
