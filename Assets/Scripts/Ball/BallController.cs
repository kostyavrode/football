using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BallController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float sensevityX;
    [SerializeField] private float offsetY;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody ballRigidbody;

    private Vector3 currentForce;
    private Vector3 ballStartPos;
    private IDisposable ballMover;
    private float percent;

    private void OnEnable()
    {
        ObstacleCollisionChecker.onBallCollision += StopCurveFlight;
        Gate.onScoreGoal += BackBallToStart;
        ballStartPos = GameObject.FindGameObjectWithTag("BallStartPos").transform.position;
    }
    private void OnDisable()
    {
        ObstacleCollisionChecker.onBallCollision -= StopCurveFlight;
        Gate.onScoreGoal -= BackBallToStart;
    }
    public void BallMove(float centerDirection,Vector3 endPos)
    {
        Vector3 startPos = ballTransform.position;
        Vector3 centerLine = (startPos + endPos) / 2;
        try
        {
            centerPoint.position =
                new Vector3(centerLine.x + centerDirection * sensevityX, centerLine.y + offsetY, centerPoint.position.z);
        }
        catch
        {
            centerPoint.position =
                new Vector3(0, centerLine.y + offsetY, centerPoint.position.z);
        }
        percent = 0;
        ballMover = Observable.EveryFixedUpdate().TakeUntilDisable(gameObject).Subscribe(_ =>
        {
            percent += 1 / speed * Time.deltaTime;
            ballTransform.position = Bezier(startPos, centerPoint.position, endPos, percent);
        });
    }
    private void StopCurveFlight()
    {
        ballMover?.Dispose();
        currentForce = transform.forward * 20f;
        ballRigidbody.AddForce(currentForce, ForceMode.Impulse);
    }
    private void BackBallToStart()
    {
        ballMover?.Dispose();
        ballRigidbody.isKinematic = true;
        ballTransform.position = ballStartPos;
        LevelSpawner.NiceScaleChanging(ballRigidbody.gameObject);
        ballRigidbody.isKinematic = false;
    }
    private Vector3 Bezier(Vector3 start, Vector3 center, Vector3 end, float percent)
    {
        float temp = (1 - percent);
        Vector3 startCentre = temp * start + percent * center;
        Vector3 centerEnd = temp * center + percent * end;
        Vector3 result = temp * startCentre + percent * centerEnd;
        return result;
    }
}
