using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ML;
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

        public static void Run(IEnumerable<RushingRaw> rushingMetrics)
        {
            var playerMetricsPerPlay = rushingMetrics
                .GroupBy(x => (x.GameId, x.Season, x.PlayId, x.Yards))
                .ToDictionary(g => g.Key, g => g.ToList());

            var plays = playerMetricsPerPlay
                .Select(x => x.Value)
                .Select(x => new PlayMetrics
                {
                    GameId = x[0].GameId,
                    Season = x[0].Season,
                    Yards = x[0].Yards,
                    PlayId = x[0].PlayId,
                    Quarter = x[0].Quarter,
                    Down = x[0].Down,
                    MinutesRemainingInQuarter = x[0].MinutesRemainingInQuarter,
                    YardsFromOwnGoal = x[0].YardsFromOwnGoal,
                    IsOffenseLeading = x[0].IsOffenseLeading,

                    Player1StandardisedX = x[0].StandardisedX,
                    Player1StandardisedY = x[0].StandardisedY,
                    Player1StandardisedDir = x[0].StandardisedDir,
                    Player1StandardisedOrientation = x[0].StandardisedOrientation,
                    Player1RelativeX = x[0].RelativeX,
                    Player1RelativeY = x[0].RelativeY,
                    Player1RelativeSpeedX = x[0].RelativeSpeedX,
                    Player1RelativeSpeedY = x[0].RelativeSpeedY,

                    Player2StandardisedX = x[1].StandardisedX,
                    Player2StandardisedY = x[1].StandardisedY,
                    Player2StandardisedDir = x[1].StandardisedDir,
                    Player2StandardisedOrientation = x[1].StandardisedOrientation,
                    Player2RelativeX = x[1].RelativeX,
                    Player2RelativeY = x[1].RelativeY,
                    Player2RelativeSpeedX = x[1].RelativeSpeedX,
                    Player2RelativeSpeedY = x[1].RelativeSpeedY,

                    Player3StandardisedX = x[2].StandardisedX,
                    Player3StandardisedY = x[2].StandardisedY,
                    Player3StandardisedDir = x[2].StandardisedDir,
                    Player3StandardisedOrientation = x[2].StandardisedOrientation,
                    Player3RelativeX = x[2].RelativeX,
                    Player3RelativeY = x[2].RelativeY,
                    Player3RelativeSpeedX = x[2].RelativeSpeedX,
                    Player3RelativeSpeedY = x[2].RelativeSpeedY,

                    Player4StandardisedX = x[3].StandardisedX,
                    Player4StandardisedY = x[3].StandardisedY,
                    Player4StandardisedDir = x[3].StandardisedDir,
                    Player4StandardisedOrientation = x[3].StandardisedOrientation,
                    Player4RelativeX = x[3].RelativeX,
                    Player4RelativeY = x[3].RelativeY,
                    Player4RelativeSpeedX = x[3].RelativeSpeedX,
                    Player4RelativeSpeedY = x[3].RelativeSpeedY,

                    Player5StandardisedX = x[4].StandardisedX,
                    Player5StandardisedY = x[4].StandardisedY,
                    Player5StandardisedDir = x[4].StandardisedDir,
                    Player5StandardisedOrientation = x[4].StandardisedOrientation,
                    Player5RelativeX = x[4].RelativeX,
                    Player5RelativeY = x[4].RelativeY,
                    Player5RelativeSpeedX = x[4].RelativeSpeedX,
                    Player5RelativeSpeedY = x[4].RelativeSpeedY,

                    Player6StandardisedX = x[5].StandardisedX,
                    Player6StandardisedY = x[5].StandardisedY,
                    Player6StandardisedDir = x[5].StandardisedDir,
                    Player6StandardisedOrientation = x[5].StandardisedOrientation,
                    Player6RelativeX = x[5].RelativeX,
                    Player6RelativeY = x[5].RelativeY,
                    Player6RelativeSpeedX = x[5].RelativeSpeedX,
                    Player6RelativeSpeedY = x[5].RelativeSpeedY,

                    Player7StandardisedX = x[6].StandardisedX,
                    Player7StandardisedY = x[6].StandardisedY,
                    Player7StandardisedDir = x[6].StandardisedDir,
                    Player7StandardisedOrientation = x[6].StandardisedOrientation,
                    Player7RelativeX = x[6].RelativeX,
                    Player7RelativeY = x[6].RelativeY,
                    Player7RelativeSpeedX = x[6].RelativeSpeedX,
                    Player7RelativeSpeedY = x[6].RelativeSpeedY,

                    Player8StandardisedX = x[7].StandardisedX,
                    Player8StandardisedY = x[7].StandardisedY,
                    Player8StandardisedDir = x[7].StandardisedDir,
                    Player8StandardisedOrientation = x[7].StandardisedOrientation,
                    Player8RelativeX = x[7].RelativeX,
                    Player8RelativeY = x[7].RelativeY,
                    Player8RelativeSpeedX = x[7].RelativeSpeedX,
                    Player8RelativeSpeedY = x[7].RelativeSpeedY,

                    Player9StandardisedX = x[8].StandardisedX,
                    Player9StandardisedY = x[8].StandardisedY,
                    Player9StandardisedDir = x[8].StandardisedDir,
                    Player9StandardisedOrientation = x[8].StandardisedOrientation,
                    Player9RelativeX = x[8].RelativeX,
                    Player9RelativeY = x[8].RelativeY,
                    Player9RelativeSpeedX = x[8].RelativeSpeedX,
                    Player9RelativeSpeedY = x[8].RelativeSpeedY,

                    Player10StandardisedX = x[9].StandardisedX,
                    Player10StandardisedY = x[9].StandardisedY,
                    Player10StandardisedDir = x[9].StandardisedDir,
                    Player10StandardisedOrientation = x[9].StandardisedOrientation,
                    Player10RelativeX = x[9].RelativeX,
                    Player10RelativeY = x[9].RelativeY,
                    Player10RelativeSpeedX = x[9].RelativeSpeedX,
                    Player10RelativeSpeedY = x[9].RelativeSpeedY,

                    Player11StandardisedX = x[10].StandardisedX,
                    Player11StandardisedY = x[10].StandardisedY,
                    Player11StandardisedDir = x[10].StandardisedDir,
                    Player11StandardisedOrientation = x[10].StandardisedOrientation,
                    Player11RelativeX = x[10].RelativeX,
                    Player11RelativeY = x[10].RelativeY,
                    Player11RelativeSpeedX = x[10].RelativeSpeedX,
                    Player11RelativeSpeedY = x[10].RelativeSpeedY,

                    Player12StandardisedX = x[11].StandardisedX,
                    Player12StandardisedY = x[11].StandardisedY,
                    Player12StandardisedDir = x[11].StandardisedDir,
                    Player12StandardisedOrientation = x[11].StandardisedOrientation,
                    Player12RelativeX = x[11].RelativeX,
                    Player12RelativeY = x[11].RelativeY,
                    Player12RelativeSpeedX = x[11].RelativeSpeedX,
                    Player12RelativeSpeedY = x[11].RelativeSpeedY,

                    Player13StandardisedX = x[12].StandardisedX,
                    Player13StandardisedY = x[12].StandardisedY,
                    Player13StandardisedDir = x[12].StandardisedDir,
                    Player13StandardisedOrientation = x[12].StandardisedOrientation,
                    Player13RelativeX = x[12].RelativeX,
                    Player13RelativeY = x[12].RelativeY,
                    Player13RelativeSpeedX = x[12].RelativeSpeedX,
                    Player13RelativeSpeedY = x[12].RelativeSpeedY,

                    Player14StandardisedX = x[13].StandardisedX,
                    Player14StandardisedY = x[13].StandardisedY,
                    Player14StandardisedDir = x[13].StandardisedDir,
                    Player14StandardisedOrientation = x[13].StandardisedOrientation,
                    Player14RelativeX = x[13].RelativeX,
                    Player14RelativeY = x[13].RelativeY,
                    Player14RelativeSpeedX = x[13].RelativeSpeedX,
                    Player14RelativeSpeedY = x[13].RelativeSpeedY,

                    Player15StandardisedX = x[14].StandardisedX,
                    Player15StandardisedY = x[14].StandardisedY,
                    Player15StandardisedDir = x[14].StandardisedDir,
                    Player15StandardisedOrientation = x[14].StandardisedOrientation,
                    Player15RelativeX = x[14].RelativeX,
                    Player15RelativeY = x[14].RelativeY,
                    Player15RelativeSpeedX = x[14].RelativeSpeedX,
                    Player15RelativeSpeedY = x[14].RelativeSpeedY,

                    Player16StandardisedX = x[15].StandardisedX,
                    Player16StandardisedY = x[15].StandardisedY,
                    Player16StandardisedDir = x[15].StandardisedDir,
                    Player16StandardisedOrientation = x[15].StandardisedOrientation,
                    Player16RelativeX = x[15].RelativeX,
                    Player16RelativeY = x[15].RelativeY,
                    Player16RelativeSpeedX = x[15].RelativeSpeedX,
                    Player16RelativeSpeedY = x[15].RelativeSpeedY,

                    Player17StandardisedX = x[16].StandardisedX,
                    Player17StandardisedY = x[16].StandardisedY,
                    Player17StandardisedDir = x[16].StandardisedDir,
                    Player17StandardisedOrientation = x[16].StandardisedOrientation,
                    Player17RelativeX = x[16].RelativeX,
                    Player17RelativeY = x[16].RelativeY,
                    Player17RelativeSpeedX = x[16].RelativeSpeedX,
                    Player17RelativeSpeedY = x[16].RelativeSpeedY,

                    Player18StandardisedX = x[17].StandardisedX,
                    Player18StandardisedY = x[17].StandardisedY,
                    Player18StandardisedDir = x[17].StandardisedDir,
                    Player18StandardisedOrientation = x[17].StandardisedOrientation,
                    Player18RelativeX = x[17].RelativeX,
                    Player18RelativeY = x[17].RelativeY,
                    Player18RelativeSpeedX = x[17].RelativeSpeedX,
                    Player18RelativeSpeedY = x[17].RelativeSpeedY,

                    Player19StandardisedX = x[18].StandardisedX,
                    Player19StandardisedY = x[18].StandardisedY,
                    Player19StandardisedDir = x[18].StandardisedDir,
                    Player19StandardisedOrientation = x[18].StandardisedOrientation,
                    Player19RelativeX = x[18].RelativeX,
                    Player19RelativeY = x[18].RelativeY,
                    Player19RelativeSpeedX = x[18].RelativeSpeedX,
                    Player19RelativeSpeedY = x[18].RelativeSpeedY,

                    Player20StandardisedX = x[19].StandardisedX,
                    Player20StandardisedY = x[19].StandardisedY,
                    Player20StandardisedDir = x[19].StandardisedDir,
                    Player20StandardisedOrientation = x[19].StandardisedOrientation,
                    Player20RelativeX = x[19].RelativeX,
                    Player20RelativeY = x[19].RelativeY,
                    Player20RelativeSpeedX = x[19].RelativeSpeedX,
                    Player20RelativeSpeedY = x[19].RelativeSpeedY,

                    Player21StandardisedX = x[20].StandardisedX,
                    Player21StandardisedY = x[20].StandardisedY,
                    Player21StandardisedDir = x[20].StandardisedDir,
                    Player21StandardisedOrientation = x[20].StandardisedOrientation,
                    Player21RelativeX = x[20].RelativeX,
                    Player21RelativeY = x[20].RelativeY,
                    Player21RelativeSpeedX = x[20].RelativeSpeedX,
                    Player21RelativeSpeedY = x[20].RelativeSpeedY,

                    Player22StandardisedX = x[21].StandardisedX,
                    Player22StandardisedY = x[21].StandardisedY,
                    Player22StandardisedDir = x[21].StandardisedDir,
                    Player22StandardisedOrientation = x[21].StandardisedOrientation,
                    Player22RelativeX = x[21].RelativeX,
                    Player22RelativeY = x[21].RelativeY,
                    Player22RelativeSpeedX = x[21].RelativeSpeedX,
                    Player22RelativeSpeedY = x[21].RelativeSpeedY
                });

            var mlContext = new MLContext();
            var tensorFlowModelFilePath = CsvReader.GetAbsolutePath(@"../../../MLModels/TensorFlowModel");

            var data = mlContext.Data.LoadFromEnumerable(plays);
            var shuffledData = mlContext.Data.ShuffleRows(data);

            var dataSplit = mlContext.Data.TrainTestSplit(shuffledData, 0.2);
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

            var trainingPipeline = mlContext.Transforms
                .CopyColumns("xYards", nameof(ExpectedYardsPrediction.Yards))
                .Append(OneHotEncode(nameof(PlayMetrics.Season)))
                .Append(OneHotEncode(nameof(PlayMetrics.Quarter)))
                .Append(OneHotEncode(nameof(PlayMetrics.Down)))
                .Append(Normalize(nameof(PlayMetrics.MinutesRemainingInQuarter)))
                .Append(OneHotEncode(nameof(PlayMetrics.YardsFromOwnGoal)))
                .Append(OneHotEncode(nameof(PlayMetrics.IsOffenseLeading)))

                .Append(Normalize(nameof(PlayMetrics.Player1StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player1StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player1StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player1StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player1RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player1RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player1RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player1RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player2StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player2StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player2StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player2StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player2RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player2RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player2RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player2RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player3StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player3StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player3StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player3StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player3RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player3RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player3RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player3RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player4StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player4StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player4StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player4StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player4RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player4RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player4RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player4RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player5StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player5StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player5StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player5StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player5RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player5RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player5RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player5RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player6StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player6StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player6StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player6StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player6RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player6RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player6RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player6RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player7StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player7StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player7StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player7StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player7RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player7RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player7RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player7RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player8StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player8StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player8StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player8StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player8RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player8RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player8RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player8RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player9StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player9StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player9StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player9StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player9RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player9RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player9RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player9RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player10StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player10StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player10StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player10StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player10RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player10RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player10RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player10RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player11StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player11StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player11StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player11StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player11RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player11RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player11RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player11RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player12StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player12StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player12StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player12StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player12RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player12RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player12RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player12RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player13StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player13StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player13StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player13StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player13RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player13RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player13RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player13RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player14StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player14StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player14StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player14StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player14RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player14RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player14RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player14RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player15StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player15StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player15StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player15StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player15RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player15RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player15RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player15RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player16StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player16StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player16StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player16StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player16RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player16RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player16RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player16RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player17StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player17StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player17StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player17StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player17RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player17RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player17RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player17RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player18StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player18StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player18StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player18StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player18RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player18RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player18RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player18RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player19StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player19StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player19StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player19StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player19RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player19RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player19RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player19RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player20StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player20StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player20StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player20StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player20RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player20RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player20RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player20RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player21StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player21StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player21StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player21StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player21RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player21RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player21RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player21RelativeSpeedY)))

                .Append(Normalize(nameof(PlayMetrics.Player22StandardisedX)))
                .Append(Normalize(nameof(PlayMetrics.Player22StandardisedY)))
                .Append(Normalize(nameof(PlayMetrics.Player22StandardisedDir)))
                .Append(Normalize(nameof(PlayMetrics.Player2StandardisedOrientation)))
                .Append(Normalize(nameof(PlayMetrics.Player22RelativeX)))
                .Append(Normalize(nameof(PlayMetrics.Player22RelativeY)))
                .Append(Normalize(nameof(PlayMetrics.Player22RelativeSpeedX)))
                .Append(Normalize(nameof(PlayMetrics.Player22RelativeSpeedY)))

                .Append(mlContext.Transforms.Concatenate(
                    "Features",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.Season))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.Quarter))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.Down))}",
                    nameof(PlayMetrics.MinutesRemainingInQuarter),
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.YardsFromOwnGoal))}",
                    $"{AppendStringWithEncoded(nameof(PlayMetrics.IsOffenseLeading))}",

                    nameof(PlayMetrics.Player1StandardisedX),
                    nameof(PlayMetrics.Player1StandardisedY),
                    nameof(PlayMetrics.Player1StandardisedDir),
                    nameof(PlayMetrics.Player1StandardisedOrientation),
                    nameof(PlayMetrics.Player1RelativeX),
                    nameof(PlayMetrics.Player1RelativeY),
                    nameof(PlayMetrics.Player1RelativeSpeedX),
                    nameof(PlayMetrics.Player1RelativeSpeedY),

                    nameof(PlayMetrics.Player2StandardisedX),
                    nameof(PlayMetrics.Player2StandardisedY),
                    nameof(PlayMetrics.Player2StandardisedDir),
                    nameof(PlayMetrics.Player2StandardisedOrientation),
                    nameof(PlayMetrics.Player2RelativeX),
                    nameof(PlayMetrics.Player2RelativeY),
                    nameof(PlayMetrics.Player2RelativeSpeedX),
                    nameof(PlayMetrics.Player2RelativeSpeedY),

                    nameof(PlayMetrics.Player3StandardisedX),
                    nameof(PlayMetrics.Player3StandardisedY),
                    nameof(PlayMetrics.Player3StandardisedDir),
                    nameof(PlayMetrics.Player3StandardisedOrientation),
                    nameof(PlayMetrics.Player3RelativeX),
                    nameof(PlayMetrics.Player3RelativeY),
                    nameof(PlayMetrics.Player3RelativeSpeedX),
                    nameof(PlayMetrics.Player3RelativeSpeedY),

                    nameof(PlayMetrics.Player4StandardisedX),
                    nameof(PlayMetrics.Player4StandardisedY),
                    nameof(PlayMetrics.Player4StandardisedDir),
                    nameof(PlayMetrics.Player4StandardisedOrientation),
                    nameof(PlayMetrics.Player4RelativeX),
                    nameof(PlayMetrics.Player4RelativeY),
                    nameof(PlayMetrics.Player4RelativeSpeedX),
                    nameof(PlayMetrics.Player4RelativeSpeedY),

                    nameof(PlayMetrics.Player5StandardisedX),
                    nameof(PlayMetrics.Player5StandardisedY),
                    nameof(PlayMetrics.Player5StandardisedDir),
                    nameof(PlayMetrics.Player5StandardisedOrientation),
                    nameof(PlayMetrics.Player5RelativeX),
                    nameof(PlayMetrics.Player5RelativeY),
                    nameof(PlayMetrics.Player5RelativeSpeedX),
                    nameof(PlayMetrics.Player5RelativeSpeedY),

                    nameof(PlayMetrics.Player6StandardisedX),
                    nameof(PlayMetrics.Player6StandardisedY),
                    nameof(PlayMetrics.Player6StandardisedDir),
                    nameof(PlayMetrics.Player6StandardisedOrientation),
                    nameof(PlayMetrics.Player6RelativeX),
                    nameof(PlayMetrics.Player6RelativeY),
                    nameof(PlayMetrics.Player6RelativeSpeedX),
                    nameof(PlayMetrics.Player6RelativeSpeedY),

                    nameof(PlayMetrics.Player7StandardisedX),
                    nameof(PlayMetrics.Player7StandardisedY),
                    nameof(PlayMetrics.Player7StandardisedDir),
                    nameof(PlayMetrics.Player7StandardisedOrientation),
                    nameof(PlayMetrics.Player7RelativeX),
                    nameof(PlayMetrics.Player7RelativeY),
                    nameof(PlayMetrics.Player7RelativeSpeedX),
                    nameof(PlayMetrics.Player7RelativeSpeedY),

                    nameof(PlayMetrics.Player8StandardisedX),
                    nameof(PlayMetrics.Player8StandardisedY),
                    nameof(PlayMetrics.Player8StandardisedDir),
                    nameof(PlayMetrics.Player8StandardisedOrientation),
                    nameof(PlayMetrics.Player8RelativeX),
                    nameof(PlayMetrics.Player8RelativeY),
                    nameof(PlayMetrics.Player8RelativeSpeedX),
                    nameof(PlayMetrics.Player8RelativeSpeedY),

                    nameof(PlayMetrics.Player9StandardisedX),
                    nameof(PlayMetrics.Player9StandardisedY),
                    nameof(PlayMetrics.Player9StandardisedDir),
                    nameof(PlayMetrics.Player9StandardisedOrientation),
                    nameof(PlayMetrics.Player9RelativeX),
                    nameof(PlayMetrics.Player9RelativeY),
                    nameof(PlayMetrics.Player9RelativeSpeedX),
                    nameof(PlayMetrics.Player9RelativeSpeedY),

                    nameof(PlayMetrics.Player10StandardisedX),
                    nameof(PlayMetrics.Player10StandardisedY),
                    nameof(PlayMetrics.Player10StandardisedDir),
                    nameof(PlayMetrics.Player10StandardisedOrientation),
                    nameof(PlayMetrics.Player10RelativeX),
                    nameof(PlayMetrics.Player10RelativeY),
                    nameof(PlayMetrics.Player10RelativeSpeedX),
                    nameof(PlayMetrics.Player10RelativeSpeedY),

                    nameof(PlayMetrics.Player11StandardisedX),
                    nameof(PlayMetrics.Player11StandardisedY),
                    nameof(PlayMetrics.Player11StandardisedDir),
                    nameof(PlayMetrics.Player11StandardisedOrientation),
                    nameof(PlayMetrics.Player11RelativeX),
                    nameof(PlayMetrics.Player11RelativeY),
                    nameof(PlayMetrics.Player11RelativeSpeedX),
                    nameof(PlayMetrics.Player11RelativeSpeedY),

                    nameof(PlayMetrics.Player12StandardisedX),
                    nameof(PlayMetrics.Player12StandardisedY),
                    nameof(PlayMetrics.Player12StandardisedDir),
                    nameof(PlayMetrics.Player12StandardisedOrientation),
                    nameof(PlayMetrics.Player12RelativeX),
                    nameof(PlayMetrics.Player12RelativeY),
                    nameof(PlayMetrics.Player12RelativeSpeedX),
                    nameof(PlayMetrics.Player12RelativeSpeedY),

                    nameof(PlayMetrics.Player13StandardisedX),
                    nameof(PlayMetrics.Player13StandardisedY),
                    nameof(PlayMetrics.Player13StandardisedDir),
                    nameof(PlayMetrics.Player13StandardisedOrientation),
                    nameof(PlayMetrics.Player13RelativeX),
                    nameof(PlayMetrics.Player13RelativeY),
                    nameof(PlayMetrics.Player13RelativeSpeedX),
                    nameof(PlayMetrics.Player13RelativeSpeedY),

                    nameof(PlayMetrics.Player14StandardisedX),
                    nameof(PlayMetrics.Player14StandardisedY),
                    nameof(PlayMetrics.Player14StandardisedDir),
                    nameof(PlayMetrics.Player14StandardisedOrientation),
                    nameof(PlayMetrics.Player14RelativeX),
                    nameof(PlayMetrics.Player14RelativeY),
                    nameof(PlayMetrics.Player14RelativeSpeedX),
                    nameof(PlayMetrics.Player14RelativeSpeedY),

                    nameof(PlayMetrics.Player15StandardisedX),
                    nameof(PlayMetrics.Player15StandardisedY),
                    nameof(PlayMetrics.Player15StandardisedDir),
                    nameof(PlayMetrics.Player15StandardisedOrientation),
                    nameof(PlayMetrics.Player15RelativeX),
                    nameof(PlayMetrics.Player15RelativeY),
                    nameof(PlayMetrics.Player15RelativeSpeedX),
                    nameof(PlayMetrics.Player15RelativeSpeedY),

                    nameof(PlayMetrics.Player16StandardisedX),
                    nameof(PlayMetrics.Player16StandardisedY),
                    nameof(PlayMetrics.Player16StandardisedDir),
                    nameof(PlayMetrics.Player16StandardisedOrientation),
                    nameof(PlayMetrics.Player16RelativeX),
                    nameof(PlayMetrics.Player16RelativeY),
                    nameof(PlayMetrics.Player16RelativeSpeedX),
                    nameof(PlayMetrics.Player16RelativeSpeedY),

                    nameof(PlayMetrics.Player17StandardisedX),
                    nameof(PlayMetrics.Player17StandardisedY),
                    nameof(PlayMetrics.Player17StandardisedDir),
                    nameof(PlayMetrics.Player17StandardisedOrientation),
                    nameof(PlayMetrics.Player17RelativeX),
                    nameof(PlayMetrics.Player17RelativeY),
                    nameof(PlayMetrics.Player17RelativeSpeedX),
                    nameof(PlayMetrics.Player17RelativeSpeedY),

                    nameof(PlayMetrics.Player18StandardisedX),
                    nameof(PlayMetrics.Player18StandardisedY),
                    nameof(PlayMetrics.Player18StandardisedDir),
                    nameof(PlayMetrics.Player18StandardisedOrientation),
                    nameof(PlayMetrics.Player18RelativeX),
                    nameof(PlayMetrics.Player18RelativeY),
                    nameof(PlayMetrics.Player18RelativeSpeedX),
                    nameof(PlayMetrics.Player18RelativeSpeedY),

                    nameof(PlayMetrics.Player19StandardisedX),
                    nameof(PlayMetrics.Player19StandardisedY),
                    nameof(PlayMetrics.Player19StandardisedDir),
                    nameof(PlayMetrics.Player19StandardisedOrientation),
                    nameof(PlayMetrics.Player19RelativeX),
                    nameof(PlayMetrics.Player19RelativeY),
                    nameof(PlayMetrics.Player19RelativeSpeedX),
                    nameof(PlayMetrics.Player19RelativeSpeedY),

                    nameof(PlayMetrics.Player20StandardisedX),
                    nameof(PlayMetrics.Player20StandardisedY),
                    nameof(PlayMetrics.Player20StandardisedDir),
                    nameof(PlayMetrics.Player20StandardisedOrientation),
                    nameof(PlayMetrics.Player20RelativeX),
                    nameof(PlayMetrics.Player20RelativeY),
                    nameof(PlayMetrics.Player20RelativeSpeedX),
                    nameof(PlayMetrics.Player20RelativeSpeedY),

                    nameof(PlayMetrics.Player21StandardisedX),
                    nameof(PlayMetrics.Player21StandardisedY),
                    nameof(PlayMetrics.Player21StandardisedDir),
                    nameof(PlayMetrics.Player21StandardisedOrientation),
                    nameof(PlayMetrics.Player21RelativeX),
                    nameof(PlayMetrics.Player21RelativeY),
                    nameof(PlayMetrics.Player21RelativeSpeedX),
                    nameof(PlayMetrics.Player21RelativeSpeedY),

                    nameof(PlayMetrics.Player22StandardisedX),
                    nameof(PlayMetrics.Player22StandardisedY),
                    nameof(PlayMetrics.Player22StandardisedDir),
                    nameof(PlayMetrics.Player22StandardisedOrientation),
                    nameof(PlayMetrics.Player22RelativeX),
                    nameof(PlayMetrics.Player22RelativeY),
                    nameof(PlayMetrics.Player22RelativeSpeedX),
                    nameof(PlayMetrics.Player22RelativeSpeedY)
                ))
                .Append(mlContext.Model
                    .LoadTensorFlowModel(tensorFlowModelFilePath)
                    .ScoreTensorFlowModel(new[] {"xYards"}, new[] {"Features"}, false));

            var trainedModel = trainingPipeline.Fit(trainData);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<PlayMetrics, ExpectedYardsPrediction>(trainedModel);

            var testPlays = 
                mlContext.Data.CreateEnumerable<PlayMetrics>(testData, reuseRowObject: true);

            foreach (var play in testPlays)
            {
                var xYards = predictionEngine.Predict(play);

                Console.WriteLine($"PlayId: {play.PlayId}; Predicted: {xYards.Yards}; Actual: {play.Yards}");
            }
        }

        private float PredictExpectedYards(
            PredictionEngineBase<PlayMetrics, ExpectedYardsPrediction> predictionFunction, PlayMetrics play)
        {
            var prediction = predictionFunction.Predict(play);
            Console.WriteLine($"Expected Yards = {prediction.Yards}");

            return prediction.Yards;
        }
    }

    public class ExpectedYardsPrediction
    {
        public float Yards;
    }
}