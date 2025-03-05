using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text txtGold;
    public Text txtRuby;
    void Update()
    {
        InvokeRepeating("UImoney", 0f, 0.1f);
    }

    void UImoney()
    {
        txtGold.text = UImanager.Singleton.txtgold.text;
        txtRuby.text = UImanager.Singleton.txtruby.text;
    }
    private void OnDisable()
    {
        CancelInvoke("UImoney");
    }
}
