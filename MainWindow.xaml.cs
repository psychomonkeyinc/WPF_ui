using System;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WPF_ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _micTimer;
        private readonly Random _rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            _micTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(120) };
            _micTimer.Tick += MicTimer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gemma started.", "Gemma", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open settings (not implemented).", "Gemma", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Camera controls (placeholders)
        private void StartCamButton_Click(object sender, RoutedEventArgs e)
        {
            CameraPlaceholderText.Visibility = Visibility.Collapsed;
            CameraImage.Visibility = Visibility.Visible;
            // TODO: implement actual camera capture using MediaCapture, DirectShow, AForge, or OpenCvSharp.
            // For a quick start with webcam, consider OpenCvSharp and capture frames to a WriteableBitmap bound to CameraImage.Source.
            StartCamButton.IsEnabled = false;
            StopCamButton.IsEnabled = true;
        }

        private void StopCamButton_Click(object sender, RoutedEventArgs e)
        {
            CameraPlaceholderText.Visibility = Visibility.Visible;
            CameraImage.Visibility = Visibility.Collapsed;
            // TODO: stop camera capture and dispose resources.
            StartCamButton.IsEnabled = true;
            StopCamButton.IsEnabled = false;
        }

        // Microphone simulation (visual only for now)
        private void StartMicButton_Click(object sender, RoutedEventArgs e)
        {
            _micTimer.Start();
            StartMicButton.IsEnabled = false;
            StopMicButton.IsEnabled = true;
        }

        private void StopMicButton_Click(object sender, RoutedEventArgs e)
        {
            _micTimer.Stop();
            MicLevelBar.Value = 0;
            StartMicButton.IsEnabled = true;
            StopMicButton.IsEnabled = false;
        }

        private void MicTimer_Tick(object? sender, EventArgs e)
        {
            // simulate a mic level value between 0 and 100
            MicLevelBar.Value = Math.Abs((_rand.NextDouble() - 0.2) * 140);
        }

        // STT/TTS placeholders. For real speech, use Microsoft Cognitive Services Speech SDK or System.Speech (desktop).
        private void StartSttButton_Click(object sender, RoutedEventArgs e)
        {
            TranscriptBox.Text += "[STT started — listening...]\n";
            StartSttButton.IsEnabled = false;
            StopSttButton.IsEnabled = true;
            // TODO: hook microphone input and perform streaming recognition
        }

        private void StopSttButton_Click(object sender, RoutedEventArgs e)
        {
            TranscriptBox.Text += "[STT stopped]\n";
            StartSttButton.IsEnabled = true;
            StopSttButton.IsEnabled = false;
            // TODO: stop recognition
        }

        private void SpeakButton_Click(object sender, RoutedEventArgs e)
        {
            var text = TtsInputBox.Text?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Enter text to speak.", "Gemma TTS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TranscriptBox.Text += "[TTS] " + text + "\n";
            SpeakButton.IsEnabled = false;
            StopSpeakButton.IsEnabled = true;

            // TODO: implement TTS using System.Speech.Synthesis or Azure Speech SDK
            MessageBox.Show("(Simulated) Speaking: " + text, "Gemma TTS", MessageBoxButton.OK, MessageBoxImage.Information);

            SpeakButton.IsEnabled = true;
            StopSpeakButton.IsEnabled = false;
        }

        private void StopSpeakButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: stop any playing TTS audio
            SpeakButton.IsEnabled = true;
            StopSpeakButton.IsEnabled = false;
            TranscriptBox.Text += "[TTS stopped]\n";
        }
    }
}