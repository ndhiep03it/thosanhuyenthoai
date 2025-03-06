using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeChicken : MonoBehaviour
{
    public SpriteRenderer[] ListHideHome;
    public SpriteRenderer[] ListActiveHome;
    public GameObject gruondBlack;
    public GameObject DogObj;





    private void Start()
    {
        if(GameManager.Singleton.dog == 1)
        {
            DogObj.SetActive(true);
        }
        else
        {
            DogObj.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(SpriteRenderer renderer in ListHideHome)
            {
                renderer.enabled = false;
            }
            foreach (SpriteRenderer rendererActive in ListActiveHome)
            {
                rendererActive.enabled = true;
            }
            gruondBlack.SetActive(true);
            PlayerController.Singleton.SetCamera(3.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (SpriteRenderer renderer in ListHideHome)
            {
                renderer.enabled = true;
            }
            foreach (SpriteRenderer rendererActive in ListActiveHome)
            {
                rendererActive.enabled = false;
            }
            gruondBlack.SetActive(false);
            PlayerController.Singleton.DefaultCamera();


        }
    }
}
