using UnityEngine;
using UnityEngine.UI;
//using PlayFab;
//using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Collections;
using System;

public class EquipmentManagerGame : MonoBehaviour
{
    //public static EquipmentManagerGame Singleton;
    //public PlayerEquipment playerEquipment;
    //public EquipmentUIManager equipmentUIManager;
    //public PlayerStats playerStats;
    ////public Equipment equipment1;
    //public string idItem;
    //public string ItemInstanceId;
    //public string nameItem;
    //public string mota;
    //public string contentData;
    //public GameObject PanelThao;
    //public Text txtContent;
    //public Text txtNameItem;
    //public Image IconItem;
    //public Image[] imagesItemMac;
    //public ItemEquipment itemEquipment;
    //private void Start()
    //{
    //    LoadEquipment();
    //}
    //private void Awake()
    //{

    //    if (Singleton == null) // kiểm tra xem đã tồn tại chưa,nếu chưa
    //    {
    //        Singleton = this;
    //    }
    //    else { }
        
    //}

    //public void MacDau()
    //{
    //    EquipItem(playerEquipment.DAU, "armor");
    //}
    //public void MacVulhi()
    //{

    //    EquipItem(playerEquipment.VUKHI, "weapon");

    //}
    //public void Maccanh()
    //{
    //    EquipItem(playerEquipment.WING, "wing");

    //}
    //public void Macset()
    //{
    //    EquipItem(playerEquipment.SET, "set");

    //}
    //public void ThaoVK()
    //{
    //    UnequipItem( "weapon");

    //}
    //public void ThaoDau()
    //{
    //    UnequipItem("armor");

    //}
    //public void ThaoSet()
    //{
    //    UnequipItem("set");

    //}
    //public void SetDataUnequipItem(string id)
    //{
    //    //ItemEquipment.Singleton.itemType = itemType;
    //    PanelThao.SetActive(true);
    //    ItemProties.Singleton.PANEL_TT.SetActive(false);
    //    switch (id)
    //    {
    //        case "dau":
    //            itemEquipment.itemType = SlotName.ao;
    //            txtContent.text = playerEquipment.DAU.contentData;
    //            txtNameItem.text = playerEquipment.DAU.nameItem;
    //            IconItem.sprite = imagesItemMac[0].sprite;
    //            break;

    //        case "vukhi":
    //            itemEquipment.itemType = SlotName.quan;
    //            txtContent.text = playerEquipment.VUKHI.contentData;
    //            txtNameItem.text = playerEquipment.VUKHI.nameItem;
    //            IconItem.sprite = imagesItemMac[5].sprite;
    //            break;
    //        case "wing":
    //            itemEquipment.itemType = SlotName.gang;
    //            txtContent.text = playerEquipment.WING.contentData;
    //            txtNameItem.text = playerEquipment.WING.nameItem;
    //            IconItem.sprite = imagesItemMac[6].sprite;
    //            break;
    //        case "set":
    //            itemEquipment.itemType = SlotName.giay;
    //            txtContent.text = playerEquipment.SET.contentData;
    //            txtNameItem.text = playerEquipment.SET.nameItem;
    //            IconItem.sprite = imagesItemMac[7].sprite;
    //            break;
    //        case "rada":
    //            itemEquipment.itemType = SlotName.rada;
    //            txtContent.text = playerEquipment.SET.contentData;
    //            txtNameItem.text = playerEquipment.SET.nameItem;
    //            IconItem.sprite = imagesItemMac[7].sprite;
    //            break;
    //        // You can add more cases for other item types if needed
    //        default:
    //            //Debug.LogWarning("Item type not recognized: " + ItemEquipment.Singleton.itemType);
    //            break;
    //    }
    //}
    //public void SetTBUnequipItem()
    //{
    //    //switch (ItemEquipment.Singleton.itemType)
    //    //{
    //    //    case ItemType.VUKHI:
    //    //        SetItemAttackValue(playerEquipment.VUKHI.ItemInstanceId);
    //    //        ItemContent.Singleton.LoadAll();
    //    //        UnequipItem("weapon");

               

