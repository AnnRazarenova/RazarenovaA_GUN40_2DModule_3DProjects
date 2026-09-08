using UnityEngine;
public class SearchState : CharacterState
{
    private float _timer;
    private Vector3 _randomDestination;

    public CharacterStateID GetID()
    {
        return CharacterStateID.Search;
    }

    public void Enter(CharacterAI characterAI)
    {
        _timer = 0;

        characterAI.NavMeshAgent.isStopped = false;

        RandomPathPoint(characterAI);
        characterAI.NavMeshAgent.SetDestination(_randomDestination);
    }

    public void Exit(CharacterAI characterAI)
    {
        characterAI.NavMeshAgent.ResetPath();
    }

    public void Update(CharacterAI characterAI)
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _timer = characterAI.Config.maxSearchTime;

            foreach (Collectible item in characterAI.Treasures)
            {
                float distance = Vector3.Distance(characterAI.CharacterTransform.position, item.transform.position);

                if (distance < characterAI.Config.findDistant)
                {
                    characterAI.Target = item;

                    characterAI.NavMeshAgent.isStopped = true;

                    characterAI.Animator.SetInteger("state", 2);
                    characterAI.stateMachine.ChangeState(CharacterStateID.Collect);

                    return;
                }
            }

            
        }

        if (!characterAI.NavMeshAgent.hasPath)
        {
            RandomPathPoint(characterAI);
            characterAI.NavMeshAgent.SetDestination(_randomDestination);
        }
    }

    private void RandomPathPoint(CharacterAI characterAI)
    {
        float randomX = Random.Range(characterAI.Config.minRandPoint, characterAI.Config.maxRandPoint);
        float randomZ = Random.Range(characterAI.Config.minRandPoint, characterAI.Config.maxRandPoint);

        _randomDestination = characterAI.CharacterTransform.position + new Vector3(randomX, 0, randomZ);
    }
}
