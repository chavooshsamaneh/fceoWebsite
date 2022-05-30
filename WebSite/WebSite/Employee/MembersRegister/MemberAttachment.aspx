<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberAttachment.aspx.cs" Inherits="Employee_MembersRegister_MemberAttachment"
    Title="مستندات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">

        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'FilePath;Description', SetValue);
        }
        function SetValue(values) {
            var d = values[0];
            if (d != null && d != '') {
                d = d.replace('~/', '');
                d = '../' + d;
            }

            img.SetImageUrl(d);

            desc.SetText(values[1]);

        }

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
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                    <table >
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                                                    
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                    <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
                                                if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                 
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                  
                                                    <Image  Url="~/Images/icons/Back.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                                    CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                  
                                                    <Image  Url="../../Images/icons/BakToManagment.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                               
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuTop" runat="server" 
                        OnItemClick="MenuTop_ItemClick" >
                        <Items>
                            <dxm:MenuItem Name="Request" Text="مشخصات عضو">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Language" Text="زبان ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attachment" Text="مستندات" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مالی" Name="AccFish">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Message" Text="پیام ها" Visible="false">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="گزارش تنظیمات" Name="PollAnswer">
                            </dxm:MenuItem>
                        </Items>
                       
                    </TSPControls:CustomAspxMenuHorizontal>
         
                <br />
                <div style="width: 100%; text-align: right; display: none">
                    <dxe:ASPxLabel ID="lblSex" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblT" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblOfName" runat="server">
                    </dxe:ASPxLabel>
                </div>
                <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
                <br /> 
                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="100%"
                        OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" OnHtmlRowCreated="CustomAspxDevGridView1_HtmlRowCreated"
                        OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" ClientInstanceName="grid"
                        KeyFieldName="AttachId" AutoGenerateColumns="False" OnPageIndexChanged="CustomAspxDevGridView1_PageIndexChanged">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AttachId"
                                Name="AttachId">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataHyperLinkColumn Visible="False" VisibleIndex="0" FieldName="FilePath"
                                Caption="فایل" Name="FilePath">
                                <PropertiesHyperLinkEdit Target="_blank">
                                </PropertiesHyperLinkEdit>
                            </dxwgv:GridViewDataHyperLinkColumn>
                            <dxwgv:GridViewDataImageColumn Visible="False" VisibleIndex="0" FieldName="FilePath"
                                Caption="تصویر" Name="FilePath">
                                <PropertiesImage ImageWidth="75px" ImageHeight="75px">
                                </PropertiesImage>
                            </dxwgv:GridViewDataImageColumn>
                            <dxwgv:GridViewDataTextColumn Width="300px" VisibleIndex="0" Caption="فایل">
                                <DataItemTemplate>
                                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" Target="_blank"
                                        NavigateUrl='<%# Bind("FilePath") %>' OnDataBinding="ASPxHyperLink1_DataBinding">
                                    </dxe:ASPxHyperLink>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption=" " VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Width="300px" VisibleIndex="1" FieldName="Description"
                                Caption="توضیحات" Name="Description">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
 
                <br />
                <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" 
                      ClientInstanceName="pop"
                    PopupElementID="btnSearch1"
                    HeaderText="جدید" >
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table>
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <table width="100%">
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <table width="100%">
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomAspxUploadControl ID="flp" runat="server" ClientInstanceName="flpc"
                                                                        InputType="Files" UploadWhenFileChoosed="True" OnFileUploadComplete="flp_FileUploadComplete"
                                                                        Width="200px">
                                                                      
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
    HiddenFieldUploadControl.Set('HasFile','1');
    lblerrorupload.SetVisible(false);    
	}
	else{
	imgEndUploadImgClient.SetVisible(false);
    HiddenFieldUploadControl.Set('HasFile','0');
    lblerrorupload.SetVisible(false); 
	}
}" />
                                                                        <CancelButton Text="انصراف">
                                                                        </CancelButton>
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxImage ID="imgEndUploadImg" runat="server" ClientInstanceName="imgEndUploadImgClient"
                                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="فایل انتخاب شد">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel ID="lblerrorupload" runat="server" ClientInstanceName="lblerrorupload"
                                                            ClientVisible="False" ForeColor="Red" Text="فایل را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtDesc"  Width="328px"
                                                >
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
                                    <tr>
                                        <td align="center" colspan="2">
                                            <br />
                                            <TSPControls:CustomAspxButton  runat="server" Text="ذخیره"  ID="btnSave" UseSubmitBehavior="False"
                                                 OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
                                                  if(CheckFileUpload()==false)  e.processOnServer=false;
	pop.Hide();
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle>
                        <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px">
                    </SizeGripImage>
                    <CloseButtonImage Height="17px" Width="17px">
                    </CloseButtonImage>
                </TSPControls:CustomASPxPopupControl>
                <asp:HiddenField ID="MemberId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="MemberRequest" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                    <table >
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                                                  
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                    <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
                                                 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                   
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                  
                                                    <Image  Url="~/Images/icons/Back.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضا"
                                                    CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                  
                                                    <Image  Url="../../Images/icons/BakToManagment.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                               </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dx:ASPxHiddenField ID="HiddenFieldUploadControl" runat="server" ClientInstanceName="HiddenFieldUploadControl">
                </dx:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
    
</asp:Content>
