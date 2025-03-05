using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Globalization;
using System.Collections;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager _singleton;
    public GameObject panelMove;

    public PlayerEquipment playerEquipment; // Sử dụng PlayerEquipment để chứa các trang bị
    public int level;
    public int quantity;
    public int hp;
    public int mp;
    public int hutmau;
    public int hutki;
    public int chimang;
    public int dame;
    public float ne;
    public int solanepsao;
    public int doben;
    [Header("DATA SHOW")]
    public int hpshow;
    public int mpshow;
    public int hutmaushow;
    public int hutkishow;
    public int chimangshow;
    public int dameshow;
    public float neshow;
    public int dobenshow;


    [Header("THUỘC TÍNH TRANG BỊ MẶC")]
    public string slotNameMac; // tên slot
    public string slotNameThao; // tên slot
    public string itemParama; // Mô tả item
    public GameObject PanelPropertiesThao;
    public GameObject PanelPropertiesDamac;
    public GameObject PanelTeleport;
    public Text txtNameItem;
    public Text txtDescriptionItem;
    public Text txtParamaItem;
    public Text txtTypeItem;
    public Image IconItem;
    public Image Anhbodercap;
    public string itemType;
    public GameObject[] objStars;
    public ItemProfileUI itemProfile;
    public Item[] item; // Mảng Item
    public int vitriitem = 0;

    [Header("THUỘC TÍNH TRANG BỊ MẶC ĐÃ SỞ HỮU")]
    public Text txtNameItem2;
    public Text txtDescriptionItem2;
    public Text txtParamaItem2;
    public Text txtTypeItem2;
    public Image IconItem2;
    public Image Anhbodercap2;
    public GameObject[] objStars2;
    public EquipmentUIManager equipmentUI;
    public AnimationControllerSwitcher wing;
    public AnimationControllerSwitcher phukien;

    public static EquipmentManager Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<EquipmentManager>();
                if (_singleton == null)
                {
                    GameObject obj = new GameObject("EquipmentManager");
                    _singleton = obj.AddComponent<EquipmentManager>();
                }
            }
            return _singleton;
        }
    }

    private void Awake()
    {
        if (_singleton == null)
        {
            _singleton = this;
        }
    }
    private void Start()
    {
        // LoadEquipment(); // Lấy trang bị khi game bắt đầu
        LoadSprender();
    }
    public void LoadSprender()
    {
        // Kiểm tra wing
        string itemName = EquipmentManager.Singleton?.playerEquipment?.canh?.itemNameScriptObject;
        if (!string.IsNullOrWhiteSpace(itemName))
        {
            switch (itemName)
            {
                case "CANHTRANGVIP":
                    wing.ChangeAnimator(0);
                    break;
                case "CANHTRANHRUC":
                    wing.ChangeAnimator(1);
                    break;
                default:
                    Debug.LogWarning("Tên item không hợp lệ: " + itemName);
                    break;
            }
            wing.AnimSelectObj.SetActive(true);
        }
        else
        {
            wing.AnimSelectObj.SetActive(false);
        }

        // Kiểm tra phukien
        string itemNameBua = EquipmentManager.Singleton?.playerEquipment?.phukien?.itemNameScriptObject;
        if (!string.IsNullOrWhiteSpace(itemNameBua))
        {
            switch (itemNameBua)
            {
                case "BUATHO":
                    phukien.ChangeAnimator(0);
                    SetLocalPosition(phukien.AnimSelectObj.transform, -0.137f, -0.75f);
                    break;
                case "QUATBATIEU":
                    phukien.ChangeAnimator(1);
                    SetLocalPosition(phukien.AnimSelectObj.transform, 0.007f, -0.37f);
                    break;
                case "LONGDENHOIAN":
                    phukien.ChangeAnimator(2);
                    SetLocalPosition(phukien.AnimSelectObj.transform, 0f, -0.802f);
                    break;
                case "SENHONG":
                    phukien.ChangeAnimator(3);
                    SetLocalPosition(phukien.AnimSelectObj.transform, 0f, -0.802f);
                    break;
                case "KIEMANHSANG":
                    phukien.ChangeAnimator(4);
                    SetLocalPosition(phukien.AnimSelectObj.transform, 0f, -0.802f);
                    break;
                case "DAOHONG":
                    phukien.ChangeAnimator(5);
                    SetLocalPosition(phukien.AnimSelectObj.transform, -0.353f, -0.536f);
                    break;
                default:
                    Debug.LogWarning("Tên item không hợp lệ: " + itemNameBua);
                    break;
            }
            phukien.AnimSelectObj.SetActive(true);
        }
        else
        {
            phukien.AnimSelectObj.SetActive(false);
        }
    }

    private void SetLocalPosition(Transform obj, float x, float y)
    {
        Vector3 pos = obj.localPosition;
        pos.x = x;
        pos.y = y;
        obj.localPosition = pos;
    }
    public void ShowAo()
    {

        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.ao.itemName;
        txtParamaItem.text = playerEquipment.ao.itemParama;

        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.ao.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.ao.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.ao.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.ao.hpshow}";
        if (playerEquipment.ao.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.ao.mpshow}";
        if (playerEquipment.ao.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.ao.dameshow}";
        if (playerEquipment.ao.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.ao.chimangshow}";
        if (playerEquipment.ao.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.ao.hutkishow}";
        if (playerEquipment.ao.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.ao.hutmaushow}";
        if (playerEquipment.ao.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.ao.ne}";
        if (playerEquipment.ao.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.ao.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.ao.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.ao.description + $"\n{statsDescription}";
        txtTypeItem.text = "Loại:" + playerEquipment.ao.itemType;
        IconItem.sprite = EquipmentUIManager.Singleton.slotAoImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;
        PanelPropertiesThao.SetActive(true);

        int numberOfStars = playerEquipment.ao.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowQuan()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.quan.itemName;
        txtParamaItem.text = playerEquipment.quan.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.quan.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.quan.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.quan.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.quan.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.quan.hpshow}";
        if (playerEquipment.quan.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.quan.mpshow}";
        if (playerEquipment.quan.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.quan.dameshow}";
        if (playerEquipment.quan.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.quan.chimangshow}";
        if (playerEquipment.quan.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.quan.hutkishow}";
        if (playerEquipment.quan.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.quan.hutmaushow}";
        if (playerEquipment.quan.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.quan.neshow}";
        if (playerEquipment.quan.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.quan.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.quan.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.quan.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotQuanImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[1].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.quan.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowGang()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.gang.itemName;
        txtParamaItem.text = playerEquipment.gang.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.gang.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.gang.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.gang.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.gang.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.gang.hpshow}";
        if (playerEquipment.gang.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.gang.mpshow}";
        if (playerEquipment.gang.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.gang.dameshow}";
        if (playerEquipment.gang.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.gang.chimangshow}";
        if (playerEquipment.gang.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.gang.hutkishow}";
        if (playerEquipment.gang.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.gang.hutmaushow}";
        if (playerEquipment.gang.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.gang.neshow}";
        if (playerEquipment.gang.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.gang.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.gang.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.gang.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotGangImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[2].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.gang.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowGiay()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.giay.itemName;
        txtParamaItem.text = playerEquipment.giay.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.giay.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.giay.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.giay.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.giay.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.giay.hpshow}";
        if (playerEquipment.giay.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.giay.mpshow}";
        if (playerEquipment.giay.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.giay.dameshow}";
        if (playerEquipment.giay.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.giay.chimangshow}";
        if (playerEquipment.giay.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.giay.hutkishow}";
        if (playerEquipment.giay.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.giay.hutmaushow}";
        if (playerEquipment.giay.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.giay.neshow}";
        if (playerEquipment.giay.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.giay.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.giay.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.giay.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotGiayImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[3].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.giay.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowRada()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.rada.itemName;
        txtParamaItem.text = playerEquipment.rada.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.rada.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.rada.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.rada.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.rada.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.rada.hpshow}";
        if (playerEquipment.rada.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.rada.mpshow}";
        if (playerEquipment.rada.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.rada.dameshow}";
        if (playerEquipment.rada.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.rada.chimangshow}";
        if (playerEquipment.rada.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.rada.hutkishow}";
        if (playerEquipment.rada.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.rada.hutmaushow}";
        if (playerEquipment.rada.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.rada.neshow}";
        if (playerEquipment.rada.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.rada.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.rada.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.rada.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotRadaImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[4].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.rada.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowCanh()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.canh.itemName;
        txtParamaItem.text = playerEquipment.canh.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.canh.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.canh.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.canh.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.canh.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.canh.hpshow}";
        if (playerEquipment.canh.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.canh.mpshow}";
        if (playerEquipment.canh.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.canh.dameshow}";
        if (playerEquipment.canh.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.canh.chimangshow}";
        if (playerEquipment.canh.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.canh.hutkishow}";
        if (playerEquipment.canh.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.canh.hutmaushow}";
        if (playerEquipment.canh.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.canh.neshow}";
        if (playerEquipment.canh.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.canh.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.canh.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.canh.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotCanhImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[10].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.canh.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowDaychuyen()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.daychuyen.itemName;
        txtParamaItem.text = playerEquipment.daychuyen.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.daychuyen.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.daychuyen.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.daychuyen.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.daychuyen.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.daychuyen.hpshow}";
        if (playerEquipment.daychuyen.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.daychuyen.mpshow}";
        if (playerEquipment.daychuyen.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.daychuyen.dameshow}";
        if (playerEquipment.daychuyen.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.daychuyen.chimangshow}";
        if (playerEquipment.daychuyen.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.daychuyen.hutkishow}";
        if (playerEquipment.daychuyen.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.daychuyen.hutmaushow}";
        if (playerEquipment.daychuyen.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.daychuyen.neshow}";
        if (solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.daychuyen.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.daychuyen.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotDaychuyenmage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[5].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.daychuyen.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowNhan()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.nhan.itemName;
        txtParamaItem.text = playerEquipment.nhan.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.nhan.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.nhan.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.nhan.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.nhan.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.nhan.hpshow}";
        if (playerEquipment.nhan.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.nhan.mpshow}";
        if (playerEquipment.nhan.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.nhan.dameshow}";
        if (playerEquipment.nhan.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.nhan.chimangshow}";
        if (playerEquipment.nhan.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.nhan.hutkishow}";
        if (playerEquipment.nhan.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.nhan.hutmaushow}";
        if (playerEquipment.nhan.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.nhan.neshow}";
        if (playerEquipment.nhan.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.nhan.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.nhan.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.nhan.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotNhanImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[6].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.nhan.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowVukhi()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.vukhi.itemName;
        txtParamaItem.text = playerEquipment.vukhi.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.vukhi.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.vukhi.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.vukhi.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.vukhi.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.vukhi.hpshow}";
        if (playerEquipment.vukhi.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.vukhi.mpshow}";
        if (playerEquipment.vukhi.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.vukhi.dameshow}";
        if (playerEquipment.vukhi.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.vukhi.chimangshow}";
        if (playerEquipment.vukhi.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.vukhi.hutkishow}";
        if (playerEquipment.vukhi.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.vukhi.hutmaushow}";
        if (playerEquipment.vukhi.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.vukhi.neshow}";
        if (playerEquipment.vukhi.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.vukhi.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.vukhi.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.vukhi.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotVukhiImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[7].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.vukhi.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowPet()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.pet.itemName;
        txtParamaItem.text = playerEquipment.pet.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.pet.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.pet.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.pet.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.pet.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.pet.hpshow}";
        if (playerEquipment.pet.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.pet.mpshow}";
        if (playerEquipment.pet.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.pet.dameshow}";
        if (playerEquipment.pet.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.pet.chimangshow}";
        if (playerEquipment.pet.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.pet.hutkishow}";
        if (playerEquipment.pet.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.pet.hutmaushow}";
        if (playerEquipment.pet.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.pet.neshow}";
        if (playerEquipment.pet.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.pet.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.pet.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.pet.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotPetImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[8].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.pet.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }
    public void ShowPhukien()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Click);
        txtNameItem.text = playerEquipment.phukien.itemName;
        txtParamaItem.text = playerEquipment.phukien.itemParama;
        txtTypeItem.text = "Loại:" + playerEquipment.phukien.itemType;
        // Tạo chuỗi mô tả các chỉ số
        string statsDescription = "";
        if (playerEquipment.phukien.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.phukien.level}</color>";
        else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (playerEquipment.phukien.hpshow > 0) statsDescription += $"\nHP: +{playerEquipment.phukien.hpshow}";
        if (playerEquipment.phukien.mpshow > 0) statsDescription += $"\nMP: +{playerEquipment.phukien.mpshow}";
        if (playerEquipment.phukien.dameshow > 0) statsDescription += $"\nTấn công: +{playerEquipment.phukien.dameshow}";
        if (playerEquipment.phukien.chimangshow > 0) statsDescription += $"\nChí mạng: +{playerEquipment.phukien.chimangshow}";
        if (playerEquipment.phukien.hutkishow > 0) statsDescription += $"\nHút ki: +{playerEquipment.phukien.hutkishow}";
        if (playerEquipment.phukien.hutmaushow > 0) statsDescription += $"\nHút máu: +{playerEquipment.phukien.hutmaushow}";
        if (playerEquipment.phukien.neshow > 0) statsDescription += $"\nNé: +{playerEquipment.phukien.neshow}";
        if (playerEquipment.phukien.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{playerEquipment.phukien.solanepsao}/7";
        statsDescription += $"\nĐộ bền trang bị:{playerEquipment.phukien.dobenshow}/100";
        txtDescriptionItem.text = playerEquipment.phukien.description + $"\n{statsDescription}";
        IconItem.sprite = EquipmentUIManager.Singleton.slotPhukienImage.sprite;
        Anhbodercap.sprite = EquipmentUIManager.Singleton.BoderAnhcap[9].sprite;
        PanelPropertiesThao.SetActive(true);
        int numberOfStars = playerEquipment.phukien.stars; // Giả sử bạn có thuộc tính "stars" trong item
        // Kích hoạt sao tương ứng với số sao của item
        for (int j = 0; j < objStars.Length; j++)
        {
            objStars[j].SetActive(j < numberOfStars); // Kích hoạt số sao
        }
    }

    public void CheclSlot(string slots)
    {

        slotNameThao = slots;
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) UpdatePlayerStats();
    }
    // Lưu trang bị vào tệp
    public void SaveEquipment()
    {
        try
        {
            if (playerEquipment == null)
            {
                Debug.LogError("Dữ liệu trang bị không hợp lệ (null). Không thể lưu.");
                return;
            }

            // Tạo danh sách các trang bị
            EquipmentSaveData equipmentSaveData = new EquipmentSaveData
            {
                equippedItems = new List<EquipmentItem>
            {
                new EquipmentItem { key = "ao", value = playerEquipment.ao },
                new EquipmentItem { key = "quan", value = playerEquipment.quan },
                new EquipmentItem { key = "gang", value = playerEquipment.gang },
                new EquipmentItem { key = "giay", value = playerEquipment.giay },
                new EquipmentItem { key = "rada", value = playerEquipment.rada },
                new EquipmentItem { key = "canh", value = playerEquipment.canh },
                new EquipmentItem { key = "daychuyen", value = playerEquipment.daychuyen },
                new EquipmentItem { key = "nhan", value = playerEquipment.nhan },
                new EquipmentItem { key = "vukhi", value = playerEquipment.vukhi },
                new EquipmentItem { key = "pet", value = playerEquipment.pet },
                new EquipmentItem { key = "phukien", value = playerEquipment.phukien },
            }
            };

            // Chuyển dữ liệu thành JSON
            string equipmentJson = JsonUtility.ToJson(equipmentSaveData, true);
            //Debug.Log($"Dữ liệu JSON trước khi mã hóa: {equipmentJson}");

            // Mã hóa dữ liệu trước khi lưu
            string encryptedJson = EncryptionUtility.Encrypt(equipmentJson);

            // Lưu vào file
            string filePath = Application.persistentDataPath + "/newjson.json";
            File.WriteAllText(filePath, encryptedJson);

            //Debug.Log("Trang bị đã được lưu thành công.");
        }
        catch (Exception e)
        {
            Debug.LogError("Không thể lưu dữ liệu trang bị: " + e.Message);
        }
    }

    public void LoadEquipment()
    {
        try
        {
            string filePath = Application.persistentDataPath + "/newjson.json";
            if (!File.Exists(filePath))
            {
                Debug.LogWarning("File lưu trang bị không tồn tại.");
                return;
            }

            // Đọc dữ liệu từ file
            string encryptedJson = File.ReadAllText(filePath);

            // Giải mã dữ liệu
            string json = EncryptionUtility.Decrypt(encryptedJson);

            // Chuyển đổi từ JSON sang đối tượng
            EquipmentSaveData equipmentSaveData = JsonUtility.FromJson<EquipmentSaveData>(json);
            if (equipmentSaveData == null || equipmentSaveData.equippedItems == null)
            {
                throw new Exception("Dữ liệu trang bị không hợp lệ hoặc bị thiếu.");
            }

            // Đảm bảo playerEquipment không bị null
            if (playerEquipment == null)
            {
                playerEquipment = new PlayerEquipment();
            }

            // Phục hồi dữ liệu trang bị
            foreach (var item in equipmentSaveData.equippedItems)
            {
                switch (item.key)
                {
                    case "ao": playerEquipment.ao = item.value; break;
                    case "quan": playerEquipment.quan = item.value; break;
                    case "gang": playerEquipment.gang = item.value; break;
                    case "giay": playerEquipment.giay = item.value; break;
                    case "rada": playerEquipment.rada = item.value; break;
                    case "canh": playerEquipment.canh = item.value; break;
                    case "daychuyen": playerEquipment.daychuyen = item.value; break;
                    case "nhan": playerEquipment.nhan = item.value; break;
                    case "vukhi": playerEquipment.vukhi = item.value; break;
                    case "pet": playerEquipment.pet = item.value; break;
                    case "phukien": playerEquipment.phukien = item.value; break;
                }
            }
            StartCoroutine(LoadChiso());
            // Debug.Log("Trang bị đã được tải thành công.");
        }
        catch (Exception e)
        {
            Debug.LogError("Không thể tải dữ liệu trang bị: " + e.Message);
        }
    }
    

    private DataEquipment LoadEquipmentData(DataEquipment savedData)
    {
        if (savedData == null || string.IsNullOrEmpty(savedData.itemName))
        {
            //Debug.LogWarning("Dữ liệu trang bị không hợp lệ.");
            return null;
        }
       

        // Tải Item từ Resources hoặc cách thức phù hợp trong game của bạn
        Item loadedItem = LoadItem(savedData.itemName);

        if (loadedItem == null)
        {
            //Debug.LogWarning($"Không thể tải Item: {savedData.itemName}");
            return null;
        }

        // Tạo một DataEquipment mới và đồng bộ chỉ số từ Item
        DataEquipment equipmentData = new DataEquipment();
        equipmentData.SyncStatsFromItem(loadedItem);  // Đồng bộ stats từ item vào trang bị
        equipmentData.SyncItemNameFromItem(loadedItem);  // Đồng bộ tên từ item vào trang bị
        equipmentData.SyncItemDescriptionFromItem(loadedItem);  // Đồng bộ Mô tả từ item vào trang bị
        equipmentData.SyncItemParamaFromItem(loadedItem);  // Đồng bộ Parama từ item vào trang bị
        equipmentData.SyncItemTypeFromItem(loadedItem);  // Đồng bộ Kiểu từ item vào trang bị
        equipmentData.SyncItemScriptObjectFromItem(loadedItem);  // Đồng bộ name đối tượng SO từ item vào trang bị
        equipmentData.SyncItemItemSOFromItem(loadedItem);  // Đồng bộ name đối tượng SO từ item vào trang bị                                                             
        // CHỈ SỐ GỐC
        equipmentData.stars = savedData.stars;
        equipmentData.status = savedData.status;
        equipmentData.level = savedData.level;
        equipmentData.hp = savedData.hp;
        equipmentData.mp = savedData.mp;
        equipmentData.chimang = savedData.chimang;
        equipmentData.dame = savedData.dame;
        equipmentData.hutki = savedData.hutki;
        equipmentData.hutmau = savedData.hutmau;
        // CHỈ SỐ NÂNG CẤP
        equipmentData.starsshow = savedData.stars;
        equipmentData.hpshow = savedData.hpshow;
        equipmentData.mpshow = savedData.mpshow;
        equipmentData.chimangshow = savedData.chimangshow;
        equipmentData.dameshow = savedData.dameshow;
        equipmentData.hutkishow = savedData.hutkishow;
        equipmentData.hutkishow = savedData.hutkishow;
        equipmentData.solanepsao = savedData.solanepsao;
        

        return equipmentData;
    }
    public static Item LoadItem(string itemName)
{
    // Đường dẫn tương đối đến thư mục Resources
    string path = $"Items/{itemName}";

    // Tải Item từ Resources
    Item item = Resources.Load<Item>(path);

    if (item == null)
    {
        //Debug.LogError($"Không thể tải Item: {itemName} từ đường dẫn: {path}");
        return null; // Trả về null nếu không tải được
    }

    return item; // Trả về Item nếu tải thành công
}
    IEnumerator LoadChiso()
    {
        yield return new WaitForSeconds(1.2f);
        UpdatePlayerStats();
        
    }

    public void UpdatePlayerStats()
    {
        // Reset chỉ số trước khi cộng lại từ đầu

        hp = 0;
        mp = 0;
        hutmau = 0;
        hutki = 0;
        chimang = 0;
        dame = 0;
        ne = 0;
        // Tạo danh sách các trang bị
        var equipmentList = new[] {
        playerEquipment.ao,
        playerEquipment.quan,
        playerEquipment.gang,
        playerEquipment.giay,
        playerEquipment.rada,
        playerEquipment.canh,
        playerEquipment.daychuyen,
        playerEquipment.nhan,
        playerEquipment.vukhi,
        playerEquipment.pet,
        playerEquipment.phukien,
    };
        //// Cộng chỉ số từ từng trang bị
        foreach (var equipment in equipmentList)
        {
            if (equipment != null)
            {

                hp += equipment.hp + equipment.hpshow;
                mp += equipment.mp + equipment.mpshow;
                hutmau += equipment.hutmau + equipment.hutmaushow;
                hutki += equipment.hutki + equipment.hutkishow;
                chimang += equipment.chimang + equipment.chimangshow;
                dame += equipment.dame + equipment.dameshow;
                ne += equipment.ne + equipment.neshow;
                doben += equipment.doben + equipment.dobenshow;


            }
        }


        // Cập nhật chỉ số cho GameManager (Reset giá trị trước khi cộng)
        GameManager.Singleton.hp = hp + GameManager.Singleton.hpao;
        GameManager.Singleton.mp = hp + GameManager.Singleton.mpao;
        GameManager.Singleton.hpao = hp + GameManager.Singleton.hpmax;
        GameManager.Singleton.mpao = mp + GameManager.Singleton.mpmax;
        GameManager.Singleton.dameao = dame + GameManager.Singleton.dame;
        GameManager.Singleton.chimangao = chimang + GameManager.Singleton.chimang;
        GameManager.Singleton.hutkiao = hutki + GameManager.Singleton.hutki;
        GameManager.Singleton.hutmauao = hutmau + GameManager.Singleton.hutmau;
        GameManager.Singleton.neao = ne + GameManager.Singleton.necoban;
        GameManager.Singleton.hp = GameManager.Singleton.hpao;
        GameManager.Singleton.mp = GameManager.Singleton.mpao;
        UImanager.Singleton.SetValue();
        Debug.Log("Các chỉ số đã được cập nhật.");
    }
    private void UpdateCS()
    {
        // Reset chỉ số trước khi cộng lại từ đầu
        hp = 0;
        mp = 0;
        hutmau = 0;
        hutki = 0;
        chimang = 0;
        dame = 0;
        ne = 0;
        doben = 0;

        // Tạo danh sách các trang bị
        var equipmentList = new[] {
        playerEquipment.ao,
        playerEquipment.quan,
        playerEquipment.gang,
        playerEquipment.giay,
        playerEquipment.rada,
        playerEquipment.canh,
        playerEquipment.daychuyen,
        playerEquipment.nhan,
        playerEquipment.vukhi,
        playerEquipment.pet,
        playerEquipment.phukien,

    };

        //// Cộng chỉ số từ từng trang bị
        foreach (var equipment in equipmentList)
        {
            if (equipment != null)
            {

                hp += equipment.hp + equipment.hpshow;
                mp += equipment.mp + equipment.mpshow;
                hutmau += equipment.hutmau + equipment.hutmaushow;
                hutki += equipment.hutki + equipment.hutkishow;
                chimang += equipment.chimang + equipment.chimangshow;
                dame += equipment.dame + equipment.dameshow;
                ne += equipment.ne + equipment.neshow;
                doben += equipment.doben + equipment.dobenshow;

            }
        }

        // Cập nhật chỉ số cho GameManager (Reset giá trị trước khi cộng)
        GameManager.Singleton.hp = hp + GameManager.Singleton.hpao;
        GameManager.Singleton.mp = hp + GameManager.Singleton.mpao;
        GameManager.Singleton.hpao = hp + GameManager.Singleton.hpmax;
        GameManager.Singleton.mpao = mp + GameManager.Singleton.mpmax;
        GameManager.Singleton.dameao = dame + GameManager.Singleton.dame;
        GameManager.Singleton.chimangao = chimang + GameManager.Singleton.chimang;
        GameManager.Singleton.hutkiao = hutki + GameManager.Singleton.hutki;
        GameManager.Singleton.hutmauao = hutmau + GameManager.Singleton.hutmau;
        GameManager.Singleton.neao = ne + GameManager.Singleton.necoban;
        UImanager.Singleton.SetValue();
        GameManager.Singleton.hp = GameManager.Singleton.hpao;
        GameManager.Singleton.mp = GameManager.Singleton.mpao;
       
    }
    public void Giamdoben()
    {
        // Tạo danh sách các trang bị
        var equipmentList = new[] {
        playerEquipment.ao,
        playerEquipment.quan,
        playerEquipment.gang,
        playerEquipment.giay,
        playerEquipment.rada,
        playerEquipment.canh,
        playerEquipment.daychuyen,
        playerEquipment.nhan,
        playerEquipment.vukhi,
        playerEquipment.pet,
        playerEquipment.phukien,

    };

        //// Cộng chỉ số từ từng trang bị
        foreach (var equipment in equipmentList)
        {
            if (equipment != null)
            {

                if (equipment.dobenshow >= 1)
                {
                    equipment.dobenshow -= 1;
                }
                    

            }
        }
    }
    public void EquipItem(Item item, string slot)
    {
        if (item == null)
        {
            Thongbao.Singleton.ShowThongbao("Vật phẩm không hợp lệ.");
            Debug.LogWarning("[EquipItem] Lỗi: item null.");
            return;
        }

        Debug.Log($"[EquipItem] Đang trang bị {item.itemName} vào slot {slot}");

        // Kiểm tra cấp độ yêu cầu
        if (GameManager.Singleton.level < item.level)
        {
            Thongbao.Singleton.ShowThongbao("Bạn chưa đạt sức mạnh yêu cầu.");
            Debug.LogWarning($"[EquipItem] Cấp hiện tại ({GameManager.Singleton.level}) < yêu cầu ({item.level}).");
            return;
        }

        if (itemProfile.itemTypeSelect == ItemType.Equipment && dobenshow <= 0)
        {
            Thongbao.Singleton.ShowThongbao("Độ bền thấp hơn 0 không thể mặc.");
            Debug.LogWarning("[EquipItem] Độ bền không đủ.");
            return;
        }

        // Tháo trang bị cũ trước khi mặc đồ mới
        Debug.Log($"[EquipItem] Đang tháo trang bị cũ từ slot: {slot}");
        RemovePreviousEquipment(slot);

        switch (item.itemType)
        {
            case ItemType.Equipment:
                Debug.Log($"[EquipItem] Đang mặc trang bị: {item.itemName} vào slot {slot}");
                playerEquipment.EquipItem(item, slot, dameshow, hpshow, mpshow, chimangshow, hutkishow, hutkishow, neshow, solanepsao, dobenshow);
                break;

            case ItemType.Consumable:
                HandleConsumableItem(item);
                Debug.Log($"[EquipItem] Dùng vật phẩm tiêu hao: {item.itemName}");
                return;

            case ItemType.Teleport:
                Teleport(item);
                Debug.Log($"[EquipItem] Dịch chuyển bằng vật phẩm: {item.itemName}");
                return;

            case ItemType.ItemRand:
                HandleRandomItem(item);
                Debug.Log($"[EquipItem] Nhận vật phẩm ngẫu nhiên: {item.itemName}");
                return;

            default:
                Debug.LogWarning($"[EquipItem] Không hỗ trợ loại vật phẩm: {item.itemType}");
                return;
        }

        // Xóa item khỏi kho sau khi trang bị
        Debug.Log($"[EquipItem] Xóa vật phẩm khỏi kho: {item.itemName}");
        Inventory.Singleton.UnRemoveItem(vitriitem, 1);

        // Lưu trạng thái mới và cập nhật UI
        Debug.Log("[EquipItem] Lưu trạng thái mới của trang bị.");
        SaveEquipment();
        Inventory.Singleton.SaveInventory();
        Inventory.Singleton.LoadInventory();
       

        if (InventoryUI.Singleton != null)
        {
            Debug.Log("[EquipItem] Cập nhật UI kho đồ.");
            InventoryUI.Singleton.UpdateUIAll();
        }
    }

    private void HandleConsumableItem(Item item)
    {
        // Kiểm tra tính hợp lệ của dữ liệu
        if (item.itemParama.Length != item.parameterValue.Length)
        {
            //Debug.LogWarning($"Dữ liệu không khớp giữa itemParama và parameterValue trong {item.itemName}.");
            return;
        }

        for (int i = 0; i < item.itemParama.Length; i++)
        {
            int value = item.parameterValue[i];
            switch (item.itemParama[i])
            {
                case ItemParama.HP:
                    if (GameManager.Singleton.hp >= GameManager.Singleton.hpao)
                    {
                        Thongbao.Singleton.ShowThongbao("HP đã đầy, không thể tăng thêm.");
                    }
                    else
                    {
                        GameManager.Singleton.hp = Mathf.Min(GameManager.Singleton.hp + value, GameManager.Singleton.hpao);
                        Thongbao.Singleton.ShowThongbao($"Tăng HP thêm {value}.");
                        Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    }
                    break;

                case ItemParama.MP:
                    if (GameManager.Singleton.mp >= GameManager.Singleton.mpao)
                    {
                        Thongbao.Singleton.ShowThongbao("MP đã đầy, không thể tăng thêm.");
                    }
                    else
                    {
                        GameManager.Singleton.mp = Mathf.Min(GameManager.Singleton.mp + value, GameManager.Singleton.mpao);
                        Thongbao.Singleton.ShowThongbao($"Tăng MP thêm {value}.");
                        Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    }
                    break;
                case ItemParama.THELUC:
                    if (GameManager.Singleton.theluc >= 500)
                    {
                        Thongbao.Singleton.ShowThongbao("Thể lực đang qua 50%, không thể tăng thêm.");
                    }
                    else
                    {
                        GameManager.Singleton.theluc = value;
                        Thongbao.Singleton.ShowThongbao($"Đã tăng {value} thể lực.");
                        Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    }
                    break;
                case ItemParama.EXP:
                    GameManager.Singleton.level = Mathf.Min(GameManager.Singleton.level + value, 100);
                    Thongbao.Singleton.ShowThongbao($"Tăng EXP thêm {value}.");
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    break;
                case ItemParama.BOXREWARD:
                    Item item1 = new Item();
                    string rdBox = item.RandomReward(item1);
                    RewardPanel.Singleton.SpanInstantiate(item1, 1);
                    Thongbao.Singleton.ShowThongbao($"Bạn nhận được {rdBox}.");
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    break;

                case ItemParama.RANDOMGOLD:
                    
                    int rdGold = item.RandomGold(item);
                    GameManager.Singleton.gold += rdGold;
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    Thongbao.Singleton.ShowThongbao($"Bạn nhận được {FormatNumber(rdGold)} vàng.");
                    break;
                case ItemParama.PETBOX:
                    Item item2 = new Item();
                    string rdPet = item.RandomPetParama(item2);
                    RewardPanel.Singleton.SpanInstantiate(item2, 1);
                    Thongbao.Singleton.ShowThongbao($"Bạn nhận được {rdPet}.");
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    break;
                case ItemParama.VATPHAM:
                    Item item3 = new Item();
                    string rdVP = item.RandomCountItem(item3);
                    RewardPanel.Singleton.SpanInstantiate(item3, item3.quantity);
                    Thongbao.Singleton.ShowThongbao($"Bạn nhận được {rdVP} .");
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    break;
                case ItemParama.ATTACK:
                    GameManager.Singleton.dame += value;
                    Thongbao.Singleton.ShowThongbao($"Tăng Công thêm {value}.");
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    break;

                case ItemParama.DEFENSE:
                    //GameManager.Singleton.defense += value;
                    Thongbao.Singleton.ShowThongbao($"Tăng Thủ thêm {value}.");
                    break;

                case ItemParama.SPEED:
                    //GameManager.Singleton.speed += value;
                    Thongbao.Singleton.ShowThongbao($"Tăng Tốc độ thêm {value}.");
                    break;

                case ItemParama.RUBY:
                    GameManager.Singleton.ruby += value;
                    Thongbao.Singleton.ShowThongbao($"Tăng Ruby thêm {value}.");
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    break;

                case ItemParama.GOLD:
                    GameManager.Singleton.gold += value;
                    Thongbao.Singleton.ShowThongbao($"Tăng Gold thêm {value}.");
                    Inventory.Singleton.UnRemoveItem(vitriitem, 1);
                    break;
               
                default:
                   // Debug.LogWarning($"Hiệu ứng {item.itemParama[i]} chưa được xử lý.");
                    break;
            }
        }

        // Cập nhật giao diện và lưu dữ liệu
        //Inventory.Singleton.UnRemoveItem(vitriitem, 1);
        if (InventoryUI.Singleton != null)
        {
            InventoryUI.Singleton.UpdateUIAll();

        }
        GameManager.Singleton.SaveData();
       
        // Debug.Log($"Đã sử dụng vật phẩm tiêu hao {item.itemName}.");
    }
    private void HandleRandomItem(Item item)
    {
        if (item.parameterValue.Length != item.parameterValue.Length)
        {
            Debug.LogWarning($"Dữ liệu không khớp giữa itemParama và parameterValue trong {item.itemName}.");
            return;
        }

        for (int i = 0; i < item.itemParama.Length; i++)
        {
            int minGold = item.parameterValue[0];
            int maxGold = item.parameterValue[1];

            if (minGold > maxGold)
            {
                Debug.LogWarning("Giá trị minGold lớn hơn maxGold. Vui lòng kiểm tra lại dữ liệu.");
                continue;
            }

            int randomGold = new System.Random().Next(minGold, maxGold + 1);
            GameManager.Singleton.gold += randomGold;
            Thongbao.Singleton.ShowThongbao($"Bạn nhận được {randomGold} vàng.");
        }

        // Cập nhật giao diện và lưu dữ liệu
        Inventory.Singleton.UnRemoveItem(InventoryUI.Singleton.vitriitem, 1);
        InventoryUI.Singleton.UpdateItem();
        GameManager.Singleton.SaveData();
    }

    

    public void UseItem(Item item)
    {
        if (item.itemType == ItemType.Consumable)
        {
            int hpIncrease = item.GetParameterValue(ItemParama.HP);
            int mpIncrease = item.GetParameterValue(ItemParama.MP);

            // Áp dụng chỉ số vào nhân vật
            if (hpIncrease > 0)
            {
               
                GameManager.Singleton.hp += hpIncrease;
                Thongbao.Singleton.ShowThongbao($"Đã sử dụng vật phẩm {item.itemName}, tăng {hpIncrease} HP.");
            }

            if (mpIncrease > 0)
            {
                GameManager.Singleton.mp += mpIncrease;
                Thongbao.Singleton.ShowThongbao($"Đã sử dụng vật phẩm {item.itemName}, tăng {hpIncrease} MP.");

            }
            //Thongbao.Singleton.ShowThongbao($"Đã sử dụng vật phẩm {item.itemName}, tăng {hpIncrease} HP và {mpIncrease} MP.");
            //Debug.Log($"Đã sử dụng vật phẩm {item.itemName}, tăng {hpIncrease} HP và {mpIncrease} MP.");
        }
    }
    public void Teleport(Item item)
    {
        if (item.itemType == ItemType.Teleport)
        {
            PanelTeleport.gameObject.SetActive(true);
        }
    }
    private void RemovePreviousEquipment(string slot)
    {
        DataEquipment previousEquipment = null;

        // Lấy trang bị trước đó dựa vào slot
        switch (slot)
        {
            case "ao": previousEquipment = playerEquipment.ao; break;
            case "quan": previousEquipment = playerEquipment.quan; break;
            case "gang": previousEquipment = playerEquipment.gang; break;
            case "giay": previousEquipment = playerEquipment.giay; break;
            case "rada": previousEquipment = playerEquipment.rada; break;
            case "canh": previousEquipment = playerEquipment.canh; break;
            case "daychuyen": previousEquipment = playerEquipment.daychuyen; break;
            case "nhan": previousEquipment = playerEquipment.nhan; break;
            case "vukhi": previousEquipment = playerEquipment.vukhi; break;
            case "pet": previousEquipment = playerEquipment.pet; break;
            case "phukien": previousEquipment = playerEquipment.phukien; break;
            default:
                Debug.LogError($"[RemovePreviousEquipment] Slot không hợp lệ: {slot}");
                return;
        }

        if (previousEquipment == null)
        {
            Debug.LogWarning($"[RemovePreviousEquipment] Không có trang bị cũ trong slot: {slot}");
            return;
        }

        Item itemToAdd = previousEquipment.Item;

        // Nếu `Item` bị null, cố gắng load lại từ Resources
        if (itemToAdd == null && !string.IsNullOrEmpty(previousEquipment.itemNameScriptObject))
        {
            itemToAdd = Resources.Load<Item>("Items/" + previousEquipment.itemNameScriptObject);

            if (itemToAdd == null)
            {
                Debug.LogWarning($"[RemovePreviousEquipment] Không thể tải Item từ Resources: {previousEquipment.itemNameScriptObject}");
                return;
            }
        }

        // Nếu vẫn null sau khi load, không làm gì nữa
        if (itemToAdd == null)
        {
            Debug.LogWarning($"[RemovePreviousEquipment] Không có Item hợp lệ để thêm vào kho.");
            return;
        }

        // Thêm trang bị vào kho
        Inventory.Singleton.AddThaoItem(
            itemToAdd,
            1,
            previousEquipment.status,
            previousEquipment.stars,
            previousEquipment.level,
            previousEquipment.dameshow,
            previousEquipment.hpshow,
            previousEquipment.mpshow,
            previousEquipment.chimangshow,
            previousEquipment.hutkishow,
            previousEquipment.hutmaushow,
            previousEquipment.neshow,
            previousEquipment.solanepsao,
            previousEquipment.dobenshow
        );

        Debug.Log($"[RemovePreviousEquipment] Trang bị '{itemToAdd.name}' đã được thêm vào kho từ slot: {slot}");

        // Xóa trạng thái trang bị khỏi slot
        ClearEquipmentStats(slot);
    }


    private void ClearEquipmentStats(string slot)
    {
        switch (slot)
        {
            case "ao":
                playerEquipment.ao = null;
                break;
            case "quan":
                playerEquipment.quan = null;
                break;
            case "gang":
                playerEquipment.gang = null;
                break;
            case "giay":
                playerEquipment.giay = null;
                break;
            case "rada":
                playerEquipment.rada = null;
                break;
            case "canh":
                playerEquipment.canh = null;
                break;
            case "daychuyen":
                playerEquipment.daychuyen = null;
                break;
            case "nhan":
                playerEquipment.nhan = null;
                break;
            case "vukhi":
                playerEquipment.vukhi = null;
                break;
            case "pet":
                playerEquipment.pet = null;
                break;
            case "phukien":
                playerEquipment.phukien = null;
                break;
        }
    }

    public void ThaoTrangbi()
    {
        //UnequipItem();
    }
    public void UnequipItem() // Thêm tham số để đảm bảo có giá trị đầu vào
    {
        // Kiểm tra nếu playerEquipment bị null
        if (playerEquipment == null)
        {
            Debug.LogError("[UnequipItem] playerEquipment bị null!");
            return;
        }

        // Kiểm tra nếu slotName null hoặc rỗng
        if (string.IsNullOrEmpty(slotNameThao))
        {
            Debug.LogError("[UnequipItem] slotName bị null hoặc rỗng!");
            return;
        }

        Debug.Log($"[UnequipItem] Bắt đầu tháo trang bị từ slot: {slotNameThao}");

        // Kiểm tra xem slot có hợp lệ không
        if (!playerEquipment.IsSlotValid(slotNameThao))
        {
            Debug.LogWarning($"[UnequipItem] Slot '{slotNameThao}' không hợp lệ hoặc không tồn tại.");
            return;
        }

        // Tạo Dictionary chứa các trang bị trong từng slot
        Dictionary<string, DataEquipment> equipmentMap = new Dictionary<string, DataEquipment>
    {
        { "ao", playerEquipment.ao },
        { "quan", playerEquipment.quan },
        { "gang", playerEquipment.gang },
        { "giay", playerEquipment.giay },
        { "rada", playerEquipment.rada },
        { "canh", playerEquipment.canh },
        { "daychuyen", playerEquipment.daychuyen },
        { "nhan", playerEquipment.nhan },
        { "vukhi", playerEquipment.vukhi },
        { "pet", playerEquipment.pet },
        { "phukien", playerEquipment.phukien }
    };

        // In ra danh sách các slot hợp lệ
        Debug.Log("[UnequipItem] Danh sách các slot hợp lệ:");
        foreach (var key in equipmentMap.Keys)
        {
            Debug.Log($"- {key}");
        }

        // Kiểm tra xem slot có tồn tại trong equipmentMap không
        if (!equipmentMap.ContainsKey(slotNameThao))
        {
            Debug.LogWarning($"[UnequipItem] Slot '{slotNameThao}' không tồn tại trong equipmentMap.");
            return;
        }

        // Lấy trang bị từ slot
        DataEquipment removedEquipment = equipmentMap[slotNameThao];

        // Kiểm tra xem có trang bị trong slot không
        if (removedEquipment == null)
        {
            Debug.LogWarning($"[UnequipItem] Không có trang bị trong slot '{slotNameThao}' để tháo.");
            return;
        }
        if (removedEquipment == null || removedEquipment.Item == null)
        {
            //Debug.LogWarning($"[UnequipItem] Không có trang bị trong slot '{slotNameThao}' để tháo.");
            Item item = Resources.Load<Item>("Items/" + removedEquipment.itemNameScriptObject);
            // Chuyển trang bị vào kho
            Inventory.Singleton.AddThaoItem(
                item,
                1,
                removedEquipment.status,
                removedEquipment.stars,
                removedEquipment.level,
                removedEquipment.dameshow,
                removedEquipment.hpshow,
                removedEquipment.mpshow,
                removedEquipment.chimangshow,
                removedEquipment.hutkishow,
                removedEquipment.hutmaushow,
                removedEquipment.neshow,
                removedEquipment.solanepsao,
                removedEquipment.dobenshow
            );
            Debug.Log($"[UnequipItem] Trang bị '{removedEquipment.Item}' đã được thêm vào kho.");

            // Xóa trang bị khỏi slot
            playerEquipment.ClearSlot(slotNameThao);
            Debug.Log($"[UnequipItem] Slot '{slotNameThao}' đã được xóa.");

            // Kiểm tra lại slot sau khi xóa
            DataEquipment checkSlot1 = equipmentMap.ContainsKey(slotNameThao) ? equipmentMap[slotNameThao] : null;
            Debug.Log($"[UnequipItem] Kiểm tra sau khi xóa, slot '{slotNameThao}' còn trang bị không? {(checkSlot1 == null ? "Không" : "Có")}");

            // Cập nhật UI và dữ liệu
            UpdateCS();
            SaveEquipment();
            EquipmentUIManager.Singleton.UpdateEquipmentUI();

            if (InventoryUI.Singleton != null)
            {
                InventoryUI.Singleton.UpdateUIAll();
                Debug.Log("[UnequipItem] Inventory UI đã cập nhật.");
            }
            return;
        }

        //Item item = Resources.Load<Item>("Items/" + removedEquipment.itemNameScriptObject);
        Debug.Log($"[UnequipItem] Tháo trang bị: {removedEquipment.Item}");
        // Chuyển trang bị vào kho
        Inventory.Singleton.AddThaoItem(
            removedEquipment.Item,
            1,
            removedEquipment.status,
            removedEquipment.stars,
            removedEquipment.level,
            removedEquipment.dameshow,
            removedEquipment.hpshow,
            removedEquipment.mpshow,
            removedEquipment.chimangshow,
            removedEquipment.hutkishow,
            removedEquipment.hutmaushow,
            removedEquipment.neshow,
            removedEquipment.solanepsao,
            removedEquipment.dobenshow
        );

        Debug.Log($"[UnequipItem] Trang bị '{removedEquipment.Item}' đã được thêm vào kho.");

        // Xóa trang bị khỏi slot
        playerEquipment.ClearSlot(slotNameThao);
        Debug.Log($"[UnequipItem] Slot '{slotNameThao}' đã được xóa.");

        // Kiểm tra lại slot sau khi xóa
        DataEquipment checkSlot = equipmentMap.ContainsKey(slotNameThao) ? equipmentMap[slotNameThao] : null;
        Debug.Log($"[UnequipItem] Kiểm tra sau khi xóa, slot '{slotNameThao}' còn trang bị không? {(checkSlot == null ? "Không" : "Có")}");

        // Cập nhật UI và dữ liệu
        UpdateCS();
        SaveEquipment();
        EquipmentUIManager.Singleton.UpdateEquipmentUI();

        if (InventoryUI.Singleton != null)
        {
            InventoryUI.Singleton.UpdateUIAll();
            Debug.Log("[UnequipItem] Inventory UI đã cập nhật.");
        }
    }


    string FormatNumber(int value)
    {
        return value.ToString("N0", CultureInfo.InvariantCulture);
    }
}

