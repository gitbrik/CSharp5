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
using _05_Lab_CS.Model;
using _05_Lab_CS.ViewModels;

namespace _05_Lab_CS.Views
{
    /// <summary>
    /// Логика взаимодействия для InfoView.xaml
    /// </summary>
    public partial class InfoView : Window
    {
        internal InfoView(MyProcess p)
        {
            InitializeComponent();
            Title = $"{p.Name} Info";
            DataContext = new InfoViewModel(p);
        }
       
    }
}
