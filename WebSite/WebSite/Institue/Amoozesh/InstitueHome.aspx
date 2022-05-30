<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="InstitueHome.aspx.cs" Inherits="Institue_Amoozesh_InstitueHome" Title="پورتال مؤسسه" %>

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
            href="#">بستن</a>]
    </div>
    <div class="row">


        <div class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #ff5394;">
            <a runat="server" id="btnPeriods" href="/Institue/Amoozesh/Period.aspx">

                <div class="Inside">
                    <div class="col-md-9 QuickMenuText"><span>دوره های آموزشی</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img src="../../Images/HomePage/Periods.png" />
                    </div>
                </div>
            </a>
        </div>



        <div class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #90d802;">
            <a runat="server" id="btnSeminars" href="/Institue/Amoozesh/Seminar.aspx">
                <div class="Inside">
                    <div class="col-md-9 QuickMenuText"><span>سمینارهای آموزشی</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img src="../../Images/HomePage/conference-48.png" />
                    </div>
                </div>
            </a>
        </div>

      
    </div>
     <div class="row">
           <div class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #f7bc48;">

            <a runat="server" id="btnPeriodRegister" href="/Institue/Amoozesh/CourseRegister.aspx">

                <div class="Inside">
                    <div class="col-md-9 QuickMenuText"><span>ثبت نام دوره های آموزشی</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img src="../../Images/HomePage/edit-user-48.png" />
                    </div>
                </div>


            </a>
        </div>
         </div>
    <div class="row">

        <div id="divSavePeriodInfo" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #ff8800;">
            <a runat="server" id="linkHyperLinkSavePeriodInfo">
                <div class="Inside">
                    <div class="col-md-9 QuickMenuText" id="HyperLinkSavePeriodInfo" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img id="ImgSavePeriodInfo" runat="server" src="../../Images/HomePage/NeweDoc.png" />
                    </div>
                </div>
            </a>
        </div>

        <div id="divLearningExpertConfirmingPeriod" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #bb42ca8f;">
            <a runat="server" id="linkHyperLinkLearningExpertConfirmingPeriod">
                <div class="Inside">
                    <div class="col-md-9 QuickMenuText" id="HyperLinkLearningExpertConfirmingPeriod" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img id="ImgLearningExpertConfirmingPeriod" runat="server" src="../../Images/HomePage/NeweDoc.png" />
                    </div>
                </div>
            </a>
        </div>

        <div class="col-md-3 "></div>

    </div>
    <asp:HiddenField ID="MeId" runat="server" Visible="False" />
</asp:Content>

