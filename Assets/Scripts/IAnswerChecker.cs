public interface IAnswerChecker
{
	public void SetCorrectAnswer(TileConfig correctConfig);
	bool CheckAnswer(TileConfig tileConfig);
}