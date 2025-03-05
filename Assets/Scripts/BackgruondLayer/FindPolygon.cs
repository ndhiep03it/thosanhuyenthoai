using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPolygon : MonoBehaviour
{
    public static FindPolygon Singleton;
    public PolygonCollider2D polygonCollider2;



    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
           
        }
        
    }

    private void Start()
    {
        polygonCollider2 = GetComponent<PolygonCollider2D>();
    }
}
