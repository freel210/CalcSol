using Calc.Model;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System;
using System.Text;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;

namespace Calc.ViewModel
{
    sealed class MainViewModel: INotifyPropertyChanged
    {
        private MainModel model = new MainModel();

        private delegate decimal CalculateDeligate(decimal x, decimal y);
        CalculateDeligate calcDeligate;

        public static readonly Brush ORANGE_RED_BRUSH = new SolidColorBrush(Colors.OrangeRed);
        public static readonly Brush WHITE_BRUSH = new SolidColorBrush(Colors.White);

        Dictionary<string, Action<Brush>> dirOperButtons = new Dictionary<string, Action<Brush>>();

        #region Цвета контролов
        private Brush _firstStringColor = ORANGE_RED_BRUSH;
        public Brush FirstStringColor 
        {
            get { return _firstStringColor; }
            set
            {
                _firstStringColor = value;
                OnPropertyChanged("FirstStringColor");
            }
        }

        private Brush _secondStringColor = WHITE_BRUSH;
        public Brush SecondStringColor
        {
            get { return _secondStringColor; }
            set
            {
                _secondStringColor = value;
                OnPropertyChanged("SecondStringColor");
            }
        }

        private Brush _plusOperaorButtonColor = WHITE_BRUSH;
        public Brush PlusOperatorButtonColor
        {
            get { return _plusOperaorButtonColor; }
            set
            {
                if (_plusOperaorButtonColor != value)
                {
                    _plusOperaorButtonColor = value;
                    OnPropertyChanged("PlusOperatorButtonColor");
                }
            }
        }

        private Brush _minusOperaorButtonColor = WHITE_BRUSH;
        public Brush MinusOperatorButtonColor
        {
            get { return _minusOperaorButtonColor; }
            set
            {
                if (_minusOperaorButtonColor != value)
                {
                    _minusOperaorButtonColor = value;
                    OnPropertyChanged("MinusOperatorButtonColor");
                }
            }
        }

        private Brush _divisionOperaorButtonColor = WHITE_BRUSH;
        public Brush DivisionOperatorButtonColor
        {
            get { return _divisionOperaorButtonColor; }
            set
            {
                if (_divisionOperaorButtonColor != value)
                {
                    _divisionOperaorButtonColor = value;
                    OnPropertyChanged("DivisionOperatorButtonColor");
                }
            }
        }
        
        private Brush _multiplyOperaorButtonColor = WHITE_BRUSH;
        public Brush MultiplyOperatorButtonColor
        {
            get { return _multiplyOperaorButtonColor; }
            set
            {
                if (_multiplyOperaorButtonColor != value)
                {
                    _multiplyOperaorButtonColor = value;
                    OnPropertyChanged("MultiplyOperatorButtonColor");
                }
            }
        }

        private Brush _squareOperaorButtonColor = WHITE_BRUSH;
        public Brush SquareOperatorButtonColor
        {
            get { return _squareOperaorButtonColor; }
            set
            {
                if (_squareOperaorButtonColor != value)
                {
                    _squareOperaorButtonColor = value;
                    OnPropertyChanged("SquareOperatorButtonColor");
                }
            }
        }

        private Brush _percentOperaorButtonColor = WHITE_BRUSH;
        public Brush PercentOperatorButtonColor
        {
            get { return _percentOperaorButtonColor; }
            set
            {
                if (_percentOperaorButtonColor != value)
                {
                    _percentOperaorButtonColor = value;
                    OnPropertyChanged("PercentOperatorButtonColor");
                }
            }
        }

        private Brush _button7Color = WHITE_BRUSH;
        public Brush Button7Color
        {
            get { return _button7Color; }
            set
            {
                if (_button7Color != value)
                {
                    _button7Color = value;
                    OnPropertyChanged("Button7Color");
                }
            }
        }
        
        #endregion

        #region Поля ввода/вывода
        private StringBuilder _firstString = new StringBuilder();        
        public string FirstString
        {
            get { return _firstString.ToString(); }
            
            set 
            {
                SetStringBuilderValue(_firstString, value);
                OnPropertyChanged("FirstString");

                UpdateResultString();
            }
         }
        public string FirstStringChangeValue
        {
            set
            {
                _firstString.Clear().Append(value);
                OnPropertyChanged("FirstString");

                UpdateResultString();
            }
        }
        
        private StringBuilder _secondString = new StringBuilder();       
        public string SecondString
        {
            get { return _secondString.ToString(); }

            set
            {
                SetStringBuilderValue(_secondString, value);
                OnPropertyChanged("SecondString");

                UpdateResultString();
            }
        }
        public string SecondStringChangeValue
        {
            set
            {
                _secondString.Clear().Append(value);
                OnPropertyChanged("SecondString");

                UpdateResultString();
            }
        }

