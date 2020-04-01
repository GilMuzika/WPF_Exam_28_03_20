﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPF_Exam_28_03_20
{
    class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        //IDataErrorInfo members and methods
        public string Error => string.Empty;
        public string this[string propertyName]
        {
            get => GetError();
        }
        protected virtual string GetError() { return string.Empty; }
    }
}

