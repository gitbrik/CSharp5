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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _05_Lab_CS.Tools.Navigation;
using _05_Lab_CS.ViewModels;


namespace _05_Lab_CS.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskManagerView.xaml
    /// </summary>
    public partial class TaskManagerView : UserControl, INavigatable
    {
        public TaskManagerView()
        {
            InitializeComponent();
            DataContext = new TaskManagerViewModel();
        }
    }
}
