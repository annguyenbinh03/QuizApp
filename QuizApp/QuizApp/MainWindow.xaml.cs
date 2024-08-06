using GalaSoft.MvvmLight.Messaging;
using QuizApp.Messages;
using QuizApp.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel();
            Messenger.Default.Register<EndOfGame>(this, CloseTheGame);
            InitializeComponent();
        }
        void CloseTheGame(EndOfGame obj)
        {
            this.Close();
        }
    }
}