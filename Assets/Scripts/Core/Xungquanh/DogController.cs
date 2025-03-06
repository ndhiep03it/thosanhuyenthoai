using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float idleTime = 2f;
    public float barkInterval = 5f;
    public float hungerTime = 10f;
    public float maxHunger = 5f;
    private float currentHunger;
    public Collider2D[] moveArea;

    public AudioClip idlesound;
    public AudioClip barkSound;
    public AudioClip eatSound;
    public AudioClip sleepSound;

    private Vector2 targetPosition;
    private float moveSpeed;
    private bool isIdle = false;
    private bool isHungry = false;
    private bool isSleeping = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    public Animator animator;

    void Start()
    {
        if (moveArea == null)
        {
            Debug.LogError("Chưa gán vùng di chuyển cho chó!");
            return;
        }

        audioSource = GetComponent<AudioSource>();
        currentHunger = maxHunger;

        StartCoroutine(RandomBark());
        StartCoroutine(HungerCycle());

        SetNewTargetPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Feed();
        }

        if (isIdle || isSleeping) return;

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            StartCoroutine(EnterIdleState());
        }
        else
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        animator.SetBool("Walk", true);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.localScale = new Vector3(targetPosition.x < transform.position.x ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    IEnumerator EnterIdleState()
    {
        audioSource.PlayOneShot(idlesound);
        isIdle = true;
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(idleTime);
        isIdle = false;
        SetNewTargetPosition();
    }

    void SetNewTargetPosition()
    {
        if (isHungry) return;

        for (int i = 0; i < 10; i++)
        {
            Collider2D randomArea = moveArea[Random.Range(0, moveArea.Length)];
            Bounds bounds = randomArea.bounds;
            Vector2 potentialPosition = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));

            if (randomArea.OverlapPoint(potentialPosition))
            {
                targetPosition = potentialPosition;
                moveSpeed = Random.Range(minSpeed, maxSpeed);
                return;
            }
        }
    }

    IEnumerator RandomBark()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(barkInterval, barkInterval + 3f));
            if (!isSleeping && !isIdle)
            {
                audioSource.PlayOneShot(barkSound);
            }
        }
    }

    IEnumerator HungerCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(hungerTime);
            currentHunger--;
            if (currentHunger <= 0)
            {
                isHungry = true;
                animator.SetBool("Hungry", true);
            }
        }
    }

    public void Feed()
    {
        if (isHungry)
        {
            currentHunger = Mathf.Min(currentHunger + 2, maxHunger);
            if (currentHunger > 0)
            {
                isHungry = false;
                animator.SetBool("Hungry", false);
            }
            Thongbao.Singleton.ShowThongbao("Cho ăn thành công.");
            audioSource.PlayOneShot(eatSound);
            StartCoroutine(SleepAfterEat());
        }
    }

    IEnumerator SleepAfterEat()
    {
        yield return new WaitForSeconds(2f);
        isSleeping = true;
        animator.SetBool("Sleep", true);
        audioSource.PlayOneShot(sleepSound);
        yield return new WaitForSeconds(5f);
        isSleeping = false;
        animator.SetBool("Sleep", false);
        SetNewTargetPosition();
    }
}
