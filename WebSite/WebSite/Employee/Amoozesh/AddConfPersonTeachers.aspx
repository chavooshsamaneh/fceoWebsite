<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddConfPersonTeachers.aspx.cs" Inherits="Employee_Amoozesh_AddConfPersonTeachers" Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>


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
                            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                          
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldConfirm" ClientInstanceName="HiddenFieldSMSConfirm"></dxhf:ASPxHiddenField><TSPControls:CustomASPxRoundPanelMenu ID="RounPanelHeader" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack" AutoPostBack="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/Back.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره" CausesValidation="False" ID="btnSave" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/save.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                        &nbsp;</td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف" CausesValidation="False" ID="BtnDelete" AutoPostBack="False" EnableViewState="False" EnableTheming="False" OnClick="BtnDelete_Click">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                                Click="function(s, e) {
	
  		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	
	
}
"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="btnNew" AutoPostBack="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient" OnClick="btnNew_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	
	txtbDescription.SetText(&quot;&quot;);
	txtbDescription.SetEnabled(true);
	CmbNezamChartName.SetEnabled(true);
	
	HiddenFieldSMSConfirm.Set(&quot;PageMode&quot;,HiddenFieldSMSConfirm.Get(&quot;New&quot;));
	HiddenFieldSMSConfirm.Set(&quot;ConfPerId&quot;,&quot;&quot;);
	}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
    <br />
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelConfirmPerson" HeaderText="ویرایش" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td colspan="5" dir="rtl" style="vertical-align: top; height: 16px; text-align: center">
                                                        <dxe:ASPxLabel ID="lblConfirmDetails" runat="server" Text="ASPxLabel">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 100px; height: 16px" dir="rtl">
                                                        <dxe:ASPxLabel runat="server" Text="سمت:" ID="ASPxLabel1"></dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" colspan="2">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="170px"  TextField="NcName" ID="CmbNezamChartName"  AutoPostBack="True" DataSourceID="ObjdsNezamChart" ValueType="System.String" ValueField="NcId" ClientInstanceName="CmbNezamChartName"  OnSelectedIndexChanged="CmbNezamChartName_SelectedIndexChanged">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>

                                                            <ButtonStyle Width="13px"></ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td style="vertical-align: top" dir="ltr" colspan="1"></td>
                                                    <td style="width: 3px" dir="ltr" colspan="1"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 100px">
                                                        <dxe:ASPxLabel runat="server" Text="نام:" ID="ASPxLabel2"></dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="2">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" Enabled="False" ID="txtbName" ClientInstanceName="txtbName" >
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top" colspan="1">
                                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی:" Width="79px" ID="ASPxLabel3"></dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 3px" colspan="1">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" Enabled="False" ID="txtbFamily" ClientInstanceName="txtbFamily" >
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 100px; height: 37px">
                                                        <dxe:ASPxLabel runat="server" Text="نوع تایید:" ID="ASPxLabel6"></dxe:ASPxLabel>
                                                    </td>
                                                    <td style="height: 37px" dir="ltr" colspan="2">
                                                        <TSPControls:CustomAspxComboBox runat="server"  ID="cmbPrioityType"  ValueType="System.String" >
                                                            <Items>
                                                                <dxe:ListEditItem Value="0" Text="تایید پرونده"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="1" Text="امتیازدهی مدارک"></dxe:ListEditItem>
                                                            </Items>

                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>

                                                            <ButtonStyle Width="13px"></ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td style="vertical-align: top" dir="rtl" colspan="1">
                                                        <dxe:ASPxLabel runat="server" Text="اولویت تایید:" ID="ASPxLabel5"></dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 3px; height: 37px" dir="rtl" colspan="1">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" Enabled="False" ID="txtPriority" ClientInstanceName="txtbFamily" >
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 100px; height: 37px">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات:" ID="ASPxLabel4"></dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top" dir="rtl" colspan="4">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="29px"  Width="472px" ID="txtbDescription" ClientInstanceName="txtbDescription" >
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <TSPControls:CustomASPxCheckBox runat="server" Text="دارای  اولویت یکسان با اولویت پیشین" ID="ChbHasPrePriority"></TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                    <td dir="rtl" colspan="1"></td>
                                                    <td dir="ltr" colspan="1"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                        <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
            
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack2" AutoPostBack="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/Back.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره" CausesValidation="False" ID="btnSave2" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/save.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                        &nbsp;</td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف" CausesValidation="False" ID="btnDelete2" AutoPostBack="False" EnableViewState="False" EnableTheming="False" OnClick="BtnDelete_Click">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
}"
                                                                Click="function(s, e) {
	
  		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit2" EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {
	
	
}
"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 32px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="btnNew2" AutoPostBack="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient">
                                                            <ClientSideEvents Click="function(s, e) {

	txtbDescription.SetText(&quot;&quot;);
	txtbDescription.SetEnabled(true);
	CmbNezamChartName.SetEnabled(true);
	
	HiddenFieldSMSConfirm.Set(&quot;PageMode&quot;,HiddenFieldSMSConfirm.Get(&quot;New&quot;));
	HiddenFieldSMSConfirm.Set(&quot;ConfPerId&quot;,&quot;&quot;);
	}"></ClientSideEvents>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>

                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsNezamChart" runat="server" OldValuesParameterFormatString="original_{0}" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Insert" TypeName="TSP.DataManager.NezamMemberChartManager" SelectMethod="FindByIsMaster">
                    
                    <SelectParameters>
                        <asp:Parameter Type="Boolean" DefaultValue="true" Name="IsMaster"></asp:Parameter>
                    </SelectParameters>
                   
                </asp:ObjectDataSource>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                                <img src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>

