using Core.Declaration;
using Level.Data;
using Level.Declaration;
using Level.Implementation;
using Cards.Data;
using Cards.Declaration;
using Cards.Implementation;
using UI;
using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;

namespace Core.Implementation
{
	public class GameLifeTimeScope : LifetimeScope
	{
		[SerializeField] private LevelsCatalogConfig _levelsCatalogConfig;
		[SerializeField] private CardsGroupsCatalogConfig _cardsGroupsCatalogConfig;
		[SerializeField] private AnswerDisplayService _answerDisplayService;
		[SerializeField] private EndGameScreen _endGameScreen;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<ParticleSystemService>(Lifetime.Singleton);
			builder.RegisterInstance(_endGameScreen);
			builder.RegisterInstance(_levelsCatalogConfig);
			builder.Register<LevelService>(Lifetime.Singleton).As<ILevelService>();
			builder.RegisterInstance(_cardsGroupsCatalogConfig);
			builder.Register<CardsGroupProvider>(Lifetime.Singleton).As<ICardsGroupProvider>();
			builder.RegisterInstance(_answerDisplayService);
			builder.Register<AnswerChecker>(Lifetime.Singleton).As<IAnswerChecker>();
		}
	}
}