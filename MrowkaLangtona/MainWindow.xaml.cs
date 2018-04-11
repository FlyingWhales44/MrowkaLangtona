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

namespace MrowkaLangtona
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Engine engine;
        List<Canvas> CanvasList;

        public MainWindow()
        {
            InitializeComponent();

            engine = new Engine();

            SetPlansza(30, 30);

            Start.Click += new RoutedEventHandler(GameStep);
        }

        private void GameStep(object sender, EventArgs e)
        {
            
            ConvertCells();
            engine.game();
        }

        //private void OnClick(object sender, EventArgs e)
        //{
        //    int x = 0, y = 0;
        //    var canvas = (Canvas)sender;

        //    if (canvas.Background == Brushes.DarkBlue)
        //        canvas.Background = Brushes.Gray;
        //    else
        //        canvas.Background = Brushes.DarkBlue;

        //    GetCord(canvas.Name, out x, out y);

        //    engine.board[y][x] = !engine.board[y][x];
        //}

        private void ConvertCells()
        {
            Canvas temp,ant;
            for (int i = 0; i < engine.boardX ; i++)//zoptymalizuj wyswietlanie
                for (int j = 0; j < engine.boardY ; j++)
                {
                    temp = CanvasList.SingleOrDefault(r => r.Name == "I" + i + "I" + j);
                    if (temp != null)
                    {
                        if (!engine.board[i][j])
                            temp.Background = Brushes.DarkBlue;
                        else
                            temp.Background = Brushes.Gray;
                    }
                }
            ant = CanvasList.SingleOrDefault(r => r.Name == "I" + engine.ant.X + "I" + engine.ant.Y);
            ant.Background = Brushes.Red;
        }

        //private void DrawAnt()
        //{
        //    Canvas antImagePosition = CanvasList.SingleOrDefault(r => r.Name == "I" + engine.ant.X + "I" + engine.ant.Y);
        //    antImagePosition.Ad
        //}

        private void GetCord(string name, out int x, out int y)
        {
            string[] Cord = name.Split('I');
            y = Convert.ToInt32(Cord[1]);
            x = Convert.ToInt32(Cord[2]);
        }

        private void SetPlansza(int x, int y)
        {
            Canvas pole;
            CanvasList = new List<Canvas>();
            Board.RowDefinitions.Clear();
            Board.ColumnDefinitions.Clear();
            Board.Children.Clear();

           // antImage = Bitmap.FromFile("ant_image.jpg");

            for (int j = 0; j < x; j++)
                Board.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < y; i++)
                Board.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < y ; i++)
                for (int j = 0; j < x ; j++)
                {
                    pole = new Canvas();
                    pole.Background = Brushes.Gray;
                    pole.Name = "I" + i + "I" + j;
                    pole.SetValue(Grid.RowProperty, i);
                    pole.SetValue(Grid.ColumnProperty, j);
                   // pole.MouseDown += new MouseButtonEventHandler(OnClick);
                    CanvasList.Add(pole);

                    Board.Children.Add(pole);
                }
            engine.set(x, y);
        }
    }
}
