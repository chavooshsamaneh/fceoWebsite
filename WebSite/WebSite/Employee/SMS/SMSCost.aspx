<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SMSCost.aspx.cs" Inherits="Employee_SMS_SMSCost" Title="مدیریت هزینه پیام کوتاه" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient"
                                            OnClick="btnNew_Click" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                            CausesValidation="False" ID="BtnDelete" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnDelete_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewCost.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/delete.png">
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

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelCreditInfo" ClientInstanceName="RoundPanelRequest"
                ShowHeader="false" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%">
                            <tr>
                                <td width="20%" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="اعتبار باقیمانده در مگفا" ID="ASPxLabel1">
                                    </dxe:ASPxLabel>
                                </td>
                                <td width="30%" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" RightToLeft="True" ForeColor="Blue" Font-Bold="true"
                                        ID="lblMagfaCreditInfo">
                                    </dxe:ASPxLabel>
                                </td>
                                <td width="25%" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="اعتبار باقیمانده در عصر فرا ارتباط" ID="ASPxLabel2">
                                    </dxe:ASPxLabel>
                                </td>
                                <td width="25%" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" RightToLeft="True" ForeColor="Blue"
                                        ID="lblAFECreditInfo">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>  <dxe:ASPxLabel runat="server" Text="اعتبار باقیمانده در پویا رایانه دنا:" ID="ASPxLabel7">
                            </dxe:ASPxLabel></td>
                                <td>   <dxe:ASPxLabel runat="server" Font-Bold="true" RightToLeft="True" ForeColor="Blue"
                                ID="lblPrdcoCreditInfo">
                            </dxe:ASPxLabel></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewCost" runat="server" DataSourceID="ObjdsSMSCost"
                AutoGenerateColumns="False" ClientInstanceName="GridViewCost" Width="100%" KeyFieldName="CostId">

                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeComName" Caption="نام وب سرویس">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="MailNo" Caption="شماره نامه">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MailDate" Caption="تاریخ نامه">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="CostEn" Caption="هزینه(انگلیسی)">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CostFr" Caption="هزینه(فارسی)">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="StartDate" Caption="تاریخ">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="IsActives" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
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
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                            CausesValidation="False" ID="btnDelete2" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnDelete_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                Click="function(s, e) {
		if (GridViewCost.GetFocusedRowIndex()&lt;0)
 		{
  			 e.processOnServer=false;
  			 alert(&quot;ردیفی انتخاب نشده است&quot;);
		 }
		 else
  		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsSMSCost" runat="server" TypeName="TSP.DataManager.SmsCostManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
