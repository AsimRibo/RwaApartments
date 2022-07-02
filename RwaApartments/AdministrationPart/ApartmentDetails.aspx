<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ApartmentDetails.aspx.cs" Inherits="AdministrationPart.ApartmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <%--First row--%>
        <div class="row mb-4 justify-content-center">
            <div class="col-sm-8">
                <div id="carouselExampleCaptions" class="carousel slide overflow-hidden" data-bs-ride="false">
                    <div class="carousel-inner">

                        <div class="carousel-item active">
                            <img runat="server" src="..." id="imgActive" class="d-block w-100" alt="Representative picture" style="height: 25rem">
                            <div class="carousel-caption d-none d-md-block">
                                <asp:Label runat="server" ID="lblDescription"></asp:Label>
                            </div>
                        </div>
                        <asp:Literal runat="server" ID="literalPictures">

                        </asp:Literal>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                </div>
            </div>
        </div>


        <%--Second row--%>
        <div class="row mb-2 justify-content-center">
            <div class="form-group col-sm-3">
                <label for="txtName">Name</label>
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group col-sm-3">
                <label for="txtOwner">Owner</label>
                <asp:TextBox runat="server" ID="txtOwner" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group col-sm-2">
                <label for="txtCreatedAt">Created at</label>
                <asp:TextBox runat="server" ID="txtCreatedAt" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>


        <%--Third row--%>
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


        <%--Fourth row--%>
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


        <%--Fifth row--%>
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


        <%--Sixth row--%>
        <div class="row mb-2 justify-content-center">

            <div class="form-group col-sm-8">
                <label for="blTags">Tags</label>
                <asp:BulletedList runat="server" ID="blTags" CssClass="border h-50 overflow-auto bg-light"></asp:BulletedList>
            </div>
        </div>
    </div>
</asp:Content>
