using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace WPF_Exam_28_03_20
{
    class ViewModelPart_1 : ViewModelBase
    {
        private Timer _timer = new Timer();
        private string _text = "I love WPF";
        public string Text
        {
            get => _text;
            set
            {
                if (String.Equals(_text, value)) return;
                _text = value;
                OnPropertyChanged();
            }
        }
        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                if (_width == value) return;
                _width = value;
                OnPropertyChanged();
            }
        }
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                if (_height == value) return;
                _height = value;
                OnPropertyChanged();
            }
        }


        private AkaMessageBoxWindow _AkaMessageBoxWindow = new AkaMessageBoxWindow();

        public ICommand MyCommand { get; set; }

        public ViewModelPart_1()
        {
            MyCommand = new RelayCommand(ExecuteCommand, CanExecuteCommand);

            _timer.Interval = 100;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                CanExecuteCommand(new object());
            };
            _timer.Start();
        }

        private void ExecuteCommand(object o)
        {
            _AkaMessageBoxWindow.ShowMesage($"Text: {Text}\n Width: {Convert.ToInt32(Width)}\n Height:{Convert.ToInt32(Height)}");            
        }
        private bool CanExecuteCommand(object o)
        {
            return !String.IsNullOrEmpty(Text);
        }
    }
}
