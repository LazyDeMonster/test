using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 班级作业查询系统
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string sql;
        SqlConnection conn = new SqlConnection(DBHelper.ConnString);
        DataTable dt = new DataTable();
        SqlDataAdapter dap;
        //加载作业列表
        private void ZuoYe(string sql)
        {
            dt.Clear();
            dap = new SqlDataAdapter(sql, conn);
            dap.Fill(dt);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.comboBox1.Text = "====请选择科目====";
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("请输出要查询学生的学号");
            }
            else
            {
                if (this.dataGridView1.RowCount == 0)
                    MessageBox.Show("查无此人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                else
                ZuoYe("select * from homework where stuID='" + this.textBox1.Text + "'");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "")
            {
                MessageBox.Show("请输出要查询的科目");
            }
            else 
            {
                ZuoYe("select * from homework where subjectName='" + this.comboBox1.Text + "'");
            }
        }
       
        private void Form2_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.Add("====请选择科目====");
            sql = "select distinct subjectName from homework";
            DataTable dt=DBHelper.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            this.comboBox1.Items.Add(row["subjectName"]);

            ZuoYe("select *from homework");
        }
    }
}
