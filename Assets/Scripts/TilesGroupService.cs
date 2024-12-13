using System.Linq;
using JetBrains.Annotations;

[UsedImplicitly]
public class TilesGroupService : ITilesGroupService
{
	private readonly TilesGroupsCatalogConfig _tilesGroupsCatalogConfig;

	public TilesGroupService(TilesGroupsCatalogConfig tilesGroupsCatalogConfig)
	{
		_tilesGroupsCatalogConfig = tilesGroupsCatalogConfig;
	}

	public TilesGroupConfig GetTileGroupByLevelConfig(LevelConfig levelConfig)
	{
		return _tilesGroupsCatalogConfig.TileGroupsConfigs.FirstOrDefault(group => group.TilesType == levelConfig.LevelType);
	}
}