<%@ Page Title="تماس با پشتیبان فنی سایت" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="ContactMaintance.aspx.cs" Inherits="ContactMaintance" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<TSPControls:CustomASPxRoundPanel ID="RoundPanelPrj" HeaderText="تماس با پشتیبان فنی سایت" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                 <div class="row">
                        <div class="col-sm-2">
                             <p>پشتیبانی توسط: </p>
                            <p>شماره تماس: </p>
                            <p>ساعات اداری:</p>
                            <p>ایام هفته:</p>                         
                        </div>
                        <div class="col-sm-10">
                            <p><strong> شرکت چاووش سامانه پارسیان</strong></p>
                            <p><strong> 09171123560</strong></p>
                            <p><strong> 9 - 18</strong></p>                            
                            <p><strong>شنبه تا پنجشنبه </strong></p>
                        </div>
                
                        
                    </div>
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
     
 
</asp:Content>

