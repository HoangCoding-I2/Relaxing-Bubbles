using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float scaleSizeValue;      
    [SerializeField] private float scaleChangeSpeedX;   
    [SerializeField] private float scaleChangeSpeedY;   

    [Space(10)]
    [SerializeField] private float sideMoveDistance;        
    [SerializeField] private float sideMoveSpeed;           

    [Space(10)]
    [SerializeField] private float upDownMoveDistance;      
    [SerializeField] private float upDownMoveSpeed;         
    
    private float angleX = 0;
    private float angleY = 0;
    
    private float originalScaleX;
    private float originalScaleY;
    private Vector2 originalPosition;

    private float sideMovementAngle = 0;
    private float upDownMovementAngle = 0;

    void Start()
    {
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;
        
        originalPosition = transform.position;
    }

    void Update()
    {
        if (scaleChangeSpeedX != 0 || scaleChangeSpeedY != 0)
        {
            ScaleChange();
        }

        if (sideMoveSpeed != 0)
        {
            SideToSidePositionChange();
        }

        if (upDownMoveDistance != 0)
        {
            UpDownPositionChange();
        }
    }

    void ScaleChange()
    {
        float newScaleX = originalScaleX + Mathf.Sin(angleX) * scaleSizeValue;
        float newScaleY = originalScaleY + Mathf.Sin(angleY) * scaleSizeValue;

        
        transform.localScale = new Vector2(newScaleX, newScaleY);
        
        angleX += Time.deltaTime * scaleChangeSpeedX;
        angleY += Time.deltaTime * scaleChangeSpeedY;
    }

    void SideToSidePositionChange()
    {
        Vector2 pos = transform.position;
        pos.x = originalPosition.x + Mathf.Sin(sideMovementAngle) * sideMoveDistance;
        transform.position = pos;
        sideMovementAngle += Time.deltaTime * sideMoveSpeed;
    }

    void UpDownPositionChange()
    {
        Vector2 pos = transform.position;
        pos.y = originalPosition.y + Mathf.Sin(upDownMovementAngle) * upDownMoveDistance;
        transform.position = pos;
        upDownMovementAngle += Time.deltaTime * upDownMoveSpeed;
    }
}
