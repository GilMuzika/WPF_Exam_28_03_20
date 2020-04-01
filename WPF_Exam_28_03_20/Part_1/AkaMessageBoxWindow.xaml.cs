using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Exam_28_03_20
{
    /// <summary>
    /// Interaction logic for AkaMessageBoxWindow.xaml
    /// </summary>
    public partial class AkaMessageBoxWindow : Window
    {
        private AkaMessageBoxWindow _newSameWindow;

        public string Message
        {
            get => tblMessage.Text;
            set 
            {
                if (_newSameWindow == null) this.tblMessage.Text = value;
                else _newSameWindow.tblMessage.Text = value;


            }
        }

        public AkaMessageBoxWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void  ShowMesage(string message, string title)
        {
            ShowMessageInternal(message, title);
        }
        public void ShowMesage(string message)
        {
            ShowMessageInternal(message, null);
        }
        private void ShowMessageInternal(string message, string title)
        {
            if (title != null) this.Title = title;
            if (message != null) this.tblMessage.Text = message;
            try
            {
                this.Show();
            }
            catch(InvalidOperationException)
            {
                _newSameWindow = new AkaMessageBoxWindow();
                if (title != null) _newSameWindow.Title = title;
                if (message != null) _newSameWindow.tblMessage.Text = message;
                _newSameWindow.Show();
            }
        }
    }
}
