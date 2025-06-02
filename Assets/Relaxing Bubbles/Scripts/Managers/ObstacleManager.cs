using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] easyObstacleArray;
    [SerializeField] private GameObject[] normalObstacleArray;
    
    private int obstacleIndex = 0;
    private int easyObstacleIndex = 0;
    private int normalObstacleIndex = 0;
    private int distanceToNextObstacle = 50;
    private int playerPositionIndex = -1;

    private void Start()
    {
        InstantiateObstacle();
    }

    private void Update()
    {
        if (Player.Instance == null) return;
        
        if (playerPositionIndex != (int)Player.Instance.transform.position.y / 25)
        {
            InstantiateObstacle();
            playerPositionIndex = (int)Player.Instance.transform.position.y / 25;
        }
    }

    private void InstantiateObstacle()
    {
        int score = ScoreManager.Instance.GetScore();
        
        if (score < 20)
        {
            Vector3 spawnPosition = new Vector3(0, obstacleIndex * distanceToNextObstacle);
            
            GameObject newObstacle = Instantiate(easyObstacleArray[easyObstacleIndex], spawnPosition, Quaternion.identity);
            newObstacle.transform.SetParent(transform);
            
            easyObstacleIndex++;
            if (easyObstacleIndex >= easyObstacleArray.Length)
            {
                easyObstacleIndex = 0;
            }
        }
        else
        {
            Vector3 spawnPosition = new Vector3(0, obstacleIndex * distanceToNextObstacle);
            
            GameObject newObstacle = Instantiate(normalObstacleArray[normalObstacleIndex], spawnPosition, Quaternion.identity);
            newObstacle.transform.SetParent(transform);
            
            normalObstacleIndex++;
            if (normalObstacleIndex >= normalObstacleArray.Length)
            {
                normalObstacleIndex = 0;
            }
        }

        obstacleIndex++;
    }
}