<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapacityUserControl.ascx.cs"
    Inherits="UserControl_CapacityUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

            	<fieldset ><legend class="HelpUL" id="RoundPanelCapacity" runat="server">ظرفیت</legend>
                   <ul class="HelpUL">
                <li>متراژ کسر ظرفیت شخص را متراژ واقعی کارکرد وی وارد نمایید.متراژ کسر ظرفیت بر اساس نوع پروژه برای شخص محاسبه می گردد. </li>
                    </ul>
            <table id="Table1" width="100%">
                <tbody>                   
                    <tr>
                        <td valign="top" align="right" width="22%">
                            <dxe:ASPxLabel runat="server" Text="درصد متراژ کسر ظرفیت پروژه" ID="ASPxLabel1">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right" width="28%">
                            <dxe:ASPxLabel runat="server" ID="txtcDecrementPercent" ClientInstanceName="txtcDecrementPercent"  Width="100%"
                                   Font-Bold="true"
                                 Text="0">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right" width="22%">
                            <dxe:ASPxLabel runat="server" Text="درصد متراژ دستمزد پروژه" ID="ASPxLabel2">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right" width="28%">
                            <dxe:ASPxLabel runat="server" ID="txtcWagePercent" ClientInstanceName="txtcWagePercent"  Width="100%"
                                  Font-Bold="true"
                                 Text="0">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" width="22%">
                            <dxe:ASPxLabel runat="server" Text="متراژ کل پروژه" ID="ASPxLabel18">
                            </dxe:ASPxLabel></td>
                        <td valign="top" align="right" width="22%">
                            <dxe:ASPxLabel runat="server" ID="txtcFoundation" ClientInstanceName="txtcFoundation"  Width="100%"
                                  Font-Bold="true"
                                 Text="---">
                            </dxe:ASPxLabel></td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="ظرفیت کل" ID="ASPxLabeTotalCapacity">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" ID="txtcTotalCapacity" ClientInstanceName="txtcTotalCapacity"  Width="100%"
                                  Font-Bold="true"
                                 Text="---">
                            </dxe:ASPxLabel>
                        </td>
                       
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                             <dxe:ASPxLabel runat="server" Text="حداکثر ظرفیت مجاز طراحی صدرا" ID="lblSadraCapacity">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            
                            <dxe:ASPxLabel runat="server" ID="txtSadraCapacity" ClientInstanceName="txtSadraCapacity"  Width="100%"
                                  Font-Bold="true"
                                 Text="---">
                            </dxe:ASPxLabel>
                        </td>

                         <td valign="top" align="right">
                             <dxe:ASPxLabel runat="server" Text="حداکثر ظرفیت مجاز نظارت صدرا" ID="lblSadraCapacityOBS">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            
                            <dxe:ASPxLabel runat="server" ID="txtSadraCapacityOBS"  ClientInstanceName="txtSadraCapacityOBS" Width="100%"
                                  Font-Bold="true"
                                 Text="---">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="ظرفیت باقیمانده" ID="ASPxLabeRemainCapacity">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" ID="txtcRemainCapacity"   ClientInstanceName="txtcRemainCapacity"  Width="100%"
                                  Font-Bold="true"
                                  Text="---">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="کل کارکرد" ID="ASPxLabeTotalFunction">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" ID="txtcTotalFunction"  ClientInstanceName="txtcTotalFunction" Width="100%"
                                  Font-Bold="true"
                                 Text="---">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="تعداد پروژه" ID="ASPxLabeProjectCount">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" ID="txtcProjectCount"   ClientInstanceName="txtcProjectCount" Width="100%"
                                  Font-Bold="true"
                                 Text="---">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="کل رزرو شده" ID="LabeReserve">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top">
                            <dxe:ASPxLabel runat="server" ID="txtcReserve" ClientInstanceName="txtcReserve"  Width="100%"
                                  Font-Bold="true"
                                 Text="---">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel Font-Bold="true" runat="server" Text="متراژ کارکرد طراح" ID="lblcRealCapacityDecrement">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtcRealCapacityDecrement" ClientInstanceName="txtcRealCapacityDecrement"
                                 Width="100%" >
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="متراژ کارکرد را وارد نمایید"></RequiredField>
                                    <RegularExpression ErrorText="این مقدار صحیح نیست" ValidationExpression="[-+]?\d*"></RegularExpression>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){
                                txtcRealWage.SetText('');
                                txtcCapacityDecrement.SetText('');
                                txtcWage.SetText('');
                                if(parseInt(txtcRealCapacityDecrement.GetText())>parseInt(txtcFoundation.GetText()))
                                {
                                    alert('متراژ کارکرد وارد شده بیش از متراژ کل پروژه می باشد.');
                                    return;
                                }
                                if(100 >=parseInt(txtcFoundation.GetText()))
                                {
                                txtcRealWage.SetText('100');
                                txtcCapacityDecrement.SetText(Math.round(txtcRealCapacityDecrement.GetText()*txtcDecrementPercent.GetText()/100));
                                txtcWage.SetText(Math.round(txtcRealWage.GetText()*txtcWagePercent.GetText()/100));
                                }
                                else
                                {
                                txtcRealWage.SetText(txtcRealCapacityDecrement.GetText());
                                txtcCapacityDecrement.SetText(Math.round(txtcRealCapacityDecrement.GetText()*txtcDecrementPercent.GetText()/100));
                                txtcWage.SetText(Math.round(txtcRealWage.GetText()*txtcWagePercent.GetText()/100));
                                }                                                               
                                }" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel Font-Bold="true" runat="server" Text="متراژ واقعی دستمزد طراح" ID="lblRealcWage">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtcRealWage" ClientInstanceName="txtcRealWage" 
                                Width="100%"  >
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="متراژ واقعی دستمزد را وارد نمایید"></RequiredField>
                                    <RegularExpression ErrorText="این مقدار صحیح نیست" ValidationExpression="[-+]?\d*"></RegularExpression>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel Font-Bold="false" ForeColor="DarkViolet" runat="server" Text="متراژ کسر ظرفیت طراح:" ID="lblcCapacityDecrement">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtcCapacityDecrement" ClientInstanceName="txtcCapacityDecrement"
                                 Width="100%"    Font-Bold="true"
                                >
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="متراژ کسر ظرفیت را وارد نمایید"></RequiredField>
                                  <%--  <RegularExpression ErrorText="این مقدار صحیح نیست" ValidationExpression="\d*"></RegularExpression>--%>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){txtcWage.SetText(txtcCapacityDecrement.GetText())}" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td valign="top" align="right">
                            <dxe:ASPxLabel Font-Bold="false" ForeColor="DarkViolet" runat="server" Text="متراژ دستمزد طراح:" ID="lblcWage">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtcWage" ClientInstanceName="txtcWage" 
                                Width="100%"    Font-Bold="true"
                                >
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="متراژ دستمزد را وارد نمایید"></RequiredField>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
               <TSPControls:CustomAspxDevGridView2 ID="GridViewProject" runat="server" Width="100%"
                ClientInstanceName="GridViewProject" DataSourceID="ObjectDataSourceReportMemberWageByCity"
                KeyFieldName="CitId" AutoGenerateColumns="False">
                <SettingsCookies Enabled="true" />              
                <Settings ShowHorizontalScrollBar="true" ShowFooter="true"></Settings>
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="SumWage" SummaryType="Sum" />
                    <dxwgv:ASPxSummaryItem FieldName="SumCapacityDecrement" SummaryType="Sum" />
                </TotalSummary>
                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="CitName"
                        Caption="شهر" Name="CitName">
                        <CellStyle Wrap="True" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="ProjectIngridientResName"
                        Caption="مسئولیت" Name="ProjectIngridientResName">
                        <CellStyle Wrap="True" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="SumWage" Caption="مجموع کسر دستمزد"
                        Name="CapacityDecrement" Width="100px">
                        <PropertiesTextEdit EnableFocusedStyle="False">
                        </PropertiesTextEdit>
                        <CellStyle Wrap="True" HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle Wrap="False" />
                    </dxwgv:GridViewDataTextColumn>
                      <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SumCapacityDecrement" Caption="مجموع کسر ظرفیت"
                        Name="SumCapacityDecrement" Width="100px">
                        <PropertiesTextEdit EnableFocusedStyle="False">
                        </PropertiesTextEdit>
                        <CellStyle Wrap="True" HorizontalAlign="Center">
                        </CellStyle>
                        <HeaderStyle Wrap="False" />
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView2>
            <asp:ObjectDataSource ID="ObjectDataSourceReportMemberWageByCity" runat="server" SelectMethod="ReportMemberWageByCity" TypeName="TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager">
                <SelectParameters>
                    <asp:Parameter DbType="Int32" DefaultValue="-2" Name="MeId" />
                    <asp:Parameter DbType="Int32" DefaultValue="-1" Name="ProjectIngridientTypeId" />
                </SelectParameters>

            </asp:ObjectDataSource>
      </fieldset>
