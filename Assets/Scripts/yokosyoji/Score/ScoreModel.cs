using UnityEngine;

public class ScoreModel
{
    public float _score {  get; private set; }
    public void AddSore(float value)
    {
        _score += value;
    }
    public void Reset()
    {
        _score = 0;
    }
}
