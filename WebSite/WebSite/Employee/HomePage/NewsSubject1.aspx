<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="NewsSubject1.aspx.cs" Inherits="Employee_News_NewsSubject1"
    Title="مدیریت موضوعات خبر" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function CheckFileUpload() {
            if (HiddenFieldUploadControl.Get('HasFile') == '0') {
                lblerrorupload.SetVisible(true);
                return false;
            }
            lblerrorupload.SetVisible(false);
            return true;
        }
    </script>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" AutoPostBack="false" EnableViewState="False" Text=" " ToolTip="جدید"
                                                                        UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
    CallbackPanelPopup.PerformCallback('New');
	
}" />
                                                                       
                                                                        <Image  Url="~/Images/icons/new.png"  />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" AutoPostBack="false" EnableViewState="False" Text=" "
                                                                        ToolTip="ویرایش"  UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
CallbackPanelPopup.PerformCallback('Edit');
}" /> 
                                                                        <Image  Url="~/Images/icons/edit.png"  />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                         EnableTheming="False" EnableViewState="False"
                                                                        OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" /> 
                                                                        <Image  Url="~/Images/icons/delete.png"   />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                      EnableViewState="False"
                    DataSourceID="ObjectDataSource1" KeyFieldName="SubId" Width="100%">
                   
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="SubId" Name="SubId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="موضوع" FieldName="Name" Name="Name" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="ImageUrl" VisibleIndex="0">
                            <PropertiesImage ImageHeight="30px" ImageWidth="30px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                    </Columns>
                    
                </TSPControls:CustomAspxDevGridView>
                <TSPControls:CustomAspxCallbackPanel runat="server"
                    ClientInstanceName="CallbackPanelPopup" Width="100%" ID="CallbackPanelPopup"
                    OnCallback="CallbackPanelPopup_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
     if(s.cpIsFormOK==1)
       pop.Show();	
}" />
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent3" runat="server">
                            <dx:ASPxHiddenField ID="HiddenFieldModeID" ClientInstanceName="HiddenFieldModeID"
                                runat="server">
                            </dx:ASPxHiddenField>
                            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True"
                                ClientInstanceName="pop" CloseAction="CloseButton" 
                                 HeaderText="جدید" Modal="true" 
                                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table dir="rtl" style="text-align: right">
                                            <tbody>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <asp:Label ID="Label58" runat="server" Text="موضوع" Width="68px"></asp:Label>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" ID="txtSub" runat="server" ClientInstanceName="sub" 
                                                             Width="200px">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                
                                                                <RequiredField ErrorText="موضوع را وارد نمایید" IsRequired="True" />
                                                                <RegularExpression ErrorText="" />
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <asp:Label ID="Label1" runat="server" Text="تصویر" Width="68px"></asp:Label>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <table width="100%">
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align="right">
                                                                                <TSPControls:CustomAspxUploadControl ID="FileUploadIcon" runat="server" ClientInstanceName="flp"
                                                                                    InputType="Images" UploadWhenFileChoosed="True" OnFileUploadComplete="FileUploadIcon_FileUploadComplete"
                                                                                    >
                                                                                   
                                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
    HiddenFieldUploadControl.Set('HasFile','1');
    lblerrorupload.SetVisible(false);  
    imgNewsSubject.SetVisible(true);    
    imgNewsSubject.SetImageUrl('../../Image/Temp/' + e.callbackData);
	}
	else{
	imgEndUploadImgClient.SetVisible(false);
    HiddenFieldUploadControl.Set('HasFile','0');
    lblerrorupload.SetVisible(false); 
    imgNewsSubject.SetVisible(false);  
    imgNewsSubject.SetImageUrl('');
	}
}" />
                                                                                    <CancelButton Text="انصراف">
                                                                                    </CancelButton>
                                                                                </TSPControls:CustomAspxUploadControl>
                                                                            </td>
                                                                            <td valign="top" align="right">
                                                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgEndUploadImgClient"
                                                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel ID="lblerrorupload" runat="server" ClientInstanceName="lblerrorupload"
                                                                        ClientVisible="False" ForeColor="Red" Text="تصویر را انتخاب نمایید">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <dxe:ASPxImage ID="imgNewsSubject" ClientVisible="false" runat="server" ClientInstanceName="imgNewsSubject"
                                                                        Height="50px" Width="50px">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="Red" Text="ابعاد تصویر را حداکثر 24*24 انتخاب کنید">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" valign="top">
                                                        <br />
                                                        <TSPControls:CustomAspxButton  ID="btnSave" runat="server" 
                                                             OnClick="btnSave_Click" Text="ذخیره" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
  if(CheckFileUpload()==false)  e.processOnServer=false;
  if(sub.GetIsValid())
  {
	pop.Hide();
  }

}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                                <HeaderStyle>
                                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                                </HeaderStyle>
                                <SizeGripImage Height="12px" Width="12px" />
                                <CloseButtonImage Height="17px" Width="17px" />
                            </TSPControls:CustomASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomAspxCallbackPanel>
                <br />
              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                                                        EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
CallbackPanelPopup.PerformCallback('New');
}" />
                                                                      
                                                                        <Image  Url="~/Images/icons/new.png"  />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                                                        EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="ویرایش" 
                                                                        UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
CallbackPanelPopup.PerformCallback('Edit');
}" />
                                                                     
                                                                        <Image  Url="~/Images/icons/edit.png"  />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                         EnableTheming="False" EnableViewState="False"
                                                                        OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                                      
                                                                        <Image  Url="~/Images/icons/delete.png"  />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                               </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img alt="" align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
            TypeName="TSP.DataManager.NewsSubjectManager">
        </asp:ObjectDataSource>
        <dx:ASPxHiddenField ID="HiddenFieldUploadControl" runat="server" ClientInstanceName="HiddenFieldUploadControl">
        </dx:ASPxHiddenField>
 
</asp:Content>
