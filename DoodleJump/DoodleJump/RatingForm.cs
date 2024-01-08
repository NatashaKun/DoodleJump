using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DoodleJump
{
    public partial class RatingForm : Form
    {
        public RatingForm()
        {
            InitializeComponent();
            this.Load += RatingForm_Load;
        }

        private void RatingForm_Load(object sender, EventArgs e)
        {
            string connectionString = "Host=students.ami.nstu.ru;Username=pmi-b0408;Password=Flikaic8;Database=students";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string schemaName = "pmib0408"; // Замените на имя вашей схемы
                string tableName = "doodlejump"; // Замените на имя вашей таблицы
                string sql = $"SELECT name, score FROM {schemaName}.{tableName} ORDER BY score DESC LIMIT 5";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
    }
}
