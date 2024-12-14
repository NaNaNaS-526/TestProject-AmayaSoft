using System.Collections.Generic;
using UnityEngine;

namespace Level.Data
{
	public class LevelsCatalogConfig : ScriptableObject
	{
		[field: SerializeField] public List<LevelConfig> LevelConfigs { get; private set; }
	}
}
