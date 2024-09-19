using UnityEngine;
using UnityEngine.UI;
//using YG;

namespace BizzyBeeGames.DotConnect
{
    public class LevelListScreen : Screen
    {
        #region Inspector Variables

        [Space]

        [SerializeField] private LevelListItem levelListItemPrefab = null;
        [SerializeField] private RectTransform levelListContainer = null;
        [SerializeField] private ScrollRect levelListScrollRect = null;
        [SerializeField] private Text packNameText = null;

        #endregion

        #region Member Variables

        private PackInfo currentPackInfo;
        private RecyclableListHandler<LevelData> levelListHandler;

        #endregion

        #region Properties

        #endregion

        #region Unity Methods

        #endregion

        #region Public Methods

        public override void Initialize()
        {
            base.Initialize();

            GameEventManager.Instance.RegisterEventHandler(GameEventManager.EventId_PackSelected, OnPackSelected);
        }

        public override void Show(bool back, bool immediate)
        {
            base.Show(back, immediate);

            if (back)
            {
                levelListHandler.Refresh();
            }
        }

        #endregion

        #region Private Methods

        private void OnPackSelected(string eventId, object[] data)
        {
            PackInfo selectedPackInfo = data[0] as PackInfo;

            if (currentPackInfo != selectedPackInfo)
            {
                UpdateList(selectedPackInfo);
            }
        }

        private void UpdateList(PackInfo packInfo)
        {
            currentPackInfo = packInfo;

            //if (YandexGame.savesData.language == "ru")
            //{
            //    if (packInfo.packName == "BEGINNER")
            //    {
            //        packNameText.text = "НОВИЧОК";
            //    }
            //    else if (packInfo.packName == "EASY")
            //    {
            //        packNameText.text = "ЛЕГКИЙ";
            //    }
            //    else if (packInfo.packName == "NOT SO BAD")
            //    {
            //        packNameText.text = "СРЕДНИЙ";
            //    }
            //    else if (packInfo.packName == "CHALLENGING")
            //    {
            //        packNameText.text = "СЛОЖНЫЙ";
            //    }
            //    else if (packInfo.packName == "PRETTY HARD")
            //    {
            //        packNameText.text = "МАСТЕР";
            //    }
            //    else if (packInfo.packName == "VERY DIFFICULT")
            //    {
            //        packNameText.text = "ЭКСПЕРТ";
            //    }
            //    else if (packInfo.packName == "IMPOSSIBLE")
            //    {
            //        packNameText.text = "СУПЕР ЭКСПЕРТ";
            //    }
            //    else if (packInfo.packName == "BLOCKERS - STARTER")
            //    {
            //        packNameText.text = "СТАРТ";
            //    }
            //    else if (packInfo.packName == "BLOCKERS - BONUS")
            //    {
            //        packNameText.text = "БОНУС";
            //    }
            //    else if (packInfo.packName == "BLOCKERS - PREMIUM")
            //    {
            //        packNameText.text = "ПРЕМИУМ";
            //    }
            //    else if (packInfo.packName == "SHAPES - STARTER")
            //    {
            //        packNameText.text = "ФИГУРА - 1";
            //    }
            //    else if (packInfo.packName == "SHAPES - BONUS")
            //    {
            //        packNameText.text = "ФИГУРА - 2";
            //    }
            //}
            //else
            //{
            packNameText.text = packInfo.packName;
            Debug.Log("packNameText: " + packInfo.packName);
            //}

            if (levelListHandler == null)
            {
                levelListHandler = new RecyclableListHandler<LevelData>(packInfo.LevelDatas, levelListItemPrefab, levelListContainer, levelListScrollRect);
                levelListHandler.OnListItemClicked = OnLevelListItemClicked;
                levelListHandler.Setup();
            }
            else
            {
                levelListHandler.UpdateDataObjects(packInfo.LevelDatas);
            }
        }

        private void OnLevelListItemClicked(LevelData levelData)
        {
            if (!GameManager.Instance.IsLevelLocked(levelData))
            {
                GameManager.Instance.StartLevel(currentPackInfo, levelData);
            }
        }

        public void CallOnLevelListItemClickedUnLocked(PackInfo currentPackInfo, LevelData levelData)
        {
            GameManager.Instance.StartLevel(currentPackInfo, levelData);            
        }

        #endregion
    }
}
