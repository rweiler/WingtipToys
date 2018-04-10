<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="WingtipToys.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <section>
    <div>
      <hgroup>
        <h2><%: Page.Title %></h2>
      </hgroup>

      <asp:ListView ID="productList" runat="server" DataKeyNames="ProductId" GroupItemCount="4" ItemType="WingtipToys.Models.Product" SelectMethod="GetProducts">
        <EmptyDataTemplate>
          <table>
            <tr>
              <td>No data was returned.</td>
            </tr>
          </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
          <td></td>
        </EmptyItemTemplate>
        <LayoutTemplate>
          <table style="width: 100%;">
            <tbody>
              <tr>
                <td>
                  <table id="groupPlaceholderContainer" runat="server" style="width: 100%;">
                    <tr id="groupPlaceholder"></tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td></td>
              </tr>
              <tr></tr>
            </tbody>
          </table>
        </LayoutTemplate>
        <GroupTemplate>
          <tr id="itemPlaceholderContainer" runat="server">
            <td id="itemPlaceholder" runat="server"></td>
          </tr>
        </GroupTemplate>
        <ItemTemplate>
          <td runat="server">
            <table>
              <tr>
                <td>
                  <a href="<%#: GetRouteUrl("ProductByNameRoute", new { productName = Item.Name }) %>">
                    <img src="/Catalog/Images/Thumbs/<%#: Item.ImagePath %>" width="100" height="75" style="border: solid;" />
                  </a>
                </td>
              </tr>
              <tr>
                <td>
                  <a href="<%#: GetRouteUrl("ProductByNameRoute", new { productName = Item.Name }) %>">
                    <%#: Item.Name %>
                  </a>
                  <br />
                  <span>
                    <b>Price: </b><%#: string.Format("{0:c2}", Item.UnitPrice) %>
                  </span>
                  <br />
                  <%--<a href="/AddToCart.aspx?productId=<%#: Item.ProductId %>">
                    <span class="ProductListItem"><b>Add to Cart</b></span>
                  </a>--%>
                </td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
            </table>
          </td>
        </ItemTemplate>
      </asp:ListView>
    </div>
  </section>
</asp:Content>
