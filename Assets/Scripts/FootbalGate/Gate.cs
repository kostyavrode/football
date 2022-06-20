using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gate : MonoBehaviour
{
    public static Action onScoreGoal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            onScoreGoal?.Invoke();
    }
}
