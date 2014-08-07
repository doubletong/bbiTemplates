<%@ Page Language="C#" MasterPageFile="~/CRMaster.master" AutoEventWireup="true" CodeFile="GenericError.aspx.cs" Inherits="GenericError" %>


<asp:Content ID="CenterColumnContent" ContentPlaceHolderID="CenterColumn" runat="Server">
    <div>
        <div id="ContentTitle">
            <h1>
                Opps We Have a Problem</h1>
        </div>
        <div id="ContentBody">
            <p>{Place Message Here}
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="RightColumnContent" ContentPlaceHolderID="RightColumn" runat="Server">
</asp:Content>