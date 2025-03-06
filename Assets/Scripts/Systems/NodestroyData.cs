using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodestroyData : MonoBehaviour
{
    public static NodestroyData Singleton;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
