using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using NFL.BigDataBowl.DataModels;
using NFL.BigDataBowl.Utilities;

namespace NFL.BigDataBowl.MLModels
{
    public class ModelConfigurator
    {
        public ModelConfigurator()
        {
        }

        public static void Run(IEnumerable<RushingRaw> rushingMetrics, CancellationToken cancellationToken)
        {
            var playerMetricsPerPlay = rushingMetrics
                .GroupBy(x => (x.GameId, x.Season, x.PlayId, x.Yards))
                .ToDictionary(g => g.Key, g => g.ToList());

            var plays = playerMetricsPerPlay
                .Select(x => x.Value)
                .SelectMany(_ => _)
                .Select(x => new PlayMetrics()
                {
                    NflId = x.NflId,
                    GameId = x.GameId,
                    Season = x.Season,
                    Yards = x.Yards,
                    PlayId = x.PlayId,
                    Quarter = x.Quarter,
                    Down = x.Down,
                    MinutesRemainingInQuarter = x.MinutesRemainingInQuarter,
                    YardsFromOwnGoal = x.YardsFromOwnGoal,
                    IsOffenseLeading = x.IsOffenseLeading,
                    StandardisedX = x.StandardisedX,
                    StandardisedY = x.StandardisedY,
                    StandardisedDir = x.StandardisedDir,
                    RelativeX = x.RelativeX,
                    RelativeY = x.RelativeY,
                    RelativeSpeedX = x.RelativeSpeedX,
                    RelativeSpeedY = x.RelativeSpeedY
                });

            var mlContext = new MLContext();
            var tensorFlowModelFilePath = CsvReader.GetAbsolutePath(@"../../../MLModels/TensorFlowModel");

            var data = mlContext.Data.LoadFromEnumerable(plays);
            var dataSplit = mlContext.Data.TrainTestSplit(data, 0.2);

            var trainData = dataSplit.TrainSet;
            var testData = dataSplit.TestSet;

            string AppendStringWithEncoded(string input)
            {
                return $"{input}Encoded";
            }

            OneHotEncodingEstimator OneHotEncode(string input)
            {
                return mlContext.Transforms.Categorical.OneHotEncoding(AppendStringWithEncoded(input), input);
            }

            NormalizingEstimator Normalize(string input)
            {
                return mlContext.Transforms.NormalizeMeanVariance(input, useCdf: false);
            }

            var trainingPipeline = mlContext.Transforms.Categorical
                .OneHotEncoding(AppendStringWithEncoded(nameof(PlayMetrics.NflId)), nameof(PlayMetrics.NflId))
                .Append(OneHotEncode(nameof(PlayMetrics.Season)))
                .Append(OneHotEncode(nameof(PlayMetrics.Quarter)))
                .Append(OneHotEncode(nameof(PlayMetrics.Down)))
                .Append(OneHotEncode(nameof(PlayMetrics.YardsFromOwnGoal)))
                .Append(OneHotEncode(nameof(PlayMetrics.IsOffenseLeading)))
                .Append(Normalize(nameof(PlayMetrics.MinutesRemainingInQuarter)))
                .Append(Normalize(nameof(PlayMetrics.StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.RelativeSpeedY)))
                .Append(mlContext.Transforms.Concatenate(
                    "Features",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.NflId))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.Season))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.Quarter))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.Down))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.YardsFromOwnGoal))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.IsOffenseLeading))}",
                    nameof(PlayMetrics.MinutesRemainingInQuarter),
                    nameof(PlayMetrics.StandardisedX),
                    nameof(PlayMetrics.StandardisedY),
                    nameof(PlayMetrics.StandardisedDir),
                    nameof(PlayMetrics.RelativeX),
                    nameof(PlayMetrics.RelativeY),
                    nameof(PlayMetrics.RelativeSpeedX),
                    nameof(PlayMetrics.RelativeSpeedY)))
                .Append(mlContext.Model
                    .LoadTensorFlowModel(tensorFlowModelFilePath)
                    .ScoreTensorFlowModel(nameof(ExpectedYardsPrediction.ExpectedYards), "X", false));

            var trainedModel = trainingPipeline.Fit(trainData);
            var predictionFunction =
                mlContext.Model.CreatePredictionEngine<PlayMetrics, ExpectedYardsPrediction>(trainedModel);

            var predictions = trainedModel.Transform(testData);
            var metrics = mlContext.Regression.Evaluate(predictions);

            Console.WriteLine($"**ModelBuilder metrics**");
            Console.WriteLine($"Loss Function            : {metrics.LossFunction:0.###}");
            Console.WriteLine($"R Squared                : {metrics.RSquared:0.###}");
            Console.WriteLine($"Mean Absolute Error      : {metrics.MeanAbsoluteError:0.###}");
            Console.WriteLine($"Mean Squared Error       : {metrics.MeanSquaredError:0.###}");
            Console.WriteLine($"Root Mean Squared Error  : {metrics.RootMeanSquaredError:0.###}");
        }

        private float PredictExpectedYards(
            PredictionEngineBase<PlayMetrics, ExpectedYardsPrediction> predictionFunction, PlayMetrics play)
        {
            var prediction = predictionFunction.Predict(play);
            Console.WriteLine($"Expected Yards = {prediction.ExpectedYards}");

            return prediction.ExpectedYards;
        }
    }

    public class ExpectedYardsPrediction
    {
        [ColumnName("Score")] public float ExpectedYards;
    }
}