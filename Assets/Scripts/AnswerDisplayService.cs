using System.Collections;
using TMPro;
using UnityEngine;

public class AnswerDisplayService : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _answerText;
	[SerializeField] private float _fadeDuration = 1.0F;

	public void DisplayCorrectAnswerText(string symbol)
	{
		var text = _answerText.text;
		_answerText.text = $"{text} {symbol}";
		_answerText.gameObject.SetActive(true);
		StopAllCoroutines();
		StartCoroutine(FadeAnswerText(0.0F, 1.0F, _fadeDuration));
	}

	public void HideAnswerText()
	{
		StopAllCoroutines();
		StartCoroutine(FadeAnswerText(1.0F, 0.0F, _fadeDuration));
	}


	private IEnumerator FadeAnswerText(float startAlpha, float endAlpha, float duration)
	{
		var startColor = _answerText.color;
		startColor.a = startAlpha;
		_answerText.color = startColor;
		var delay = duration / 2;
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
}