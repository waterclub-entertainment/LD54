using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour {

	public RawImage spiritImage;
	public GameObject cross;

	[Header("Spirit tokens")]
	public Texture stag;
	public Texture cat;
	public Texture rat;
	public Texture raven;
	public Texture sparrow;
	public Texture lizard;
	public Texture frog;
	public Texture crane;
	public Texture capibara;
	public Texture mantis;

	public void SetSpirit(Enums.Species species, bool negate) {
		cross.SetActive(negate);
		switch (species) {
			case Enums.Species.STAG:
				spiritImage.texture = stag;
				break;
			case Enums.Species.CAT:
				spiritImage.texture = cat;
				break;
			case Enums.Species.RAT:
				spiritImage.texture = rat;
				break;
			case Enums.Species.RAVEN:
				spiritImage.texture = raven;
				break;
			case Enums.Species.SPARROW:
				spiritImage.texture = sparrow;
				break;
			case Enums.Species.LIZARD:
				spiritImage.texture = lizard;
				break;
			case Enums.Species.FROG:
				spiritImage.texture = frog;
				break;
			case Enums.Species.CRANE:
				spiritImage.texture = crane;
				break;
			case Enums.Species.CAPIBARA:
				spiritImage.texture = capibara;
				break;
			case Enums.Species.MANTIS:
				spiritImage.texture = mantis;
				break;
		}
	}

}
