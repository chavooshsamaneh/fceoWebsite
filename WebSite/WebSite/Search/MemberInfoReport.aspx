<%@ Page Title="خلاصه اطلاعات شخص حقیقی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberInfoReport.aspx.cs" Inherits="Search_MemberInfoReport" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Src="~/UserControl/MeMembershipInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeMembershipInfoUserControl" %>
<%@ Register Src="~/UserControl/MeDocumentInfoUserControl.ascx" TagPrefix="TSP" TagName="MeDocumentInfoUserControl" %>
<%@ Register Src="~/UserControl/MeMembershipLicenceInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeMembershipLicenceInfoUserControl" %>
<%@ Register Src="~/UserControl/MeOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeOfficeInfoUserControlUserControl" %>
<%@ Register Src="~/UserControl/MeEngOfficeInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeEngOfficeInfoUserControl" %>
<%@ Register Src="~/UserControl/MemberImplementDocInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MemberImplementDocInfoUserControl" %>
<%@ Register Src="~/UserControl/MemberObservationDocInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MemberObservationDocInfoUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>
    <TSPControls:CustomAspxCallbackPanel runat="server"
        ClientInstanceName="CallbackPanelMain" Width="100%" ID="CallbackPanelMain" OnCallback="CallbackPanelMain_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
   if(s.cpSearch==0)
   {
     
        if(s.cpPrintMe==1)
        {
            window.open(s.cpPrintURL);                      
        }

    }
}" />
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent3" runat="server">
                <div align="right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="false">

                                                <Image Url="~/Images/icons/printred.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s,e){
                                         CallbackPanelMain.PerformCallback('PrintMe');
                                            }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel13">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMeIdSearch" Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e) { SearchKeyPress(e, 1);}" />
                                            <ValidationSettings>
                                                <RegularExpression ErrorText="* نامعتبر" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomTextBox ID="txtFileNo" runat="server" ClientInstanceName="txtFileNo"
                                            Width="100%" RightToLeft="True" Style="direction: ltr">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            <ValidationSettings>
                                                <RegularExpression ErrorText="شماره پروانه به صورت *****-***-**  می باشد" ValidationExpression="\d{2}-\d{3}-\d{1,5}" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <br />
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="false"
                                                        Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="true">
                                                        <ClientSideEvents Click="function(s, e) {	
CallbackPanelMain.PerformCallback('search');
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right">
                                                    <TSPControls:CustomAspxButton ID="ASPxButton3" runat="server" AutoPostBack="false"
                                                        Text="پاک کردن فرم" Width="100px" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {	
CallbackPanelMain.PerformCallback('clear');
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSP:MeMembershipInfoUserControl runat="server" ID="UserControlMeMembershipInfo"
                    MeId="-2" />


                <TSP:MeMembershipLicenceInfoUserControl runat="server" ID="UserControlMeMembershipLicenceInfo"
                    MeId="-2" />
                <TSP:MeDocumentInfoUserControl runat="server" ID="UserControlMeDocumentInfo" MeId="-2" />

                <TSP:MeOfficeInfoUserControlUserControl runat="server" ID="UserControlMeOfficeInfoUserControl"
                    MeId="-2" />

                <TSP:MeEngOfficeInfoUserControl runat="server" ID="UserControlMeEngOfficeInfoUserControl" />

                <TSP:MemberImplementDocInfoUserControl runat="server" ID="UserControlMemberImplementDocInfo" />

                <TSP:MemberObservationDocInfoUserControl runat="server" ID="UserControlMemberObservationDocInfo" />
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" cellpadding="0" align="right">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="false">

                                                <Image Url="~/Images/icons/printred.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s,e){
                                         CallbackPanelMain.PerformCallback('PrintMe');
                                            }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>
</asp:Content>
