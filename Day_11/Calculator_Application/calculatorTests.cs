using NUnit.Framework;
using DemoCalcApp;
using System;

namespace CalcApp_Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Add_ReturnsCorrectSum()
        {
            Assert.That(calculator.Add(5, 2), Is.EqualTo(7));
        }

        [Test]
        public void Subtract_ReturnsCorrectDifference()
        {
            Assert.That(calculator.Subtract(5, 2), Is.EqualTo(3));
        }

        [Test]
        public void Multiply_ReturnsCorrectProduct()
        {
            Assert.That(calculator.Multiply(5, 2), Is.EqualTo(10));
        }

        [Test]
        public void Divide_ReturnsCorrectQuotient()
        {
            Assert.That(calculator.Divide(5, 2), Is.EqualTo(2.5));
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(5, 0));
        }
    }
}
