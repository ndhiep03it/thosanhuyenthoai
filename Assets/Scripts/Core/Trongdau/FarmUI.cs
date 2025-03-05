using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FarmUI : MonoBehaviour
{
    //public FarmManager farmManager;
    //public GameObject plotPrefab;
    //public Transform gridLayout;
    //public List<Button> plotButtons = new List<Button>();

    //void Start()
    //{
    //    GeneratePlots();
    //}

    //void GeneratePlots()
    //{
    //    for (int i = 0; i < farmManager.farmSize; i++)
    //    {
    //        GameObject plotObj = Instantiate(plotPrefab, gridLayout);
    //        Button plotButton = plotObj.GetComponent<Button>();
    //        int index = i;
    //        plotButton.onClick.AddListener(() => OnPlotClicked(index));
    //        plotButtons.Add(plotButton);
    //    }
    //    UpdateUI();
    //}

    //void OnPlotClicked(int index)
    //{
    //    if (farmManager.farmData.plots[index].isPlanted)
    //    {
    //        if (farmManager.CheckGrowth(index))
    //        {
    //            farmManager.HarvestBean(index);
    //            Debug.Log($"Thu hoạch ô {index}!");
    //        }
    //        else
    //        {
    //            Debug.Log($"Đậu ở ô {index} chưa chín!");
    //        }
    //    }
    //    else
    //    {
    //        farmManager.PlantBean(index, 10); // Ví dụ thời gian trồng là 10 giây
    //        Debug.Log($"Trồng đậu tại ô {index}!");
    //    }
    //    UpdateUI();
    //}

    //void UpdateUI()
    //{
    //    for (int i = 0; i < plotButtons.Count; i++)
    //    {
    //        var plot = farmManager.farmData.plots[i];
    //        plotButtons[i].GetComponentInChildren<Text>().text = plot.isPlanted ? (plot.isReady ? "🌿 Thu hoạch!" : "🌱 Đang lớn...") : "🟩 Trống";
    //    }
    //}
}
