using System.Threading.Tasks;

namespace NFL.Combine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CombineCollector.AllCombineWorkouts(2020);
        }
    }
}
