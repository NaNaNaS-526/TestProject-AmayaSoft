using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
	public class AnswerDisplayService : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _answerText;
		[SerializeField] private float _fadeDuration = 1.0F;

		private string _defaultAnswerText;
		private bool _isAnswerFaded;

		private void Awake()
		{
			_defaultAnswerText = _answerText.text;
		}

		public void DisplayCorrectAnswerText(string symbol)
		{
			_answerText.gameObject.SetActive(true);
			var answer = $"{_defaultAnswerText} {symbol}";
			ChangeAnswerText(answer);
			if (!_isAnswerFaded)
			{
				StopAllCoroutines();
				StartCoroutine(FadeAnswerText(0.0F, 1.0F, _fadeDuration));
				_isAnswerFaded = true;
			}
		}

		private IEnumerator FadeAnswerText(float startAlpha, float endAlpha, float duration)
		{
			var startColor = _answerText.color;
			startColor.a = startAlpha;
			_answerText.color = startColor;
			var delay = duration / 2.0F;
			yield return new WaitForSeconds(delay);
			var timeElapsed = 0.0F;

			while (timeElapsed < duration)
			{
				timeElapsed += Time.deltaTime;
				var alphaValue = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / duration);
				_answerText.color = new Color(_answerText.color.r, _answerText.color.g, _answerText.color.b, alphaValue);
				yield return null;
			}

			_answerText.color = new Color(_answerText.color.r, _answerText.color.g, _answerText.color.b, endAlpha);
		}

		private void ChangeAnswerText(string newText)
		{
			_answerText.text = newText;
		}
	}
}