    //    //        break;

    //    //    case ItemType.DAU:
    //    //        SetItemStatuskValue(playerEquipment.DAU.ItemInstanceId);
    //    //        ItemContent.Singleton.LoadAll();
    //    //        UnequipItem("armor");
               

    //    //        break;
    //    //    case ItemType.WING:
    //    //        SetItemAttackValue(playerEquipment.WING.ItemInstanceId);
    //    //        ItemContent.Singleton.LoadAll();
    //    //        UnequipItem("wing");

    //    //        break;
    //    //             case ItemType.SET:
    //    //        SetItemAttackValue(playerEquipment.SET.ItemInstanceId);
    //    //        ItemContent.Singleton.LoadAll();
    //    //        UnequipItem("set");

    //    //        break;
    //    //    // You can add more cases for other item types if needed
    //    //    default:
    //    //        //Debug.LogWarning("Item type not recognized: " + ItemEquipment.Singleton.itemType);
    //    //        break;
    //    //}

    //}
    

    //private void OnError(PlayFabError obj)
    //{
       
    //}

    
    

    // Mặc trang bị
    //public void EquipItem(EquimentGame equipment, string type)
    //{
    //    switch (type)
    //    {
    //        case "armor":
    //            playerEquipment.DAU = equipment;
    //            playerEquipment.DAU.itemId = idItem;
    //            playerEquipment.DAU.nameItem = nameItem;
    //            playerEquipment.DAU.ItemInstanceId = ItemInstanceId;
    //            playerEquipment.DAU.mota = mota;
    //            playerEquipment.DAU.contentData = contentData;
    //            playerEquipment.DAU.mau = playerStats.mau;
    //            playerEquipment.DAU.mp = playerStats.mp;
    //            playerEquipment.DAU.phongthu = playerStats.phongthu;
    //            playerEquipment.DAU.tocdo = playerStats.tocdo;
    //            playerEquipment.DAU.crit = playerStats.crit;
    //            playerStats.EquipItem(equipment);
    //            //PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        case "gloves":
    //            playerEquipment.THAN = equipment;
    //            playerEquipment.THAN.itemId = idItem;
    //            //playerStats.EquipItem(equipment);
    //            break;
    //        case "shoes":
    //            playerEquipment.NON = equipment;
    //            playerEquipment.NON.itemId = idItem;
    //            //playerStats.EquipItem(equipment);
    //            break;
    //        case "weapon":
    //            playerEquipment.VUKHI = equipment;
    //            playerEquipment.VUKHI.itemId = idItem;
    //            playerEquipment.VUKHI.nameItem = nameItem;
    //            playerEquipment.VUKHI.ItemInstanceId = ItemInstanceId;
    //            playerEquipment.VUKHI.mota = mota;
    //            playerEquipment.VUKHI.contentData = contentData;
    //            playerEquipment.VUKHI.tancong = playerStats.tancong;
    //            playerEquipment.VUKHI.mau = playerStats.mau;
    //            playerEquipment.VUKHI.mp = playerStats.mp;
    //            playerEquipment.VUKHI.phongthu = playerStats.phongthu;
    //            playerEquipment.VUKHI.tocdo = playerStats.tocdo;
    //            playerEquipment.VUKHI.crit = playerStats.crit;
    //            playerStats.EquipItem(equipment);
    //            //PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        case "wing":
    //            playerEquipment.WING = equipment;
    //            playerEquipment.WING.itemId = idItem;
    //            playerEquipment.WING.nameItem = nameItem;
    //            playerEquipment.WING.ItemInstanceId = ItemInstanceId;
    //            playerEquipment.WING.mota = mota;
    //            playerEquipment.WING.contentData = contentData;
    //            playerEquipment.WING.tancong = playerStats.tancong;
    //            playerEquipment.WING.mau = playerStats.mau;
    //            playerEquipment.WING.mp = playerStats.mp;
    //            playerEquipment.WING.phongthu = playerStats.phongthu;
    //            playerEquipment.WING.tocdo = playerStats.tocdo;
    //            playerEquipment.WING.crit = playerStats.crit;
    //            playerStats.EquipItem(equipment);
    //           // PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        case "set":
    //            playerEquipment.SET = equipment;
    //            playerEquipment.SET.itemId = idItem;
    //            playerEquipment.SET.nameItem = nameItem;
    //            playerEquipment.SET.ItemInstanceId = ItemInstanceId;
    //            playerEquipment.SET.mota = mota;
    //            playerEquipment.SET.contentData = contentData;
    //            playerEquipment.SET.tancong = playerStats.tancong;
    //            playerEquipment.SET.mau = playerStats.mau;
    //            playerEquipment.SET.mp = playerStats.mp;
    //            playerEquipment.SET.phongthu = playerStats.phongthu;
    //            playerEquipment.SET.tocdo = playerStats.tocdo;
    //            playerEquipment.SET.crit = playerStats.crit;
    //            playerStats.EquipItem(equipment);
    //           // PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        default:
    //            Debug.LogWarning("Loại trang bị không hợp lệ: " + type);
    //            break;
    //    }
    //    //ItemContent.Singleton.LoadAll();
    //    // Cập nhật giao diện trang bị
       

