using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText; // Text để hiển thị văn bản
    public Button continueButton; // Nút tiếp tục
    public string[] dialogues; // Mảng các hội thoại
    public float typingSpeed = 0.05f; // Tốc độ gõ ký tự
    private int currentDialogueIndex = 0; // Chỉ số hội thoại hiện tại
    private AudioSource soundchu; // Âm thanh phát khi gõ từng ký tự

    void Start()
    {
        soundchu = GetComponent<AudioSource>();
        continueButton.onClick.AddListener(OnContinueClicked);
        continueButton.gameObject.SetActive(false); // Ẩn nút "Tiếp tục" ban đầu
        ShowDialogue();
    }

    void ShowDialogue()
    {
        // Hiển thị hội thoại với hiệu ứng gõ chữ
        StartCoroutine(TypeSentence(dialogues[currentDialogueIndex]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            // Phát âm thanh cho từng ký tự
            if (soundchu != null && !soundchu.isPlaying) // Chỉ phát nếu chưa đang phát âm thanh
            {
                soundchu.Play();
            }

            yield return new WaitForSeconds(typingSpeed); // Thời gian giữa các ký tự
        }

        // Hiển thị nút "Tiếp tục" khi chữ chạy xong
        continueButton.gameObject.SetActive(true);
        soundchu.Stop(); // Dừng âm thanh khi hoàn thành hội thoại
    }


    void OnContinueClicked()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Length)
        {
            continueButton.gameObject.SetActive(false); // Ẩn nút "Tiếp tục" khi chuyển hội thoại
            ShowDialogue(); // Hiển thị hội thoại tiếp theo
        }
        else
        {
            // Nếu hết hội thoại, chuyển cảnh
            //kiểm tra nếu người chơi đã lưu intro = 1
            SceneManager.LoadSceneAsync(3);
        }
    }
}
