using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoManager : MonoBehaviour
{
    public GameObject[] Enemies; // Danh sách các Enemy
    private bool isTansatActive = false; // Trạng thái tàn sát
    private int currentEnemyIndex = 0; // Chỉ số Enemy hiện tại
    public float moveSpeed = 5.0f; // Tốc độ di chuyển của Player
    public float targetRadius = 1.0f; // Khoảng cách tối thiểu để coi là đến nơi

    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Bật/tắt chế độ tàn sát
            isTansatActive = !isTansatActive;
            Debug.Log(gameObject.name);
            if (isTansatActive)
            {
                Thongbao.Singleton.ShowThongbao("Tàn sát bật");
                //PlayerController.Singleton.isWalking = false;


                if (Enemies.Length > 0)
                {
                    currentEnemyIndex = 0; // Bắt đầu từ Enemy đầu tiên
                }
                else
                {
                    Thongbao.Singleton.ShowThongbao("Không tìm thấy Enemy nào!");
                    isTansatActive = false; // Tắt chế độ nếu không có Enemy
                }
            }
            else
            {
                Thongbao.Singleton.ShowThongbao("Tàn sát tắt");
                //PlayerController.Singleton.isWalking = true;
                Thongbao.Singleton.UIBar.SetActive(false);
            }
        }

        if (isTansatActive)
        {
            AutoTarget();
        }
    }

    public void AutoTS()
    {
        // Bật/tắt chế độ tàn sát
        isTansatActive = !isTansatActive;

        if (isTansatActive)
        {
            Thongbao.Singleton.ShowThongbao("Tàn sát bật");
            PlayerController.Singleton.isWalking = false;
            Thongbao.Singleton.txtTrangthai.text= "Tàn sát";
            Thongbao.Singleton.txtTrangthai.gameObject.SetActive(true);

            if (Enemies.Length > 0)
            {
                currentEnemyIndex = 0; // Bắt đầu từ Enemy đầu tiên
            }
            else
            {
                Thongbao.Singleton.ShowThongbao("Không tìm thấy Enemy nào!");
                isTansatActive = false; // Tắt chế độ nếu không có Enemy
                Thongbao.Singleton.UIBar.SetActive(false);
            }
        }
        else
        {
            Thongbao.Singleton.ShowThongbao("Tàn sát tắt");
            PlayerController.Singleton.isWalking = true;
            Thongbao.Singleton.txtTrangthai.gameObject.SetActive(false);
            Thongbao.Singleton.UIBar.SetActive(false);

        }
    }
    void AutoTarget()
    {
        // Kiểm tra nếu không còn Enemy hoặc đã vượt quá danh sách Enemy
        if (Enemies.Length == 0 || currentEnemyIndex >= Enemies.Length)
        {
            Thongbao.Singleton.ShowThongbao("Tất cả Enemy đã bị tiêu diệt!");
            Thongbao.Singleton.UIBar.SetActive(false);
            isTansatActive = false;
            return;
        }

        GameObject currentEnemy = Enemies[currentEnemyIndex];
        EnemyController enemyController = currentEnemy.GetComponent<EnemyController>();

        // Kiểm tra nếu quái đã bị tiêu diệt hoặc biến mất
        if (currentEnemy == null || !currentEnemy.activeInHierarchy)
        {
            // Chuyển sang quái tiếp theo
            currentEnemyIndex++;

            // Nếu đã hết Enemy, thông báo
            if (currentEnemyIndex >= Enemies.Length)
            {
                Thongbao.Singleton.ShowThongbao("Tất cả Enemy đã bị tiêu diệt!");
                Thongbao.Singleton.UIBar.SetActive(false);
                isTansatActive = false;
            }

            return; // Kết thúc để tránh tiếp tục xử lý Enemy hiện tại
        }

        // Tính khoảng cách giữa người chơi và quái hiện tại
        float distanceToEnemy = Vector3.Distance(PlayerController.Singleton.transform.position, currentEnemy.transform.position);

        if (distanceToEnemy > targetRadius)
        {
            // Di chuyển người chơi đến vị trí của Enemy
            Vector3 direction = (currentEnemy.transform.position - PlayerController.Singleton.transform.position).normalized;
            PlayerController.Singleton.transform.position += direction * moveSpeed * Time.deltaTime;
            Thongbao.Singleton.UIBar.SetActive(true);
            Thongbao.Singleton.sliderHpEnemy.value = enemyController.HP;
            Thongbao.Singleton.sliderHpEnemy.maxValue = enemyController.HPMAX;
            Thongbao.Singleton.txtHPenemy.text = enemyController.HP + "/" + enemyController.HPMAX;

            PlayerController.Singleton.isWalking = false;

            if (direction.x > 0)
            {
                PlayerController.Singleton.transform.localScale = new Vector3(1f, 1f, 1f);
                PlayerController.Singleton.animator.SetBool("Run", true);
                PlayerController.Singleton.canvas.transform.localScale = new Vector3(1f, 1f, 1f);
                PlayerController.Singleton.HaoquangSP.SetActive(false);
                Thongbao.Singleton.txtTrangthai.gameObject.transform.localScale = new Vector3(0.007537152f, 0.007537152f, 0.007537152f);

            }
            else if (direction.x < 0)
            {
                PlayerController.Singleton.transform.localScale = new Vector3(-1f, 1f, 1f);
                PlayerController.Singleton.animator.SetBool("Run", true);
                PlayerController.Singleton.canvas.transform.localScale = new Vector3(-1f, 1f, 1f);
                Thongbao.Singleton.txtTrangthai.gameObject.transform.localScale = new Vector3(0.007537152f, 0.007537152f, 0.007537152f);
                PlayerController.Singleton.HaoquangSP.SetActive(false);

            }
            else
            {
                PlayerController.Singleton.HaoquangSP.SetActive(true);

            }


        }
        else
        {
            // Nếu đã đến đủ gần, kiểm tra trạng thái của quái trước khi tấn công
            if (currentEnemy != null && currentEnemy.activeInHierarchy)
            {
                // Chỉ thực hiện tấn công nếu quái vẫn tồn tại
                PlayerController.Singleton.animator.SetBool("Run", false);
                Thongbao.Singleton.UIBar.SetActive(true);
                Thongbao.Singleton.sliderHpEnemy.value = enemyController.HP;
                Thongbao.Singleton.sliderHpEnemy.maxValue = enemyController.HPMAX;
                Thongbao.Singleton.txtHPenemy.text = enemyController.HP + "/" + enemyController.HPMAX;
                SkillController.Singleton.AutoSkill();

            }

            // Kiểm tra lại Enemy sau khi tấn công (có thể quái đã chết trong quá trình)
            if (currentEnemy == null || !currentEnemy.activeInHierarchy)
            {
                // Chuyển sang quái tiếp theo
                currentEnemyIndex++;

                // Nếu đã hết Enemy, thông báo
                if (currentEnemyIndex >= Enemies.Length)
                {
                    Thongbao.Singleton.ShowThongbao("Tất cả Enemy đã bị tiêu diệt!");
                    isTansatActive = false;
                }
            }
        }
    }


}
