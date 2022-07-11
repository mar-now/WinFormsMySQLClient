using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormMySQLClient
{
    public class OutputPanel : Panel
    {
        public Label HeaderLb { get; private set; }
        public Button ClearBt { get; private set; }
        public DataGridView OutputDGV { get; private set; }
        public ToolTip ToolTip { get; private set; }

        public OutputPanel(Point location)
        {
            this.Location = location;
            this.Size = new Size(655, 190);
            this.BorderStyle = BorderStyle.FixedSingle;

            ToolTip = new ToolTip();

            HeaderLb = new Label()
            {
                Size = new Size(120, 25),
                Location = new Point(10, 5),
                Font = new Font("verdana", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "OUTPUT",
            };
            this.Controls.Add(HeaderLb);

            ClearBt = new Button()
            {
                Size = new Size(25, 25),
                Location = new Point(620, 5),
                Font = new Font("vardana", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "C"
            };
            ClearBt.MouseHover += (o, e) => ToolTip.Show("Clear output", ClearBt);
            ClearBt.Click += (o, e) => { OutputDGV.Rows.Clear(); };
            this.Controls.Add(ClearBt);

            OutputDGV = new DataGridView();
            SetupDataGridView();

            Form1.connection.CommandExecuted += OnCommandExecuted;
        }

        private void OnCommandExecuted(object? sender, MySqlCommandExecutionEventArgs e)
        {
            string[] row = { e.Status.ToString(), e.ID.ToString(), e.Time.TimeOfDay.ToString(),
                e.Action, e.Message };

            OutputDGV.Rows.Add(row);
        }

        private void SetupDataGridView()
        {
            this.Controls.Add(OutputDGV);

            OutputDGV.ColumnCount = 5;

            OutputDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            OutputDGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            OutputDGV.ColumnHeadersDefaultCellStyle.Font =
                new Font(OutputDGV.Font, FontStyle.Bold);


            OutputDGV.Name = "OutputDataGridView";
            OutputDGV.Size = new Size(635, 145);
            OutputDGV.Location = new Point(10, 35);
            OutputDGV.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            OutputDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            OutputDGV.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            OutputDGV.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            OutputDGV.GridColor = Color.Black;
            OutputDGV.RowHeadersVisible = false;
            OutputDGV.Font = new Font("verdana", 8);

            OutputDGV.Columns[0].Name = " ";
            OutputDGV.Columns[0].Width = 25;
            OutputDGV.Columns[1].Name = "#";
            OutputDGV.Columns[1].Width = 25;
            OutputDGV.Columns[2].Name = "Time";
            OutputDGV.Columns[2].Width = 75;
            OutputDGV.Columns[3].Name = "Action";
            OutputDGV.Columns[3].Width = 255;
            OutputDGV.Columns[4].Name = "Message";
            OutputDGV.Columns[4].Width = 255;

            OutputDGV.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            OutputDGV.MultiSelect = false;
            this.Controls.Add(OutputDGV);
        }
    }
}
