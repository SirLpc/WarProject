using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DajiaGame.Px
{
    public class UIView : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreTxt;
        [SerializeField]
        private Button _restartBtn;

        public void Start()
        {
            GameCtr.Instance.OnScoreChanged.AddListener(SetScoreText);
            _restartBtn.onClick.AddListener(OnClicRestart);
        }

        public void OnDestroy()
        {
            if(GameCtr.Instance != null)
                GameCtr.Instance.OnScoreChanged.RemoveListener(SetScoreText);
            _restartBtn.onClick.RemoveListener(OnClicRestart);
        }

        private void SetScoreText(int score)
        {
            _scoreTxt.text = string.Format("得分:{0}", score);
        }

        private void OnClicRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
