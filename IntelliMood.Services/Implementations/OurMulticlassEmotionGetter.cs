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
    public class OurMulticlassEmotionGetter : IEmotionGetter
    {
        public class SentimentData
        {
            public string Sentiment { get; set; }

            public string Content { get; set; }
        }

        public class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]
            public string Sentiment;
        }

        private static readonly string TrainDataPath = Path.Combine(Environment.CurrentDirectory, @"..\IntelliMood.Services\Datasets", "text_emotion.csv");
        private static readonly string ModelPath = Path.Combine(Environment.CurrentDirectory, "", "SentimentModel.zip");
        private static TextLoader textLoader;
        private static MLContext mlContext;
        private static PredictionEngine<SentimentData, SentimentPrediction> predictionFunction;

        public OurMulticlassEmotionGetter()
        {
            mlContext = new MLContext();

            textLoader = mlContext.Data.CreateTextLoader(separatorChar: '\t', hasHeader: true, columns: new[]
            {
                new TextLoader.Column("Sentiment", DataKind.String, 1),
                new TextLoader.Column("Content", DataKind.String, 3)
            });

            //var model = Train(mlContext, TrainDataPath);
            var model = this.LoadFromFile();

            predictionFunction = model.CreatePredictionEngine<SentimentData, SentimentPrediction>(mlContext);

            //Evaluate(mlContext, model);
        }

        private ITransformer LoadFromFile()
        {
            try
            {
                using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var loadedModel = mlContext.Model.Load(stream);

                    return loadedModel;
                }
            }
            catch (FileNotFoundException)
            {
                return Train(mlContext, TrainDataPath);
            }
        }

        public static ITransformer Train(MLContext mlContext, string dataPath)
        {
            var trainData = textLoader.Load(dataPath);

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Sentiment", outputColumnName: "Label")
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Content", outputColumnName: "ContentFeaturized"))
                .Append(mlContext.Transforms.Concatenate("Features", "ContentFeaturized"))
                .Append(mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(DefaultColumnNames.Label, DefaultColumnNames.Features))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

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

        public string GetEmotionFromText(string text)
        {
            var prediction = predictionFunction.Predict(new SentimentData()
            {
                Content = text
            }).Sentiment;

            var firstChar = prediction[0];
            prediction = $"{char.ToUpper(firstChar)}{prediction.Substring(1)}";

            return prediction;
        }
    }
}
