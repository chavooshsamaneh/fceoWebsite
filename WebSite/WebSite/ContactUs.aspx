<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" Title="تماس با ما" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="Test">
        <ContentTemplate>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel4" HeaderText="اطلاعات سازمان" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table align="center" dir="rtl" border="0" cellpadding="0" cellspacing="0" style="border-left-color: #ffffff; border-bottom-color: #ffffff; vertical-align: top; border-top-color: #ffffff; border-collapse: collapse; height: auto; border-right-color: #ffffff"
                            width="100%">
                            <tr>
                                <td align="justify" width="100%">
                                    <br />
                                    <blockquote>
                                        <asp:Label ID="labelNezam" runat="server"></asp:Label>
                                    </blockquote>
                                </td>
                            </tr>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <asp:ObjectDataSource ID="ObjectDataSourceIntroduction" runat="server"
                SelectMethod="GetData" TypeName="TSP.DataManager.IntroductionManager"></asp:ObjectDataSource>

            <asp:Panel ID="PanelPublicContact" runat="server">
                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="تماس با ما" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <asp:Panel ID="panelSend" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblSendError" runat="server" Text="خطایی در اسال پیام بوجود آمده است<br><br>"
                                                Font-Size="11pt" ForeColor="Brown" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" align="right">نام
                                        </td>
                                        <td width="50%" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtName" MaxLength="100" ClientInstanceName="txtName" runat="server"
                                                Width="100%">
                                                <ValidationSettings>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td width="30%" rowspan="6" valign="middle" align="center">
                                            <asp:Image ID="imgContact" runat="server" ImageUrl="~/Images/contact-icon.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">شماره تماس&nbsp;
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtTelNo" MaxLength="50" ClientInstanceName="txtTelNo" runat="server"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="شماره تماس فقط باید عدد باشد" ValidationExpression="\d*" />


                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">آدرس پست الكترونيكي&nbsp;
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtEmail" MaxLength="50" ClientInstanceName="txtEmail" runat="server"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                    <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نماييد" IsRequired="True" />

                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">نوع
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxComboBox ID="cmbMessageType" ClientInstanceName="cmbMessageType" DataSourceID="ObjectDataSource_MessageType"
                                                ValueField="TypeId" TextField="TypeName" runat="server"
                                                ValueType="System.String"
                                                HorizontalAlign="Right" Width="100%" RightToLeft="True">
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSource_MessageType" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.PublicMessageTypesManager"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">بخش
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxComboBox ID="cmbMessageGroup" ClientInstanceName="cmbMessageGroup" DataSourceID="ObjectDataSource_MessageGroup"
                                                ValueField="NezamChartId" TextField="GroupName" runat="server"
                                                ValueType="System.String"
                                                HorizontalAlign="Right" Width="100%" RightToLeft="True">
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSource_MessageGroup" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.PublicMessageGroupsManager"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">موضوع&nbsp;
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtSubject" MaxLength="150" ClientInstanceName="txtSubject"
                                                runat="server" Width="100%">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">متن پیام&nbsp;
                                        </td>
                                        <td colspan="2" align="right">
                                            <TSPControls:CustomASPXMemo ID="txtBody" ClientInstanceName="txtBody" runat="server" Height="120px"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="متن پیام را وارد نماييد" IsRequired="True" />

                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div class="row">
                                    <div class="col-md-6" align="left">
                                        <TSPControls:CustomAspxButton runat="server" Text="ارسال پیام" ID="btnSend"
                                            EncodeHtml="false" OnClick="btnSend_Click">
                                        </TSPControls:CustomAspxButton>
                                    </div>
                                    <div class="col-md-6"  align="right">
                                        <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                            Text="پاک کردن فرم" ID="btnClear" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
	txtName.SetText('');
	txtTelNo.SetText('');
	txtEmail.SetText('');
	txtSubject.SetText('');
	txtBody.SetText('');
	if(cmbMessageType.GetItemCount()&gt;0)
        cmbMessageType.SetSelectedIndex(0);
	if(cmbMessageGroup.GetItemCount()&gt;0)
        cmbMessageGroup.SetSelectedIndex(0);
}" />
                                        </TSPControls:CustomAspxButton>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panelSendFinish" runat="server" Visible="false">
                                <br />
                                <br />
                                <asp:Label ID="lblSendFinish" runat="server" Text="پیغام شما با موفقیت ارسال شد."
                                    Font-Bold="True" Font-Size="11pt" ForeColor="DarkGreen"></asp:Label>
                                <br />
                                <br />
                                <br />
                            </asp:Panel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="Test" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
