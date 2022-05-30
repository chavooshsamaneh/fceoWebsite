<%@ Page Title="مدیریت ناظران لیست حق الزحمه ناظرین" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AccountingDocumentDetail.aspx.cs" Inherits="Employee_TechnicalServices_Report_AccountingDocumentDetail" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0">
                    <tbody>
                        <tr>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="BtnNew_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccountingDocumentDetail.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                    ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccountingDocumentDetail.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer=confirm('آیا مطمئن به حذف ردیف انتخاب شده می باشید؟');		    
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server"  OnItemClick="MainMenu_ItemClick"  >
        <Items>
            <dxm:menuitem text="مشخصات لیست" name="AccDoc"  >
                            
                            </dxm:menuitem>
            <dxm:menuitem text="لیست ناظران" name="AccDocDetail" Selected="true" Enabled="false">
                            </dxm:menuitem>

        </Items>        
    </TSPControls:CustomAspxMenuHorizontal>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelDocumnetInfo"   ShowCollapseButton="true" AllowCollapsingByHeaderClick="true"   HeaderText="مشخصات لیست" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right" width="15%">شماره لیست                 
                            </td>
                            <td valign="top" align="right" width="35%">
                                <TSPControls:CustomTextBox runat="server" ID="txtListNo" Enabled="false"  Width="100%"
                                    ClientInstanceName="txtListNo" >
                                </TSPControls:CustomTextBox>
                            </td>
                            <td width="15%">تاریخ لیست
                            </td>
                            <td width="35%">
                                <TSPControls:CustomTextBox runat="server" ID="txtListDate" Enabled="false"   Width="100%"
                                    ClientInstanceName="txtListDate" >
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>وضعیت ارسال
                            </td>
                            <td>
                                <TSPControls:CustomTextBox runat="server" ID="txtStatusName" Enabled="false"   Width="100%"
                                    ClientInstanceName="txtStatusName" >
                                </TSPControls:CustomTextBox>
                            </td>
                            <td valign="top" align="right">نوع لیست                              
                            </td>
                            <td valign="top" align="right">
                                <TSPControls:CustomAspxComboBox Enabled="false"   runat="server"  Width="100%" 
                                    ID="CmbType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="CmbType"
                                     RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {

}"></ClientSideEvents>
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="نوع لیست را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <Items>
                                        <dxe:ListEditItem Value="0" Text="پرداخت حق الزحمه ناظر" ></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="1" Text="آزاد سازی بر اساس پرداخت اقساط"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="2" Text="اصلاحیه" />
                                    </Items>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </td>

                        </tr>
                        <tr >
                            <td valign="top" align="right">نام لیست *
                            </td>
                            <td colspan="3" valign="top" align="right">
                                <TSPControls:CustomTextBox Enabled="false"   runat="server" ID="txtListName"  Width="100%"
                                    ClientInstanceName="txtListName" >
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                       <RequiredField IsRequired="true" ErrorText="نام لیست را وارد نمایید" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>

                        </tr>                     
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewAccountingDocumentDetail" KeyFieldName="AccDocDetailId"
        runat="server" DataSourceID="ObjdAccountingDocumentDetail" Width="100%" ClientInstanceName="GridViewAccountingDocumentDetail">
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
        <SettingsCustomizationWindow Enabled="True" />
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="CapacityDecrement" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="Wage" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="ObserverInsurancePrice" SummaryType="Sum" />
            <%--<dxwgv:ASPxSummaryItem FieldName="InsurancePrice" SummaryType="Sum" />--%>
            <dxwgv:ASPxSummaryItem FieldName="InsuranceShare" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="NezamShare" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="ObserverShare" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="NezamKardanShare" SummaryType="Sum" />            
        </TotalSummary>
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت" FieldName="CreateDate" VisibleIndex="1"
                Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="1"
                Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ناظر" FieldName="ObserverName" VisibleIndex="1"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn Caption="نوع ناظر" FieldName="ObserversTypeName" VisibleIndex="4"
                Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="متراژ کسر ظرفیت" FieldName="CapacityDecrement"
                VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="متراژ دستمزد" FieldName="Wage" VisibleIndex="6"
                Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
     <%--        <dxwgv:GridViewDataTextColumn Caption="هزینه پایه بیمه(ریال)" FieldName="InsurancePrice"
                                        VisibleIndex="12" Width="200px">
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                        <PropertiesTextEdit DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تعرفه بیمه(ریال)" FieldName="ObserverInsurancePrice"
                                        VisibleIndex="13" Width="200px">
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                        <PropertiesTextEdit DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="سهم بیمه(ریال)" FieldName="InsuranceShare" VisibleIndex="6"
                Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سهم سازمان(ریال)" FieldName="NezamShare" VisibleIndex="6"
                Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سهم نظام کاردان ها(ریال)" FieldName="NezamKardanShare" VisibleIndex="6"
                Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سهم ناظر(ریال)" FieldName="ObserverShare"
                VisibleIndex="6" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد پروژه" FieldName="ProjectId" VisibleIndex="6"
                Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پلاک ثبتی" FieldName="RegisteredNo" VisibleIndex="6"
                Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مالک" FieldName="OwnerName" VisibleIndex="6"
                Width="150px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0">
                    <tbody>
                        <tr>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="BtnNew_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccountingDocumentDetail.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                    ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewAccountingDocumentDetail.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer=confirm('آیا مطمئن به حذف ردیف انتخاب شده می باشید؟');		    
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjdAccountingDocumentDetail" runat="server" SelectMethod="SelectDocumentDetail"
        TypeName="TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="-2" Name="AccDocId" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxHiddenField ID="HiddenFieldPage" runat="server"></dx:ASPxHiddenField>

</asp:Content>
