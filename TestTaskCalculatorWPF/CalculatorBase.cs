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
        public decimal CurrentNumber { get; set; } = 0;

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
    }
}
