<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="WingtipToys.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <asp:FormView ID="productDetail" runat="server" ItemType="WingtipToys.Models.Product" SelectMethod="GetProduct" RenderOuterTable="false">
    <ItemTemplate>
      <div class="row" style="margin-top: 40px;">
        <div class="col-md-7">
          <img src="/Catalog/Images/<%#: Item.ImagePath %>" style="width: 100%; height: auto;" alt="<%# Item.Name %>" />
        </div>
        <div class="col-md-5">
          <h1><%#: Item.Name %></h1>
          <div class="form-group">
            <%#: Item.Description %>
          </div>
          <div class="form-group text-warning" style="font-size: 2.5rem;">
            <strong><%#: $"{Item.UnitPrice:c2}" %></strong>
          </div>
          <div class="row">
            <div class="col-md-3">
              <div class="form-group float-label">
                <asp:Label runat="server" AssociatedControlID="txtQuantity">Quantity</asp:Label>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" MaxLength="3" TextMode="Number" min="1" max="999" placeholder="Quantity" />
                <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" CssClass="text-danger" Display="Dynamic">Enter a quantity</asp:RequiredFieldValidator>
              </div>
            </div>
          </div>          
          <div class="form-group">
            <asp:LinkButton runat="server" CssClass="btn btn-primary" CommandName="AddToCart" CommandArgument="<%#: Item.ProductId %>">Add to Cart</asp:LinkButton>
          </div>
        </div>
      </div>
    </ItemTemplate>
  </asp:FormView>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="injectedStyles">
  <link rel="stylesheet" href="/Content/float-label.css" />
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="injectedScripts">
  <script src="/Scripts/float-label.js"></script>
</asp:Content>