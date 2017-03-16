using Calc.View;
using Calc.ViewModel;
using System.Windows;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var mw = new MainWindow
            {
                DataContext = new MainViewModel()
            };

            mw.Show();
        }
    }
}
