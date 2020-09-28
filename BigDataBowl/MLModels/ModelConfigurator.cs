using System;
using System.Collections.Generic;
using System.Linq;
using BigDataBowl.DataModels;
using NumSharp;
using Tensorflow;
using static Tensorflow.Binding;

namespace BigDataBowl.MLModels
{
    public class ModelConfigurator
    {
        private const int Classes = 100;
        private const float LearningRate = 0.1f;
        private const int DisplayStep = 100;
        private const int BatchSize = 180;
        private int TrainingSteps = 1000;

        private float accuracy;
        private IDatasetV2 TrainData;
        private NDArray XTest, YTest, XTrain, YTrain;

        public ModelConfig Config { get; set; }

        public ModelConfig InitConfig()
            => Config = new ModelConfig
            {
                Name = "Fully Connected Neural Network (Keras)",
                Enabled = true,
                IsImportingGraph = false,
                Priority = 12
            };

        public void Run(IEnumerable<RushingRaw> rushing)
        {
            var playerMetricsPerPlay = rushing
                .GroupBy(x => (x.GameId, x.Season, x.PlayId, x.Yards))
                .ToDictionary(g => g.Key, g => g.ToList());

            var plays = playerMetricsPerPlay
                .Select(x => x.Value).ToList();

            #region Train-Test Data

            var testSplit = (int) (0.2 * plays.Count);

            var trainMetrics = plays.Select(x => new[]
            {
                // x[0].GameId,
                // x[0].Season,
                // x[0].Yards,
                // x[0].PlayId,
                x[0].Quarter,
                x[0].Down,
                x[0].MinutesRemainingInQuarter,
                x[0].YardsFromOwnGoal,
                // x[0].IsOffenseLeading,

                x[0].StandardisedX,
                x[0].StandardisedY,
                x[0].StandardisedDir,
                x[0].StandardisedOrientation,
                x[0].RelativeX,
                x[0].RelativeY,
                x[0].RelativeSpeedX,
                x[0].RelativeSpeedY,

                x[1].StandardisedX,
                x[1].StandardisedY,
                x[1].StandardisedDir,
                x[1].StandardisedOrientation,
                x[1].RelativeX,
                x[1].RelativeY,
                x[1].RelativeSpeedX,
                x[1].RelativeSpeedY,

                x[2].StandardisedX,
                x[2].StandardisedY,
                x[2].StandardisedDir,
                x[2].StandardisedOrientation,
                x[2].RelativeX,
                x[2].RelativeY,
                x[2].RelativeSpeedX,
                x[2].RelativeSpeedY,

                x[3].StandardisedX,
                x[3].StandardisedY,
                x[3].StandardisedDir,
                x[3].StandardisedOrientation,
                x[3].RelativeX,
                x[3].RelativeY,
                x[3].RelativeSpeedX,
                x[3].RelativeSpeedY,

                x[4].StandardisedX,
                x[4].StandardisedY,
                x[4].StandardisedDir,
                x[4].StandardisedOrientation,
                x[4].RelativeX,
                x[4].RelativeY,
                x[4].RelativeSpeedX,
                x[4].RelativeSpeedY,

                x[5].StandardisedX,
                x[5].StandardisedY,
                x[5].StandardisedDir,
                x[5].StandardisedOrientation,
                x[5].RelativeX,
                x[5].RelativeY,
                x[5].RelativeSpeedX,
                x[5].RelativeSpeedY,

                x[6].StandardisedX,
                x[6].StandardisedY,
                x[6].StandardisedDir,
                x[6].StandardisedOrientation,
                x[6].RelativeX,
                x[6].RelativeY,
                x[6].RelativeSpeedX,
                x[6].RelativeSpeedY,

                x[7].StandardisedX,
                x[7].StandardisedY,
                x[7].StandardisedDir,
                x[7].StandardisedOrientation,
                x[7].RelativeX,
                x[7].RelativeY,
                x[7].RelativeSpeedX,
                x[7].RelativeSpeedY,

                x[8].StandardisedX,
                x[8].StandardisedY,
                x[8].StandardisedDir,
                x[8].StandardisedOrientation,
                x[8].RelativeX,
                x[8].RelativeY,
                x[8].RelativeSpeedX,
                x[8].RelativeSpeedY,

                x[9].StandardisedX,
                x[9].StandardisedY,
                x[9].StandardisedDir,
                x[9].StandardisedOrientation,
                x[9].RelativeX,
                x[9].RelativeY,
                x[9].RelativeSpeedX,
                x[9].RelativeSpeedY,

                x[10].StandardisedX,
                x[10].StandardisedY,
                x[10].StandardisedDir,
                x[10].StandardisedOrientation,
                x[10].RelativeX,
                x[10].RelativeY,
                x[10].RelativeSpeedX,
                x[10].RelativeSpeedY,

                x[11].StandardisedX,
                x[11].StandardisedY,
                x[11].StandardisedDir,
                x[11].StandardisedOrientation,
                x[11].RelativeX,
                x[11].RelativeY,
                x[11].RelativeSpeedX,
                x[11].RelativeSpeedY,

                x[12].StandardisedX,
                x[12].StandardisedY,
                x[12].StandardisedDir,
                x[12].StandardisedOrientation,
                x[12].RelativeX,
                x[12].RelativeY,
                x[12].RelativeSpeedX,
                x[12].RelativeSpeedY,

                x[13].StandardisedX,
                x[13].StandardisedY,
                x[13].StandardisedDir,
                x[13].StandardisedOrientation,
                x[13].RelativeX,
                x[13].RelativeY,
                x[13].RelativeSpeedX,
                x[13].RelativeSpeedY,

                x[14].StandardisedX,
                x[14].StandardisedY,
                x[14].StandardisedDir,
                x[14].StandardisedOrientation,
                x[14].RelativeX,
                x[14].RelativeY,
                x[14].RelativeSpeedX,
                x[14].RelativeSpeedY,

                x[15].StandardisedX,
                x[15].StandardisedY,
                x[15].StandardisedDir,
                x[15].StandardisedOrientation,
                x[15].RelativeX,
                x[15].RelativeY,
                x[15].RelativeSpeedX,
                x[15].RelativeSpeedY,

                x[16].StandardisedX,
                x[16].StandardisedY,
                x[16].StandardisedDir,
                x[16].StandardisedOrientation,
                x[16].RelativeX,
                x[16].RelativeY,
                x[16].RelativeSpeedX,
                x[16].RelativeSpeedY,

                x[17].StandardisedX,
                x[17].StandardisedY,
                x[17].StandardisedDir,
                x[17].StandardisedOrientation,
                x[17].RelativeX,
                x[17].RelativeY,
                x[17].RelativeSpeedX,
                x[17].RelativeSpeedY,

                x[18].StandardisedX,
                x[18].StandardisedY,
                x[18].StandardisedDir,
                x[18].StandardisedOrientation,
                x[18].RelativeX,
                x[18].RelativeY,
                x[18].RelativeSpeedX,
                x[18].RelativeSpeedY,

                x[19].StandardisedX,
                x[19].StandardisedY,
                x[19].StandardisedDir,
                x[19].StandardisedOrientation,
                x[19].RelativeX,
                x[19].RelativeY,
                x[19].RelativeSpeedX,
                x[19].RelativeSpeedY,

                x[20].StandardisedX,
                x[20].StandardisedY,
                x[20].StandardisedDir,
                x[20].StandardisedOrientation,
                x[20].RelativeX,
                x[20].RelativeY,
                x[20].RelativeSpeedX,
                x[20].RelativeSpeedY,

                x[21].StandardisedX,
                x[21].StandardisedY,
                x[21].StandardisedDir,
                x[21].StandardisedOrientation,
                x[21].RelativeX,
                x[21].RelativeY,
                x[21].RelativeSpeedX,
                x[21].RelativeSpeedY
            }).ToArray();

            XTrain = np.array(trainMetrics);
            YTrain = np.array(plays.Select(x => x[0].Yards));

            #endregion

            tf.enable_eager_execution();

            TrainData = tf.data.Dataset.from_tensor_slices(XTrain, YTrain);
            TrainData = TrainData
                .repeat()
                .shuffle(5000)
                .batch(BatchSize)
                .prefetch(1)
                .take(TrainingSteps);

            // Build neural network model
            var neural_net = new NeuralNet(new NeuralNetArgs
            {
                NumClasses = Classes,
                NeuronOfHidden1 = 128,
                Activation1 = tf.keras.activations.Relu,
                NeuronOfHidden2 = 256,
                Activation2 = tf.keras.activations.Relu,
                ActivationOutput = tf.keras.activations.Relu
            });

            /*
             * Cross-Entropy Loss
             * - Convert labels to int 64 for tf cross-entropy function.
             * - Apply softmax to logits and compute cross-entropy.
             *
             * Return average loss across the batch.
             */
            Tensor CrossEntropyLoss(Tensor x, Tensor y)
            {
                y = tf.cast(y, tf.int64);
                var loss = tf.nn.sparse_softmax_cross_entropy_with_logits(y, x);

                return tf.reduce_mean(loss);
            }

            /*
             * Accuracy metric
             * 
             * Predicted class is the index of highest score in prediction vector (i.e. argmax).
             */
            Tensor Accuracy(Tensor y_pred, Tensor y_true)
            {
                var correct_prediction = tf.equal(tf.argmax(y_pred, 1), tf.cast(y_true, tf.int64));
                return tf.reduce_mean(tf.cast(correct_prediction, tf.float32), -1);
            }

            // Stochastic gradient descent optimizer.
            var optimizer = tf.optimizers.SGD(LearningRate);

            /*
             * Optimization process
             *
             * - Wrap computation inside a GradientTape for automatic differentiation. 
             */
            void RunOptimization(Tensor x, Tensor y)
            {
                using var g = tf.GradientTape();

                var pred = neural_net.Apply(x, is_training: true);
                var loss = CrossEntropyLoss(pred, y);

                var gradients = g.gradient(loss, neural_net.trainable_variables);

                // Update W and b following gradients
                optimizer.apply_gradients(
                    zip(gradients, neural_net.trainable_variables.Select(i => i as ResourceVariable)));
            }

            // Run training for the given number of steps.
            foreach (var (step, (batch_x, batch_y)) in enumerate(TrainData, 1))
            {
                // Run the optimization to update W and b values.
                RunOptimization(batch_x, batch_y);

                if (step % DisplayStep != 0)
                    continue;

                var pred = neural_net.Apply(batch_x, is_training: true);
                var loss = CrossEntropyLoss(pred, batch_y);
                var acc = Accuracy(pred, batch_y);

                Console.WriteLine($"step: {step}, loss: {(float) loss}, accuracy: {(float) acc}");
            }

            // Test model on validation set.
            {
                var pred = neural_net.Apply(XTest, is_training: false);
                accuracy = (float) Accuracy(pred, YTest);
                Console.WriteLine($"Test Accuracy: {this.accuracy}");
            }

            Console.WriteLine($"Model accuracy: {accuracy}");
        }
    }
}