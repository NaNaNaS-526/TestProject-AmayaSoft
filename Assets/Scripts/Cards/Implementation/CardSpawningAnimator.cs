using System.Collections.Generic;
using Cards.Presentation;
using DG.Tweening;
using UnityEngine;

namespace Cards.Implementation
{
	public class CardSpawningAnimator
	{
		public void AnimateCards(List<Card> spawnedCards)
		{
			for (var index = 0; index < spawnedCards.Count; index++)
			{
				var card = spawnedCards[index];
				AnimateCardAppearance(card, index);
			}
		}

		private void AnimateCardAppearance(Card card, int index)
		{
			var smallScale = Vector3.zero;
			var normalScale = card.transform.localScale;
			var punchStrength = 0.5F;
			var duration = 0.9F;
			var vibrato = 4;
			var elasticity = 1.0F;
			var delayOffset = 0.5F;

			card.transform.localScale = smallScale;

			card.transform
				.DOScale(normalScale, 0.0F)
				.SetDelay(0.1F + delayOffset * index)
				.OnStart(() => card.transform.DOPunchScale(
					new Vector3(punchStrength, punchStrength, punchStrength),
					duration,
					vibrato,
					elasticity)
				);
		}
	}
}