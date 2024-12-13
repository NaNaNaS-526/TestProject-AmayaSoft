using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private SpriteRenderer _image;

	public TileConfig Config { get; private set; }
	private IAnswerChecker _answerChecker;

	public void Initialize(IAnswerChecker answerChecker, TileConfig config)
	{
		_answerChecker = answerChecker;
		Config = config;
	}

	public void SetSprite(Sprite sprite, Vector2 size, float rotation)
	{
		_image.sprite = sprite;
		_image.transform.localScale = new Vector3(size.x, size.y, 1);
		_image.transform.rotation = Quaternion.Euler(0, 0, rotation);
	}

	public void SetTileSize(Vector2 size)
	{
		transform.localScale = new Vector3(size.x, size.y, 1);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		bool isCorrect = _answerChecker.CheckAnswer(Config);
		if (!isCorrect)
		{
			ShakeTile();
		}
		else
		{
			BounceTile();
		}
	}

	private void ShakeTile()
	{
		var shakeDuration = 0.3F;
		var shakeStrength = 0.3F;
		var shakeVibrato = 10;
		var shakeRandomness = 0F;

		transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, false, false)
			.SetEase(Ease.InBounce);
	}

	private void BounceTile()
	{
		var punchStrength = 0.1F;
		var duration = 1.0F;
		var vibrato = 4;
		var elasticity = 1.0F;

		var normalScale = _image.transform.localScale;

		_image.transform.DOScale(normalScale, 0.0F)
			.SetDelay(0.05F)
			.OnStart(() => _image.transform.DOPunchScale(
				new Vector3(punchStrength, punchStrength, punchStrength),
				duration,
				vibrato,
				elasticity)
			);
	}
}