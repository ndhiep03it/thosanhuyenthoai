using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thuxaphu : MonoBehaviour
{
    public GameObject ThuxathuObj;
    public GameObject player;
    public GameObject playerThuxathu;
    [SerializeField] private float timePlayer;




    private void Update()
    {
        if (GameManager.Singleton.thuxathu == 1)
        {
            player = FindObjectOfType<PlayerController>().gameObject;
            if (player != null)
            {
                playerThuxathu.SetActive(false);
                player.SetActive(true);
                ThuxathuObj.SetActive(false);
                //StartCoroutine(ActivePlayer());
            }
        }
        else
        {
            
        }
    }
    void Start()
    {
        if (GameManager.Singleton.thuxathu == 1)
        {
            player = FindObjectOfType<Player>().gameObject;
            if(player!= null)
            {
                playerThuxathu.SetActive(false);
                player.SetActive(true);
                ThuxathuObj.SetActive(false);
                //StartCoroutine(ActivePlayer());
            }
        }
        else
        {
            player = FindObjectOfType<Player>().gameObject;
            if (player != null)
            {
                StartCoroutine(HidePlayer());
            }
        }
    }
    
    IEnumerator ActivePlayer()
    {
        yield return new WaitForSeconds(timePlayer);
        player.SetActive(true);
        StartCoroutine(HidePlayer1());
    }
    IEnumerator HidePlayer()
    {
        yield return new WaitForSeconds(timePlayer);
        playerThuxathu.SetActive(false);
        player.SetActive(true);
        ThuxathuObj.SetActive(false);
        GameManager.Singleton.intro = 1;
        GameManager.Singleton.thuxathu = 1;
        GameManager.Singleton.hp = 100;
        GameManager.Singleton.level = 1;
        GameManager.Singleton.dame = 10;
        GameManager.Singleton.SaveData();
        StartCoroutine(HidePlayer1());
    }
    IEnumerator HidePlayer1()
    {
        yield return new WaitForSeconds(3f);
        playerThuxathu.SetActive(false);
       
        
    }

}
