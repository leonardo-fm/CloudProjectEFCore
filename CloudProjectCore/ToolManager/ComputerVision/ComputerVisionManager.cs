using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.ComputerVision
{
    class ComputerVisionManager
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

        public async Task<List<string>> GetTagsFromImage(Stream photo, double computerVisionConfidence = 0.5)
        {
            if (computerVisionConfidence > 1 || computerVisionConfidence < 0)
                computerVisionConfidence = 0.5;

            var imageAnalysisAsync = await _computerVisionService.AnalyzeImageInStreamAsync(photo, _features);
            return DisplayResults(imageAnalysisAsync, computerVisionConfidence);
        }

        private List<string> DisplayResults(ImageAnalysis analysis, double computerVisionConfidence)
        {
            List<string> resoult = new List<string>();

            int numberOfTags = analysis.Tags.Count;

            for (int i = 0; i < numberOfTags; i++)
                if (analysis.Tags[i].Confidence > computerVisionConfidence)
                    resoult.Add(analysis.Tags[i].Name.ToLower());

            return resoult;
        }
        private ComputerVisionClient SetupComputerVision()
        {
            var computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptionKey));
            computerVision.Endpoint = _endpoint;

            return computerVision;
        }
    }
}
