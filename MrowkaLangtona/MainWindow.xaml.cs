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
using System.Windows.Threading;
using System.Timers;

namespace MrowkaLangtona
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Engine engine;
        List<Button> ButtonList;
        DispatcherTimer UInterface;
        Brush frozenWhite = new SolidColorBrush(Colors.Gray);
        Brush frozenBlue = new SolidColorBrush(Colors.Blue);
        
        int AntPrevX, AntPrevY;

        public MainWindow()
        {
            InitializeComponent();

            engine = new Engine();

            SetPlansza(30, 30);

            declareTimers();

            Start.Click += new RoutedEventHandler(startGame);
        }

        private void declareTimers()
        {
            UInterface = new DispatcherTimer();
            UInterface.Interval = TimeSpan.FromSeconds(0.2);
            UInterface.Tick += new EventHandler(TickUI);
        }

        private void startGame(object sender, EventArgs e)
        {         
            UInterface.Start();
        }

        private void TickUI(object sender, EventArgs e)
        {
            engine.game();
            ConvertCells();
        }

        private void ConvertCells()
        {
            Button temp;

            for (int i = engine.ant.X - 3; i < engine.ant.Y + 3; i++)
                for (int j = engine.ant.Y- 3; j < engine.ant.Y + 3; j++)
                {
                    temp = ButtonList.SingleOrDefault(r => r.Name == "I" + i + "I" + j);

                        if (!engine.board[i][j])
                            temp.Background = frozenBlue;
                        else
                            temp.Background = frozenWhite;
                }
            ClearPrevAnt();
            DrawAnt();
        }

        private void ClearPrevAnt()
        {
            Button ant = ButtonList.SingleOrDefault(r => r.Name == "I" + AntPrevY + "I" + AntPrevX);
            ant.Content = null;
        }

        private void DrawAnt()
        {
            Button ant = ButtonList.SingleOrDefault(r => r.Name == "I" + engine.ant.Y + "I" + engine.ant.X);

            Image antImg = new Image() { Source = new BitmapImage(new Uri("antimage.jpg", UriKind.Relative)) };
            ant.Content = antImg;

            AntPrevX = engine.ant.X;
            AntPrevY = engine.ant.Y;
        }

        private void GetCord(string name, out int x, out int y)
        {
            string[] Cord = name.Split('I');
            y = Convert.ToInt32(Cord[1]);
            x = Convert.ToInt32(Cord[2]);
        }

        private void SetPlansza(int x, int y)
        {
            Button pole;
            ButtonList = new List<Button>();
            Board.RowDefinitions.Clear();
            Board.ColumnDefinitions.Clear();
            Board.Children.Clear();

            frozenBlue.Freeze();
            frozenWhite.Freeze();

            for (int j = 0; j < x; j++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < y; i++)
                Board.RowDefinitions.Add(new RowDefinition());

                    for (int i = 0; i < y ; i++)
                for (int j = 0; j < x ; j++)
                {
                    pole = new Button();
                    pole.Background = frozenWhite;
                    pole.Name = "I" + i + "I" + j;
                    pole.SetValue(Grid.RowProperty, i);
                    pole.SetValue(Grid.ColumnProperty, j);
                    ButtonList.Add(pole);
                    Board.Children.Add(pole);
                }
            engine.set(x, y);
        }
    }
}
