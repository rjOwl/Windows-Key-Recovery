using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;


//https://www.youtube.com/watch?v=44PB5GcWw5w

    // populate DataGridView 
//https://www.youtube.com/watch?v=7rtyVR46eGg
namespace Product_Key_Getter
{
    public partial class Form1 : Form
    {
        int i = 0;
        regClass regOb = new regClass();
        FireMe fireOb = new FireMe();
        ValidationsClass valOb = new ValidationsClass();
        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e){}
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e){}
        private void TextBox1_TextChanged(object sender, EventArgs e){}
        private void Button1_Click(object sender, EventArgs e)
        {
            //config.Serializer = new ServiceStackJsonSerializer(); //Register ServiceStack.Text

            String l = "DATA";
            String l2 = "DATA";

            if (fireOb.CheckForInternetConnection())
            {
                try{
                    String[] info = regOb.Initial();
                    dataGridView1.Rows.Add();//https://stackoverflow.com/questions/2397895/how-to-insert-value-into-datagridview-cell

                    dataGridView1.Rows[i].Cells[0].Value = info[0];
                    dataGridView1.Rows[i].Cells[1].Value = info[1];
                    dataGridView1.Rows[i].Cells[2].Value = info[2];
                    if(valOb.CheckRun() == valOb.CREATED)
                        fireOb.SetDataToFirebase(new string[] { info[0], info[1], info[2], info[3] });
                }catch (Exception ex){  //just for demonstration...it's always best to handle specific exceptions
                    Console.WriteLine(ex.ToString());
                }
                i++;
                if (i == 1)
                    button2.Visible = true;
            }
            else MessageBox.Show("Please connect to the Internet to show your data");
        }

        private Task<int> PushToBaseAsync((string, string, string, string) p){
            throw new NotImplementedException();
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e){}
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e){}
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e){}
        private void TextBox1_TextChanged_1(object sender, EventArgs e){}
        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e){}
        private void Form1_Load(object sender, EventArgs e){}
        private void Button2_Click(object sender, EventArgs e){}
    }
}
