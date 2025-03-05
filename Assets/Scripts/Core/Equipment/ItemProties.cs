using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemProties : MonoBehaviour
{
    public static ItemProties Singleton;
    public string idItem;
    public Text txtNameItem;
    public Text txtMotaItem;
    public Text txtMotaItem2;
    public Text txtNgaytao;
    public Image IconItem;
    public GameObject PANEL_TT;
    public GameObject TextMota;
    public GameObject BTN_SUDUNG;
    public GameObject BTN_MAC;


    private void Awake()
    {
        if (Singleton == null) // kiểm tra xem đã tồn tại chưa,nếu chưa
        {
            Singleton = this;

        }
        else { }

    }

    private void OnDisable()
    {
        PANEL_TT.SetActive(false);
        TextMota.SetActive(false);
        

    }
}
