using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEnabler : MonoBehaviour
{

    public Enums.Species species;
    public int count;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag != "Hint")
                transform.GetChild(i).gameObject.SetActive(false);
            else
                transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OnScoreChanged(ScoreKeeper keeper)
    {
        bool cond = false;
        if (species == Enums.Species.COUNT)
            cond = keeper.GetTotalScore() >= count;
        else
            cond = keeper.GetScore(species) >= count;

        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag != "Hint")
                transform.GetChild(i).gameObject.SetActive(cond);
            else
                transform.GetChild(i).gameObject.SetActive(!cond);
        }
    }
}
