<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="WingtipToys.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div id="ShoppingCartTitle" runat="server" class="ContentHead"><h1>Shopping Cart</h1></div>

  <asp:GridView ID="CartList" runat="server" CssClass="table table-striped table-bordered" ItemType="WingtipToys.Models.CartItem" SelectMethod="GetShoppingCartItems" AutoGenerateColumns="false" ShowFooter="true" GridLines="Vertical" CellPadding="4">
    <Columns>
      <asp:BoundField DataField="ProductId" HeaderText="Id" SortExpression="ProductId" />
      <asp:BoundField DataField="Product.Name" HeaderText="Name" />
      <asp:BoundField DataField="Product.UnitPrice" HeaderText="Price (each)" DataFormatString="{0:c2}" />
      <asp:TemplateField HeaderText="Quantity">
        <ItemTemplate>
          <asp:TextBox ID="PurchaseQuantity" runat="server" Width="40" Text="<%#: Item.Quantity %>"></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Item Total">
        <ItemTemplate>
          <%#: string.Format("{0:c2}", ((Convert.ToDouble(Item.Quantity)) * Convert.ToDouble(Item.Product.UnitPrice))) %>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Remove Item">
        <ItemTemplate>
          <asp:CheckBox ID="Remove" runat="server" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
  <div>
    <p></p>
    <strong>
      <asp:Label ID="LabelTotalText" runat="server" Text="Order Total:"></asp:Label>
      <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
    </strong>
  </div>
  <br />
  <table>
    <tr>
      <td><asp:Button ID="UpdateBtn" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="UpdateBtn_Click" /></td> 
      <td style="padding-left: 12px;">
        <asp:ImageButton ID="CheckoutImageBtn" runat="server" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" AlternateText="Check out with PayPal" Width="145" BackColor="Transparent" BorderWidth="0" OnClick="CheckoutBtn_Click" />
      </td>
    </tr>
  </table>
</asp:Content>
