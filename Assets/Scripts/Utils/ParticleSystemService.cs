using JetBrains.Annotations;
using UnityEngine;

namespace Utils
{
	[UsedImplicitly]
	public class ParticleSystemService
	{
		public void ActivateParticleSystem(ParticleSystem particles, Vector3 position)
		{
			particles.transform.SetParent(null);
			particles.transform.localScale = new Vector3(1.0F, 1.0F, 0.0F);
			particles.transform.position = new Vector3(position.x, position.y, -1.0F);

			particles.Play();
		}
	}
}