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

namespace op2_d12_grotta
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Console.WriteLine("Here!");
            InitializeComponent();
            // Gör all initiering nedanför den här texten!
            Title.Text = Labyrinth.CurrentTitle();
            StoryField.Text = Labyrinth.CurrentText() + "\n\n";
            StoryField.Text += Labyrinth.Directions() + "\n";
            StoryField.Text += Labyrinth.WarningText() + "\n";
            StoryField.Text += "Skriv 'h' för hjälp,\n" +
                "      'z' för att sluta!";
            MainImage.Source = Labyrinth.GetImage();
        }
        private void ApplicationKeyPress(object sender, KeyEventArgs e)
        {
            string output = "Key pressed: ";
            output += e.Key.ToString();
            KeyPressDisplay.Text = output;
            if (e.Key == Key.Z)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else if (e.Key == Key.H)
            {
                Title.Text = Labyrinth.HelpTitle();
                StoryField.Text = Labyrinth.HelpText() + "\n";
            }
            else if (e.Key == Key.T)
            {
                Title.Text = Labyrinth.CurrentTitle();
                StoryField.Text = Labyrinth.CurrentText() + "\n\n";
                StoryField.Text += Labyrinth.Directions() + "\n";
                StoryField.Text += Labyrinth.WarningText() + "\n";
                StoryField.Text += "Skriv 'h' för hjälp,\n" +
                    "      'z' för att sluta!";
            }
            else
            {
                Labyrinth.DoCommand(e.Key.ToString().ToLower());
                Title.Text = Labyrinth.CurrentTitle();
                StoryField.Text = Labyrinth.CurrentText() + "\n\n";
                StoryField.Text += Labyrinth.Directions() + "\n";
                StoryField.Text += Labyrinth.WarningText() + "\n";
                StoryField.Text += "Skriv 'h' för hjälp,\n" +
                    "      'z' för att sluta!";
                MainImage.Source = Labyrinth.GetImage();
            }
        }
    }
}
