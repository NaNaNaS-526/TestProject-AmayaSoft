using Core.Declaration;
using JetBrains.Annotations;
using Level.Declaration;
using Cards.Data;
using UI;

namespace Core.Implementation
{
	[UsedImplicitly]
	public class AnswerChecker : IAnswerChecker
	{
	
		private CardConfig _correctCardConfig;
		private readonly AnswerDisplayService _answerDisplayService;
		private readonly ILevelService _levelService;

		public AnswerChecker(AnswerDisplayService answerDisplayService, ILevelService levelService)
		{
			_answerDisplayService = answerDisplayService;
			_levelService = levelService;
		}

		public void SetCorrectAnswer(CardConfig correctConfig)
		{
			_correctCardConfig = correctConfig;
			_answerDisplayService.DisplayCorrectAnswerText(_correctCardConfig.CardSymbol);
		}

		public bool CheckAnswer(CardConfig cardConfig)
		{
			var isCorrect = cardConfig == _correctCardConfig;
			if (!isCorrect)
			{
				return false;
			}

			_levelService.GoToNextLevel();
			return true;
		}
	}
}