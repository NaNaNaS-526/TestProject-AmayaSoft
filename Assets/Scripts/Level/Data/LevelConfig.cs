using Cards.Data;
using UnityEngine;

namespace Level.Data
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig", order = 1)]
	public class LevelConfig : ScriptableObject
	{
		[field: SerializeField] public int Rows { get; private set; }
		[field: SerializeField] public int Columns { get; private set; }
		[field: SerializeField] public CardType LevelType { get; private set; }
	}
}