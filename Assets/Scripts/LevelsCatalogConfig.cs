using System.Collections.Generic;
using UnityEngine;

public class LevelsCatalogConfig : ScriptableObject
{
	[field: SerializeField] public List<LevelConfig> LevelConfigs { get; private set; }
}