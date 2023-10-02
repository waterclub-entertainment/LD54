using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour {

	public RawImage spiritImage;
	public Image roomIconImage;
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

	[Header("Room icons")]
    public Sprite changing_room;
    public Sprite exit;
    public Sprite sauna;
    public Sprite hot_spring_inside;
    public Sprite hot_spring_outside;
    public Sprite normal_bath;
    public Sprite shower;
    public Sprite cold_bath;
    public Sprite hotRoom;

	public void SetSpirit(Enums.Species species, bool negate) {
		roomIconImage.gameObject.SetActive(false);
		spiritImage.gameObject.SetActive(true);
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

	public void SetRoomIcon(Enums.Operation op, bool negate) {
		spiritImage.gameObject.SetActive(false);
		roomIconImage.gameObject.SetActive(true);
		cross.gameObject.SetActive(negate);
		switch (op) {
	        case Enums.Operation.CHANGING_ROOM:
				roomIconImage.sprite = changing_room;
				break;
	        case Enums.Operation.EXIT:
				roomIconImage.sprite = exit;
				break;
	        case Enums.Operation.SAUNA:
				roomIconImage.sprite = sauna;
				break;
	        case Enums.Operation.HOT_SPRING_INSIDE:
				roomIconImage.sprite = hot_spring_inside;
				break;
	        case Enums.Operation.HOT_SPRING_OUTSIDE:
				roomIconImage.sprite = hot_spring_outside;
				break;
	        case Enums.Operation.NORMAL_BATH:
				roomIconImage.sprite = normal_bath;
				break;
	        case Enums.Operation.SHOWER:
				roomIconImage.sprite = shower;
				break;
	        case Enums.Operation.COLD_BATH:
				roomIconImage.sprite = cold_bath;
				break;
		}
	}

	public void SetHotRoom(bool negate) {
		spiritImage.gameObject.SetActive(false);
		roomIconImage.gameObject.SetActive(true);
		cross.gameObject.SetActive(negate);
		roomIconImage.sprite = hotRoom;
	}

}
