using UnityEngine;
using UnityEngine.Events;

public class MoveRoboVac : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _turnSpeed;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _sideRayDistance;
    [SerializeField]
    private float _forwardRayDistance;

    private Turn _turn = Turn.None;
    private TurnPhase _turnPhase = TurnPhase.None;

    private Vector3 _lastPosition;
    private float _angleFixRotated = 0;
    private float _startupTimer = 0f;

    private bool _isStarted = false;
    private bool _isTurning = false;
    private bool _isStuck = false;

    private bool _hitLeft = false;
    private bool _hitRight = false;
    private bool _hitForward = false;

    private Rigidbody _body;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _lastPosition = transform.position;

        FreezeRotation();
    }

    private void FreezeRotation()
    {
        if(_body != null)
        {
            _body.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
    }

    private void FixedUpdate()
    {
        _startupTimer += Time.fixedDeltaTime;

        CaptureHits();

        DrawRay();
        
        _body.velocity = transform.forward * _moveSpeed;

        HitLogic();
    }

    private void CaptureHits()
    {
        _hitLeft = Physics.Raycast(transform.position, -transform.right, _sideRayDistance, _layerMask);
        _hitRight = Physics.Raycast(transform.position, transform.right, _sideRayDistance, _layerMask);
        _hitForward = Physics.Raycast(transform.position, transform.forward, _forwardRayDistance, _layerMask);
    }

    private void DrawRay()
    {
        Debug.DrawRay(transform.position, -transform.right * _sideRayDistance, Color.red);
        Debug.DrawRay(transform.position, transform.right * _sideRayDistance, Color.red);
        Debug.DrawRay(transform.position, transform.forward * _forwardRayDistance, Color.red);
    }

    private void HitLogic()
    {
        if (!_isTurning)
        {
            if (_startupTimer >= 1)
            {
                CheckRoboStuck();
            }

            if(_isStuck)
            {
                return;
            }

            

            if (_hitForward)
            {
                _turnPhase = TurnPhase.TurningToWall;
                _isTurning = true;
                RotateLeftOrRight();
            }
            else
            if (_hitLeft || _hitRight)
            {
                _turnPhase = TurnPhase.TurningAway;
                _isTurning = true;
                RotateLeftOrRight();
            }
        }
        else
        {
            RotateRobo();
        }
    }

    private void RotateRobo()
    {
        if (_turn == Turn.None || _turnPhase == TurnPhase.None)
            return;

         float diraction = (_turn == Turn.Right) ? 1 : -1;

        if (_turnPhase == TurnPhase.TurningToWall)
        {
            bool hitSide = _hitRight || _hitLeft;

            if (!hitSide)
            {
                _body.MoveRotation(_body.rotation * Quaternion.Euler(0, diraction * _turnSpeed * Time.deltaTime, 0));
            }
            else
                _turnPhase = TurnPhase.TurningAway;
        }
        else
        {
            bool hitSide = _hitRight || _hitLeft;

            if (hitSide)
            {
                _body.MoveRotation(_body.rotation * Quaternion.Euler(0, diraction * _turnSpeed * Time.deltaTime, 0));
            }
            else
            {
                _isTurning = false;
                _turn = Turn.None;
                _turnPhase = TurnPhase.None;
            }
        }
    }

    private void CheckRoboStuck()
    {
        if (!_isStuck)
        {
            float distance = Vector3.Distance(transform.position, _lastPosition);
            _lastPosition = transform.position;

            if (distance < 0.001)
            {
                RoboStuckFix();
                _isStuck = true;
            }
        }
    }

    private void RoboStuckFix()
    {
        _body.velocity = Vector3.zero;

        RotateLeftOrRight();
        float diraction = (_turn == Turn.Right) ? 1 : -1;

        float angleNeed = 45;

        if (angleNeed > _angleFixRotated)
        {
            float step = _turnSpeed * 1f * Time.fixedDeltaTime;

            if (step > angleNeed - _angleFixRotated)
                step = angleNeed - _angleFixRotated;

            transform.Rotate(new Vector3(0, step * diraction, 0));

            _angleFixRotated += step;
        }
        else
        {
            _turn = Turn.None;
            _isStuck = false;
            _angleFixRotated = 0;
            _startupTimer = 0;
        }
    }

    private void RotateLeftOrRight()
    {
        float random = Random.value;

        if (_hitLeft && !_hitRight)
        {
            _turn = Turn.Right;
        }
        else
            if (!_hitLeft && _hitRight)
        {
            _turn = Turn.Left;
        }
        else if (_hitLeft && _hitRight)
        {
            _turn = Random.value <= 0.5f ? Turn.Left : Turn.Right;
        }
        else
        {
            _turn = Random.value <= 0.5f ? Turn.Left : Turn.Right;
        }
        

        
    }
}
