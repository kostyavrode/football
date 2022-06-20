using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleCollisionChecker : MonoBehaviour
{
    public static Action onBallCollision;
    private MovingObstacle movingObstacle;
    private void Start()
    {
        movingObstacle = GetComponentInParent<MovingObstacle>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            movingObstacle.DoRotation();
            onBallCollision?.Invoke();
        }
    }
}
