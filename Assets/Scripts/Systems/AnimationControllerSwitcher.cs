using UnityEngine;

public class AnimationControllerSwitcher : MonoBehaviour
{
    public Animator animator; // Gán Animator vào đây
    public RuntimeAnimatorController[] animatorControllers; // Danh sách các Animator Controller
    public GameObject AnimSelectObj;

    private int currentIndex = 0;

    void Start()
    {
        if (animatorControllers.Length > 0)
        {
            //ChangeAnimator(0); // Đặt mặc định controller đầu tiên
        }
    }

    public void ChangeAnimator(int index)
    {
        if (index >= 0 && index < animatorControllers.Length)
        {
            animator.runtimeAnimatorController = animatorControllers[index];
            currentIndex = index;
            
        }
        else
        {
            //Debug.LogWarning("Index ngoài phạm vi AnimatorController.");
            AnimSelectObj.SetActive(false);
        }
    }

    public void NextAnimator()
    {
        int nextIndex = (currentIndex + 1) % animatorControllers.Length;
        ChangeAnimator(nextIndex);
    }

    public void PreviousAnimator()
    {
        int prevIndex = (currentIndex - 1 + animatorControllers.Length) % animatorControllers.Length;
        ChangeAnimator(prevIndex);
    }
}
