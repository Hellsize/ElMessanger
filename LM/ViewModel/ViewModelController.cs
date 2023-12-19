using LM.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace LM
{
	class ViewModelController: INotifyPropertyChanged
    {
        HubConnection connection;
        
        private string newMessage = string.Empty;
        private string username = string.Empty;
        private RelayCommand sendCommand;
        private string isConnected = string.Empty;
        private ObservableCollection<HubInfo> hubs;


        public ObservableCollection<MessageInfo> Messages { get; set; }

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
            
			ObservableCollection<HubInfo> hubs = new ObservableCollection<HubInfo>();
            Messages = new ObservableCollection<MessageInfo>();
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7045/chat")
                .Build();
            ConnectToChat();

            connection.On<MessageInfo>("Receive", (messageInfo) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    var newMessage = new MessageInfo();
                    newMessage.Username = messageInfo.Username;
                    newMessage.Message = messageInfo.Message;
                    if(!Messages.Contains(newMessage)) 
                        Messages.Add(newMessage);

                });
            });

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
