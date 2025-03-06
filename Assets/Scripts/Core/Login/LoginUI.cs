using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public static LoginUI Instance;
    public GameObject LoginObject;
    public GameObject AccountObject;
    public InputField input_UserName;
    public InputField input_PassWord;
    public Text textLoginAccount;


    protected void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        textLoginAccount.text = "Chơi TK:" + PlayerPrefs.GetString("user");
    }




}
