using System;
using Level.Data;

namespace Level.Declaration
{
	public interface ILevelService
	{
		public event Action OnLevelUpdated;
		public LevelConfig GetCurrentLevel();
		public void GoToNextLevel();
	}
}