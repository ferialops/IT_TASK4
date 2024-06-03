using System;
using System.Threading.Tasks;

namespace task4
{
    public class Miller
    {
        public event EventHandler<DetailEventArgs> DetailReady;

        public void ProcessDetail(Detail detail)
        {
            Task.Run(() =>
            {
                Console.WriteLine($"Miller: Processing detail {detail.DetailId}...");
                System.Threading.Thread.Sleep(2000); // Имитация времени на обработку

                OnDetailReady(new DetailEventArgs { Detail = detail });
            });
        }

        protected virtual void OnDetailReady(DetailEventArgs e)
        {
            DetailReady?.Invoke(this, e);
        }
    }
}
