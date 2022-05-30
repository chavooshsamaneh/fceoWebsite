<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberActivity.aspx.cs" Inherits="Members_MemberInfo_MemberActivity"
    Title="فعالیت ها" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">

        function SetTaskOrderError(result) {

            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
    document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
    document.getElementById('<%=LabelWarning.ClientID%>').innerText = result;
}

}

function SetDivVisible() {
    document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
    document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
}
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" __designer:mapid="25e5"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
	//pop.Show();
grid.cpError=0;
grid.AddNewRow();
	
	
}"></ClientSideEvents>
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </hoverstyle>
                                                            <image url="~/Images/icons/new.png">
                                                                </image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                                            EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);

 }
 else
{
  //pop.Show();
	
	grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
}
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnInActive_Click" Text=" " ToolTip="غیر فعال"
                                                            UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
  else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');

 
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/disactive.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </hoverstyle>
                                                            <image url="~/Images/icons/Back.png">
                                                                </image>
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
            <TSPControls:CustomAspxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="callmenu"
                OnCallback="ASPxCallbackPanel1_Callback" Width="100%" HideContentOnCallback="False">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" CssClass="ProjectMainMenuHorizontal"
                            OnItemClick="ASPxMenu1_ItemClick">
                            <Items>
                                <dxm:MenuItem Name="Member" Text="مشخصات عضو" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Job" Text="سوابق کاری" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Language" Text="زبان ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Activity" Text="فعالیت ها" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                            </Items>

                        </TSPControls:CustomAspxMenuHorizontal>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>
            </div>
        <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" Width="100%" runat="server"
                SettingsEditing-PopupEditFormModal="true"
                OnPageIndexChanged="CustomAspxDevGridView1_PageIndexChanged"
                EnableViewState="False" ClientInstanceName="grid" AutoGenerateColumns="False"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
                OnRowInserting="CustomAspxDevGridView1_RowInserting" OnRowUpdating="CustomAspxDevGridView1_RowUpdating"
                OnCustomDataCallback="CustomAspxDevGridView1_CustomDataCallback">
                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                <Columns>
                    <dxwgv:GridViewDataComboBoxColumn Caption="نام فعالیت" FieldName="AsId" VisibleIndex="1"
                        Width="90px">
                        <PropertiesComboBox DataSourceID="ODBAtSubj" TextField="AsName" ValueField="AsId"
                            ValueType="System.String" Width="170px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="نام فعالیت را انتخاب نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="درصد مشارکت" FieldName="AsPercent" VisibleIndex="2"
                        Width="80px">
                        <PropertiesTextEdit Width="80px"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="3">
                        <PropertiesTextEdit Width="300px"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="4"
                        Width="50px">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="MReId" Visible="False" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="20px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

                <Settings ShowHorizontalScrollBar="true"></Settings>
                <ClientSideEvents EndCallback="function(s, e) {
	if(grid.cpError==2)
      SetTaskOrderError(grid.cpErrorMsg);
      if(grid.cpMenu==1)
	callmenu.PerformCallback('');
}"
                    BeginCallback="function(s, e) {
	grid.cpMenu=0;
}" />
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
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
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </hoverstyle>
                                                            <image url="~/Images/icons/new.png">
                                                                </image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                                            EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);

 }
 else
{
  //pop.Show();
	
	grid.cpError=0;
	e.processOnServer=false;
	grid.GetValuesOnCustomCallback(grid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(grid.cpShow==1)
	{		
		grid.StartEditRow(grid.GetFocusedRowIndex());
	}
}
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                                            ToolTip="غیر فعال" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
 
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/disactive.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </hoverstyle>
                                                            <image url="~/Images/icons/Back.png">
                                                                </image>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="MemberId" runat="server" />
    <asp:ObjectDataSource ID="ODBMemberAtSubj" runat="server" DeleteMethod="Delete" FilterExpression="MeId={0}"
        InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.MemberActivitySubjectManager"
        UpdateMethod="Update">

        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBAtSubj" runat="server" CacheDuration="30" DeleteMethod="Delete"
        EnableCaching="True" InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.ActivitySubjectManager"
        UpdateMethod="Update"></asp:ObjectDataSource>
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
