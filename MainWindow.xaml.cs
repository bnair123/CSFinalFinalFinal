using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RealWorldIntFinal
{
    public partial class MainWindow : Window
    {
        Controller controller = new Controller();
        User user;
        public MainWindow()
        {
            InitializeComponent();
            loadSymbols();
        }

        public async void loadSymbols()
        {
            await controller.getStockDetails(Constants.ApiKey);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUserName.Text != "" && txtPassword.Password != "")
                {
                    try
                    {
                        user = controller.handleUserLogin(txtUserName.Text, txtPassword.Password);
                        this.Visibility = Visibility.Hidden;
                        StockView stockView = new StockView(user);
                        stockView.Show();
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("Wrong username or Password");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid username and password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.handleUserCreation(txtUserName.Text, txtPassword.Password);
                MessageBox.Show("Registration successfull!, Please login", "Good boy", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear the username and password after registration
                txtPassword.Clear();
                txtUserName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed. Please try again " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
