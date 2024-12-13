using JetBrains.Annotations;
using UnityEngine;

[UsedImplicitly]
public class AnswerChecker : IAnswerChecker
{
	private TileConfig _correctTileConfig;
	
	private readonly AnswerDisplayService _answerDisplayService;

	private AnswerChecker(AnswerDisplayService answerDisplayService)
	{
		_answerDisplayService = answerDisplayService;
	}

	public void SetCorrectAnswer(TileConfig correctConfig)
	{
		_correctTileConfig = correctConfig;
		_answerDisplayService.DisplayCorrectAnswerText(_correctTileConfig.TileSymbol);
	}

	public bool CheckAnswer(TileConfig tileConfig)
	{
		Debug.LogError(tileConfig == _correctTileConfig ? "Correct!" : "Incorrect!");
		return tileConfig == _correctTileConfig;
	}
}