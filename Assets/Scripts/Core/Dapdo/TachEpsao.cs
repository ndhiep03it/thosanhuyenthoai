using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TachEpsao : MonoBehaviour
{
    public static TachEpsao Singleton;
    public Chisotinh chisotinh;
    public SlotName slotName;
    private string slot;

    public Image ItemBUA;
    public Image ItemTB;

    [Header("SLOT BUA")]
    public Item[] itemBUA;

    [Header("SLOT TB")]
    public Item[] itemTB;
    public GameObject Panelrpchisolayra;
    public GameObject Paneltach;
    public int vitriitemBUA;
    public int vitriitemTB;
    [Header("CHỈ SỐ GỐC")]
    public int hpgoc;
    public int mpgoc;
    public int hutmaugoc;
    public int hutkigoc;
    public int chimanggoc;
    public int damegoc;
    [Header("CHỈ SỐ CHƯA ÉP")]
    public int hp;
    public int mp;
    public int hutmau;
    public int hutki;
    public int chimang;
    public int dame;
    public int solanepsao;

   

    [Header("THÔNG TIN NÂNG CẤP")]
    public Image iconItemNangcap;
    public Text txtparamaNangcap;
    public Text txtNameItemNangcap;
   
    


    private void Awake()
    {
        if (Singleton == null) Singleton = this;
    }



    public void AddItem()
    {
     
        switch (slotName)
        {
            case SlotName.bua:
                SetItem(itemBUA[0], "bua");
                break;
            case SlotName.ao:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.quan:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.gang:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.giay:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.rada:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.daychuyen:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.nhan:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.vukhi:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.pet:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            case SlotName.phukien:
                chisotinh.hpgoc = 0;
                chisotinh.mpgoc = 0;
                chisotinh.chimanggoc = 0;
                chisotinh.hutkigoc = 0;
                chisotinh.hutmaugoc = 0;
                chisotinh.damegoc = 0;

                hpgoc += chisotinh.hpgoc;
                mpgoc += chisotinh.mpgoc;
                chimanggoc += chisotinh.chimanggoc;
                hutkigoc += chisotinh.hutkigoc;
                hutmaugoc += chisotinh.hutmaugoc;
                damegoc += chisotinh.damegoc;


                // Cập nhật chỉ số tính toán
                dame = chisotinh.dame;
                hp = chisotinh.hp;
                mp = chisotinh.mp;
                hutki = chisotinh.hutki;
                hutmau = chisotinh.hutmau;
                chimang = chisotinh.chimang;
                SetItem(itemTB[0], "trangbi");
                break;
            default:
                Thongbao.Singleton.ShowThongbao("Vật phẩm không hợp lệ.");
                break;
        }


    }

    public void Laybua()
    {
        slot = "bua";
        Panelrpchisolayra.SetActive(true);
    }
    public void Laytrangbi()
    {
        slot = "trangbi";
        Panelrpchisolayra.SetActive(true);
    }

    public void UnItemOK()
    {
        UnItem(slot);
    }
    public void UnItem(string slot)
    {
        switch (slot)
        {
            case "da":

                ItemBUA.gameObject.SetActive(false);
                ItemBUA.sprite = null;
                hpgoc -= itemBUA[0].GetParameterValue(ItemParama.HP);
                mpgoc -= itemBUA[0].GetParameterValue(ItemParama.MP);
                chimanggoc -= itemBUA[0].GetParameterValue(ItemParama.CHIMANG);
                hutkigoc -= itemBUA[0].GetParameterValue(ItemParama.HUTKHI);
                hutmaugoc -= itemBUA[0].GetParameterValue(ItemParama.HUTMAU);




                hpgoc = 0;
                mpgoc = 0;
                damegoc = 0;
                chimanggoc = 0;
                hutkigoc = 0;
                hutmaugoc = 0;
                dame = 0;
                hp = 0;
                mp = 0;
                hutki = 0;
                hutmau = 0;
                chimang = 0;
                itemBUA = null;
                break;
            case "trangbi":

                ItemTB.gameObject.SetActive(false);
                ItemTB.sprite = null;
                hpgoc -= itemTB[0].GetParameterValue(ItemParama.HP);
                mpgoc -= itemTB[0].GetParameterValue(ItemParama.MP);
                chimanggoc -= itemTB[0].GetParameterValue(ItemParama.CHIMANG);
                hutkigoc -= itemTB[0].GetParameterValue(ItemParama.HUTKHI);
                hutmaugoc -= itemTB[0].GetParameterValue(ItemParama.HUTMAU);
                dame = 0;
                hp = 0;
                mp = 0;
                hutki = 0;
                hutmau = 0;
                chimang = 0;

                hpgoc = 0;
                mpgoc = 0;
                damegoc = 0;
                chimanggoc = 0;
                hutkigoc = 0;
                hutmaugoc = 0;


                itemTB = null;

                break;
        }
       

        chisotinh.hpgoc = 0;
        chisotinh.mpgoc = 0;
        chisotinh.chimanggoc = 0;
        chisotinh.hutkigoc = 0;
        chisotinh.hutmaugoc = 0;
        chisotinh.damegoc = 0;

        chisotinh.hp = 0;
        chisotinh.mp = 0;
        chisotinh.chimang = 0;
        chisotinh.hutki = 0;
        chisotinh.hutmau = 0;
        chisotinh.dame = 0;
       
    }
    public void SetItem(Item item, string slot)
    {
        switch (slot)
        {
            case "bua":
                itemBUA[0] = item;
                ItemBUA.gameObject.SetActive(true);
                ItemBUA.sprite = item.icon;
                break;
            case "trangbi":
                itemTB[0] = item;
                ItemTB.gameObject.SetActive(true);
                ItemTB.sprite = item.icon;
                break;
        }
    }
    public void Checktach()
    {
        // Kiểm tra nếu đá nâng cấp chưa được thêm
        // Kiểm tra nếu đá nâng cấp chưa được thêm
        if (itemBUA == null || itemBUA.Length == 0)
        {
            Thongbao.Singleton.ShowThongbao("Vui lòng thêm bùa để tách.");
            return;
        }

        // Kiểm tra nếu trang bị nâng cấp chưa được thêm
        if (itemTB == null || itemTB.Length == 0)
        {
            Thongbao.Singleton.ShowThongbao("Vui lòng thêm trang bị để tách.");
            return;
        }
       
        if (Inventory.Singleton.items[vitriitemTB].solanepsao <= 0)
        {
            Thongbao.Singleton.ShowThongbao($"Trang bị {itemTB[0].itemName} không thể tách.");
            return;
        }
        hpgoc = 0;
        mpgoc = 0;
        damegoc = 0;
        chimanggoc = 0;
        hutkigoc = 0;
        hutmaugoc = 0;
        // Hiển thị giao diện nâng cấp
        Paneltach.SetActive(true);
        string statsDescription = "";
        if (hp > 0) statsDescription += $"\nHP: +{hp} <color=red> (Về 0 )</color>";
        if (mp > 0) statsDescription += $"\nMP: +{mp} <color=red> (Về 0 )</color>";
        if (dame > 0) statsDescription += $"\nTấn công: +{dame} <color=red> (Về 0 )</color>";
        if (chimang > 0) statsDescription += $"\nChí mạng: +{chimang} <color=red> (Về 0 )</color>";
        if (hutki > 0) statsDescription += $"\nHút ki: +{hutki} <color=red> (Về 0 )</color>";
        if (hutmau > 0) statsDescription += $"\nHút máu: +{hutmau} <color=red> (Về 0 )</color>";

        iconItemNangcap.sprite = ItemTB.sprite;

        txtNameItemNangcap.text = itemTB[0].itemName;
        //if (hp > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.pet.level}</color>";
        //else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        
        txtparamaNangcap.text = itemTB[0].description + $"\n<color=red>{itemTB[0].GetItemChiso()}</color>\n<color=black>Chỉ số đã ép</color> {statsDescription}  \n Cần 10 Ruby để tách \n{itemBUA[0].description}\n Số ép sao sẽ về 0/7";


    }
    public void Tienhanhtachsao()
    {
        if (GameManager.Singleton.ruby >= 10)
        {
            GameManager.Singleton.ruby -= 1;
            Inventory.Singleton.items[vitriitemTB].hp = 0;
            Inventory.Singleton.items[vitriitemTB].mp = 0;
            Inventory.Singleton.items[vitriitemTB].dame = 0;
            Inventory.Singleton.items[vitriitemTB].chimang = 0;
            Inventory.Singleton.items[vitriitemTB].hutki = 0;
            Inventory.Singleton.items[vitriitemTB].hutmau = 0;

            //trừ số lượng đá nc
            Inventory.Singleton.RemoveItem(vitriitemBUA, 1);
            Inventory.Singleton.items[vitriitemTB].solanepsao = 0;

            Inventory.Singleton.SaveInventory();
            Inventory.Singleton.LoadInventory();

            TachchisoInventory.Singleton.UpdateUI();
            Paneltach.SetActive(false);
            Thongbao.Singleton.ShowThongbao("Tách sao thành công");
           

            chisotinh.hpgoc = 0;
            chisotinh.mpgoc = 0;
            chisotinh.chimanggoc = 0;
            chisotinh.hutkigoc = 0;
            chisotinh.hutmaugoc = 0;
            chisotinh.damegoc = 0;

            chisotinh.hp = 0;
            chisotinh.mp = 0;
            chisotinh.chimang = 0;
            chisotinh.hutki = 0;
            chisotinh.hutmau = 0;
            chisotinh.dame = 0;
            UnItem("da");
            UnItem("trangbi");
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Bạn không đủ Ruby tím.");
        }


    }



}
