using System.Collections.Generic;
using System.Linq;
using Cards.Data;
using Cards.Presentation;
using Core.Declaration;
using UnityEngine;

namespace Cards.Implementation
{
	public class CardsSelectionService
	{
		private readonly IAnswerChecker _answerChecker;
		private readonly HashSet<CardConfig> _usedConfigs = new HashSet<CardConfig>();

		public CardsSelectionService(IAnswerChecker answerChecker)
		{
			_answerChecker = answerChecker;
		}

		public void SelectCorrectCard(List<Card> spawnedCards)
		{
			foreach (var potentialCard in spawnedCards.Select(card => spawnedCards[Random.Range(0, spawnedCards.Count)]).Where(potentialCard => !_usedConfigs.Contains(potentialCard.Config)))
			{
				_usedConfigs.Add(potentialCard.Config);
				_answerChecker.SetCorrectAnswer(potentialCard.Config);
				return;
			}
		}
	}
}