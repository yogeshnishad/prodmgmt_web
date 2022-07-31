using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page 
{
    string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename="+HttpRuntime.AppDomainAppPath+"projectmgt.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
    int selectedIndex = 0;
    int selectedId = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(DropDownList1.Items.Count<=0){
            this.loadCombobox();
        }
        
        this.refreshDataset();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int newAddedQuantity = Int32.Parse(TextBox1.Text);
        
        int oldQuantity = Int32.Parse(GridView1.Rows[DropDownList1.SelectedIndex].Cells[2].Text);
        int newQuantity = oldQuantity + newAddedQuantity;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE mstProduct set availableQuantity= @newQuantity WHERE productId = @productId";
                cmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                cmd.Parameters.AddWithValue("@productId", Int32.Parse(DropDownList1.SelectedValue));
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }

            this.refreshDataset();
        }

    }

    private void refreshDataset()
    {
        //GridView1.AutoGenerateColumns = false;

        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();
        try
        {
            //create query string(SELECT QUERY)
            String query = "SELECT productId, product, availableQuantity, basePrice, availableQuantity*basePrice as total FROM mstProduct";
            con.Open();
            //Adapter bind to query and connection object
            adapter = new SqlDataAdapter(query, con);
            //fill the dataset
            adapter.Fill(ds);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            con.Close();
        }
    }

    private void loadCombobox(){
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();
        try
        {
            //create query string(SELECT QUERY)
            String query = "SELECT productId, product, availableQuantity, basePrice, availableQuantity*basePrice as total FROM mstProduct";
            con.Open();
            //Adapter bind to query and connection object
            adapter = new SqlDataAdapter(query, con);
            //fill the dataset
            adapter.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string id = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                string productName = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                DropDownList1.Items.Add(new ListItem(productName, id));
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            con.Close();
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int selectedIndex = DropDownList1.SelectedIndex;
            TextBox1.Text = GridView1.Rows[selectedIndex].Cells[2].Text;
            this.selectedIndex = selectedIndex;
            this.selectedId = Int32.Parse(GridView1.Rows[selectedIndex].Cells[0].Text);
        }
        catch (Exception ex) { }
    }
}
