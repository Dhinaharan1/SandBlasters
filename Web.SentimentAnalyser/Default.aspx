<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table class="tblGrayBackground" width="70%">
        <tr>
            <td width="30%" valign="top">
                <h3>Load Feedback :</h3>
            </td>
            <td width="70%">
                <div style="height: 80px; vertical-align: middle;">
                    <asp:FileUpload ID="fileLoadFeedback" runat="server" CssClass="browsebutton" ClientIDMode="Static"
                        Height="1" Width="1" />
                </div>
            </td>
        </tr>
        
    </table>
    <table width="100%">
        <tr>
            <td>
                <div id="divGridView" style="width: 90%; margin-left: auto; margin-right: auto;">
                    <span style="display: none;">##StartTag##</span>
                    <div id="divGridViewDetails">
                        <div id="divShowOutput" runat="server" style="display: none;">
                          <p>  <a id="lnkShowOutput" runat="server" target="_blank" clientidmode="Static">Open Output File</a>
                            </p>
                          
                        </div>

                        <asp:GridView ID="gvFeedbackAnalysis" runat="server" AutoGenerateColumns="false"
                            ClientIDMode="Static" ShowHeaderWhenEmpty="true"
                            GridLines="None" CssClass="GridStyle" EmptyDataText="No Feedbacks Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Feedback" HeaderStyle-Width="500px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFeedbackContent" runat="server" Text='<%# Eval("strFeedbackContent") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="strScores" HeaderText="Score" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="strResults" HeaderText="Result" HeaderStyle-Width="100px" />
                                <asp:TemplateField HeaderText="Feedback" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <img src='<%# Eval("strEmoticons") %>' alt="emotion" height="28px" width="28px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <span style="display: none;">##EndTag##</span>
                </div>
            </td>
        </tr>
    </table>
    <script src="Default.aspx.cs.js" type="text/javascript"></script>
    <script src="Scripts/ajaxblockunblock.js" type="text/javascript"></script>
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
</asp:Content>
