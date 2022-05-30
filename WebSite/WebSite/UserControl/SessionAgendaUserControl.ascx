<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SessionAgendaUserControl.ascx.cs"
    Inherits="SessionAgendaUserControl" %>
<%--<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register TagPrefix="TSP" Namespace="TSP" %>
<script type="text/javascript">
    function ShowSessionAgenda() {
        Clear();
        PopupSessionAgenda.Show();
    }

    function ShowSessionAgenda(TableType, TableId) {
        Clear();
        hiddenData.Set('TableId', TableId);
        hiddenData.Set('TableType', TableType);
        PopupSessionAgenda.Show();
    }

    function Clear() {
        PanelInputSessionAgenda.SetVisible(true);
        PanelMessageSessionAgenda.SetVisible(false);
        hiddenData.Set('TableId', '');
        hiddenData.Set('TableType', '');
        hiddenData.Set('SessionId', '');
        hiddenData.Set('RequestId', '');
        txtSessionNumber.SetText('');
        txtSessionDateTime.SetText('');
        txtSessionTitle.SetText('');
        txtTitleSesionAgenda.SetText('');
        txtDetailSesionAgenda.SetText('');
        cmbTypeSesionAgenda.SetSelectedIndex(-1);
        UploadAttachSessionAgenda.ClearText();
        imgEndUploadSessionAgenda.SetVisible(false);
        txtDescriptionSesionAgenda.SetText('');
        PanelInputSessionAgenda_Agenda.SetVisible(false);
    }
