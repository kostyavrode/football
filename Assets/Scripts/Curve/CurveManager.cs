using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveManager : MonoBehaviour
{
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private CurveCalculation curveCalculation;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //drawManager.Draw();
            curveCalculation.CollectMousePos();
        }
        if(Input.GetMouseButtonUp(0))
        {
            curveCalculation.CallKick();
            Debug.Log("GO");
        }
    }
}
