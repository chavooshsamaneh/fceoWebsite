<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherJobHistory.aspx.cs" Inherits="Teachers_TeacherInfo_TeacherJobHistory"
    Title="سوابق آموزشی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                            <image url="~/Images/icons/Back.png"></image>                                                           
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                        <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server"  OnItemClick="ASPxMenu1_ItemClick" >
                            <Items>
                                <dxm:MenuItem Text="مشخصات فردی" Name="BasicInfo"></dxm:MenuItem>
                                <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="سوابق آموزشی" Name="Job" Selected="true"></dxm:MenuItem>
                                <dxm:MenuItem Name="Research" Text="تالیفات و تحقیقات">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Atachment" Text="مستندات">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Course" Text="دروس" Visible="false">
                                </dxm:MenuItem>
                            </Items>
                          
                        </TSPControls:CustomAspxMenuHorizontal>
                   
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="Grdv_JobHistory" runat="server" DataSourceID="ObjdsTeacherJobHistory" Width="100%" AutoGenerateColumns="False" KeyFieldName="TJobHistoryId"
                        OnHtmlDataCellPrepared="Grdv_JobHistory_HtmlDataCellPrepared"
                        OnAutoFilterCellEditorInitialize="Grdv_JobHistory_AutoFilterCellEditorInitialize">
                        
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TJobHistoryId"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TJobPlace" Caption="نام مؤسسه"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TJobName" Caption="نوع فعالیت آموزشی"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="CounName" Caption="کشور"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CitName" Caption="شهر"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="StartDate" Caption="تاریخ شروع"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="EndDate" Caption="تاریخ پایان"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " Width="30px" ShowClearFilterButton="true">
                            </dxwgv:GridViewCommandColumn>
                        </Columns>                     
                    </TSPControls:CustomAspxDevGridView>
                    <asp:ObjectDataSource ID="ObjdsTeacherJobHistory" runat="server" TypeName="TSP.DataManager.TeacherJobHistoryManager" SelectMethod="SelectByTeacherId" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="TeacherId"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="TcId"></asp:Parameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <br />
                         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False" OnClick="btnBack_Click">
                                                            <image url="~/Images/icons/Back.png"></image>

                                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
</hoverstyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                            </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                        <dxhf:ASPxHiddenField ID="HiddenFieldTeCertificate" runat="server">
                                        </dxhf:ASPxHiddenField>
                                  
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>

</asp:Content>
