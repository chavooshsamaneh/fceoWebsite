<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintEnvelopeDetailsUserControl.ascx.cs" Inherits="UserControl_PrintEnvelopeDetailsUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<div style="direction:rtl;">
<TSPControls:CustomASPxPopupControl ID="popupChooseDetails" runat="server" 
             ShowPageScrollbarWhenModal="True"
            AutoUpdatePosition="True" ClientInstanceName="popupChooseDetails" PopupVerticalAlign="WindowCenter"
            PopupHorizontalAlign="WindowCenter" Modal="True" CloseAction="CloseButton" AllowDragging="True"
             HeaderText="انتخاب جزئیات">
    <ClientSideEvents Closing ="alert(1);" />
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                        <table style="width: 300px; text-align: right;">
                            <tr>
                                <td valign="top" style="width: 89px" >
                                 <dxe:ASPxLabel runat="server" Text="انتخاب آدرس"  ID="ASPxLabel1"  
                                            >
                                </dxe:ASPxLabel>
                                </td>
                                <td valign="top">
                                <TSPControls:CustomAspxComboBox runat="server"  Width="180px" 
                                 ID="comboAddress" 
                                 ClientInstanceName="comboAddress" 
                                EnableIncrementalFiltering="True" HorizontalAlign="Right" SelectedIndex="0" ValueType="System.String">
                                <Items>
                                <dxe:ListEditItem Text="آدرس محل سکونت" Value="0" Selected="True"/>
                                <dxe:ListEditItem Text="آدرس محل کار" Value="1" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="LetterInputs">
                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                                </td>
                            </tr>
                            <tr>
                            <td valign="top" style="width: 89px">
                            <dxe:ASPxLabel runat="server" Text="انتخاب نوع چاپ"  ID="ASPxLabel2"  
                                            >
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top">
                            <TSPControls:CustomAspxComboBox runat="server"  Width="180px" 
                                 ID="comboPrintType" 
                                 ClientInstanceName="comboPrintType" 
                                EnableIncrementalFiltering="True" HorizontalAlign="Right" SelectedIndex="0" ValueType="System.String">
                                <Items>
                                <dxe:ListEditItem Text="چاپ پشت سر هم" Value="0" Selected="True" />
                                <dxe:ListEditItem Text="چاپ صفحه به صفحه" Value="1" />
                                </Items><ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="LetterInputs">
                                    
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <dxe:ASPxLabel runat="server" Text="دبیرخانه"  ID="ASPxLabel3"  
                                            >
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxComboBox runat="server"  Width="180px" 
                                 ID="comboSecretariat" SelectedIndex="0"
                                 ClientInstanceName="comboSecretariat" 
                                EnableIncrementalFiltering="True" HorizontalAlign="Right" ValueType="System.Int32" DataSourceID="ObjectDataSourceSecretariat" TextField="SName" ValueField="SId">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="LetterInputs">
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                    <asp:ObjectDataSource ID="ObjectDataSourceSecretariat" runat="server" SelectMethod="SelectAutomationSecretariatByEmId"
                                        TypeName="TSP.DataManager.Automation.SecretariatManager">
                                        <SelectParameters>
                                            <asp:Parameter Name="EmpId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                            <td align="center" colspan="2">
                            <br />
                            <TSPControls:CustomAspxButton runat="server" Text="تایید" ToolTip="تایید" 
                                CausesValidation="False" ID="btnClosePopupChooseSender" AutoPostBack="False" 
                                UseSubmitBehavior="False" EnableViewState="False"  EnableTheming="False" ClientInstanceName="btnClosePopup">
                                <ClientSideEvents Click="popupChooseDetails.Hide()"></ClientSideEvents>
                                <Image Height="10px" Width="10px" Url="~/Images/icons/Check.png"></Image>
                            </TSPControls:CustomAspxButton>
                            </td>
                            
                            </tr>
                        </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
           
</TSPControls:CustomASPxPopupControl>
</div>