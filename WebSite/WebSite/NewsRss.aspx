<%@ Page Language="C#" ContentType="text/xml" AutoEventWireup="true" CodeFile="NewsRss.aspx.cs" Inherits="NewsRss" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>--%>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>


<asp:Repeater id="rptRSS" runat="server">
  <HeaderTemplate>

    <rss version="2.0">
      <channel>
        <title>مهمترین عناوین اخبار سازمان نظام مهندسی استان فارس</title>
        <link>http://www.fceo.ir/Headlines/</link>
        <description>
            دوره آزمایشی فید خبری
        </description>
        <language>fa-ir</language>
        <category><%# Utility.DecryptQS(Request.QueryString["Category"].ToString()) %></category>

  </HeaderTemplate>

  <ItemTemplate>
        <item>
          <title><%# FormatForXML(DataBinder.Eval(Container.DataItem,
                                              "Title")) %>
          </title>
          <description>
             <%# FormatForXML(DataBinder.Eval(Container.DataItem, 
                                     "Summary")) %>
          </description>

          <link>
            

               <%# String.Format("http://www.fceo.ir/NewsShow.Aspx?NewsId={0}", 
                 EncryptQS(DataBinder.Eval(Container.DataItem,"NewsId").ToString())) %>
          </link>

          <pubDate>
             <%# String.Format("{0:R}", 
                  DataBinder.Eval(Container.DataItem, 
                                         "Date")) %>
          </pubDate>
            
        </item>
  </ItemTemplate>

  <FooterTemplate>
      <copyright>www.fceo.ir-2005 references Data as. All rights reserved.</copyright>
      </channel>
    </rss>  
  </FooterTemplate>
</asp:Repeater>
