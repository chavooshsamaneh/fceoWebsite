<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberLanguage.aspx.cs" Inherits="Members_MemberInfo_MemberLanguage"
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript">

        function SetTaskOrderError(result) {
            //alert(result);
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



                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False"
                                                            AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
	grid.cpError=0;
grid.AddNewRow();
	
	
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
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
	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
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
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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
                                <dxm:MenuItem Name="Language" Text="زبان ها" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Activity" Text="فعالیت ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                                </dxm:MenuItem>
                            </Items>

                        </TSPControls:CustomAspxMenuHorizontal>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>

            <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" Width="100%" runat="server"
                SettingsEditing-PopupEditFormModal="true"
                EnableViewState="False" ClientInstanceName="grid" AutoGenerateColumns="False"
                OnPageIndexChanged="CustomAspxDevGridView1_PageIndexChanged"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" OnCustomDataCallback="CustomAspxDevGridView1_CustomDataCallback"
                OnRowInserting="CustomAspxDevGridView1_RowInserting" OnRowUpdating="CustomAspxDevGridView1_RowUpdating">

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlanId"
                        Name="MlanId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="LanId" Width="100px" Caption="نام زبان"
                        VisibleIndex="0">
                        <PropertiesComboBox ValueType="System.String" TextField="LanName" ValueField="LanId"
                            DataSourceID="ODBLanguage" Width="150px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="نام زبان را انتخاب نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="LqId" Width="100px" Caption="حد دانش"
                        VisibleIndex="1">
                        <PropertiesComboBox ValueType="System.String" TextField="LqName" ValueField="LqId"
                            DataSourceID="ODBLanguageQuality" Width="150px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="حد دانش را انتخاب نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Width="300px"
                        Caption="توضیحات">
                        <PropertiesTextEdit Width="300px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="False" ErrorText="توضیحات را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="InActiveName" Caption="وضعیت"
                        Width="50px">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="MReId" Visible="False" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
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
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"
                ClientInstanceName="pop"
                HeaderText="جدید" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                AllowDragging="True" CloseAction="CloseButton" Modal="True">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table style="text-align: right" dir="rtl">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <asp:Label runat="server" Text="نام زبان" ID="Label57"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: right" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="145px"
                                            TextField="LanName" ID="drdLanName" EnableIncrementalFiltering="true"
                                            DataSourceID="ODBLanguage" ValueType="System.String" ValueField="LanId" ClientInstanceName="ComboName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نوع  زبان را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <asp:Label runat="server" Text="حد دانش" Width="62px" ID="Label58"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: right" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="145px"
                                            TextField="LqName" ID="drdLanQuality" EnableIncrementalFiltering="true"
                                            DataSourceID="ODBLanguageQuality" ValueType="System.String" ValueField="LqId"
                                            ClientInstanceName="ComboLq">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="حد دانش را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label62"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" Width="322px" ID="txtDesc"
                                            ClientInstanceName="TextDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="2">
                                        <br />
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="ذخیره" ID="btnSave" UseSubmitBehavior="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
  if(ComboName.GetIsValid() &amp;&amp; ComboLq.GetIsValid())
  {
	pop.Hide();
  }

}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                                            EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                 grid.cpError=0;
grid.AddNewRow();
	//pop.Show();
	//pop.SetHeaderText('جدید');
	//Enable();
	//HDMode.Set(&quot;Mode&quot;,HDLetterId.Get(&quot;New&quot;));
	
	//SetEmpty();
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
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
	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
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
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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
