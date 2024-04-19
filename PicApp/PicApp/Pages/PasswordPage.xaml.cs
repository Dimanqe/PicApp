using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordPage : ContentPage
    {
        private const int PinLength = 4;
        private static int _initialPasswordLength;
        private static int _repeatablePasswordLength;
        private readonly Label _initialPasswordLabel;
        private readonly Button _numButton0;
        private readonly Button _numButton1;
        private readonly Button _numButton2;
        private readonly Button _numButton3;
        private readonly Button _numButton4;
        private readonly Button _numButton5;
        private readonly Button _numButton6;
        private readonly Button _numButton7;
        private readonly Button _numButton8;
        private readonly Button _numButton9;
        private readonly Label _repeatablePasswordLabel;
        private readonly Label _repeatPasswordMessage;
        private readonly Label _setPasswordMessage;

        private bool isAnyButtonClicked;
        private bool isPasswordMismatch;

        public PasswordPage()
        {
            InitializeComponent();
            _setPasswordMessage = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            _repeatPasswordMessage = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            _initialPasswordLabel = new Label
            {
                FontSize = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            _repeatablePasswordLabel = new Label
            {
                FontSize = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            _numButton1 = new Button { Text = "1" };
            _numButton2 = new Button { Text = "2" };
            _numButton3 = new Button { Text = "3" };
            _numButton4 = new Button { Text = "4" };
            _numButton5 = new Button { Text = "5" };
            _numButton6 = new Button { Text = "6" };
            _numButton7 = new Button { Text = "7" };
            _numButton8 = new Button { Text = "8" };
            _numButton9 = new Button { Text = "9" };
            _numButton0 = new Button { Text = "0" };


            PasswordInputGrid.Children.Add(_numButton1, 0, 0);
            PasswordInputGrid.Children.Add(_numButton2, 1, 0);
            PasswordInputGrid.Children.Add(_numButton3, 2, 0);
            PasswordInputGrid.Children.Add(_numButton4, 0, 1);
            PasswordInputGrid.Children.Add(_numButton5, 1, 1);
            PasswordInputGrid.Children.Add(_numButton6, 2, 1);
            PasswordInputGrid.Children.Add(_numButton7, 0, 2);
            PasswordInputGrid.Children.Add(_numButton8, 1, 2);
            PasswordInputGrid.Children.Add(_numButton9, 2, 2);
            PasswordInputGrid.Children.Add(_numButton0, 1, 3);
            PasswordDisplayStack.Children.Add(_setPasswordMessage);
            PasswordDisplayStack.Children.Add(_repeatPasswordMessage);
            PasswordDisplayStack.Children.Add(_initialPasswordLabel);
            PasswordDisplayStack.Children.Add(_repeatablePasswordLabel);
        }

        private ObservableCollection<int> _initialPasswordNumbers { get; } = new ObservableCollection<int>();
        private ObservableCollection<int> _repeatablePasswordNumbers { get; } = new ObservableCollection<int>();

        protected override void OnAppearing()
        {
            _initialPasswordLength = 0;
            _repeatablePasswordLength = 0;
            _initialPasswordNumbers.Clear();
            _repeatablePasswordNumbers.Clear();
            _initialPasswordLabel.Text = "";
            _repeatablePasswordLabel.Text = "";
            _numButton1.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton2.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton3.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton4.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton5.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton6.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton7.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton8.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton9.Clicked -= SetNewPasswordNumButtonClicked;
            _numButton1.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton2.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton3.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton4.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton5.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton6.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton7.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton8.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton9.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton0.Clicked -= ButtonClickedAfterInitialPassMismatch;
            _numButton1.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton2.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton3.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton4.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton5.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton6.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton7.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton8.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton9.Clicked -= EnterExistingPasswordButtonClicked;
            _numButton0.Clicked -= EnterExistingPasswordButtonClicked;

            if (Preferences.ContainsKey("password"))
            {
                _initialPasswordLength = 0;
                _initialPasswordLabel.Text = "";

                EnterExistingPassword();
            }
            else
            {
                Preferences.Set("password", "");
                SetNewPassword();
            }

            base.OnAppearing();
        }


        private void SetNewPassword()
        {
            _setPasswordMessage.Text = $"Придумайте {PinLength}-х значный пин-код";
            _numButton1.Clicked += SetNewPasswordNumButtonClicked;
            _numButton2.Clicked += SetNewPasswordNumButtonClicked;
            _numButton3.Clicked += SetNewPasswordNumButtonClicked;
            _numButton4.Clicked += SetNewPasswordNumButtonClicked;
            _numButton5.Clicked += SetNewPasswordNumButtonClicked;
            _numButton6.Clicked += SetNewPasswordNumButtonClicked;
            _numButton7.Clicked += SetNewPasswordNumButtonClicked;
            _numButton8.Clicked += SetNewPasswordNumButtonClicked;
            _numButton9.Clicked += SetNewPasswordNumButtonClicked;
            _numButton0.Clicked += SetNewPasswordNumButtonClicked;
            _numButton1.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton2.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton3.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton4.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton5.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton6.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton7.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton8.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton9.Clicked += ButtonClickedAfterInitialPassMismatch;
            _numButton0.Clicked += ButtonClickedAfterInitialPassMismatch;
        }

        public void SetNewPasswordNumButtonClicked(object sender, EventArgs e)
        {
            if (isPasswordMismatch)
                return;

            if (sender is Button button)
            {
                if (_initialPasswordLength < PinLength)
                {
                    _initialPasswordNumbers.Add(int.Parse(button.Text));
                    _initialPasswordLabel.Text = string.Join("", _initialPasswordNumbers);
                    _initialPasswordLength++;

                    if (_initialPasswordLength == PinLength) _setPasswordMessage.Text = "Повторите пин-код";
                }
                else if (_repeatablePasswordLength < PinLength)
                {
                    _repeatablePasswordNumbers.Add(int.Parse(button.Text));
                    _repeatablePasswordLabel.Text = string.Join("", _repeatablePasswordNumbers);
                    _repeatablePasswordLength++;
                }

                if (_initialPasswordLength == PinLength && _repeatablePasswordLength == PinLength)
                {
                    if (_initialPasswordLabel.Text == _repeatablePasswordLabel.Text)
                    {
                        Preferences.Set("password", _initialPasswordLabel.Text);
                        _setPasswordMessage.Text = "Пин-код установлен!";
                        Navigation.PushAsync(new GalleryPage());
                    }
                    else
                    {
                        _setPasswordMessage.Text = "Пин код не совпадает. Повторите заново";
                        _repeatablePasswordLabel.Text = string.Join("", _repeatablePasswordNumbers);

                        isPasswordMismatch = true;
                        if (isAnyButtonClicked) isAnyButtonClicked = false;
                    }
                }
            }
        }

        private void ButtonClickedAfterInitialPassMismatch(object sender, EventArgs e)
        {
            if (isPasswordMismatch)
            {
                isPasswordMismatch = false;
                _repeatablePasswordLabel.Text = string.Join("", _repeatablePasswordNumbers);
                _repeatablePasswordNumbers.Clear();
                _repeatablePasswordLength = 0;
            }

            isAnyButtonClicked = true;
        }

        private void ButtonClickedAfterExistingPassMismatch(object sender, EventArgs e)
        {
            if (isPasswordMismatch)
            {
                isPasswordMismatch = false;
                _initialPasswordLabel.Text = string.Join("", _initialPasswordNumbers);
                _initialPasswordNumbers.Clear();
                _initialPasswordLength = 0;
            }

            isAnyButtonClicked = true;
        }

        private void EnterExistingPassword()
        {
            _initialPasswordNumbers.Clear();
            _initialPasswordLabel.Text = "";
            _setPasswordMessage.Text = "Введите пин-код";

            _numButton1.Clicked += EnterExistingPasswordButtonClicked;
            _numButton2.Clicked += EnterExistingPasswordButtonClicked;
            _numButton3.Clicked += EnterExistingPasswordButtonClicked;
            _numButton4.Clicked += EnterExistingPasswordButtonClicked;
            _numButton5.Clicked += EnterExistingPasswordButtonClicked;
            _numButton6.Clicked += EnterExistingPasswordButtonClicked;
            _numButton7.Clicked += EnterExistingPasswordButtonClicked;
            _numButton8.Clicked += EnterExistingPasswordButtonClicked;
            _numButton9.Clicked += EnterExistingPasswordButtonClicked;
            _numButton0.Clicked += EnterExistingPasswordButtonClicked;
            _numButton1.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton2.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton3.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton4.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton5.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton6.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton7.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton8.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton9.Clicked += ButtonClickedAfterExistingPassMismatch;
            _numButton0.Clicked += ButtonClickedAfterExistingPassMismatch;
        }

        private void EnterExistingPasswordButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
                if (_initialPasswordLength < PinLength)
                {
                    _initialPasswordNumbers.Add(int.Parse(button.Text));
                    _initialPasswordLabel.Text = string.Join("", _initialPasswordNumbers);
                    _initialPasswordLength++;

                    if (_initialPasswordLength == PinLength)
                    {
                        if (_initialPasswordLabel.Text == Preferences.Get("password", ""))
                        {
                            Navigation.PushAsync(new GalleryPage());
                        }
                        else
                        {
                            _setPasswordMessage.Text = "Пин код неверный. Повторите ввод";
                            _initialPasswordLabel.Text = "";
                            _initialPasswordNumbers.Clear();
                            _initialPasswordLength = 0;
                        }
                    }
                }
        }
    }
}