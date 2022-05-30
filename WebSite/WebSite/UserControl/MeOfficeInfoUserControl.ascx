<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MeOfficeInfoUserControl.ascx.cs"
    Inherits="UserControl_MeOfficeInfoUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<fieldset id="RoundPanelMain"
    runat="server">
    <legend class="HelpUL">اطلاعات شرکت عضو</legend>
 
    <TSPControls:CustomAspxDevGridView2 ID="GridViewOfficeMember" ClientInstanceName="GridViewOfficeMember"
        runat="server" Width="100%"
        AutoGenerateColumns="False" KeyFieldName="OfmId">
        <Columns>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfId" Caption="کد شرکت"
                Name="OfId">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfName" Caption="نام شرکت"
                Name="OfName">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MFTypeName" Caption="نوع شرکت"
                Name="MFTypeName">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MembershipRequstType" Caption="زمینه موضوعی شرکت"
                Name="">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>  
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfficeFileNo" Caption="پروانه شرکت"
                Name="OfficeFileNo">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfficeExp" Caption="تاریخ اعتبار شرکت"
                Name="OfficeExp">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MemberGradeInOffice" Caption="پایه عضو در شرکت"
                Name="MemberGradeInOffice">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfficeManager" Caption="مدیر عامل"
                Name="OfficeManager">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DocumentStatusName" Caption="وضعیت پروانه"
                Name="DocumentStatusName">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TaskName" Caption="وضعیت آخرین درخواست"
                Name="TaskName">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LastReqCreatDate" Caption="تاریخ آخرین درخواست"
                Name="LastReqCreatDate">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>          
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="16" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>

    </TSPControls:CustomAspxDevGridView2>
</fieldset>
