using UnityEngine;

public class IdleState : CharacterState
{
    private float _timer;

    public CharacterStateID GetID()
    {
        return CharacterStateID.Idle;
    }
    
    public void Enter(CharacterAI characterAI)
    {
        _timer = 0;

        characterAI.NavMeshAgent.isStopped = true;
    }

    public void Exit(CharacterAI characterAI)
    {
        characterAI.Animator.SetInteger("state", 1);
    }


    public void Update(CharacterAI characterAI)
    {
        _timer += Time.deltaTime;

        if (_timer > characterAI.Config.idleTime)
        {
            
            characterAI.stateMachine.ChangeState(CharacterStateID.Search);
        }
    }
}
