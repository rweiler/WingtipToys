<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="WingtipToys.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <asp:FormView ID="productDetail" runat="server" ItemType="WingtipToys.Models.Product" SelectMethod="GetProduct" RenderOuterTable="false">
    <ItemTemplate>
      <div>
        <h1><%#: Item.Name %></h1>
      </div>
      <br />
      <table>
        <tr>
          <td>
            <img src="/Catalog/Images/<%#: Item.ImagePath %>" style="border: solid; height: 300px;" alt="<%# Item.Name %>" />
          </td>
          <td>&nbsp;</td>
          <td style="vertical-align: top; text-align: left;">
            <b>Description:</b><br /><%#: Item.Description %>
            <br />
            <span><b>Price: </b><%#: string.Format("{0:c2}", Item.UnitPrice) %></span>
            <br />
            <span><b>Product Number: </b><%#: Item.ProductId %></span>
            <br />
            <a href="/AddToCart.aspx?productId=<%#: Item.ProductId %>">Add to Cart</a>
          </td>
        </tr>
      </table>
    </ItemTemplate>
  </asp:FormView>
</asp:Content>
