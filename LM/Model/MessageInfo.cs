using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model
{
    public class MessageInfo: INotifyPropertyChanged
    {
        private string message;
        private string username;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
