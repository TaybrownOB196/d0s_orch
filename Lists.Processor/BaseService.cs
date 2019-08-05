namespace Lists.Processor
{
    public abstract class BaseService : IService
    {
        protected readonly string _name; 
        protected BaseService(string name)
        {
            _name = name;
        }

        //Not used
        public void Prestart() { }
        //Not used
        public void Prestop() { }

        public abstract void Start();
        public abstract void Stop();
    }
}