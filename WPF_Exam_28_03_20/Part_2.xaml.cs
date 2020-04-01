using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Exam_28_03_20
{
    public enum ErrorState
    {
        NoError,         
        ErrorFromViewModel,
        Correct,
        Error
    }

    /// <summary>
    /// Interaction logic for Part_2.xaml
    /// </summary>
    public partial class Part_2 : UserControl
    {
        private Timer _timer = new Timer();
        private int _timerCount;
        private Timer _dependenciesvalueBindingTimer = new Timer();
        private ExpressionGenerator _expressionGenerator = new ExpressionGenerator();
        private List<double> _resultsList = new List<double>();

        private double _firstExpressionMember;
        private double _secondExpressionMember;
        private string _expressionSymbol;
        private double _correctResult;
        private double _fakeResult1;
        private double _fakeResult2;
        private double _fakeResult3;

        private ErrorState _errorState = ErrorState.NoError;



        public double CorrectResultDependency
        {
            get => (double)this.GetValue(CorrectResultDependencyProperty);            
            set => this.SetValue(CorrectResultDependencyProperty, value);
        }
        public static readonly DependencyProperty CorrectResultDependencyProperty =
        DependencyProperty.Register("CorrectResultDependency", typeof(double), typeof(Part_2), new PropertyMetadata(0d));

        public ErrorState ErrorStateDependency
        {
            get => (ErrorState)this.GetValue(ErrorStateDependencyProperty);
            set { SafeInvoke(() => { this.SetValue(ErrorStateDependencyProperty, value); });   }
        }
        public static readonly DependencyProperty ErrorStateDependencyProperty =
        DependencyProperty.Register("ErrorStateDependency", typeof(ErrorState), typeof(Part_2), new PropertyMetadata(ErrorState.Correct));

        public Brush BackgroundDependency
        {
            get => (Brush)this.GetValue(BackgroundDependencyProperty);
            set => this.SetValue(BackgroundDependencyProperty, value);
        }
        public static readonly DependencyProperty BackgroundDependencyProperty =
        DependencyProperty.Register("BackgroundDependency", typeof(Brush), typeof(Part_2), new PropertyMetadata(Brushes.LightGreen));


        public string Title
        {
            get => tblTitle.Text;
            set
            {
                tblTitle.Text = value;
            }
        }
        public Part_2()
        {
            InitializeComponent();
            Initialize();
            
        }
        private void Initialize()
        {


            Populate(); //first time populating
            ErrorStateDependency = _errorState;


            _dependenciesvalueBindingTimer.Interval = 10;
            _dependenciesvalueBindingTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                SafeInvoke(() => 
                {                 
                    CorrectResultDependency = _correctResult;                    
                    if (ErrorStateDependency == ErrorState.ErrorFromViewModel || ErrorStateDependency == ErrorState.Correct)
                    {
                        _errorState = ErrorStateDependency;
                        _timerCount = 0;
                    }
                    if (_errorState != ErrorState.NoError)
                    {
                        _timer.Stop();
                        Background = BackgroundDependency; ///====>>
                    }
                });                
            };
            _dependenciesvalueBindingTimer.Start();            

             _timerCount = 32; //intended interval + 2
            _timer.Interval = 1000;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                if (_timerCount == 1)
                {
                    _timer.Stop();
                    _errorState = ErrorState.Error;
                    ErrorStateDependency = _errorState;
                }
                if (_timerCount == 0)
                {
                    this.SafeInvoke(() => 
                    {
                        this.Populate();                        
                    });
                    _timerCount = 31; //intended interval + 1
                    
                }
                this.SafeInvoke(() => 
                {
                    lblTimeCount.Content = $"Time Left: {_timerCount}";
                    if (_timerCount > 15) lblTimeCount.Foreground = Brushes.Black;
                    if (_timerCount <= 15) lblTimeCount.Foreground = Brushes.LightBlue;
                });

                _timerCount--;
            };
            _timer.Start();
        }

        private void Populate()
        {
            _resultsList.Clear();

            _expressionGenerator.Generate(out _firstExpressionMember, out _secondExpressionMember, out _expressionSymbol, out _correctResult, out _fakeResult1, out _fakeResult2, out _fakeResult3);

            lblExpression.Content = $"{_firstExpressionMember} {_expressionSymbol} {_secondExpressionMember} = ?";
            _resultsList.Add(_correctResult);            
            _resultsList.Add(_fakeResult1);
            _resultsList.Add(_fakeResult2);
            _resultsList.Add(_fakeResult3);

            _resultsList = Statics.ShuffleList(_resultsList);

            btn1.Content = _resultsList[0];
            btn1.Tag = _resultsList[0];
            btn2.Content = _resultsList[1];
            btn2.Tag = _resultsList[1];
            btn3.Content = _resultsList[2];
            btn3.Tag = _resultsList[2];
            btn4.Content = _resultsList[3];
            btn4.Tag = _resultsList[3];

        }


        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess())
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }
        private T SafeInvoke<T>(Func<T> work)
        {
            if (Dispatcher.CheckAccess())
            {
                return work.Invoke();
            }
            return this.Dispatcher.Invoke(work);
        }

        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            _errorState = ErrorState.NoError;
            ErrorStateDependency = _errorState;
            Populate();
            _timer.Start();
        }

    }
}
