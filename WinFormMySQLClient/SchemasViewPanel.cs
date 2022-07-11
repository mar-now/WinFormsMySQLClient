using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormMySQLClient
{
    public class SchemasViewPanel : Panel
    {
        private Label headerLbl;
        private Button refreshBtn;
        private Label viewLbl;

        public SchemasViewPanel(Point location)
        {
            this.Size = new Size(200, 475);
            this.Location = location;
            this.BorderStyle = BorderStyle.FixedSingle;

            headerLbl = new Label()
            {
                Size = new Size(120, 25),
                Location = new Point(10, 5),
                Font = new Font("verdana", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "SCHEMAS",
            };
            this.Controls.Add(headerLbl);

            refreshBtn = new Button()
            {
                Size = new Size(25, 25),
                Location = new Point(150, 5),
                Font = new Font("Lucida Sans Unicode", 10),
                TextAlign= ContentAlignment.MiddleCenter,
                Text = "\u21BB"
            };
            refreshBtn.Click += (o, e) => DisplaySchemas();
            this.Controls.Add(refreshBtn);

            viewLbl = new Label()
            {
                Size = new Size(180, 455),
                Location = new Point(10, 40),
                Font = new Font("verdana", 8)
            };
            this.Controls.Add(viewLbl);
        }

        private void DisplaySchemas()
        {
            if (Form1.connection.Connection == null)
                return;

            string tab = "   ";
            DataTable tables = Form1.connection.Connection.GetSchema("Tables");
            viewLbl.Text = Form1.connection.Connection.Database + "\n" + tab + "Tables \n";
            for (int i = 0; i < tables.Rows.Count; i++)
            {
                var row = tables.Rows[i];
                viewLbl.Text += String.Format("{0}{1}{2}\n",tab, tab, row[2].ToString());
            }
        }
    }
}
