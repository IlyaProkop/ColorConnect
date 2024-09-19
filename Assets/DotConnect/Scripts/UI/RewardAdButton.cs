using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using YG;

namespace BizzyBeeGames.DotConnect
{
	[RequireComponent(typeof(Button))]
	public class RewardAdButton : MonoBehaviour
	{
		#region Inspector Variables

		[SerializeField] private int hintsToReward;

		#endregion

		#region Properties

		public Button Button { get { return gameObject.GetComponent<Button>(); } }

		#endregion

		#region Unity Methods

		//private void Awake()
		//{
		//	Button.onClick.AddListener(OnClick);
		//}

        //void OnEnable()
        //{
        //    YandexGame.RewardVideoEvent += Rewarded;
        //}
        //private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

        void Rewarded(int id)
        {
            if (id == 0)
                OnRewardAdGranted();
        }

        #endregion

        #region Private Methods

  //      private void OnClick()
		//{
  //          YandexGame.RewVideoShow(0);
  //      }

		private void OnRewardAdGranted()
		{
			// Give the hints
			GameManager.Instance.GiveHints(hintsToReward);

			// Show the popup to the user so they know they got the hint
			PopupManager.Instance.Show("reward_ad_granted");
            SaveManager.Instance.Save();
        }

		#endregion
	}
}
