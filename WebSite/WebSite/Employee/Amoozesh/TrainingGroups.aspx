<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TrainingGroups.aspx.cs" Inherits="Employee_Amoozesh_TrainingGroups"
    Title="گروه های آموزشی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function HasError() {

            if (name1.GetIsValid())
                return false;
            return true;
        }
        function SetEmpty() {
            name1.SetText("");
            memo.SetText("");
            //cmb.InsertItem(0,'--------',null);
            //cmb.SetSelectedIndex(-1);
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {
	SetEmpty();
	HDmode.Set(&quot;col&quot;,&quot;New&quot;);
	cmb.SetSelectedIndex(0);
	pop.Show();
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" ID="btnEdit" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {

	if (tree.GetFocusedNodeKey()==&quot;&quot;)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		if(HDId.Get('col')=='-1')
		{
	    	e.processOnServer=false;
   		    alert(&quot;ردیفی انتخاب نشده است&quot;);
		}
		else
		{		
		    HDId.Set('col',tree.GetFocusedNodeKey());
		    HDmode.Set('col',&quot;Edit&quot;);
		    call.PerformCallback('FillForm'+';'+HDId.Get('col'));
		}
	}	
	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                            ToolTip="حذف" CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True"
                                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (tree.GetFocusedNodeKey()==&quot;&quot;)// || HDId.Get(&quot;col&quot;)==&quot;&quot;
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		HDId.Set(&quot;col&quot;,tree.GetFocusedNodeKey());
		 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
	} 
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/delete.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="گروه های آموزشی" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <TSPControls:CustomAspxDevTreeList runat="server" ID="CustomAspxDevTreeList1" Width="500px" DataSourceID="ObjectDataSource1"
                            KeyFieldName="TgrId" ParentFieldName="ParentId" AutoGenerateColumns="False" ClientInstanceName="tree"
                            OnCustomCallback="CustomAspxDevTreeList1_CustomCallback" 
                            >

                            <Columns>
                                <dxwtl:TreeListTextColumn VisibleIndex="0" FieldName="Name" Caption="نام">
                                </dxwtl:TreeListTextColumn>
                                <dxwtl:TreeListTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات">
                                </dxwtl:TreeListTextColumn>
                                <dxwtl:TreeListTextColumn Visible="False" VisibleIndex="2" FieldName="TgrId">
                                </dxwtl:TreeListTextColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevTreeList>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" 
                  HeaderText="مشخصات گروه"
                PopupElementID="btnSearch1"  ClientInstanceName="pop" Width="400px">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <TSPControls:CustomAspxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="call"
                            OnCallback="ASPxCallbackPanel1_Callback" Width="100%">
                            <ClientSideEvents EndCallback="function(s, e) {
                                if(s.cpPopShow==1)
                                    pop.Show();
                                else
                                {
                                    tree.PerformCallback();
	                                pop.Hide();
                                }                                

                                }"
                                BeginCallback="function(s,e){ call.cpPopShow = 0;}"></ClientSideEvents>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table dir="rtl" width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="سرگروه"  ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td dir="ltr" align="right" valign="top">
                                                    <TSPControls:CustomAspxComboBox runat="server" 
                                                        TextField="Name" ID="CmbParent"  DataSourceID="ObjectDataSource1"
                                                        ValueType="System.String" ValueField="TgrId" SelectedIndex="0" ClientInstanceName="cmb"
                                                        >
                                                        <ValidationSettings>
                                                          
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="نام"  ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server"   MaxLength="40" ID="txtName"
                                                        ClientInstanceName="name1" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                          
                                                            <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel4">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td colspan="3" align="right" valign="top">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="30px"  ID="txtDesc"
                                                        ClientInstanceName="memo" >
                                                        <ValidationSettings>
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center" valign="top">
                                                    <br />
                                                    <TSPControls:CustomAspxButton runat="server" Text="ذخیره"  Width="70px" ID="btnSave"
                                                        AutoPostBack="False" UseSubmitBehavior="False" >
                                                        <ClientSideEvents Click="function(s, e) {

if(!HasError())
{
//pop.Hide();
call.PerformCallback(HDmode.Get('col')+';'+HDId.Get('col'));

}
else
  e.processOnServer=false;
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomAspxCallbackPanel>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px"></SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px"></CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
            <br />
            <dxhf:ASPxHiddenField ID="PgMode" runat="server" ClientInstanceName="HDmode">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="GroupId" runat="server" ClientInstanceName="HDId">
            </dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {
	//tree.AddNewRow();
	SetEmpty();
	HDmode.Set(&quot;col&quot;,&quot;New&quot;);
	cmb.SetSelectedIndex(0);
	pop.Show();
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" ID="btnEdit2" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {
	if (tree.GetFocusedNodeKey()==&quot;&quot;)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{	
		if(HDId.Get('col')=='-1')
		{
	    	e.processOnServer=false;
   		    alert(&quot;ردیفی انتخاب نشده است&quot;);
		}
		else
		{		
		    HDId.Set('col',tree.GetFocusedNodeKey());
		    HDmode.Set('col',&quot;Edit&quot;);
		    call.PerformCallback('FillForm'+';'+HDId.Get('col'));
		}		
	}	
	
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                            ToolTip="حذف" CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True"
                                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 
if (tree.GetFocusedNodeKey()==&quot;&quot;)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		HDId.Set(&quot;col&quot;,tree.GetFocusedNodeKey());
		 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
	} 
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/delete.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="TSP.DataManager.TrainingGroupsManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
