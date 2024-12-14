using System.Collections.Generic;
using UnityEngine;

namespace Cards.Data
{
	[CreateAssetMenu(fileName = "CardsGroupConfig", menuName = "CardsGroupConfig", order = 1)]
	public class CardsGroupConfig : ScriptableObject
	{
		[field: SerializeField] public CardType CardsType { get; private set; }
		[field: SerializeField] public Vector2 SpriteSize { get; private set; }
		[field: SerializeField] public List<CardConfig> CardsConfigs { get; private set; }
	}
}