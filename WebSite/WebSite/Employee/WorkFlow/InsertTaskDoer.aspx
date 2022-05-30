<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="InsertTaskDoer.aspx.cs" Inherits="Employee_WorkFlow_InserTaskDoer"
    Title="مشخصات انجام دهنده عملیات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                        <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                            [<a class="closeLink" href="#">بستن</a>]
                        </div>

                        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>




                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        ClientInstanceName="btnViewClient"  OnClick="btnNew_Click"
                                                        CausesValidation="False">
                                                       
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click" Height="25px">
                                                       
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnSave_Click" >
                                                     
                                                        <Image  Url="~/Images/icons/save.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click" Height="25px"
                                                        Width="25px">
                                                       
                                                        <Image  Url="~/Images/icons/Back.png">
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
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelTaskDoer" HeaderText="مشاهده" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>

                                    <fieldset runat="server" id="RoundPanelTaskDoerInfo">
                                        <legend id="RoundPanelTaskDoerInfoHeader" runat="server" class="fieldset-legend" dir="rtl">
                                        </legend>

                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="سمت" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 85%" valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                            TextField="FullName" ID="CmbNezamChartName" AutoPostBack="false" DataSourceID="ObjdsNezamChart"
                                                            ValueType="System.String" ValueField="NcId" ClientInstanceName="CmbNezamChartName"
                                                             RightToLeft="True" IncrementalFilteringMode="StartsWith" >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </fieldset>


                                    <fieldset runat="server" id="Fieldset1">
                                        <legend id="Legend1" runat="server" class="fieldset-legend" dir="rtl"><b>مراحل قابل ارسال</b>
                                        </legend>

                                        <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                            Width="100%" ID="GridViewSendBackTask" AutoGenerateColumns="False" ClientInstanceName="GridViewSendBackTask"
                                             OnHtmlRowPrepared="GridViewSendBackTask_HtmlRowPrepared">
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TaskName" Caption="مرحله">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TaskOrder" Caption="اولویت">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="3" Caption="امکان ارسال" ShowSelectCheckbox="True">
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView>
                                    </fieldset>
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
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        ClientInstanceName="btnViewClient" OnClick="btnNew_Click" Width="25px" CausesValidation="False">
                                                      
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                      
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                        ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" Width="25px" OnClick="btnSave_Click">
                                                      
                                                        <Image  Url="~/Images/icons/save.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click" Width="25px">
                                                     
                                                        <Image  Url="~/Images/icons/Back.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanelMenu>
                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTaskDoer" ClientInstanceName="HiddenFieldTaskDoer">
                        </dxhf:ASPxHiddenField>

                        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="ObjdsNezamChart" runat="server" TypeName="TSP.DataManager.NezamChartManager"
                            SelectMethod="SelectByWorkFlowTask" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
           
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
