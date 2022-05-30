<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="NezamMemberChart.aspx.cs" Inherits="Employee_Nezam_NezamChartMember"
    Title="پست سازمانی اعضا" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetType() {
            // alert( document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value);
            if (document.getElementById('chart' + TreeNmChart.GetFocusedNodeKey()).value == 0) {
                alert(1);
                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
            }
        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }

        function SetlableError(cpMsgContent) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }

        function SetErrorMeChart() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = "امکان اضافه کردن سمت به زیر شاخه اشخاص وجود ندارد.";
        }
        function SetErrorChart() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = "زیر شاخه ای از سمت ها را انتخاب نمایید.";
        }
        function SetErrorMeChart1() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = "زیر شاخه ای از اشخاص را انتخاب نمایید.";
        }
    </script>
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
//e.processOnServer=true;
//if(TreeNmChart.GetVisibleNodeKeys().lenght&gt;0)
//{
	 if(TreeNmChart.GetVisibleNodeKeys().lenght&gt;0 &amp;&amp; document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
	  {
		SetErrorMeChart();
		//e.processOnServer=false;
	  }
	else	
	{
		PanelSaveSuccessfully.SetVisible(false);
		PanelMain.SetVisible(true);
		txtPgMd.SetText('New');
		txtNcName.SetText('');
		//TreeNmChart.cpcmbVisible=0;
		cmbParents.SetVisible(false);
		lblParent.SetVisible(false);
		PopupChart.Show();
	}

}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>

                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                    CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) 
{
e.processOnServer=true;
if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
  {
	if(TreeNmChart.GetFocusedNodeKey()==0)
	{
		//TreeNmChart.cpcmbVisible=0;
		cmbParents.SetVisible(false);
	    lblParent.SetVisible(false);
	}
	else
	{
	   // TreeNmChart.cpcmbVisible=1;
	    cmbParents.SetVisible(true);
	    lblParent.SetVisible(true);	
	}
	txtPgMd.SetText('Edit');
	TreeNmChart.PerformCallback('View');
	e.processOnServer=false;

	PanelSaveSuccessfully.SetVisible(false);
	PanelMain.SetVisible(true);
	
	//alert(TreeNmChart.cpcmbVisible);
	PopupChart.Show();	
  }
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتساب سمت"
                                    CausesValidation="False" ID="btnNewMemberChart" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnNewMemberChart_Click">
                                    <ClientSideEvents Click="function(s, e) {
e.processOnServer=true;
 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
  {
	SetErrorChart();
	e.processOnServer=false;
  }
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ChartMember.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                    ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click">
                                    <ClientSideEvents Click="function(s, e) {

if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
	 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
 	 {
		txtPgMd.SetText('InActiveNc');
	}
 	if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
 	 {
		txtPgMd.SetText('DeleteNmc');
 	 }
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}

}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/disactive.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف پست سازمانی"
                                    ID="btnDeleteNC" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDeleteNC_Click">
                                    <ClientSideEvents Click="function(s, e) {

if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
	 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
 	 {
		txtPgMd.SetText('DeleteNc');
	}
 	if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
 	 {
			e.processOnServer=false;
   		    alert('پست سازمانی را انتخاب نمایید.');
            retutn;
 	 }
	 e.processOnServer= confirm('آیا مطمئن به حذف کردن این ردیف هستید؟');
}

}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>

                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                    ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnActive_Click">
                                    <ClientSideEvents Click="function(s, e) {

if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
	 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
 	 {
		txtPgMd.SetText('ActiveNc');
	}
 	if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
 	 {
		txtPgMd.SetText('ActiveNmc');
 	 }
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}

}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/active.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    CausesValidation="False" ID="btnView" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
e.processOnServer=false;
if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		//e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
  {
SetErrorMeChart1();	
  }
else
{
e.processOnServer=true;
}
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/view.png">
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
    <TSPControls:CustomAspxDevTreeList ID="TreeListNmChart" runat="server" DataSourceID="ObjdsNezamChart"
        Width="100%"
        OnHtmlRowPrepared="TreeListNmChart_HtmlRowPrepared" ClientInstanceName="TreeNmChart"
        AutoGenerateColumns="False" OnNodeExpanded="TreeListNmChart_NodeExpanded" OnNodeExpanding="TreeListNmChart_NodeExpanding"
        KeyFieldName="Id" ParentFieldName="ParentId" OnCustomCallback="TreeListNmChart_CustomCallback">


        <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
        <SettingsBehavior AllowFocusedNode="True"></SettingsBehavior>
        <ClientSideEvents EndCallback="function(s, e) {
