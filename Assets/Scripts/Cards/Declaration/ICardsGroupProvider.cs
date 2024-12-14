using Cards.Data;

namespace Cards.Declaration
{
	public interface ICardsGroupProvider
	{
		public CardsGroupConfig GetCardGroupForCurrentLevel();
	}
}