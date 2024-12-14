using System.Collections;
using System.Collections.Generic;
using Core.Declaration;
using DG.Tweening;
using Level.Data;
using Level.Declaration;
using Cards.Data;
using Cards.Declaration;
using Cards.Implementation;
using Cards.Presentation;
using UnityEngine;
using Utils;
using VContainer;

namespace Core.Presentation
{
	public class Grid : MonoBehaviour
	{
		[SerializeField] private Card _cardPrefab;
		[SerializeField] private Vector2 _cardSize;
		private IAnswerChecker _answerChecker;
		private LevelConfig _levelConfig;
		private ILevelService _levelService;
		private ParticleSystemService _particleSystemService;

		private CardsGenerator _cardsGenerator;
		private CardsSelectionService _cardsSelectionService;

		private ICardsGroupProvider _cardsGroupProvider;
		private CardSpawningAnimator _cardSpawningAnimator;

		[Inject]
		private void Construct(ICardsGroupProvider cardsGroupProvider, IAnswerChecker answerChecker, ParticleSystemService particleSystemService, ILevelService levelService)
		{
			_cardsGroupProvider = cardsGroupProvider;
			_answerChecker = answerChecker;
			_particleSystemService = particleSystemService;
			_levelService = levelService;
		}

		private void Start()
		{
			DOTween.Init();

			_levelConfig = _levelService.GetCurrentLevel();
			var cardsGroupConfig = _cardsGroupProvider.GetCardGroupForCurrentLevel();
			_cardsGenerator = new CardsGenerator(_cardPrefab, _cardSize, _levelConfig, cardsGroupConfig, _answerChecker, _particleSystemService);
			_cardSpawningAnimator = new CardSpawningAnimator();
			_cardsSelectionService = new CardsSelectionService(_answerChecker);
			_levelService.OnLevelUpdated += UpdateCards;
			CreateGrid();
		}

		private void CreateGrid()
		{
			_cardsGenerator.GenerateCards(_levelConfig, _cardsGroupProvider.GetCardGroupForCurrentLevel());
			_cardSpawningAnimator.AnimateCards(_cardsGenerator.SpawnedCards);
			_cardsSelectionService.SelectCorrectCard(_cardsGenerator.SpawnedCards);
		}

		private void UpdateCards()
		{
			StartCoroutine(UpdateCardsWithDelay());
		}

		private IEnumerator UpdateCardsWithDelay()
		{
			yield return new WaitForSeconds(1.3F);

			var cardsGroupConfig = _cardsGroupProvider.GetCardGroupForCurrentLevel();
			_levelConfig = _levelService.GetCurrentLevel();

			_cardsGenerator.GenerateCards(_levelConfig, _cardsGroupProvider.GetCardGroupForCurrentLevel());

			var usedCardConfigs = new List<CardConfig>();

			foreach (var card in _cardsGenerator.SpawnedCards)
			{
				CardConfig randomCardConfig;

				do
				{
					randomCardConfig = cardsGroupConfig.CardsConfigs[Random.Range(0, cardsGroupConfig.CardsConfigs.Count)];
				} while (usedCardConfigs.Contains(randomCardConfig));

				usedCardConfigs.Add(randomCardConfig);

				card.Initialize(_answerChecker, randomCardConfig, _particleSystemService);
				card.SetSprite(randomCardConfig.CardSprite, cardsGroupConfig.SpriteSize, randomCardConfig.CardRotation);
			}

			_cardsSelectionService.SelectCorrectCard(_cardsGenerator.SpawnedCards);
		}
	}
}