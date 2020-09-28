using Tensorflow;
using Tensorflow.Keras.ArgsDefinition;
using Tensorflow.Keras.Engine;
using static Tensorflow.Binding;
using Activation = Tensorflow.Keras.Activation;

namespace BigDataBowl.MLModels
{
    public class NeuralNet : Model
    {
        private readonly Layer fc1;
        private readonly Layer fc2;
        private readonly Layer norm;
        private readonly Layer output;

        public NeuralNet(NeuralNetArgs args) :
            base(args)
        {
            // First fully-connected hidden layer.
            fc1 = Dense(args.NeuronOfHidden1, args.Activation1);
            
            // Second fully-connected hidden layer.
            fc2 = Dense(args.NeuronOfHidden2, args.Activation2);

            output = Dense(args.NumClasses);
        }

        // Set forward pass.
        protected override Tensor call(Tensor inputs, bool is_training = false, Tensor state = null)
        {
            inputs = fc1.Apply(inputs);
            inputs = fc2.Apply(inputs);
            inputs = output.Apply(inputs);

            if (!is_training)
                inputs = tf.nn.softmax(inputs);

            return inputs;
        }
    }

    public class NeuralNetArgs : ModelArgs
    {
        public int NeuronOfHidden1 { get; set; }
        public Activation Activation1 { get; set; }

        public int NeuronOfHidden2 { get; set; }
        public Activation Activation2 { get; set; }

        public int NumClasses { get; set; }
        public Activation ActivationOutput { get; set; }
    }
}