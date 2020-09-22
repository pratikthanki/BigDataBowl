using System.Collections.Generic;
using Microsoft.ML;
using NFL.BigDataBowl.Models;

namespace NFL.BigDataBowl
{
    public class Model
    {
        private static MLContext mlContext;
     
        public Model()
        {
            mlContext = new MLContext();
        }
        
        public ITransformer BuildModel(IEnumerable<RushingRaw> plays)
        {
            var dataView = mlContext.Data.LoadFromEnumerable(plays);

            // Split into train-test
            var dataSplit = mlContext.Data.TrainTestSplit(dataView, 0.2);
            var trainData = dataSplit.TrainSet;
            var testData = dataSplit.TestSet;

            // Transform and train
            var trainedModel = Train(trainData);

            return trainedModel;
        }

        private static ITransformer Train(IDataView trainData)
        {
            // Play features; GameId, PlayId, Season, Yards
            var trainingPipeline = mlContext.Transforms
                .CopyColumns("xYards", null)
                .Append(mlContext.Transforms.Concatenate(
                    "Features",
                    nameof(RushingRaw.GameId), nameof(RushingRaw.PlayId), nameof(RushingRaw.Season), 
                    nameof(RushingRaw.Yards)));
            
            return null;
        }
    }
}