<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTag.ascx.cs" Inherits="AdministrationPart.AddTag" %>

<div class="p-2">
    <div class="form-floating mb-3">
        <input runat="server" id="txtTagName" type="text" class="form-control" placeholder="2" required>
        <label for="TagName">Tag name</label>
    </div>
    <div class="form-floating mb-3">
        <input runat="server" id="txtTagNameEng" type="text" class="form-control" placeholder="2" required>
        <label for="TagName">Tag name english</label>
    </div>
    <div>
        <asp:DropDownList runat="server" ID="ddlTagType" CssClass="form-select mb-3"></asp:DropDownList>
    </div>
    <div>
        <asp:Button Style="width: 100%" ID="btnAdd" OnClick="btnAdd_Click" runat="server" class="btn btn-success" Text="Add"></asp:Button>
    </div>
</div>
