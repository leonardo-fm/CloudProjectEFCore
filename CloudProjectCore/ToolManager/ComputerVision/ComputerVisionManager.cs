using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ToolManager.ComputerVision
{
    public class ComputerVisionManager : IDisposable
    {
        private readonly List<VisualFeatureTypes> _features;
        private readonly string _endpoint;
        private readonly string _subscriptionKey;

        private ComputerVisionClient _computerVisionService;
        
        public ComputerVisionManager(string endpoint, string subscriptionKey, List<VisualFeatureTypes> features)
        {
            _endpoint = endpoint;
            _subscriptionKey = subscriptionKey;
            _features = features;

            _computerVisionService = SetupComputerVision();
        }

        public async Task<ComputerVisionModelData> GetDataFromImageAsync(Stream photo, double computerVisionConfidence = 0.5)
        {
            if (computerVisionConfidence > 1 || computerVisionConfidence < 0)
                computerVisionConfidence = 0.5;

            try
            {
                var imageAnalysisAsync = await _computerVisionService.AnalyzeImageInStreamAsync(photo, _features);
                return DisplayResults(imageAnalysisAsync, computerVisionConfidence);
            }
            catch (Exception)
            {
                return new ComputerVisionModelData() { Description = "", Tags = new List<string>() };
            }
        }

        private ComputerVisionModelData DisplayResults(ImageAnalysis analysis, double computerVisionConfidence)
        {
            List<string> tags = new List<string>();

            int numberOfTags = analysis.Tags.Count;

            string imageDescription = "";
            if (analysis.Description.Captions.Count != 0)
                imageDescription = analysis.Description.Captions[0].Text;

            for (int i = 0; i < numberOfTags; i++)
                if (analysis.Tags[i].Confidence > computerVisionConfidence)
                    tags.Add(analysis.Tags[i].Name.ToLower());

            return new ComputerVisionModelData() { Description = imageDescription, Tags = tags };
        }
        private ComputerVisionClient SetupComputerVision()
        {
            var computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptionKey));
            computerVision.Endpoint = _endpoint;

            return computerVision;
        }

        public void Dispose()
        {
            _computerVisionService = null;
        }
    }
}
