<%@ Page Title="مدیریت تایید اسناد مالی پروانه اشتغال" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AccountingUnitDocConfirm.aspx.cs" Inherits="Employee_Document_AccountingUnitDocConfirm" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]</div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu3" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="تایید و ارسال به واحد پروانه"
                                    Width="25px" ID="btnConfirm" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False" OnClick="btnConfirm_OnClick">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
  	e.processOnServer= confirm('آیا مطمئن به تایید و ارسال به واحد پروانه درخواست انتخاب شده هستید؟')		
	}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="عدم تایید و ارسال جهت تصحیح"
                                    Width="25px" ID="btnReject" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
  	    if( confirm('آیا مطمئن به عدم تایید و ارسال جهت تصحیح پرونده انتخاب شده هستید؟')	)
        {
          PopupReject.Show();
        }
	}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/button_cancel.png">
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
    <TSPControls:CustomAspxDevGridView ID="GridViewMemberFile" Width="100%" runat="server"
        DataSourceID="ObjdsMemberFileMainRequest" 
         AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile"
        KeyFieldName="MfId">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0"
                Width="60px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانودگی">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="MeImg" VisibleIndex="2">
                <PropertiesImage ImageHeight="50px" ImageWidth="50px">
                </PropertiesImage>
            </dxwgv:GridViewDataImageColumn>
            <dxwgv:GridViewDataHyperLinkColumn Caption="فرم تسویه حساب" FieldName="AccConfirmURL"
                VisibleIndex="2" Width="150px">
                <PropertiesHyperLinkEdit Target="_blank" Text="فرم تسویه حساب">
                </PropertiesHyperLinkEdit>
            </dxwgv:GridViewDataHyperLinkColumn>
            <dxwgv:GridViewDataHyperLinkColumn Caption="فیش تسویه حساب" FieldName="FishAccConfirmURL"
                VisibleIndex="2" Width="150px">
                <PropertiesHyperLinkEdit Target="_blank" Text="فیش تسویه حساب">
                </PropertiesHyperLinkEdit>
            </dxwgv:GridViewDataHyperLinkColumn>
            <dxwgv:GridViewDataTextColumn Caption="کدملی" FieldName="SSN" Visible="False" VisibleIndex="12">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " Width="50px"  ShowClearFilterButton="true">
             
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <asp:ObjectDataSource ID="ObjdsMemberFileMainRequest" runat="server" SelectMethod="SelectMainRequest"
        TypeName="TSP.DataManager.DocMemberFileManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="DocType" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MFNo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="LastConfirmReqType" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MFNoWithOutSerial" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="MFSerialNo" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCodeAccConf" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="تایید و ارسال به واحد پروانه"
                                    Width="25px" ID="btnConfirm2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" CausesValidation="False" OnClick="btnConfirm_OnClick">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
  	e.processOnServer= confirm('آیا مطمئن به تایید و ارسال به واحد پروانه درخواست انتخاب شده هستید؟')		
	}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>

                               <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="عدم تایید و ارسال جهت تصحیح"
                                    Width="25px" ID="btnReject2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
  	    if( confirm('آیا مطمئن به عدم تایید و ارسال جهت تصحیح پرونده انتخاب شده هستید؟')	)
        {
          PopupReject.Show();
        }
	}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/button_cancel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomASPxPopupControl ID="PopupReject" runat="server" AllowDragging="True" ClientInstanceName="PopupReject"
        CloseAction="CloseButton"  
        HeaderText="" RightToLeft="True"  Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="535px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <table width="100%">
                    <tr>
                        <td align="right" valign="top" width="15%">
                            توضیحات
                        </td>
                        <td align="right" valign="top" width="85%">
                            <TSPControls:CustomASPXMemo runat="server"  
                                ID="txtDescription" ClientInstanceName="txtDescription" Width="100%">
                            </TSPControls:CustomASPXMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="2">
                          <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"    CausesValidation="False" OnClick="btnSaveReject_OnClick"
                                ID="btnSaveReject" ClientInstanceName="btnSaveReject" Text="ذخیره">
                                <ClientSideEvents Click="function(s,e){
                                PopupReject.Hide();
                                }" />
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle HorizontalAlign="Center" Wrap="False">
            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
        </HeaderStyle>
        <SizeGripImage Height="12px" Width="12px" />
        <CloseButtonImage Height="17px" Width="17px" />
    </TSPControls:CustomASPxPopupControl>
</asp:Content>
