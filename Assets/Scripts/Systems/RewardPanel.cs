using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    public static RewardPanel Singleton { get; protected set; }
    [SerializeField] GameObject panelReardObj;
    [SerializeField] Transform canvas;

    protected void Awake()
    {
        if (Singleton == null) Singleton = this;
    }


    public void SpanInstantiate(Item item,int quantity)
    {
        GameObject newobj = Instantiate(panelReardObj, canvas, false);
        RewardProfile rewardProfile = newobj.GetComponent<RewardProfile>();
        rewardProfile.txtNameItem.text = "Bạn nhận được vật phẩm " + item.itemName;
        rewardProfile.txtQuantity.text = quantity.ToString();
        rewardProfile.iconItem.sprite = item.icon;

    }
}
