using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text CharacterName;
    [SerializeField]
    private TMP_Text CharacterText;
    [SerializeField]
    private TMP_Text CharacterResponse;
    [SerializeField]
    private Canvas TutorialCanvas;
    private string[,] Dialogue = new string[8, 3]
    {
        {"Markus", "Hello, departed soul! Welcome to the Spirit Spa.", "Uhm... hi? Why am I here?"},
        {"Markus", "You're here to assist, to guide the forest's spirits, on their path to peace at last. An exciting endeavor, wouldn't you agree?", "W-What?"},
        {"Markus", "This is your workplace, the spa, a haven of tranquil pleasure. Hot springs, saunas, ice baths, and massages aplenty. The perfect setting for them to prepare for their final journey. But as you can see, times are growing harsh, hence we only have limited space and skilled hands are dwindling. Therefore, your role is to coordinate and accompany the guests.","Guests? didn'd you say Spirits just now?"},
        {"Markus", "Ah, here comes our very first guest! Their name is Spiritname. Let's discover their desires.","YAY! I'm excited!"},
        {"Markus", "The guest wishes to indulge in the hot springs. It's as simple as this: Just place their token at a spot in the hot springs, and they will know precisely where to go.", "Won't the bones soak all the Water up?"},
        {"Markus", "Excellent! Do you see how they move? Now, we shall patiently await their recovery.", "It'S not as they are not already dead."},
        {"Markus", "Now, our guest is ready; can you see the smile upon their bony visage? They are now prepared to step into the realm of the departed. Take a close look!","mhm, i can certanly see the smile..."},
        {"Markus", "The energy you witness, we call it blissfulness. It's vital to maintain an ambiance of serenity and peace. The more blissfulness you gather, the happier your guests will be, and the spa's atmosphere will thrive. Remember, even a soon-to-be-departed guest is treated as royalty!","And I'm their slave?"}
    };
    private int DialogueLine = 0;



    // Start is called before the first frame update
    void Start()
    {
        UpdateText(DialogueLine);
    }

    public void OnButtonClick()
    {
        DialogueLine += 1;
        UpdateText(DialogueLine);
    }

    private void UpdateText(int Zeile)
    {
        Debug.Log(DialogueLine + " " + Dialogue.Length);
        if (Zeile < Dialogue.GetLength(0))
        {
            CharacterName.text = Dialogue[Zeile, 0];
            CharacterText.text = Dialogue[Zeile, 1];
            CharacterResponse.text = Dialogue[Zeile, 2];
        }
        else
            TutorialCanvas.enabled = false;

    }
}
