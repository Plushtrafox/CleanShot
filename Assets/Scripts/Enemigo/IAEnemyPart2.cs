using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemyPart2 : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //public float health;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet = true;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public Transform CanonBalaZona;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject projectile;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("==Player==").transform;
    }
    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) Attackplayer();
    }
    private void Patroling()
    {
        print("Patrullando");
        if (walkPointSet) SearchWalkPoint();
        if(walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        
        }
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange,walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,transform.position.z + randomZ);
        
        if (Physics.Raycast(walkPoint, -transform.up,2f, whatIsGround))
        walkPointSet = true;
        }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void Attackplayer()
    {
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {

            //attack code
            Rigidbody rb = Instantiate(projectile, CanonBalaZona.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 42f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);        
    }
}
