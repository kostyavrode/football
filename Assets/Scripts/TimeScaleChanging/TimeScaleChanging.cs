using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimeScaleChanging : MonoBehaviour
{
    private void OnEnable()
    {
        Gate.onScoreGoal += DecreaseTime;
    }
    private void OnDisable()
    {
        Gate.onScoreGoal -= DecreaseTime;
    }
    private void DecreaseTime()
    {
        //Time.timeScale = 0.5f;
        //bool isEffectEnd = false;
        //Observable.EveryFixedUpdate().TakeUntilDisable(this).TakeWhile(x => !isEffectEnd).Subscribe(x =>
        //  {
        //      if (Time.timeScale != 1&&Time.timeScale<=1)
        //          Time.timeScale += 0.05f;
        //      else
        //          isEffectEnd = true;
        //  });
    }
}
