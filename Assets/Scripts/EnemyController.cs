using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region PRIVATE VARIABLE
    [SerializeField] private GameObject _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _colliderDesactivationTime = 2f;

    [SerializeField] private Transform[] _wayPointList;

    private bool rayMode = false;
    private float _lastColliderDesctivationTime = 0;


    //private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Transform _currentTarget;
    private BoxCollider _boxCollider;

    public Transform Target
    {
        get { return _currentTarget; }
    }

    //private bool _onPursuit = false;
    //private bool _onAttack = false;
    //private CharacterLife _characterLife;
    #endregion

    #region BUILTIN METHOD
    void Start()
    {
        //_animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        //if (_player)
        //    _characterLife = _player.GetComponent<CharacterLife>();
    }

    
    void Update()
    {
        Locomotion();
        Attack();
        if (_boxCollider.enabled == false && _lastColliderDesctivationTime + _colliderDesactivationTime <= Time.time)
        {
            rayMode = false;
            _boxCollider.enabled = true;
        }
        //Debug.DrawRay(transform.position, Vector3.forward * 10000f, Color.blue);
        //UpdateAnimation();
        //Vector3 dir = (_player.transform.position - transform.position).normalized;
        //RaycastHit hit;
        //Debug.DrawRay(transform.position + new Vector3(0, 10, 0), transform.TransformDirection(Vector3.forward) * 10000f, Color.green);
        //Debug.DrawRay(transform.position, dir * 10000f, Color.green);
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_player && other.gameObject == _player)
        {
            //Debug.Log("Player détecté !");
            rayMode = true;
            _boxCollider.enabled = false;
            _lastColliderDesctivationTime = Time.time;
            //_gameManager.Detected();
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (_player && other.gameObject == _player)
            rayMode = false;
    }

    #endregion

    #region CUSTOM METHOD
    private void Locomotion()
    {
        // si pas de destination ou destination atteinte
        if (_currentTarget == null
            || (_currentTarget.position.x == transform.position.x && _currentTarget.position.z == transform.position.z))
        {
            // Choix de la prochaine destination aléatoirement
            ChooseRandomTarget();
        }
        if (_navMeshAgent)
        {
            // L'ennemi marche
            //_navMeshAgent.speed = _walkSpeed;
            // Assignation de la destination
            _navMeshAgent.destination = _currentTarget.position;
        }

    }

    private void Attack()
    {
        if (rayMode)
        {
            Vector3 dir = (_player.transform.position - transform.position).normalized;
            //Vector3 dir = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            Debug.DrawRay(transform.position, dir * 10000f, Color.green);
            
            if (Physics.Raycast(transform.position, dir, out hit, 10000f))
            {
                Debug.Log(hit.transform.gameObject.name);
                //Debug.Log(hit.distance);
                if (hit.transform.gameObject == _player)
                {
                    _gameManager.Detected();
                }
            }
        }
        // Calcul la distance du player
        //float playerDistance = Vector3.Distance(_player.transform.position, transform.position);
        // Test si le player est à portée
        //_onAttack = playerDistance <= _minPlayerDistanceToAttack ? true : false;
    }

    //private void UpdateAnimation()
    //{
    //    if (_animator)
    //    {
    //        _animator.SetBool("Pursuit", _onPursuit);
    //        _animator.SetBool("Attack", _onAttack);
    //    }
    //}

    private void ChooseRandomTarget()
    {
        if (_wayPointList!= null && _wayPointList.Length > 1)
        {
            Transform futurTarget = _wayPointList[Random.Range(0, _wayPointList.Length)];
            if (_currentTarget != null)
            {
                // Recherche aléatoirement une prochine destination de ronde qui n'est pas celle où l'on est déja
                while (futurTarget == _currentTarget)
                {
                    futurTarget = _wayPointList[Random.Range(0, _wayPointList.Length)];
                }
            }
            _currentTarget = futurTarget;
        }
    }

    //private void MakeDamage()
    //{
    //    if (_characterLife)
    //        _characterLife.TakeDamage();
    //}
    #endregion
}
