<%@ Page Language="C#" MasterPageFile="~/LCMaster.master" AutoEventWireup="true" CodeFile="ArchivedPolls.aspx.cs" Inherits="ArchivedPolls" %>

<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div>
        <div id="ContentTitle">
            <h1>
                Archived Polls</h1>
        </div>
        <div id="ContentBody">
            <p>
                Here is the complete list of archived polls run in the past. Click on the poll's
                question to see its results.</p>
            <asp:ListView ID="lvPolls" runat="server" ItemPlaceholderID="itemPlaceHolder">
                <LayoutTemplate>
                    <div runat="server" id="itemPlaceHolder" />
                </LayoutTemplate>
                <ItemTemplate>
                    <div>
                        <img src="Images/ArrowR2.gif" />
                        <a href="javascript:toggleDivState('poll<%# Eval("PollID") %>');">
                            <%# Eval("QuestionText") %></a> <small>(archived on
                                <%# Eval("ArchivedDate", "{0:d}") %>)</small>
                        <div style="display: none;" id="poll<%# Eval("PollID") %>">
                            <uc1:PollBox ID="PollBox1" runat="server" PollID='<%# Eval("PollID") %>' ShowHeader="False"
                                ShowQuestion="False" ShowArchiveLink="False" />
                        </div>
                        <asp:ImageButton runat="server" ID="ibtnDelete" ImageUrl="~/Images/Delete.gif" CommandName="Delete" />
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div>
                        <b>No polls to show</b></div>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="LeftColumn" runat="Server">
    <uc1:Featuredproduct ID="Featuredproduct1" runat="server" />
    <uc1:PollBox ID="PollBox1" runat="server" />
</asp:Content>
