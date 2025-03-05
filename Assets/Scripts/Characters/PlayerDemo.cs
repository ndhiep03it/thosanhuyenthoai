using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : MonoBehaviour
{
    [SerializeField] public Joystick MovementJoystick;
    public bool IsPlatform = false;
    public float moveSpeed = 5f;       // Tốc độ di chuyển của nhân vật
    public float jumpForce = 10f;     // Lực nhảy khi bay
    private Rigidbody2D rb;           // Rigidbody2D để quản lý vật lý
    private Vector2 moveDirection;    // Hướng di chuyển
    private Animator animator;        // Animator để điều khiển animation
    private SpriteRenderer spriteRenderer; // SpriteRenderer để lật hình ảnh
    private bool isGrounded = true;   // Kiểm tra xem nhân vật đang trên mặt đất hay không

    public Transform groundCheck;     // Điểm kiểm tra mặt đất
    public float groundCheckRadius = 0.2f; // Bán kính kiểm tra mặt đất
    public LayerMask groundLayer;     // Lớp định nghĩa mặt đất

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy Rigidbody2D gắn với nhân vật
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer gắn với nhân vật
        animator = GetComponent<Animator>(); // Lấy Animator gắn với nhân vật
    }

    void Update()
    {
        // Kiểm tra trạng thái trên mặt đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        if (IsPlatform)
        {
            // Áp dụng di chuyển theo trục X
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

            // Nhận input từ người chơi
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector2(moveX, moveY).normalized; // Chuẩn hóa hướng di chuyển

            // Animation di chuyển ngang
            if (moveDirection.x != 0) // Di chuyển trái/phải
            {
                animator.SetBool("Run", true);
                spriteRenderer.flipX = moveDirection.x < 0; // Lật hình ảnh nếu di chuyển sang trái
            }
            else
            {
                animator.SetBool("Run", false);
            }

            // Animation bay (nhảy)
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) // Nếu nhấn nút lên và đang trên mặt đất
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Thêm lực nhảy theo trục Y
                animator.SetTrigger("Fly"); // Kích hoạt animation bay
            }
        }
        else
        {
            //mobile
            if (MovementJoystick.Direction.y != 0)
            {
                rb.velocity = new Vector2(MovementJoystick.Direction.x * moveSpeed, MovementJoystick.Direction.y * moveSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            if (MovementJoystick.Direction.x > 0)
            {
                animator.SetBool("Run", true);
                spriteRenderer.flipX = false; // Lật hình ảnh nếu di chuyển sang trái
               
            }
            else if (MovementJoystick.Direction.x < 0)
            {

                animator.SetBool("Run", true);
                spriteRenderer.flipX = true; // Lật hình ảnh nếu di chuyển sang trái
                
            }
            else if (MovementJoystick.Direction.x == 0)
            {

                animator.SetBool("Run", false);

               

            }
        }

        // Kiểm tra trạng thái rơi (khi không trên mặt đất)
        animator.SetBool("isFalling", !isGrounded);
    }

    void FixedUpdate()
    {
        
    }
}
