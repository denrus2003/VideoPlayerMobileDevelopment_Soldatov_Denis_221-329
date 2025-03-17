using System;
using System.Diagnostics;
using VideoPlayerLibrary;

namespace VideoConsolePlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("Введите путь к видео файлу: ");
            string filePath = Console.ReadLine();

            Console.Write("Введите название видео: ");
            string title = Console.ReadLine();

            Video video = new Video(title, filePath);
            VideoPlayer player = new VideoPlayer();
            player.LoadVideo(video);

            player.VideoStarted += Player_VideoStarted;
            player.VideoPaused += Player_VideoPaused;
            player.VideoStopped += Player_VideoStopped;

            Console.WriteLine("Введите команду (play, pause, stop, exit):");
            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                try
                {
                    switch (command.ToLower())
                    {
                        case "play":
                            player.Play();
                            // Запускаем видео с помощью стандартного плеера
                            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                            break;
                        case "pause":
                            player.Pause();
                            // Реальное приостановление не поддерживается – можно вывести сообщение
                            Console.WriteLine("Пауза не поддерживается в консольном режиме.");
                            break;
                        case "stop":
                            player.Stop();
                            Console.WriteLine("Остановка не поддерживается в консольном режиме.");
                            break;
                        default:
                            Console.WriteLine("Неизвестная команда");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private static void Player_VideoStarted(object sender, VideoEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void Player_VideoPaused(object sender, VideoEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void Player_VideoStopped(object sender, VideoEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}