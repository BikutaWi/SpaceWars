using Microsoft.VisualStudio.PlatformUI;

namespace SpaceWars.ViewModel
{
    class CreditsViewModel
    {
        private MainWindow mainWindow;
        public CreditsViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            // Change view on click
            ButtonBack_Click = new DelegateCommand(() => this.mainWindow.DataContext = new MainMenuViewModel(this.mainWindow));
        }

        public DelegateCommand ButtonBack_Click { get; }
    }
}
