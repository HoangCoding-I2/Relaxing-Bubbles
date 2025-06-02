using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleParent : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CalculateDistance());
    }

    private IEnumerator CalculateDistance()
    {
        while (true)
        {
            if (Player.Instance != null)
            {
                float distanceY = Player.Instance.transform.position.y - transform.position.y;
                
                if (distanceY > 60)
                {
                    Destroy(gameObject);
                    yield break;
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}