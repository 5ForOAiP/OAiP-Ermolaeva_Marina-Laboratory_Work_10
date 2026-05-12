using System;
using System.Linq;
using System.Windows;
using lab10OAiP.Data;
using lab10OAiP.services;

namespace lab10OAiP
{
        public partial class VerifyCodeWindow : Window
        {
            private readonly string _email;
            private readonly string _correctCode;

            public VerifyCodeWindow(string email, string code)
            {
                InitializeComponent();
                _email = email;
                _correctCode = code;
            }

            private void BtnReset_Click(object sender, RoutedEventArgs e)
            {
                string enteredCode = txtCode.Text.Trim();
                string newPassword = txtNewPassword.Password;
                string confirmPassword = txtConfirmPassword.Password;

                if (enteredCode != _correctCode)
                {
                    MessageBox.Show("Неверный код подтверждения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Введите новый пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (newPassword.Length < 4)
                {
                    MessageBox.Show("Пароль должен быть не короче 4 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using var db = new AppDbContext();
                var student = db.Students.FirstOrDefault(s => s.Email == _email);

                if (student != null)
                {
                    student.PasswordHash = PasswordHasher.HashPassword(newPassword);
                    db.SaveChanges();

                    MessageBox.Show("Пароль успешно изменён! Теперь войдите с новым паролем.",
                                    "Успех",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);

                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                    Close();
                }
            }
        }
}
