<%@ Page Title="Product Maintenance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WingtipToys.Admin.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <h1><%: Page.Title %></h1>
  <p></p>
  <asp:GridView ID="gvProducts" runat="server" CssClass="table table-striped table-bordered" ItemType="WingtipToys.Models.Product" SelectMethod="GetProducts" AutoGenerateColumns="false" AllowSorting="true"
    OnRowCommand="gvProducts_RowCommand">
    <Columns>
      <asp:BoundField HeaderText="Product Name" DataField="Name" SortExpression="Name" ItemStyle-Width="200px" />
      <asp:BoundField HeaderText="Cateogry" DataField="Category.CategoryName" SortExpression="Category.CategoryName" ItemStyle-Width="100px" />
      <asp:TemplateField HeaderText="Product Options">
        <ItemTemplate>
          <%#: GetProductOptionsForProduct(Item.ProductId) %>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <ItemStyle Width="250px" />
        <ItemTemplate>
          <div class="input-group">
            <asp:DropDownList ID="ddlProductOptions" runat="server" CssClass="form-control" ItemType="WingtipToys.Models.ProductOption" SelectMethod="GetProductOptions" DataTextField="Name" DataValueField="ProductOptionId" AppendDataBoundItems="true">
              <asp:ListItem Text="Select a Product Option" Value="" />
            </asp:DropDownList>
            <span class="input-group-btn">
              <asp:LinkButton runat="server" CssClass="btn btn-default" CommandName="AddProductOptionToProduct" CommandArgument="<%#: Item.ProductId %>" ValidationGroup='<%#: $"ProductOption{Container.DataItemIndex}" %>'><i class="glyphicon glyphicon-plus text-success"></i></asp:LinkButton>
            </span>
          </div>
          <asp:RequiredFieldValidator ID="rfvProductOptions" runat="server" ControlToValidate="ddlProductOptions" CssClass="text-danger" Display="Dynamic" ValidationGroup='<%#: $"ProductOption{Container.DataItemIndex}" %>'>Select an option before adding to the product</asp:RequiredFieldValidator>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:BoundField HeaderText="Price" DataField="UnitPrice" DataFormatString="{0:c2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="75px" />
    </Columns>
  </asp:GridView>
</asp:Content>
