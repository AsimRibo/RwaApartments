<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="AdministrationPart.Tags" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    @<%@ Register Src="~/AddTag.ascx" TagName="addTag" TagPrefix="uc" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="container py-4">
        <%--Add panel--%>
        <asp:Panel ID="panelAdd" runat="server" CssClass="container mt-5" Visible="true">
            <asp:PlaceHolder runat="server" ID="phAddTag" />
        </asp:Panel>
    </div>

    <div class="container py-4">
        <%--Message output--%>
        <asp:Panel ID="panelMessage" runat="server" CssClass="container mt-5" Visible="false">
            <div class="alert alert-danger" role="alert">
                <label>Only tags with tag count value 0 can be deleted!</label>
            </div>
        </asp:Panel>
    </div>

    <div class="container py-4">
        <div>
            <asp:Button runat="server" ID="btnAddTag" CssClass="btn btn-success" Text="Add tag" OnClick="btnAddTag_Click" />
        </div>
        <div class="row">
            <asp:Repeater runat="server" ID="rptTags">
                <HeaderTemplate>
                    <table id="myTable" class="table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Name</th>
                                <th scope="col">Created at</th>
                                <th scope="col">Tag count</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval(nameof(RwaUtilities.Models.Tags.Tag.Id)) %></th>
                        <td><%# Eval(nameof(RwaUtilities.Models.Tags.Tag.NameEng)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.Tags.Tag.CreatedAt)) %></td>
                        <td><%# Eval(nameof(RwaUtilities.Models.Tags.Tag.TagCount)) %></td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" CommandArgument="<%# Eval(nameof(RwaUtilities.Models.Tags.Tag.Id)) %>" CssClass="btn btn-secondary" OnClientClick="return confirm('Are you sure you want to delete this tag?');"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