</script>
<TSPControls:CustomASPxPopupControl ID="PopupSessionAgenda" runat="server" 
      CloseAction="CloseButton"
    HeaderText="ثبت دستورجلسه" RightToLeft="True" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" AllowDragging="true" Width="550px" ClientInstanceName="PopupSessionAgenda">
    <LoadingPanelImage Url="~/App_Themes/Glass/Web/Loading.gif">
    </LoadingPanelImage>
    <HeaderStyle>
        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
    </HeaderStyle>
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelSessionAgenda" runat="server" Width="100%"
                RightToLeft="True"  
                 ClientInstanceName="CallbackPanelSessionAgenda"
                OnCallback="CallbackPanelSessionAgenda_Callback" LoadingDivStyle-BackColor="Black">
                <SettingsLoadingPanel Text="در حال بارگذاری" />
                <LoadingPanelImage Url="~/App_Themes/Glass/Web/Loading.gif">
                </LoadingPanelImage>
                <PanelCollection>
                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                        <dxp:ASPxPanel ID="PanelInput" runat="server" ClientInstanceName="PanelInputSessionAgenda">
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dxrp:ASPxRoundPanel ID="RoundPanelSession" runat="server" Width="100%" 
                                        BackColor="#EBF2F4"  HeaderText="مشخصات جلسه"
                                        HorizontalAlign="Center" RightToLeft="True" Font-Size="8pt">
                                        <BottomRightCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpBottomRightCorner.png">
                                        </BottomRightCorner>
                                        <HeaderContent>
                                            <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpHeaderBack.gif">
                                            </BackgroundImage>
                                        </HeaderContent>
                                        <ContentPaddings PaddingTop="10PX" PaddingRight="3PX" PaddingLeft="3PX" PaddingBottom="5PX">
                                        </ContentPaddings>
                                        <NoHeaderTopRightCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpNoHeaderTopRightCorner.png">
                                        </NoHeaderTopRightCorner>
                                        <RightEdge>
                                            <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpLeftRightEdge.gif">
                                            </BackgroundImage>
                                        </RightEdge>
                                        <HeaderRightEdge>
                                            <BackgroundImage VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpHeaderRightEdge.gif">
                                            </BackgroundImage>
                                        </HeaderRightEdge>
                                        <Border BorderWidth="1px" BorderStyle="Solid" BorderColor="#7EACB1"></Border>
                                        <HeaderStyle HorizontalAlign="Right" BackColor="White" Height="23px">
                                            <Paddings PaddingTop="0px" PaddingBottom="0px" PaddingLeft="2px"></Paddings>
                                            <BorderBottom BorderStyle="None"></BorderBottom>
                                        </HeaderStyle>
                                        <Content>
                                            <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpContentBack.gif">
                                            </BackgroundImage>
                                        </Content>
                                        <HeaderLeftEdge>
                                            <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpHeaderLeftEdge.gif">
                                            </BackgroundImage>
                                        </HeaderLeftEdge>
                                        <BottomEdge BackColor="#D7E9F1">
                                        </BottomEdge>
                                        <LeftEdge>
                                            <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpLeftRightEdge.gif">
                                            </BackgroundImage>
                                        </LeftEdge>
                                        <TopRightCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpTopRightCorner.png">
                                        </TopRightCorner>
                                        <NoHeaderTopLeftCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpNoHeaderTopLeftCorner.png">
                                        </NoHeaderTopLeftCorner>
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent3" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td width="15%" valign="top">
                                                            شماره جلسه *
                                                        </td>
                                                        <td width="30%">
                                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                                ID="txtSessionNumber" RightToLeft="True" 
                                                                ClientInstanceName="txtSessionNumber" HorizontalAlign="Left">
                                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                                                    ValidationGroup="SeesionAgenda">
                                                                    <RequiredField IsRequired="true" ErrorText="شماره جلسه را وارد نمایید" />
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                                <ClientSideEvents TextChanged="function(s, e) { 
                                                                if(s.GetText()!='')
                                                                  CallbackPanelSessionAgenda.PerformCallback('SessionNumber'); }" />
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td width="5%" align="right">
                                                        </td>
                                                        <td width="20%">
                                                            تاریخ و زمان برگزاری
                                                        </td>
                                                        <td width="30%" valign="top">
                                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                                ID="txtSessionDateTime" RightToLeft="True" 
                                                                ReadOnly="true" ClientInstanceName="txtSessionDateTime">
                                                                <ReadOnlyStyle BackColor="Snow">
                                                                </ReadOnlyStyle>
                                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td colspan="4">
                                                            <dxe:ASPxLabel ID="lblSesionError" runat="server" Text="" Visible="false" ForeColor="DarkRed">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            موضوع جلسه
                                                        </td>
                                                        <td colspan="4">
                                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                                ID="txtSessionTitle" RightToLeft="True" ReadOnly="true" ClientInstanceName="txtSessionTitle">
                                                                <ReadOnlyStyle BackColor="Snow">
                                                                </ReadOnlyStyle>
                                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                        <TopLeftCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpTopLeftCorner.png">
                                        </TopLeftCorner>
                                        <NoHeaderTopEdge BackColor="#EBF2F4">
                                        </NoHeaderTopEdge>
                                        <BottomLeftCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpBottomLeftCorner.png">
                                        </BottomLeftCorner>
                                    </dxrp:ASPxRoundPanel>
                                    <dxp:ASPxPanel ID="PanelInputAgenda" runat="server" ClientInstanceName="PanelInputSessionAgenda_Agenda">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent4" runat="server" SupportsDisabledAttribute="True">
                                                <br />
                                                <dxrp:ASPxRoundPanel ID="RoundPanelAgenda" runat="server" Width="100%" 
                                                    BackColor="#EBF2F4"  HeaderText="مشخصات دستورجلسه"
                                                    HorizontalAlign="Center" RightToLeft="True" Font-Size="8pt">
                                                    <BottomRightCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpBottomRightCorner.png">
                                                    </BottomRightCorner>
                                                    <HeaderContent>
                                                        <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpHeaderBack.gif">
                                                        </BackgroundImage>
                                                    </HeaderContent>
                                                    <ContentPaddings PaddingTop="10PX" PaddingRight="3PX" PaddingLeft="3PX" PaddingBottom="5PX">
                                                    </ContentPaddings>
                                                    <NoHeaderTopRightCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpNoHeaderTopRightCorner.png">
                                                    </NoHeaderTopRightCorner>
                                                    <RightEdge>
                                                        <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpLeftRightEdge.gif">
                                                        </BackgroundImage>
                                                    </RightEdge>
                                                    <HeaderRightEdge>
                                                        <BackgroundImage VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpHeaderRightEdge.gif">
                                                        </BackgroundImage>
                                                    </HeaderRightEdge>
                                                    <Border BorderWidth="1px" BorderStyle="Solid" BorderColor="#7EACB1"></Border>
                                                    <HeaderStyle HorizontalAlign="Right" BackColor="White" Height="23px">
                                                        <Paddings PaddingTop="0px" PaddingBottom="0px" PaddingLeft="2px"></Paddings>
                                                        <BorderBottom BorderStyle="None"></BorderBottom>
                                                    </HeaderStyle>
                                                    <Content>
                                                        <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpContentBack.gif">
                                                        </BackgroundImage>
                                                    </Content>
                                                    <HeaderLeftEdge>
                                                        <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpHeaderLeftEdge.gif">
                                                        </BackgroundImage>
                                                    </HeaderLeftEdge>
                                                    <BottomEdge BackColor="#D7E9F1">
                                                    </BottomEdge>
                                                    <LeftEdge>
                                                        <BackgroundImage Repeat="RepeatX" VerticalPosition="bottom" ImageUrl="~/App_Themes/Glass/Web/rpLeftRightEdge.gif">
                                                        </BackgroundImage>
                                                    </LeftEdge>
                                                    <TopRightCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpTopRightCorner.png">
                                                    </TopRightCorner>
                                                    <NoHeaderTopLeftCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpNoHeaderTopLeftCorner.png">
                                                    </NoHeaderTopLeftCorner>
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent1" runat="server">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="15%">
                                                                        عنوان *
                                                                    </td>
                                                                    <td width="85%">
                                                                        <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                                            ID="txtAgendaTitle" RightToLeft="True" 
                                                                            ClientInstanceName="txtTitleSesionAgenda">
                                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                                                                ValidationGroup="SeesionAgenda">
                                                                                <RequiredField IsRequired="true" ErrorText="عنوان را وارد نمایید" />
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        شرح *
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomASPXMemo runat="server" Height="70px" Width="100%"  
                                                                            ID="txtDetail" RightToLeft="True" ClientInstanceName="txtDetailSesionAgenda">
                                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                                                                ValidationGroup="SeesionAgenda">
                                                                                <RequiredField IsRequired="true" ErrorText="شرح دستورجلسه را وارد نمایید" />
                                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomASPXMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        نوع *
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjectDataSourceAgendaTypes"
                                                                            Width="100%" TextField="TypeName" ValueField="TypeId" 
                                                                              ID="cmbAgendaType"
                                                                            RightToLeft="True" ClientInstanceName="cmbTypeSesionAgenda">
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SeesionAgenda">
                                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                                <RequiredField ErrorText="نوع دستورجلسه را انتخاب نمایید" IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        فایل پیوست
                                                                    </td>
                                                                    <td>
                                                                        <table cellpadding="0px" cellspacing="0px">
                                                                            <tr>
                                                                                <td dir="rtl" align="right">
                                                                                    <TSPControls:CustomAspxUploadControl ID="UploadAttach" runat="server" Width="400px"
                                                                                        InputType="Files" ClientInstanceName="UploadAttachSessionAgenda" OnFileUploadComplete="UploadAttach_FileUploadComplete"
                                                                                        UploadWhenFileChoosed="true">
                                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {  
if(e.isValid)
 imgEndUploadSessionAgenda.SetVisible(true);
else 
 imgEndUploadSessionAgenda.SetVisible(false);
}"></ClientSideEvents>
                                                                                    </TSPControls:CustomAspxUploadControl>
                                                                                </td>
                                                                                <td valign="top">
                                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/Images/button_ok.png" ClientInstanceName="imgEndUploadSessionAgenda"
                                                                                        ClientVisible="False" ToolTip="فایل انتخاب شد" ID="imgEndUpload">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        توضیحات
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomASPXMemo runat="server" Height="46px" Width="100%"  
                                                                            ID="txtDescription" RightToLeft="True" ClientInstanceName="txtDescriptionSesionAgenda">
                                                                            <ValidationSettings>
                                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomASPXMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                    <TopLeftCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpTopLeftCorner.png">
                                                    </TopLeftCorner>
                                                    <NoHeaderTopEdge BackColor="#EBF2F4">
                                                    </NoHeaderTopEdge>
                                                    <BottomLeftCorner Width="5px" Height="5px" Url="~/App_Themes/Glass/Web/rpBottomLeftCorner.png">
                                                    </BottomLeftCorner>
                                                </dxrp:ASPxRoundPanel>
                                                <br />
                                                <div align="center" style="width: 100%">
                                                    <asp:Label ID="lblErrorInSave" runat="server" ForeColor="DarkRed" Visible="false">
                                                    </asp:Label>
                                                    <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;ذخیره" 
                                                         ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False"
                                                        Width="120px" ClientInstanceName="btnSaveSessionAgenda" >
                                                        <ClientSideEvents Click="function(s, e) {	
