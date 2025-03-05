using System;
using UnityEngine;

[System.Serializable]
public class PlayerEquipment
{
    public DataEquipment ao;
    public DataEquipment quan;
    public DataEquipment gang;
    public DataEquipment giay;
    public DataEquipment rada;
    public DataEquipment canh;

    public DataEquipment daychuyen;
    public DataEquipment nhan;
    public DataEquipment vukhi;
    public DataEquipment pet;
    public DataEquipment phukien;

    // Phương thức đồng bộ các chỉ số từ item
    //public void EquipItem(Item item, string slot, int damage, int hp, int mp, int chimang, int lifesteal, int manasteal)
    //{
    //    if (item == null)
    //    {
    //        Debug.LogWarning("Không có món đồ nào để trang bị.");
    //        return;
    //    }
    //    if (GameManager.Singleton.level < item.level)
    //    {
    //        Thongbao.Singleton.ShowThongbao("Bạn chưa đạt sức mạnh yêu cầu.");

    //        return;
    //    }

    //    // Cập nhật trang bị theo slot
    //    switch (slot)
    //    {
    //        case "ao":
    //            ao = new DataEquipment();
    //            ao.SyncStatsFromItem(item);
    //            ao.itemName = item.name;
    //            ao.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            ao.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;
    //            ao.SyncItemDescriptionFromItem(item);
    //            ao.itemType = item.GetItemTypeName();
    //            ao.itemParama = item.GetItemEffect();
    //            ao.Item = item;
    //            ao.level = EquipmentManager.Singleton.level;
    //            ao.dame += damage;
    //            ao.hp += hp;
    //            ao.hp += hp;
    //            ao.mp += mp;
    //            ao.chimang += chimang;
    //            ao.hutki += lifesteal;
    //            ao.hutmau += manasteal;

    //            break;
    //        case "quan":
    //            quan = new DataEquipment();
    //            quan.SyncStatsFromItem(item);
    //            quan.itemName = item.name;
    //            quan.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            quan.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            quan.SyncItemDescriptionFromItem(item);
    //            quan.itemType = item.GetItemTypeName();
    //            quan.itemParama = item.GetItemEffect();
    //            quan.Item = item;
    //            quan.level = EquipmentManager.Singleton.level;

    //            break;
    //        case "gang":
    //            gang = new DataEquipment();
    //            gang.SyncStatsFromItem(item);
    //            gang.itemName = item.name;
    //            gang.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            gang.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            gang.SyncItemDescriptionFromItem(item);
    //            gang.itemType = item.GetItemTypeName();
    //            gang.itemParama = item.GetItemEffect();
    //            gang.Item = item;
    //            gang.level = EquipmentManager.Singleton.level;


    //            break;
    //        case "giay":
    //            giay = new DataEquipment();
    //            giay.SyncStatsFromItem(item);
    //            giay.itemName = item.name;
    //            giay.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            giay.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            giay.SyncItemDescriptionFromItem(item);
    //            giay.itemType = item.GetItemTypeName();
    //            giay.itemParama = item.GetItemEffect();
    //            giay.Item = item;
    //            giay.level = EquipmentManager.Singleton.level;


    //            break;
    //        case "rada":
    //            rada = new DataEquipment();
    //            rada.SyncStatsFromItem(item);
    //            rada.itemName = item.name;
    //            rada.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            rada.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            rada.SyncItemDescriptionFromItem(item);
    //            rada.itemType = item.GetItemTypeName();
    //            rada.itemParama = item.GetItemEffect();
    //            rada.Item = item;
    //            rada.level = EquipmentManager.Singleton.level;


    //            break;
    //        case "daychuyen":
    //            daychuyen = new DataEquipment();
    //            daychuyen.SyncStatsFromItem(item);
    //            daychuyen.itemName = item.name;
    //            daychuyen.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            daychuyen.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            daychuyen.SyncItemDescriptionFromItem(item);
    //            daychuyen.itemType = item.GetItemTypeName();
    //            daychuyen.itemParama = item.GetItemEffect();
    //            daychuyen.Item = item;
    //            daychuyen.level = EquipmentManager.Singleton.level;


