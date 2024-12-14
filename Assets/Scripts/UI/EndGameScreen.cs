using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class EndGameScreen : MonoBehaviour
	{
		[SerializeField] private Image _endGamePanel;
		[SerializeField] private Button _restartButton;

		private void Awake()
		{
			var color = _endGamePanel.color;
			_endGamePanel.color = new Color(color.r, color.g, color.b, 0.0F);
			_endGamePanel.raycastTarget = false;

			_restartButton.gameObject.SetActive(false);
			_restartButton.onClick.AddListener(RestartGame);
		}

		public void Show()
		{
			StartCoroutine(FadeCoroutine(0.7F, 1.0F));
		}

		private void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		private IEnumerator FadeCoroutine(float targetAlpha, float duration)
		{
			var startAlpha = _endGamePanel.color.a;
			var elapsed = 0.0F;

			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				var newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
				var color = _endGamePanel.color;
				_endGamePanel.color = new Color(color.r, color.g, color.b, newAlpha);
				yield return null;
			}

			var finalColor = _endGamePanel.color;
			_endGamePanel.color = new Color(finalColor.r, finalColor.g, finalColor.b, targetAlpha);
			_restartButton.gameObject.SetActive(true);
			_endGamePanel.raycastTarget = true;
		}
	}
}