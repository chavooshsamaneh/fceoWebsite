<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master"
    CodeFile="EmployeeHome.aspx.cs" Inherits="Employee_EmployeeHome" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%--<%@ Register Src="../UserControl/FormBuilder_ActiveFormsUserControl.ascx" TagName="FormBuilder_ActiveFormsUserControl"
    TagPrefix="TSP" %>--%>
<%@ Register Src="../UserControl/Poll_ValidPollListUserControl.ascx" TagName="Poll_ValidPollListUserControl"
    TagPrefix="TSP" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset runat="server" id="PanelMember">
        <legend class="HelpUL" style="color: #ff5394" runat="server" id="PanelMemberHeader">درخواست های عضویت</legend>
        <div class="row">
            <div id="divMessage" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #ff5394;">
                <a runat="server" id="linkHyperLinkPublicMessages" href="~/Employee/Message/MessageFromPublicUsers.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span runat="server" id="HyperLinkPublicMessages">پیام های عمومی رسیده(0)</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/message-outline-48.png" />
                        </div>
                    </div>
                </a>
            </div>
            
            <div id="divMemberReq" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #90d802;">
                <a runat="server" id="linkHyperLinkMemberReq">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" runat="server" id="HyperLinkMemberReq"><span>تکمیل پرونده اعضای دائم: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/Revival.png" />
                        </div>
                    </div>
                </a>
            </div>
           
            <div id="divTempMemberReq" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #f7bc48;">
                <a runat="server" id="linkHyperLinkTempMemberReq">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" runat="server" id="HyperLinkTempMemberReq"><span>تکمیل پرونده اعضای موقت: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/edit-user-48.png" />
                        </div>
                    </div>
                </a>
            </div>

        </div>
    </fieldset>
    <fieldset runat="server" id="PanelDocument">
        <legend class="HelpUL" style="color: #4d4c78">درخواست های پروانه</legend>
        <div class="row">
            <div id="divMeFileReq" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkMeFileReq">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkMeFileReq" runat="server"><span>تکمیل پرونده پروانه اشتغال: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
           

            <div id="divMeFileRevival" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #4D4C78;">
                <a runat="server" id="linkHyperLinkMeFileRevival">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" runat="server" id="HyperLinkMeFileRevival"><span>تمدید پروانه اشتغال: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/LicenceRequest.png" />
                        </div>
                    </div>
                </a>
            </div>

           

            <div id="divOfficeMe" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #79aefe;">
                <a runat="server" id="linkHyperLinkOfficeMe">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkOfficeMe" runat="server"><span>تکمیل پرونده عضویت حقوقی: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/conference-48.png" />
                        </div>
                    </div>
                </a>
            </div>
            

        </div>
    </fieldset>
    <fieldset runat="server" id="PanelLearning">
        <legend class="HelpUL" style="color: #cb368a">درخواست های آموزش</legend>
        <div class="row">
            <div id="divSavePeriodInfo" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkSavePeriodInfo">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkSavePeriodInfo" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgSavePeriodInfo" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
            
            <div id="divLearningExpertConfirmingPeriod" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkLearningExpertConfirmingPeriod">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkLearningExpertConfirmingPeriod" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgLearningExpertConfirmingPeriod" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
           
            <div id="divLearningManagerConfirmingPeriod" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkLearningManagerConfirmingPeriod">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkLearningManagerConfirmingPeriod" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgLearningManagerConfirmingPeriod" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
           

        </div>
        <div class="row">
            <div id="divPeriodSaveExamMinute" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkPeriodSaveExamMinute">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkPeriodSaveExamMinute" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgPeriodSaveExamMinute" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
           
            <div id="divPeriodConfirmPointsByLearningExpert" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkPeriodConfirmPointsByLearningExpert">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkPeriodConfirmPointsByLearningExpert" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgPeriodConfirmPointsByLearningExpert" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
       
            <div id="divPeriodConfirmPointsByLearningManager" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkPeriodConfirmPointsByLearningManager">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkPeriodConfirmPointsByLearningManager" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgPeriodConfirmPointsByLearningManager" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
           
        </div>
        <div class="row">
            <div id="divPeriodConfirmPointsByLearningAssistant" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkPeriodConfirmPointsByLearningAssistant">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkPeriodConfirmPointsByLearningAssistant" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgPeriodConfirmPointsByLearningAssistant" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
           
            <div id="divPeriodConfirmingByRiasatSazemanAndSign" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="linkHyperLinkPeriodConfirmingByRiasatSazemanAndSign">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText" id="HyperLinkPeriodConfirmingByRiasatSazemanAndSign" runat="server"><span>دوره های آموزشی در گردش: 0</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img id="ImgPeriodConfirmingByRiasatSazemanAndSign" runat="server" src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>
          
           
        </div>
    </fieldset>
        <fieldset runat="server" id="PanelTS">
        <legend class="HelpUL" style="color:#d1080f" runat="server" id="Legend1">درخواست های خدمات مهندسی</legend>
        <div class="row">
            <div id="divChooseControler" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #d1080f;">
                <a runat="server" id="HyperLinkChooseControler" href="~/Employee/TechnicalServices/Project/PlansChooseControler.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span runat="server" id="HyperLinkChooseControlerTitle">نقشه های در انتظار اختصاص بازبین</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/WF/TS/TS_ChooseCheckMaps.png" />
                        </div>
                    </div>
                </a>
            </div>
             <div id="divConfirmedPlan" runat="server" class="col-md-3 Divborder QuickMenu" style="border-bottom-color: #69dd25;">
                <a runat="server" id="HyperLinkConfirmedPlan" href="~/Employee/TechnicalServices/Report/ReportPlans.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span runat="server" id="HyperLinkConfirmedPlanTitle">نقشه های تایید شده توسط بازبین در ماه جاری</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/WFConfirmAndEnd.png" />
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </fieldset>
    <br />
    <TSP:Poll_ValidPollListUserControl ID="Poll_ValidPollListUserControl" DisplayLocationType="EmployeePortal"
        DataviewColumnCount="4" DataviewItemHeight="150" DataviewItemWidth="150" DataviewItemSpacing="1" runat="server" QuestionCountType="-1" />
    <%--    <TSP:FormBuilder_ActiveFormsUserControl ID="FormBuilder_ActiveFormsUserControl" runat="server"
        DisplayLocationType="EmployeePortal" />--%>
</asp:Content>
