using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Exam_28_03_20
{
    class WebDownloader
    {
        public Exception InternalException { get; set; }

        private long _contentAsStringLength = -1;
        private Stream _contentAsStream;
        private string _contentAsstring;

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                 if (isUrl(value)) _url = value;
                else
                {
                    _url = " ";
                    throw new YourTextIsntURLException();
                }
            }
        }
        public bool isUrl(string value)
        {
            Match m = r.Match(value);
            return m.Success;
        }

        private Regex r = new Regex(@"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*");


        public WebDownloader(string url)
        {
            Url = url;
        }
        public WebDownloader() { }

        
        public async Task<long> GetContentAsStringlenghtAsync()
        {
            await this.GetContentAsyncInternal();
            return _contentAsStringLength; 
        }
        public async Task<string> GetContentAsStringAsync()
        {
            await this.GetContentAsyncInternal();
            return _contentAsstring;            
        }
        public async Task<Stream> GetContentAsStreamAsync()
        {
            await this.GetContentAsyncInternal();
            return _contentAsStream;                         
        }
        private async Task GetContentAsyncInternal()
        {           
            try
            {                
                if (String.IsNullOrWhiteSpace(Url) || String.IsNullOrWhiteSpace(Url)) throw new YourTextIsntURLException();
                WebRequest webRequest = WebRequest.Create(Url);
                WebResponse webResponce = await webRequest.GetResponseAsync();
                Stream stream = webResponce.GetResponseStream();
                string text = string.Empty;
                using (StreamReader reader = new StreamReader(stream))
                { text = await reader.ReadToEndAsync(); }
                stream.Dispose();
                _contentAsStringLength = text.Length;
                _contentAsStream = stream;
                _contentAsstring = text;                
            }
            catch(Exception ex)
            {
                InternalException = ex;
            }
        }

        private void HandlingExceptions(Action act)
        {
            try
            {
                act();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\n\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

    }

    class YourTextIsntURLException : Exception
    {
        public YourTextIsntURLException() : base("Your text isn't a URL") { }
        public YourTextIsntURLException(string userMessasge) : base("Your text isn't a URL\n" + userMessasge) { }
    }
}
