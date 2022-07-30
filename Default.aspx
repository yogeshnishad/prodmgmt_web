<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:40px">
      <table cellspacing="30px">
      <tr>
      <th>Product</th>
      <th>Quantity</th>
      </tr>
      <tr>
      <td>
          <asp:DropDownList ID="DropDownList1" runat="server" 
              DataSourceID="SqlDataSource1" DataTextField="Product" 
              DataValueField="ProductId" 
              onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
              AutoPostBack="True">
             
          </asp:DropDownList>
          <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
              ConnectionString="<%$ ConnectionStrings:projectmgtConnectionString %>" 
              SelectCommand="SELECT [Product], [ProductId] FROM [mstProduct]">
          </asp:SqlDataSource>
      </td>
      <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
    ControlToValidate="TextBox1" runat="server"
    ErrorMessage="Only Numbers allowed"
    ValidationExpression="\d+">
</asp:RegularExpressionValidator></td>
      <td>
        <asp:Button ID="Button1" runat="server" Text="Submit" ForeColor="#CC3300" 
              onclick="Button1_Click" />
              
        </td>
      </tr>
      </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Width="859px">
        <Columns>  
                    <asp:BoundField HeaderText="ID" DataField="productId" />  
                    <asp:BoundField HeaderText="Product" DataField="product"/>
                    <asp:BoundField HeaderText="Available Qty" DataField="availableQuantity" />  
                    <asp:BoundField HeaderText="Base Price" DataField="basePrice" />  
                    <asp:BoundField HeaderText="Total" DataField="total" />  
                </Columns>  
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:projectmgtConnectionString %>" 
        SelectCommand="SELECT productId, product, availableQuantity, basePrice, availableQuantity*basePrice as total FROM mstProduct">
    </asp:SqlDataSource>
    </form>
</body>
</html>
