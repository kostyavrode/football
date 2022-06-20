using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class CurveCalculation : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private GameObject targetIconPrefab;
    [SerializeField] private LayerMask KickZone;
    public RaycastHit hit;
    private List<Vector3> positionsForCurve=new List<Vector3>();
    private IDisposable mousePosCollector;
    public void CollectMousePos()
    {
            if (Input.GetMouseButtonDown(0))
            {
                positionsForCurve.Clear();
                mousePosCollector = Observable.Interval(System.TimeSpan.FromSeconds(0.05f)).TakeUntilDisable(gameObject).Subscribe(x =>
                  {
                      positionsForCurve.Add(Input.mousePosition);
                  });
            }
    }
    private float CalculateCurve()
    {

        if (positionsForCurve.Count < 1)
            return 0;
        else
        {
            int center = (positionsForCurve.Count - 1) / 2;
            Vector3 startMousePos = positionsForCurve[0];
            Vector3 endMousePos = positionsForCurve[positionsForCurve.Count - 1];
            float a = Vector3.Distance(endMousePos, startMousePos);
            float b = Vector3.Distance(positionsForCurve[center], startMousePos);
            float c = Vector3.Distance(endMousePos, positionsForCurve[center]);
            float perimeter = (a + b + c) / 2;
            float height = 2 / a * Mathf.Sqrt(perimeter * (perimeter - a) * (perimeter - b) * (perimeter - c));
            float temp = (startMousePos.x - positionsForCurve[center].x);
            float side = (temp == 0) ? 0 : temp / Mathf.Abs(temp);
            float direction = height * -side;
            return direction;
        }
    }
    public void CallKick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, KickZone))
        {
            ballController.BallMove(CalculateCurve(), hit.point);
        }
            //GameObject target = Instantiate(targetIconPrefab, hit.point, Quaternion.identity, transform);
    }
}
