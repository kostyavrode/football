using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private GameObject manipulable;
    private Sequence sequence;
    private bool canRotate = true;
    private void Start()
    {
        manipulable.transform.DOLocalMoveX(3.3f, 4).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    public void DoRotation()
    {
        if (canRotate)
        {
            canRotate = false;
            sequence = DOTween.Sequence();
            sequence.Append(manipulable.transform.DOLocalRotate(new Vector3(60f, 0f, 0f), 0.3f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine));
            sequence.Append(manipulable.transform.DOLocalRotate(new Vector3(40f, 0f, 0f), 0.3f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine));
            sequence.Append(manipulable.transform.DOLocalRotate(new Vector3(20f, 0f, 0f), 0.3f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine));
            Observable.Timer(System.TimeSpan.FromSeconds(2f)).TakeUntilDisable(gameObject).Subscribe(x => canRotate = true);
        }
    }
}