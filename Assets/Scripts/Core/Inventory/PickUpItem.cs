using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // Để sử dụng Event Trigger

public class PickUpItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public const float pickupRange = 3f;
    private Transform player;
    private bool isTooFar = false;   // Kiểm tra xem người chơi có ở quá xa không
    public GameObject arrowIndicator;  // Mũi tên chỉ vật phẩm

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            PlayerController.Singleton.SetTarget(gameObject);
            if (distance <= pickupRange)
            {
                PickUp();
            }
            else
            {
                Thongbao.Singleton.ShowThongbao("Bạn đang ở quá xa để nhặt vật phẩm!");
            }
            
        }
    }
    public void ClickButton()
    {
        Debug.Log("ClickButton called");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            PlayerController.Singleton.SetTarget(gameObject);
            if (distance <= pickupRange)
            {
                PickUp();
            }
            else
            {
                Thongbao.Singleton.ShowThongbao("Bạn đang ở quá xa để nhặt vật phẩm!");
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                PlayerController.Singleton.SetTarget(gameObject);
                if (distance <= pickupRange)
                {
                    PickUp();
                }
                else
                {
                    if (!isTooFar)
                    {
                        Thongbao.Singleton.ShowThongbao("Bạn đang ở quá xa để nhặt vật phẩm!");
                        isTooFar = true;  // Ngăn thông báo lặp lại liên tục
                        Invoke(nameof(ResetTooFar), 1.5f);  // Reset lại sau 1.5 giây
                    }
                }
            }
        }
            
    }
    private void PickUp()
    {
        // Thêm vật phẩm vào túi đồ
        Inventory.Singleton.AddItem(item, 1, "Vật phẩm rơi từ quái.");
        Thongbao.Singleton.ShowThongbao("Bạn nhận được " + item.itemName + " từ quái");

        // Hiển thị hiệu ứng thu thập
        CollectEffect();
        switch (GameManager.Singleton.idnhiemvu)
        {

            case 0:
                break;
            case 1:
                break;
            case 2:
                QuestManager.Instance.EnemyKilled(2);
                break;
            case 3:
                break;
            case 4:
                 QuestManager.Instance.Collect(4);
                

                break;

        }
        // Xóa đối tượng sau khi nhặt
        Destroy(gameObject);
    }
    public void ShowArrow()
    {
        arrowIndicator.SetActive(true);  // Hiển thị mũi tên
       
    }

    public void HideArrow()
    {
        arrowIndicator.SetActive(false);
    }
    private void CollectEffect()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Drop);
    }
    private void ResetTooFar()
    {
        isTooFar = false;  // Cho phép hiển thị thông báo lần sau
    }
}