    //    //equipmentUIManager.UpdateEquipmentSlots(playerEquipment);
    //    SaveEquipment();
    //    StartCoroutine(timeLoad());
    //}
    //private Equipment FindEquipmentById(string itemId)
    //{
    //    // Giả định rằng bạn có một danh sách items chứa các trang bị trong equipmentUIManager
    //    foreach (var item in equipmentUIManager.items.Items)
    //    {
    //        if (item.Iditem == itemId) // Kiểm tra ID trang bị
    //        {
    //            break;
    //        }
    //    }
    //    return null; // Trả về null nếu không tìm thấy
    //}

    
    private IEnumerator timeLoad()
    {
        yield return new WaitForSeconds(2.2f);
        //PanelCheckNetwork.Singleton.PANEL_LOADTIN.SetActive(false);
        //LoadEquipment();
    }
    // Tháo trang bị
    //public void UnequipItem(string type)
    //{
    //    switch (type)
    //    {
    //        case "armor":
    //            //playerStats.UnequipItem(playerEquipment.armor);
    //            playerEquipment.DAU.mau -= playerStats.mau;
    //            playerEquipment.DAU.mp -= playerStats.mp;
    //            playerEquipment.DAU.phongthu -= playerStats.phongthu;
    //            playerEquipment.DAU.tocdo -= playerStats.tocdo;
    //            playerEquipment.DAU.crit -= playerStats.crit;
    //            Debug.Log("Unequipping armor: " + playerEquipment.DAU); // Kiểm tra xem armor có bị xóa không
    //            playerEquipment.DAU = null;
    //            //PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        case "gloves":
    //            playerStats.UnequipItem(playerEquipment.THAN);
    //            Debug.Log("Unequipping gloves: " + playerEquipment.THAN); // Kiểm tra xem gloves có bị xóa không
    //            playerEquipment.THAN = null;
    //          //  PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        case "shoes":
    //            playerStats.UnequipItem(playerEquipment.NON);
    //            Debug.Log("Unequipping shoes: " + playerEquipment.NON); // Kiểm tra xem shoes có bị xóa không
    //            playerEquipment.NON = null;
    //          //  PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        case "weapon":
    //            //playerStats.UnequipItem(playerEquipment.weapon);
    //            playerEquipment.VUKHI.tancong -= playerStats.tancong;
    //            playerEquipment.VUKHI.mau -= playerStats.mau;
    //            playerEquipment.VUKHI.mp -= playerStats.mp;
    //            playerEquipment.VUKHI.phongthu -= playerStats.phongthu;
    //            playerEquipment.VUKHI.tocdo -= playerStats.tocdo;
    //            playerEquipment.VUKHI.crit -= playerStats.crit;
    //            Debug.Log("Unequipping weapon: " + playerEquipment.VUKHI); // Kiểm tra xem weapon có bị xóa không
    //            playerEquipment.VUKHI = null;
    //          //  PlayerMoveGame.Singleton.dataUser();

