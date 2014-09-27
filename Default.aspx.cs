using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Server=f8dc8339-1138-4bb7-b208-a3b30030c557.sqlserver.sequelizer.com;Database=dbf8dc833911384bb7b208a3b30030c557;User ID=bsyqfqpidklhntbl;Password=fDReqRgJqwfe2LrPw2TniCmE7WkZCaJpa2mpoYuuKxwYGEAChT3MbYsJrmgBEpfp;");
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand("insert into udata values(@uname,@uemail)", con);
        cmd.Parameters.AddWithValue("@uname", TextBox1.Text);
        cmd.Parameters.AddWithValue("@uemail", TextBox2.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Write("Data inserted...");
        fillgrid();
        TextBox1.Text = "";
        TextBox2.Text = "";
        
    }

    private void fillgrid()
    {
        cmd = new SqlCommand("select * from udata", con);
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        cmd = new SqlCommand("update udata set uname=@uname,uemail=@uemail where uid=@uid", con);
        string s = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox3")).Text;
        string s1 = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox4")).Text;
        int i = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Value);
        cmd.Parameters.AddWithValue("@uname", s);
        cmd.Parameters.AddWithValue("@uemail", s1);
        cmd.Parameters.AddWithValue("@uid", i);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        fillgrid();
        Response.Write("Data updated");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        cmd = new SqlCommand("delete from udata where uid=@uid", con);
        int i = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Value);
        cmd.Parameters.AddWithValue("@uid", i);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Write("Data Deleted");
        fillgrid();
    }
}