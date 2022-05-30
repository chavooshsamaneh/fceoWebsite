<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="TrainingRules.aspx.cs" Inherits="Employee_Amoozesh_TrainingRules" Title="احکام آموزشی" %>

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
           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                               <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                   width="100%">
                                   <tbody>
                                       <tr>
                                           <td style="vertical-align: top; text-align: right">
                                               <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                   <tbody>
                                                       <tr>
                                                           <td>
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" EnableTheming="False"
                                                                   EnableViewState="False" OnClick="btnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                   <ClientSideEvents Click="function(s, e) {
		
}" />
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                           <td>
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                                                   EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                                   <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                           <td>
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                   EnableTheming="False" EnableViewState="False"
                                                                   Text=" " ToolTip="ارسال به مرحله بعد" UseSubmitBehavior="False">
                                                                   <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}" />
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                           <td>
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                                                   EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                   ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                       </tr>
                                                   </tbody>
                                               </table>
                                           </td>
                                       </tr>
                                   </tbody>
                               </table>
                          </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                  DataSourceID="ObjectDataSourceGrid" ClientInstanceName="gridview" KeyFieldName="TrId">
                
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="TrId" Visible="False" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="Subject" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام عضو" FieldName="MeName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع" FieldName="TrTypeName" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
               
            </TSPControls:CustomAspxDevGridView>
            <br /> <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                               <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                   width="100%">
                                   <tbody>
                                       <tr>
                                           <td style="vertical-align: top; text-align: right">
                                               <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                   <tbody>
                                                       <tr>
                                                           <td>
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" EnableTheming="False"
                                                                   EnableViewState="False" OnClick="btnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                           <td style="width: 30px">
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView1" runat="server" CausesValidation="False"
                                                                   EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                                   ToolTip="مشاهده" UseSubmitBehavior="False">
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                                   <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                           <td>
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep1" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                   EnableTheming="False" EnableViewState="False"
                                                                   Text=" " ToolTip="ارسال به مرحله بعد" UseSubmitBehavior="False">
                                                                   <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}" />
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                           <td>
                                                               <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton3" runat="server" CausesValidation="False"
                                                                   EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                   ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                   <HoverStyle BackColor="#FFE0C0">
                                                                       <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                   </HoverStyle>
                                                                   <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                               </TSPControls:CustomAspxButton>
                                                           </td>
                                                       </tr>
                                                   </tbody>
                                               </table>
                                           </td>
                                       </tr>
                                   </tbody>
                               </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        <asp:ObjectDataSource ID="ObjectDataSourceGrid" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TrainingRulesManager"></asp:ObjectDataSource>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                   <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress> 
        <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" AllowDragging="True" ClientInstanceName="PopupWorkFlow"
            CloseAction="CloseButton"  
            HeaderText=""  Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Width="387px">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelWorkFlow" runat="server" ClientInstanceName="CallbackPanelWorkFlow"
                     OnCallback="CallbackPanelWorkFlow_Callback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxp:ASPxPanel ID="PanelMain" runat="server" ClientInstanceName="PanelMain" Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            &nbsp;<table>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="2">
                                                            <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError" ForeColor="Red" Visible="False"></dxe:ASPxLabel>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; text-align: right">
                                                            <dxe:ASPxLabel runat="server" Text="ارسال به مرحله:" Font-Size="X-Small" ID="lblSenBack"></dxe:ASPxLabel>


                                                        </td>
                                                        <td style="text-align: right" dir="ltr">
                                                            <TSPControls:CustomAspxComboBox runat="server"  ID="cmbSendBackTask"  ValueType="System.String" >
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>

                                                                <ButtonStyle Width="13px"></ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>


                                                            &nbsp; </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; width: 159px; height: 37px; text-align: right">
                                                            <dxe:ASPxLabel runat="server" Text="توضیحات:" Font-Size="X-Small" Width="56px" ID="ASPxLabel1"></dxe:ASPxLabel>


                                                        </td>
                                                        <td style="width: 600px; height: 37px" dir="rtl">
                                                            <TSPControls:CustomASPXMemo runat="server" Height="71px"  Width="100%" ID="txtDescription" >
                                                                <ValidationSettings>
                                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomASPXMemo>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 37px; text-align: center" dir="ltr" colspan="2">
                                                            <TSPControls:CustomAspxButton runat="server" Text="ارسال"  Width="93px" ID="btnSendNextWorkStep" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep" >
                                                                <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	gridview.PerformCallback('');
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxButton>


                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>
                                <dxp:ASPxPanel ID="PanelSaveSuccessfully" runat="server" ClientInstanceName="PanelSaveSuccessfully"
                                    Height="100%" Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <br />
                                            <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small" ID="lblTeacherWarning" ForeColor="Red"></dxe:ASPxLabel>


                                            <br />
                                            <br />
                                            <TSPControls:CustomAspxButton  runat="server" Text="خروج"  Width="93px" ID="btnClose" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep" >
                                                <ClientSideEvents Click="function(s, e) {	
	//CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Hide();
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>


                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomAspxCallbackPanel>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
            </HeaderStyle>
            <SizeGripImage Height="12px" Width="12px" />
            <CloseButtonImage Height="17px" Width="17px" />
        </TSPControls:CustomASPxPopupControl> 
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectByWorkId"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
  

</asp:Content>


