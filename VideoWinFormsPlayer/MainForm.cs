using System;
using System.Windows.Forms;
using VideoPlayerLibrary;

namespace VideoWinFormsPlayer
{
    public partial class MainForm : Form
    {
        private VideoPlayer player;

        public MainForm()
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
            lblStatus.Text = e.Message;
        }
        private void Player_VideoPaused(object sender, VideoEventArgs e)
        {
            lblStatus.Text = e.Message;
        }
        private void Player_VideoStopped(object sender, VideoEventArgs e)
        {
            lblStatus.Text = e.Message;
        }

        // Обработчик выбора файла
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Video Files|*.mp4;*.avi;*.mkv|All files|*.*";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Video video = new Video(System.IO.Path.GetFileName(openFile.FileName), openFile.FileName);
                player.LoadVideo(video);
                lblStatus.Text = $"Выбрано: {video.Title}";
               
                axWmp.URL = openFile.FileName;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                player.Play();
                axWmp.Ctlcontrols.play();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                player.Pause();
                axWmp.Ctlcontrols.pause();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                player.Stop();
                axWmp.Ctlcontrols.stop();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Компонент axWmp занимает всё доступное пространство (растягивается по всем сторонам)
            axWmp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Метка статуса всегда прижата к нижней части и растягивается по ширине
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Кнопки оставляем привязанными к верхнему левому углу (при необходимости можно настроить иначе)
            btnSelectFile.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnPlay.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnPause.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnStop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }
    }
}