//[System.Serializable]
//public class EquipmentSaveData
//{
//    public Dictionary<string, DataEquipment> equippedItems = new Dictionary<string, DataEquipment>();
//}

[System.Serializable]
public class EquipmentSaveData
{
    public List<EquipmentItem> equippedItems = new List<EquipmentItem>();
}

[Serializable]
public class EquipmentItem
{
    public string key; // Tên trang bị (e.g., "ao", "quan")
    public DataEquipment value; // Dữ liệu trang bị
}


[System.Serializable]
public class DataEquipment
{
    public string itemName;
    public string itemNameScriptObject;
    public string itemParama; // Mô tả item
    public string itemType;
    public string description; // Mô tả item
    public string spriteName; // Tên của Sprite
    public int level;
    public int hp;
    public int mp;
    public int hutmau;
    public int hutki;
    public int chimang;
    public int dame;
    public int ne;
    public int doben;
    public int stars; // số sao sở hữu
    public string status; // số sao sở hữu
    [Header("CHỈ SỐ NÂNG CẤP")]
    public int hpshow;
    public int mpshow;
    public int hutmaushow;
    public int hutkishow;
    public int chimangshow;
    public int dameshow;
    public float neshow;
    public int starsshow; // số sao sở hữu
    public int solanepsao;
    public int dobenshow;
    [JsonIgnore] // Newtonsoft.Json sẽ bỏ qua trường này khi lưu/đọc JSON
    public Item Item;
    



