using System;
using JetBrains.Annotations;
using Level.Data;
using Level.Declaration;
using UI;

namespace Level.Implementation
{
	[UsedImplicitly]
	public class LevelService : ILevelService
	{
		public event Action OnLevelUpdated;
		private readonly LevelsCatalogConfig _levelsCatalogConfig;
		private readonly EndGameScreen _endGameScreen;
		private int _currentLevelIndex;

		public LevelService(LevelsCatalogConfig levelsCatalogConfig, EndGameScreen endGameScreen)
		{
			_levelsCatalogConfig = levelsCatalogConfig;
			_endGameScreen = endGameScreen;
			_currentLevelIndex = 0;
		}

		public LevelConfig GetCurrentLevel()
		{
			return _levelsCatalogConfig.LevelConfigs[_currentLevelIndex];
		}

		public void GoToNextLevel()
		{
			if (_currentLevelIndex < _levelsCatalogConfig.LevelConfigs.Count - 1)
			{
				_currentLevelIndex++;
				OnLevelUpdated?.Invoke();
			}
			else
			{
				ShowEndGameScreen();
			}
		}

		private void ShowEndGameScreen()
		{
			_endGameScreen.Show();
		}
	}
}