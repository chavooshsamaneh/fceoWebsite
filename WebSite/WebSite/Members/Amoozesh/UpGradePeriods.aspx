<%@ Page Title="لیست دوره های آموزشی جهت ارتقا پایه" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="UpGradePeriods.aspx.cs" Inherits="Members_Amoozesh_UpGradePeriods" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#"><span style="color: #000000">ب</span>ستن</a>]
            </div>
            <TSPControls:CustomAspxDevGridView ID="GridViewUpGradePoint" runat="server" DataSourceID="ObjdsUpGradePoint"
                Width="100%"
                ClientInstanceName="GridViewUpGradePoint"
                AutoGenerateColumns="False" KeyFieldName="UpGrdPId">

                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="TypeName" Caption="دوره/سمینار" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="CrsNameAndHour" Caption="عنوان دوره/سمینار" VisibleIndex="1" Width="300px">
                    </dxwgv:GridViewDataTextColumn>


                    <dxwgv:GridViewDataComboBoxColumn FieldName="UpGrdId" Width="100px" Caption="ارتقاء پایه"
                        VisibleIndex="1">
                        <HeaderStyle Font-Size="X-Small"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="right"></CellStyle>
                        <PropertiesComboBox ValueType="System.String" TextField="UpGrdName" DataSourceID="ObjdsUpGrade"
                            ValueField="UpGrdId">
                        </PropertiesComboBox>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته" Width="200px">
                        <CellStyle Wrap="False"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ResName" Caption="صلاحیت">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="JobDuration" Width="100px"
                        Caption="تعداد سال از اخذ پایه">
                        <HeaderStyle Wrap="True" Font-Size="X-Small"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="MinPeriodNeed" Width="100px"
                        Caption="تعداد دوره های مورد نیاز ">
                        <HeaderStyle Wrap="True" Font-Size="X-Small"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    
                    <dxwgv:GridViewDataTextColumn FieldName="Description" Caption="توضیحات" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Width="30px" VisibleIndex="11" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjdsUpGrade" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.DocAcceptedUpGradeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsUpGradePoint" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="SelectActivePeriodsForUpgrad" TypeName="TSP.DataManager.TrainingAcceptedGradeManager">
                <SelectParameters  >
                    <asp:Parameter DbType="Int32"  Name="MjId" DefaultValue="-1" /> 
                    <asp:Parameter DbType="Int32" Name="ResId" DefaultValue="-1"/> 
                    <asp:Parameter DbType="Int32" Name="GrdIdOrigin" DefaultValue="-1"/> 
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


