using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormMySQLClient
{
    public class QueryInputPanel : Panel
    {
        public Label HeaderLb;
        public Button ExecuteBt;
        public Button SaveBt;
        public TextBox InputTb;
        public ToolTip ToolTip;

        public QueryInputPanel(Point location)
        {
            this.Location = location;
            this.Size = new Size(655, 280);
            this.BorderStyle = BorderStyle.FixedSingle;

            ToolTip = new ToolTip();

            HeaderLb = new Label()
            {
                Size = new Size(120, 25),
                Location = new Point(10, 5),
                Font = new Font("verdana", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "QUARY",
            };
            this.Controls.Add(HeaderLb);

            ExecuteBt = new Button()
            {
                Size = new Size(25, 25),
                Location = new Point(585, 5),
                Font = new Font("vardana", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "\u25B6"
            };
            ExecuteBt.MouseHover += (o, e) => ToolTip.Show("Execute quary", ExecuteBt);
            ExecuteBt.Click += OnExecuteBtClick;
            this.Controls.Add(ExecuteBt);
            
            SaveBt = new Button()
            {
                Size = new Size(25, 25),
                Location = new Point(620, 5),
                Font = new Font("verdana", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "\u2193",
                
            };
            SaveBt.MouseHover += (o, e) => ToolTip.Show("Save quary to file", SaveBt);
            this.Controls.Add(SaveBt);

            InputTb = new TextBox()
            {
                Size = new Size(635, 235),
                Location = new Point(10, 35),
                Multiline = true,
            };
            this.Controls.Add(InputTb);
        }

        private void OnExecuteBtClick(object? sender, EventArgs e)
        {
            Form1.connection.ExecuteQuery(InputTb.Text);
        }
    }
}