if(TreeNmChart.cpMsg)
{
PanelSaveSuccessfully.SetVisible(true);
PanelMain.SetVisible(false);
lblChartWarning.SetText(TreeNmChart.cpMsgContent)
TreeNmChart.cpMsg=false;
}
txtNcName.SetText(TreeNmChart.cptxtNcName);
cmbParents.SetSelectedIndex(TreeNmChart.cpcmbParent);

}"></ClientSideEvents>
        <Columns>
            <dxwtl:TreeListTextColumn VisibleIndex="0" FieldName="NcName" Caption="پست سازمانی">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
                <HeaderStyle HorizontalAlign="Center" />
            </dxwtl:TreeListTextColumn>
            <dxwtl:TreeListTextColumn VisibleIndex="1" FieldName="FullName" Caption="نام و نام خانوادگی">
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
                <DataCellTemplate>
                    <dxe:ASPxLabel ID="lblFullName" runat="server" Width="189px" Text='<%# Bind("FullName") %>'>
                    </dxe:ASPxLabel>
                    <input id="chart<%# Container.NodeKey %>" type="hidden" value='<%# Eval("NodeType") %>' />
                </DataCellTemplate>
            </dxwtl:TreeListTextColumn>
            <dxwtl:TreeListTextColumn VisibleIndex="2" FieldName="FirstName" Caption="نام" Visible="False">
            </dxwtl:TreeListTextColumn>
            <dxwtl:TreeListTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی"
                Visible="False">
                <DataCellTemplate>
                    <dxe:ASPxLabel ID="lblLastName" runat="server" Width="189px" Text='<%# Bind("LastName") %>'>
                    </dxe:ASPxLabel>
                    <input id="chart<%# Container.NodeKey %>" type="hidden" value='<%# Eval("NodeType") %>' />
                </DataCellTemplate>
            </dxwtl:TreeListTextColumn>
            <dxwtl:TreeListTextColumn Visible="False" VisibleIndex="4" FieldName="NcId">
            </dxwtl:TreeListTextColumn>
            <dxwtl:TreeListTextColumn Visible="False" VisibleIndex="5" FieldName="NodeType" Name="NodeType">
                <DataCellTemplate>
                </DataCellTemplate>
            </dxwtl:TreeListTextColumn>
        </Columns>
    </TSPControls:CustomAspxDevTreeList>
    <br />
    <fieldset style="width: 98%">
        <legend>راهنما</legend>
        <table width="100%">
            <tbody>
                <tr>
                    <td valign="middle" align="right">
                        <asp:Label ID="Label1" runat="server" Width="16px" BackColor="Wheat" Height="16px"></asp:Label>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Width="108px" Text="ارشد پست سازمانی">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Label ID="Label4" runat="server" Width="16px" BackColor="Blue" Height="16px"></asp:Label>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Width="131px" Text="پست سازمانی اصلی فرد">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <asp:Label ID="Label2" runat="server" Width="16px" BackColor="LightSlateGray" Height="16px"></asp:Label>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Width="131px" Text="فردغیر فعال">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <asp:Label ID="Label3" runat="server" Width="16px" BackColor="Orange" Height="16px"></asp:Label></td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Width="131px" Text="سمت غیر فعال">
                        </dxe:ASPxLabel></td>
                    <td valign="middle" align="right"></td>
                    <td valign="middle" align="right"></td>
                    <td valign="middle" align="right"></td>
                    <td valign="middle" align="right"></td>
                </tr>
            </tbody>
        </table>
    </fieldset>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
//e.processOnServer=true;
//if(TreeNmChart.GetVisibleNodeKeys().lenght&gt;0)
//{
	 if(TreeNmChart.GetVisibleNodeKeys().lenght&gt;0 &amp;&amp; document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
	  {
		SetErrorMeChart();
		//e.processOnServer=false;
	  }
	else	
	{
		PanelSaveSuccessfully.SetVisible(false);
		PanelMain.SetVisible(true);
		txtPgMd.SetText('New');
		txtNcName.SetText('');
		//TreeNmChart.cpcmbVisible=0;
		cmbParents.SetVisible(false);
		lblParent.SetVisible(false);
		PopupChart.Show();
	}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتساب سمت"
                                    CausesValidation="False" ID="btnNewMemberChart2" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnNewMemberChart_Click">
                                    <ClientSideEvents Click="function(s, e) {
e.processOnServer=true;
 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
  {
	SetErrorChart();
	e.processOnServer=false;
  }
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ChartMember.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                    CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) {
	
e.processOnServer=true;
if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
  {
	if(TreeNmChart.GetFocusedNodeKey()==0)
	{
		//TreeNmChart.cpcmbVisible=0;
		cmbParents.SetVisible(false);
	    lblParent.SetVisible(false);
                                                                    
	}
	else
	{
	   // TreeNmChart.cpcmbVisible=1;
	    cmbParents.SetVisible(true);
	    lblParent.SetVisible(true);	
	}
	txtPgMd.SetText('Edit');
	TreeNmChart.PerformCallback('View');
	e.processOnServer=false;

	PanelSaveSuccessfully.SetVisible(false);
	PanelMain.SetVisible(true);
	
	//alert(TreeNmChart.cpcmbVisible);
	PopupChart.Show();	
  }
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                    ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click">
                                    <ClientSideEvents Click="function(s, e) {
if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
	 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
 	 {
		txtPgMd.SetText('InActiveNc');
	}
 	if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
 	 {
		txtPgMd.SetText('DeleteNmc');
 	 }
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/disactive.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف پست سازمانی"
                                    ID="btnDeleteNC2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDeleteNC_Click">
                                    <ClientSideEvents Click="function(s, e) {

if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
	 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
 	 {
		txtPgMd.SetText('DeleteNc');
	}
 	if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
 	 {
			e.processOnServer=false;
   		    alert('پست سازمانی را انتخاب نمایید.');
            retutn;
 	 }
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}

}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                    ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnActive_Click">
                                    <ClientSideEvents Click="function(s, e) {

