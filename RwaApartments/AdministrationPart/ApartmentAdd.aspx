<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ApartmentAdd.aspx.cs" Inherits="AdministrationPart.ApartmentAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">

        <%--First row--%>
        <div class="row mb-2 justify-content-center">
            <div class="form-group col-sm-3">
                <label for="txtName">Name</label>
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-sm-3">
                <label for="txtOwner">Owner</label>
                <asp:TextBox runat="server" ID="txtOwner" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-sm-2">
                <label for="txtCreatedAt">Created at</label>
                <asp:TextBox runat="server" ID="txtCreatedAt" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        
        
        <%--Second row--%>
        <div class="row mb-2 justify-content-center">
            <div class="form-group col-sm-2">
                <label for="txtMaxAdults">Max adults</label>
                <asp:TextBox runat="server" ID="txtMaxAdults" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group col-sm-2">
                <label for="txtMaxChildren">Max children</label>
                <asp:TextBox runat="server" ID="txtMaxChildren" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group col-sm-2">
                <label for="txtTotalRooms">Total rooms</label>
                <asp:TextBox runat="server" ID="txtTotalRooms" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group col-sm-2">
                <label for="txtBeachDistance">Beach distance</label>
                <asp:TextBox runat="server" ID="txtBeachDistance" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        
        <%--Third row--%>
        <div class="row mb-2 justify-content-center">
            <div class="form-group col-sm-6">
                <label for="txtCity">City</label>
                <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group col-sm-2">
                <label for="txtPrice">Price</label>
                <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        
        <%--Fourth row--%>
        <div class="row mb-2 justify-content-center">
            <div class="form-group col-sm-4">
                <label for="txtStatus">Status</label>
                <asp:TextBox runat="server" ID="txtStatus" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group col-sm-4">
                <label for="txtReservedBy">Reserved by</label>
                <asp:TextBox runat="server" ID="txtReservedBy" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        
        <%--Fifth row--%>
        <div class="row mb-2 justify-content-center">

            <div class="form-group col-sm-8">
                <label for="blTags">Tags</label>
                <asp:BulletedList runat="server" ID="blTags" CssClass="border h-50 overflow-auto bg-light"></asp:BulletedList>
            </div>

        </div>
    </div>
</asp:Content>
