using lab10OAiP.Data;
using lab10OAiP.Models;
using lab10OAiP.services;
using System;
using System.Linq;
using System.Windows;

namespace lab10OAiP
{
        public partial class RegisterWindow : Window
        {
            public RegisterWindow()
            {
                InitializeComponent();

                dpEnrollment.SelectedDate = DateTime.Now;
            }

            private void BtnRegister_Click(object sender, RoutedEventArgs e)
            {

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("Введите имя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtFirstName.Focus();
                    return;
                }

            if (!IsOnlyLetters(txtFirstName.Text))
            {
                MessageBox.Show("Имя должно содержать только буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Введите email!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtEmail.Focus();
                    return;
                }
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Введите корректный email (должен содержать @ и .)",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtEmail.Focus();
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtLastName.Text)) 
            {
                if (!IsOnlyLetters(txtLastName.Text))
                {
                    MessageBox.Show("Фамилия должна содержать только буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtLastName.Focus();
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Введите номер телефона!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPhone.Focus();
                return;
            }

            if (!IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Номер телефона должен содержать ровно 11 цифр (только цифры, без +, скобок и пробелов)!",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (txtPassword.Password.Length < 4)
                {
                    MessageBox.Show("Пароль должен быть не короче 4 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int course = 1;
                if (!int.TryParse(txtCourse.Text, out course))
                {
                    course = 1;
                }
                if (course < 1 || course > 6)
                {
                    MessageBox.Show("Курс должен быть от 1 до 6!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            if (string.IsNullOrWhiteSpace(txtFaculty.Text))
            {
                MessageBox.Show("Введите факультет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFaculty.Focus();
                return;
            }

            if (!IsOnlyLetters(txtFaculty.Text))
            {
                MessageBox.Show("Название факультета должно содержать только буквы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFaculty.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGroup.Text))
            {
                MessageBox.Show("Введите группу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtGroup.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCourse.Text))
            {
                MessageBox.Show("Введите курс (1-6)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCourse.Focus();
                return;
            }

            if (dpEnrollment.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату поступления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpEnrollment.Focus();
                return;
            }
          
            if (dpBirthDate.SelectedDate.HasValue && dpBirthDate.SelectedDate.Value > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть в будущем!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpBirthDate.Focus();
                return;
            }

            if (dpEnrollment.SelectedDate.HasValue && dpEnrollment.SelectedDate.Value > DateTime.Now)
            {
                MessageBox.Show("Дата поступления не может быть в будущем!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpEnrollment.Focus();
                return;
            }

            if (dpEnrollment.SelectedDate.HasValue && dpEnrollment.SelectedDate.Value.Year < 1990)
            {
                MessageBox.Show("Введите корректную дату поступления (после 1990 года)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpEnrollment.Focus();
                return;
            }

            if (dpBirthDate.SelectedDate.HasValue && dpEnrollment.SelectedDate.HasValue)
            {
                if (dpBirthDate.SelectedDate.Value >= dpEnrollment.SelectedDate.Value)
                {
                    MessageBox.Show("Дата рождения должна быть раньше даты поступления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    dpBirthDate.Focus();
                    return;
                }
            }


            var student = new Student
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text ?? "",
                    BirthDate = dpBirthDate.SelectedDate ?? DateTime.Now.AddYears(-18),
                    Email = txtEmail.Text,
                    PasswordHash = PasswordHasher.HashPassword(txtPassword.Password), 
                    PhoneNumber = txtPhone.Text ?? "",
                    Faculty = txtFaculty.Text ?? "",
                    Group = txtGroup.Text ?? "",
                    Course = course,
                    EnrollmentDate = dpEnrollment.SelectedDate ?? DateTime.Now
                };


                using var db = new AppDbContext();

            db.Database.EnsureCreated();

            if (db.Students.Any(s => s.Email == student.Email))
                {
                    MessageBox.Show("Пользователь с таким email уже существует!",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }

                db.Students.Add(student);
                db.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти.",
                                "Успех",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        private bool IsOnlyLetters(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (char c in text)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-')
                    return false;
            }
            return true;
        }

        private bool IsValidPhoneNumber(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false; 

            string cleaned = text.Trim();

            if (cleaned.Length != 11)
                return false;

            foreach (char c in cleaned)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

    }

}

