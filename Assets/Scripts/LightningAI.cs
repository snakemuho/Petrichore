using UnityEngine;

public class LightningAI : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    private bool _prawlReached = true;
    private Vector3 _startPosition;
    private float _idleTimer;
    [SerializeField] private float _minIdleTime = 1, _maxIdleTime = 3;
    [SerializeField] private float _speed;
  
    private Vector3 _destination;
    private Ray _rayToPlayer;
    private RaycastHit _rayToPlayerHit;

    private enum State
    {
        Idle,
        Walking,
        Hunting,
        Prawling
    }
    State monsterState = State.Idle;

    private void Start()
    {
        _startPosition = transform.TransformPoint(transform.position);
        _idleTimer = 2f;
    }

    private void FixedUpdate()
    {

        switch (monsterState)
        {
            case State.Idle:
                if (_idleTimer > 0)
                {
                    _idleTimer -= Time.fixedDeltaTime;
                }
                else GoToRandomArea();
                break;
            case State.Walking:
                Walking();
                break;
            case State.Hunting:
                HuntingPlayer();
                break;
            case State.Prawling:
                Prawl();
                break;
        }
    }


    private void GoToRandomArea()
    {
        float rand = Random.Range(-10f, 10f);
        _destination = new Vector3(transform.position.x + rand, transform.position.y, transform.position.z + rand);
        monsterState = State.Walking;
    }
    private void SetIdleTimer()
    {
        _idleTimer = Random.Range(_minIdleTime, _maxIdleTime);
    }
    private void Walking()
    {
        transform.position = Vector3.Lerp(transform.position, _destination, Time.deltaTime * _speed / 2);
        if (Vector3.Distance(_destination, transform.position) < 1)
        {
            SetIdleTimer();
            monsterState = State.Idle;
        }
    }

    private void HuntingPlayer()
    {
        _destination = _playerTransform.position + Vector3.up * 0.5f;
        transform.position = Vector3.Lerp(transform.position, _destination, Time.deltaTime * _speed);
    }
    private void Prawl()
    {
        if (_prawlReached)
        {
            float rand = Random.Range(-10, 10);
            _destination = new Vector3(transform.position.x + rand, transform.position.y, transform.position.z + rand);
        }
        transform.position = Vector3.Lerp(transform.position, _destination, Time.deltaTime * _speed * 0.75f);
        if (Vector3.Distance(_destination,transform.position) < 1)
            _prawlReached = true;
        else _prawlReached = false;
    }
    public void TriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _rayToPlayer = new Ray(transform.position, ((_playerTransform.position + Vector3.up) - transform.position));
            Debug.DrawRay(transform.position, ((_playerTransform.position + Vector3.up) - transform.position), Color.red);
            if (Physics.Raycast(_rayToPlayer, out _rayToPlayerHit))
            {
                if (_rayToPlayerHit.collider.gameObject.tag == "Cover")
                {
                    monsterState = State.Prawling;
                }
                else
                {
                    if (!_prawlReached)
                        _prawlReached = true;
                    monsterState = State.Hunting;
                }
            }
        }
    }

    public void TriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            monsterState = State.Idle;
        }
    }
}
