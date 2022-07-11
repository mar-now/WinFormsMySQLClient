using System.Data;
using MySql.Data.MySqlClient;

namespace WinFormMySQLClient
{
    public partial class Form1 : Form
    {
        public static SchemasViewPanel schemasViewPanel;
        public static QueryInputPanel queryInputPanel;
        public static LoginPanel loginPanel;
        public static OutputPanel outputPanel;

        public static DBConnection connection;
        public Form1()
        {
            connection = new DBConnection();

            this.Size = new Size(900, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            loginPanel = new LoginPanel(new Point(10, 10));
            this.Controls.Add(loginPanel);

            schemasViewPanel = new SchemasViewPanel(new Point(10, 75));
            this.Controls.Add(schemasViewPanel);

            queryInputPanel = new QueryInputPanel(new Point(220, 75));
            this.Controls.Add(queryInputPanel);

            outputPanel = new OutputPanel(new Point(220, 360));
            this.Controls.Add(outputPanel);
        }
    }
}