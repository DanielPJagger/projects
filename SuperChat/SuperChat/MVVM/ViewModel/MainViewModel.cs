using SuperChat.Core;
using SuperChat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SuperChat.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        /* Commands */

        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set 
            { _selectedContact = value;
                OnPropertyChanged();
            }
        }


        private string _message;

        public string Message
        {
            get { return _message; }
            set { 
                _message = value;
                OnPropertyChanged();
            }
           

        }



        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                });

                Message = "";
            });

            Messages.Add(new MessageModel
            { 
                Username = "XnModnX",
                UsernameColour = "#409aff",
                ImageSource = "https://wallpapers.com/images/hd/funny-profile-picture-1l2l3tmmbobjqd53.jpg",
                Message = "Test",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "XnModnX",
                    UsernameColour = "#409aff",
                    ImageSource = "https://wallpapers.com/images/hd/funny-profile-picture-1l2l3tmmbobjqd53.jpg",
                    Message = "Test",
                    Time = DateTime.Now,
                    IsNativeOrigin = false,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Chaotic43",
                    UsernameColour = "#409aff",
                    ImageSource = "https://wallpapers.com/images/hd/funny-profile-picture-1l2l3tmmbobjqd53.jpg",
                    Message = "Test",
                    Time = DateTime.Now,
                    IsNativeOrigin = true,
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "Chaotic43",
                UsernameColour = "#409aff",
                ImageSource = "https://wallpapers.com/images/hd/funny-profile-picture-1l2l3tmmbobjqd53.jpg",
                Message = "Last",
                Time = DateTime.Now,
                IsNativeOrigin = true,
            });

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                   Username = $"XnModnX {i}",
                   ImageSource = "https://i.pinimg.com/736x/93/45/89/934589f3aa2f266b260de8bfeb3ae1ab.jpg",
                   Messages = Messages
                });
            }
        }
    }
}
