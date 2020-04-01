using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    /// <summary>
    /// Interaction logic for Part_1CombinedElement.xaml
    /// </summary>
    public partial class Part_1CombinedElement : UserControl
    {
        public double SliderValue
        {
            get => sldSlider.Value;
            set => sldSlider.Value = value;
        }
        public double SliderMax
        {
            get { return SafeInvoke(() => { return sldSlider.Maximum; }); }
            set => sldSlider.Maximum = value;
        }
        public double SliderMin
        {
            get { return SafeInvoke(() => { return sldSlider.Minimum; }); }
            set => sldSlider.Minimum = value;
        }


        public double SliderValueDependency
        {
            get => (double)this.GetValue(SliderValueDependencyProperty);
            set => this.SetValue(SliderValueDependencyProperty, value);
        }
        public static readonly DependencyProperty SliderValueDependencyProperty =
        DependencyProperty.Register("SliderValueDependency", typeof(double), typeof(Part_1CombinedElement), new PropertyMetadata(0d));


        private Timer _timer = new Timer();
        public Part_1CombinedElement()
        {            
            InitializeComponent();            
            Initialise();
            
        }
        private async void Initialise()
        {

            double[] converterParameter = 
            await Task.Run(() => {   return new double[] { SliderMin, SliderMax };    });

            _timer.Interval = 10;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                

                SafeInvoke(() => 
                {                    
                    lblButtonSize.Content = new DoubleToQuartersConverter().Convert(SliderValue, typeof(string), converterParameter, new System.Globalization.CultureInfo(0x00000C0A));
                    SliderValueDependency = SliderValue;
                });
            };
            _timer.Start();           
            
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
    }
}
