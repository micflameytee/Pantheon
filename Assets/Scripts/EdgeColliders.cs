using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliders : MonoBehaviour
{

   // Variables referencing two edge colliders
   public EdgeCollider2D LeftWall;
   public EdgeCollider2D RightWall;
   public EdgeCollider2D TopWall;
   public EdgeCollider2D BottomWall;

   // Use this for initialization
   void Start () {
      // Get the width and height of the camera (in pixels)
      float screenW = Camera.main.pixelWidth;
      float screenH = Camera.main.pixelHeight;
      
      // Create an array consisting of two Vector2 objects
      Vector2[] edgePoints = new Vector2[2];
      
      // Convert screen coordinates point (0,0) to world coordinates
      Vector3 leftBottom  = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));      
      // Convert screen coordinates point (0,H) to world coordinates
      Vector3 leftTop = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenH, 0f));      
      
      // Set the two points in the array to screen left bottom
      // and screen left top points            
      edgePoints[0] = leftBottom;
      edgePoints[1] = leftTop;
      
      // Position the left wall edge collider
      // at the left edge of the camera
      LeftWall.points = edgePoints;
      
      
      
      
      // Allocate a new array consisting of two Vector2 objects
      edgePoints = new Vector2[2];

      // Convert screen coordinates point (W,0) to world coordinates
      Vector3 rightBottom = Camera.main.ScreenToWorldPoint(new Vector3(screenW, 0f, 0f));      
      // Convert screen coordinates point (W,H) to world coordinates
      Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(screenW, screenH, 0f));      
      
      // Set the two points in the array to screen right bottom
      // and screen right top points            
      edgePoints[0] = rightBottom;
      edgePoints[1] = rightTop;
      
      // Position the left wall edge collider
      // at the left edge of the camera
      RightWall.points = edgePoints;
      
      
      
      
      
      
      
      // Allocate a new array consisting of two Vector2 objects
      edgePoints = new Vector2[2];

      // Convert screen coordinates point (W,0) to world coordinates
      Vector3 TopLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenH, 0f));      
      // Convert screen coordinates point (W,H) to world coordinates
      Vector3 TopRight = Camera.main.ScreenToWorldPoint(new Vector3(screenW, screenH, 0f));      
      
      // Set the two points in the array to screen right bottom
      // and screen right top points            
      edgePoints[0] = TopLeft;
      edgePoints[1] = TopRight;
      
      // Position the left wall edge collider
      // at the left edge of the camera
      TopWall.points = edgePoints;
      
      
      
      
      
      
      
      
      // Allocate a new array consisting of two Vector2 objects
      edgePoints = new Vector2[2];

      // Convert screen coordinates point (W,0) to world coordinates
      Vector3 BottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));      
      // Convert screen coordinates point (W,H) to world coordinates
      Vector3 BottomRight = Camera.main.ScreenToWorldPoint(new Vector3(screenW, 0f, 0f));      
      
      // Set the two points in the array to screen right bottom
      // and screen right top points            
      edgePoints[0] = BottomLeft;
      edgePoints[1] = BottomRight;
      
      // Position the left wall edge collider
      // at the left edge of the camera
      BottomWall.points = edgePoints;
   }
}
