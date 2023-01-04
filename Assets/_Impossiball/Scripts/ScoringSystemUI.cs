using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ScoringSystem _scoringSystem;
    [SerializeField] Text _text;

    private void Awake()
    {
        _scoringSystem.OnScoreUpdated += HandleScoreUpdate;
    }

    void HandleScoreUpdate (int score)
    {
        _text.text = score.ToString();
    }
}
