using Cards.Data;

namespace Core.Declaration
{
	public interface IAnswerChecker
	{
		public void SetCorrectAnswer(CardConfig correctConfig);
		bool CheckAnswer(CardConfig cardConfig);
	}
}