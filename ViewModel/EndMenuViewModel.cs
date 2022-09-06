using Microsoft.VisualStudio.PlatformUI;
using System.ComponentModel;

namespace SpaceWars.ViewModel
{
    class EndMenuViewModel : INotifyPropertyChanged
    {
        private MainWindow mainWindow;
        private string message;
        private string textButton;
        private string foregroundColor;

        public EndMenuViewModel(MainWindow mainWindow, bool hasWon)
        {
            this.mainWindow = mainWindow;

            if(hasWon)
            {
                Message = "Congratulations, you beat the Galactic Empire";
                ForegroundColor = "#FFFFE81F";
                Button = "Play again";
            }
            else
            {
                Message = "You were shot down by the Galactic Empire";
                ForegroundColor = "#FF0000";
                Button = "Try again";
            }

            // Change view on click
            ButtonTryAgain_Click = new DelegateCommand(() => this.mainWindow.DataContext = new GameViewModel(this.mainWindow));

            ButtonMainMenu_Click = new DelegateCommand(() => this.mainWindow.DataContext = new MainMenuViewModel(this.mainWindow));

            // Quit application on click
            ButtonQuit_Click = new DelegateCommand(() => System.Windows.Application.Current.Shutdown());
        }

        public DelegateCommand ButtonTryAgain_Click { get; }
        public DelegateCommand ButtonMainMenu_Click { get; }
        public DelegateCommand ButtonQuit_Click { get; }

        public string ForegroundColor
        {
            get => foregroundColor;
            set
            {
                foregroundColor = value;
                OnPropertyRaised("ForegroundColor");
            }
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyRaised("Message");
            }
        }

        public string Button
        {
            get => textButton;
            set
            {
                textButton = value;
                OnPropertyRaised("Button");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call event when property change
        /// </summary>
        /// <param name="propertyname"></param>
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
