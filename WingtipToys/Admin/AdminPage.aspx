<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="WingtipToys.Admin.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <h1>Administration</h1>
  <hr />
  <h3>Add Product</h3>
  <table>
    <tr>
      <td><asp:Label ID="LabelAddCategory" runat="server">Category:</asp:Label></td>
      <td>
        <asp:DropDownList ID="ddlAddCategory" runat="server" ItemType="WingtipToys.Models.Category" SelectMethod="GetCategories" DataTextField="CategoryName" DataValueField="CategoryId">
        </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td><asp:Label ID="LabelAddName" runat="server">Name:</asp:Label></td>
      <td>
        <asp:TextBox ID="AddProductName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAddProductName" runat="server" ControlToValidate="AddProductName" SetFocusOnError="true" Display="Dynamic" Text="* Product name required."></asp:RequiredFieldValidator>
      </td>
    </tr>
    <tr>
      <td><asp:Label ID="LabelAddDescription" runat="server">Description:</asp:Label></td>
      <td>
        <asp:TextBox ID="AddProductDescription" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAddProductDescription" runat="server" ControlToValidate="AddProductDescription" SetFocusOnError="true" Display="Dynamic" Text="* Description required."></asp:RequiredFieldValidator>
      </td>
    </tr>
    <tr>
      <td><asp:Label ID="LabelAddPrice" runat="server">Price:</asp:Label></td>
      <td>
        <asp:TextBox ID="AddProductPrice" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAddProductPrice" runat="server" ControlToValidate="AddProductPrice" SetFocusOnError="true" Display="Dynamic" Text="* Price required"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAddProductPrice" runat="server" ControlToValidate="AddProductPrice" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$" SetFocusOnError="true" Display="Dynamic" Text="* Must be a valid price without $."></asp:RegularExpressionValidator>
      </td>
    </tr>
    <tr>
      <td><asp:Label ID="LabelAddImageFile" runat="server">Image File:</asp:Label></td>
      <td>
        <asp:FileUpload ID="ProductImage" runat="server" />
        <asp:RequiredFieldValidator ID="rfvProductImage" runat="server" ControlToValidate="ProductImage" SetFocusOnError="true" Display="Dynamic" Text="* Image path required."></asp:RequiredFieldValidator>
      </td>
    </tr>
  </table>
  <p></p>
  <p></p>
  <asp:Button ID="AddProductButton" runat="server" Text="Add Product" OnClick="AddProductButton_Click" />
  <asp:Label ID="LabelAddStatus" runat="server"></asp:Label>
  <p></p>
  <h3>Remove Product:</h3>
  <table>
    <tr>
      <td><asp:Label ID="LabelRemoveProduct" runat="server">Product:</asp:Label></td>
      <td>
        <asp:DropDownList ID="ddlRemoveProduct" runat="server" ItemType="WingtipToys.Models.Product" SelectMethod="GetProducts" DataTextField="ProductName" DataValueField="ProductId" AppendDataBoundItems="true"></asp:DropDownList>
      </td>
    </tr>
  </table>
  <asp:Button ID="RemoveProductButton" runat="server" Text="Remove Product" OnClick="RemoveProductButton_Click" CausesValidation="false" />
  <asp:Label ID="LabelRemoveStatus" runat="server"></asp:Label>
</asp:Content>
