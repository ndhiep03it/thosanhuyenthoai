using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopProfile : MonoBehaviour
{
    [SerializeField] private Sprite BoderImageDefault;
    [SerializeField] private Sprite BoderImageSelect;
    public Image IconItem;
    public Image IconItemMoney;
    public Text txtNameItemShop;
    public Text txtParama;
    public Text txtGia;
    public string description;
    public string levelyc;
    public Button buttonCheck;
    public int giamua;
    public List<Item> item = new List<Item>();
    public ItemMoney itemMoney;
    //public Item items ;

    private void Start()
    {
        IconItemMoney.sprite = item[0].IconMoney;
    }

    public void OnItemShopClicked()
    {
        
        ShopManager.Singleton.IconItemShop.sprite = IconItem.sprite;
        ShopManager.Singleton.SetTarget(gameObject);
        ShopManager.Singleton.txtNameItemShop.text = txtNameItemShop.text;
        ShopManager.Singleton.txtParama.text = txtParama.text;
        ShopManager.Singleton.txtDescriptions.text = description;
        ShopManager.Singleton.txtGia.text = "Mua vật phẩm này với giá " + txtGia.text + " " + itemMoney.ToString();
        ShopManager.Singleton.txtGiaVatpham.text = "Mua vật phẩm này với giá " + txtGia.text + " " + itemMoney.ToString();
        ShopManager.Singleton.gia = giamua;
        ShopManager.Singleton.txtleveyc.text = levelyc;
        ShopManager.Singleton.item = item.ToArray();
        ShopManager.Singleton.panelShopProfile.SetActive(true);



    }

    internal void HideArrow()
    {
        Image imageBoder = gameObject.GetComponent<Image>();
        imageBoder.sprite = BoderImageDefault;
    }

    internal void ShowArrow()
    {
        Image imageBoder = gameObject.GetComponent<Image>();
        imageBoder.sprite = BoderImageSelect;
    }
}
