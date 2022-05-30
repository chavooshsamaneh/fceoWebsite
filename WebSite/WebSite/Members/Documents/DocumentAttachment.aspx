<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="DocumentAttachment.aspx.cs" Inherits="Members_Documents_DocumentAttachment"
    Title="مدارک پیوست پروانه اشتغال" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table  >
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="BtnNew" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text="جدید" ToolTip="جدید" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
//flp.IsValid=true;	
PopupAttachment.Show();
}" /> 
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text="حذف"
                                ToolTip="حذف" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" /> 
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnBack" runat="server" CausesValidation="False"
                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text="بازگشت"
                                ToolTip="بازگشت" UseSubmitBehavior="False"> 
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="مدیریت پروانه اشتغال به کار" ToolTip="مدیریت پروانه اشتغال به کار"
                                CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False" OnClick="btnBackToManagment_Click"> 
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server" OnItemClick="MenuMemberFile_ItemClick">
        <Items>
            <dxm:MenuItem Name="Major" Text="مشخصات پروانه" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
            <dxm:MenuItem Name="JobHistory" Text="سابقه کار" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
            <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Exam" Text="آزمون ها" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Periods" Text="دوره آموزشی" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
            <dxm:MenuItem Name="MeDetail" Text="پایه - صلاحیت" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
            <dxm:MenuItem Text="مدارک پیوست" Name="Attachment" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
            <dxm:MenuItem Text="ظرفیت اشتغال" Name="Capacity" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
            </dxm:MenuItem>
        </Items>

    </TSPControls:CustomAspxMenuHorizontal>

    <TSPControls:CustomASPxPopupControl ID="PopupAttachment" runat="server" ClientInstanceName="PopupAttachment"
        HeaderText="جدید"
        Height="23px" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowPageScrollbarWhenModal="True" Width="402px"
        CloseAction="CloseButton" Modal="True">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <table style="text-align: right" width="100%">
                    <tr>
                        <td style="vertical-align: top;" align="right">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="سند">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="vertical-align: top;" align="right">
                            <%--<TSPControls:CustomAspxUploadControl ID="flp" runat="server" MaxSizeForUploadFile="100000"
                                        ShowProgressPanel="True" Width="328px" ClientInstanceName="flp" InputType="Files">                                        
                                    
                                    </TSPControls:CustomAspxUploadControl>--%>
                            <TSPControls:CustomAspxUploadControl runat="server" ID="flpAttachmentDoc" InputType="Files"
                                UploadWhenFileChoosed="true" ClientInstanceName="flpAttachmentDoc" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	ImgEndAttachmentDoc.SetVisible(true);
	//HiddenFieldDocMemberFile.Set('ImgFrontOld',1);
	lblValidationFImgAttachmentDoc.SetVisible(false);                                  
 //   flpAttachmentDoc.SetVisible(true);
	//flpAttachmentDoc.SetImageUrl('../../Image/Members/DocumentAttachment/'+e.callbackData);
	}
	else{
   // HiddenFieldDocMemberFile.Set('ImgFrontOld',0);
	ImgEndFrontAttachmentDoc.SetVisible(false);
	lblValidationFImgAttachmentDoc.SetVisible(true);
	}
}"></ClientSideEvents>
                            </TSPControls:CustomAspxUploadControl>
                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                ID="lblValidationFImgAttachmentDoc" ForeColor="Red" ClientInstanceName="lblValidationFImgAttachmentDoc">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                ID="ImgEndAttachmentDoc" ClientInstanceName="ImgEndAttachmentDoc">
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" align="right">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="توضیحات">
                            </dxe:ASPxLabel>
                        </td>
                        <td style="vertical-align: top;" align="right">
                            <TSPControls:CustomASPXMemo ID="txtDescription" runat="server" Height="46px" Width="330px">
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomASPXMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <br />
                            <TSPControls:CustomAspxButton ID="btnSave" runat="server"
                                OnClick="btnSave_Click" Text="ذخیره" Width="79px" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	PopupAttachment.Hide();
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>

    </TSPControls:CustomASPxPopupControl>
    <br />
    <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewAttachment" runat="server" AutoGenerateColumns="False"
        Width="100%"
        KeyFieldName="AttachId" ClientInstanceName="GridViewAttachment">

        <Columns>
            <dxwgv:GridViewDataImageColumn Caption="مستندات" FieldName="FilePath" Visible="False"
                VisibleIndex="0">
                <PropertiesImage ImageHeight="24px" ImageWidth="24px">
                </PropertiesImage>
            </dxwgv:GridViewDataImageColumn>
            <dxwgv:GridViewDataTextColumn Caption="فایل" FieldName="FilePath" Name="FilePath"
                VisibleIndex="0">
                <DataItemTemplate>
                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" Target="_blank"
                        NavigateUrl='<%# Bind("FilePath") %>' OnDataBinding="ASPxHyperLink1_DataBinding">
                    </dxe:ASPxHyperLink>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <br />
                            <dxhf:ASPxHiddenField ID="HiddenFieldDocumentAttach" runat="server">
                            </dxhf:ASPxHiddenField>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table  >
                    <tr>
                        <td  >
                            <table  >
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnNew2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" Text="جدید" ToolTip="جدید" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	PopupAttachment.Show();
}" /> 
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text="حذف"
                                            ToolTip="حذف" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" /> 
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnBack2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text="بازگشت"
                                            ToolTip="بازگشت" UseSubmitBehavior="False"> 
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="مدیریت پروانه اشتغال به کار" ToolTip="مدیریت پروانه اشتغال به کار"
                                            CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                       
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjdsAttachment" runat="server"></asp:ObjectDataSource>

</asp:Content>
