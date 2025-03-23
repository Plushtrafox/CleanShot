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

    public float disparoFuerza = 50f;
    public float disparoAltura = 7f;

    public EnemigoBalaSpawnManager balasManager;

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
            GameObject balaDisparar=balasManager.DispararBala();//notifica del disparo a manager de balas y recibe el gameobject de la bala por disparar
           
            EnemyBullet balaScript = balaDisparar.GetComponent<EnemyBullet>();//buscar el script de la bala para notificar del disparo
            


            //attack code
            balaDisparar.transform.position = CanonBalaZona.position;//coloca bala en lugar de disparo

            balaDisparar.SetActive(true);//activa la bala

            balaDisparar.transform.rotation = quaternion.identity; //coloca rotacion correcta

            Rigidbody rb = balaDisparar.GetComponent<Rigidbody>(); //busca el rigidbody de la bala para agregarle las fuerza de disparo

            rb.AddForce(transform.forward * disparoFuerza, ForceMode.Impulse);//agrega fuerza hacia adelante

            rb.AddForce(transform.up * disparoAltura, ForceMode.Impulse); //agrega fuerza hacia arriba

            balaScript.Disparar(); //notifica a la bala que ha sido disparada

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
