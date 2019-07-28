namespace Lists.Processor
{
    public interface IService 
    {
        void Prestart();
        void Prestop();
        void Start();
        void Stop();
    }
}