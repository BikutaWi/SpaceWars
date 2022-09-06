using Microsoft.VisualStudio.PlatformUI;

namespace SpaceWars.ViewModel
{
    public class MainMenuViewModel
    {
        private MainWindow mainWindow;

        public MainMenuViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            // Change view on click
            ButtonPlay_Click = new DelegateCommand(() => this.mainWindow.DataContext = new IntroViewModel(this.mainWindow));

            ButtonControls_Click = new DelegateCommand(() => this.mainWindow.DataContext = new ControlsViewModel(this.mainWindow));

            ButtonCredits_Click = new DelegateCommand(() => this.mainWindow.DataContext = new CreditsViewModel(this.mainWindow));

            // Quir app on click
            ButtonQuit_Click = new DelegateCommand(() => System.Windows.Application.Current.Shutdown());
        }

        public DelegateCommand ButtonPlay_Click { get; }
        public DelegateCommand ButtonControls_Click { get; }
        public DelegateCommand ButtonCredits_Click { get; }
        public DelegateCommand ButtonQuit_Click { get; }
    }
}
