using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEnabler : MonoBehaviour
{

    public Enums.Species species;
    public int count;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnScoreChanged(ScoreKeeper keeper)
    {
        gameObject.SetActive(keeper.GetScore(species) >= count);
    }
}
