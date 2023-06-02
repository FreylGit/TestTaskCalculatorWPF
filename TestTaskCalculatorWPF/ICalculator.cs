﻿using System.Threading.Tasks;

namespace TestTaskCalculatorWPF
{
    public interface ICalculator
    {
        public void Plus(decimal number);
        public void Minus(decimal number);
        public void Div(decimal number);
        public void Multiply(decimal number);
        public void Calculation(int seconds);
    }
}