    //            break;
    //        case "nhan":
    //            nhan = new DataEquipment();
    //            nhan.SyncStatsFromItem(item);
    //            nhan.itemName = item.name;
    //            nhan.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            nhan.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            nhan.SyncItemDescriptionFromItem(item);
    //            nhan.itemType = item.GetItemTypeName();
    //            nhan.itemParama = item.GetItemEffect();
    //            nhan.Item = item;
    //            nhan.level = EquipmentManager.Singleton.level;
    //            break;
    //        case "vukhi":
    //            vukhi = new DataEquipment();
    //            vukhi.SyncStatsFromItem(item);
    //            vukhi.itemName = item.name;
    //            vukhi.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            vukhi.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            vukhi.SyncItemDescriptionFromItem(item);
    //            vukhi.itemType = item.GetItemTypeName();
    //            vukhi.itemParama = item.GetItemEffect();
    //            vukhi.Item = item;
    //            vukhi.level = EquipmentManager.Singleton.level;
    //            break;
    //        case "pet":
    //            pet = new DataEquipment();
    //            pet.SyncStatsFromItem(item);
    //            pet.itemName = item.name;
    //            pet.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            pet.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            pet.SyncItemDescriptionFromItem(item);
    //            pet.itemType = item.GetItemTypeName();
    //            pet.itemParama = item.GetItemEffect();
    //            pet.Item = item;
    //            pet.level = EquipmentManager.Singleton.level;
    //            break;
    //        case "phukien":
    //            phukien = new DataEquipment();
    //            phukien.SyncStatsFromItem(item);
    //            phukien.itemName = item.name;
    //            phukien.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
    //            phukien.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;

    //            phukien.SyncItemDescriptionFromItem(item);
    //            phukien.itemType = item.GetItemTypeName();
    //            phukien.itemParama = item.GetItemEffect();
    //            phukien.Item = item;
    //            phukien.level = EquipmentManager.Singleton.level;
    //            break;

    //        default:
    //            Thongbao.Singleton.ShowThongbao("Trang bị không hợp lệ.");
    //            Debug.LogWarning("Slot không hợp lệ: " + slot);
    //            return;
    //    }
    //}
    public void EquipItem(Item item, string slot, int damage, int hp, int mp, int chimang, int lifesteal, int manasteal,float ne,int solanepsao,int doben)
    {
        if (item == null)
        {
            Debug.LogWarning("Không có món đồ nào để trang bị.");
            return;
        }

        if (GameManager.Singleton.level < item.level)
        {
            Thongbao.Singleton.ShowThongbao("Bạn chưa đạt sức mạnh yêu cầu.");
            return;
        }
        if (EquipmentManager.Singleton.dobenshow <=0)
        {
            Thongbao.Singleton.ShowThongbao("Độ bền thấp hơn 0 không thể mặc.");
            return;
        }
        DataEquipment targetEquipment = null;

        switch (slot)
        {
            case "ao":
                targetEquipment = new DataEquipment();
                ao = targetEquipment;
                break;
            case "quan":
                targetEquipment = new DataEquipment();
                quan = targetEquipment;
                break;
            case "gang":
                targetEquipment = new DataEquipment();
                gang = targetEquipment;
                break;
            case "giay":
                targetEquipment = new DataEquipment();
                giay = targetEquipment;
                break;
            case "rada":
                targetEquipment = new DataEquipment();
                rada = targetEquipment;
                break;
            case "canh":
                targetEquipment = new DataEquipment();
                canh = targetEquipment;
                break;
            case "daychuyen":
                targetEquipment = new DataEquipment();
                daychuyen = targetEquipment;
                break;
            case "nhan":
                targetEquipment = new DataEquipment();
                nhan = targetEquipment;
                break;
            case "vukhi":
                targetEquipment = new DataEquipment();
                vukhi = targetEquipment;
                break;
            case "pet":
                targetEquipment = new DataEquipment();
                pet = targetEquipment;
                break;
            case "phukien":
                targetEquipment = new DataEquipment();
                phukien = targetEquipment;
                break;
            // Tiếp tục cho các slot khác
            default:
                Thongbao.Singleton.ShowThongbao("Trang bị không hợp lệ.");
                Debug.LogWarning("Slot không hợp lệ: " + slot);
                return;
        }

        // Đồng bộ dữ liệu
        targetEquipment.SyncStatsFromItem(item);
        targetEquipment.itemName = item.itemName;
        targetEquipment.itemNameScriptObject = item.name;
        
        if(InventoryUI.Singleton != null)
        {
            targetEquipment.stars = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].stars;
            targetEquipment.status = Inventory.Singleton.items[InventoryUI.Singleton.vitriitem].status;
            //return;
        }
        targetEquipment.stars = Inventory.Singleton.items[EquipmentManager.Singleton.vitriitem].stars;
        targetEquipment.status = Inventory.Singleton.items[EquipmentManager.Singleton.vitriitem].status;


