namespace VideoPlayerLibrary
{
    /// <summary>
    /// Класс, представляющий видео.
    /// </summary>
    public class Video
    {
        public string Title { get; set; }
        public string FilePath { get; set; }

        public Video(string title, string filePath)
        {
            Title = title;
            FilePath = filePath;
        }
    }
}
