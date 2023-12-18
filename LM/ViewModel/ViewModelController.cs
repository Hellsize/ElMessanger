using LM.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LM
{
	class ViewModelController: INotifyPropertyChanged
    {
        HubConnection connection;
        
        private string newMessage = string.Empty;
        private string username = string.Empty;
        private RelayCommand sendCommand;
        private string isConnected = string.Empty;

        private ObservableCollection<MessageInfo> Messages { get; set; }

        #region PublicMethods
        public string NewMessage
        {
            get { return newMessage; }
            set
            {
                newMessage = value;
                OnPropertyChanged("NewMessage");
            }
        }
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        public ViewModelController()
		{
			
            connection = new HubConnectionBuilder()
                .WithUrl("https://0.0.0.0:443/chat")
                .Build();
            Messages = new ObservableCollection<MessageInfo>();
        }

        public void ConnectToChat()
		{
            try
            {
                ConnectToHub();
                isConnected = "Вы вошли в чат";
            }
            catch (Exception ex)
            {
                isConnected = "Нет подключения";
            }
        }

       

        public RelayCommand SendCommand
        {
            get
            {
                return sendCommand ??
                  (sendCommand = new RelayCommand(obj =>
                  {
                      MessageInfo message = new MessageInfo();
                      message.Username = username;
                      message.Message = newMessage;
                      SendMessage(message);
                      Messages.Add(message);
                  }));
            }
        }
        #endregion

        #region AsyncMethods
        private async void ConnectToHub()
        {
            await connection.StartAsync();
        }

        private async void SendMessage(MessageInfo message_info)
        {
            await connection.InvokeAsync("Send", message_info);
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