        targetEquipment.SyncItemDescriptionFromItem(item);
        targetEquipment.itemType = item.GetItemTypeName();
        targetEquipment.itemParama = item.GetItemEffect();
        targetEquipment.Item = item;
        targetEquipment.level = EquipmentManager.Singleton.level;
        targetEquipment.solanepsao = solanepsao;

        // Cập nhật chỉ số
        //targetEquipment.dame = damage;
        //targetEquipment.hp = hp;
        //targetEquipment.mp = mp;
        //targetEquipment.chimang = chimang;
        //targetEquipment.hutki = lifesteal;
        //targetEquipment.hutmau = manasteal;
        //Cộng chỉ số nâng cấp

        targetEquipment.dameshow = 0;
        targetEquipment.hpshow = 0;
        targetEquipment.mpshow = 0;
        targetEquipment.chimangshow = 0;
        targetEquipment.hutkishow = 0;
        targetEquipment.hutmaushow = 0;
        targetEquipment.neshow = 0;
        targetEquipment.dobenshow = 0;

        targetEquipment.dameshow = damage;
        targetEquipment.hpshow = hp;
        targetEquipment.mpshow = mp;
        targetEquipment.chimangshow = chimang;
        targetEquipment.hutkishow = lifesteal;
        targetEquipment.hutmaushow = manasteal;
        targetEquipment.neshow = ne;
        targetEquipment.dobenshow = doben;
        EquipmentManager.Singleton.UpdatePlayerStats();

        // Debug.Log($"Trang bị {slot} đã được cập nhật: {targetEquipment.itemName}");
    }

    // Kiểm tra slot hợp lệ
    public bool IsSlotValid(string slot)
    {
        return slot == "ao" || slot == "quan" || slot == "gang" || slot == "giay" || slot == "rada" || slot == "canh" || slot == "daychuyen" || slot == "nhan" || slot == "vukhi" || slot == "pet" || slot == "phukien";
    }

    // Lấy trang bị từ slot
    public DataEquipment GetEquipment(string slot)
    {
        return slot switch
        {
            "ao" => ao,
            "quan" => quan,
            "gang" => gang,
            "giay" => giay,
            "rada" => rada,
            "canh" => canh,
            "daychuyen" => daychuyen,
            "nhan" => rada,
            "vukhi" => vukhi,
            "pet" => pet,
            "phukien" => phukien,
            _ => null,
        };
    }

    // Xóa trang bị khỏi slot
    public void ClearSlot(string slot)
    {
        switch (slot)
        {
            case "ao": ao = null; break;
            case "quan": quan = null; break;
            case "gang": gang = null; break;
            case "giay": giay = null; break;
            case "rada": rada = null; break;
            case "canh": canh = null; break;
            case "daychuyen": daychuyen = null; break;
            case "nhan": nhan = null; break;
            case "vukhi": vukhi = null; break;
            case "pet": pet = null; break;
            case "phukien": phukien = null; break;
        }
    }

    

}
