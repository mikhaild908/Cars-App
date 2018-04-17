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
using Cars.Library;
using System.Windows.Threading;

namespace Cars.View
{
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer = new DispatcherTimer();
        List<Rectangle> _cars = new List<Rectangle>();

        int _counter = 1;

        public MainWindow()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;

            AddCarsToList();
            DrawCars();
            DrawLanes();
            DrawCarNames();
            InitializeTimer();
        }

        public void AddCar(Car car, Brush brush, int x, int y)
        {
            Rectangle rectangle = CreateCar(car, brush, x, y);

            _cars.Add(rectangle);
            AddCarToCanvas(rectangle);
        }

        private void InitializeTimer()
        {
            _timer.Tick += timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (var car in _cars)
            {
                var currentCar = ((Tuple<Car, Tuple<int, int>>)car.Tag).Item1;
                var x = Canvas.GetLeft(car);
                var movement = x - currentCar.DistanceTravelled + 
                    currentCar.Move(Convert.ToInt32(0.5 * _counter));

                Canvas.SetLeft(car, movement);

                MoveWheel(currentCar.Name + "_left_rear", movement + 5);
                MoveWheel(currentCar.Name + "_left_front", movement + car.Width - 15);
                MoveWheel(currentCar.Name + "_right_rear", movement + 5);
                MoveWheel(currentCar.Name + "_right_front", movement + car.Width - 15);
            }

            _counter++;
        }

        private void MoveWheel(string wheelName, double movement)
        {
            var wheel1 = (Rectangle)canvas.FindName(wheelName);            
            Canvas.SetLeft(wheel1, movement);
        }

        private void DrawCars()
        {
            Car c1 = new Civic("Civic", 2016);
            Car c2 = new Corolla("Corolla", 2016);
            Car c3 = new ModelS("ModelS", 2016);
            Car c4 = new Civic("Civic2", 2016);            

            Rectangle civic = CreateCar(c1, Brushes.Red, 10, 10);
            Rectangle corolla = CreateCar(c2, Brushes.Silver, 10, 45);
            Rectangle modelS = CreateCar(c3, Brushes.Black, 10, 80);
            Rectangle civic2 = CreateCar(c4, Brushes.Blue, 10, 115);

            _cars.Add(civic);
            _cars.Add(corolla);
            _cars.Add(modelS);
            _cars.Add(civic2);

            foreach (var car in _cars)
            {
                AddCarToCanvas(car);
            }            
        }       

        public Rectangle CreateCar(Car car, Brush brush, int x, int y)
        {
            return new Rectangle
            {
                RadiusX = 5,
                RadiusY = 5,
                Height = 25,
                Width = 60,
                Stroke = brush,
                Fill = brush,
                Name = car.Name,
                Tag = new Tuple<Car, Tuple<int, int>>(car, new Tuple<int, int>(x,y))
            };
        }

        private void AddCarToCanvas(Rectangle rectangle)
        {
            var carName = (((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item1).Name;
            
            // left rear wheel
            AddWheel(((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item1 + 5, ((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item2 - 1, carName + "_left_rear");

            // right rear wheel
            AddWheel(((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item1 + 5, ((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item2 + (int)rectangle.Height - 6, carName + "_right_rear");

            // left front wheel
            AddWheel(((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item1 + (int)rectangle.Width - 15, ((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item2 - 1, carName + "_left_front");

            // right front wheel
            AddWheel(((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item1 + (int)rectangle.Width - 15, ((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item2 + (int)rectangle.Height - 6, carName + "_right_front");

            // main body
            canvas.Children.Add(rectangle);

            // main body
            Canvas.SetTop(rectangle, ((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item2);
            Canvas.SetLeft(rectangle, ((Tuple<Car, Tuple<int, int>>)rectangle.Tag).Item2.Item1);
        }

        private void AddWheel(int x, int y, string wheelName)
        {
            Rectangle wheel = new Rectangle { Height = 7, Width = 10, Stroke = Brushes.Black, Fill = Brushes.Black, Name = wheelName };
            canvas.Children.Add(wheel);
            Canvas.SetTop(wheel, y);
            Canvas.SetLeft(wheel, x);
            canvas.RegisterName(wheelName, wheel);
        }

        private void AddCarsToList()
        {
            carsList.DataContext = new List<Car> { new Civic("My Civic 1", 2015),
                                               new Corolla("My Corolla 1", 2015),
                                               new ModelS("My Model S 1", 2015),
                                               new Civic("My Civic 2", 2016),
                                               new Corolla("My Corolla 2", 2016),
                                               new ModelS("My Model S 2", 2016),
                                               new Civic("My Civic 3", 2017),
                                               new Corolla("My Corolla 3", 2017),
                                               new ModelS("My Model S 3", 2017),
                                               new Civic("My Civic 4", 2017),
                                               new Corolla("My Corolla 4", 2017),
                                               new ModelS("My Model S 4", 2017)
            };
        }

        private void DrawLanes()
        {
            foreach (var car in _cars)
            {
                var top = Canvas.GetTop(car);

                Line leftLine = new Line();
                leftLine.Stroke = Brushes.Black;
                leftLine.X1 = 0;
                leftLine.X2 = SystemParameters.PrimaryScreenWidth;
                leftLine.Y1 = top - 4;
                leftLine.Y2 = top - 4;

                canvas.Children.Add(leftLine);
            }
        }

        private void DrawCarNames()
        {
            foreach (var car in _cars)
            {
                var x = Canvas.GetLeft(car);
                var y = Canvas.GetTop(car);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = car.Name;
                              
                Canvas.SetLeft(textBlock, x + car.Width + 25);
                Canvas.SetTop(textBlock, y + car.Height / 2 - 5);

                canvas.Children.Add(textBlock);
            }
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
                cmdStart.Content = "Stop";
            }
            else
            {
                _timer.Stop();
                cmdStart.Content = "Start";
            }            
        }
    }
}