using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }
        private void CalculateChecksumButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilePathTextBox.Text))
            {
                MessageBox.Show("Выберите файл.");
                return;
            }
            string filePath = FilePathTextBox.Text;
            string checksum = CalculateMD5Checksum(filePath);
            ChecksumTextBox.Text = checksum;
            SaveChecksumToFile(filePath, checksum);
        }
        private void VerifyChecksumButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilePathTextBox.Text) ||
           string.IsNullOrWhiteSpace(ChecksumTextBox.Text))
            {
                MessageBox.Show("Выберите файл и расчитайте его контрольную сумму.");
                return;
            }
            string filePath = FilePathTextBox.Text;
            string storedChecksum = File.ReadAllText(System.IO.Path.ChangeExtension(filePath,"md5")).Trim().ToLowerInvariant();
            string calculatedChecksum = ChecksumTextBox.Text.Trim().ToLowerInvariant();
            if (storedChecksum == calculatedChecksum)
            {
                MessageBox.Show("Контрольная сумма успешно проверена. Файл не был изменен.");
            }
            else
            {
                MessageBox.Show("Проверка контрольной суммы не удалась. Возможно, файл был изменен.");
            }
        }
        private string CalculateMD5Checksum(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        private void SaveChecksumToFile(string filePath, string checksum)
        {
            string checksumFilePath = System.IO.Path.ChangeExtension(filePath, "md5");
            File.WriteAllText(checksumFilePath, checksum);
            MessageBox.Show($"Контрольная сумма сохраняется в {checksumFilePath}");
        }
    }
}
