using System;

namespace task4
{
    public class Loader : ILoader
    {
        public void LoadDetail(Detail detail)
        {
            Console.WriteLine($"Loader: Loading detail {detail.DetailId}...");
            System.Threading.Thread.Sleep(500); // Имитация времени на загрузку

            Console.WriteLine($"Loader: Detail {detail.DetailId} loaded successfully.");
        }
    }
}
