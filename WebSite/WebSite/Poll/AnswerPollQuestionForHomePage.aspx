<%@ Page Title="سوالات نظرسنجی" Language="C#" MasterPageFile="~/MasterPageWebsite.master"
    AutoEventWireup="true" CodeFile="AnswerPollQuestionForHomePage.aspx.cs" Inherits="Poll_AnswerPollQuestionForHomePage" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPollAnswer" HeaderText="سوالات نظر سنجی"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <dxp:ASPxPanel ID="PanelQuestions" runat="server" Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>
                                    <table width="100%">
                                        <tr>
                                            <td align="center" valign="top">
                                                <TSPControls:CustomAspxDevDataView ID="DataViewPollQuestion" ClientInstanceName="DataViewPollQuestion"
                                                    runat="server" ColumnCount="1" RowPerPage="100" Width="100%" 
                                                    RightToLeft="True" ItemSpacing="0px" PagerStyle-ItemSpacing="0px" Border-BorderStyle="None">
                                                    <%--OnCustomCallback="OnCustomCallback_DataViewPollQuestion">--%>
                                                    <ItemStyle Height="1px" Width="1px" Paddings-Padding="2px" />
                                                    <ItemTemplate>
                                                        <table class="TableBorder" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="top" align="right"  colspan="2">
                                                                        <dxe:ASPxLabel ID="lblQustion" runat="server" Width="100%" Text='<%# Bind("Question") %>'
                                                                            Wrap="True">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="top" colspan="2">
                                                                        <dxe:ASPxRadioButtonList  
                                                                            Border-BorderColor="Transparent" ID="rdbChoise" TextField="ChoiseName" ValueField="ChoiseId"
                                                                            runat="server" Width="100%" DataSourceID="objdsChoise">
                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Left" ErrorDisplayMode="ImageWithTooltip"
                                                                                ValidationGroup="PollChoise">
                                                                                <RequiredField IsRequired="true" ErrorText="گزینه را انتخاب نمایید" />
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxRadioButtonList>
                                                                        <asp:ObjectDataSource ID="objdsChoise" runat="server" SelectMethod="FindByQuestionId"
                                                                            TypeName="TSP.DataManager.PollChoiseManager" OldValuesParameterFormatString="original_{0}">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="lblQId" DefaultValue="-1" Name="QuestionId" PropertyName="Text"
                                                                                    Type="Int16" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                        <dxe:ASPxLabel ID="lblQId" Visible="false" runat="server" Text='<%# Bind("QuestionId") %>'>
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" valign="top" width="15%" >
                                                                        توضیحات
                                                                    </td>
                                                                    <td align="right" valign="top" width="85%">
                                                                        <TSPControls:CustomASPXMemo runat="server" ID="txtDescription"  Width="100% "
                                                                            >
                                                                        </TSPControls:CustomASPXMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="top"  colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="top" colspan="2">
                                                                        <dxe:ASPxLabel ID="lblPollReport" runat="server" Text="" ForeColor="Red">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                    <PagerSettings Visible="false">
                                                    </PagerSettings>
                                                </TSPControls:CustomAspxDevDataView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top">
                                                <TSPControls:CustomAspxButton runat="server" 
                                                    Text="ثبت نظر" ImagePosition="left"  
                                                    ID="btnSavePollAnswer" ClientInstanceName="btnSavePollAnswer" UseSubmitBehavior="False"
                                                    AutoPostBack="true" CausesValidation="true" ValidationGroup="PollChoise" OnClick="btnSavePollAnswer_OnClick"
                                                    OnLoad="btnSavePollAnswer_OnLoad">
                                                    <Image Width="16px" Height="16px" Url="~/Images/save.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                        <dxp:ASPxPanel ID="PanelSucces" runat="server" Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>
                                    <table>
                                        <tr>
                                            <td align="right" valign="middle" width="15%">
                                                <asp:Image ID="imgPol" runat="server" ImageUrl="~/Images/Poll.png" />
                                            </td>
                                            <td width="85%" align="center" valign="middle">
                                                <dxe:ASPxLabel ID="lblSucessMsg" Wrap="False" Font-Bold="true" Font-Size="10pt" ForeColor="Green"
                                                    runat="server" Text="اطلاعات با موفقیت ثبت گردید.با تشکر از شرکت شما در نظر سنجی ....">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <dx:ASPxHiddenField ID="HiddenFieldPage" runat="server">
            </dx:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
