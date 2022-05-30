<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberLanguage.aspx.cs" Inherits="Employee_MembersRegister_MemberLanguage"
    Title="زبان ها" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <script language="javascript">

            function SetTaskOrderError(result) {
                if (result != null) {

                    document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                    document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
                    document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = result;
                }

            }

            function SetDivVisible() {
                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
            }
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true"
                    __designer:mapid="25e5">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" AutoPostBack="False">
                                                        <ClientSideEvents Click="function(s, e) {
	//pop.Show();
grid.cpError=0;
grid.AddNewRow();
	
	
}"></ClientSideEvents>
                                                      
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" AutoPostBack="False">
                                                        <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  {
  grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
  }
	
}"></ClientSideEvents>
                                                        
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                        ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnInActive_Click">
                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                       
                                                        <Image  Url="~/Images/icons/disactive.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                        ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnActive_Click">
                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                       
                                                        <Image  Url="~/Images/icons/active.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
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
                    <TSPControls:CustomAspxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callmenu"
                        OnCallback="ASPxCallbackPanel1_Callback" Width="100%" HideContentOnCallback="False" >
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  
                                  
                                    OnItemClick="ASPxMenu1_ItemClick" >
                                    <Items>
                                        <dxm:MenuItem Name="Request" Text="مشخصات عضو">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Job" Text="سوابق کاری">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Language" Text="زبان ها" Selected="true">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Attach" Text="مستندات">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Group" Text="گروه ها">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="مالی" Name="AccFish">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="Message" Text="پیام ها" Visible="false">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="گزارش تنظیمات" Name="PollAnswer">
                                        </dxm:MenuItem>
                                    </Items>
                                </TSPControls:CustomAspxMenuHorizontal>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomAspxCallbackPanel>
                </div>
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
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="grid" EnableViewState="False" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
                    OnCustomDataCallback="CustomAspxDevGridView1_CustomDataCallback" OnRowInserting="CustomAspxDevGridView1_RowInserting"
                    OnRowUpdating="CustomAspxDevGridView1_RowUpdating" OnRowValidating="CustomAspxDevGridView1_RowValidating"
                    Width="100%">
                    <Columns>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="4" Width="30px" ShowClearFilterButton="true">
                     
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="MlanId" Name="MlanId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="نام زبان" FieldName="LanId" VisibleIndex="0"
                            Width="150px">
                            <EditCellStyle HorizontalAlign="Right">
                            </EditCellStyle>
                            <PropertiesComboBox DataSourceID="ODBLanguage" TextField="LanName" ValueField="LanId"
                                ValueType="System.String" Width="150px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="نام زبان را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesComboBox>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="حد دانش" FieldName="LqId" VisibleIndex="1"
                            Width="150px">
                            <EditCellStyle HorizontalAlign="Right">
                            </EditCellStyle>
                            <PropertiesComboBox DataSourceID="ODBLanguageQuality" TextField="LqName" ValueField="LqId"
                                ValueType="System.String" Width="150px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="حد دانش را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesComboBox>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="2"
                            Width="300px">
                            <EditCellStyle HorizontalAlign="Right">
                            </EditCellStyle>
                            <PropertiesTextEdit Width="300px" Height="35px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="False" ErrorText="توضیحات را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="3"
                            Width="100px">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="MReId" Visible="False" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRow="True"
                        ShowFilterRowMenu="True"></Settings>
                    <ClientSideEvents EndCallback="function(s, e) {
if(grid.cpError==2)
       SetTaskOrderError(grid.cpErrorMsg);

if(grid.cpMenu==1)
	callmenu.PerformCallback('');
}" BeginCallback="function(s, e) {
	grid.cpMenu=0;
}" />
                    <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter"
                        PopupEditFormVerticalAlign="WindowCenter" Mode="PopupEditForm" PopupEditFormModal="True" />
                </TSPControls:CustomAspxDevGridView>
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
                                                        ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        AutoPostBack="False">
                                                        <ClientSideEvents Click="function(s, e) {
grid.cpError=0;
grid.AddNewRow();
	//pop.Show();
	//pop.SetHeaderText('جدید');
	//Enable();
	//HDMode.Set(&quot;Mode&quot;,HDLetterId.Get(&quot;New&quot;));
	
	//SetEmpty();
}"></ClientSideEvents>
                                                       
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" AutoPostBack="False">
                                                        <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  {
  grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
  }
  
  
	
}"></ClientSideEvents>
                                                       
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                        ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnInActive_Click">
                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                       
                                                        <Image  Url="~/Images/icons/disactive.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                        ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnActive_Click">
                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                       
                                                        <Image  Url="~/Images/icons/active.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="OdbMeLanguage" runat="server" DeleteMethod="Delete" FilterExpression="MeId={0}"
            InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.MemberLanguageManager"
            UpdateMethod="Update">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODBLanguage" runat="server" CacheDuration="30" DeleteMethod="Delete"
            EnableCaching="True" InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.LanguageManager"
            UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODBLanguageQuality" runat="server" CacheDuration="30" DeleteMethod="Delete"
            EnableCaching="True" InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.LanguageQualityManager"
            UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:HiddenField ID="MemberId" runat="server" />
        <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
        <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
 
</asp:Content>
