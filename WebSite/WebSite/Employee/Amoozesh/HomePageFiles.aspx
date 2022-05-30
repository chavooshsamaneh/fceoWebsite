<%@ Page Title="مدیریت فایل های صفحه نخست" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="HomePageFiles.aspx.cs" Inherits="Employee_Amoozesh_HomePageFiles" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>     
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table  >
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="ذخیره" ID="BtnSave" EnableViewState="False" OnClick="BtnSave_Click">
                                            <Image  Url="~/Images/icons/save.png">
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
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="فایل های صفحه اول وب سایت" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <%--برنامه آموزشی سال جاری--%>
                        <%--راهنمای ورود به صفحه وب کنفرانس--%>

                        <table width="100%">
                        
                            
                            <tr>
                                <td valign="top">برنامه آموزشی سال جاری
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td dir="rtl" align="right">
                                                <TSPControls:CustomAspxUploadControl ID="UploadAttachSchedule" runat="server" UploadWhenFileChoosed="true"   MaxSizeForUploadFile="10000000"
                                                    InputType="Files" ClientInstanceName="UploadAttachSchedule" OnFileUploadComplete="UploadAttachSchedule_FileUploadComplete"
                                                    >
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {  
if(e.isValid){
imgEndUploadSchedule.SetVisible(true);
HyperLinkSchedule.SetNavigateUrl('../../Help/Amoozesh/'+e.callbackData);
}
else {
 imgEndUploadSchedule.SetVisible(false);
HyperLinkSchedule.SetNavigateUrl('');}
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxUploadControl>
                                             
                                            </td>
                                            <td valign="top">
                                                <dxe:ASPxImage runat="server" ImageUrl="~/Images/button_ok.png" ClientInstanceName="imgEndUploadSchedule"
                                                    ClientVisible="False" ToolTip="تصویر انتخاب شد" ID="imgEndUploadSchedule">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </table>
                                </td>  <td>  <dxe:ASPxHyperLink ID="HyperLinkSchedule" runat="server" ClientInstanceName="HyperLinkSchedule"
                                            Target="_blank" Text="آدرس فایل">
                                        </dxe:ASPxHyperLink></td>
                            </tr>
                              <tr>
                                <td valign="top">راهنمای ورود به صفحه وب کنفرانس
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td dir="rtl" align="right">
                                                <TSPControls:CustomAspxUploadControl ID="UploadAttachHelpLms" runat="server" UploadWhenFileChoosed="true" MaxSizeForUploadFile="10000000"
                                                    InputType="Files" ClientInstanceName="UploadAttachHelpLms" OnFileUploadComplete="UploadAttachHelpLms_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {  
if(e.isValid){
imgEndUploadHelpLms.SetVisible(true);
                                                     
HyperLinkhHelpLms.SetNavigateUrl('../../Help/Amoozesh/'+e.callbackData);
}
else{ 
 imgEndUploadHelpLms.SetVisible(false);
HyperLinkhHelpLms.SetNavigateUrl('');
                                                        }
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxUploadControl>
                                             
                                            </td>
                                            <td valign="top">
                                                <dxe:ASPxImage runat="server" ImageUrl="~/Images/button_ok.png" ClientInstanceName="imgEndUploadHelpLms"
                                                    ClientVisible="False" ToolTip="تصویر انتخاب شد" ID="imgEndUploadHelpLms">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>
                                  <td>  <dxe:ASPxHyperLink ID="HyperLinkhHelpLms" runat="server" ClientInstanceName="HyperLinkhHelpLms"
                                            Target="_blank" Text="آدرس فایل">
                                        </dxe:ASPxHyperLink></td>
                            </tr>
                         <%--   <tr>
                                <td colspan="2" align="center">
                                    <asp:Panel ID="PanelShowAttachEvents" runat="server">
                                        <br />
                                        <asp:Image ID="imgEvents" runat="server" />
                                        <br />
                                    </asp:Panel>
                                </td>
                            </tr>--%>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="ذخیره" ID="BtnSave2" EnableViewState="False" OnClick="BtnSave_Click">
                                            <Image  Url="~/Images/icons/save.png">
                                            </Image>
                                         
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>