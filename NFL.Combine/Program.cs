namespace NFL.Combine
{
    class Program
    {
        static void Main(string[] args)
        {
            CombineCollector cc = new CombineCollector();
            cc.AllCombineWorkouts(2020);
        }
    }
}
