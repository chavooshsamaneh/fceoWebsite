<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SettlmentHomePage.aspx.cs" Inherits="Settlement_SettlmentHomePage"
    Title="پرتال مسکن" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>  
         <div class="row">
            <div runat="server" id="btnNewDoc" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="A4" href="/Settlement/MemberDocument/MemberFile.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت پروانه اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/Document.png" />
                        </div>
                    </div>
                </a>
            </div>
          

            <div runat="server" id="btnQualification" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #4D4C78;">
                <a runat="server" id="A5" href="/Settlement/ImplementDoc/ImplementDoc.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت مجوزفعالیت مجری حقیقی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/ImpDoc.png" />
                        </div>
                    </div>
                </a>
            </div>

        

            <div runat="server" id="btnRevival" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #79aefe;">
                <a runat="server" id="A6" href="/Settlement/OfficeDocument/OfficeRequest.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت پروانه اشخاص حقوقی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/OfficeDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
            

        <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #42B4BC;">
            <a runat="server" id="A7" href="/Settlement/EngOfficeDocument/EngOffice.aspx">
                <div class="Inside">
                    <div class="col-md-9 QuickMenuText"><span>مدیریت پروانه دفاتر</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img src="../Images/HomePage/EngOfficeDoc.png" />
                    </div>
                </div>
            </a>
        </div>
        </div>
      <div class="row">
           <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #90d802;">
            <a  href="/Settlement/Amoozesh/Periods.aspx">
                <div class="Inside">
                    <div id="txtMeDebt" style="text-align:center" runat="server" class="col-md-9 QuickMenuText"><span>مدیریت دوره های آموزشی</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img src="../Images/HomePage/book-stack-48.png" />
                    </div>
                </div>
            </a>
        </div>
     </div>
    
</asp:Content>
