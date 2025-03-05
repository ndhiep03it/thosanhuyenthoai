using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;

public class PolygonAdd : MonoBehaviour
{
    public CinemachineConfiner2D cinemachineConfiner2D;



    private void Update()
    {
        cinemachineConfiner2D.m_BoundingShape2D = FindPolygon.Singleton.polygonCollider2;
    }
}
