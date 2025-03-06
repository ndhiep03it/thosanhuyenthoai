using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewContent : MonoBehaviour
{
    public Text txtTitle;
    public Text txtContent;
    public Text txtLink;
    public string link;

    public void OpenLink()
    {
        Application.OpenURL(txtLink.text);
    }
    public void ShowNew()
    {
        NewManager.singleton.SetProNew(txtTitle.text,txtContent.text,link);
    }
}
