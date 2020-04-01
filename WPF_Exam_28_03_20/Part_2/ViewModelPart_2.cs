using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Exam_28_03_20
{
    class ViewModelPart_2 : ViewModelBase
    {
        private AkaMessageBoxWindow _akaMessageBoxWindow = new AkaMessageBoxWindow();

        private double _correctResult;
        public double CorrectResult
        {
            get => _correctResult;
            set
            {
                _correctResult = value;
                OnPropertyChanged();
            }
        }
        private ErrorState _errorState;
        public ErrorState ErrorState
        {
            get => _errorState;
            set
            {
                _errorState = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand Part_2ButtonCllick_RelayComm { get; set; }

        public ViewModelPart_2()
        {
            Part_2ButtonCllick_RelayComm = new RelayCommand(tacklingButtonClick, CanExecute);
        }

        private void tacklingButtonClick(object parameter)
        {
            if ((double)(parameter as Button).Tag == _correctResult)
            {
                _akaMessageBoxWindow.ShowMesage("You're right, the result is:\n\n" + _correctResult.ToString(), "Congrats, you're right sir:");
                //MessageBox.Show(_correctResult.ToString());
                ErrorState = ErrorState.Correct;
            }
            else
            {
                ErrorState = ErrorState.ErrorFromViewModel;
                _akaMessageBoxWindow.ShowMesage("You're WRONG");
            }
            
          
        }
        private bool CanExecute(object parameter)
        {
            if (ErrorState != ErrorState.NoError) return false;

            return true;
        }



        protected override string GetError()
        {
            if (ErrorState != ErrorState.Error && ErrorState != ErrorState.ErrorFromViewModel)
                return String.Empty;


            return "fuckingError";
        }
    }
}
