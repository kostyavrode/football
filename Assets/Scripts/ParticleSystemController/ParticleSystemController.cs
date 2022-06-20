using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private GameObject goalEffect;
    private void OnEnable()
    {
        Gate.onScoreGoal += PlayGoalEffect;
    }
    private void OnDisable()
    {
        Gate.onScoreGoal -= PlayGoalEffect;
    }
    private void PlayGoalEffect()
    {
        Instantiate(goalEffect, GameObject.FindGameObjectWithTag("Ball").transform.position, Quaternion.identity, gameObject.transform)
            .GetComponent<ParticleSystem>().Play();
    }
}
