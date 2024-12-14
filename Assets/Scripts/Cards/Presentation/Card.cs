using System.Collections;
using Cards.Data;
using Core.Declaration;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Cards.Presentation
{
	public class Card : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private SpriteRenderer _image;
		[SerializeField] private ParticleSystem _particleSystem;
		public CardConfig Config { get; private set; }
		private IAnswerChecker _answerChecker;
		private ParticleSystemService _particleSystemService;

		private bool _isInteractable = true;

		public void Initialize(IAnswerChecker answerChecker, CardConfig config, ParticleSystemService particleSystemService)
		{
			_answerChecker = answerChecker;
			Config = config;
			_particleSystemService = particleSystemService;
		}

		public void SetSprite(Sprite sprite, Vector2 size, float rotation)
		{
			_image.sprite = sprite;
			_image.transform.rotation = Quaternion.Euler(0.0F, 0.0F, rotation);
			_image.transform.localScale = new Vector3(size.x, size.y, 1.0F);
			StopAllCoroutines();
			_isInteractable = true;
		}

		public void SetCardSize(Vector2 size)
		{
			transform.localScale = new Vector3(size.x, size.y, 1);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!_isInteractable)
			{
				return;
			}
			StopAllCoroutines();
			StartCoroutine(DisableClickTemporarily(0.8F));
			Check();
		
		}

		private IEnumerator DisableClickTemporarily(float duration)
		{
			_isInteractable = false;

			yield return new WaitForSeconds(duration);

			_isInteractable = true;
		}

		private void Check()
		{
			var isCorrect = _answerChecker.CheckAnswer(Config);
			if (!isCorrect)
			{
				ShakeCard();
			}
			else
			{
				BounceCard();
				TriggerParticles();
				StopAllCoroutines();
				StartCoroutine(DisableClickTemporarily(2.0F));
			}
		}

		private void ShakeCard()
		{
			var shakeDuration = 0.6F;
			var shakeStrength = 0.2F;
			var shakeVibrato = 2;
			var startPosition = _image.transform.localPosition;
			var calculatedDuration = shakeDuration / (shakeVibrato * 2);


			DOTween.Sequence()
				.Append(CreateShakeTween(startPosition.x - shakeStrength, calculatedDuration))
				.Append(CreateShakeTween(startPosition.x + shakeStrength, calculatedDuration))
				.SetLoops(shakeVibrato, LoopType.Yoyo)
				.OnComplete(() => { _image.transform.localPosition = startPosition; });
		}

		private Tweener CreateShakeTween(float targetX, float duration)
		{
			return _image.transform.DOLocalMoveX(targetX, duration).SetEase(Ease.InOutSine);
		}

		private void BounceCard()
		{
			var punchStrength = 0.1F;
			var duration = 1.0F;
			var vibrato = 4;
			var elasticity = 1.0F;
			var normalScale = _image.transform.localScale;

			_image.transform
				.DOScale(normalScale, 0.0F)
				.SetDelay(0.05F)
				.OnStart(() => _image.transform.DOPunchScale(
					new Vector3(punchStrength, punchStrength, punchStrength),
					duration,
					vibrato,
					elasticity))
				.OnComplete(() => _image.transform.localScale = normalScale)
				.OnKill(() => _image.transform.localScale = normalScale);
		}

		private void TriggerParticles()
		{
			_particleSystemService.ActivateParticleSystem(_particleSystem, transform.position);
		}
	}
}