<%@ Page Title="مشخصات سوالات نظرسنجی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="QuestionInsert.aspx.cs" Inherits="Employee_Poll_QuestionInsert" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">

                                        <Image Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                        ID="btnEdit" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">

                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">

                                        <Image Url="~/Images/icons/back.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            </br>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" ClientInstanceName="RoundPanelMain"
                HeaderText="مشاهده" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <fieldset class="HelpUL" id="RoundPanelQuestion">
                            <legend>مشخصات سوال</legend>
                            <table width="100%">
                                <tr>
                                    <td width="15%" valign="top" align="right">متن سوال
                                    </td>
                                    <td width="85%" valign="top" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" ID="txtQuestion" Width="100%"
                                            Height="37px">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="true" ErrorText="متن سوال را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" colspan="2">
                                        <TSPControls:CustomASPxCheckBox Visible="false" runat="server" ID="chbIsCompulsory" Checked="true" Text="پاسخ به این سوال اجباری می باشد."
                                            RightToLeft="True">
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset class="HelpUL" id="RoundPanelChoice">
                            <legend>گزینه ها</legend>
                            <table width="100%">
                                <tr>
                                    <td width="15%">گزینه
                                    </td>
                                    <td width="85%" valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtChoice" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="valchoise">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="true" ErrorText="متن گزینه را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" valign="middle">
                                        <TSPControls:CustomAspxButton runat="server"
                                            Text="اضافه به لیست" ImagePosition="Right"
                                            ID="btnAddChoise" OnClick="btnAddChoise_Click" UseSubmitBehavior="False" ValidationGroup="valchoise">
                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top" align="center">
                                        <TSPControls:CustomAspxDevGridView2 runat="server" KeyFieldName="ChoiseId" AutoGenerateColumns="False" Width="100%" ID="GridViewChoise" ClientInstanceName="GridViewChoise" OnRowDeleting="GridViewChoise_RowDeleting">
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Name="clmnDelete"
                                                    ToolTip="حذف" ShowDeleteButton="true">
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="ChoiseName" Width="500px" Caption="گزینه"
                                                    VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                        </TSPControls:CustomAspxDevGridView2>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>


                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            </br>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">

                                        <Image Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                        ID="btnEdit2" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">

                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">

                                        <Image Url="~/Images/icons/back.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dx:ASPxHiddenField ID="HiddenFieldPoll" runat="server">
            </dx:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
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
