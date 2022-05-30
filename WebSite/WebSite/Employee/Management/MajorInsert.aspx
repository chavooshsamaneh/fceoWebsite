<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MajorInsert.aspx.cs" Inherits="Employee_Management_MajorInsert"
    Title="مشخصات رشته تحصیلی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dxhf:ASPxHiddenField ID="HiddenFieldMajor" runat="server" ClientInstanceName="HiddenFieldEmployeeClient">
            </dxhf:ASPxHiddenField>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table id="tableCallback" style="vertical-align: middle; display: block; overflow: hidden; border-collapse: collapse"
                            cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew" EnableViewState="False" EnableTheming="False"
                                                            ClientInstanceName="btnNewClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                            OnClick="btnNew_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	//SetNewMode();
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False"
                                                            ClientInstanceName="btnEditClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                            OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	
}
" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td style="width: 30px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                            ID="btnSave" EnableClientSideAPI="True" EnableTheming="False" ClientInstanceName="btnSaveClient2"
                                                            OnClick="btnSave_Click" EnableViewState="False" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {

}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack" EnableViewState="False" EnableTheming="False"
                                                            ClientInstanceName="btnBackClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                            OnClick="buttonBack_Click">
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
            <br />

            <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelMajor" runat="server" Width="100%" ClientInstanceName="CallbackPanelMajor"
                OnCallback="CallbackPanelMajor_Callback" HideContentOnCallback="False">
                <SettingsLoadingPanel Text="در حال بارگذاری" />
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelRequest" HeaderText="اطلاعات رشته" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>


                                    <table dir="rtl" width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel3" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" colspan="3" style="width: 85%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtmjname"  Width="100%" ClientInstanceName="TextNo"
                                                        >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="نام وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ClientSideEvents TextChanged="function(s, e) {

}"></ClientSideEvents>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="کدرشته" ID="labelContry" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                <%--    <dxwgv:GRIDVIEWDATACHECKCOLUMN Width="60px" Caption="رشته اصلی" FieldName="IsMaster"
                                                        Name="IsMaster" VisibleIndex="0" />--%>
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="textmjCode"
                                                         Width="100%" ClientInstanceName="TextNo"  Enabled="false">
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="رشته واد نشده"></RequiredField>
                                                            <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                    <editcellstyle horizontalalign="Right" />
                                                </td>
                                                <td valign="top" align="right" dir="ltr" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="زیرشاخه" ID="ASPxLabel2" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" IncrementalFilteringMode="StartsWith"
                                                        TextField="MJName" ID="cmbMajor"  DataSourceID="ObjectDataSourceDropDown"
                                                        ValueType="System.Int32" ValueField="MJId" ClientInstanceName="comboRegion" 
                                                        EnableIncrementalFiltering="True" Style="direction: ltr" OnDataBound="cmbMajor_DataBound" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//CallbackPanelCity.PerformCallback('Agent');
}"></ClientSideEvents>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Width="14px" Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="زیر شاخه را انتخاب نمایید"></RequiredField>
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
                                                <td valign="top" align="right" colspan="4">
                                                    <asp:CheckBox runat="server" Text="رشته با کد اصلی می باشد" ID="chemajor"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel1" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="33px" ID="txtmjdes"  Width="100%"
                                                        ClientInstanceName="TextNo" >
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                        <asp:ObjectDataSource runat="server"
                            SelectMethod="FindMjParents" ID="ObjectDataSourceDropDown"
                            TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                    </dxp:PanelContent>
                </PanelCollection>
                <ClientSideEvents EndCallback="function(s, e) {
//alert(CallbackPanelCity.cpcomboAgent);
//comboAgent.SetEnabled(CallbackPanelCity.cpcomboAgent);
}
"></ClientSideEvents>
            </TSPControls:CustomAspxCallbackPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
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
                                                            CausesValidation="False" ID="btnNew2" EnableViewState="False" EnableTheming="False"
                                                            ClientInstanceName="btnNewClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                            OnClick="btnNew_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	//SetNewMode();
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit2" EnableViewState="False" EnableTheming="False"
                                                            ClientInstanceName="btnEditClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                            OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	
}
"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                            ID="btnSave2" EnableClientSideAPI="True" EnableTheming="False" ClientInstanceName="btnSaveClient2"
                                                            OnClick="btnSave_Click" EnableViewState="False" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) {


}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack2" EnableViewState="False" EnableTheming="False"
                                                            ClientInstanceName="btnBackClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                            OnClick="buttonBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
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
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
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
