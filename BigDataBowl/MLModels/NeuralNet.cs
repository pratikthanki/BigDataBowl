using Tensorflow;
using Tensorflow.Keras.ArgsDefinition;
using Tensorflow.Keras.Engine;
using static Tensorflow.Binding;
using Activation = Tensorflow.Keras.Activation;

namespace BigDataBowl.MLModels
{
    public class NeuralNet : Model
    {
        private readonly Layer layerOne;
        private readonly Layer layerTwo;
        private readonly Layer layerThree;
        private readonly Layer layerFour;
        private readonly Layer output;

        public NeuralNet(NeuralNetArgs args) :
            base(args)
        {
            // Fully connected hidden layers 
            layerOne = Dense(args.NeuronOfHidden1, args.Activation1);
            layerTwo = Dense(args.NeuronOfHidden2, args.Activation2);
            layerThree = Dense(args.NeuronOfHidden3, args.Activation3);
            layerFour = Dense(args.NeuronOfHidden4, args.Activation4);
            output = Dense(args.NumClasses, args.ActivationOutput);
        }

        // Set forward pass.
        protected override Tensor call(Tensor inputs, bool is_training = false, Tensor state = null)
        {
            inputs = layerOne.Apply(inputs);
            inputs = layerTwo.Apply(inputs);
            inputs = layerThree.Apply(inputs);
            inputs = layerFour.Apply(inputs);
            inputs = output.Apply(inputs);

            if (!is_training)
                inputs = tf.nn.softmax(inputs);

            return inputs;
        }
    }

    public class NeuralNetArgs : ModelArgs
    {
        public int NumClasses { get; set; }
        public Activation ActivationOutput { get; set; }
        public int NeuronOfHidden1 { get; set; }
        public Activation Activation1 { get; set; }
        public int NeuronOfHidden2 { get; set; }
        public Activation Activation2 { get; set; }
        public int NeuronOfHidden3 { get; set; }
        public Activation Activation3 { get; set; }
        public int NeuronOfHidden4 { get; set; }
        public Activation Activation4 { get; set; }
    }
}