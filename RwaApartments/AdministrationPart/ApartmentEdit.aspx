<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ApartmentEdit.aspx.cs" Inherits="AdministrationPart.ApartmentEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container">
        <%--Message output--%>
        <asp:Panel ID="panelMessage" runat="server" CssClass="container mt-5 col-sm-8" Visible="false">
            <div class="alert alert-danger" role="alert">
                <label>You must select at least one tag</label>
            </div>
        </asp:Panel>


        <%--First row--%>
        <div class="row mb-2 justify-content-center">
            <div class="form-group col-sm-2">
                <label>Name</label>
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtName" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-sm-2">
                <label>Name eng</label>
                <asp:TextBox runat="server" ID="txtNameEng" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtNameEng" runat="server" ControlToValidate="txtNameEng" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
            </div>

            <div class="form-group col-sm-2">
                <label>Price</label>
                <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtPrice" runat="server" ControlToValidate="txtPrice" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPrice" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPrice" Operator="DataTypeCheck" Type="Currency">* Must be a number</asp:CompareValidator>
            </div>

            <div class="col-sm-2">
                <label>Status</label>
                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>



        <%--Second row--%>
        <div class="row mb-2 justify-content-center">
            <div class="form-group col-sm-2">
                <label>Max adults</label>
                <asp:TextBox runat="server" ID="txtMaxAdults" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtMaxAdults" runat="server" ControlToValidate="txtMaxAdults" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvTxtMaxAdults" runat="server" ControlToValidate="txtMaxAdults" ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck" Type="Integer">* Must be a number</asp:CompareValidator>
            </div>

            <div class="form-group col-sm-2">
                <label>Max children</label>
                <asp:TextBox runat="server" ID="txtMaxChildren" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtMaxChildren" runat="server" ControlToValidate="txtMaxChildren" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvTxtMaxChildren" runat="server" ControlToValidate="txtMaxChildren" ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck" Type="Integer">* Must be a number</asp:CompareValidator>
            </div>

            <div class="form-group col-sm-2">
                <label>Total rooms</label>
                <asp:TextBox runat="server" ID="txtTotalRooms" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtTotalRooms" runat="server" ControlToValidate="txtTotalRooms" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvTxtTotalRooms" runat="server" ControlToValidate="txtTotalRooms" ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck" Type="Integer">* Must be a number</asp:CompareValidator>
            </div>

            <div class="form-group col-sm-2">
                <label>Beach distance</label>
                <asp:TextBox runat="server" ID="txtBeachDistance" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtBeachDistance" runat="server" ControlToValidate="txtBeachDistance" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvTxtBeachDistance" runat="server" ControlToValidate="txtBeachDistance" ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck" Type="Integer">* Must be a number</asp:CompareValidator>
            </div>
        </div>


        <%--Third row--%>
        <div class="row mb-2 justify-content-center">
            <div class="col-sm-5">
                <div class="form-group mb-3">
                    <label>City</label>
                    <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-select"></asp:DropDownList>
                </div>


                <div class="form-group">
                    <label>Owner</label>
                    <asp:DropDownList runat="server" ID="ddlOwners" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>


            <div class="form-group col-sm-3">
                <label>Tags</label>
                <div class="border">
                    <div class="checkboxContainer">
                        <asp:CheckBoxList runat="server" ID="chbListTags" CssClass="form-check">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
        </div>

        <% if (ddlStatus.SelectedItem.ToString() != RwaUtilities.Models.ApartmentStatus.Vacant.ToString())
                {
            %>
        <div class="row mb-2 justify-content-center">
            <div class="col-sm-2">
                <label>Registered</label>
                <asp:CheckBox runat="server" ID="chbTypeOfUser" CssClass="form-check" AutoPostBack="true" />
            </div>
            <%  if (ddlStatus.SelectedItem.ToString() != RwaUtilities.Models.ApartmentStatus.Vacant.ToString() && chbTypeOfUser.Checked)
                {
            %>
            <div class="col-sm-6">
                <label>User</label>
                <asp:DropDownList runat="server" ID="ddlUsers" CssClass="form-select"></asp:DropDownList>
            </div>
            <%   }
                else if(ddlStatus.SelectedItem.ToString() != RwaUtilities.Models.ApartmentStatus.Vacant.ToString() && !chbTypeOfUser.Checked)
                {  %>
            <div class="col-sm-3">
                <label>Username</label>
                <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtUsername" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
            </div>

            <div class="col-sm-3">
                <label>Phone</label>
                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvTxtPhone" runat="server" ControlToValidate="txtPhone" ForeColor="Red" Display="Dynamic" Operator="DataTypeCheck" Type="Integer">* Must be a number</asp:CompareValidator>
            </div>
            <% } %>
        </div>

        <% if (ddlStatus.SelectedItem.ToString() != RwaUtilities.Models.ApartmentStatus.Vacant.ToString())
            { %>
        <div class="row mb-2 justify-content-center">
            <div class="col-sm-8">
                <label>Details</label>
                <asp:TextBox runat="server" ID="txtDetails" CssClass="form-control" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtDetails" runat="server" ControlToValidate="txtDetails" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
            </div>
        </div>
        <% }
            if (ddlStatus.SelectedItem.ToString() != RwaUtilities.Models.ApartmentStatus.Vacant.ToString() && !chbTypeOfUser.Checked)
            {  %>
        <div class="row mb-2 justify-content-center">
            <div class="col-sm-4">
                <label>Email</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ID="revTxtEmail" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">* Invalid email</asp:RegularExpressionValidator>
            </div>
            <div class="col-sm-4">
                <label>Address</label>
                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTxtAddress" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ForeColor="Red">* Can't be empty</asp:RequiredFieldValidator>
            </div>
        </div>

        <% } %>
        <% } %>

        <%--Fourth row--%>
        <div class="row mb-2 justify-content-center">
            <div class="col-sm-3">
                <div class="form-group">
                    <asp:FileUpload ID="fuImage" runat="server" CssClass="my-3" accept=".jpg, .jpeg, .png" />
                </div>
                <div class="form-group">
                    <asp:Button runat="server" ID="btnAddImage" OnClick="btnAddImage_Click" CausesValidation="false" Text="Add" CssClass="btn btn-primary" />
                </div>
            </div>

            <div class="col-sm-5">
                <div class="gridViewContainer border mt-2">
                    <asp:GridView runat="server" ID="gvImages" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" CssClass="table table-borderless" BorderStyle="None">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="mb-3">
                                        <asp:Image ID="imgApartment" ImageUrl="<%#Eval(nameof(RwaUtilities.Models.ApartmentImage.Path))%>" runat="server" Height="150" Width="250" CssClass="mx-auto d-block" />
                                    </div>
                                    <div class="mb-3" style="display: flex; flex-direction: row; align-items: center; justify-content: space-between">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text="<%#Eval(nameof(RwaUtilities.Models.ApartmentImage.Name))%>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTxtName" runat="server" ControlToValidate="txtName" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="mb-3" style="display: flex; flex-direction: row; align-items: center; justify-content: space-between">
                                        <asp:LinkButton ID="btnDeleteImage" CssClass="btn btn-danger" runat="server" CommandArgument="<%#Eval(nameof(RwaUtilities.Models.ApartmentImage.Guid)) %>" OnClick="btnDeleteImage_Click" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this image?');" CausesValidation="false"></asp:LinkButton>
                                        <asp:Label runat="server" ID="lblGuid" Visible="false" Text="<%#Eval(nameof(RwaUtilities.Models.ApartmentImage.Guid)) %>"></asp:Label>
                                        <div style="display: flex; flex-direction: row; align-items: center; gap: .5em">
                                            <asp:LinkButton runat="server" ID="btnSetRepresentative" CssClass="btn btn-secondary" Text="Set representative" CommandArgument="<%#Eval(nameof(RwaUtilities.Models.ApartmentImage.Guid)) %>" OnClick="btnSetRepresentative_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <%--Fifth row--%>
        <div class="row justify-content-center">
            <div class="col-sm-8 my-5">
                <asp:Button runat="server" ID="btnUpdate" Text="Update apartment" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>
