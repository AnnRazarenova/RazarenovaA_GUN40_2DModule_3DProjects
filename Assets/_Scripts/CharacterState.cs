public enum CharacterStateID
{
    Idle,
    Search,
    Collect
}

public interface CharacterState
{
    CharacterStateID GetID();

    void Enter(CharacterAI characterAI);

    void Exit(CharacterAI characterAI);

    void Update(CharacterAI characterAI);
}
