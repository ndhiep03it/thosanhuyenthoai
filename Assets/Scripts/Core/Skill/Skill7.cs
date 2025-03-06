using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill7 : MonoBehaviour
{
    public SpriteRenderer[] spriteRendererTanhinh;
    public GameObject player;
    private void OnEnable()
    {
       foreach(SpriteRenderer renderer in spriteRendererTanhinh)
       {
            renderer.enabled = false;
       }
        player.gameObject.tag = "Untagged";
    }


    private void OnDisable()
    {
        foreach (SpriteRenderer renderer in spriteRendererTanhinh)
        {
            renderer.enabled = true;
        }
        player.gameObject.tag = "Player";

    }
}