if(!ASPxClientEdit.ValidateGroup('SeesionAgenda'))
	return;
if(hiddenData.Get('SessionId')=='' || hiddenData.Get('RequestId')=='')
{
    txtSessionNumber.SetIsValid(false);
    return;
}
CallbackPanelSessionAgenda.PerformCallback('Save');
}"></ClientSideEvents>
                                                        <Image Width="16px" Height="16px" Url="~/Images/WorkFlow_Save.png" />
                                                    </TSPControls:CustomAspxButton>
                                                </div>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                        <dxp:ASPxPanel ID="PanelMessage" runat="server" ClientInstanceName="PanelMessageSessionAgenda"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                    <div align="center" style="width: 100%">
                                        <asp:Label ID="lblMessage" runat="server">
                                        </asp:Label>
                                        <br />
                                        <br />
                                        <TSPControls:CustomAspxButton runat="server" Text="خروج" 
                                             ID="ASPxButton1" AutoPostBack="False" UseSubmitBehavior="False"
                                            Width="100px" >
                                            <ClientSideEvents Click="function(s, e) { PopupSessionAgenda.Hide(); }"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </div>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                        <dxhf:ASPxHiddenField ID="hiddenData" ClientInstanceName="hiddenData" runat="server">
                        </dxhf:ASPxHiddenField>
                        <asp:ObjectDataSource ID="ObjectDataSourceAgendaTypes" runat="server" SelectMethod="FilterByGroup"
                            TypeName="TSP.DataManager.Session.AgendaTypesManager">
                            <SelectParameters>
                                <asp:Parameter Type="Int32" Name="Group" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</TSPControls:CustomASPxPopupControl>--%>
