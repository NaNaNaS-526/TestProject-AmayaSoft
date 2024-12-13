using System;
using UnityEngine;

[Serializable]
public class TileConfig
{
	[field: SerializeField] public Sprite TileSprite { get; private set; }
	[field: SerializeField] public float TileRotation { get; private set; }
	[field: SerializeField] public string TileSymbol { get; private set; }
}