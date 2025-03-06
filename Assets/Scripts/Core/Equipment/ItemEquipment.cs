using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipment : MonoBehaviour
{
    public static ItemEquipment Singleton;
    public ItemProties itemProties; // This will be assigned per item in the Inspector
    public SlotName itemType;
    public int capdoyeucau;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {

        }
    }

    public void Equipment()
    {
        //EquipmentManagerGame.Singleton.idItem = itemProties.idItem;  // Assign the new item ID
        // Check if player meets level requirement
        //if (PlayerData.Singleton.level < capdoyeucau)
        //{
        //    Thongbao.Singleton.Message("Bạn chưa đạt cấp độ yêu cầu.");
        //}
        //else
        //{
        //    // Check if the specific item type is already equipped and remove it if so
        //    bool isAlreadyEquipped = false;
        //    switch (itemType)
        //    {
        //        case ItemType.DAU:
        //            isAlreadyEquipped = EquipmentManager.Singleton.playerEquipment.DAU.itemId == EquipmentManager.Singleton.idItem;
        //            break;
        //        case ItemType.WING:
        //            isAlreadyEquipped = EquipmentManager.Singleton.playerEquipment.WING.itemId == EquipmentManager.Singleton.idItem;
        //            break;
        //        case ItemType.VUKHI:
        //            isAlreadyEquipped = EquipmentManager.Singleton.playerEquipment.VUKHI.itemId == EquipmentManager.Singleton.idItem;
        //            break;
        //        case ItemType.SET:
        //            isAlreadyEquipped = EquipmentManager.Singleton.playerEquipment.SET.itemId == EquipmentManager.Singleton.idItem;
        //            break;
        //            // Add other cases as needed
        //    }
        //    if (isAlreadyEquipped)
        //    {
        //        Thongbao.Singleton.Message("Bạn đã mặc trang bị này.");
        //    }
        //    else
        //    {
        //        PanelCheckNetwork.Singleton.PANEL_LOADTIN.SetActive(true);
        //        switch (itemType)
        //        {
        //            case ItemType.DAU:
        //                if (EquipmentManager.Singleton.playerEquipment.DAU.itemId != null)
        //                {
        //                    // Unequip the old "DAU" item
        //                    //EquipmentManager.Singleton.SetTBUnequipItem();
        //                    if (EquipmentManager.Singleton.playerEquipment.VUKHI.itemId == null)
        //                    {
                                
        //                        Mac();

        //                    }
        //                    else
        //                    {
        //                        //EquipmentManager.Singleton.SetTBUnequipItem();
        //                        StartCoroutine(timethao(2.3f));

        //                    }
        //                    break;
        //                }
        //                break;

        //            case ItemType.WING:
        //                if (EquipmentManager.Singleton.playerEquipment.VUKHI.itemId == null)
        //                {
        //                    // Unequip the old "VUKHI" item
        //                    Mac();

        //                }
        //                else
        //                {
        //                    //EquipmentManager.Singleton.SetTBUnequipItem();
        //                    StartCoroutine(timethao(2.3f));


        //                }
        //                break;

        //            case ItemType.VUKHI:
        //                if (EquipmentManager.Singleton.playerEquipment.VUKHI.itemId == null)
        //                {
        //                    // Unequip the old "VUKHI" item
        //                    Mac();

        //                }
        //                else
        //                {
        //                    //EquipmentManager.Singleton.SetTBUnequipItem();
        //                    StartCoroutine(timethao(2.3f));

        //                }
        //                break;
        //            case ItemType.SET:
        //                if (EquipmentManager.Singleton.playerEquipment.SET.itemId == null)
        //                {
        //                    // Unequip the old "VUKHI" item
        //                    Mac();

        //                }
        //                else
        //                {
        //                    //EquipmentManager.Singleton.SetTBUnequipItem();
        //                    StartCoroutine(timethao(2.3f));

        //                }
        //                break;


        //                // Add other cases as needed for additional item types
        //        }
        //    }
            

            
        //}

        // Close the panel regardless of the result
        itemProties.PANEL_TT.SetActive(false);
    }



   IEnumerator timethao(float timerUn)
   {
        yield return new WaitForSeconds(timerUn);
        // Equip the new item
        //EquipmentManager.Singleton.idItem = itemProties.idItem;  // Assign the new item ID
        //PlayerMoveGame.Singleton.dataUser();  // Update player data

        // Handle equipping based on item type
        //switch (itemType)
        //{
        //    case ItemType.DAU:
        //        EquipmentManager.Singleton.MacDau();
        //        break;

        //    case ItemType.THAN:
        //        // Handle equipping "THAN"
        //        // EquipmentManager.Singleton.MacThan(); 
        //        break;

        //    case ItemType.MU:
        //        // Handle equipping "MU"
        //        // EquipmentManager.Singleton.MacMu(); 
        //        break;

        //    case ItemType.VUKHI:
        //        EquipmentManager.Singleton.MacVulhi();
        //        break;

        //    case ItemType.KHIEN:
        //        // Handle equipping "KHIEN"
        //        // EquipmentManager.Singleton.MacKhien(); 
        //        break;

        //    case ItemType.VATPHAM:
        //        Debug.LogWarning("Không thể trang bị vật phẩm: " + itemProties.idItem);
        //        Thongbao.Singleton.Message("Không thể mặc trang bị này.");
        //        break;

        //    case ItemType.WING:
        //        EquipmentManager.Singleton.Maccanh();
        //        break;
        //    case ItemType.SET:
        //        EquipmentManager.Singleton.Macset();
        //        break;

        //    default:
        //        Debug.LogWarning("Item không được nhận diện: " + itemProties.idItem);
        //        Thongbao.Singleton.Message("Không thể mặc trang bị này.");
        //        break;
        //}
   }

    public void Mac()
    {
        //EquipmentManager.Singleton.idItem = itemProties.idItem;  // Assign the new item ID
        //PlayerMoveGame.Singleton.dataUser();  // Update player data

        //// Handle equipping based on item type
        //switch (itemType)
        //{
        //    case ItemType.DAU:
        //        EquipmentManager.Singleton.MacDau();
        //        break;

        //    case ItemType.THAN:
        //        // Handle equipping "THAN"
        //        // EquipmentManager.Singleton.MacThan(); 
        //        break;

        //    case ItemType.MU:
        //        // Handle equipping "MU"
        //        // EquipmentManager.Singleton.MacMu(); 
        //        break;

        //    case ItemType.VUKHI:
        //        EquipmentManager.Singleton.MacVulhi();
        //        break;

        //    case ItemType.KHIEN:
        //        // Handle equipping "KHIEN"
        //        // EquipmentManager.Singleton.MacKhien(); 
        //        break;

        //    case ItemType.VATPHAM:
        //        Debug.LogWarning("Không thể trang bị vật phẩm: " + itemProties.idItem);
        //        Thongbao.Singleton.Message("Không thể mặc trang bị này.");
        //        break;

        //    case ItemType.WING:
        //        EquipmentManager.Singleton.Maccanh();
        //        break;
        //    case ItemType.SET:
        //        EquipmentManager.Singleton.Macset();
        //        break;

        //    default:
        //        Debug.LogWarning("Item không được nhận diện: " + itemProties.idItem);
        //        Thongbao.Singleton.Message("Không thể mặc trang bị này.");
        //        break;
        //}
    }

    private void EquipFishingRod()
    {
        //Debug.Log("Equipped Fishing Rod: " + itemProties.itemName);
        //// Apply behavior specific to the fishing rod
        ///
        //if(PlayerMovement.Singleton.transform.transform.position.x < NpcController.Singleton.npc_cauca_vector.transform.position.x)
        //{
        //    Thongbao.Singleton.Message("Khoảng cách câu cá quá xa. Vị trí cần tới " + NpcController.Singleton.npc_cauca_vector.transform.position.x);
        //}
        //else
        //{
        //    Thongbao.Singleton.Message("Đã tới vị trí câu.");
        //}
    }

    private void EquipSword()
    {
        //Debug.Log("Equipped Sword with Damage: " + itemProties.damage);
        //// Apply behavior specific to the sword (e.g., increase attack power)
    }

    private void UseHealthPotion()
    {
        //Debug.Log("Used Health Potion, Healing: " + itemProties.healthBoost);
        //// Apply behavior specific to the health potion (e.g., increase player's health)
    }
}
