using System.Collections.Generic;
using UnityEngine;

namespace Cards.Data
{
	public class CardsGroupsCatalogConfig : ScriptableObject
	{
		[field: SerializeField] public List<CardsGroupConfig> CardGroupsConfigs { get; private set; }
	}
}