        private void UpdateResultString()
        {
            decimal x, y;
            
            if (Decimal.TryParse(FirstString, out x) && Decimal.TryParse(SecondString, out y))
                try
                {
                    ResultString = calcDeligate(x, y).ToString();
                }
                catch
                {
                    ResultString = "ERROR";
                }
            else
                ResultString = "ERROR";
        }

        private void SetStringBuilderValue(StringBuilder field, string value)
        {
            if (field.ToString() == "0") field.Clear().Append(value);
            else field.Append(value);
        }

        private StringBuilder _resultString = new StringBuilder();
        public string ResultString
        {
            get { return _resultString.ToString(); }

            set
            {
                if (value != _resultString.ToString())
                {
                    _resultString.Clear().Append(value);
                    OnPropertyChanged("ResultString");
                }
            }
        }
        #endregion
        
        public MainViewModel()
        {
            FillDirectory();
          
            calcDeligate = model.addition;

            TypeCommand = new Command(arg => TypeMethod(arg));
            ClearCommand = new Command(arg => ClearMethod());
            
            SetOperatorCommand = new Command(arg => SetOperatorMethod(arg));

            SetFirstStringActiveCommand = new Command(arg => SetFirstStringActiveMethod());
            SetSecondStringActiveCommand = new Command(arg => SetSecondStringActiveMethod());

            ChangeActiveStringCommand = new Command(arg => ChangeActiveStringMethod());

            SetDefaultValues();
        }

        private void FillDirectory()
        {
            dirOperButtons.Add("plus", arg =>
            {
                PlusOperatorButtonColor = arg;

                if (arg == ORANGE_RED_BRUSH)
                {
                    calcDeligate = model.addition;
                    ChangeStringsValues();
                }
            });

            dirOperButtons.Add("minus", arg =>
            {
                MinusOperatorButtonColor = arg;
                if (arg == ORANGE_RED_BRUSH)
                {
                    calcDeligate = model.subtraction;
                    ChangeStringsValues();
                }
            });

            dirOperButtons.Add("division", arg =>
            {
                DivisionOperatorButtonColor = arg;
                if (arg == ORANGE_RED_BRUSH)
                {
                    calcDeligate = model.division;
                    ChangeStringsValues();
                }
            });

            dirOperButtons.Add("multiply", arg =>
            {
                MultiplyOperatorButtonColor = arg;
                if (arg == ORANGE_RED_BRUSH)
                {
                    calcDeligate = model.multiplicate;
                    ChangeStringsValues();
                }
            });

            dirOperButtons.Add("square", arg =>
            {
                SquareOperatorButtonColor = arg;
                if (arg == ORANGE_RED_BRUSH) calcDeligate = model.square;
            });

            dirOperButtons.Add("percent", arg =>
            {
                PercentOperatorButtonColor = arg;
                if (arg == ORANGE_RED_BRUSH) calcDeligate = model.percent;
            });
        }

        private void ChangeStringsValues()
        {
            FirstStringChangeValue = ResultString;
            SecondStringChangeValue = "0";

            SetSecondStringActiveMethod();
        }

        #region Команды
        public ICommand ClearCommand { get; set; }
        public void ClearMethod()
        {
            SetFirstStringActiveMethod();
            SetDefaultValues();
        }

        public ICommand TypeCommand { get; set; }
        public void TypeMethod(object arg)
        {
            string s = arg as string;

            if (s == "7") Button7Color = ORANGE_RED_BRUSH;
            
            if (FirstStringColor == ORANGE_RED_BRUSH)
                FirstString = s;
            else
                SecondString = s;
        }

        public ICommand SetFirstStringActiveCommand { get; set; }
        public void SetFirstStringActiveMethod()
        {
            FirstStringColor = ORANGE_RED_BRUSH;
            SecondStringColor = WHITE_BRUSH;
        }
        
        public ICommand SetSecondStringActiveCommand { get; set; }
        public void SetSecondStringActiveMethod()
        {
            FirstStringColor = WHITE_BRUSH;
            SecondStringColor = ORANGE_RED_BRUSH;
        }

        public ICommand SetOperatorCommand { get; set; }
        public void SetOperatorMethod(object arg)
        {
            string oper = arg as string;

            foreach (var item in dirOperButtons)
            {
                if (item.Key == oper)
                {
                    dirOperButtons[item.Key](ORANGE_RED_BRUSH);
                    UpdateResultString();
                }
                else dirOperButtons[item.Key](WHITE_BRUSH);
            }           
        }

        public ICommand ChangeActiveStringCommand { get; set;}
        public void ChangeActiveStringMethod()
        {
            if (FirstStringColor == ORANGE_RED_BRUSH)
            {
                FirstStringColor = WHITE_BRUSH;
                SecondStringColor = ORANGE_RED_BRUSH;
            }
            else
            {
                FirstStringColor = ORANGE_RED_BRUSH;
                SecondStringColor = WHITE_BRUSH;
            }
        }
        #endregion
        
        private void SetDefaultValues()
        {
            FirstStringChangeValue = "0";

            SecondStringChangeValue = "0";

            ResultString = "0";
        }
               
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
