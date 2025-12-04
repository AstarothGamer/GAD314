using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Portal portal;
    //enemy prefab i want to spawn
    public GameObject enemyPrefab;

    //spawn 15 enemies
    public int maxEnemies = 15;

    //how many enemies already spawned
    private int enemyCount = 0;

    //the timer for spawning
    private float spawnTimer = 0f;

    // random time between 1 and 2 seconds
    private float nextSpawnTime = 0f;

    // this will tell me if the player is inside the trigger
    private bool playerIsHere = false;

    public bool allSpawnedDead = false;
    private int enemyDead;

    void Start()
    {
        // first random time
        nextSpawnTime = Random.Range(1f, 2f);
    }

    void Update()
    {
        // if player is not inside the trigger, do nothing
        if (!playerIsHere)
        {
            return;
        }

        // if i already spawned 15, stop
        if (enemyCount >= maxEnemies)
        {
            return;
        }

        // timer counting
        spawnTimer += Time.deltaTime;

        // if timer reaches the random time
        if (spawnTimer >= nextSpawnTime)
        {
            SpawnEnemy();

            // reset timer
            spawnTimer = 0f;

            // get new random time
            nextSpawnTime = Random.Range(1f, 2f);
        }
    }

    void SpawnEnemy()
    {
        // spawn at the same spot as this spawner
        GameObject enemyObj = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        MeleeEnemy enemy = enemyObj.GetComponent<MeleeEnemy>();
        if(enemy != null)
        {
            enemy.spawner = this;
        }

        // add 1 enemy to the count
        enemyCount++;
    }

    public void OnSpawnedEnemyDie()
    {
        enemyDead++;

        if(enemyDead >= enemyCount && enemyCount >= maxEnemies)
        {
            allSpawnedDead = true;
            portal.ActivatePortal();
        }
    }

    // when something enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // check if its the object named "Player 1"
        if (other.gameObject.name == "Player 1")
        {
            playerIsHere = true; // now it can start spawning
        }
    }

    // when something leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player 1")
        {
            playerIsHere = false; // stop spawning when player leaves
        }
    }
}
