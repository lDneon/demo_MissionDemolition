using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{                                                            
     idle, 
     playing,
     levelEnd
}

  public class MissionDemolition : MonoBehaviour
{
      static private MissionDemolition S;                   
 
      [Header("Inscribed")]
      public TextMeshProGUI uitLevel;  
      public TextMeshProGUI uitShots;  
      public Vector3 castlePos; 
      public GameObject[] castles;   
 
      [Header("Dynamic")]
      public int level;     // The current level
      public int levelMax;  // The number of levels
      public int shotsTaken;
      public GameObject castle;    // The current castle
      public GameMode mode = GameMode.idle;
      public string showing = "Show Slingshot"; 
 
     void Start()
     {
         S = this; // Define the Singleton                                      

         level = 0;
         shotsTaken = 0;
         levelMax = castles.Length;
         StartLevel();
     }

    void StartLevel()
    {
        // Get rid of the old castle
        if (castle != null)
        {
            Destroy(castle);
        }
        // Destroy old projectiles
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // Instantiate the new castle
        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
    }
 
      void UpdateGUI()
      {
         // Show the data in the GUITexts
         uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
         uitShots.text = "Shots Taken: " + shotsTaken;
      }
 

     void Update()
     {
         UpdateGUI();

         // Check for level end
         if ((mode == GameMode.playing) && Goal.goalMet)
         {
             // Change mode to stop checking for level end
             mode = GameMode.levelEnd;

             // Start the next level in 2 seconds
             Invoke("NextLevel", 2f);                                           
         }
     }
 
     void NextLevel()
     {                                                         
          level++;
         if (level == levelMax)
         {
            level = 0;
            shotsTaken = 0;
         }
         StartLevel();
     }

  }