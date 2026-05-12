using lab10OAiP.Data;
using lab10OAiP.services;
using System;
using System.Linq;
using System.Windows;

namespace lab10OAiP
{
        public partial class ForgotPasswordWindow : Window
        {
            private string _resetCode;
            private string _userEmail;

            public ForgotPasswordWindow()
            {
                InitializeComponent();
            }

            private void BtnSendCode_Click(object sender, RoutedEventArgs e)
            {
                string email = txtEmail.Text.Trim();

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Введите email!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using var db = new AppDbContext();
                var student = db.Students.FirstOrDefault(s => s.Email == email);

                if (student == null)
                {
                    MessageBox.Show("Пользователь с таким email не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _resetCode = new Random().Next(100000, 999999).ToString();
                _userEmail = email;

                try
                {
                    EmailService.SendResetCode(email, _resetCode);
                    MessageBox.Show($"Код подтверждения отправлен на {email}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    var verifyWindow = new VerifyCodeWindow(_userEmail, _resetCode);
                    verifyWindow.Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отправки письма: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void BtnBack_Click(object sender, RoutedEventArgs e)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
        }
}
