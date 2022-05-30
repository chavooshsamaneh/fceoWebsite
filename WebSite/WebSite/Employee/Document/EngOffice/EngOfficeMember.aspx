<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeMember.aspx.cs" Inherits="Employee_Document_EngOffice_EngOfficeMember"
    Title="اعضای دفتر" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table align="right">
                            <tr>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                        EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                       
                                        <Image  Url="~/Images/icons/new.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server"  EnableTheming="False"
                                        EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" 
                                        UseSubmitBehavior="False">
                                        
                                        <Image  Url="~/Images/icons/edit.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                        EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                       
                                        <Image  Url="~/Images/icons/view.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                        ToolTip="غیر فعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                      
                                        <Image  Url="~/Images/icons/disactive.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnActive_Click" Text=" "
                                        ToolTip="فعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}" />
                                       
                                        <Image  Url="~/Images/icons/active.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" AutoPostBack="False" 
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../../Print.aspx&quot;);	
}" />
                                       
                                        <Image  Url="~/Images/icons/printers.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                       
                                        <Image  Url="~/Images/icons/Back.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت دفاتر"
                                        CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                       
                                        <Image  Url="~/Images/icons/BakToManagment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
         
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  OnItemClick="ASPxMenu1_ItemClick" >
                    <Items>
                        <dxm:MenuItem Name="EngOffice" Text="مشخصات دفتر">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Member" Selected="true" Text="اعضای دفتر">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Job" Text="سوابق کاری">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Attach" Text="مستندات">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>
    
            <ul class="HelpUL">
                <li>در صورتی که قصد ویرایش اطلاعات مربوط به درخواست های پیشین را دارید ، جهت حفظ سابقه
                    بایستی عضو را غیرفعال و اطلاعات جدید را مجددا وارد نمایید.</li>
            </ul>
            <TSPControls:CustomAspxDevGridView ID="GridViewEngOffMember" Width="100%" runat="server"
                RightToLeft="True"  
                AutoGenerateColumns="False" KeyFieldName="OfmId" OnHtmlRowPrepared="GridViewEngOffMember_HtmlRowPrepared"
                DataSourceID="ObjectDataSourceEngOffMember" OnHtmlDataCellPrepared="GridViewEngOffMember_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="GridViewEngOffMember_AutoFilterCellEditorInitialize">
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="OfmId" Name="OfmId" Visible="False" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PersonId" Name="PersonId" Visible="False"
                        VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="OfpName" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Active" VisibleIndex="1"
                        Width="70px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع همکاری" FieldName="IsFullTimeName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    
                     <dxwgv:GridViewDataTextColumn Width="50px" VisibleIndex="2" Caption="خود اظهاری">
                                <DataItemTemplate>
                                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server"  Text='<%# Bind("HasSelfreportedImageURL") %>' Target="_blank"
                                        NavigateUrl='<%# Bind("SelfreportedImageURL") %>' >
                                    </dxe:ASPxHyperLink>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" VisibleIndex="3"
                        Width="100px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار پروانه" FieldName="FileDate"
                        VisibleIndex="3" Width="100px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاريخ شروع همكاري" FieldName="StartDate" VisibleIndex="6">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="امکان عضویت گاز" FieldName="GasCert" VisibleIndex="6">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="OfReId" Visible="False" VisibleIndex="8">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0"
                        Width="70px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                   
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="True"></Settings>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table align="right">
                            <tr>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                       
                                        <Image  Url="~/Images/icons/new.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" 
                                        UseSubmitBehavior="False">
                                       
                                        <Image  Url="~/Images/icons/edit.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                        
                                        <Image  Url="~/Images/icons/view.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive1" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                        ToolTip="غیر فعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                      
                                        <Image  Url="~/Images/icons/disactive.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive1" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnActive_Click" Text=" "
                                        ToolTip="فعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}" />
                                      
                                        <Image  Url="~/Images/icons/active.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton3" runat="server" AutoPostBack="False" 
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../../Print.aspx&quot;);	
}" />
                                       
                                        <Image  Url="~/Images/icons/printers.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                       
                                        <Image  Url="~/Images/icons/Back.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت دفاتر"
                                        CausesValidation="False" ID="ASPxButton4" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                       
                                        <Image  Url="~/Images/icons/BakToManagment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjectDataSourceEngOffMember" runat="server" SelectMethod="selectEngOfficeMember"
                TypeName="TSP.DataManager.OfficeMemberManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="EOfId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="EngOfId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
            <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img align="middle" src="../../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
