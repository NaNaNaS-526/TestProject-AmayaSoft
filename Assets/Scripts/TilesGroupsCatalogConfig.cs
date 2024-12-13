using System.Collections.Generic;
using UnityEngine;

public class TilesGroupsCatalogConfig : ScriptableObject
{
	[field: SerializeField] public List<TilesGroupConfig> TileGroupsConfigs { get; private set; }
}