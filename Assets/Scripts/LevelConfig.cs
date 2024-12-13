using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
	[field: SerializeField] public int Rows { get; private set; }
	[field: SerializeField] public int Columns { get; private set; }
	[field: SerializeField] public TileType LevelType { get; private set; }
}