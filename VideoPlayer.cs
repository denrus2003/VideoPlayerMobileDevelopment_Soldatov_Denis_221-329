using System;

namespace VideoPlayerLibrary
{
    public delegate void VideoEventHandler(object sender, VideoEventArgs e);

    public class VideoEventArgs : EventArgs
    {
        public string Message { get; set; }
        public VideoEventArgs(string message)
        {
            Message = message;
        }
    }

    public class VideoPlayer
    {
        public event VideoEventHandler VideoStarted;
        public event VideoEventHandler VideoPaused;
        public event VideoEventHandler VideoStopped;

        public Video CurrentVideo { get; private set; }

        /// <summary>
        /// Загружает видео для воспроизведения.
        /// </summary>
        public void LoadVideo(Video video)
        {
            CurrentVideo = video;
        }

        /// <summary>
        /// Запускает воспроизведение видео.
        /// </summary>
        public void Play()
        {
            if (CurrentVideo == null || string.IsNullOrEmpty(CurrentVideo.FilePath))
                throw new InvalidOperationException("Видео не загружено или не выбран файл.");

            OnVideoStarted(new VideoEventArgs($"Начало воспроизведения: {CurrentVideo.Title}"));
        }

        /// <summary>
        /// Приостанавливает воспроизведение видео.
        /// </summary>
        public void Pause()
        {
            if (CurrentVideo == null || string.IsNullOrEmpty(CurrentVideo.FilePath))
                throw new InvalidOperationException("Видео не загружено или не выбран файл.");

            OnVideoPaused(new VideoEventArgs($"Видео на паузе: {CurrentVideo.Title}"));
        }

        /// <summary>
        /// Останавливает воспроизведение видео.
        /// </summary>
        public void Stop()
        {
            if (CurrentVideo == null || string.IsNullOrEmpty(CurrentVideo.FilePath))
                throw new InvalidOperationException("Видео не загружено или не выбран файл.");

            OnVideoStopped(new VideoEventArgs($"Остановка воспроизведения: {CurrentVideo.Title}"));
        }

        protected virtual void OnVideoStarted(VideoEventArgs e)
        {
            VideoStarted?.Invoke(this, e);
        }
        protected virtual void OnVideoPaused(VideoEventArgs e)
        {
            VideoPaused?.Invoke(this, e);
        }
        protected virtual void OnVideoStopped(VideoEventArgs e)
        {
            VideoStopped?.Invoke(this, e);
        }
    }
}