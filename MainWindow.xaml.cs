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
using System.Diagnostics;

namespace Advent2020
{
    public partial class MainWindow : Window
    {
        int LastDay = 13;
        public int ChoosenDay;
        private readonly MainView _mainView;
        public MainWindow()
        {
            InitializeComponent();
            _mainView = DataContext as MainView;
            InputBox.Focus();
            ChoosenDay = LastDay;
            DayBox.Text = ChoosenDay.ToString();
        }

        private void DayBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    int DayToChoose = 0;
                    DayToChoose = Int32.Parse(DayBox.Text);
                    if (DayToChoose > 0 && DayToChoose <= 25)
                        ChoosenDay = DayToChoose;
                    DayBox.Text = ChoosenDay.ToString();
                }
                catch
                {
                    DayBox.Text = ChoosenDay.ToString();
                }
            }
        }

        private async void InputBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Day d = CanIHasDay(ChoosenDay, InputBox.Text);
                d.SetMainView(_mainView);
                Tuple<string, string> OutputTuple;
                await Task.Run(() =>
                {
                    OutputTuple = d.getResult();
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    _mainView.OutText = "Del 1: " + OutputTuple.Item1 + " och del 2: " + OutputTuple.Item2 + " Executed in " + ts.TotalMilliseconds.ToString() + " ms";
                });
            }
            else
                _mainView.KeyPresses += e.Key;
        }

        private Day CanIHasDay(int _day, string _input)
        {
            Day ReturnDay;
            switch (_day)
            {
                case 1:
                    ReturnDay = new Day01(_input);
                    break;
                case 2:
                    ReturnDay = new Day02(_input);
                    break;
                case 3:
                    ReturnDay = new Day03(_input);
                    break;
                case 4:
                    ReturnDay = new Day04(_input);
                    break;
                case 5:
                    ReturnDay = new Day05(_input);
                    break;
                case 6:
                    ReturnDay = new Day06(_input);
                    break;
                case 7:
                    ReturnDay = new Day07(_input);
                    break;
                case 8:
                    ReturnDay = new Day08(_input);
                    break;
                case 9:
                    ReturnDay = new Day09(_input);
                    break;
                case 10:
                    ReturnDay = new Day10(_input);
                    break;
                case 11:
                    ReturnDay = new Day11(_input);
                    break;
                case 12:
                    ReturnDay = new Day12(_input);
                    break;
                case 13:
                    ReturnDay = new Day13(_input);
                    ReturnDay.SetMainView(_mainView);
                    break;
                case 14:
                    ReturnDay = new Day14(_input);
                    break;
                case 15:
                    ReturnDay = new Day15(_input);
                    break;
                case 16:
                    ReturnDay = new Day16(_input);
                    break;
                case 17:
                    ReturnDay = new Day17(_input);
                    break;
                case 18:
                    ReturnDay = new Day18(_input);
                    break;
                case 19:
                    ReturnDay = new Day19(_input);
                    break;
                case 20:
                    ReturnDay = new Day20(_input);
                    break;
                case 21:
                    ReturnDay = new Day21(_input);
                    break;
                case 22:
                    ReturnDay = new Day22(_input);
                    break;
                case 23:
                    ReturnDay = new Day23(_input);
                    break;
                case 24:
                    ReturnDay = new Day24(_input);
                    break;
                case 25:
                    ReturnDay = new Day25(_input);
                    break;
                default:
                    ReturnDay = new Day01(_input);
                    break;
            }
            return ReturnDay;
        }
    }
}
