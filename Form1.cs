using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTester
{
    public partial class frmTester : Form
    {
        OleDbConnection conn;

        public frmTester()
        {
            InitializeComponent();
        }

        private void frmTester_Load(object sender, EventArgs e)
        {
            var connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\DB\Books.accdb; Persist Security Info = False;";
            conn = new OleDbConnection(connString);
            conn.Open();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            OleDbCommand command = null;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable table = new DataTable();

            try
            {
                command = new OleDbCommand(txtCommand.Text, conn);
                adapter.SelectCommand = command;
                adapter.Fill(table);

                grdRecord.DataSource = table;
                lblCount.Text = table.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error In SQL Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            command.Dispose();
            adapter.Dispose();
            table.Dispose();
        }

        private void frmFormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
            conn.Dispose();
        }
    }
}
