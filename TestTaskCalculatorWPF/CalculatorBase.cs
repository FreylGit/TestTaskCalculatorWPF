using System.Collections.Concurrent;
using System.Threading;

namespace TestTaskCalculatorWPF
{
    public enum Operation
    {
        Div,
        Minus,
        Multiply,
        Plus
    }
    public abstract class CalculatorBase : ICalculator
    {
        public decimal CurrentNumber { get; set; } = 0;
        public ConcurrentQueue<Operation> QueueOperations { get; set; }
        public ConcurrentQueue<decimal> QueueRequests { get; set; }
        public ConcurrentQueue<decimal> QueueResults { get; set; }
        public CalculatorBase()
        {
            QueueOperations = new ConcurrentQueue<Operation>();
            QueueRequests = new ConcurrentQueue<decimal>();
            QueueResults = new ConcurrentQueue<decimal>();
        }

        public void Div(decimal number)
        {
            if (number == 0)
            {
                CurrentNumber = 0;
            }
            else
            {
                CurrentNumber /= number;
            }

        }

        public void Minus(decimal number)
        {
            CurrentNumber -= number;
        }

        public void Multiply(decimal number)
        {
            CurrentNumber *= number;
        }

        public void Plus(decimal number)
        {
            CurrentNumber += number;
        }
        public void Calculation(int seconds)
        {
            decimal result;
            QueueRequests.TryDequeue(out result);
            CurrentNumber = result;
            foreach (var number in QueueRequests)
            {
                // Задержка вычисления
                Thread.Sleep(1000 * seconds);
                Operation o;
                QueueOperations.TryDequeue(out o);
                switch (o)
                {
                    case Operation.Div:
                        Div(number);
                        break;
                    case Operation.Minus:
                        Minus(number);
                        break;
                    case Operation.Multiply:
                        Multiply(number);
                        break;
                    case Operation.Plus:
                        Plus(number);
                        break;
                    default:
                        break;
                }
                result = CurrentNumber;
            }
            QueueRequests.Clear();
            QueueOperations.Clear();
        }


    }
}
