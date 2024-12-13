using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class Grid : MonoBehaviour
{
	[SerializeField] private LevelConfig _levelConfig;
	[SerializeField] private Tile _tilePrefab;
	[SerializeField] private Vector2 _tileSize;

	private readonly List<Tile> _spawnedTiles = new List<Tile>();

	private TilesGroupConfig _tilesGroupConfig;

	private ITilesGroupService _tilesGroupService;
	private IAnswerChecker _answerChecker;
	private Tile _answerTile;

	[Inject]
	private void Construct(ITilesGroupService tilesGroupService, IAnswerChecker answerChecker)
	{
		_tilesGroupService = tilesGroupService;
		_answerChecker = answerChecker;
	}

	private void Start()
	{
		DOTween.Init();
		_tilesGroupConfig = _tilesGroupService.GetTileGroupByLevelConfig(_levelConfig);
		CreateGrid();
	}


	private void CreateGrid()
	{
		var gridOffsets = CalculateGridOffsets();
		var availableTileConfigs = InitializeTileConfigs();

		GenerateTiles(gridOffsets, availableTileConfigs);

		SelectCorrectTile();
	}

	private Vector2 CalculateGridOffsets()
	{
		var offsetX = (_levelConfig.Columns - 1) * _tileSize.x / 2.0F;
		var offsetY = (_levelConfig.Rows - 1) * _tileSize.y / 2.0F;
		return new Vector2(offsetX, offsetY);
	}

	private List<TileConfig> InitializeTileConfigs()
	{
		return new List<TileConfig>(_tilesGroupConfig.TilesConfigs);
	}

	private void GenerateTiles(Vector2 gridOffsets, List<TileConfig> availableTileConfigs)
	{
		for (var row = 0; row < _levelConfig.Rows; row++)
		{
			for (var column = 0; column < _levelConfig.Columns; column++)
			{
				var tilePosition = CalculateTilePosition(row, column, gridOffsets);
				var spawnedTile = CreateTile(tilePosition);

				AssignTileConfig(spawnedTile, availableTileConfigs);
				AnimateTileAppearance(spawnedTile, row, column);
			}
		}
	}

	private Vector2 CalculateTilePosition(int row, int column, Vector2 gridOffsets)
	{
		var position = new Vector2(column * _tileSize.x - gridOffsets.x, -(row * _tileSize.y - gridOffsets.y));

		return position;
	}

	private Tile CreateTile(Vector2 position)
	{
		var spawnedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
		spawnedTile.transform.SetParent(transform);
		spawnedTile.SetTileSize(_tileSize);
		_spawnedTiles.Add(spawnedTile);
		return spawnedTile;
	}

	private void AssignTileConfig(Tile tile, List<TileConfig> availableTileConfigs)
	{
		var randomIndex = Random.Range(0, availableTileConfigs.Count);
		var randomTileConfig = availableTileConfigs[randomIndex];
		availableTileConfigs.RemoveAt(randomIndex);
		tile.Initialize(_answerChecker, randomTileConfig);
		tile.SetSprite(
			randomTileConfig.TileSprite,
			_tilesGroupConfig.SpriteSize,
			randomTileConfig.TileRotation
		);
	}

	private void AnimateTileAppearance(Tile tile, int row, int column)
	{
		var smallScale = Vector3.zero;
		var normalScale = _tileSize;
		var punchStrength = 0.5f;
		var duration = 0.9f;
		var vibrato = 4;
		var elasticity = 1.0f;
		var delayOffset = 0.5f;

		tile.transform.localScale = smallScale;

		tile.transform
			.DOScale(normalScale, 0.0F)
			.SetDelay(0.1f + delayOffset * (row * _levelConfig.Columns + column))
			.OnStart(() => tile.transform.DOPunchScale(
				new Vector3(punchStrength, punchStrength, punchStrength),
				duration,
				vibrato,
				elasticity)
			);
	}

	private void SelectCorrectTile()
	{
		_answerChecker.SetCorrectAnswer(_spawnedTiles[Random.Range(0, _spawnedTiles.Count)].Config);
	}
}