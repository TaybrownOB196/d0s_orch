using System.Threading.Tasks;

namespace Lists.Processor
{
    public interface IService 
    {
        Task PrestartAsync();
        Task PrestopAsync();
        Task StartAsync();
        Task StopAsync();
    }
}