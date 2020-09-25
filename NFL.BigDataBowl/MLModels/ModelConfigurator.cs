using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using NFL.BigDataBowl.Models;
using NFL.BigDataBowl.Utilities;
using NumSharp.Extensions;

namespace NFL.BigDataBowl.MLModels
{
    public class ModelConfigurator
    {
        private static MLContext mlContext;

        private static readonly string tensorFlowModelFilePath =
            CsvReader.GetAbsolutePath(@"../../../MLModels/rushing_model");

        public ITransformer Model { get; }

        public ModelConfigurator()
        {
            mlContext = new MLContext();
        }

        public static void Run(
            Dictionary<(long GameId, int Season, long PlayId, int Yards), List<PlayMetrics>> playerMetricsPerPlay,
            CancellationToken cancellationToken)
        {
            var meta = playerMetricsPerPlay.Keys;
            var plays = playerMetricsPerPlay.Values;

            var data = mlContext.Data.LoadFromEnumerable(plays);
            var dataSplit = mlContext.Data.TrainTestSplit(data, 0.2);

            var trainData = dataSplit.TrainSet;
            var testData = dataSplit.TestSet;

            static string AppendStringWithEncoded(string input)
            {
                return $"{input}Encoded";
            }

            OneHotEncodingEstimator OneHotEncode(string input)
            {
                return mlContext.Transforms.Categorical.OneHotEncoding(
                    AppendStringWithEncoded(input), input);
            }

            static NormalizingEstimator Normalize(string input)
            {
                return mlContext.Transforms.NormalizeMeanVariance(input, useCdf: false);
            }

            var trainingPipeline = mlContext.Transforms
                .CopyColumns("Y", nameof(PlayMetrics.Yards))
                .Append(OneHotEncode(nameof(PlayMetrics.NflId)))
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
                .Append(mlContext.Transforms.Concatenate("X",
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
                    .ScoreTensorFlowModel(nameof(ExpectedYardsPrediction.RegScores),
                        "X",
                        addBatchDimensionInput: false));

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

        private float[] PredictExpectedYards(
            PredictionEngineBase<PlayMetrics, ExpectedYardsPrediction> predictionFunction,
            PlayMetrics play)
        {
            var prediction = predictionFunction.Predict(play);
            Console.WriteLine($"Expected Yards = {prediction.RegScores}");

            return prediction.RegScores;
        }
    }

    public class ExpectedYardsPrediction
    {
        [VectorType(1)] public float[] RegScores;
    }
}