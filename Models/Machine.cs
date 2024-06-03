using System;

namespace task4
{
    public class Machine
    {
        public event EventHandler<DetailEventArgs> NewDetail;

        public void ProduceDetail()
        {
            Console.WriteLine("Machine: Producing new detail...");
            System.Threading.Thread.Sleep(1000); // Имитация времени на производство

            OnNewDetail(new DetailEventArgs { Detail = new Detail() });
        }

        protected virtual void OnNewDetail(DetailEventArgs e)
        {
            NewDetail?.Invoke(this, e);
        }
    }

    public class DetailEventArgs : EventArgs
    {
        public Detail Detail { get; set; }
    }

    public class Detail
    {
        public Guid DetailId { get; } = Guid.NewGuid();
    }
}
