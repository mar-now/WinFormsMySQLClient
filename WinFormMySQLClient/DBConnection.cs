using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WinFormMySQLClient
{
    public class DBConnection
    {
        public event EventHandler Connecting;
        public event EventHandler ConnectionOpen;
        public event EventHandler ConnectionClosed;
        public event EventHandler ConnectionChanged;
        public event EventHandler<MySqlCommandExecutionEventArgs> CommandExecuting;
        public event EventHandler<MySqlCommandExecutionEventArgs> CommandExecuted;

        public DBConnection() { }

        public DBConnection(string conncectionString) 
        {
            ConnectionString = conncectionString;
        }
        public DBConnection(string server, string databaseName, string userName, string password)
        {
            ConnectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}",
                server, databaseName, userName, password);
        }

        public string ConnectionString 
        {
            get; set;
        }
        public MySqlConnection? Connection { get; private set; }

        public bool Connect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(ConnectionString))
                    return false;
                Connection = new MySqlConnection(ConnectionString);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            if (Connection != null)
                Connection.Close();
        }

        public MySqlCommandExecutionEventArgs ExecuteCommand(string command)
        {
            MySqlCommandExecutionEventArgs eventArgs;
            string message;

            if (Connection == null || Connection.State != System.Data.ConnectionState.Open)
            {
                message = "Command not executed - client not connected to the database";
                eventArgs = new MySqlCommandExecutionEventArgs(-1, 1, DateTime.Now, command, message, null);
            }
            else 
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(command, Connection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (command.Contains("SELECT", StringComparison.OrdinalIgnoreCase))
                    {
                        Form form = new Form();
                        DataGridView dataGrid = new DataGridView();
                        dataGrid.ColumnCount = reader.FieldCount;
                        dataGrid.Dock = DockStyle.Fill;
                        dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        while(reader.Read())
                        {
                            string[] row = new string[reader.FieldCount];

                            for (int i = 0; i < reader.FieldCount; i++)
                                row[i] = reader[i].ToString();

                            dataGrid.Rows.Add(row);
                        }
                        form.Controls.Add(dataGrid);
                        form.ShowDialog();

                        message = String.Format("Command executed, {0} rows returned", dataGrid.Rows.Count);
                    }
                    else
                        message = String.Format("Command executed, {0} rows affected", reader.RecordsAffected);

                    reader.Close();
                    eventArgs = new MySqlCommandExecutionEventArgs(1, 1, DateTime.Now, command, message, reader);
                }
                catch(MySqlException e)
                {
                    message = "Command not executed: "  + e.Message;
                    eventArgs = new MySqlCommandExecutionEventArgs(-1, 1, DateTime.Now, command, message, null);
                }
                catch 
                {
                    message = "Command not executed: " ;
                    eventArgs = new MySqlCommandExecutionEventArgs(-1, 1, DateTime.Now, command, message, null); ;
                }
            }

            OnCommandExecuted(eventArgs);
            return eventArgs;
        }
        public void ExecuteQuery(string query)
        {
            string[] commands = query.Split(';');

            foreach (string command in commands)
                if (ExecuteCommand(command).Status != 1)
                    break;

        }

        #region OnEvent methods
        protected virtual void OnConnecting()
        {
            if (Connecting != null)
                Connecting(this, EventArgs.Empty);
        }

        protected virtual void OnConnectionOpen()
        {
            if (ConnectionOpen != null)
                ConnectionOpen(this, EventArgs.Empty);
        }

        protected virtual void OnConnectionClosed()
        {
            if(ConnectionClosed != null)
                ConnectionClosed(this, EventArgs.Empty);
        }

        protected virtual void OnConnectionChanged()
        {
            if(ConnectionChanged != null)
                ConnectionChanged(this, EventArgs.Empty);
        }

        protected virtual void OnCommandExecuting(MySqlCommandExecutionEventArgs e)
        {
            if (CommandExecuting != null)
                CommandExecuting(this, e);
        }

        protected virtual void OnCommandExecuted(MySqlCommandExecutionEventArgs e)
        {
            if (CommandExecuted != null)
                CommandExecuted(this, e);
        }
        #endregion

    }

    public class MySqlCommandExecutionEventArgs
    {
        public int Status { get; private set; }
        public int ID { get; private set; }
        public DateTime Time { get; private set; }
        public string Action { get; private set; }
        public string Message { get; private set; }
        public MySqlDataReader? Reader { get; private set; }
        public MySqlCommandExecutionEventArgs(
            int status, int id, DateTime time, string action, string message, MySqlDataReader? reader)
        {
            Status = status;
            ID = id;
            Time = time;
            Action = action;
            Message = message;
            Reader = reader;
        }
    }
}
