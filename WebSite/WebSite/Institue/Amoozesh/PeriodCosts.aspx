<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodCosts.aspx.cs" Inherits="Institue_Amoozesh_PeriodCosts"
    Title="هزینه های متفرقه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'CoId;Price;TId;Body;Description', SetValue);
        }
        function SetValue(values) {
            CoId.SetText(values[0]);
            price.SetText(values[1]);
            type.SetValue(values[2]);
            body.SetText(values[3]);
            desc.SetText(values[4]);

        }

        function HasError() {
            if (type.GetIsValid() && price.GetIsValid())
                return false;
            return true;
        }
        function Enable() {
            price.SetEnabled(true);
            type.SetEnabled(true);
            body.SetEnabled(true);
            desc.SetEnabled(true);
        }
        function Disable() {
            price.SetEnabled(false);
            type.SetEnabled(false);
            body.SetEnabled(false);
            desc.SetEnabled(false);
        }
        function SetEmpty() {
            price.SetText("");
            type.SetSelectedIndex(-1);
            body.SetText("");
            desc.SetText("");
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td style="width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False"
                                            OnClick="BtnNew_Click">
                                            <image height="25px" url="~/Images/icons/new.png" width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
//mode.SetText(&quot;New&quot;);
//bedit.SetVisible(false);
//binsert.SetVisible(true);
//pop.SetHeaderText(&quot;جدید&quot;);
//Enable();
//SetEmpty();
pop.Show();
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش" Width="25px"
                                            OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
	//mode.SetText(&quot;Edit&quot;);
	//binsert.SetVisible(false);
	//bedit.SetVisible(true);
	//pop.SetHeaderText(&quot;ویرایش&quot;);
	//Enable();
//	SetControlValues();
if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
else
pop.Show();
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnShow" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="مشاهده" OnClick="btnShow_Click"
                                            UseSubmitBehavior="False">
                                            <image height="25px" url="~/Images/icons/view.png" width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
	
	//binsert.SetVisible(false);
	//bedit.SetVisible(false);
	//pop.SetHeaderText(&quot;مشاهده&quot;);
	//Disable();
	//SetControlValues();
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
else
pop.Show();
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                            <image height="25px" url="~/Images/icons/delete.png" width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
                        if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ButtonRet" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/Back.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="AspxMenu1" runat="server" SeparatorHeight="100%" OnItemClick="AspxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="InValid" Text="لغو دوره" Visible="false">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="TestMarks" Text="نتایج آزمون">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Costs" Text="هزینه های متفرقه" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Period" Text="مشخصات دوره">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl2" runat="server" ClientInstanceName="pop"
                HeaderText="جدید" PopupElementID="btnSearch1">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <table dir="rtl" id="TABLE1">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="مبلغ(ریال)" Width="63px" ID="Label4"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="140px" ID="txtPrice"
                                            ClientInstanceName="price">
                                            <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="نوع هزینه" Width="62px" ID="Label6"></asp:Label>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="OdbCostsType"
                                            TextField="TName" ValueField="TId" Width="145px" ID="ComboType"
                                            ClientInstanceName="type">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="نوع هزینه را انتخاب نمایید" IsRequired="True" />
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="شرح" ID="Label5"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo ID="txtBody" runat="server" ClientInstanceName="body"
                                            Height="28px" Width="370px">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="توضیحات" Width="59px" ID="Label7"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" ClientInstanceName="desc"
                                            Height="28px" Width="370px">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <br />
                                        <TSPControls:CustomAspxButton ID="btnInsert" runat="server" ClientInstanceName="binsert"
                                            OnClick="btnInsert_Click" Text="ذخیره" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		if(!HasError())
{
pop.Hide();

}
else
  e.processOnServer=false;
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </TSPControls:CustomASPxPopupControl>
            <br />
            <asp:HiddenField ID="PeriodId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PkCoId" runat="server" Visible="False"></asp:HiddenField>
            <TSPControls:CustomAspxDevGridView Width="100%" ID="CustomAspxDevGridView1" runat="server"
                KeyFieldName="CoId"
                AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ClientInstanceName="grid"
                EnableViewState="False">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="CoId" FieldName="CoId" Name="CoId" Visible="False"
                        VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="مبلغ" FieldName="Price" Name="Price" VisibleIndex="0">
                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="نوع هزینه" FieldName="Type" Name="TId"
                        VisibleIndex="1">
                        <PropertiesComboBox DataSourceID="OdbCostsType" TextField="TName" ValueField="TId"
                            ValueType="System.String">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شرح" FieldName="Body" Name="Body" VisibleIndex="2"
                        Width="300px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Visible="False"
                        VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="3" ShowClearFilterButton="true">                   
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew1" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False"
                                            OnClick="BtnNew_Click">
                                            <image height="25px" url="~/Images/icons/new.png" width="25px" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <ClientSideEvents Click="function(s, e) {

pop.Show();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit1" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش" Width="25px"
                                            UseSubmitBehavior="False" OnClick="btnEdit_Click">
                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <ClientSideEvents Click="function(s, e) {
	
	//binsert.SetVisible(false);
	//bedit.SetVisible(false);
	//pop.SetHeaderText(&quot;مشاهده&quot;);
	//Disable();
	//SetControlValues();
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
else
pop.Show();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnShow1" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False"
                                            OnClick="btnShow_Click">
                                            <image height="25px" url="~/Images/icons/view.png" width="25px" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <ClientSideEvents Click="function(s, e) {
	
	//binsert.SetVisible(false);
	//bedit.SetVisible(false);
	//pop.SetHeaderText(&quot;مشاهده&quot;);
	//Disable();
	//SetControlValues();
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
else
pop.Show();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete1" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                            <image height="25px" url="~/Images/icons/delete.png" width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
                        if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/Back.png" width="25px" />
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
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.ExpertCostsManager" FilterExpression="EpId={0} and TableType={1}">
        <FilterParameters>
            <asp:Parameter Name="EpId" />
            <asp:Parameter Name="TableType" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbCostsType" runat="server" SelectMethod="SelectCosts"
        TypeName="TSP.DataManager.ExpertCostsTypeManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="2" Name="Type" Type="Byte" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
