using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemProfile : MonoBehaviour
{
   [SerializeField] private Sprite BoderImageDefault;
   [SerializeField] private Sprite BoderImageSelect;
    public Image Icon;
    public Image BoderAnhcap;
    public Image BoderSelect;
    public Image ExpireditemsIcon;
    public Text txtQuantity;
    public ItemType itemType;
    public string description; // Mô tả item
    public string LoaiItemType;   // Loại item (Tiêu hao, Trang bị, Nguyên liệu)
    private ItemProfileUI itemProfileUI1;
    private ItemDoben itemDoben;
    private EpchisoUI EpchisoUI;
    private TachepsaoUI tachEpsao;
    private ChestProfile chestProfile;
    public Button Click;
    public GameObject[] starImages;     // Danh sách các GameObject hình sao
    public int stars;
    public List<Item> item = new List<Item>();
    public List<Item> itemBoxReward = new List<Item>();
    public string boxMessage;
    public int vitriitem = 0;
    public int dame; // cấp trang bị
    public int hp; // cấp trang bị
    public int mp; // cấp trang bị
    public int chimang; // cấp trang bị
    public int hutmau; // cấp trang bị
    public int hutki; // cấp trang bị
    public float ne; // cấp trang bị
    public int solanepsao; // cấp trang bị
    public int doben; // độ bền trang bị
    public string status;
    public SlotName slotName;
    public string levelyc;
    public int giaban;
    public int quantity;
    public int level;

    public Sprite[] spritesAnhcap;


    private void Start()
    {
        if (level > 0 && level <= spritesAnhcap.Length)
        {
            // Gán sprite từ mảng dựa theo level
            BoderAnhcap.sprite = spritesAnhcap[level - 1];
        }
        else
        {
        }
        if (itemType == ItemType.Equipment)
        {
            if (doben > 0)
            {

                ExpireditemsIcon.gameObject.SetActive(false);
            }
            else
            {
                ExpireditemsIcon.gameObject.SetActive(true);

            }
        }
        else if (itemType == ItemType.Consumable)
        {
            if (doben > 0)
            {

                ExpireditemsIcon.gameObject.SetActive(false);
            }
            else
            {
                ExpireditemsIcon.gameObject.SetActive(true);

            }
        }
        else if (itemType == ItemType.Teleport)
        {
            if (doben > 0)
            {

                ExpireditemsIcon.gameObject.SetActive(false);
            }
            else
            {
                ExpireditemsIcon.gameObject.SetActive(true);

            }
        }
    }

    private void OnEnable()
    {
        if (level > 0 && level <= spritesAnhcap.Length)
        {
            // Gán sprite từ mảng dựa theo level
            BoderAnhcap.sprite = spritesAnhcap[level - 1];
        }
        else
        {
        }
        if(itemType == ItemType.Equipment)
        {
            if (doben > 0)
            {

                ExpireditemsIcon.gameObject.SetActive(false);
            }
            else
            {
                ExpireditemsIcon.gameObject.SetActive(true);

            }
        }
        else if (itemType == ItemType.Consumable)
        {
            ExpireditemsIcon.gameObject.SetActive(false);
        }
        
    }

    public void SetTarget()
    {
        // Kiểm tra playerEquipment trước khi lấy dữ liệu
        if (EquipmentManager.Singleton.playerEquipment == null)
        {
            Debug.LogError("playerEquipment is null! Không thể tạo danh sách trang bị.");
            return;
        }
        // Tạo danh sách các trang bị
        var equipmentList = new[] {
        EquipmentManager.Singleton.playerEquipment.ao,
        EquipmentManager.Singleton.playerEquipment.quan,
        EquipmentManager.Singleton.playerEquipment.gang,
        EquipmentManager.Singleton.playerEquipment.giay,
        EquipmentManager.Singleton.playerEquipment.rada,
        EquipmentManager.Singleton.playerEquipment.canh,
        EquipmentManager.Singleton.playerEquipment.daychuyen,
        EquipmentManager.Singleton.playerEquipment.nhan,
        EquipmentManager.Singleton.playerEquipment.vukhi,
        EquipmentManager.Singleton.playerEquipment.pet,
        EquipmentManager.Singleton.playerEquipment.phukien,
    };
        itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
        itemDoben = FindObjectOfType<ItemDoben>();
        InventoryUI.Singleton.SetTarget(gameObject);
        InventoryUI.Singleton.item = item.ToArray();
        InventoryUI.Singleton.vitriitem = vitriitem;
        InventoryUI.Singleton.slotName = slotName;
        InventoryUI.Singleton.star = stars;
        //InventoryUI.Singleton.le = level;
        ChestManager.Singleton.status = status;
        ChestManager.Singleton.quantity = quantity;
        //EquipmentManager.Singleton.slotName = slotName.ToString();
        EquipmentManager.Singleton.item = item.ToArray();
        EquipmentManager.Singleton.vitriitem = vitriitem;
        //EquipmentManager.Singleton.itemParama = ITE.ToString();
        EquipmentManager.Singleton.itemType= LoaiItemType;
        EquipmentManager.Singleton.slotNameMac= slotName.ToString();
        EquipmentManager.Singleton.level= level;
        EquipmentManager.Singleton.quantity= quantity;
        EquipmentManager.Singleton.dameshow= dame;
        EquipmentManager.Singleton.hpshow= hp;
        EquipmentManager.Singleton.mpshow= mp;
        EquipmentManager.Singleton.hutkishow= hutki;
        EquipmentManager.Singleton.hutmaushow= hutmau;
        EquipmentManager.Singleton.chimangshow= chimang;
        EquipmentManager.Singleton.neshow= ne;
        EquipmentManager.Singleton.dobenshow= doben;
        EquipmentManager.Singleton.solanepsao= solanepsao;

        

        if (doben > 0)
        {

            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
            itemDoben.ExpireditemsIcon.gameObject.SetActive(false);

        }
        else if (doben <= 0)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(true);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(true);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(false);
            itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
            itemDoben = FindObjectOfType<ItemDoben>();
            itemDoben.ExpireditemsIcon.gameObject.SetActive(true);
            if (item != null)
            {
                itemDoben.ShowItemDetails(item[0], BoderAnhcap, level, quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau, ne, solanepsao, doben, boxMessage);
            }
            else
            {
                itemDoben.HideItemDetails();
            }


        }
        if (itemType == ItemType.Material)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
            itemDoben.ExpireditemsIcon.gameObject.SetActive(false);
            EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
        }
        else if (itemType == ItemType.Teleport)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
            itemDoben.ExpireditemsIcon.gameObject.SetActive(false);
            EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
        }
        //else if (itemType == ItemType.Consumable)
        //{
        //    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
        //    itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
        //    itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
        //    itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
        //    itemDoben.ExpireditemsIcon.gameObject.SetActive(false);

        //}

       
        if (itemType == ItemType.Consumable)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
            itemDoben.ExpireditemsIcon.gameObject.SetActive(false);
            EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
            if (itemBoxReward.Count > 0)
            {
                for (int i = 0; i < itemBoxReward.Count; i++)
                {
                    //boxMessage += $"{itemBoxReward[i].itemName}: +{parameterValue[i]}.\n";
                    boxMessage += $"\n{itemBoxReward[i].itemName}.\n";
                }
               
            }
            else
            {
               

            }
        }
        else if (itemType == ItemType.Equipment)
        {
            // Kiểm tra xem có item nào được chọn không
            if (item == null || item.Count == 0 || string.IsNullOrEmpty(item[0].itemName))
            {
                Debug.Log("Lỗi: item[0] null hoặc không có itemName hợp lệ!");
                EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
                return;
            }
            Item itemLoad = Resources.Load<Item>("Items/" + item[0].name);
            // Biến kiểm tra xem item đã tồn tại chưa
            bool itemExists = false;

            //// Duyệt qua danh sách trang bị để kiểm tra
            //foreach (var equipItem in equipmentList)
            //{
            //    if (equipItem != null && equipItem.Item != null)
            //    {
            //        // Kiểm tra điều kiện nếu slotName trùng với item hiện tại
            //        if (equipItem.Item != null && equipItem.Item.slotName != SlotName.no && equipItem.Item.slotName == item[0].slotName)
            //        {
            //            itemExists = true; // Nếu tìm thấy trùng khớp, gán true
            //                               // Tạo chuỗi mô tả các chỉ số
            //            string statsDescription = "";
            //            switch (equipItem.Item.slotName)
            //            {
            //                case SlotName.ao:
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.ao.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.ao.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.ao.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.ao.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.ao.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.ao.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.ao.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.ao.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.ao.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.ao.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.ao.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.ao.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.ao.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.ao.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.ao.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.ao.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotAoImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;

            //                    int numberOfStars = EquipmentManager.Singleton.playerEquipment.ao.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                             // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.quan:
            //                    // Xử lý cho trường hợp "quan"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.quan.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.quan.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.quan.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.quan.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.quan.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.quan.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.quan.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.quan.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.quan.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.quan.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.quan.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.quan.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.quan.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.quan.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.quan.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.quan.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotQuanImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;

            //                    int numberOfStars1 = EquipmentManager.Singleton.playerEquipment.quan.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars1); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.gang:
            //                    // Xử lý cho trường hợp "gang"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.gang.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.gang.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.gang.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.gang.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.gang.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.gang.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.gang.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.gang.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.gang.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.gang.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.gang.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.gang.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.gang.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.gang.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.gang.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.gang.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotGangImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;

            //                    int numberOfStars2 = EquipmentManager.Singleton.playerEquipment.gang.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars2); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.giay:
            //                    // Xử lý cho trường hợp "giay"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.giay.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.giay.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.giay.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.giay.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.giay.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.giay.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.giay.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.giay.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.giay.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.giay.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.giay.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.giay.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.giay.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.giay.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.giay.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.giay.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotGiayImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;

            //                    int numberOfStars3 = EquipmentManager.Singleton.playerEquipment.giay.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars3); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.rada:
            //                    // Xử lý cho trường hợp "rada"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.rada.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.rada.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.rada.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.rada.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.rada.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.rada.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.rada.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.rada.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.rada.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.rada.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.rada.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.rada.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.rada.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.rada.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.rada.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.rada.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotRadaImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;

            //                    int numberOfStars4 = EquipmentManager.Singleton.playerEquipment.rada.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars4); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.canh:
            //                    // Xử lý cho trường hợp "canh"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.canh.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.canh.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.canh.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.canh.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.canh.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.canh.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.canh.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.canh.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.canh.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.canh.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.canh.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.canh.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.canh.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.canh.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.canh.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.canh.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotCanhImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;
            //                    int numberOfStars5 = EquipmentManager.Singleton.playerEquipment.rada.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars5); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;
            //                case SlotName.daychuyen:
            //                    // Xử lý cho trường hợp "daychuyen"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.daychuyen.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.daychuyen.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.daychuyen.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.daychuyen.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.daychuyen.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.daychuyen.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.daychuyen.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.daychuyen.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.daychuyen.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.daychuyen.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.daychuyen.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.daychuyen.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.daychuyen.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.daychuyen.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.daychuyen.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotDaychuyenmage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;
            //                    int numberOfStars6 = EquipmentManager.Singleton.playerEquipment.daychuyen.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars6); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.nhan:
            //                    // Xử lý cho trường hợp "nhan"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.nhan.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.nhan.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.nhan.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.nhan.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.nhan.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.nhan.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.nhan.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.nhan.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.nhan.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.nhan.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.nhan.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.nhan.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.nhan.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.nhan.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.nhan.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotNhanImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;
            //                    int numberOfStars7 = EquipmentManager.Singleton.playerEquipment.nhan.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                     // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars7); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.vukhi:
            //                    // Xử lý cho trường hợp "vukhi"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.vukhi.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.vukhi.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.vukhi.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.vukhi.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.vukhi.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.vukhi.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.vukhi.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.vukhi.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.vukhi.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.vukhi.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.vukhi.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.vukhi.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.vukhi.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.vukhi.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.vukhi.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotVukhiImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;
            //                    int numberOfStars8 = EquipmentManager.Singleton.playerEquipment.vukhi.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars8); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.pet:
            //                    // Xử lý cho trường hợp "pet"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.pet.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.pet.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.pet.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.pet.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.pet.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.pet.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.pet.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.pet.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.pet.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.pet.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.pet.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.pet.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.pet.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.pet.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.pet.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.pet.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotPetImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;
            //                    int numberOfStars9 = EquipmentManager.Singleton.playerEquipment.pet.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                 // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars9); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;

            //                case SlotName.phukien:
            //                    // Xử lý cho trường hợp "phukien"
            //                    EquipmentManager.Singleton.txtNameItem2.text = EquipmentManager.Singleton.playerEquipment.phukien.itemName;
            //                    EquipmentManager.Singleton.txtParamaItem2.text = EquipmentManager.Singleton.playerEquipment.phukien.itemParama;


            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.level > 0) statsDescription += $"\n<color=red>Cấp trang bị:{EquipmentManager.Singleton.playerEquipment.phukien.level}</color>";
            //                    else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.hpshow > 0) statsDescription += $"\nHP: +{EquipmentManager.Singleton.playerEquipment.phukien.hpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.mpshow > 0) statsDescription += $"\nMP: +{EquipmentManager.Singleton.playerEquipment.phukien.mpshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.dameshow > 0) statsDescription += $"\nTấn công: +{EquipmentManager.Singleton.playerEquipment.phukien.dameshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.chimangshow > 0) statsDescription += $"\nChí mạng: +{EquipmentManager.Singleton.playerEquipment.phukien.chimangshow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.hutkishow > 0) statsDescription += $"\nHút ki: +{EquipmentManager.Singleton.playerEquipment.phukien.hutkishow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.hutmaushow > 0) statsDescription += $"\nHút máu: +{EquipmentManager.Singleton.playerEquipment.phukien.hutmaushow}";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.neshow > 0) statsDescription += $"\nNé: +{EquipmentManager.Singleton.playerEquipment.phukien.ne}";
            //                    if (EquipmentManager.Singleton.playerEquipment.phukien.solanepsao >= 0) statsDescription += $"\nSố lần ép sao tối đa:{EquipmentManager.Singleton.playerEquipment.phukien.solanepsao}/7";
            //                    statsDescription += $"\nĐộ bền trang bị:{EquipmentManager.Singleton.playerEquipment.phukien.dobenshow}/100";
            //                    EquipmentManager.Singleton.txtDescriptionItem2.text = EquipmentManager.Singleton.playerEquipment.phukien.description + $"\n{statsDescription}";
            //                    EquipmentManager.Singleton.txtTypeItem2.text = "Loại:" + EquipmentManager.Singleton.playerEquipment.phukien.itemType;
            //                    EquipmentManager.Singleton.IconItem2.sprite = EquipmentUIManager.Singleton.slotPhukienImage.sprite;
            //                    EquipmentManager.Singleton.Anhbodercap2.sprite = EquipmentUIManager.Singleton.BoderAnhcap[0].sprite;
            //                    int numberOfStars11 = EquipmentManager.Singleton.playerEquipment.phukien.stars; // Giả sử bạn có thuộc tính "stars" trong item
            //                                                                                                     // Kích hoạt sao tương ứng với số sao của item
            //                    for (int j = 0; j < EquipmentManager.Singleton.objStars2.Length; j++)
            //                    {
            //                        EquipmentManager.Singleton.objStars2[j].SetActive(j < numberOfStars11); // Kích hoạt số sao
            //                    }
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);
            //                    break;
            //                case SlotName.no:
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
            //                    break;

            //                default:
            //                    // Xử lý cho trường hợp không khớp với bất kỳ slotName nào
            //                    EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
            //                    break;
            //            }
            //            //break; // Thoát khỏi vòng lặp khi tìm thấy
            //        }
            //        else
            //        {
            //            //Debug.LogWarning("[❌] Điều kiện không thỏa mãn!");

            //            if (equipItem.itemName == "")
            //            {
            //                EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(false);
            //            }
            //            else
            //            {
            //                EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(true);
            //            }
            //            //    //Debug.LogWarning("[Lỗi] equipItem.Item == null");
            //            //    equipItem.Item = itemLoad;

            //            //    if (equipItem.Item != null && equipItem.Item.slotName == SlotName.no)
            //            //    equipItem.Item = itemLoad;

            //            //if (equipItem.Item != null && equipItem.Item.slotName != item[0].slotName) return;
            //            //equipItem.Item = itemLoad;
                       
            //        }
            //    }
            //    else
            //    {
            //        //Debug.LogError("[❌] equipItem hoặc equipItem.Item bị null!");
            //        //equipItem.Item = itemLoad;
            //    }

            //}

            //// Bật/tắt PanelPropertiesDamac dựa vào kết quả kiểm tra
            ////EquipmentManager.Singleton.PanelPropertiesDamac.SetActive(itemExists);


            //// Ghi log kết quả
            ////Debug.Log($"Kết quả kiểm tra: {(itemExists ? "Item đã tồn tại" : "Item chưa có")} - itemExists: {itemExists}");

           




            
        }

    }
    public void SetTargetEpchiso()
    {
        Epchiso.Singleton.slotName = slotName;
        Epchiso.Singleton.chisotinh.damegoc = 0;
        Epchiso.Singleton.chisotinh.hpgoc = 0;
        Epchiso.Singleton.chisotinh.mpgoc = 0;
        Epchiso.Singleton.chisotinh.hutkigoc = 0;
        Epchiso.Singleton.chisotinh.hutmaugoc =0;
        Epchiso.Singleton.chisotinh.negoc =0;
        Epchiso.Singleton.chisotinh.chimanggoc = 0;
        if (slotName == SlotName.da)
        {
            Epchiso.Singleton.itemDA = item.ToArray();
            Epchiso.Singleton.vitriitemDA = vitriitem;
           

            Epchiso.Singleton.chisotinh.damegoc += item[0].GetParameterValue(ItemParama.TANCONG);
            Epchiso.Singleton.chisotinh.hpgoc += item[0].GetParameterValue(ItemParama.HP);
            Epchiso.Singleton.chisotinh.mpgoc += item[0].GetParameterValue(ItemParama.MP);
            Epchiso.Singleton.chisotinh.hutkigoc += item[0].GetParameterValue(ItemParama.HUTKHI);
            Epchiso.Singleton.chisotinh.hutmaugoc += item[0].GetParameterValue(ItemParama.HUTMAU);
            Epchiso.Singleton.chisotinh.chimanggoc += item[0].GetParameterValue(ItemParama.CHIMANG);
            Epchiso.Singleton.chisotinh.negoc += item[0].GetParameterValue(ItemParama.NETRANH);

            Epchiso.Singleton.chisotinh.dame = 0;
            Epchiso.Singleton.chisotinh.hp = 0;
            Epchiso.Singleton.chisotinh.mp = 0;
            Epchiso.Singleton.chisotinh.hutki = 0;
            Epchiso.Singleton.chisotinh.hutmau = 0;
            Epchiso.Singleton.chisotinh.chimang = 0;
            Epchiso.Singleton.chisotinh.ne = 0;
            Epchiso.Singleton.solanepsao = solanepsao;
        }
        else if (slotName == SlotName.no )         
        {
            //Debug.Log("Không hợp lệ.");
        }
        else
        {
            Epchiso.Singleton.itemTB = item.ToArray();
            Epchiso.Singleton.vitriitemTB = vitriitem;

            Epchiso.Singleton.chisotinh.damegoc += item[0].GetParameterValue(ItemParama.TANCONG);
            Epchiso.Singleton.chisotinh.hpgoc += item[0].GetParameterValue(ItemParama.HP);
            Epchiso.Singleton.chisotinh.mpgoc += item[0].GetParameterValue(ItemParama.MP);
            Epchiso.Singleton.chisotinh.hutkigoc += item[0].GetParameterValue(ItemParama.HUTKHI);
            Epchiso.Singleton.chisotinh.hutmaugoc += item[0].GetParameterValue(ItemParama.HUTMAU);
            Epchiso.Singleton.chisotinh.chimanggoc += item[0].GetParameterValue(ItemParama.CHIMANG);
            Epchiso.Singleton.chisotinh.negoc += item[0].GetParameterValue(ItemParama.NETRANH);


            // chỉ số 
            Epchiso.Singleton.chisotinh.dame = dame;
            Epchiso.Singleton.chisotinh.hp = hp;
            Epchiso.Singleton.chisotinh.mp = mp;
            Epchiso.Singleton.chisotinh.hutki = hutki;
            Epchiso.Singleton.chisotinh.hutmau = hutmau;
            Epchiso.Singleton.chisotinh.chimang = chimang;
            Epchiso.Singleton.chisotinh.ne = ne;
            Epchiso.Singleton.solanepsao = solanepsao;
        }
       


    }
    public void SetTargetTachsao()
    {
        TachEpsao.Singleton.slotName = slotName;
        TachEpsao.Singleton.chisotinh.damegoc = 0;
        TachEpsao.Singleton.chisotinh.hpgoc = 0;
        TachEpsao.Singleton.chisotinh.mpgoc = 0;
        TachEpsao.Singleton.chisotinh.hutkigoc = 0;
        TachEpsao.Singleton.chisotinh.hutmaugoc = 0;
        TachEpsao.Singleton.chisotinh.chimanggoc = 0;
        if (slotName == SlotName.bua)
        {
            TachEpsao.Singleton.itemBUA = item.ToArray();
            TachEpsao.Singleton.vitriitemBUA = vitriitem;


            TachEpsao.Singleton.chisotinh.damegoc += item[0].GetParameterValue(ItemParama.TANCONG);
            TachEpsao.Singleton.chisotinh.hpgoc += item[0].GetParameterValue(ItemParama.HP);
            TachEpsao.Singleton.chisotinh.mpgoc += item[0].GetParameterValue(ItemParama.MP);
            TachEpsao.Singleton.chisotinh.hutkigoc += item[0].GetParameterValue(ItemParama.HUTKHI);
            TachEpsao.Singleton.chisotinh.hutmaugoc += item[0].GetParameterValue(ItemParama.HUTMAU);
            TachEpsao.Singleton.chisotinh.chimanggoc += item[0].GetParameterValue(ItemParama.CHIMANG);
            TachEpsao.Singleton.chisotinh.negoc += item[0].GetParameterValue(ItemParama.NETRANH);

            //TachEpsao.Singleton.chisotinh.dame = 0;
            //TachEpsao.Singleton.chisotinh.hp = 0;
            //TachEpsao.Singleton.chisotinh.mp = 0;
            //TachEpsao.Singleton.chisotinh.hutki = 0;
            //TachEpsao.Singleton.chisotinh.hutmau = 0;
            //TachEpsao.Singleton.chisotinh.chimang = 0;
            //TachEpsao.Singleton.solanepsao = solanepsao;

        }
        else if (slotName == SlotName.no)
        {
            //Debug.Log("Không hợp lệ.");
        }
        else
        {
            TachEpsao.Singleton.itemTB = item.ToArray();
            TachEpsao.Singleton.vitriitemTB = vitriitem;

            TachEpsao.Singleton.chisotinh.damegoc += item[0].GetParameterValue(ItemParama.TANCONG);
            TachEpsao.Singleton.chisotinh.hpgoc += item[0].GetParameterValue(ItemParama.HP);
            TachEpsao.Singleton.chisotinh.mpgoc += item[0].GetParameterValue(ItemParama.MP);
            TachEpsao.Singleton.chisotinh.hutkigoc += item[0].GetParameterValue(ItemParama.HUTKHI);
            TachEpsao.Singleton.chisotinh.hutmaugoc += item[0].GetParameterValue(ItemParama.HUTMAU);
            TachEpsao.Singleton.chisotinh.chimanggoc += item[0].GetParameterValue(ItemParama.CHIMANG);
            TachEpsao.Singleton.chisotinh.negoc += item[0].GetParameterValue(ItemParama.NETRANH);


            // chỉ số 
            TachEpsao.Singleton.chisotinh.dame = dame;
            TachEpsao.Singleton.chisotinh.hp = hp;
            TachEpsao.Singleton.chisotinh.mp = mp;
            TachEpsao.Singleton.chisotinh.hutki = hutki;
            TachEpsao.Singleton.chisotinh.hutmau = hutmau;
            TachEpsao.Singleton.chisotinh.chimang = chimang;
            TachEpsao.Singleton.chisotinh.ne = ne;
            TachEpsao.Singleton.solanepsao = solanepsao;
            
        }



    }

    public void SetTargetChest()
    {
        ChestManager.Singleton.SetTarget(gameObject);
        ChestManager.Singleton.item = item.ToArray();
        ChestManager.Singleton.vitriitem = vitriitem;
        ChestManager.Singleton.star = stars;
       // ChestManager.Singleton.level = level;
        ChestManager.Singleton.status = status;
        ChestManager.Singleton.quantity = quantity;
        ChestManager.Singleton.p_thao.SetActive(false);
        ChestManager.Singleton.p_cat.SetActive(true);
        ChestManager.Singleton.level = level;
        ChestManager.Singleton.quantity = quantity;
        ChestManager.Singleton.dame = dame;
        ChestManager.Singleton.hp = hp;
        ChestManager.Singleton.mp = mp;
        ChestManager.Singleton.hutki = hutki;
        ChestManager.Singleton.hutmau = hutmau;
        ChestManager.Singleton.chimang = chimang;
        ChestManager.Singleton.ne = ne;
        ChestManager.Singleton.doben = doben;
        ChestManager.Singleton.star = stars;
        ChestManager.Singleton.solanepsao = solanepsao;

        EquipmentManager.Singleton.item = item.ToArray();
        EquipmentManager.Singleton.vitriitem = vitriitem;
        EquipmentManager.Singleton.itemParama = slotName.ToString();
        EquipmentManager.Singleton.itemType = LoaiItemType;
        EquipmentManager.Singleton.slotNameMac = slotName.ToString();
        EquipmentManager.Singleton.level = level;
        EquipmentManager.Singleton.quantity = quantity;
        EquipmentManager.Singleton.dameshow = dame;
        EquipmentManager.Singleton.hpshow = hp;
        EquipmentManager.Singleton.mpshow = mp;
        EquipmentManager.Singleton.hutkishow = hutki;
        EquipmentManager.Singleton.hutmaushow = hutmau;
        EquipmentManager.Singleton.chimangshow = chimang;
        EquipmentManager.Singleton.neshow = ne;
        EquipmentManager.Singleton.dobenshow = doben;
        EquipmentManager.Singleton.solanepsao = solanepsao;
        itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
        if (itemType == ItemType.Consumable)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
            if (itemBoxReward.Count > 0)
            {
                for (int i = 0; i < itemBoxReward.Count; i++)
                {
                    //boxMessage += $"{itemBoxReward[i].itemName}: +{parameterValue[i]}.\n";
                    boxMessage += $"\n{itemBoxReward[i].itemName}.\n";
                }

            }
            else
            {


            }
        }
        else if (itemType == ItemType.Equipment)
        {
            if (itemType == ItemType.Equipment)
            {
                if (doben > 0)
                {

                    itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
                    itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
                    itemProfileUI1.ButtonSudung.gameObject.SetActive(true);

                }
                else if (doben <= 0)
                {
                    itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(true);
                    itemProfileUI1.ButtonGiahan.gameObject.SetActive(true);
                    itemProfileUI1.ButtonSudung.gameObject.SetActive(false);
                    itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
                    if (item != null)
                    {
                        //itemDoben.ShowItemDetails(item[0], BoderAnhcap, level, quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau, ne, solanepsao, doben, boxMessage);
                    }
                    else
                    {
                        //itemDoben.HideItemDetails();
                    }


                }
            }
           
        }
        else if (itemType == ItemType.Material)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(false);

        }
        else if (itemType == ItemType.Teleport)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);

        }
    }
    public void SetTargetLayChest()
    {
        ChestManager.Singleton.SetTarget(gameObject);
        ChestManager.Singleton.item = item.ToArray();
        ChestManager.Singleton.vitriitem = vitriitem;
        ChestManager.Singleton.star = stars;
       // ChestManager.Singleton.level = level;
        ChestManager.Singleton.status = status;
        ChestManager.Singleton.quantity = quantity;
        ChestManager.Singleton.p_thao.SetActive(true);
        ChestManager.Singleton.p_cat.SetActive(false);
        ChestManager.Singleton.level = level;
        ChestManager.Singleton.quantity = quantity;
        ChestManager.Singleton.dame = dame;
        ChestManager.Singleton.hp = hp;
        ChestManager.Singleton.mp = mp;
        ChestManager.Singleton.hutki = hutki;
        ChestManager.Singleton.hutmau = hutmau;
        ChestManager.Singleton.chimang = chimang;
        ChestManager.Singleton.ne = ne;
        ChestManager.Singleton.doben = doben;
        ChestManager.Singleton.solanepsao = solanepsao;
        EquipmentManager.Singleton.itemParama = slotName.ToString();
        EquipmentManager.Singleton.itemType = LoaiItemType;
        EquipmentManager.Singleton.slotNameMac = slotName.ToString();
        EquipmentManager.Singleton.level = level;
        EquipmentManager.Singleton.quantity = quantity;
        EquipmentManager.Singleton.dameshow = dame;
        EquipmentManager.Singleton.hpshow = hp;
        EquipmentManager.Singleton.mpshow = mp;
        EquipmentManager.Singleton.hutkishow = hutki;
        EquipmentManager.Singleton.hutmaushow = hutmau;
        EquipmentManager.Singleton.chimangshow = chimang;
        EquipmentManager.Singleton.neshow = ne;
        EquipmentManager.Singleton.dobenshow = doben;
        EquipmentManager.Singleton.solanepsao = solanepsao;
        itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
        if (itemType == ItemType.Consumable)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
            if (itemBoxReward.Count > 0)
            {
                for (int i = 0; i < itemBoxReward.Count; i++)
                {
                    //boxMessage += $"{itemBoxReward[i].itemName}: +{parameterValue[i]}.\n";
                    boxMessage += $"\n{itemBoxReward[i].itemName}.\n";
                }

            }
            else
            {


            }
        }
        else if (itemType == ItemType.Equipment)
        {
            if (itemType == ItemType.Equipment)
            {
                if (doben > 0)
                {

                    itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
                    itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
                    itemProfileUI1.ButtonSudung.gameObject.SetActive(true);

                }
                else if (doben <= 0)
                {
                    itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(true);
                    itemProfileUI1.ButtonGiahan.gameObject.SetActive(true);
                    itemProfileUI1.ButtonSudung.gameObject.SetActive(false);
                    itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
                    if (item != null)
                    {
                        //itemDoben.ShowItemDetails(item[0], BoderAnhcap, level, quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau, ne, solanepsao, doben, boxMessage);
                    }
                    else
                    {
                        //itemDoben.HideItemDetails();
                    }


                }
            }
            else if (itemType == ItemType.Consumable)
            {
                itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
                itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
                itemProfileUI1.ButtonSudung.gameObject.SetActive(true);

            }
        }


    }
    public void SetTargetInvensHOP()
    {
        ShopInventory.Singleton.SetTarget(gameObject);
        ShopInventory.Singleton.item = item.ToArray();
        ShopInventory.Singleton.vitriitem = vitriitem;
        ShopInventory.Singleton.level = level;
        ShopInventory.Singleton.giaban = giaban;
        ShopInventory.Singleton.txtGiaban.text = "Bán với giá " + giaban + " Vàng";
        EquipmentManager.Singleton.itemParama = slotName.ToString();
        EquipmentManager.Singleton.itemType = LoaiItemType;
        EquipmentManager.Singleton.slotNameMac = slotName.ToString();
        EquipmentManager.Singleton.level = level;
        EquipmentManager.Singleton.quantity = quantity;
        EquipmentManager.Singleton.dameshow = dame;
        EquipmentManager.Singleton.hpshow = hp;
        EquipmentManager.Singleton.mpshow = mp;
        EquipmentManager.Singleton.hutkishow = hutki;
        EquipmentManager.Singleton.hutmaushow = hutmau;
        EquipmentManager.Singleton.chimangshow = chimang;
        EquipmentManager.Singleton.neshow = ne;
        EquipmentManager.Singleton.dobenshow = doben;
        EquipmentManager.Singleton.solanepsao = solanepsao;
        itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
        if (itemType == ItemType.Consumable)
        {
            itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
            itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
            itemProfileUI1.ButtonSudung.gameObject.SetActive(true);
            if (itemBoxReward.Count > 0)
            {
                for (int i = 0; i < itemBoxReward.Count; i++)
                {
                    //boxMessage += $"{itemBoxReward[i].itemName}: +{parameterValue[i]}.\n";
                    boxMessage += $"\n{itemBoxReward[i].itemName}.\n";
                }

            }
            else
            {


            }
        }
        else if (itemType == ItemType.Equipment)
        {
            if (itemType == ItemType.Equipment)
            {
                if (doben > 0)
                {

                    itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
                    itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
                    itemProfileUI1.ButtonSudung.gameObject.SetActive(true);

                }
                else if (doben <= 0)
                {
                    itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(true);
                    itemProfileUI1.ButtonGiahan.gameObject.SetActive(true);
                    itemProfileUI1.ButtonSudung.gameObject.SetActive(false);
                    itemProfileUI1 = FindObjectOfType<ItemProfileUI>();
                    if (item != null)
                    {
                        //itemDoben.ShowItemDetails(item[0], BoderAnhcap, level, quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau, ne, solanepsao, doben, boxMessage);
                    }
                    else
                    {
                        //itemDoben.HideItemDetails();
                    }


                }
            }
            else if (itemType == ItemType.Consumable)
            {
                itemProfileUI1.ExpireditemsIcon.gameObject.SetActive(false);
                itemProfileUI1.ButtonGiahan.gameObject.SetActive(false);
                itemProfileUI1.ButtonSudung.gameObject.SetActive(true);

            }
        }
    }

    public void OnItemClicked(ItemSlot itemSlot)
    {
        itemProfileUI1 = FindObjectOfType<ItemProfileUI>();

        if (itemSlot.item != null)
        {
            itemProfileUI1.ShowItemDetails(itemSlot.item, BoderAnhcap,level, itemSlot.quantity, stars,itemType,status,levelyc,dame,hp,mp,chimang,hutki,hutmau,ne,solanepsao,doben,boxMessage);
        }
        else
        {
            itemProfileUI1.HideItemDetails();
        }
    }
    public void OnItemClickedNangcapcs(ItemSlot itemSlot)
    {
        EpchisoUI = FindObjectOfType<EpchisoUI>();

        if (itemSlot.item != null)
        {
            EpchisoUI.ShowItemDetails(itemSlot.item, level, itemSlot.quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau,ne,solanepsao);
        }
        else
        {
            EpchisoUI.HideItemDetails();
        }
    }
    public void OnItemClickedTachsaos(ItemSlot itemSlot)
    {
        tachEpsao = FindObjectOfType<TachepsaoUI>();

        if (itemSlot.item != null)
        {
            tachEpsao.ShowItemDetails(itemSlot.item, level, itemSlot.quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau,ne, solanepsao);
        }
        else
        {
            tachEpsao.HideItemDetails();
        }
    }
    public void OnItemClickedChest(ChestSlot itemSlot)
    {
        itemProfileUI1 = FindObjectOfType<ItemProfileUI>();

        if (itemSlot.item != null)
        {
            itemProfileUI1.ShowItemDetails(itemSlot.item, BoderAnhcap, level, itemSlot.quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau,ne,solanepsao,doben,boxMessage);
        }
        else
        {
            itemProfileUI1.HideItemDetails();
        }
    }
    public void OnItemClickedLayChest(ChestSlot itemSlot)
    {
        chestProfile = FindObjectOfType<ChestProfile>();

        if (itemSlot.item != null)
        {
            chestProfile.ShowItemDetails(itemSlot.item, level, itemSlot.quantity, stars, itemType, status, levelyc, dame, hp, mp, chimang, hutki, hutmau,ne,solanepsao);
        }
        else
        {
            chestProfile.HideItemDetails();
        }
    }

    internal void HideArrow()
    {
        //Image imageBoder = gameObject.GetComponent<Image>();
        BoderSelect.sprite = BoderImageDefault;
        //BoderSelect.gameObject.SetActive(false);
    }

    internal void ShowArrow()
    {
        //Image imageBoder = gameObject.GetComponent<Image>();
        BoderSelect.sprite = BoderImageSelect;
        //BoderSelect.gameObject.SetActive(true);

    }
}
