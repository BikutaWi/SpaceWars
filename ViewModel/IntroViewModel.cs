using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace SpaceWars.ViewModel
{
    class IntroViewModel : INotifyPropertyChanged
    {
        private MainWindow mainWindow;
        private string textIntro;
        private SoundPlayer player;

        public IntroViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            string path = @"assets\text\TextIntro.txt";

            // if file exists
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);

                string value;
                string line = "";

                // read file line by line
                while ((value = file.ReadLine()) != null)
                {
                    line += $"{value}\n";
                }

                file.Close();

                TextIntro = line;
            }
            else
            {
                Console.WriteLine("Error: File doesn't exist");
            }

            BeginAnimation();
        }

        /// <summary>
        /// Begin text animation
        /// </summary>
        private async void BeginAnimation()
        {
            // play sound
            this.player = new SoundPlayer(@"assets\sounds\Star_Wars.wav");
            this.player.Load();
            this.player.Play();

            await Task.Delay(30000);

            this.player.Stop();

            // if datacontext is this intro instance
            // datacontext will change when user press a key
            if (this.mainWindow.DataContext == this)
                this.mainWindow.DataContext = new GameViewModel(this.mainWindow);
        }

        /// <summary>
        /// Stop text animation on key press
        /// </summary>
        public void StopAnimation()
        {
            if(this.player != null)
            {
                this.player.Stop();
                this.mainWindow.DataContext = new GameViewModel(this.mainWindow);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TextIntro
        {
            get => textIntro;
            set
            {
                textIntro = value;
                OnPropertyRaised("TextIntro");
            }
        }

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
