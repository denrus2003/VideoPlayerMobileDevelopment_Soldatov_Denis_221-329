using System;
using System.Windows;
using Microsoft.Win32;
using VideoPlayerLibrary;

namespace WPFVideoPlayer
{
    public partial class MainWindow : Window
    {
        private VideoPlayer player;

        public MainWindow()
        {
            InitializeComponent();
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            // Изначально видео не выбрано
            Video video = new Video("Не выбрано", "");
            player = new VideoPlayer();
            player.LoadVideo(video);

            player.VideoStarted += Player_VideoStarted;
            player.VideoPaused += Player_VideoPaused;
            player.VideoStopped += Player_VideoStopped;
        }

        private void Player_VideoStarted(object sender, VideoEventArgs e)
        {
            Dispatcher.Invoke(() => txtStatus.Text = e.Message);
        }
        private void Player_VideoPaused(object sender, VideoEventArgs e)
        {
            Dispatcher.Invoke(() => txtStatus.Text = e.Message);
        }
        private void Player_VideoStopped(object sender, VideoEventArgs e)
        {
            Dispatcher.Invoke(() => txtStatus.Text = e.Message);
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Video Files|*.mp4;*.avi;*.mkv|All files|*.*";
            if (openFile.ShowDialog() == true)
            {
                Video video = new Video(System.IO.Path.GetFileName(openFile.FileName), openFile.FileName);
                player.LoadVideo(video);
                txtStatus.Text = $"Выбрано: {video.Title}";
                try
                {
                    // Устанавливаем абсолютный путь для MediaElement
                    mediaElement.Source = new Uri(openFile.FileName, UriKind.Absolute);
                }
                catch (Exception ex)
                {
                    txtStatus.Text = "Ошибка установки источника: " + ex.Message;
                }
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                player.Play();
                mediaElement.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка воспроизведения: " + ex.Message);
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                player.Pause();
                mediaElement.Pause();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка паузы: " + ex.Message);
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                player.Stop();
                mediaElement.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка остановки: " + ex.Message);
            }
        }

        // Обработчик успешной загрузки видео
        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "Видео загружено, готово к воспроизведению.";
        }

        // Обработчик ошибки при загрузке видео
        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            txtStatus.Text = "Ошибка загрузки видео: " + e.ErrorException.Message;
        }
    }
}