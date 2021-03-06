using System.Text.Json;
using System.Threading.Tasks; 
using System.IO; 
using System.Collections;
using System.Collections.Generic;


namespace Mathieu.Degand.FeatureMatching.Console;

class Program
{
    public static async Task Main(string[] args)
    {

        var imageObjectPath = args[0];
        var objectImageData = await 
            File.ReadAllBytesAsync(imageObjectPath); 

        var scenesDirectory = args[1];
        var imageScenesData = new List<byte[]>(); 
        foreach (var imagePath in 
                 Directory.EnumerateFiles(scenesDirectory)) 
        { 
            var imageBytes = await File.ReadAllBytesAsync(imagePath); 
            imageScenesData.Add(imageBytes); 
        } 

        var tasks = new
            ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData).Result;

        foreach (var task in tasks)
        {
            System.Console.WriteLine(JsonSerializer.Serialize(task.Points));
        }
        
    }
    
    
}