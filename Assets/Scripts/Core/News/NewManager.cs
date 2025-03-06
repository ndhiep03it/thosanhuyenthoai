using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewManager : MonoBehaviour
{
    public static NewManager singleton;
    public string[] Lines;
    public string[] FinalNews;

    public GameObject NewPrefabs;
    public GameObject PANEL_LOADTIN;
    public Transform ContentNews;
    public GameObject PANEL_NEWPRO;
    public Text txtTitlePro;
    public Text txtNewChidlrenPro;
    public Text txtNewLinkPro;



    private Dictionary<string, GameObject> existingNews = new Dictionary<string, GameObject>();

    [Obsolete]
    protected void OnEnable()
    {
        StartCoroutine(NewServer("https://drive.google.com/uc?export=download&id=1VIXQT68x2yCseOQMJ7dxWFwGRiRHH4wb"));
    }
    private void OnDisable()
    {
        PANEL_NEWPRO.SetActive(false);

    }

    private void Awake()
    {
        if (singleton == null) singleton = this;
    }
    private void Update()
    {
        if (ContentNews.childCount <= 0)
        {
            PANEL_LOADTIN.SetActive(true);
        }
        else
        {
            PANEL_LOADTIN.SetActive(false);
        }
    }

    [Obsolete]
    protected IEnumerator NewServer(string url)
    {
        WWW data = new WWW(url);
        yield return data;

        Lines = data.text.Split('\n');
        HashSet<string> currentNewsIds = new HashSet<string>();

        for (int i = 0; i < Lines.Length; i++)
        {
            FinalNews = Lines[i].Split('*');
            string newsId = FinalNews[0]; // Assuming the title is unique for each news item

            currentNewsIds.Add(newsId);

            // If the news item does not exist, add it
            if (!existingNews.ContainsKey(newsId))
            {
                GameObject n = Instantiate(NewPrefabs, ContentNews, false);
                n.GetComponent<NewContent>().txtTitle.text = FinalNews[0];
                n.GetComponent<NewContent>().txtContent.text = FinalNews[1];
                n.GetComponent<NewContent>().txtLink.text = FinalNews[2];
                n.GetComponent<NewContent>().link = FinalNews[2];

                existingNews.Add(newsId, n);
            }
        }

        // Remove news items that are no longer in the source
        List<string> newsToRemove = new List<string>();

        foreach (var newsId in existingNews.Keys)
        {
            if (!currentNewsIds.Contains(newsId))
            {
                Destroy(existingNews[newsId]);
                newsToRemove.Add(newsId);
            }
        }

        // Clean up removed news items from the dictionary
        foreach (var newsId in newsToRemove)
        {
            existingNews.Remove(newsId);
            //foreach(Transform children in ContentNews)
            //{
            //    Destroy(children.gameObject);
            //    Load();
            //}
        }

        // Ensure UI updates even when the last item is removed
        LayoutRebuilder.ForceRebuildLayoutImmediate(ContentNews.GetComponent<RectTransform>());
    }

    public void Load()
    {
        HashSet<string> currentNewsIds = new HashSet<string>();
        for (int i = 0; i < Lines.Length; i++)
        {
            FinalNews = Lines[i].Split('*');
            string newsId = FinalNews[0]; // Assuming the title is unique for each news item

            currentNewsIds.Add(newsId);

            // If the news item does not exist, add it
            if (!existingNews.ContainsKey(newsId))
            {
                GameObject n = Instantiate(NewPrefabs, ContentNews, false);
                n.GetComponent<NewContent>().txtTitle.text = FinalNews[0];
                n.GetComponent<NewContent>().txtContent.text = FinalNews[1];
                n.GetComponent<NewContent>().txtLink.text = FinalNews[2];

                existingNews.Add(newsId, n);
            }
        }
    }

    public void SetProNew(string newTitle, string newContent,string link)
    {
        PANEL_NEWPRO.SetActive(true);
        txtTitlePro.text = newTitle;
        txtNewChidlrenPro.text = newContent;
        txtNewLinkPro.text = link;
    }
}
