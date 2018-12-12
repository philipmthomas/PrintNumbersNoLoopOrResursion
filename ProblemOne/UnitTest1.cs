using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Tests.ProblemOne.NumberContext;

namespace Tests.ProblemOne
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UnitTest1()
        {
            var numberContext = new NumberContext(100);

            numberContext.OnNumberChange += OnNumberChange(numberContext);
            numberContext.CurrentNumber = 1;
        }

        OnNumberChangeHandler OnNumberChange(NumberContext numberContext)
        {
            return (o, args) =>
            {
                if (args.Counter > numberContext.LastNumber)
                    return;

                Console.WriteLine(numberContext.CurrentNumber);

                args.Counter += 1;
                numberContext.CurrentNumber = args.Counter;
            };
        }
    }

    public delegate void OnNumberChangeHandler(object source, OnNumberChangeEventArgs e);
    public class NumberContext
    {
        public NumberContext(int lastNumber)
        {
            LastNumber = lastNumber;
        }

        public event OnNumberChangeHandler OnNumberChange;
        private int currentNumber;
        public int CurrentNumber
        {
            get { return currentNumber; }
            set {
                currentNumber = value;
                OnNumberChange(this, new OnNumberChangeEventArgs(value));
            }
        }

        public int LastNumber { get; set; }

        public class OnNumberChangeEventArgs : EventArgs
        {
            public OnNumberChangeEventArgs(int counter)
            {
                Counter = counter;
            }

            public int Counter { get; set; }
        }
    }

}