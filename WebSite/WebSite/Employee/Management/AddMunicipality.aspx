<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddMunicipality.aspx.cs" Inherits="Employee_Management_AddMunicipality" Title="مشخصات شهرداری" %>

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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetCityControlValues() {
            gridCity.GetRowValues(gridCity.GetFocusedRowIndex(), 'CitName;CitId;CitCode;AgentName;AgentCode;AgentAddress', SetCityValue);
        }

        function SetCityValue(values) {
            txtCity.SetText(values[0]);
            HiddenFieldMunipality.Set('CitId', values[1]);
            HiddenFieldMunipality.Set('CitCode', values[2]);
        }

        function SetMunControlValues() {
            gridMun.GetRowValues(gridMun.GetFocusedRowIndex(), 'MunName;MunId;CitName', SetMunValue);
        }

        function SetMunValue(values) {
            txtMun.SetText(values[0]);
            HiddenFieldMunipality.Set('ParentId', values[1]);

        }
    </script>
    <asp:UpdatePanel ID='UpdatePanel1' runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/new.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/edit.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/save.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/Back.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMunipality" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomTextBox Caption="شهر" runat="server" ID="txtCity" ReadOnly="True" ClientInstanceName="txtCity">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="ValidCity" ErrorTextPosition="Bottom">


                                                <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" ToolTip="جستجو" CausesValidation="False" ID="btnSearchCity" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	popUpCity.Show();
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/Search.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                      <TSPControls:CustomTextBox Caption="نام شهرداری" runat="server" ID="txtMunName">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td>
                                       
                                    </td>

                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox Caption="شهرداری مرکزی" IsMenuButton="true" runat="server" ID="txtMunicipality" ReadOnly="True" ClientInstanceName="txtMun">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="ValidCity" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" ToolTip="جستجو" CausesValidation="False"  ID="btnSearchMun" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	popUpMun.Show();
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/Search.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                  
                                    <td dir="rtl" valign="top" align="right" colspan="4">
                                        <TSPControls:CustomASPXMemo Caption="توضیحات" runat="server" Height="37px" ID="txtDescription">
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/new.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/edit.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top" dir="ltr">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/save.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/Back.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldMunipality" ClientInstanceName="HiddenFieldMunipality"></dxhf:ASPxHiddenField>

            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" HeaderText="جستجو شیراز" PopupElementID="btnSearchCity" MaxWidth="200px" ClientInstanceName="popUpCity" PopupHorizontalAlign="Center" ResizingMode="Live">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">

                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" Width="100%" ID="GridViewCity" DataSourceID="ObjdsCity" KeyFieldName="CitId" AutoGenerateColumns="False" ClientInstanceName="gridCity" OnCustomCallback="GridViewCity_CustomCallback">
                            <ClientSideEvents RowDblClick="function(s, e) {
	SetCityControlValues();
	gridCity.cpSetObjds=1;
	popUpCity.Hide();
}"></ClientSideEvents>


                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CitName" Caption="شهر" Name="CitName"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="AgentName" Caption="نام نمایندگی" Name="AgentName"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="AgentCode" Width="50px" Caption="کد نمایندگی" Name="AgentCode"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="1px"></dxwgv:GridViewDataTextColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView>

                        <asp:ObjectDataSource runat="server" DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="SelectByCountry" ID="ObjdsCity" TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}">

                            <SelectParameters>
                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="CounId"></asp:Parameter>
                            </SelectParameters>

                        </asp:ObjectDataSource>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </TSPControls:CustomASPxPopupControl>
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl2" runat="server" HeaderText="جستجو شهرداری" PopupVerticalAlign="Middle"  MaxWidth="200px"  PopupHorizontalAlign="Center" CloseAction="CloseButton" PopupElementID="btnSearchMun"  ResizingMode="Live"  ClientInstanceName="popUpMun">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                
                            <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" Width="100%" ID="CustomAspxDevGridView1" DataSourceID="ObjdsMun" KeyFieldName="CitId" AutoGenerateColumns="False" ClientInstanceName="gridMun">
                                <ClientSideEvents RowDblClick="function(s, e) {
	SetMunControlValues();
	popUpMun.Hide();
}"></ClientSideEvents>


                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MunName" Caption="نام شهرداری" Name="MunName"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CitName" Caption="شهر" Name="CitName"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="1px"></dxwgv:GridViewDataTextColumn>
                                </Columns>

                            </TSPControls:CustomAspxDevGridView>

                            <asp:ObjectDataSource runat="server" SelectMethod="SelectByCity" ID="ObjdsMun" TypeName="TSP.DataManager.TechnicalServices.MunicipalityManager" OldValuesParameterFormatString="original_{0}">
                                <SelectParameters>
                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="CitId"></asp:Parameter>
                                </SelectParameters>
                            </asp:ObjectDataSource>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>


            </TSPControls:CustomASPxPopupControl>
            </div>
                </div>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
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

