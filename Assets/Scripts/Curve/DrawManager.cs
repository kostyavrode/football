using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private CurveCalculation curveCalculation;
    [SerializeField] private GameObject drawPrefab;
    [SerializeField] private float drawDistance;

    
    private GameObject trail;
    private Plane plane;
    private Vector3 startPosition;

    private void Start()
    {
        plane = new Plane(-Camera.main.transform.forward, this.transform.position);
        plane.Translate(new Vector3(0f, 4f, 0f));
    }

    private void FixedUpdate()
    {
        Draw();
        curveCalculation.CollectMousePos();
    }
    private void ChangeStartDrawPosition()
    {
        Vector3 temp = Input.mousePosition;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, drawDistance);
        temp.z = transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

    public void Draw()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            //ChangeStartDrawPosition();
            trail = Instantiate(drawPrefab, transform.position, Quaternion.identity,this.transform);
            trail.layer = 7;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(mouseRay, out float distance))
            {
                startPosition = mouseRay.GetPoint(distance);
            }
        }
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(mouseRay, out float distance))
            {
                trail.transform.position = mouseRay.GetPoint(distance);
            }
        }
         else
        {
            Destroy(trail);
        }
    }
}
