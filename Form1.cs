using System;
using System.Threading.Tasks;
using RethinkDb.Driver;


using System.Windows.Forms;


namespace Fidelity_RealTime_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private String dbName;
        private String tableName;

        private classes.Connection connection;
        public static RethinkDB R = RethinkDB.R;
        

        private  void Form1_Load(object sender, EventArgs e)
        {
            this.dbName = "test";
            this.tableName = "test";

            this.connection = new classes.Connection("localhost", 28015, "123456");
            Task task = Task.Run(async () => { await HandleUpdates(); });        
        }


        private void insertButton_Click(object sender, EventArgs e)
        {
            var objData = new dto.User { Name = nameTextBox.Text, Surname = surnameTextBox.Text, Age = Int32.Parse(ageTextBox.Text) };
            R.Db(this.dbName).Table(this.tableName).Insert(objData).Run(this.connection.getConnection());
        }

   
        private void userInfoBtn_Click(object sender, EventArgs e)
        {
           dto.User user = R.Db(this.dbName).Table(this.tableName).Get(userInfoTextBox.Text).Run<dto.User>(this.connection.getConnection());
           if(user != null)  MessageBox.Show("Name:" + user.Name + "\nSurname:" + user.Surname + "\nAge:" + user.Age );
        }



      public  async Task HandleUpdates()
      {
         var conn = this.connection.getConnection();

         var feed = await R.Db(this.dbName).Table(this.tableName)
                           .Changes().RunChangesAsync<dto.User>(conn);
         foreach (var user in feed){

                MessageBox.Show(user.NewValue.Name);

                String item = user.NewValue.Name + " " + user.NewValue.Surname + " " + user.NewValue.Age;


                updateListBox.Items.Add(item);

                
         }
     }
       
  
    }
}
