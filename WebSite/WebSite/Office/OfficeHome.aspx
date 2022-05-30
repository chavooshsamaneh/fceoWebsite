<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="OfficeHome.aspx.cs" Inherits="Office_OfficeHome" Title="پورتال اعضای حقوقی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  
   <div class="row">
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #ff5394;">
                <a runat="server" id="test"  href="/Office/OfficeMembershipRequest.aspx" >
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت درخواست های عضويت</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/edit-user-48.png" />
                        </div>
                    </div>
                </a>
            </div>
          
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #90d802;">
                <a runat="server" id="A1" href="/Office/OfficeRequest.aspx" >
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت درخواست های پروانه</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/LicenceRequest.png" />
                        </div>
                    </div>
                </a>
            </div>
          

        </div>

</asp:Content>
