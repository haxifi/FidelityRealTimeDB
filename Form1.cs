using System;
using System.Threading.Tasks;
using RethinkDb.Driver;

using System.Windows.Forms;
using System.ComponentModel;

namespace Fidelity_RealTime_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        int port = 28015;
        private string dbName = "test";
        private string tableName = "test";

        private string hostname = "localhost";
        private string password = "123456";
        
        

        private classes.Connection connection;
        public static RethinkDB R = RethinkDB.R;
        

        private  void Form1_Load(object sender, EventArgs e)
        {
            this.connection = new classes.Connection(this.hostname, this.port, this.password);
            Task task = Task.Run(async () => { await HandleUpdates(); });
        }



        private void insertButton_Click(object sender, EventArgs e)
        {
            try
            {
                var objData = new dto.User { Name = nameTextBox.Text, Surname = surnameTextBox.Text, Age = Int32.Parse(ageTextBox.Text) };
                R.Db(this.dbName).Table(this.tableName).Insert(objData).Run(this.connection.getConnection());
            }catch(FormatException)
            {
                MessageBox.Show("Format Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   
        private void userInfoBtn_Click(object sender, EventArgs e)
        {
           dto.User user = R.Db(this.dbName).Table(this.tableName).Get(userInfoTextBox.Text).Run<dto.User>(this.connection.getConnection());
           if(user != null)  MessageBox.Show("Name:" + user.Name + "\nSurname:" + user.Surname + "\nAge:" + user.Age );
        }


      public void addItem(ref ListBox list, String item)
        {
            list.Items.Add(item);
        }



    

      public  async Task HandleUpdates()
      {
          
         var conn = this.connection.getConnection();
         var feed = await R.Db(this.dbName).Table(this.tableName)
                           .Changes().RunChangesAsync<dto.User>(conn);

         foreach (var user in feed){
                String item = user.NewValue.Name + " " + user.NewValue.Surname + " " + user.NewValue.Age;
                updateListBox.BeginInvoke((Action)(() => updateListBox.Items.Add(item)));
            }
        }

    }
}
