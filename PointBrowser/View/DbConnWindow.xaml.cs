using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using PointBrowser;
using PointBrowser.Logicum;

namespace PointBrowser.View
{
    /// <summary>
    /// Логика взаимодействия для DbConnWindow.xaml
    /// </summary>
    public partial class DbConnWindow : Window
    {
        ConncetionLogic DbConn = new ConncetionLogic();
        Logic logicClass = new Logic();

        public DbConnWindow()
        {
            InitializeComponent();
        }


        private void SrvConnBtn_Click(object sender, RoutedEventArgs e)
        {
            if (addrBox.Text != "" &&
            nameDbBox.Text != "" &&
            userNameBox.Text != "" &&
            passwordBox.Password != "")
            {
                try {
                    PbChange(10);
                    DbConn.SrvConnect(addrBox.Text, portBox.Text, userNameBox.Text, passwordBox.Password);
                    DbConn.DbConnect(addrBox.Text, portBox.Text, nameDbBox.Text, userNameBox.Text, passwordBox.Password);
                    PbChange(100);
                    
                    
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

            }
            else { MessageBox.Show("Поля, необходимые для подключения заполнены не верно/ не заполнены."); }
        }

        private void PbChange(int val)
        { 
            DbConnPb.Value = val;
        }
    }
}