    // Đồng bộ chỉ số từ Item (nếu cần thiết, có thể không cần dùng trong trường hợp này)
    public void SyncStatsFromItem(Item item)
    {
        if (item == null || item.itemParama.Length != item.parameterValue.Length)
        {
            ClearStats();
            return;
        }

        for (int i = 0; i < item.itemParama.Length; i++)
        {
            switch (item.itemParama[i])
            {
                case ItemParama.HP: hp = item.parameterValue[i]; break;
                case ItemParama.MP: mp = item.parameterValue[i]; break;
                case ItemParama.HUTMAU: hutmau = item.parameterValue[i]; break;
                case ItemParama.HUTKHI: hutki = item.parameterValue[i]; break;
                case ItemParama.CHIMANG: chimang = item.parameterValue[i]; break;
                case ItemParama.TANCONG: dame = item.parameterValue[i]; break;
                case ItemParama.DEFENSE: dame = item.parameterValue[i]; break;
                case ItemParama.NETRANH: ne = item.parameterValue[i]; break;
            }
        }
        

        spriteName = item.icon != null ? item.icon.name : ""; // Lưu tên Sprite
    }
    public void SyncItemNameFromItem(Item item)
    {
        if (item != null)
        {
            itemName = item.itemName; // Lấy tên từ Item
        }
        else
        {
            itemName = "None"; // Giá trị mặc định nếu Item null
        }
    }   
    public void SyncItemParamaFromItem(Item item)
    {
        if (item != null)
        {
            itemParama = item.GetItemEffect(); // Lấy tên từ Item
        }
        else
        {
            itemParama = "None"; // Giá trị mặc định nếu Item null
        }
    }
    public void SyncItemItemSOFromItem(Item item)
    {
        
        if (item != null)
        {
            Item = item; // Lấy tên từ Item
            
        }
        else
        {
            Item = null;
        }
    } 
    public void SyncItemScriptObjectFromItem(Item item)
    {
        if (item != null)
        {
            itemNameScriptObject = item.name; // Lấy tên từ Item
        }
        else
        {
            itemNameScriptObject = "None"; // Giá trị mặc định nếu Item null
        }
    }
    public void SyncItemTypeFromItem(Item item)
    {
        if (item != null)
        {
            itemType = item.GetItemTypeName(); // Lấy tên từ Item
        }
        else
        {
            itemType = "None"; // Giá trị mặc định nếu Item null
        }
    }
    public void SyncItemDescriptionFromItem(Item item)
    {
        if (item != null)
        {
            description = item.description; // Lấy tên từ Item
        }
        else
        {
            description = "None"; // Giá trị mặc định nếu Item null
        }
    }

    // Xóa chỉ số
    public void ClearStats()
    {
        itemName = "";
        level = 1;
        description = "";
        itemType = "";
        itemParama = "";
        hp = 0;
        mp = 0;
        hutmau = 0;
        hutki = 0;
        chimang = 0;
        dame = 0;
        ne = 0;
        dameshow = 0;
        hpshow = 0;
        mpshow = 0;
        chimangshow = 0;
        hutkishow = 0;
        hutmaushow = 0;
        dobenshow = 0;
    }

}
