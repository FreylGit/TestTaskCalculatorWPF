using System;
using System.Threading;
using System.Windows;

namespace TestTaskCalculatorWPF
{
    public partial class MainWindow : Window
    {
        private decimal _currentNumber = 0.0M;
        private int _fractionNumber = 0;
        private bool _isInteger = true;
        private bool _isOperation = false;
        private CalculatorBase _calculator;
        public MainWindow()
        {
            InitializeComponent();
            _calculator = new CalculatorDefault
            {
                CurrentNumber = _currentNumber
            };
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
            _calculator.QueueResults.Clear();
            _calculator.QueueOperations.Clear();
            _calculator.QueueRequests.Clear();
            UpdateLabelQueueRequestsAndQueueResults();
            _isOperation = false;
        }
        // Поведение числа с запятой
        private void CommaBtn_Click(object sender, RoutedEventArgs e)
        {
            _isOperation = false;
            _isInteger = false;
            var integerCurrentNumber = Math.Truncate(_currentNumber);
            CurrentNumberL.Content = integerCurrentNumber.ToString() + ",";
        }

        // Операция вычисления 
        private void ResultBtn_Click(object sender, RoutedEventArgs e)
        {
            int seconds;
            int.TryParse(SecondsSleepTB.Text, out seconds);

            decimal currentNumber;
            decimal.TryParse(CurrentNumberL.Content.ToString(), out currentNumber);
            _calculator.QueueRequests.Enqueue(currentNumber);

            Thread calculationThread = new Thread(() =>
            {
                _calculator.Calculation(seconds);

                Dispatcher.Invoke(() =>
                {
                    UpdateLabelQueueRequestsAndQueueResults();
                    ResetCurrentNumber(_calculator.CurrentNumber);
                    _calculator.QueueResults.Enqueue(_calculator.CurrentNumber);
                });
            });

            calculationThread.Start();
        }
        // Сброс числа
        private void ResetCurrentNumber()
        {
            _currentNumber = 0.0M;
            CurrentNumberL.Content = "0";
            _isInteger = true;
            _fractionNumber = 0;
        }
        // Сброс числа если оно было дробное
        private void ResetCurrentNumber(decimal number)
        {
            CurrentNumberL.Content = number.ToString();
            var numberString = number.ToString().Split(",");
            if (numberString.Length > 1)
            {
                decimal.TryParse(numberString[0], out _currentNumber);
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
            _currentNumber *= 10.0M;
            _currentNumber += number;
            var integerCurrentNumber = Math.Truncate(_currentNumber);
            CurrentNumberL.Content = integerCurrentNumber.ToString();
        }
        // Добавление цифры если число дробное
        private void AppendFractionalCurrentNumber(int number)
        {
            var integerCurrentNumber = Math.Truncate(_currentNumber);
            _fractionNumber *= 10;
            _fractionNumber += number;
            CurrentNumberL.Content = integerCurrentNumber + "," + _fractionNumber.ToString();
        }
        #region Operation Buttons
        private void Div_Click(object sender, RoutedEventArgs e)
        {
            if (!_isOperation)
            {
                SaveCurrentNumber();
                ResetCurrentNumber();
                _calculator.QueueOperations.Enqueue(Operation.Div);
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
                _calculator.QueueOperations.Enqueue(Operation.Multiply);
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
                _calculator.QueueOperations.Enqueue(Operation.Plus);
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
                _calculator.QueueOperations.Enqueue(Operation.Minus);
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
            decimal result;
            decimal.TryParse(CurrentNumberL.Content.ToString(), out result);
            _calculator.CurrentNumber = result;
            _calculator.QueueRequests.Enqueue(result);
            UpdateLabelQueueRequestsAndQueueResults();
        }
        private void UpdateLabelQueueRequestsAndQueueResults()
        {
            QueueRequestsL.Content = "QueueRequests: " + _calculator.QueueRequests.Count;
            QueueResultsL.Content = "QueueResults: " + _calculator.QueueResults.Count;
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
                var integerCurrentNumber = Math.Truncate(_currentNumber);
                CurrentNumberL.Content = integerCurrentNumber.ToString();
            }
            else
            {
                var stringNumber = CurrentNumberL.Content.ToString().Split(",");
                var stringFractionNumber = stringNumber[1];
                if (stringFractionNumber.Length >= 2)
                {
                    stringFractionNumber = stringFractionNumber.Remove(stringFractionNumber.Length - 1);
                    CurrentNumberL.Content = stringNumber[0] + "," + stringFractionNumber;
                    _fractionNumber %= 10;
                }
                else
                {
                    _fractionNumber = 0;
                    CurrentNumberL.Content = stringNumber[0];
                    _isInteger = true;
                }
                decimal.TryParse(CurrentNumberL.Content.ToString(), out _currentNumber);
            }
        }
    }
}
