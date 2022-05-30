<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ConflictManagment.aspx.cs" Inherits="Members_Message_ConflictManagment" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnc" %>
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

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


<%--            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
          --%>
              

          
            <TSPControls:CustomAspxDevGridView ID="GridViewConflictManagment" Width="100%" runat="server"
                DataSourceID="ObjdsConflictManagment"  AutoGenerateColumns="False" KeyFieldName="ConfId" 
                ClientInstanceName="GridViewConflictManagment" EnableViewState="False">
            
                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ConfId"
                        Name="ConfId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="MobileNo" Caption="شماره موبایل">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                     <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="RegisterDate" Caption="تاریخ ثبت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع عدم انطباق">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="TypeCodeName" Caption="حوزه مربوطه">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="SatisfaiedDate" Caption="تاریخ بسته شدن">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="SatisfaiedName"
                        Caption="وضعیت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description"
                        Caption="توضیحات" Name="Description">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtDescription" ClientInstanceName="txtDescription" runat="server"
                                ValueField="Description" TextField="Description"
                                Value='<%# Bind("Description") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewCommandColumn Caption=" " Visible="true" VisibleIndex="3" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>

            <asp:ObjectDataSource ID="ObjdsConflictManagment" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetAllForManagementPage" TypeName="TSP.DataManager.ConflictManager">
                <SelectParameters>
                    <asp:Parameter Name="MeId" Type="Int32" DefaultValue="-1" />
                </SelectParameters>

            </asp:ObjectDataSource>
</asp:Content>

