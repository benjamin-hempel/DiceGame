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

using DiceGame.Models;

namespace DiceGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game Game { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Game = new Game();
            PopulateGameInfo();
        }

        private void PopulateGameInfo()
        {
            foreach (Player player in Game.Players)
            {
                PlayersList.Items.Add($"{player.Name} reist {player.Result} km");
            }

            MainLabel.Content = $"{Game.Players[0].Name} ist dran!";
        }

        private void Toss(object sender, RoutedEventArgs e)
        {
            NextTossNoteLabel.Foreground = Brushes.Black;

            int selected = 0;

            if (OnesRadio.IsChecked == true) selected = 1;
            else if (TensRadio.IsChecked == true) selected = 10;
            else if (HundredsRadio.IsChecked == true) selected = 100;
            else NextTossNoteLabel.Foreground = Brushes.Red;

            Game.Toss(selected);
            UpdateAfterToss();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            Game.ResetGame();

            NextTossNoteLabel.Visibility = Visibility.Visible;
            OnesRadio.Visibility = Visibility.Visible;
            TensRadio.Visibility = Visibility.Visible;
            HundredsRadio.Visibility = Visibility.Visible;
            TossButton.Visibility = Visibility.Visible;
            NewGameButton.Visibility = Visibility.Hidden;

            PlayersList.Items.Clear();
            PopulateGameInfo();
        }

        private void UpdateAfterToss()
        {
            Player player = Game.GetCurrentPlayer();
            PlayersList.Items[Game.CurrentPlayer] = $"{player.Name} reist {player.Result} km";

            OnesRadio.IsChecked = false;
            TensRadio.IsChecked = false;
            HundredsRadio.IsChecked = false;

            if (player.Selected[1]) 
                OnesRadio.IsEnabled = false;
            if (player.Selected[10])
                TensRadio.IsEnabled = false;
            if (player.Selected[100]) 
                HundredsRadio.IsEnabled = false;

            if (player.HasSelectedEverything())
                NextPlayer();     
        }

        private void NextPlayer()
        {
            OnesRadio.IsEnabled = true;
            TensRadio.IsEnabled = true;
            HundredsRadio.IsEnabled = true;

            bool gameContinues = Game.NextPlayer();

            if (gameContinues)
                MainLabel.Content = $"{Game.GetCurrentPlayer().Name} ist dran!";
            else
                FinishGame();
        }

        private void FinishGame()
        {
            Player winner = Game.GetWinner();
            MainLabel.Content = $"{winner.Name} hat gewonnen!";

            NextTossNoteLabel.Visibility = Visibility.Hidden;
            OnesRadio.Visibility = Visibility.Hidden;
            TensRadio.Visibility = Visibility.Hidden;
            HundredsRadio.Visibility = Visibility.Hidden;
            TossButton.Visibility = Visibility.Hidden;
            NewGameButton.Visibility = Visibility.Visible;
        }
    }
}
