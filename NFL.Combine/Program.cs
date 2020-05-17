using System.Threading.Tasks;

namespace NFL.Combine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CombineCollector cc = new CombineCollector();
            await cc.AllCombineWorkouts(2020);
        }
    }
}
