namespace TestTaskCalculatorWPF
{
    enum Operation
    {
        Div,
        Minus,
        Multiply,
        Plus
    }
    public abstract class CalculatorBase : ICalculator
    {
        public double CurrentNumber { get; set; } = 0;

        public void Div(double number)
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

        public void Minus(double number)
        {
            CurrentNumber -= number;
        }

        public void Multiply(double number)
        {
            CurrentNumber *= number;
        }

        public void Plus(double number)
        {
            CurrentNumber += number;
        }
    }
}
