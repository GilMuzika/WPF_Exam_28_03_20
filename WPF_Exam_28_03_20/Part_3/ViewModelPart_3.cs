using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;

namespace WPF_Exam_28_03_20
{
    class ViewModelPart_3 : ViewModelBase
    {
        private long _sizeOfStream = default(long);
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private Timer _buttonTimer = new Timer();
        private bool _canExecuteFlag = true;
        private WebDownloader _webDownloader = new WebDownloader();

        private string _url = string.Empty;
        public string Url
        {
            get => _url;
            set
            {
                try
                {
                    _webDownloader.Url = value;                 
                }
                catch(Exception ex)
                {
                    TextSizeText = $"{ex.GetType().Name}\n{ex.Message}";
                }
                _url = value;                
                OnPropertyChanged();
            }                          
        }
        private string _textSizeText = "Please wait...";
        public string TextSizeText
        {
            get => _textSizeText;
            set
            {
                _textSizeText = value;
                OnPropertyChanged();
            }
        }
        private string _timePassed = String.Empty;
        public string TimePassed
        {
            get => _timePassed;
            set
            {
                _timePassed = value;
                OnPropertyChanged();
            }
        }
        private bool _isTextAUrl;
        public bool IsTextAUrl
        {
            get => _isTextAUrl;
            set
            {
                _isTextAUrl = value;
                OnPropertyChanged();
            }
        }



        public RelayCommand Part_3ButtonCllick_RelayComm { get; set; }

        public ViewModelPart_3()
        {
            Part_3ButtonCllick_RelayComm = new RelayCommand(tacklingButtonClick, CanExecute);

            _buttonTimer.Interval = 10;
            _buttonTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                IsTextAUrl = _webDownloader.isUrl(Url); 
            };
            _buttonTimer.Start();
        }

        private async void tacklingButtonClick(object parameter)
        {
            _webDownloader = new WebDownloader();
            TextSizeText = "Please wait...";
            try
            {
                _webDownloader.Url = Url;
            }
            catch (Exception ex) 
            {
                TextSizeText = $"{ex.GetType().Name}\n{ex.Message}";
            }                
            _canExecuteFlag = false;            
            _sizeOfStream = await _webDownloader.GetContentAsStringlenghtAsync();
            if (_webDownloader.InternalException == null) TextSizeText = $"{_sizeOfStream} chars";
            else
            {
                TextSizeText = $"{_webDownloader.InternalException.GetType().Name}\n{_webDownloader.InternalException.Message}";
                _webDownloader.InternalException = null;             
            }            
        }
        private bool CanExecute(object parameter)
        {
            _timer.Interval = 1;
            int counter = 0;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                if (!_canExecuteFlag)
                {
                    TimePassed = $"Time Passed: {counter}";
                    if (_sizeOfStream != default(long))
                    {
                        _canExecuteFlag = true;                        
                        _timer.Stop();
                        counter = 0;
                    }
                }
                
                counter++;
            };
            _timer.Start();
            return _canExecuteFlag;
        }


        protected override string GetError()
        {
            //IsTextAUrl = _webDownloader.isUrl(TextSizeText);

            if (IsTextAUrl)
                return String.Empty;


            return "fuckingError";
        }





    }
}
