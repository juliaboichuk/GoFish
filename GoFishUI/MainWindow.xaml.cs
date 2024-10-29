using GoFish;
using System.Windows;
using System.Windows.Controls;

namespace GoFishUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string HUMAN_NAME = "You";
        /// <summary>
        /// The GameController to manage the game
        /// </summary>
        static GameController gameController;
        static List<string> booksInfo;

        public MainWindow()
        {
            InitializeComponent();
            booksInfo = new List<string>();
        }

        private void opponentsCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem opponentsCountSelectedItem = (ComboBoxItem)opponentsCountComboBox.SelectedItem;
            int opponetnsCount = int.Parse(opponentsCountSelectedItem.Content.ToString());
            gameController = new GameController(
                HUMAN_NAME, Enumerable.Range(1, opponetnsCount).Select(i => $"Computer #{i}"));

            SetUpNewWindow();
            opponentsCountComboBox.IsEnabled = false;

        }

        private void askCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (!gameController.GameOver)
            {
                var cardValue = playerHandListBox.SelectedValue;
                var playerIndex = opponentsListBox.SelectedIndex;
                if (cardValue == null) return;

                gameController.NextRound(
                    gameController.Opponents.ToList()[playerIndex],
                    (Values)Enum.Parse(typeof(Values), cardValue.ToString().Split(' ')[0]));
                gameProgressTextBox.Text = gameController.Status;
                playerHandListBox.ItemsSource = gameController.HumanPlayer.Hand
                    .ToList().OrderBy(card => card.Value);
                booksTextBox.Text = string.Join($"{Environment.NewLine}", BooksInfo());
                if (gameController.GameOver)
                {
                    askCardButton.Content = "Play New Game";
                }
            }
            else
            {
                gameController.NewGame();
                booksInfo.Clear();
                SetUpNewWindow();
                askCardButton.Content = "Ask for a card";
            }
        }

        private List<string> BooksInfo()
        {
            foreach (var book in gameController.HumanPlayer.Books)
            {
                var message = $"{gameController.HumanPlayer.Name} have a book of {book}";
                if (!booksInfo.Contains(message)) booksInfo.Add(message);
            }
            var playersWithBooks = gameController.Opponents.Where(o => o.Books.Count() > 0);
            foreach (var player in playersWithBooks)
            {
                foreach (var book in player.Books)
                {
                    var message = $"{player.Name} has a book of {book}";
                    if (!booksInfo.Contains(message)) booksInfo.Add(message);
                }
            }
            return booksInfo;
        }

        private void SetUpNewWindow()
        {
            playerHandListBox.ItemsSource = gameController.HumanPlayer.Hand
                .ToList().OrderBy(card => card.Value);
            playerHandListBox.SelectedIndex = 0;
            opponentsListBox.ItemsSource = gameController.Opponents;
            opponentsListBox.SelectedIndex = 0;
            gameProgressTextBox.Text = gameController.Status;
            booksTextBox.Text = string.Join($"{Environment.NewLine}", BooksInfo());
        }
    }
}