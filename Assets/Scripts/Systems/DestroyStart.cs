using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStart : MonoBehaviour
{
    public float timerDestroy;

    void Start()
    {
        Destroy(gameObject, timerDestroy);
    }

}
