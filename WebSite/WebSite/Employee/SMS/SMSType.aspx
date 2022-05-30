<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SMSType.aspx.cs" Inherits="Employee_SMS_SMSType" Title="مدیریت نوع پیام" %>

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
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            ClientInstanceName="btnViewClient" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
	GridViewSMSType.AddNewRow();
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewSMSType.cpError=0;
	e.processOnServer=false;
	GridViewSMSType.GetValuesOnCustomCallback(GridViewSMSType.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(GridViewSMSType.cpShow==1)
	{		
		GridViewSMSType.StartEditRow(GridViewSMSType.GetFocusedRowIndex());
	}
	
}	
}
"></ClientSideEvents>

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                            CausesValidation="False" ID="btnActive" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnActive_Click">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                Click="function(s, e) {
		if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}" />

                                            <Image Height="25px" Url="~/Images/icons/button_ok.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="BtnInActive1" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnDelete_Click">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                Click="function(s, e) {
		if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="هزینه نوع پیام کوتاه"
                                            CausesValidation="False" ID="btnSmsTypeCost" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnSmsTypeCost_Click">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                Click="function(s, e) {
			if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>

                                            <Image Url="~/Images/Money-32.png">
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
            <TSPControls:CustomAspxDevGridView ID="GridViewSMSType" runat="server" DataSourceID="ObjdsSMSType"
                Width="100%"
                OnCustomDataCallback="GridViewSMSType_CustomDataCallback" AutoGenerateColumns="False"
                KeyFieldName="SmsTypeId" ClientInstanceName="GridViewSMSType" OnRowInserting="GridViewSMSType_RowInserting"
                OnRowUpdating="GridViewSMSType_RowUpdating" OnRowValidating="GridViewSMSType_RowValidating">
                <ClientSideEvents EndCallback="function(s, e) {
	if(GridViewSMSType.cpError==2)
		SetTaskOrderError(GridViewSMSType.cpErrorMsg);
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SmsTypeName" Caption="نوع پیام"
                        Width="30%">
                        <PropertiesTextEdit Width="300px">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                <RequiredField IsRequired="True" ErrorText="نوع پیام را وارد  نماييد"></RequiredField>
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="SmsTypeDescription" VisibleIndex="1"
                        Width="50%">
                        <PropertiesMemoEdit Height="37px" Width="300px"></PropertiesMemoEdit>
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="InActives" Caption="وضعیت" Width="20%">
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="3" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter"
                    PopupEditFormModal="True" Mode="PopupEditForm">
                </SettingsEditing>


            </TSPControls:CustomAspxDevGridView>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
		GridViewSMSType.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewSMSType.cpError=0;
	e.processOnServer=false;
	GridViewSMSType.GetValuesOnCustomCallback(GridViewSMSType.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	if(GridViewSMSType.cpShow==1)
	{		
		GridViewSMSType.StartEditRow(GridViewSMSType.GetFocusedRowIndex());
	}
	
}
}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                            CausesValidation="False" ID="btnActive2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnActive_Click">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                Click="function(s, e) {
		if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به  فعال کردن این ردیف هستید؟');
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/button_ok.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                            ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            ClientInstanceName="btnViewClient" OnClick="BtnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به  غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="هزینه نوع پیام کوتاه"
                                            CausesValidation="False" ID="btnSmsTypeCost2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnSmsTypeCost_Click">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                Click="function(s, e) {
		if (GridViewSMSType.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/Money-32.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>


                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjdsSMSType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.SmsTypeManager"
        OnInserting="ObjdsSMSType_Inserting"></asp:ObjectDataSource>
</asp:Content>
