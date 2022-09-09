using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private Text ScoreTxt;
    public int Score { get { return score; } }

    private int score;

    void Start()
    {
        Instance = this;
        score = 0;
    }

    public void Hit()
    {
        score += 1;
    }

    private void Update()
    {
        ScoreTxt.text = score.ToString();
    }
}