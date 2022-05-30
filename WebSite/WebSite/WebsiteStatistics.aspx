<%@ Page Title="آمار بازدید سایت" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="WebsiteStatistics.aspx.cs" Inherits="WebsiteStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<br />
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>آمار بازدید سایت</h4>
                </div>
                <div class="panel-body" >
                     <%--<asp:Label ID="lblWebsiteStatistics" runat="server"></asp:Label>--%>
                    <div class="row">
                        <div class="col-sm-2">
                             <p>کاربران مهمان: </p>
                            <p>کاربران عضو: </p>
                            <p>بازدیدکنندگان امروز:</p>
                            <p>بازدیدکنندگان هفتگی:</p>
                            <p>بازدیدکنندگان ماهانه:</p>                        
                        </div>
                        <div class="col-sm-10">
                            <p><strong> <%= Utility.GetGuestVisitors().ToString() %></strong></p>
                            <p><strong> <%= Utility.GetOnlineUsers().ToString()  %></strong></p>
                            <p><strong> <%= Utility.GetDailyVisitor().ToString() %></strong></p>
                            <p><strong> <%= Utility.GetWeeklyVisitor().ToString()%></strong></p>                            
                            <p><strong> <%= Utility.GetMonthlyVisitor().ToString() %></strong></p>
                        </div>
                
                        
                    </div>
                </div>
        </div>
   
</asp:Content>

