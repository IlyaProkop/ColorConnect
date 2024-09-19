using BizzyBeeGames.DotConnect;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    [SerializeField] Button playButton;
    [SerializeField] private LevelListScreen levelListScreen;

    private List<BundleInfo> bundleInfos;

    private int currentPackIndex = 0;
    private int currentLevelIndex = 0;

    public int CurrentPackIndex
    {
        get => currentPackIndex; set
        {
            if (value > bundleInfos[0].PackInfos.Count - 1)
            {
                return;
            }
            currentPackIndex = value;
            currentLevelIndex = 0;
        }
    }
    public int CurrentLevelIndex
    {
        get => currentLevelIndex;
        set
        {
            if (value > bundleInfos[0].PackInfos[currentPackIndex].LevelDatas.Count - 1)
            {

                CurrentPackIndex++;
            }

        }
    }

    private void Awake()
    {
       // PlayerPrefs.DeleteAll();
        instance = this;
        playButton.onClick.AddListener(() => StartLevel(
            LoadData()[0],
            LoadData()[1]
        ));

        bundleInfos = GameManager.Instance.BundleInfos;

        CurrentPackIndex = LoadData()[0];
        CurrentLevelIndex = LoadData()[1];
    }

    public void UpLevel()
    {
        CurrentLevelIndex++;
        SaveData(new int[] { currentPackIndex, ++currentLevelIndex });
        //  StartLevel(CurrentPackIndex, currentLevelIndex);
    }
    private void StartLevel(int packIndex, int levelIndex)
    {
        levelListScreen.CallOnLevelListItemClickedUnLocked(GetPackInfo(packIndex), GetLevelData(GetPackInfo(packIndex), levelIndex));
    }

    private int[] LoadData()
    {
        Debug.Log("LoadData: " + PlayerPrefs.GetInt("currentPackIndex") + "  " + PlayerPrefs.GetInt("currentLevelIndex"));
        return new int[] { PlayerPrefs.GetInt("currentPackIndex"), PlayerPrefs.GetInt("currentLevelIndex") };
    }
    private void SaveData(int[] data)
    {
        PlayerPrefs.SetInt("currentPackIndex", data[0]);
        PlayerPrefs.SetInt("currentLevelIndex", data[1]);
        Debug.Log("SaveData: " + data[0] + "  " + data[1]);
    }

    private LevelData GetLevelData(PackInfo packInfo, int indexLevel)
    {
        return packInfo.LevelDatas[indexLevel];

    }

    private PackInfo GetPackInfo(int packIndex)
    {
        return bundleInfos[0].PackInfos[packIndex];
    }
}
