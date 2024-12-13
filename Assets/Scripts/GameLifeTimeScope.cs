using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
	[SerializeField] private LevelsCatalogConfig _levelsCatalogConfig;
	[SerializeField] private TilesGroupsCatalogConfig _tilesGroupsCatalogConfig;
	[SerializeField] private AnswerDisplayService _answerDisplayService;

	protected override void Configure(IContainerBuilder builder)
	{
		builder.RegisterInstance(_levelsCatalogConfig.LevelConfigs[0]);
		builder.RegisterInstance(_tilesGroupsCatalogConfig);
		builder.Register<TilesGroupService>(Lifetime.Singleton).As<ITilesGroupService>();
		builder.RegisterInstance(_answerDisplayService);
		builder.Register<AnswerChecker>(Lifetime.Singleton).As<IAnswerChecker>();
	}
}