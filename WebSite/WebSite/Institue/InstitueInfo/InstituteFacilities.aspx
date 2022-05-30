<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="InstituteFacilities.aspx.cs" Inherits="Institue_InstitueInfo_InstituteFacilities"
    Title="امکانات و تجهیزات مؤسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
       
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
                    </div>
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                            <image url="~/Images/icons/Back.png">
                                                    </image>
                                                            <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                    </hoverstyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                        <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server"
                          
                            OnItemClick="ASPxMenu1_ItemClick" >
                            <Items>
                                <dxm:MenuItem Name="BasicInfo" Text="اطلاعات پایه">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Manager" Text="هیئت اجرایی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Activity" Text="زمینه های فعالیت">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Facility" Text="امکانات و تجهیزات" Selected="true">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="InsTeacher" Text="اساتید مؤسسه">
                                </dxm:MenuItem>
                            </Items>

                        </TSPControls:CustomAspxMenuHorizontal>
                    <br />
              <TSPControls:CustomASPxRoundPanel ID="RoundPanelFacility" HeaderText="امکانات و تجهیزات" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                                            Width="100%" ID="GridViewFacility" DataSourceID="ObjdsInstitueFacility" KeyFieldName="InsFacilityId"
                                            AutoGenerateColumns="False" ClientInstanceName="GridViewFacility"
                                            OnHtmlRowPrepared="GridViewFacility_HtmlRowPrepared">
                                           
                                            <Columns>
                                                <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="IsEquipment" Width="150px"
                                                    Caption="نوع">
                                                    <DataItemTemplate>
                                                        <dxe:ASPxLabel ID="lblFacilityType" runat="server" Text="ASPxLabel"></dxe:ASPxLabel>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FacilityName" Width="200px"
                                                    Caption="تجهیزات/فضای آموزشی">
                                                    <PropertiesTextEdit Width="200px">
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="نام فضای آموزشی را وارد نمایید"></RequiredField>
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Capacity" Width="50px"
                                                    Caption="تعداد/ظرفیت">
                                                    <PropertiesTextEdit Width="200px">
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="ظرفیت فضای آموزشی را وارد نمایید"></RequiredField>

                                                            <RegularExpression ErrorText="ظرفیت فضای آموزشی عدد صحیح مثبت می باشد."></RegularExpression>
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Width="200px"
                                                    Caption="توضیحات">
                                                    <PropertiesTextEdit Width="200px"></PropertiesTextEdit>
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                        </TSPControls:CustomAspxDevGridView>
                                           </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                    <br />
                              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton  IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False"
                                                            OnClick="btnBack_Click">
                                                            <image url="~/Images/icons/Back.png">
                                                    </image>
                                                            <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                    </hoverstyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                            </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldFacility">
                                        </dxhf:ASPxHiddenField>
                    <asp:ObjectDataSource ID="ObjdsInstitueFacility" runat="server" SelectMethod="FindByCertificate"
                        TypeName="TSP.DataManager.InstitueFacilityManager" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="InsCId" Type="Int32" />
                            <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
</asp:Content>
