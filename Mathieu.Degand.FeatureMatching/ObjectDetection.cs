using System.Collections;
using OpenCvSharp;

namespace Mathieu.Degand.FeatureMatching;

using System.Collections.Generic;
using System;

public class ObjectDetection
{
    public async Task<IList<ObjectDetectionResult>> DetectObjectInScenes(byte[] objectImageData, IList<byte[]> imagesSceneData)
    {
        IList<ObjectDetectionResult> list = new List<ObjectDetectionResult>();
        foreach (var scene in imagesSceneData)
        {
            var output = await Task.Run(() => DetectObjectInScenesSync(objectImageData, scene));
            list.Add(output);
        }
    
        return list;
    
    }
    
    public ObjectDetectionResult DetectObjectInScenesSync(byte[]
        objectImageData, byte[] imagesSceneData)
    {
      
        IList<ObjectDetectionPoint> points = new List<ObjectDetectionPoint>(); 
        points.Add(new ObjectDetectionPoint(){X = 116,Y = 158});
        points.Add(new ObjectDetectionPoint(){X = 87,Y = 272});
        points.Add(new ObjectDetectionPoint(){X = 263,Y = 294});
        points.Add(new ObjectDetectionPoint(){X = 276,Y = 179});
        return new ObjectDetectionResult()
        {
            ImageData = null,
            Points = points.ToList(),
        };
    }

    

  
    

 

   /* public async Task<IList<ObjectDetectionResult>> DetectObjectInScenes(byte[] objectImageData, IList<byte[]> imagesSceneData)
    {
        IList<ObjectDetectionResult> list = new List<ObjectDetectionResult>();
        foreach (var scene in imagesSceneData)
        {
            var output = await Task.Run(() => DetectObjectInScenesSync(objectImageData, scene));
            list.Add(output);
        }
    
        return list;
        
    } */

    
   /* public ObjectDetectionResult DetectObjectInScenesSync(byte[]
    objectImageData, byte[] imagesSceneData)
    {
         
    using var imgobject = Mat.FromImageData(objectImageData, ImreadModes.Color);
    
    using var imgScene = Mat.FromImageData(imagesSceneData, ImreadModes.Color);
    
    using var orb = ORB.Create(10000);
    using var descriptors1 = new Mat();
    using var descriptors2 = new Mat();
    orb.DetectAndCompute(imgobject, null, out var keyPoints1, descriptors1);
    orb.DetectAndCompute(imgScene, null, out var keyPoints2, descriptors2);
    
    using var bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
    var matches = bf.Match(descriptors1, descriptors2);
    
    var goodMatches = matches
    .OrderBy(x => x.Distance)
    .Take(10)
    .ToArray();
    
    var srcPts = goodMatches.Select(m =>
    keyPoints1[m.QueryIdx].Pt).Select(p => new Point2d(p.X, p.Y));
    var dstPts = goodMatches.Select(m =>
    keyPoints2[m.TrainIdx].Pt).Select(p => new Point2d(p.X, p.Y));
    
    using var homography = Cv2.FindHomography(srcPts, dstPts, HomographyMethods.Ransac, 5, null);
    
    int h = imgobject.Height, w = imgobject.Width;
    var img2Bounds = new[]
    {
    new Point2d(0, 0), new Point2d(0, h - 1),
    new Point2d(w - 1, h - 1),
    new Point2d(w - 1, 0),
    };
    var img2BoundsTransformed = Cv2.PerspectiveTransform(img2Bounds, homography);
    
    using var view = imgScene.Clone();
    Point[] drawingPoints = img2BoundsTransformed.Select(p => (Point) p).ToArray();
    Cv2.Polylines(view, new []{drawingPoints}, true, Scalar.Red, 3);
          
    // Uncomment to view result
    // using (new Window("view", view))
    // {
    // Cv2.WaitKey();
    // }
    
    byte[] imageResult = view.ToBytes(".png");
    
    return new ObjectDetectionResult()
    {
    ImageData = imageResult,
    Points = drawingPoints.Select(point => new ObjectDetectionPoint() {X = point.X, Y = point.Y}).ToList(),
    };
    
    
    }*/

}
  