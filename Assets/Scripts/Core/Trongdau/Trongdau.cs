using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Bean
{
    public int id;
    public long plantTime;
    public int growTime;
}

[System.Serializable]
public class BeanData
{
    public List<Bean> beans = new List<Bean>();
}

public class Trongdau : MonoBehaviour
{
    private string savePath;
    private BeanData beanData;

    void Start()
    {
        savePath = Application.persistentDataPath + "/beans.json";
        LoadData();
    }

    public void PlantBean(int growTime)
    {
        long currentTimestamp = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Bean newBean = new Bean { id = beanData.beans.Count + 1, plantTime = currentTimestamp, growTime = growTime };
        beanData.beans.Add(newBean);
        SaveData();
        Debug.Log("Trồng đậu thần ID: " + newBean.id);
    }

    public List<Bean> GetHarvestableBeans()
    {
        long currentTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return beanData.beans.FindAll(bean => (currentTime - bean.plantTime) >= bean.growTime);
    }

    public void HarvestAllBeans()
    {
        List<Bean> harvestableBeans = GetHarvestableBeans();
        if (harvestableBeans.Count > 0)
        {
            foreach (var bean in harvestableBeans)
            {
                Debug.Log("Thu hoạch đậu thần ID: " + bean.id);
            }
            beanData.beans.RemoveAll(bean => harvestableBeans.Contains(bean));
            SaveData();
        }
        else
        {
            Debug.Log("Không có đậu nào để thu hoạch.");
        }
    }

    void SaveData()
    {
        string json = JsonUtility.ToJson(beanData, true);
        File.WriteAllText(savePath, json);
    }

    void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            beanData = JsonUtility.FromJson<BeanData>(json);
        }
        else
        {
            beanData = new BeanData();
        }
    }
}
