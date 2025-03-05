using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Fx : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.Singleton.StartBlinkEffect();
    }
}
