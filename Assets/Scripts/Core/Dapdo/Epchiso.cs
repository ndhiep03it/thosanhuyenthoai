using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Epchiso : MonoBehaviour
{
    public static Epchiso Singleton;
    public Chisotinh chisotinh;
    public SlotName slotName;
    private string slot;
    public Image ItemDA;
    public Image ItemTB;

    [Header("SLOT DA")]
    public Item[] itemDA;

    [Header("SLOT TB")]
    public Item[] itemTB;
    public GameObject Panelrpchisolayra;
    public GameObject Panelnangcap;
    public int vitriitemDA;
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

    [Header("CHỈ SỐ TIẾP")]
    public int hptiep;
    public int mptiep;
    public int hutmautiep;
    public int hutkitiep;
    public int chimangtiep;
    public int dametiep;

    [Header("THÔNG TIN NÂNG CẤP")]
    public Image iconItemNangcap;
    public Text txtparamaNangcap;
    public Text txtNameItemNangcap;
    public int count;
    public string nameDa;
    public string nameTB;
    int upGame = 0;



    private void Awake()
    {
        if (Singleton == null) Singleton = this;
    }



    // Hàm thêm item vào slot
    public void AddItem()
    {
        //nameDa = itemDA[0].itemName;
        //nameTB = itemTB[0].itemName;

        // Cộng chỉ số từ đá và trang bị
       




        switch (slotName)
        {
            case SlotName.da:
                SetItem(itemDA[0], "da");
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
            case SlotName.canh:
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


    public void SetItem(Item item,string slot)
    {
        switch (slot)
        {
            case "da":
                itemDA[0] = item;
                ItemDA.gameObject.SetActive(true);
                ItemDA.sprite = item.icon;
                break;
            case "trangbi":
                itemTB[0] = item;
                ItemTB.gameObject.SetActive(true);
                ItemTB.sprite = item.icon;
                break;
        }
    }

    public void Layda()
    {
        slot = "da";
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
    public void CheckNangcap()
    {
        // Kiểm tra nếu đá nâng cấp chưa được thêm
        // Kiểm tra nếu đá nâng cấp chưa được thêm
        if (itemDA == null || itemDA.Length == 0)
        {
            Thongbao.Singleton.ShowThongbao("Vui lòng thêm đá nâng cấp.");
            return;
        }

        // Kiểm tra nếu trang bị nâng cấp chưa được thêm
        if (itemTB == null || itemTB.Length == 0)
        {
            Thongbao.Singleton.ShowThongbao("Vui lòng thêm trang bị nâng cấp.");
            return;
        }
        const int MaxEpSao = 7;
        if (Inventory.Singleton.items[vitriitemTB].solanepsao >= MaxEpSao)
        {
            Thongbao.Singleton.ShowThongbao($"Trang bị {itemTB[0].itemName} đã đạt ép sao tối đa ({MaxEpSao}).");
            return;
        }
        hpgoc = 0;
        mpgoc = 0;
        damegoc = 0;
        chimanggoc = 0;
        hutkigoc = 0;
        hutmaugoc = 0;
        hptiep = 0;
        mptiep = 0;
        chimangtiep = 0;
        dametiep = 0;
        hutkitiep = 0;
        hutmautiep = 0;
        // Hiển thị giao diện nâng cấp
        Panelnangcap.SetActive(true);
        //hptiep = 0;
        //mptiep = 0;
        //chimangtiep = 0;
        //dametiep = 0;
        //hutkitiep = 0;
        //hutmautiep = 0;

        //chisotinh.hp = 0;
        //chisotinh.mp = 0;
        //chisotinh.chimang = 0;
        //chisotinh.hutki = 0;
        //chisotinh.hutmau = 0;
        //chisotinh.dame = 0;

        //hp = 0;
        //mp = 0;
        //hutki = 0;
        //hutmau = 0;
        //chimang = 0;
        //dame = 0;
        string statsDescription = "";
        switch (itemDA[0].name)
        {
            case "DATC":
                upGame = 3;
                hptiep += hp;
                mptiep  += mp;
                chimangtiep  += chimang;
                dametiep += dame + upGame;
                hutkitiep  += hutki;
                hutmautiep += hutmau;

                if (hptiep > 0) statsDescription += $"\nHP: +{hptiep}";
                if (mptiep > 0) statsDescription += $"\nMP: +{mptiep}";
                if (dametiep > 0) statsDescription += $"\nTấn công: +{dametiep} <color=red> (+{upGame})</color>";
                if (chimangtiep > 0) statsDescription += $"\nChí mạng: +{chimangtiep}";
                if (hutkitiep > 0) statsDescription += $"\nHút ki: +{hutkitiep}";
                if (hutmautiep > 0) statsDescription += $"\nHút máu: +{hutmautiep}";
                break;
            case "DAHP":
                upGame = 5;
                hptiep  += hp + upGame;
                mptiep  += mp;
                chimangtiep  += chimang;
                dametiep += dame;
                hutkitiep  += hutki;
                hutmautiep  += hutmau;

                if (hptiep > 0) statsDescription += $"\nHP: +{hptiep} <color=red> (+{upGame})</color>";
                if (mptiep > 0) statsDescription += $"\nMP: +{mptiep}";
                if (dametiep > 0) statsDescription += $"\nTấn công: +{dametiep}";
                if (chimangtiep > 0) statsDescription += $"\nChí mạng: +{chimangtiep}";
                if (hutkitiep > 0) statsDescription += $"\nHút ki: +{hutkitiep}";
                if (hutmautiep > 0) statsDescription += $"\nHút máu: +{hutmautiep}";
                break;
            case "DAMP":
                upGame = 5;
                hptiep = hpgoc += hp;
                mptiep = mpgoc += mp + upGame;
                chimangtiep = chimanggoc += chimang;
                dametiep = damegoc += dame;
                hutkitiep = hutkigoc += hutki;
                hutmautiep = hutmaugoc += hutmau;

                if (hptiep > 0) statsDescription += $"\nHP: +{hptiep}";
                if (mptiep > 0) statsDescription += $"\nMP: +{mptiep} <color=red> (+{upGame})</color>";
                if (dametiep > 0) statsDescription += $"\nTấn công: +{dametiep}";
                if (chimangtiep > 0) statsDescription += $"\nChí mạng: +{chimangtiep}";
                if (hutkitiep > 0) statsDescription += $"\nHút ki: +{hutkitiep}";
                if (hutmautiep > 0) statsDescription += $"\nHút máu: +{hutmautiep}";
                break;
            case "DAHK":
                upGame = 5;
                hptiep = hpgoc += hp;
                mptiep = mpgoc += mp;
                chimangtiep = chimanggoc += chimang;
                dametiep = damegoc += dame;
                hutkitiep = hutkigoc += hutki + upGame;
                hutmautiep = hutmaugoc += hutmau;
                break;
            case "DAHM":
                upGame = 5;
                hptiep = hpgoc += hp;
                mptiep = mpgoc += mp;
                chimangtiep = chimanggoc += chimang;
                dametiep = damegoc += dame;
                hutkitiep = hutkigoc += hutki;
                hutmautiep = hutmaugoc += hutmau + upGame;
                break;

            default:
                Thongbao.Singleton.ShowThongbao("Đá nâng cấp không hợp lệ.");
                break;
            
        }
       
        iconItemNangcap.sprite = ItemTB.sprite;
       
        txtNameItemNangcap.text = itemTB[0].itemName;
        //if (hp > 0) statsDescription += $"\n<color=red>Cấp trang bị:{playerEquipment.pet.level}</color>";
        //else statsDescription += $"\n<color=red>Trang bị chưa cấp hoặc không thể nâng cấp.</color>";
        if (upGame == 5)
        {
            
        }

        
        txtparamaNangcap.text = itemTB[0].description + $"\n<color=red>{itemTB[0].GetItemChiso()}</color> \n<color=black>Chỉ số sau ép</color>{statsDescription}\nSố sao:{solanepsao}/7 <color=red>+1</color>";
        
        
    }


    public void Tienhanhnangcap()
    {
        if (GameManager.Singleton.ruby >= 1)
        {
            GameManager.Singleton.ruby -= 1;
            Inventory.Singleton.items[vitriitemTB].hp = hptiep;
            Inventory.Singleton.items[vitriitemTB].mp = mptiep;
            Inventory.Singleton.items[vitriitemTB].dame = dametiep;
            Inventory.Singleton.items[vitriitemTB].chimang = chimangtiep;
            Inventory.Singleton.items[vitriitemTB].hutki = hutkitiep;
            Inventory.Singleton.items[vitriitemTB].hutmau = hutmautiep;

            //trừ số lượng đá nc
            
            Inventory.Singleton.items[vitriitemTB].solanepsao += 1;
            Inventory.Singleton.NangcapItem(vitriitemDA, 1);
            Inventory.Singleton.SaveInventory();
            Inventory.Singleton.LoadInventory();
            NangcapInventory.Singleton.UpdateUI();

            Panelnangcap.SetActive(false);
            Thongbao.Singleton.ShowThongbao("Ép sao thành công");
            SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Nangcap);
            hptiep = 0;
            mptiep = 0;
            chimangtiep = 0;
            dametiep = 0;
            hutkitiep = 0;
            hutmautiep = 0;

            count = 0;
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
            upGame = 0;
            UnItem("da");
            UnItem("trangbi");
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Bạn không đủ Ruby tím.");
        }

        
    }



    public void UnItem( string slot)
    {
        switch (slot)
        {
            case "da":
                
                ItemDA.gameObject.SetActive(false);
                ItemDA.sprite = null;
                hpgoc -= itemDA[0].GetParameterValue(ItemParama.HP);
                mpgoc -= itemDA[0].GetParameterValue(ItemParama.MP);
                chimanggoc -= itemDA[0].GetParameterValue(ItemParama.CHIMANG);
                hutkigoc -= itemDA[0].GetParameterValue(ItemParama.HUTKHI);
                hutmaugoc -= itemDA[0].GetParameterValue(ItemParama.HUTMAU);

               

                
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
                itemDA = null;
                count = 0;
                upGame = 0;
                break;
            case "trangbi":
                
                ItemTB.gameObject.SetActive(false);
                ItemTB.sprite = null;
                hpgoc -= itemTB[0].GetParameterValue(ItemParama.HP);
                mpgoc -= itemTB[0].GetParameterValue(ItemParama.MP);
                chimanggoc -= itemTB[0].GetParameterValue(ItemParama.CHIMANG);
                hutkigoc -= itemTB[0].GetParameterValue(ItemParama.HUTKHI);
                hutmaugoc -= itemTB[0].GetParameterValue(ItemParama.HUTMAU);
                dame =0;
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

                count = 0;
                itemTB = null;
                upGame = 0;

                break;
        }
        hptiep = 0;
        mptiep = 0;
        chimangtiep = 0;
        dametiep = 0;
        hutkitiep = 0;
        hutmautiep = 0;

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
        count--;
    }
}


[System.Serializable]
public class Chisotinh
{
    [Header("CHỈ SỐ GỐC TĨNH")]
    public int hpgoc;
    public int mpgoc;
    public int hutmaugoc;
    public int hutkigoc;
    public int chimanggoc;
    public int damegoc;
    public int negoc;
    [Header("CHỈ SỐ CHƯA ÉP TĨNH")]
    public int hp;
    public int mp;
    public int hutmau;
    public int hutki;
    public int chimang;
    public int dame;
    public float ne;
}