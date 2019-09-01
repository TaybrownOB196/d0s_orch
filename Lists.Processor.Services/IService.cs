using System.Threading.Tasks;

namespace Lists.Processor.Services
{
    public interface IService 
    {
        Task StartAsync();
        Task StopAsync();
    }
}