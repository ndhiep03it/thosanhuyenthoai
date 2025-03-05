using UnityEngine;
using System.Linq;
using System;

public enum ItemType
{
    Consumable, // Tiêu hao
    Equipment,  // Trang bị
    Material,   // Nguyên liệu
    Teleport,    // Dịch chuyển
    ItemRand
}
public enum RewardType
{
    Coin,
    Shovel,
    Health,
    ItemFragment,
    SpecialItem ,
    Ruby,
    NUOCTHANH,
}

public enum ItemParama
{
    ITEM,
    HP,
    MP,
    HUTMAU,
    HUTKHI,
    TANCONG,
    CHIMANG,
    EXP,
    ATTACK,
    DEFENSE,
    SPEED,
    RUBY,
    GOLD ,
    BOXREWARD,
    RANDOMGOLD,
    NETRANH,
    PETBOX,
    VATPHAM,
    GAGOLD,
    GAWHITE,
    THELUC
}

public enum SlotName
{
    no,
    ao,
    quan,
    gang,
    giay,
    rada,
    daychuyen,
    nhan,
    vukhi,
    pet,
    phukien ,
    bua,
    da,
    trangbi,
    canh
}

public enum ItemMoney
{
    GOLD,
    RUBY
}
[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Tên vật phẩm
    public int level;       // Cấp độ yêu cầu
    public Sprite icon;     // Hình ảnh đại diện
    public int maxStack;    // Số lượng tối đa
    public ItemType itemType; // Loại vật phẩm
    public SlotName slotName; // Loại slot cho trang bị
    public int giaMua;      // Giá mua
    public int giaBan;      // Giá bán
    public ItemMoney itemMoney;
    public Sprite IconMoney;

    [TextArea]
    public string description; // Mô tả vật phẩm

    public ItemParama[] itemParama;  // Các chỉ số của vật phẩm
    public Item[] itemReward;
    public int[] parameterValue;    // Giá trị của từng chỉ số
    public int itemID;              // ID duy nhất
    public bool isEquippable;       // Có thể trang bị không
    public int[] star;              // Đánh giá vật phẩm
    public int quantity;

    // Lấy hiệu ứng của vật phẩm
    public string GetItemEffect()
    {
        if (itemParama.Length != parameterValue.Length)
        {
            //Debug.LogWarning($"Item {itemName} có số lượng itemParama và parameterValue không khớp!");
            return "";
        }

        string effectDescription = "";

        if (itemType == ItemType.Consumable)
        {
            for (int i = 0; i < itemParama.Length; i++)
            {
                string paramName = GetParameterName(itemParama[i]);
                effectDescription += $"{paramName} +{parameterValue[i]}.\n";
                
            }
        }
        else if (itemType == ItemType.Equipment)
        {
            for (int i = 0; i < itemParama.Length; i++)
            {
                string paramName = GetParameterName(itemParama[i]);
                effectDescription += $"{paramName}: +{parameterValue[i]}.\n";
            }
        }
        else if (itemType == ItemType.Material)
        {
            effectDescription = "Nguyên liệu này dùng để chế tạo.";
        }
        else if (itemType == ItemType.Teleport)
        {
            effectDescription = "Dùng để dịch chuyển qua các địa điểm.";
        }

        return effectDescription;
    }

    // Lấy chỉ số nâng cấp của vật phẩm
    public string GetItemChiso()
    {
        if (itemParama.Length != parameterValue.Length)
        {
            //Debug.LogWarning($"Item {itemName} có số lượng itemParama và parameterValue không khớp!");
            return "Lỗi: Thông tin không đầy đủ.";
        }
        string effectDescription = "";

        if (slotName == SlotName.da)
        {
            for (int i = 0; i < itemParama.Length; i++)
            {
                string paramName = GetParameterName(itemParama[i]);
                effectDescription += $"{paramName}: +{parameterValue[i]}.\n";
            }
        }
        else
        {
            for (int i = 0; i < itemParama.Length; i++)
            {
                string paramName = GetParameterName(itemParama[i]);
                effectDescription += $"{paramName}: +{parameterValue[i]}.\n";
            }
        }
        

        return effectDescription;
    }
    

    // Lấy giá trị của chỉ số cụ thể
    public int GetParameterValue(ItemParama param)
    {
        int index = itemParama.ToList().IndexOf(param);
        return index >= 0 ? parameterValue[index] : 0;
    }
   
    // Lấy tên loại vật phẩm
    public string GetItemTypeName()
    {
        switch (itemType)
        {
            case ItemType.Consumable: return "\u2605 Tiêu hao";
            case ItemType.Equipment: return "\u23F3 Trang bị";
            case ItemType.Material: return "\u23F3 Nguyên liệu";
            case ItemType.Teleport: return "\u23F3 Dịch chuyển";
            case ItemType.ItemRand: return "\u23F3 Rương";
            default: return "Không xác định";
        }
    }

    // Lấy tên slot trang bị
    public string GetSlotName(SlotName slot)
    {
        switch (slot)
        {
            case SlotName.no: return "Nón";
            case SlotName.ao: return "Áo";
            case SlotName.quan: return "Quần";
            case SlotName.gang: return "Găng tay";
            case SlotName.giay: return "Giày";
            case SlotName.rada: return "Radar";
            case SlotName.canh: return "Cánh";
            case SlotName.daychuyen: return "Dây chuyền";
            case SlotName.nhan: return "Nhẫn";
            case SlotName.vukhi: return "Vũ khí";
            case SlotName.pet: return "Thú cưng";
            case SlotName.phukien: return "Phụ kiện";
            default: return "Không xác định";
        }
    }

    // Lấy tên chỉ số
    private string GetParameterName(ItemParama param)
    {
        switch (param)
        {
            case ItemParama.HP: return "\u2764 HP";
            case ItemParama.MP: return "\u2764 MP";
            case ItemParama.HUTMAU: return "\u2764 Hút máu";
            case ItemParama.HUTKHI: return "\u2764 Hút Ki";
            case ItemParama.TANCONG: return "\u2764 Tấn công";
            case ItemParama.CHIMANG: return "\u2764 Chí mạng";
            case ItemParama.EXP: return "\u2764 Kinh nghiệm";
            case ItemParama.ATTACK: return "\u2764 Công";
            case ItemParama.DEFENSE: return "\u2764 Phòng thủ";
            case ItemParama.SPEED: return "\u2764 Tốc độ";
            case ItemParama.RUBY: return "\u2764 Ngọc Tím";
            case ItemParama.GOLD: return "\u2764 Vàng";
            case ItemParama.BOXREWARD: return "\u2764 Số lượng vật phẩm trong Box";
            case ItemParama.RANDOMGOLD: return "\u2764 Vàng kho";
            case ItemParama.NETRANH: return "\u2764 Né";
            case ItemParama.PETBOX: return "\u2764 Chỉ số ngẫu nhiên từ 0 - ";
            case ItemParama.VATPHAM: return "\u2764 Ngẫu nhiên - ";
            default: return "Thuộc tính không xác định";
        }
    }

    // Tạo ID duy nhất cho vật phẩm
    public void GenerateUniqueID()
    {
        itemID = System.DateTime.Now.Ticks.GetHashCode();
    }




    public string RandomReward(Item item)
    {
        if (itemReward != null && itemReward.Length > 0) // Kiểm tra itemReward không null và có phần tử
        {
            System.Random rdRewardBox = new System.Random();
            int rand = rdRewardBox.Next(0, itemReward.Length);
            Inventory.Singleton.BuyItem(itemReward[rand], 1, "Vật phẩm nhận từ hộp quà", 1,100);
            item.icon = itemReward[rand].icon;
            item.itemName = itemReward[rand].itemName;
            return itemReward[rand].itemName; // Trả về tên của vật phẩm
        }
        else
        {
            Debug.Log("Danh sách phần thưởng rỗng hoặc không tồn tại.");
            return "Không có vật phẩm"; // Trả về giá trị mặc định
        }
    }
    public string RandomPetParama(Item item)
    {
        if (itemReward != null && itemReward.Length > 0) // Kiểm tra itemReward không null và có phần tử
        {
            System.Random rdChiso= new System.Random();
            int hp_parama = rdChiso.Next(0, parameterValue[1]);
            int mp_parama = rdChiso.Next(0, parameterValue[2]);
            int dame_parama = rdChiso.Next(0, parameterValue[5]);
            int hutki_parama = rdChiso.Next(0, parameterValue[3]);
            int hutmau_parama = rdChiso.Next(0, parameterValue[4]);
            int chimang_parama = rdChiso.Next(0, parameterValue[6]);
            float ne_parama = rdChiso.Next(0, parameterValue[7]);



            System.Random rdRewardBox = new System.Random();
            int rand = rdRewardBox.Next(0, itemReward.Length);
            item.icon = itemReward[rand].icon;
            item.itemName = itemReward[rand].itemName;
            Inventory.Singleton.AddThaoItem(itemReward[rand], 1,"Vật phẩm nhận từ hộp quà",0, 0,dame_parama,hp_parama, mp_parama, chimang_parama,hutmau_parama,hutki_parama,ne_parama,0,100);
            return itemReward[rand].itemName; // Trả về tên của vật phẩm
        }
        else
        {
            Debug.Log("Danh sách phần thưởng rỗng hoặc không tồn tại.");
            return "Không có vật phẩm"; // Trả về giá trị mặc định
        }
    }
    public int RandomGold(Item item)
    {
        int minGold = item.parameterValue[0]; // Giá trị từ mảng 1
        int maxGold = item.parameterValue[1]; // Giá trị từ mảng 2
        System.Random randomGold = new System.Random();
        int randomValue = randomGold.Next(minGold, maxGold + 1); // Tạo số ngẫu nhiên từ minGold đến maxGold

        return randomValue;
    }

    public string RandomCountItem(Item item)
    {
        if (itemReward != null && itemReward.Length > 0) // Kiểm tra itemReward không null và có phần tử
        {
            System.Random rdRewardBox = new System.Random();
            int rand = rdRewardBox.Next(parameterValue[0], parameterValue[1]);
            Inventory.Singleton.BuyItem(itemReward[0],rand, "Vật phẩm nhận từ hộp quà", 1, 100);
            item.icon = itemReward[0].icon;
            item.itemName = itemReward[0].itemName;
            quantity = rand;
            item.quantity = rand;
            return itemReward[0].itemName + "số lượng "+ rand; // Trả về tên của vật phẩm
        }
        else
        {
            Debug.Log("Danh sách phần thưởng rỗng hoặc không tồn tại.");
            return "Không có vật phẩm"; // Trả về giá trị mặc định
        }
    }
}
