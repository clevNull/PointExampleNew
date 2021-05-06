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

using PointExample.Model;

namespace PointExample.View
{
    /// <summary>
    /// Логика взаимодействия для DbConnWindow.xaml
    /// </summary>
    public partial class DbConnWindow : Window
    {
        public DbConnect dbconn = new DbConnect();
        public DbConnWindow()
        {
            InitializeComponent();
        }

       


        

       
    }
}
