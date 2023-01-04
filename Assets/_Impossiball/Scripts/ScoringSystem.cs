using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ScoreHandler(int score);
public class ScoringSystem : MonoBehaviour
{
    public event ScoreHandler OnScoreUpdated;
    public int CurrentScore { get; private set; }
    
    [Header("References")]
    [SerializeField] BallController _ballController;
    private void Awake()
    {
        _ballController._OnBallCollided += HandleBallCollision;
    }

    void HandleBallCollision (Collision collision)
    {
        if (collision.collider.tag == "Wall")
            CurrentScore++;
        else
            CurrentScore = 0;
        if (CurrentScore < 0)
            CurrentScore = 0;
        OnScoreUpdated?.Invoke(CurrentScore);
    }
}
