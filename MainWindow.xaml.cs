using SpaceWars.ViewModel;
using System.Windows;
using System.Windows.Input;


namespace SpaceWars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainMenuViewModel(this);
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(DataContext is GameViewModel)
            {
                GameViewModel game = DataContext as GameViewModel;
                game.PressKey(sender, e);
            }

            if(DataContext is IntroViewModel)
            {
                IntroViewModel intro = DataContext as IntroViewModel;
                intro.StopAnimation();
            }
        }

        private void OnKeyUpHandler(object sender, KeyEventArgs e)
        {
            if (DataContext is GameViewModel)
            {
                GameViewModel game = DataContext as GameViewModel;
                game.ReleaseKey(sender, e);
            }
        }
    }
}
