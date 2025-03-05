using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public float speed = 5f;
    public Transform player;
    public float Distance = 2f;
    private Animator Animator;

    

    void Start()
    {
        // Find the player GameObject
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        Animator = GetComponent<Animator>();
        // If this pet belongs to the local player, enable control
        //if (photonView.IsMine)
        //{
        //    // Your initialization code for the local pet
        //}
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Nếu khoảng cách quá xa (lớn hơn 20), dịch chuyển ngay lập tức
            if (distanceToPlayer > 20f)
            {
                transform.position = player.position + new Vector3(-Distance, 0, 0); // Dịch pet về gần người chơi
            }
            else if (distanceToPlayer < Distance)
            {
                // Nếu gần người chơi, chuyển sang trạng thái nhàn rỗi
                Animator.SetBool("Walk", false);
            }
            else
            {
                // Di chuyển pet đến vị trí của người chơi
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                // Đặt hướng của pet dựa trên vị trí của người chơi
                if (transform.position.x < player.position.x)
                {
                    transform.localScale = new Vector3(0.59341f, 0.59341f, 0.59341f);
                }
                else if (transform.position.x > player.position.x)
                {
                    transform.localScale = new Vector3(-0.59341f, 0.59341f, 0.59341f);
                }

                // Đặt trạng thái di chuyển cho animator
                Animator.SetBool("Walk", true);
            }
        }
    }

}
