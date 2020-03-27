using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class TYBH : Form
    {
        public TYBH()
        {
            InitializeComponent();
            
        }

        //连接
        SQLiteConnection connection = new SQLiteConnection(@"datasource=D:\石家庄\print_zhuzhuangtu\hhht.db");

        private void TYBH_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DataTable table = Excute();
            if (table.Rows.Count>=1)
            {
                WindowsFormsApp4.Form1 form1 = new Form1(this.textBox1.Text);
                form1.Show();
            }

            else
            {
                MessageBox.Show("您输入的统一编号不存在");
            }

            connection.Close();
            
        }
       
        /// <summary>
        ///  search TYBH in database(MSJB_ZJ_DCYX)
        /// </summary>
        /// <returns></returns>
        public DataTable Excute()
        {
            string TYBH = textBox1.Text;
            string ExcuteSql = "SELECT `TYBH` FROM  `MSJB_ZJ_DCYX` WHERE `TYBH`=@TYBH";
            SQLiteCommand command = new SQLiteCommand(ExcuteSql, connection);
            command.Parameters.AddWithValue("@TYBH", TYBH);

            SQLiteDataReader reader = command.ExecuteReader();

            int num;
            DataTable table = new DataTable();
            for (num = 0; num < reader.FieldCount; num++)
            {
                DataColumn column = new DataColumn();
                column.DataType = reader.GetFieldType(num);
                column.ColumnName = reader.GetName(num);
                table.Columns.Add(column);
            }
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (num = 0; num < reader.FieldCount; num++)
                {
                    row[num] = reader[num].ToString();
                }
                table.Rows.Add(row);
                row = null;
            }
            reader.Close();
            return table;
        }

        }
}
