using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms.Text;

namespace IntelliMood.Services.Implementations
{
    public class MyEmotionGetter : IEmotionGetter
    {
        public class SentimentData
        {
            public float Sentiment { get; set; }

            public string SentimentText { get; set; }
        }

        public class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }

            [ColumnName("Probability")]
            public float Probability { get; set; }

            [ColumnName("Score")]
            public float Score { get; set; }
        }

        private static readonly string TrainDataPath = Path.Combine(Environment.CurrentDirectory, "../IntelliMood.Services/Datasets", "train_data.csv");
        private static readonly string TestDataPath = Path.Combine(Environment.CurrentDirectory, "", "wikipedia-detox-250-line-test.tsv.txt");
        private static readonly string ModelPath = Path.Combine(Environment.CurrentDirectory, "", "Model.zip");
        private static TextLoader textLoader;

        public MyEmotionGetter()
        {
            var mlContext = new MLContext();

            textLoader = mlContext.Data.CreateTextLoader(separatorChar: '\t', hasHeader: true, columns: new[]
            {
                new TextLoader.Column("Label", DataKind.Boolean, 0),
                new TextLoader.Column("SentimentText", DataKind.String, 1)
            });

            var model = Train(mlContext, TrainDataPath);
            //var model = this.LoadFromFile(mlContext);

            var predictionFunction = model.CreatePredictionEngine<SentimentData, SentimentPrediction>(mlContext);

            while (true)
            {
                var text = Console.ReadLine();

                Console.WriteLine(predictionFunction.Predict(new SentimentData()
                {
                    SentimentText = text
                }).Prediction ? "Positive" : "Negative");
            }

            //Evaluate(mlContext, model);
        }

        private ITransformer LoadFromFile(MLContext mlContext)
        {
            using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var loadedModel = mlContext.Model.Load(stream);

                return loadedModel;
            }
        }

        public static ITransformer Train(MLContext mlContext, string dataPath)
        {
            var trainData = textLoader.Load(dataPath);

            var pipeline = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: "SentimentText")
                //.Append(mlContext.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeaves: 20));
                .Append(mlContext.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeaves: 20));

            Console.WriteLine("=============== Create and Train the Model ===============");
            var model = pipeline.Fit(trainData);
            Console.WriteLine("=============== End of training ===============");
            Console.WriteLine();

            SaveModelAsFile(mlContext, model);
            return model;
        }

        private static void SaveModelAsFile(MLContext mlContext, ITransformer model)
        {
            using (var fileStream = new FileStream(ModelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                mlContext.Model.Save(model, fileStream);

            Console.WriteLine("The model is saved to {0}", ModelPath);
        }

        private static void Evaluate(MLContext mlContext, ITransformer model)
        {
            var testData = textLoader.Load(TestDataPath);

            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");
            var predictions = model.Transform(testData);

            var metrics = mlContext.BinaryClassification.Evaluate(predictions, "Label");

            Console.WriteLine();
            Console.WriteLine("Model quality metrics evaluation");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.Auc:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("=============== End of model evaluation ===============");

            SaveModelAsFile(mlContext, model);
        }

        public string GetEmotionFromText(string text)
        {
            throw new NotImplementedException();
        }
    }
}
