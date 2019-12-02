using System.Threading.Tasks;

namespace DaysOfServerless.Day1.Services
{
    public interface ISpinDreidel
    {
        Task<string> Spin();
    }
}
