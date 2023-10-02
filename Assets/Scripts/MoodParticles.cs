using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class MoodParticles : MonoBehaviour {
	private ParticleSystemRenderer particleSystemRenderer;

	public Material happyMaterial;
	public Material neutralMaterial;
	public Material unhappyMaterial;
	public Material pissedMaterial;

	void Start() {
		particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
	}

	public void SetMood(Enums.Mood mood) {
		switch (mood) {
			case Enums.Mood.ASCENDED:
			case Enums.Mood.HAPPY:
				particleSystemRenderer.material = happyMaterial;
				break;
			case Enums.Mood.NEUTRAL:
				particleSystemRenderer.material = neutralMaterial;
				break;
			case Enums.Mood.UNHAPPY:
				particleSystemRenderer.material = unhappyMaterial;
				break;
			case Enums.Mood.PISSED:
				particleSystemRenderer.material = pissedMaterial;
				break;
		}
	}
}
