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
using WeatherMinus.ModelViews;

namespace WeatherMinus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        #region ListBox unfocus fix
        private void LB_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb != null && lb.HasItems) lb.SelectedIndex = 0;
        }
        #endregion
    }
}
