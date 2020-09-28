
namespace BigDataBowl.MLModels
{
    public class ModelConfig
    {
        public int Priority { get; set; } = 100;
        public string Name { get; set; }
        public bool Enabled { get; set; } = true;
        public bool IsImportingGraph { get; set; } = false;
    }
}