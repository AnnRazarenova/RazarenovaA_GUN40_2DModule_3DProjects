using UnityEngine;

public class CollectState : CharacterState
{
    private float _timer;

    private bool _isReached;

    public CharacterStateID GetID()
    {
        return CharacterStateID.Collect;
    }

    public void Enter(CharacterAI characterAI)
    {
        _timer = 0;

        if (characterAI.Target != null)
        {
            characterAI.stateMachine.ChangeState(CharacterStateID.Search);
        }
        
        characterAI.NavMeshAgent.stoppingDistance = 0.7f;

        characterAI.NavMeshAgent.isStopped = false;

        characterAI.NavMeshAgent.SetDestination(characterAI.Target.transform.position);
    }

    public void Exit(CharacterAI characterAI)
    {
        characterAI.Target = null;
    }

    public void Update(CharacterAI characterAI)
    {

    }
}
