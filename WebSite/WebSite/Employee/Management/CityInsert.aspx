<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CityInsert.aspx.cs" Inherits="CityInsert" Title="مشخصات شهر" %>

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
            <dxhf:ASPxHiddenField ID="HiddenFieldCity" runat="server" ClientInstanceName="HiddenFieldEmployeeClient">
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
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
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
            <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelCity" runat="server" Width="100%" ClientInstanceName="CallbackPanelCity"
                OnCallback="CallbackPanelCity_Callback" HideContentOnCallback="False">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelRequest" HeaderText="اطلاعات شهر" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>


                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="کد شهر" Width="100%" ID="labelCitCode">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="textCitCode" Width="100%"
                                                        ClientInstanceName="TextNo">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="کد شهر وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ClientSideEvents TextChanged="function(s, e) {

}"></ClientSideEvents>
                                                    </TSPControls:CustomTextBox>

                                                </td>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="نام شهر" Width="100%" ID="labelCitName">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="textCitName" Width="100%"
                                                        Style="direction: rtl">
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="نام شهر وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="کشور" Width="100%" ID="labelContry">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="CounName" ID="ComboCountry"
                                                        DataSourceID="ObjectDataSourceCountry" ValueType="System.Int32" ValueField="CounId"
                                                        EnableIncrementalFiltering="True"
                                                        ClientInstanceName="ComboCountry" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	CallbackPanelCity.PerformCallback('Country');
}"></ClientSideEvents>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource runat="server"
                                                        SelectMethod="GetData" ID="ObjectDataSourceCountry" TypeName="TSP.DataManager.CountryManager"></asp:ObjectDataSource>

                                                </td>
                                                <td valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="استان" Width="100%" ID="labelProvince">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="PrName" ID="comboProvince" DataSourceID="ObjectDataSourceProvince"
                                                        ValueType="System.Int32" ValueField="PrId"
                                                        EnableIncrementalFiltering="True" ClientInstanceName="comboProvince" OnDataBound="comboProvince_DataBound" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	CallbackPanelCity.PerformCallback('Province');
}"></ClientSideEvents>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="استان را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource runat="server"
                                                        SelectMethod="SelectByCounId" ID="ObjectDataSourceProvince"
                                                        TypeName="TSP.DataManager.ProvinceManager">

                                                        <SelectParameters>
                                                            <asp:ControlParameter PropertyName="Value" Type="Int32" DefaultValue="0" Name="CounId"
                                                                ControlID="ComboCountry"></asp:ControlParameter>
                                                        </SelectParameters>

                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="نمایندگی" Width="100%" ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top">
                                                    <TSPControls:CustomAspxComboBox ID="comboAgent" runat="server" Width="100%"
                                                        ClientInstanceName="comboAgent"
                                                        DataSourceID="ObjectDataSourceAgent"
                                                        TextField="Name" ValueField="AgentId" ValueType="System.Int32" OnDataBound="comboAgent_DataBound" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	CallbackPanelCity.PerformCallback('Agent');
}" />
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource runat="server" SelectMethod="FindByCurrentProvince" ID="ObjectDataSourceAgent"
                                                        TypeName="TSP.DataManager.AccountingAgentManager" OldValuesParameterFormatString="original_{0}">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="comboProvince" Name="PrId" PropertyName="Value"
                                                                Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                                <td valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="شهرستان" Width="100%" ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="ReCitName" ID="comboRegionOfCity" DataSourceID="ObjectDataSourceRegionOfCity"
                                                        ValueType="System.Int32" ValueField="ReCitId"
                                                        EnableIncrementalFiltering="True" ClientInstanceName="comboRegionOfCity" OnDataBound="comboRegionOfCity_DataBound" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="شهرستان را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource runat="server" SelectMethod="SelectByAgentId" ID="ObjectDataSourceRegionOfCity"
                                                        TypeName="TSP.DataManager.RegionOfCityManager" OldValuesParameterFormatString="original_{0}">
                                                        <SelectParameters>
                                                            <asp:ControlParameter PropertyName="Value" Type="Int32" Name="AgentId" ControlID="comboAgent"></asp:ControlParameter>
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="منطقه" Width="100%" ID="ASPxLabel2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="Name" ID="comboRegion" DataSourceID="ObjectDataSourceRegion"
                                                        ValueType="System.Int32" ValueField="ReId"
                                                        EnableIncrementalFiltering="True" ClientInstanceName="comboRegion" OnDataBound="comboRegion_DataBound" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="منطقه را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//CallbackPanelCity.PerformCallback('Agent');
}" />
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource runat="server" SelectMethod="GetData" ID="ObjectDataSourceRegion"
                                                        TypeName="TSP.DataManager.RegionManager"></asp:ObjectDataSource>
                                                </td>

                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="شماره حساب 5درصد طراحی" Width="100%" ID="ASPxLabel6">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAccountNumberDesign" Width="100%"
                                                        ClientInstanceName="txtAccountNumberDesign">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="شماره حساب وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="شماره حساب صددرصد حق الزحمه ناظرین" Width="100%" ID="ASPxLabel4">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAccountNmberObserving" Width="100%"
                                                        ClientInstanceName="txtAccountNmberObserving">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="شماره حساب وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="شماره حساب 5درصد حق الزحمه ناظر ساختمان و تاسیسات" Width="100%" ID="ASPxLabel5">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAccountNmberObserving5Percent" Width="100%"
                                                        ClientInstanceName="txtAccountNmberObserving5Percent">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="شماره حساب وارد نشده است"></RequiredField>
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
                                                    <dxe:ASPxLabel runat="server" Text="شماره حساب هزینه دفترچه فنی ملکی (پنج در هزار)" Width="100%" ID="ASPxLabel7">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAccountNmber5In1000" Width="100%"
                                                        ClientInstanceName="txtAccountNmber5In1000">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="شماره حساب وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>

                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="N در تابع ارجاع کار نظارت" Width="100%" ID="ASPxLabel12">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtNValueInFunctionA" Width="100%"
                                                        ClientInstanceName="txtNValueInFunctionA">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="N در تابع ارجاع کار نظارت"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="پین کد درگاه پارسیان  حساب طراحی" Width="100%" ID="ASPxLabel8">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtPINCodeDesign" Width="100%"
                                                        ClientInstanceName="txtPINCodeDesign">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="پین کد را وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="ترمینال درگاه پارسیان  حساب طراحی" Width="100%" ID="ASPxLabel9">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTerminalDesign" Width="100%"
                                                        ClientInstanceName="txtTerminalDesign">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="ترمینال را وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="پین کد درگاه پارسیان  حساب نظارت" Width="100%" ID="ASPxLabel10">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtPINCodeObserver" Width="100%"
                                                        ClientInstanceName="txtPINCodeObserver">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="پین کد را وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <dxe:ASPxLabel runat="server" Text="ترمینال درگاه پارسیان  حساب نظارت" Width="100%" ID="ASPxLabel11">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTerminalObserver" Width="100%"
                                                        ClientInstanceName="txtTerminalObserver">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="ترمینال را وارد نشده است"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <TSPControls:CustomASPxCheckBox runat="server" ID="checkboxShowInTsWorkRequest" Text="در لیست شهرهای صفحه آماده بکاری نمایش داده شود"></TSPControls:CustomASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <TSPControls:CustomASPxCheckBox runat="server" ID="checkboxCanObserverBeDesigner" Text="در ثبت طراحان پروژه های این شهر، امکان ثبت طراحی معماری توسط رشته عمران و معماری که فاقد دفتر طراحی وجود داشته باشد"></TSPControls:CustomASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <TSPControls:CustomASPxCheckBox runat="server" ID="checkboxIsPopulationUnder25000" Text="جمعیت شهر زیر 25000 نفر می باشید"></TSPControls:CustomASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">                                                    
                                                    <ul>
                                                        <li>ساختمان هاي گروه الف بنايي در شهرهاي زير 25000 نفر نياز به طراح تاسيسات ندارند.</li>
                                                        <li>ساختمان هاي گروه الف بنايي در شهرهاي  زير 25000 نفر نياز به ناظر تاسيسات ندارند.</li>
                                                        <li>در صدور فيش قبل از ناظر براي ساختمان هاي گروه الف بنايي در شهرهاي  زير 25000 نفر تعرفه ناظر تاسيسات و ناظر هماهنگ كننده محاسبه نمی شود. تنها متراژ مورد نظر براي ناظر سازه يا معماري ضربدر تعرفه مي شود.</li>
                                                        <li>در ارجاع كار گروهي و محدود براي اين ساختمان ها، ناظر تاسيسات حذف مي شود و ارجاع كار تنها به ناظر سازه يا معمار صورت مي گيرد. </li>
                                                        <li>در ارجاع كار ناظر سازه يا معمار بعنوان هماهنگ كننده انتخاب نمی شود و همچنين دكمه انتخاب ناظر هماهنگ كننده به صورت دستي براي اين شهرها غيرفعال می باشد. (عملا باتوجه به اينكه پروژه تنها داراي يك ناظر مي باشد انتخاب ناظر هماهنگ كننده مفهومي نخواهد داشت و ناظر هماهنگ كننده نداريم)</li>
                                                    </ul>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                    </dxp:PanelContent>
                </PanelCollection>
                <ClientSideEvents EndCallback="function(s, e) {
//alert(CallbackPanelCity.cpcomboAgent);
//comboAgent.SetEnabled(CallbackPanelCity.cpcomboAgent);
}
" />
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
                                                            <Image Url="~/Images/icons/new.png">
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
                                                            <Image Url="~/Images/icons/edit.png">
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
                                                            <Image Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) {


}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack2" EnableViewState="False" EnableTheming="False"
                                                            ClientInstanceName="btnBackClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                            OnClick="buttonBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/Back.png">
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
