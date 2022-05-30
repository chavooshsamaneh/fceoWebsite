<%@ Page Title="پرتال مالک" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="OwnerHome.aspx.cs" Inherits="Owner_OwnerHome" %>

<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <br /> <fieldset runat="server" id="PanelMember">
        <legend class="HelpUL" style="color: #ff5394" runat="server" id="PanelMemberHeader"></legend>
      
        <div class="row">
            <div id="divMessage" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #ff5394;">
                <a runat="server" id="linkHyperLinkFised" href="~/Owner/ProjectAccounting.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span runat="server" id="HyperLinkUnPaydedFised">لیست فیش های پروژه ساختمانی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/card-in-use-48.png" />
                        </div>
                    </div>
                </a>
            </div>
            
            <div id="divMemberReq" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #90d802;">
                <a runat="server" id="linkHyperLinkFishes"  href="~/Owner/ProjectImplementer.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" runat="server" id="HyperLinkPayedFishes"><span>مدیریت مجریان پروژه</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/ObsSelect.png" />
                        </div>
                    </div>
                </a>
            </div>
           
          <%--  <div id="divTempMemberReq" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #f7bc48;">
                <a runat="server" id="linkHyperLinkProjectInfo"  href="~/Owner/ProjectInfo.aspx" >
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" runat="server" id="HyperLinkProjectInfo"><span>گزارش عاملان پروژه ساختمانی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/MapManagment.png" />
                        </div>
                    </div>
                </a>
            </div>--%>

        </div>
    </fieldset>
</asp:Content>

