using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Practic2R
{
    public partial class Form1 : Form
    {
        SqlConnection dbConn;
        SqlDataAdapter daMatches, daTickets;
        DataSet ds;
        SqlCommandBuilder cbTickets;
        BindingSource bsTickets, bsMatches;

        // By clicking the update button, the changes are propagated at the level of database
        private void updateBtn_Click(object sender, EventArgs e)
        {
            daTickets.Update(ds, "Tickets");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bsMatches = new BindingSource();
            bsTickets = new BindingSource();

            // bind the data grid views to the binding sources
            dgvMatches.DataSource = bsMatches;
            dgvTickets.DataSource = bsTickets;

            // this is the connection string that we use in order to connect to our database
            dbConn = new SqlConnection("Data Source=LM_PC\\LORESQL;Initial Catalog=Practic2M;Integrated Security=True");

            // instantiate dataSet 
            ds = new DataSet();

            /// instantiate dataAdapters
            daMatches = new SqlDataAdapter("SELECT * FROM Matches", dbConn);
            daTickets = new SqlDataAdapter("SELECT * FROM Tickets", dbConn);

            // instantiate the command builder
            cbTickets = new SqlCommandBuilder(daTickets);

            // call fill method on data adapters in order to retreive all the matches and the tickets
            daMatches.Fill(ds, "Matches");
            daTickets.Fill(ds, "Tickets");

            // Matches is the parent table and Tickets is the child table
            DataRelation dr = new DataRelation("MatchesTickets",
                ds.Tables["Matches"].Columns["MatchID"],
                ds.Tables["Tickets"].Columns["MatchID"]);

            ds.Relations.Add(dr);

            bsMatches.DataSource = ds;
            bsMatches.DataMember = "Matches";

            bsTickets.DataSource = bsMatches;
            bsTickets.DataMember = "MatchesTickets";

        }
    }
}
