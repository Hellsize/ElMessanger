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
		private HubInfo selectedChat;

        public ObservableCollection<HubInfo> Chats { get; set; }
        public ObservableCollection<MessageInfo> Messages { get; set; }

        #region PublicMethods

        public ViewModelController()
        {

			Chats = new ObservableCollection<HubInfo>
			{
				new HubInfo{HubLink = "https://localhost:7045/chat1", HubName= "chat",
					EarlyMessages = Messages},
				new HubInfo{HubLink = "https://localhost:7045/chat", HubName = "chat1",
					EarlyMessages = Messages}
			};
            Messages = new ObservableCollection<MessageInfo>();
			selectedChat = Chats[0];
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7045/chat1")
                .Build();
            ConnectToChat();

            connection.On<MessageInfo>("Receive", (messageInfo) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    var newMessage = new MessageInfo();
                    newMessage.Username = messageInfo.Username;
                    newMessage.Message = messageInfo.Message;
                    if (!Messages.Contains(newMessage))
                        Messages.Add(newMessage);

                });
            });

        }


        public string NewMessage
		{
			get { return newMessage; }
			set
			{
				newMessage = value;
				OnPropertyChanged("NewMessage");
			}
		}

		public HubInfo SelectedChat
		{
			get { return selectedChat; }
			set
			{
				selectedChat = value;
				OnPropertyChanged("SelectedChat");
				ChangeChat();
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

		private void ChangeChat()
		{
            connection = null;
            connection = new HubConnectionBuilder()
                        .WithUrl("https://localhost:7045/chat1")
                        .Build();
            ConnectToChat();
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
