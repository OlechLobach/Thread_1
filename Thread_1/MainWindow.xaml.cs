using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace ProcessManager
{
    public partial class MainWindow : Window
    {
        private System.Timers.Timer _timer;
        private int _updateInterval = 5; // Default update interval in seconds

        public MainWindow()
        {
            InitializeComponent();
            LoadProcesses();
            SetUpTimer();
        }

        private void LoadProcesses()
        {
            ProcessesListBox.Items.Clear();
            var processes = Process.GetProcesses().OrderBy(p => p.ProcessName).ToList();
            foreach (var process in processes)
            {
                ProcessesListBox.Items.Add(process);
            }
        }

        private void SetUpTimer()
        {
            _timer = new System.Timers.Timer(_updateInterval * 1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(LoadProcesses);
        }

        private void SetIntervalButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UpdateIntervalTextBox.Text, out int interval))
            {
                _updateInterval = interval;
                _timer.Interval = _updateInterval * 1000;
                MessageBox.Show($"Update interval set to {_updateInterval} seconds.");
            }
            else
            {
                MessageBox.Show("Invalid interval. Please enter a valid number.");
            }
        }

        private void ProcessesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProcessesListBox.SelectedItem is Process selectedProcess)
            {
                MessageBox.Show($"Process ID: {selectedProcess.Id}\n" +
                                $"Start Time: {selectedProcess.StartTime}\n" +
                                $"Total Processor Time: {selectedProcess.TotalProcessorTime}\n" +
                                $"Thread Count: {selectedProcess.Threads.Count}\n" +
                                $"Instance Count: {Process.GetProcessesByName(selectedProcess.ProcessName).Length}");
            }
        }

        private void TerminateProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessesListBox.SelectedItem is Process selectedProcess)
            {
                try
                {
                    selectedProcess.Kill();
                    MessageBox.Show($"Process {selectedProcess.ProcessName} terminated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error terminating process: {ex.Message}");
                }
            }
        }

        private void LaunchNotepadButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe");
        }

        private void LaunchCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void LaunchPaintButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mspaint.exe");
        }

        private void LaunchCustomAppButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Process.Start(openFileDialog.FileName);
            }
        }
    }
}