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
using System.Windows.Threading;

namespace WPF_Exam_28_03_20
{
    /// <summary>
    /// Interaction logic for Part_3.xaml
    /// </summary>
    public partial class Part_3 : UserControl
    {
        private DispatcherTimer _dispTimer = new DispatcherTimer();
        private Timer _timer = new Timer();

        public string Title
        {
            get => tblTitle.Text;
            set
            {
                tblTitle.Text = value;
            }
        }
        public Part_3()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            _dispTimer.Interval = TimeSpan.FromSeconds(10);
            _dispTimer.Tick += (object sender, EventArgs e) => 
            {
                CommandManager.InvalidateRequerySuggested();
            };
            _dispTimer.Start();

            _timer.Interval = 10;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                SafeInvoke(() => 
                {                   
                    btnUrlText.IsEnabled = IsButtonEnabledDependency;                    
                });                
            };
            _timer.Start();

        }


        public bool IsButtonEnabledDependency
        {
            get => (bool)this.GetValue(IsButtonEnabledDependencyProperty);
            set => this.SetValue(IsButtonEnabledDependencyProperty, value);
        }
        public static readonly DependencyProperty IsButtonEnabledDependencyProperty =
        DependencyProperty.Register("IsButtonEnabledDependency", typeof(bool), typeof(Part_3), new PropertyMetadata(true));        


        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess())
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }



    }
}
