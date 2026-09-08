using UnityEngine;

public class StateMachine
{
    public CharacterState[] states;
    public CharacterAI characterAI;
    public CharacterStateID currentState;

    public StateMachine(CharacterAI characterAI)
    {
        this.characterAI = characterAI;
        int numState = System.Enum.GetNames(typeof(CharacterStateID)).Length;
        states = new CharacterState[numState];
    }

    public void RegisterState(CharacterState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    public CharacterState GetState(CharacterStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(characterAI);
    }

    public void ChangeState(CharacterStateID newState)
    {
        GetState(currentState)?.Exit(characterAI);
        currentState = newState;
        GetState(currentState)?.Enter(characterAI);
    }
}
