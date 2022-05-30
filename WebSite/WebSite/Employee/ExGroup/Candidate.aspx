<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Candidate.aspx.cs" Inherits="Employee_ExGroup_Candidate" Title="مدیریت نامزدهای" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


  
                            <table  >
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                            <Image  Url="~/Images/new.png" >
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <Image  Url="~/Images/icons/edit.png" >
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel" runat="server" 
                                            EnableTheming="False" EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            AutoPostBack="false" CausesValidation="False" Text=" " 
                                            EnableTheming="False" ToolTip="غیر فعال" ID="btninactive" EnableViewState="False"
                                            OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
  grid.PerformCallback('inactive');
  }

}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/disactive.png">
                                            </Image>
                                            
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            ID="btnprint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>
                                           
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                            ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){ btnTempExport.DoClick();}"></ClientSideEvents>
                                           
                                            <Image  Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" OnClick="btnback_Click">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                      </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
     
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  
                    SeparatorWidth="1px" SeparatorHeight="100%" SeparatorColor="#A5A6A8" OnItemClick="ASPxMenu1_ItemClick"
                    ItemSpacing="0px"  AutoSeparators="RootOnly" RightToLeft="True">
                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-2" LastItemX="-1" LastItemY="-2"
                        X="-1" Y="-2" />
                    <Items>
                        <dxm:MenuItem Name="Period" Text="مشخصات دوره">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Candid" Selected="true" Text="نامزدها">
                        </dxm:MenuItem>
                    </Items>
                    <VerticalPopOutImage Height="8px" Width="4px" />
                    <ItemStyle ImageSpacing="5px" PopOutImageSpacing="7px" VerticalAlign="Middle" />
                    <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1" />
                    <SubMenuItemStyle ImageSpacing="7px">
                    </SubMenuItemStyle>
                    <Border BorderColor="#A5A6A8" BorderStyle="Solid" BorderWidth="1px" />
                    <HorizontalPopOutImage Height="7px" Width="7px" />
                </TSPControls:CustomAspxMenuHorizontal>
             
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewCandidate" runat="server" DataSourceID="ObjectDataSourceCandidate"
                  AutoGenerateColumns="False"
                KeyFieldName="CandidateId" ClientInstanceName="grid" OnCustomCallback="GridViewCandidate_CustomCallback"
                Width="100%">
                <ClientSideEvents EndCallback="function(s, e) {
       if(s.cpDoPrint==1)
            {
               s.cpDoPrint = 0;
	           window.open('../../Print.aspx');
            }
}" />
               
                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="100px" FieldName="FirstName"
                        Caption="نام">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی" Width="150px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastMjName" Caption="رشته در تشکل" Width="150px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StatusName" Caption="وضعیت">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="VoteCount" Caption="تعداد آرا">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="IsManagerName" Caption="نوع نامزد">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Position" Caption="سمت">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActiveName" Caption="وضعیت فعالیت">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataImageColumn FieldName="Attachment" Caption="فایل"
                            VisibleIndex="0" Width="150px">
                            <EditCellStyle Wrap="False">
                            </EditCellStyle>
                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                </Columns>
          
                 
            </TSPControls:CustomAspxDevGridView>
            <br />
          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False"
                                            AutoPostBack="true">
                                            <Image  Url="~/Images/new.png" >
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <Image  Url="~/Images/icons/edit.png" >
                                            </Image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel2" runat="server" ClientVisible="false" 
                                            EnableTheming="False" EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            AutoPostBack="false" CausesValidation="False" Text=" " 
                                            EnableTheming="False" ToolTip="غیر فعال" ID="btninactive2" EnableViewState="False"
                                            OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');

}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/disactive.png">
                                            </Image>
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            ID="btnprint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>
                                           
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                            ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){btnTempExport.DoClick(); }"></ClientSideEvents>
                                            
                                            <Image  Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" OnClick="btnback_Click">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                       
  </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjectDataSourceCandidate" runat="server" TypeName="TSP.DataManager.CandidateManager"
                SelectMethod="FindByExGroupPeriodId">
                <SelectParameters>
                    <asp:Parameter Name="ExGroupPeriodId" Type="Int32" DefaultValue="-1" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img alt="" src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewCandidate">
    </dxwgv:ASPxGridViewExporter>
    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
        runat="server" OnClick="btntemp_Click">
    </TSPControls:CustomAspxButton>
    <dx:ASPxHiddenField ID="HiddenFieldID" runat="server">
    </dx:ASPxHiddenField>
</asp:Content>
