using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Semana12__Multiplataforma.ViewModels
{
    public class CalculatorDemoViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _resultText;

        public string ResultText
        {
            get { return _resultText; }
            set { SetProperty(ref _resultText, value); }
        }

        public ICommand OnSelectNumber { get; }
        public ICommand OnSelectOperator { get; }
        public ICommand OnClear { get; }
        public ICommand OnCalculate { get; }

        private bool isNewNumber;

        public CalculatorDemoViewModel()
        {
            ResultText = "0";
            isNewNumber = true;

            OnSelectNumber = new Command<string>(UpdateResultText);
            OnSelectOperator = new Command<string>(UpdateResultText);
            OnClear = new Command(ResetResultText);
            OnCalculate = new Command(CalculateResult);
        }

        private void UpdateResultText(string input)
        {
            if (isNewNumber)
            {
                ResultText = input;
                isNewNumber = false;
            }
            else
            {
                ResultText += input;
            }
        }

        private void ResetResultText()
        {
            ResultText = "0";
            isNewNumber = true;
        }

        private void CalculateResult()
        {
            // Verificar si el texto del resultado contiene un operador
            if (ResultText.Contains("+"))
            {
                string[] numbers = ResultText.Split('+');
                if (numbers.Length == 2)
                {
                    double num1, num2;
                    if (double.TryParse(numbers[0], out num1) && double.TryParse(numbers[1], out num2))
                    {
                        double result = num1 + num2;
                        ResultText = result.ToString();
                    }
                }
            }
            else if (ResultText.Contains("-"))
            {
                string[] numbers = ResultText.Split('-');
                if (numbers.Length == 2)
                {
                    double num1, num2;
                    if (double.TryParse(numbers[0], out num1) && double.TryParse(numbers[1], out num2))
                    {
                        double result = num1 - num2;
                        ResultText = result.ToString();
                    }
                }
            }
            else if (ResultText.Contains("×"))
            {
                string[] numbers = ResultText.Split('×');
                if (numbers.Length == 2)
                {
                    double num1, num2;
                    if (double.TryParse(numbers[0], out num1) && double.TryParse(numbers[1], out num2))
                    {
                        double result = num1 * num2;
                        ResultText = result.ToString();
                    }
                }
            }
            else if (ResultText.Contains("÷"))
            {
                string[] numbers = ResultText.Split('÷');
                if (numbers.Length == 2)
                {
                    double num1, num2;
                    if (double.TryParse(numbers[0], out num1) && double.TryParse(numbers[1], out num2))
                    {
                        if (num2 != 0)
                        {
                            double result = num1 / num2;
                            ResultText = result.ToString();
                        }
                        else
                        {
                            // Error: División por cero
                            ResultText = "Error";
                        }
                    }
                }
            }

            isNewNumber = true;
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
