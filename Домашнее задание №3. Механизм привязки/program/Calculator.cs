using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Program
{
    internal class Calculator : INotifyPropertyChanged
    {

        private string first_operand = "";
        private string second_operand = "";
        private string operation = "";
        private string result = "Welcome to calculator";

        bool first_comma = false;
        bool second_comma = false;
        int max_len = 10;

        public string FOperand
        {
            get
            {
                return first_operand;
            }
            set
            {
                _ = SetField(ref first_operand, value);
            }
        }

        public string SOperand
        {
            get
            {
                return second_operand;
            }
            set
            {
                _ = SetField(ref second_operand, value);
            }
        }

        public string Operation
        {
            get
            {
                return operation;
            }
            set
            {
                _ = SetField(ref operation, value);
            }
        }

        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                _ = SetField(ref result, value);
            }
        }

        private bool CheckOperation() => this.Operation != string.Empty ? true : false;
        private bool CheckFOperand() => this.FOperand != string.Empty ? true : false;
        private bool CheckSOperand()
        {
            if (CheckFOperand() && CheckOperation() && (second_operand != string.Empty))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearResult() => this.Result = "";

        private Oper GetOperation(string operation)
        {
            Oper arg = null;
            switch (operation)
            {
                case "+":
                    arg = new Plus();
                    break;
                case "!":
                    arg = new Factorial();
                    break;
                case "*":
                    arg = new Multipy();
                    break;
                case "-":
                    arg = new Minus();
                    break;
                case "/":
                    arg = new Divide();
                    break;
                case "^":
                    arg = new Pow();
                    break;
                case "mod":
                    arg = new Mod();
                    break;
                case "ceil":
                    arg = new Ceil();
                    break;
                case "floor":
                    arg = new Floor();
                    break;
                case "lg":
                    arg = new Lg();
                    break;
                case "ln":
                    arg = new Ln();
                    break;
                case "cos":
                    arg = new Cos();
                    break;
                case "sin":
                    arg = new Sin();
                    break;
                case "tan":
                    arg = new Tan();
                    break;
                default: break;
            }
            return arg;
        }

        private void AddNumber(string number)
        {
            ClearResult();

            if (Operation == string.Empty)
            {
                if (CheckFOperand())
                {
                    if (FOperand[0] == '0' && first_comma == false)
                    {
                        return;
                    }
                    else
                    {
                        FOperand += number;
                    }
                }
                else
                {
                    FOperand += number;
                }
            }
            else
            {
                if (CheckSOperand())
                {
                    if (SOperand[0] == '0' && second_comma == false)
                    {
                        return;
                    }
                    else
                    {
                        SOperand += number;
                    }
                }
                else
                {
                    SOperand += number;
                }
            }

        }

        public void NumberAdd(object parameter)
        {
            ClearResult();
            if (parameter is string text)
            {
                AddNumber(text);
            }
        }

        public void OneCommandAdd(string operation)
        {

            ClearResult();

            if (CheckFOperand() && !CheckSOperand())
            {
                Oper mathOperation = GetOperation(operation);
                double operand = Convert.ToDouble(FOperand);


                double answer = mathOperation.Conclusion(operand);
                if (double.IsNaN(answer))
                {
                    Result = "Not a number";
                    FOperand = "";
                }
                else
                {
                    FOperand = Convert.ToString(answer);

                    if (operation == "!")
                    {
                        Result = $"({operand}!) = {answer}";
                    }
                    else
                    {
                        Result = $"{operation}({operand}) = {answer}";
                    }

                }

                Operation = "";
                first_comma = false;

            }

        }

        public void TwoCommandAdd(string operation)
        {

            ClearResult();

            if (CheckOperation() && CheckSOperand())
            {
                double operandA = Convert.ToDouble(FOperand);
                double operandB = Convert.ToDouble(SOperand);
                Oper mathOperation = GetOperation(Operation);
                double answer = 0.0;


                answer = mathOperation.Conclusion(operandA, operandB);
                FOperand = Convert.ToString(answer);
                Result = $"{operandA}{Operation}{operandB} = {answer}";

                second_comma = false;

                if (FOperand.Contains(','))
                {
                    first_comma = true;
                }
            }
            else
            {
                if (CheckFOperand())
                {
                    Operation = operation;
                }
            }

        }

        public void ClearAll(object sender)
        {
            FOperand = "";
            SOperand = "";
            Operation = "";
            first_comma = false;
            second_comma = false;
            ClearResult();
        }

        public void ClearLast(object sender)
        {
            ClearResult();
            if (CheckFOperand() && !CheckSOperand())
            {
                if (CheckOperation())
                {
                    Operation = "";
                }
                else
                {
                    if (FOperand[FOperand.Length - 1] == ',')
                    {
                        first_comma = false;
                    }
                    FOperand = this.FOperand.Remove(FOperand.Length - 1);
                }
            }
            if (CheckSOperand())
            {
                if (SOperand[SOperand.Length - 1] == ',')
                {
                    second_comma = false;
                }
                SOperand = this.SOperand.Remove(SOperand.Length - 1);
            }
        }

        public void EqualAdd(object sender)
        {
            ClearResult();
            if (CheckFOperand() && CheckOperation() && CheckSOperand())
            {
                TwoCommandAdd(Operation);
            }
        }

        public void CommaAdd(object sender)
        {
            if (CheckFOperand() && !CheckSOperand())
            {
                if (first_comma)
                {
                    return;
                }
                else
                {
                    FOperand += ",";
                    first_comma = true;
                }
            }
            if (CheckSOperand())
            {
                if (second_comma)
                {
                    return;
                }
                else
                {
                    SOperand += ",";
                    second_comma = true;
                }
            }
        }

        public void SetOperation(string command)
        {
            switch (command)
            {
                case "+":
                    Operation = "+";
                    TwoCommandAdd("+");
                    break;
                case "!":
                    Operation = "!";
                    OneCommandAdd("!");
                    break;
                case "*":
                    Operation = "*";
                    TwoCommandAdd("*");
                    break;
                case "-":
                    Operation = "-";
                    TwoCommandAdd("-");
                    break;
                case "/":
                    Operation = "/";
                    TwoCommandAdd("/");
                    break;
                case "^":
                    Operation = "^";
                    TwoCommandAdd("^");
                    break;
                case "mod":
                    Operation = "mod";
                    TwoCommandAdd("mod");
                    break;
                case "ceil":
                    Operation = "ceil";
                    OneCommandAdd("ceil");
                    break;
                case "floor":
                    Operation = "floor";
                    OneCommandAdd("floor");
                    break;
                case "lg":
                    Operation = "lg";
                    OneCommandAdd("lg");
                    break;
                case "ln":
                    Operation = "ln";
                    OneCommandAdd("ln");
                    break;
                case "cos":
                    Operation = "cos";
                    OneCommandAdd("cos");
                    break;
                case "sin":
                    Operation = "si";
                    OneCommandAdd("sin");
                    break;
                case "tan":
                    Operation = "tan";
                    OneCommandAdd("tan");
                    break;
                default: break;
            }
        }
        public void CommandAdd(object sender)
        {
            if (sender is string text)
            {
                SetOperation(text);
            }
        }

        public Calculator() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}