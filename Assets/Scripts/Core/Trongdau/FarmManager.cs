using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public static FarmManager singleton;
    public GameObject EggesTarget;
    public GameObject Panel_Dau;
    public GameObject Panel_TRUNG;
    public Text txtTitlepointegges;
    public Text txtCountEgges;
    public Transform trdau;
    public int quantity_egges = 0;



    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
           
        }
        else
        {
            
        }
    }


    public void Thuhoachtrung()
    {
        if (EggesTarget != null)
        {
           Egges egges =  EggesTarget.GetComponent<Egges>();
            if (quantity_egges > 0)
            {
                PlayerController.Singleton.AnimThuHoachtrung();
                string itemName = null;
                itemName = "EGGES";
                Item item = Resources.Load<Item>("Items/" + itemName);
                Inventory.Singleton.BuyItem(item, quantity_egges, "Được lấy từ ổ gà.", 0, 100);
                Thongbao.Singleton.ShowThongbao($"Bạn đã thu hoạch Trứng gà số lượng {quantity_egges}!");
                egges.quantity_egges = 0;
                egges.SaveEgges();
                egges.LoadEggesData();
                Panel_TRUNG.SetActive(false);
            }
            else
            {
                Thongbao.Singleton.ShowThongbao($"Ôi của bạn không đủ trứng.Hiện tại {quantity_egges} quả!");
                Panel_TRUNG.SetActive(false);

            }
        }
        
    }

    public void SetTarget(GameObject target)
    {
        if (EggesTarget != null)
        {
            EggesTarget.GetComponent<Egges>();
            
        }

        EggesTarget = target;
        
    }
    public void ActivePanelDau()
    {
        Panel_Dau.SetActive(true);
    }

    public void ClearPanelDau()
    {
        Panel_Dau.SetActive(false);
        Destroy(trdau.gameObject);
    }
}
