using UnityEngine;

[CreateAssetMenu]
public class CharacterAIConfig : ScriptableObject
{
    public float maxSearchTime;
    public float idleTime;
    public float findDistant;

    public float minRandPoint;
    public float maxRandPoint;
}
