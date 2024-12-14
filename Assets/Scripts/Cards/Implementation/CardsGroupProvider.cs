using System.Linq;
using Cards.Data;
using Cards.Declaration;
using JetBrains.Annotations;
using Level.Declaration;

namespace Cards.Implementation
{
	[UsedImplicitly]
	public class CardsGroupProvider : ICardsGroupProvider
	{
		private readonly CardsGroupsCatalogConfig _cardsGroupsCatalogConfig;
		private readonly ILevelService _levelService;

		public CardsGroupProvider(CardsGroupsCatalogConfig cardsGroupsCatalogConfig, ILevelService levelService)
		{
			_cardsGroupsCatalogConfig = cardsGroupsCatalogConfig;
			_levelService = levelService;
		}

		public CardsGroupConfig GetCardGroupForCurrentLevel()
		{
			var cardsGroupConfig = _cardsGroupsCatalogConfig.CardGroupsConfigs.FirstOrDefault(group => group.CardsType == _levelService.GetCurrentLevel().LevelType);
			return cardsGroupConfig;
		}
	}
}