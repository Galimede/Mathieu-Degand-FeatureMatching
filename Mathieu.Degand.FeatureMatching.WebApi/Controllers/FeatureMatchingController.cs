using Microsoft.AspNetCore.Mvc;

namespace Mathieu.Degand.FeatureMatching.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FeatureMatchingController : ControllerBase
{
    private readonly ObjectDetection _objectDetection; 
 
    public FeatureMatchingController(ObjectDetection objectDetection) 
    { 
        _objectDetection = objectDetection; 
    } 
 
    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> 
        files) 
    { 
        if(files.Count != 2) 
            return BadRequest(); 
         
        using var objectSourceStream = files[0].OpenReadStream(); 
        using var objectMemoryStream = new MemoryStream(); 
        objectSourceStream.CopyTo(objectMemoryStream); 
        var imageObjectData = objectMemoryStream.ToArray(); 
         
        using var sceneSourceStream = files[1].OpenReadStream(); 
        using var sceneMemoryStream = new MemoryStream(); 
        sceneSourceStream.CopyTo(sceneMemoryStream); 
        var imageSceneData = sceneMemoryStream.ToArray(); 
        

        var result = this._objectDetection.DetectObjectInScenesSync(imageObjectData, imageSceneData);

        return File(result.ImageData, "image/jpg");
    }
}
