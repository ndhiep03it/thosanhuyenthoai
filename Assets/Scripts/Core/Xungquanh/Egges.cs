using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Egges : MonoBehaviour
{
    public int quantity_egges = 0;

    public GameObject EggesPanel;
    private string nameEggs;
    public SpriteRenderer eggesSp;
    public Slider sliderEgges;
    public GameObject Muiten;
    private bool isPlayerInTrigger;

    private void Start()
    {
        nameEggs = "Egges_" + transform.position.x;

        LoadEggesData();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            EggesPanel.SetActive(true);
            Muiten.SetActive(true);
            isPlayerInTrigger = true; // Đánh dấu rằng người chơi đã vào vùng kích hoạt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            EggesPanel.SetActive(false);
            Muiten.SetActive(false);
            isPlayerInTrigger = false; // Đánh dấu rằng người chơi đã vào vùng kích hoạt


        }
    }
    public void ShowPanelEgges()
    {
        FarmManager.singleton.SetTarget(gameObject);
        FarmManager.singleton.Panel_TRUNG.SetActive(true);
        FarmManager.singleton.txtTitlepointegges.text = "Ổ Trứng của bạn vị trí:" + nameEggs ;
        FarmManager.singleton.txtCountEgges.text =  quantity_egges.ToString();
        FarmManager.singleton.quantity_egges =  quantity_egges;
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        
    }

    public void Thuhoachtrung()
    {
       
        if (quantity_egges > 0)
        {
            string itemName = null;
            itemName = "EGGES";
            Item item = Resources.Load<Item>("Items/" + itemName);
            Inventory.Singleton.BuyItem(item, quantity_egges, "Được lấy từ ổ gà.", 0, 100);
            Thongbao.Singleton.ShowThongbao($"Bạn đã thu hoạch Trứng gà số lượng {quantity_egges}!");
            quantity_egges = 0;
        }
        else
        {
            Thongbao.Singleton.ShowThongbao($"Ôr của bạn không đủ trứng.Hiện tại {quantity_egges} quả!");

        }
    }


    public void AddEgges()
    {
        if (quantity_egges < 101)
        {
            quantity_egges += 1;
            if (quantity_egges > 0)
            {
                eggesSp.enabled = true;

            }
            else
            {

                eggesSp.enabled = false;
            }
            SaveEgges();
            LoadEggesData();
        }
        else
        {

        }
        
    }
    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ShowPanelEgges();
        }
    }
    public void SaveEgges()
    {

        PlayerPrefs.SetInt(nameEggs,quantity_egges);
        PlayerPrefs.Save();
    }

    public void LoadEggesData()
    {
        quantity_egges = PlayerPrefs.GetInt(nameEggs);
        sliderEgges.value = quantity_egges;
        if (quantity_egges > 0)
        {
            eggesSp.enabled = true;

        }
        else
        {

            eggesSp.enabled = false;
        }
    }
}
