﻿using LM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM
{
    class HubInfo
    {
        private ObservableCollection<MessageInfo> earlyMessages;
        private string hubLink;
        private string hubName;

        public ObservableCollection<MessageInfo> EarlyMessages
        {
            get { return earlyMessages; }
            set { earlyMessages = value; }
        }
        public string HubLink
        {
            get { return hubLink; }
            set { hubLink = value; }
        }
        public string HubName
        {
            get { return hubName; }
            set { hubName = value; }

        }
    }
}
