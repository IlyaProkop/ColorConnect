using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using YG;

namespace BizzyBeeGames.DotConnect
{
	public class PackListItem : ClickableListItem
	{
		#region Inspector Variables

		[SerializeField] private Text			nameText				= null;
		[SerializeField] private Text			descriptionText			= null;
		[SerializeField] private ProgressBar	progressBarContainer	= null;
		[SerializeField] private Text			progressText			= null;
		[Space]
		[SerializeField] private GameObject		lockedContainer			= null;
		[SerializeField] private GameObject		starsLockedContainer	= null;
		[SerializeField] private Text			starAmountText			= null;

		#endregion

		#region Public Variables

		public void Setup(PackInfo packInfo)
		{
			//if(YandexGame.savesData.language == "ru")
			//{
			//	if(packInfo.packName == "BEGINNER")
			//	{
   //                 nameText.text = "НОВИЧОК";
   //             }
			//	else if (packInfo.packName == "EASY")
			//	{
			//		nameText.text = "ЛЕГКИЙ";
   //             }
   //             else if (packInfo.packName == "NOT SO BAD")
   //             {
   //                 nameText.text = "СРЕДНИЙ";
   //             }
   //             else if (packInfo.packName == "CHALLENGING")
   //             {
   //                 nameText.text = "СЛОЖНЫЙ";
   //             }
   //             else if (packInfo.packName == "PRETTY HARD")
   //             {
   //                 nameText.text = "МАСТЕР";
   //             }
   //             else if (packInfo.packName == "VERY DIFFICULT")
   //             {
   //                 nameText.text = "ЭКСПЕРТ";
   //             }
   //             else if (packInfo.packName == "IMPOSSIBLE")
   //             {
   //                 nameText.text = "СУПЕР ЭКСПЕРТ";
   //             }
			//	else if (packInfo.packName == "BLOCKERS - STARTER")
			//	{
   //                 nameText.text = "СТАРТ";
   //             }
   //             else if (packInfo.packName == "BLOCKERS - BONUS")
   //             {
   //                 nameText.text = "БОНУС";
   //             }
   //             else if (packInfo.packName == "BLOCKERS - PREMIUM")
   //             {
   //                 nameText.text = "ПРЕМИУМ";
   //             }
   //             else if (packInfo.packName == "SHAPES - STARTER")
   //             {
   //                 nameText.text = "ФИГУРА - 1";
   //             }
   //             else if (packInfo.packName == "SHAPES - BONUS")
   //             {
   //                 nameText.text = "ФИГУРА - 2";
   //             }
   //         }
			//else
			//{
				nameText.text = packInfo.packName;
			//}
			
			descriptionText.text	= packInfo.packDescription;

			// Check if the pack is locked and update the ui
			bool isPackLocked = GameManager.Instance.IsPackLocked(packInfo);

			lockedContainer.SetActive(isPackLocked);
			progressBarContainer.gameObject.SetActive(!isPackLocked);
			starsLockedContainer.SetActive(isPackLocked && packInfo.unlockType == PackUnlockType.Stars);

			if (isPackLocked)
			{
				switch (packInfo.unlockType)
				{
					case PackUnlockType.Stars:
						starAmountText.text = packInfo.unlockStarsAmount + " ";
						break;
				}
			}
			else
			{
				int numLevelsInPack		= packInfo.levelFiles.Count;
				int numCompletedLevels	= GameManager.Instance.GetNumCompletedLevels(packInfo);

				progressBarContainer.SetProgress((float)numCompletedLevels / (float)numLevelsInPack);
				progressText.text = string.Format("{0} / {1}", numCompletedLevels, numLevelsInPack);
			}
		}

		#endregion

		#region Private Methods

		#endregion
	}
}
