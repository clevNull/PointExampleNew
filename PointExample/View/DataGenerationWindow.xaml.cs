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

using PointExample.Logicum;
using System.Threading;

namespace PointExample.View
{
    /// <summary>
    /// Логика взаимодействия для DataGenerationWindow.xaml
    /// </summary>
    public partial class DataGenerationWindow : Window
    {
        DbConnWindow dbConnWindow;
        ConncetionLogic DbConn = new ConncetionLogic();
        Logic logicClass = new Logic();


        public DataGenerationWindow()
        {
            InitializeComponent();

            logicClass.Notify += this.ProcessStatusOut;
        }

        private void DbConnBtn_Click(object sender, RoutedEventArgs e)
        {
            dbConnWindow = new DbConnWindow();
            dbConnWindow.Show();
        }

        private void CountSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OrderBlock.Text = Convert.ToInt32(CountSldr.Value).ToString();
        }

        private void CustomerSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CustomerBlock.Text = Convert.ToInt32(CustomerSldr.Value).ToString();
        }

        private void DataGenBtn_Click(object sender, RoutedEventArgs e)
        {
            //Thread genThread = new Thread(() => generate());
            //genThread.IsBackground = true;
            //genThread.Start();
            generate();
        }

        public void generate()
        {
            try
            {
                logicClass.FillCustomerTables(Convert.ToInt32(CustomerBlock.Text));
                logicClass.FillOrderTables(Convert.ToInt32(OrderBlock.Text), Convert.ToInt32(CustomerBlock.Text));
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public void ProcessStatusOut( string message )
        {
            //this.Dispatcher.Invoke( new Action ( () => showLog( message ) ) );
            showLog(message);
        }

        public void showLog( string message )
        {
            lbProcessStatus.Content = message;
        }
    }
}
