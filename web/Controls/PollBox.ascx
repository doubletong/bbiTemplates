<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollBox.ascx.cs" Inherits="PollBox" %>
    <div class="SideBarTitle">
        <asp:Label ID="lblQuestion" runat="server" meta:resourcekey="lblQuestionResource1"></asp:Label>
    </div>
    <div class="SideBarContent">
        <asp:Panel ID="panVote" runat="server" meta:resourcekey="panVoteResource1">
            <div class="polloptions">
                <asp:RadioButtonList ID="optlOptions" runat="server" DataTextField="OptionText" DataValueField="OptionID"
                    meta:resourcekey="optlOptionsResource1">
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="valRequireOption" runat="server" ControlToValidate="optlOptions"
                    Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                    ToolTip="You must select an option." ValidationGroup="PollVote" meta:resourcekey="valRequireOptionResource1">You must select an option.</asp:RequiredFieldValidator></div>
            <asp:Button ID="btnVote" runat="server" Text="Vote" ValidationGroup="PollVote" meta:resourcekey="btnVoteResource1" /></asp:Panel>
        <asp:Panel ID="panResults" runat="server" meta:resourcekey="panResultsResource1">
            <div class="polloptions">
                <asp:ListView runat="server" ID="lvOptions">
                    <LayoutTemplate>
                        <div runat="server" id="itemPlaceHolder">
                        </div>
                    </LayoutTemplate>
                    <ItemSeparatorTemplate>
                        <img id="Img1" runat="server" src="~/Images/spacer.gif" height="5" meta:resourcekey="imgSeparatorResource1"
                            alt="" /><br />
                    </ItemSeparatorTemplate>
                    <ItemTemplate>
                        <div>
                            <%# Eval("OptionText") %>
                            <small>(<%# Eval("Votes") %>
                                vote(s) -
                                <%# GetFixedPercentage((int)Eval("Votes"), TotalVotes) %>
                                %)</small>
                            <br />
                            <div class="pollbar" style="width: <%# GetFixedPercentage((int)Eval("Votes"), TotalVotes) %>%">
                                &nbsp;</div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <img id="Img1" runat="server" src="~/Images/spacer.gif" height="5" meta:resourcekey="imgSeparatorResource2"
                    alt="" /><br />
                <b>
                    <asp:Localize runat="server" ID="locTotVotes" meta:resourcekey="locTotVotesResource1"
                        Text="Total votes:"></asp:Localize>
                    <asp:Label runat="server" ID="lblTotalVotes" meta:resourcekey="lblTotalVotesResource1" /></b>
            </div>
        </asp:Panel>
        <img id="Img2" runat="server" src="~/Images/spacer.gif" height="10" meta:resourcekey="imgSeparatorResource1"
            alt="" /><br />
        <asp:HyperLink runat="server" ID="lnkArchive" NavigateUrl="~/ArchivedPolls.aspx"
            Text="Archived Polls" meta:resourcekey="lnkArchiveResource1" />
    </div>
</div>
