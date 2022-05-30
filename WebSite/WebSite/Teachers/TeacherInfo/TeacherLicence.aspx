<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherLicence.aspx.cs" Inherits="Teachers_TeacherInfo_TeacherLicence"
    Title="مدارک تحصیلی" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                            <image url="~/Images/icons/Back.png">
                                                </image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات فردی" Name="BasicInfo">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مدارک تحصیلی" Name="Madrak" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوابق آموزشی" Name="Job">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="تالیفات و تحقیقات" Name="Research">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مستندات" Name="Atachment">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="دروس" Name="Course" Visible="false">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <TSPControls:CustomAspxDevGridView ID="Grdv_Licence" runat="server" Width="100%"
                AutoGenerateColumns="False"
                KeyFieldName="TLiId" OnHtmlDataCellPrepared="Grdv_Licence_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="Grdv_Licence_AutoFilterCellEditorInitialize">

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TLiId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LiName" Caption="نام مدرک">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MjName" Caption="رشته">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="UnName" Caption="دانشگاه">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CitName" Caption="شهر">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="NumUnit" Caption="تعداد واحد">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Avg" Caption="معدل">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StartDate" Caption="تاریخ شروع">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="EndDate" Caption="تاریخ پایان">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Description" Caption="توضیحات">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="9" Caption=" " Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False"
                                            OnClick="btnBack_Click">
                                            <image url="~/Images/icons/Back.png">
                                                    </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldTeacherLicence" runat="server">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsTeacherLicence" runat="server" SelectMethod="SelectByTeacherId"
                TypeName="TSP.DataManager.TeacherLicenceManger" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TeacherId" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" SelectMethod="SelectByMemberId"
                TypeName="TSP.DataManager.MemberLicenceManager" OldValuesParameterFormatString="original_{0}"
                DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">

                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MemberId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="InActive" Type="Int32" />
                </SelectParameters>

            </asp:ObjectDataSource>

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
