using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBetweenTwoPoints : MonoBehaviour
{
    public Transform start;
    public Transform end;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, end.position);
    }
}
