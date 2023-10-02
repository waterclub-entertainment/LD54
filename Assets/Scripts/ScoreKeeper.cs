using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreKeeper : MonoBehaviour
{
    public UnityEvent<ScoreKeeper> OnScoreChanged = new UnityEvent<ScoreKeeper>();

    int[] score = new int[(int)Enums.Species.COUNT];

    public static ScoreKeeper mainKeeper;

    public void AddToScore(Enums.Species sp)
    {
        score[(int)sp] += 1;
        OnScoreChanged.Invoke(this);
    }

    public int GetTotalScore()
    {
        int sum = 0;
        foreach (int i in score)
        {
            sum += i;
        }
        return sum;
    }
    public int GetScore(Enums.Species sp) { return score[(int) sp]; }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < score.Length; i++) { score[i] = 0; }
        mainKeeper = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
