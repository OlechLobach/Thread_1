using System;
using System.Diagnostics;
using System.Windows;

namespace ProcessApp
{
    public partial class MainWindow : Window
    {
        private Process _process;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartAndWaitProcessButton_Click(object sender, RoutedEventArgs e)
        {
            _process = new Process();
            _process.StartInfo.FileName = "notepad.exe";
            _process.Start();
            _process.WaitForExit();

            int exitCode = _process.ExitCode;
            MessageBox.Show($"Process exited with code: {exitCode}");
        }

        private void StartProcessButton_Click(object sender, RoutedEventArgs e)
        {
            _process = new Process();
            _process.StartInfo.FileName = "notepad.exe";
            _process.Start();
        }

        private void WaitForProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null)
            {
                _process.WaitForExit();
                int exitCode = _process.ExitCode;
                MessageBox.Show($"Process exited with code: {exitCode}");
            }
        }

        private void KillProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null && !_process.HasExited)
            {
                _process.Kill();
                MessageBox.Show("Process was killed.");
            }
        }

        private void StartProcessWithArgumentsButton_Click(object sender, RoutedEventArgs e)
        {
            _process = new Process();
            _process.StartInfo.FileName = "PathToChildProcess.exe";
            _process.StartInfo.Arguments = "7 3 +";
            _process.Start();
            _process.WaitForExit();

            int exitCode = _process.ExitCode;
            MessageBox.Show($"Process exited with code: {exitCode}");
        }

        private void StartSearchProcessButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"E:\someFolder\someFile.txt";
            string searchTerm = "bicycle";

            _process = new Process();
            _process.StartInfo.FileName = "PathToSearchProcess.exe";
            _process.StartInfo.Arguments = $"{filePath} {searchTerm}";
            _process.Start();
            _process.WaitForExit();

            int exitCode = _process.ExitCode;
            MessageBox.Show($"Process exited with code: {exitCode}");
        }

        private void LaunchApplication(string appName)
        {
            try
            {
                Process.Start(appName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start application: {ex.Message}");
            }
        }

        private void LaunchNotepadButton_Click(object sender, RoutedEventArgs e)
        {
            LaunchApplication("notepad.exe");
        }

        private void LaunchCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            LaunchApplication("calc.exe");
        }

        private void LaunchPaintButton_Click(object sender, RoutedEventArgs e)
        {
            LaunchApplication("mspaint.exe");
        }

        private void LaunchCustomAppButton_Click(object sender, RoutedEventArgs e)
        {
            string customAppPath = CustomAppPathTextBox.Text;
            LaunchApplication(customAppPath);
        }
    }
}