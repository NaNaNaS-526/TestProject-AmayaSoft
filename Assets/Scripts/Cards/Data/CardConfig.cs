using System;
using UnityEngine;

namespace Cards.Data
{
	[Serializable]
	public class CardConfig
	{
		[field: SerializeField] public Sprite CardSprite { get; private set; }
		[field: SerializeField] public float CardRotation { get; private set; }
		[field: SerializeField] public string CardSymbol { get; private set; }
	}
}