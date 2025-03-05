using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4Fx : MonoBehaviour
{
    public GameObject Skill4Fxobj;
    private void OnEnable()
    {

        Instantiate(Skill4Fxobj, transform.position, Quaternion.identity);
    }
}
