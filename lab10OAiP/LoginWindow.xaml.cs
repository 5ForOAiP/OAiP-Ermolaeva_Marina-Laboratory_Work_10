using lab10OAiP.services;
using lab10OAiP.Data;
using System.Linq;
using System.Windows;


namespace lab10OAiP
{
        public partial class LoginWindow : Window
        {
            public LoginWindow()
            {
                InitializeComponent();
            }

            private void BtnLogin_Click(object sender, RoutedEventArgs e)
            {
                using var db = new AppDbContext();
                var student = db.Students.FirstOrDefault(s => s.Email == txtEmail.Text);

                if (student != null && PasswordHasher.VerifyPassword(txtPassword.Password, student.PasswordHash))
                {
                    MessageBox.Show($"Привет, {student.FirstName}! Добро пожаловать!",
                                    "Успех",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Неверный email или пароль! Попробуй еще раз.",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        private void BtnForgot_Click(object sender, RoutedEventArgs e)
        {
            var forgotWindow = new ForgotPasswordWindow();
            forgotWindow.Show();
            Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
            {
                var registerWindow = new RegisterWindow();
                registerWindow.Show();
                this.Close();
            }
        }
}

