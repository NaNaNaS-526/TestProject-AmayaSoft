using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TilesGroupConfig", menuName = "TilesGroupConfig", order = 1)]
public class TilesGroupConfig : ScriptableObject
{
	[field: SerializeField] public TileType TilesType { get; private set; }
	[field: SerializeField] public Vector2 SpriteSize { get; private set; }
	[field: SerializeField] public List<TileConfig> TilesConfigs { get; private set; }
}