if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
	 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
 	 {
		txtPgMd.SetText('ActiveNc');
	}
 	if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==1)
 	 {
		txtPgMd.SetText('ActiveNmc');
 	 }
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}

}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/active.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    CausesValidation="False" ID="btnView2" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
if ( TreeNmChart.GetFocusedNodeKey()== '' )
{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است.');
}
else
{
 if(document.getElementById('chart'+TreeNmChart.GetFocusedNodeKey()).value ==0)
  {
SetErrorMeChart1();		
  }
else
{
e.processOnServer=true;
}
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomASPxPopupControl ID="PopupChart" runat="server"
        ClientInstanceName="PopupChart"
        HeaderText="تغییر رکورد">
        <ClientSideEvents Closing="function(s, e) {
	//HiddenFieldNezamChart.Set('PgMd','');
}"></ClientSideEvents>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <TSPControls:CustomAspxCallbackPanel runat="server" Width="100%"
                    ID="CallbackPanelChart" ClientInstanceName="CallbackPanelChart" OnCallback="CallbackPanelWorkFlow_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxp:ASPxPanel runat="server" Width="100%" ID="PanelMain" ClientInstanceName="PanelMain">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">

                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td valign="top" dir="ltr" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="زير شاخه" Font-Size="X-Small" Width="56px" ID="lblParent"
                                                            ClientInstanceName="lblParent">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="240px"
                                                            TextField="NcName" ID="cmbParents" DataSourceID="ObjdsNezamChartParent" ValueType="System.String"
                                                            ValueField="NcId" ClientInstanceName="cmbParents"
                                                            __designer:wfdid="w5">
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" dir="ltr" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="عنوان سمت" Font-Size="X-Small" Width="56px" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" dir="rtl" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtNcName" Width="240px" ClientInstanceName="txtNcName">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="ValidationPopUp" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="عنوان سمت را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" dir="ltr" align="center" colspan="2">
                                                        <TSPControls:CustomAspxButton runat="server" Text="ذخیره" Width="93px" ValidationGroup="ValidationPopUp"
                                                            ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep">
                                                            <ClientSideEvents Click="function(s, e) {	
if(txtNcName.GetIsValid())
{
//e.processOnServer=true;
if(txtPgMd.GetText()=='New')
{
	TreeNmChart.PerformCallback('New');
}
if(txtPgMd.GetText()=='Edit')
{
	TreeNmChart.PerformCallback('EditChart');
}
}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                            <dxp:ASPxPanel runat="server" Height="100%" ClientVisible="False" Width="100%" ID="PanelSaveSuccessfully"
                                ClientInstanceName="PanelSaveSuccessfully">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <div align="center" width="100%">
                                            <dxe:ASPxLabel runat="server" Font-Size="X-Small" ID="lblChartWarning" ForeColor="Red"
                                                ClientInstanceName="lblChartWarning">
                                            </dxe:ASPxLabel>
                                            <br />
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" Text="خروج" CausesValidation="False"
                                                Width="93px" ID="btnClose" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep">
                                                <ClientSideEvents Click="function(s, e) {	
	TreeNmChart.PerformCallback('');
	PopupChart.Hide();
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomAspxCallbackPanel>
            </dxpc:PopupControlContentControl>
        </ContentCollection>

    </TSPControls:CustomASPxPopupControl>
    <asp:ObjectDataSource ID="ObjdsNezamChart" runat="server" TypeName="TSP.DataManager.NezamMemberChartManager"
        SelectMethod="SelectNezamMemberChart" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="0" Name="IsExternal"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <TSPControls:CustomTextBox ID="txtPgMd" runat="server" Width="170px" ClientInstanceName="txtPgMd"
        ClientVisible="False">
    </TSPControls:CustomTextBox>
    <TSPControls:CustomTextBox ID="txtName" runat="server" Width="170px" ClientInstanceName="txtName"
        ClientVisible="False">
    </TSPControls:CustomTextBox>
    <asp:ObjectDataSource ID="ObjdsNezamChartParent" runat="server" TypeName="TSP.DataManager.NezamChartManager"
        SelectMethod="SelectAllPosition" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="IsExternal" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldNezamChart" ClientInstanceName="HiddenFieldNezamChart">
    </dxhf:ASPxHiddenField>
</asp:Content>
