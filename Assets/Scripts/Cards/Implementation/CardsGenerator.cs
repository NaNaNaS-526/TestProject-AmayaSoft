using System.Collections.Generic;
using Cards.Data;
using Cards.Presentation;
using Core.Declaration;
using Level.Data;
using UnityEngine;
using Utils;

namespace Cards.Implementation
{
	public class CardsGenerator
	{
		private readonly IAnswerChecker _answerChecker;
		private readonly ParticleSystemService _particleSystemService;
		private readonly Card _cardPrefab;
		private readonly Vector2 _cardSize;
		private LevelConfig _levelConfig;
		private CardsGroupConfig _cardsGroupConfig;

		public CardsGenerator(Card cardPrefab, Vector2 cardSize, LevelConfig levelConfig, CardsGroupConfig cardsGroupConfig, IAnswerChecker answerChecker, ParticleSystemService particleSystemService)
		{
			_cardPrefab = cardPrefab;
			_cardSize = cardSize;
			_levelConfig = levelConfig;
			_cardsGroupConfig = cardsGroupConfig;
			_answerChecker = answerChecker;
			_particleSystemService = particleSystemService;
			SpawnedCards = new List<Card>();
		}

		public List<Card> SpawnedCards { get; }


		public void GenerateCards(LevelConfig levelConfig, CardsGroupConfig cardsGroupConfig)
		{
			_levelConfig = levelConfig;
			_cardsGroupConfig = cardsGroupConfig;
			var gridOffsets = CalculateGridOffsets();
			var availableCardConfigs = InitializeCardConfigs();

			if (SpawnedCards.Count < levelConfig.Rows * levelConfig.Columns)
			{
				var neededCards = levelConfig.Rows * levelConfig.Columns - SpawnedCards.Count;

				for (var i = 0; i < neededCards; i++)
				{
					var row = (SpawnedCards.Count + i) / levelConfig.Columns;
					var column = (SpawnedCards.Count + i) % levelConfig.Columns;
					var cardPosition = CalculateCardPosition(row, column, gridOffsets);
					var card = CreateCard(cardPosition);
					AssignCardConfig(card, availableCardConfigs);
				}
			}

			for (var row = 0; row < levelConfig.Rows; row++)
			{
				for (var column = 0; column < levelConfig.Columns; column++)
				{
					var card = GetCardAt(row, column);
					var cardPosition = CalculateCardPosition(row, column, gridOffsets);
					card.transform.position = cardPosition;
				}
			}
		}

		private Vector2 CalculateGridOffsets()
		{
			var offsetX = (_levelConfig.Columns - 1) * _cardSize.x * 0.5F;
			var offsetY = (_levelConfig.Rows - 1) * _cardSize.y * 0.5F;
			return new Vector2(offsetX, offsetY);
		}

		private Vector2 CalculateCardPosition(int row, int column, Vector2 gridOffsets)
		{
			var positionX = column * _cardSize.x - gridOffsets.x;
			var positionY = -(row * _cardSize.y - gridOffsets.y);
			var position = new Vector2(positionX, positionY);
			return position;
		}


		private Card GetCardAt(int row, int column)
		{
			var index = row * _levelConfig.Columns + column;
			return index < SpawnedCards.Count ? SpawnedCards[index] : null;
		}


		private List<CardConfig> InitializeCardConfigs()
		{
			return new List<CardConfig>(_cardsGroupConfig.CardsConfigs);
		}


		private Card CreateCard(Vector2 position)
		{
			var spawnedCard = Object.Instantiate(_cardPrefab, position, Quaternion.identity);
			spawnedCard.transform.SetParent(null);
			spawnedCard.SetCardSize(_cardSize);
			SpawnedCards.Add(spawnedCard);
			return spawnedCard;
		}

		private void AssignCardConfig(Card card, List<CardConfig> availableCardConfigs)
		{
			var randomIndex = Random.Range(0, availableCardConfigs.Count);
			var randomCardConfig = availableCardConfigs[randomIndex];
			availableCardConfigs.RemoveAt(randomIndex);
			card.Initialize(_answerChecker, randomCardConfig, _particleSystemService);
			card.SetSprite(
				randomCardConfig.CardSprite,
				_cardsGroupConfig.SpriteSize,
				randomCardConfig.CardRotation
			);
		}
	}
}