    //            break;
    //        case "wing":
    //            //playerStats.UnequipItem(playerEquipment.weapon);
    //            playerEquipment.WING.tancong -= playerStats.tancong;
    //            playerEquipment.WING.mau -= playerStats.mau;
    //            playerEquipment.WING.mp -= playerStats.mp;
    //            playerEquipment.WING.phongthu -= playerStats.phongthu;
    //            playerEquipment.WING.tocdo -= playerStats.tocdo;
    //            playerEquipment.WING.crit -= playerStats.crit;
    //            Debug.Log("Unequipping weapon: " + playerEquipment.WING); // Kiểm tra xem weapon có bị xóa không
    //            playerEquipment.WING = null;
    //          //  PlayerMoveGame.Singleton.dataUser();
    //            break;
    //        case "set":
    //            //playerStats.UnequipItem(playerEquipment.weapon);
    //            playerEquipment.SET.tancong -= playerStats.tancong;
    //            playerEquipment.SET.mau -= playerStats.mau;
    //            playerEquipment.SET.mp -= playerStats.mp;
    //            playerEquipment.SET.phongthu -= playerStats.phongthu;
    //            playerEquipment.SET.tocdo -= playerStats.tocdo;
    //            playerEquipment.SET.crit -= playerStats.crit;
    //            Debug.Log("Unequipping weapon: " + playerEquipment.SET); // Kiểm tra xem weapon có bị xóa không
    //            playerEquipment.SET = null;
    //          //  PlayerMoveGame.Singleton.dataUser();
    //            break;
    //    }

      

    //    // Cập nhật giao diện sau khi tháo trang bị
    //   // equipmentUIManager.UpdateEquipmentSlots(playerEquipment);
    //    SaveEquipment();
    //    StartCoroutine(timeLoad());
    //}

    public void LoadItem()
    {
        //equipmentUIManager.UpdateEquipmentSlots(playerEquipment);
    }
    // Lưu trạng thái trang bị vào PlayFab
    public void SaveEquipment()
    {
        // Kiểm tra và loại bỏ các giá trị null
        Dictionary<string, object> equipmentData = new Dictionary<string, object>();

        //if (playerEquipment.armor != null)
        //{
        //    equipmentData.Add("armor", playerEquipment.armor);
        //}

        //if (playerEquipment.gloves != null)
        //{
        //    equipmentData.Add("gloves", playerEquipment.gloves);
        //}

        //if (playerEquipment.shoes != null)
        //{
        //    equipmentData.Add("shoes", playerEquipment.shoes);
        //}

        //if (playerEquipment.weapon != null)
        //{
        //    equipmentData.Add("weapon", playerEquipment.weapon);
        //}

        //string equipmentJson = JsonUtility.ToJson(playerEquipment);

        //var request = new UpdateUserDataRequest
        //{
        //    Data = new Dictionary<string, string>
        //    {
        //        { "PlayerEquipment", equipmentJson }
        //    }
        //};

        //PlayFabClientAPI.UpdateUserData(request,
        //    result =>
        //    {

        //    },
        //    error => {
            
                       
        //    }) ;
    }

    // Tải trạng thái trang bị từ PlayFab
    //public void LoadEquipment()
    //{
    //    PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
    //        result =>
    //        {
    //            if (result.Data != null && result.Data.ContainsKey("PlayerEquipment"))
    //            {
    //                string equipmentJson = result.Data["PlayerEquipment"].Value;
    //                playerEquipment = JsonUtility.FromJson<PlayerEquipment>(equipmentJson);
    //                //equipmentUIManager.UpdateEquipmentSlots(playerEquipment);
    //            }
    //        },
    //        error => Debug.LogError("Failed to load equipment data: " + error.ErrorMessage));
    //}

   
}
