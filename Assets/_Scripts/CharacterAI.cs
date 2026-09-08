using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    public StateMachine stateMachine;
    public CharacterStateID initialState;

    private Transform _characterTransform;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    [SerializeField] private List<Collectible> _treasures = new List<Collectible>(); 

    [SerializeField] private CharacterAIConfig _config;

    private int _collectedTreasureCount = 0;
    public int CollectedTreasureCount => _collectedTreasureCount;

    public List<Collectible> Treasures => _treasures;
    public Collectible Target {  get; set; }

    public Transform CharacterTransform => _characterTransform;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Animator Animator => _animator;    

    public CharacterAIConfig Config => _config;


    void Start()
    {
        _characterTransform = transform;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _treasures.AddRange(FindObjectsOfType<Collectible>());

        stateMachine = new StateMachine(this);
        
        stateMachine.RegisterState(new SearchState());
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new CollectState());

        stateMachine.ChangeState(initialState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void AddScore()
    {
        _collectedTreasureCount++;
        Debug.Log(_collectedTreasureCount.ToString());
    }

    public void RemoveTreasure(Collectible treasure)
    {
        if(_treasures.Contains(treasure))
        {
            _treasures.Remove(treasure);
        }
    }
}
