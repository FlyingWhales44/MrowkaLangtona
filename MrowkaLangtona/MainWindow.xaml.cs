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

        Image antImg;
        int AntPrevX, AntPrevY;
        bool EngineTicking;

        public MainWindow()
        {
            InitializeComponent();

            engine = new Engine();

            EngineTicking = true;

            SetPlansza(20, 20);

            declareTimers();

            AntPositionN.Click += new RoutedEventHandler(antDirN);
            AntPositionS.Click += new RoutedEventHandler(antDirS);
            AntPositionW.Click += new RoutedEventHandler(antDirW);
            AntPositionE.Click += new RoutedEventHandler(antDirE);
            Save.Click += new RoutedEventHandler(boardset);
            Start.Click += new RoutedEventHandler(startGame);
        }

        private void declareTimers()
        {
            UInterface = new DispatcherTimer();
            UInterface.Interval = TimeSpan.FromSeconds(0.2);
            UInterface.Tick += new EventHandler(TickUI);
        }

        private void boardset(object sender, EventArgs e)
        {
            SetPlansza(Convert.ToInt32(TextBoardX.Text), (Convert.ToInt32(TextBoardY.Text)));
        }

        private void antDirN(object sender, EventArgs e)
        {
            engine.antSet(engine.ant.X, engine.ant.Y, 'N');

            ClearPrevAnt();
            DrawAnt();
        }

        private void antDirS(object sender, EventArgs e)
        {
            engine.antSet(engine.ant.X, engine.ant.Y, 'S');

            ClearPrevAnt();
            DrawAnt();
        }

        private void antDirE(object sender, EventArgs e)
        {
            engine.antSet(engine.ant.X, engine.ant.Y, 'E');

            ClearPrevAnt();
            DrawAnt();
        }

        private void antDirW(object sender, EventArgs e)
        {
            engine.antSet(engine.ant.X, engine.ant.Y, 'W');

            ClearPrevAnt();
            DrawAnt();
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
            Button temp = ButtonList.SingleOrDefault(r => r.Name == "I" + engine.ant.Y + "I" + engine.ant.X);

            if (!engine.board[engine.ant.Y][engine.ant.X])
                temp.Background = frozenBlue;
            else
                temp.Background = frozenWhite;
          
            temp = ButtonList.SingleOrDefault(r => r.Name == "I" + AntPrevY + "I" + AntPrevX);

            if (!engine.board[AntPrevY][AntPrevX])
                temp.Background = frozenBlue;
            else
                temp.Background = frozenWhite;

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
            string source="";

            switch (engine.ant.Direction)
            {
                case 'N':
                    source = "ant.jpg";
                    break;
                case 'E':
                    source = "antE.jpg";
                    break;
                case 'S':
                    source = "antS.jpg";
                    break;
                case 'W':
                    source = "antW.jpg";
                    break;
            }

            antImg = new Image() { Source = new BitmapImage(new Uri(source, UriKind.Relative)) };

            ant.Content = antImg;

            AntPrevX = engine.ant.X;
            AntPrevY = engine.ant.Y;
        }

        private void antPositionClick(object sender, EventArgs e)
        {
           if(EngineTicking)
            {
                var button = (Button)sender;

                string[] Cord = button.Name.Split('I');

                engine.antSet(Convert.ToInt32(Cord[2]), Convert.ToInt32(Cord[1]), 'N');

                ClearPrevAnt();
                DrawAnt();     
            }
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
                    pole.SetValue(Grid.RowProperty, j);
                    pole.SetValue(Grid.ColumnProperty, i);
                    pole.Click += new RoutedEventHandler(antPositionClick);
                    ButtonList.Add(pole);
                    Board.Children.Add(pole);
                }
            engine.set(x, y);
        }
    }
}
