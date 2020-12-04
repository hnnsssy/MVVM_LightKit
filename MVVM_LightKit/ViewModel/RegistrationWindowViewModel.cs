using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_LightKit.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.ViewModel
{
    class RegistrationWindowViewModel : ViewModelBase
    {    

        /*string _textBoxLogin_Text = "Логин";
        public string TextBoxLogin_Text
        {
            get
            {
                return _textBoxLogin_Text;
            }
            set
            {
                _textBoxLogin_Text = value;
                RaisePropertyChanged("TextBoxLogin_Text");
            }
        }

        //OPEN REG WINDOW COMMAND
        RelayCommand _textBox_Login_GotFocus;
        public RelayCommand TextBox_Login_GotFocus
        {
            get
            {
                if (_textBox_Login_GotFocus == null)
                    _textBox_Login_GotFocus = new RelayCommand(textBox_Login_GotFocus, true);
                return _textBox_Login_GotFocus;
            }
        }

        private void textBox_Login_GotFocus()
        {
            if (_textBoxLogin_Text == "Логин")
                _textBoxLogin_Text = "";
        }
        ////////////////////////////////////////////////////////



        RelayCommand _textBox_Login_LostFocus;
        public RelayCommand TextBox_Login_LostFocus
        {
            get
            {
                if (_textBox_Login_LostFocus == null)
                    _textBox_Login_LostFocus = new RelayCommand(textBox_Login_LostFocus, true);
                return _textBox_Login_GotFocus;
            }
        }

        private void textBox_Login_LostFocus()
        {
            if (_textBoxLogin_Text == "")
                _textBoxLogin_Text = "Логин";
        }*/
    }
}
