using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestTaskCalculatorWPF
{
    public partial class MainWindow : Window
    {
        private double _currentNumber = 0.0;
        private int _fractionNumber = 0;
        private bool _isInteger = true;
        private bool _isOperation = false;
        private CalculatorBase _calculator;

        private ConcurrentQueue<Operation> _queueOperations;
        private ConcurrentQueue<double> QueueRequests;
        private ConcurrentQueue<double> QueueResults;
        public MainWindow()
        {
            InitializeComponent();
            _calculator = new CalculatorDefault
            {
                CurrentNumber = _currentNumber
            };

            _queueOperations = new ConcurrentQueue<Operation>();
            QueueRequests = new ConcurrentQueue<double>();
            QueueResults = new ConcurrentQueue<double>();
        }
        #region Numbers Button
        private void OneBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(1);
            }
            else
            {
                AppendFractionalCurrentNumber(1);
            }

        }

        private void TwoBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(2);
            }
            else
            {
                AppendFractionalCurrentNumber(2);
            }
        }

        private void ThreeBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(3);
            }
            else
            {
                AppendFractionalCurrentNumber(3);
            }

        }

        private void FourBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(4);
            }
            else
            {
                AppendFractionalCurrentNumber(4);
            }
        }

        private void FiveBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(5);
            }
            else
            {
                AppendFractionalCurrentNumber(5);
            }
        }

        private void SixBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(6);
            }
            else
            {
                AppendFractionalCurrentNumber(6);
            }
        }

        private void SevenBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(7);
            }
            else
            {
                AppendFractionalCurrentNumber(7);
            }
        }

        private void EightBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(8);
            }
            else
            {
                AppendFractionalCurrentNumber(8);
            }
        }

        private void NineBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(9);
            }
            else
            {
                AppendFractionalCurrentNumber(9);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            if (_isInteger)
            {
                AppendCurrentNumber(0);
            }
            else
            {
                AppendFractionalCurrentNumber(0);
            }
        }
        #endregion
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetCurrentNumber();
            QueueResults.Clear();
            _queueOperations.Clear();
            QueueRequests.Clear();
            UpdateLabelQueueRequestsAndQueueResults();
            _isOperation = false;
        }
        // Поведение числа с запятой
        private void CommaBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            _isInteger = false;
            CurrentNumberL.Content = _currentNumber.ToString() + ",";
        }

        // Операция вычисления 
        private async void ResultBtn_Click(object sender, RoutedEventArgs e)
        {
            // _isOperation = false;
            int seconds;
            int.TryParse(SecondsSleepTB.Text, out seconds);
            // Получаем последнее введенное число
            double currentNumber;
            double.TryParse(CurrentNumberL.Content.ToString(), out currentNumber);
            QueueRequests.Enqueue(currentNumber);

            await Task.Run(() =>
            {
                double result;
                QueueRequests.TryDequeue(out result);
                _calculator.CurrentNumber = result;
                foreach (var number in QueueRequests)
                {
                    // Задержка вычисления
                    Thread.Sleep(1000 * seconds);
                    Operation o;
                    _queueOperations.TryDequeue(out o);
                    switch (o)
                    {
                        case Operation.Div:
                            _calculator.Div(number);
                            break;
                        case Operation.Minus:
                            _calculator.Minus(number);
                            break;
                        case Operation.Multiply:
                            _calculator.Multiply(number);
                            break;
                        case Operation.Plus:
                            _calculator.Plus(number);
                            break;
                        default:
                            break;
                    }
                    result = _calculator.CurrentNumber;
                }
                QueueRequests.Clear();
                _queueOperations.Clear();


                Dispatcher.Invoke(() =>
                {
                    UpdateLabelQueueRequestsAndQueueResults();
                    ResetCurrentNumber(result);

                    QueueResults.Enqueue(result);
                });
            });
        }
        // Сброс числа
        private void ResetCurrentNumber()
        {
            _currentNumber = 0.0;
            CurrentNumberL.Content = "0";
            _isInteger = true;
            _fractionNumber = 0;
        }
        // Сброс числа если оно было дробное
        private void ResetCurrentNumber(double number)
        {
            CurrentNumberL.Content = number.ToString();
            var numberString = number.ToString().Split(",");
            if (numberString.Length > 1)
            {
                double.TryParse(numberString[0], out _currentNumber);
                int.TryParse(numberString[1], out _fractionNumber);
            }
            else
            {
                _fractionNumber = 0;
                _currentNumber = number;
                _isInteger = true;
            }
        }
        // Добавление цифры в конец числа
        private void AppendCurrentNumber(int number)
        {
            _currentNumber *= 10.0;
            _currentNumber += number;
            CurrentNumberL.Content = _currentNumber.ToString();
        }
        // Добавление цифры если число дробное
        private void AppendFractionalCurrentNumber(int number)
        {
            _fractionNumber *= 10;
            _fractionNumber += number;
            CurrentNumberL.Content = _currentNumber.ToString() + "," + _fractionNumber.ToString();
        }
        #region Operation Buttons
        private void Div_Click(object sender, RoutedEventArgs e)
        {

            if (!_isOperation)
            {
                SaveCurrentNumber();
                ResetCurrentNumber();
                _queueOperations.Enqueue(Operation.Div);
                UpdateLabelQueueRequestsAndQueueResults();
                _isOperation = true;
            }

        }

        private void MultiplyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOperation)
            {
                SaveCurrentNumber();
                ResetCurrentNumber();
                _queueOperations.Enqueue(Operation.Multiply);
                UpdateLabelQueueRequestsAndQueueResults();
                _isOperation = true;
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOperation)
            {
                SaveCurrentNumber();
                ResetCurrentNumber();
                _queueOperations.Enqueue(Operation.Plus);
                UpdateLabelQueueRequestsAndQueueResults();
                _isOperation = true;
            }
        }
        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOperation)
            {
                SaveCurrentNumber();
                ResetCurrentNumber();
                _queueOperations.Enqueue(Operation.Minus);
                UpdateLabelQueueRequestsAndQueueResults();
                _isOperation = true;
            }
        }
        private void ChangeSignBtn_Click(object sender, RoutedEventArgs e)
        {
            _currentNumber *= -1;
            CurrentNumberL.Content = _currentNumber.ToString();
        }
        #endregion
        private void SaveCurrentNumber()
        {
            double result;
            double.TryParse(CurrentNumberL.Content.ToString(), out result);
            _calculator.CurrentNumber = result;
            QueueRequests.Enqueue(result);
            UpdateLabelQueueRequestsAndQueueResults();
        }
        private void UpdateLabelQueueRequestsAndQueueResults()
        {
            QueueRequestsL.Content = "QueueRequests: " + QueueRequests.Count;
            QueueResultsL.Content = "QueueResults: " + QueueResults.Count;
        }

        private void EraseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_isInteger)
            {
                if (_currentNumber > 10)
                {
                    _currentNumber %= 10;
                }
                else
                {
                    _currentNumber = 0;
                }

                CurrentNumberL.Content = _currentNumber.ToString();
            }
            else
            {
                if (_fractionNumber > 10)
                {
                    _fractionNumber %= 10;
                    CurrentNumberL.Content = _currentNumber.ToString() + "," + _fractionNumber.ToString();
                }
                else
                {
                    _fractionNumber = 0;
                    CurrentNumberL.Content = _currentNumber.ToString();
                }
            }
        }
    }
}
