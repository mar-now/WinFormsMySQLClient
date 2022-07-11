using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WinFormMySQLClient
{
    public class LoginPanel : Panel
    {
        public Field ServerFl { get; private set; }
        public Field DatabaseFl {get; private set; }
        public Field LoginFl {get; private set; }
        public Field PasswordFl {get; private set; }
        public Button ConnectBt {get; private set; }
        public Label StatusLb {get; private set; }

        public LoginPanel(Point location)
        {
            this.Size = new Size(865, 60);
            this.Location = location;
            this.BorderStyle = BorderStyle.FixedSingle;

            ServerFl = new Field("Server:")
            {
                Location = new Point(5, 5),
            };
            this.Controls.Add(ServerFl);

            DatabaseFl = new Field("Database:")
            {
                Location = new Point(ServerFl.Location.X + ServerFl.Width + 10, 5),
            };
            this.Controls.Add(DatabaseFl);

            LoginFl = new Field("Login:")
            {
                Location = new Point(DatabaseFl.Location.X + DatabaseFl.Width + 10, 5),
            };
            this.Controls.Add(LoginFl);

            PasswordFl = new Field("Password:")
            {
                Location = new Point(LoginFl.Location.X + LoginFl.Width + 10, 5),
            };
            PasswordFl.TextBox.PasswordChar = '*';
            this.Controls.Add(PasswordFl);

            ConnectBt = new Button()
            {
                Location = new Point(PasswordFl.Location.X + PasswordFl.Width + 20, 7),
                Size = new Size(120, 25),
                Text = "Connect",
                TextAlign = ContentAlignment.MiddleCenter,
            };
            ConnectBt.Click += OnConnectBtClick; /*(o, e) => 
            { 
                Form1.connection = new DBConnection(GenerateConnectionString()); 
                Form1.connection.Connect(); 
            };*/
            this.Controls.Add(ConnectBt);

            StatusLb = new Label()
            {
                Location = new Point(7, ServerFl.Location.Y + ServerFl.Height + 2),
                Size = new Size(800, 20),
                Font = new Font("vedana", 8),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Status: "
            };
            this.Controls.Add(StatusLb);
        }

        public string GenerateConnectionString()
        {
            return string.Format("Server={0}; database={1}; UID={2}; password={3}",
                ServerFl.TextBox.Text, DatabaseFl.TextBox.Text, LoginFl.TextBox.Text, PasswordFl.TextBox.Text);
        }

        private void OnConnectBtClick(object? sender, EventArgs e)
        {
            try
            {
                Form1.connection.ConnectionString = GenerateConnectionString();
                Form1.connection.Connect();
                StatusLb.Text = "Status: Connected";
                StatusLb.ForeColor = Color.Green;
            }
            catch(MySqlException ex)
            {
                StatusLb.ForeColor = Color.Red;
                switch (ex.Number)
                {
                    case 0:
                        StatusLb.Text = "Status: Cannot connect to server. Contact administrator. Error code: " + ex.Number;
                        break;
                    case 1045:
                        StatusLb.Text = "Status: Invalid username/password, please try again. Error code: " + ex.Number;
                        break;
                    default:
                        StatusLb.Text = "Status: Connection failed. Error code: " + ex.Number;
                        break;
                }
            }
        }
    }
}
