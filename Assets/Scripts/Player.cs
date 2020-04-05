using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float playerMaxHealth;
    public float playerHealth;
    public float hitSpeed;
    public float regenerativeAibility;
    public float kills;
    public float playerFear;
    public float gameOver = 0f;
    public float playerRank;
    public OverallUItesting overallUIScript;

    public string playerName;

    public float patrolTime, waitTime;

    public Vector3 randomPosition;

    public Transform enemy;

    public List<GameObject> allEnemies; //
    public enum State { idle, roaming, attacking, fear, redzone, running};

    public State state;

    private void Start()
    {
        //playerHealth = 90f;
        //playerMaxHealth = 100f;
        //hitSpeed = 0.1f;

        GameObject[] allenemies = GameObject.FindGameObjectsWithTag("Player"); //
        for (int i = 0; i < allenemies.Length; i++) //
        { //
            allEnemies.Add(allenemies[i]); //
        } //

        StartCoroutine(moving(randomPosition));

        state = State.idle;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameOver != 1f)
        {
            if (playerHealth < 0)
            {
                overallUIScript.removeElementFromList(this.gameObject);
                killUpdate(this.gameObject); // Kill Updater
                allEnemies.Remove(this.gameObject); //
            }
            if (playerHealth < 0) transform.gameObject.SetActive(false);

            //SafeZone();
            SphereCasting();
            Movement();
            //DebuggingPattern1();
            DebuggingPattern2();
            //DebuggingPattern3();
            //DebuggingPattern4();
            transform.LookAt(randomPosition);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }

    void SphereCasting()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

        if (colliders.Length > 2)
        {
            foreach (Collider col in colliders)
            {
                float minimumHealth = 101f;
                if (col.transform.tag == "Player" && col.transform.root != transform && col.gameObject.activeSelf == true)
                {
                    if (col.transform.GetComponent<Player>().playerHealth <= minimumHealth)
                    {
                        minimumHealth = col.transform.GetComponent<Player>().playerHealth;
                    }
                    if (col.transform.GetComponent<Player>().playerHealth == minimumHealth)
                    {
                        enemy = col.transform; // Enemy is assigned according to lowest health
                    }

                    state = State.attacking;
                }
            }
        }
        else
        {
            enemy = null;
            transform.GetComponent<Renderer>().material.color = Color.white;
            //state = State.idle;
            StopCoroutine(takingDamage(0));
        }
    }

    public void Movement()
    {
        if(state == State.idle)
        {
            transform.position =new Vector3(transform.position.x, 0, transform.position.z);
            transform.GetComponent<Renderer>().material.color = Color.white;
            transform.position = Vector3.MoveTowards(transform.position, randomPosition, 0.1f);
        }
        if(state == State.attacking)
        {
            if (enemy != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemy.position, 0.1f);
                //transform.LookAt(enemy);
            }
            else
            {
                state = State.idle;
            }
            //print(enemy);
        }
        if(state == State.running)
        {
            print(playerName + " is scared of " + enemy.name + " and is running away!");
        }
        if(state == State.redzone)
        {
            //print("Ohh shit redzone");
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            StartCoroutine(takingDamage(hitSpeed));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            print("I escaped");
            //StopCoroutine(moving(randomPosition));
        }
    }

    void SafeZone()
    {
        if (Mathf.Abs(transform.position.x) > GameObject.Find("RedZone").transform.localScale.x || Mathf.Abs(transform.position.z) > GameObject.Find("RedZone").transform.localScale.z)
        {
            state = State.redzone;
        }
        else
        {
            StartCoroutine(safeZoneRun());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //state = State.redzone;
        StartCoroutine(outOfZoneDamage());
        StopCoroutine(moving(Vector3.zero));
        randomPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        StopCoroutine(outOfZoneDamage());
        StartCoroutine(safeZoneRun());
    }

    void killUpdate(GameObject killer)
    {
        if(enemy == null)
        {
            fixing();
            if(1 == fixing())
            {
                print("fixed");
            }
            else
            {
                GameObject[] remaningPlayersException = GameObject.FindGameObjectsWithTag("Player");
                playerRank = remaningPlayersException.Length;
                return;
            }
        }

        print(enemy.name + " killed " + transform.name);
        string s = enemy.transform.GetComponent<Player>().playerName + " -> " + playerName;
        overallUIScript.potentialFunction(s);
        enemy.GetComponent<Player>().enemy = null;
        enemy.GetComponent<Player>().kills += 1;
        GameObject[] remaningPlayers = GameObject.FindGameObjectsWithTag("Player");
        playerRank = remaningPlayers.Length;
    }

    void DebuggingPattern1()
    {
        Color32 background = new Color32(
                (byte)Random.Range(0, 255),
                (byte)Random.Range(0, 255),
                (byte)Random.Range(0, 255),
                255
            );
        foreach (GameObject player in allEnemies)
        {
            Debug.DrawLine(transform.position, player.gameObject.transform.position, background, 0f);
        }
    }

    void DebuggingPattern2()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f);

        Color32 background = new Color32(
                (byte)Random.Range(0, 255),
                (byte)Random.Range(0, 255),
                (byte)Random.Range(0, 255),
                255
            );

        if (colliders.Length > 2)
        {
            foreach (Collider col in colliders)
            {
                if (col.transform.tag == "Player" && col.transform.root != transform)
                {
                    Debug.DrawLine(transform.position, col.gameObject.transform.position, Color.red, 0f);
                }
            }
        }
    }

    void DebuggingPattern3()
    {
        LineRenderer ir = GetComponent<LineRenderer>();
        ir.SetWidth(2f, 2f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f);

        if (colliders.Length > 2)
        {
            foreach (Collider col in colliders)
            {
                if (col.transform.tag == "Player" && col.transform.root != transform)
                {
                    ir.SetPosition(0, transform.position);
                    ir.SetPosition(1, col.transform.position);
                }
            }
        }
    }

    void DebuggingPattern4()
    {
        Debug.DrawLine(Vector3.zero, transform.position, Color.red);
    }

    IEnumerator moving(Vector3 targetPosition)
    {
        randomPosition = new Vector3(transform.position.x + Random.Range(-100, 100), transform.position.y, transform.position.z + Random.Range(-100, 100));

        if (Mathf.Abs(randomPosition.x) > GameObject.Find("RedZone").transform.localScale.x || Mathf.Abs(randomPosition.z) > GameObject.Find("RedZone").transform.localScale.z)
        {
            yield return null;

            //StartCoroutine(moving(Vector3.zero));
        }

        yield return new WaitForSeconds(5f);

        //float xDiffrence = transform.position.x - randomPosition.x;
        //float yDiffrence = transform.position.y - randomPosition.y;
        //float Ngle = Mathf.Atan2(xDiffrence, yDiffrence) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, -Ngle - 90, 0);

        StartCoroutine(moving(randomPosition));
    }

    int fixing()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

        if (colliders.Length > 2)
        {
            foreach (Collider col in colliders)
            {
                if (col.transform.tag == "Player" && col.transform.root != transform && col.gameObject.activeSelf == true)
                {
                    enemy = col.transform;

                    state = State.attacking;
                    return 1;
                }
            }
        }
        return 0;
    }

    IEnumerator takingDamage(float speed)
    {
        if (enemy != null)
        {
            enemy.GetComponent<Player>().playerHealth -= 10f;
            enemy.GetComponent<Renderer>().material.color = Color.red;
        }
        yield return new WaitForSeconds(speed/2);
        if (enemy != null)
        {
            enemy.GetComponent<Renderer>().material.color = Color.white;
        }
        yield return new WaitForSeconds(speed / 2);
        StartCoroutine(takingDamage(speed));
    }

    IEnumerator safeZoneRun()
    {


        yield return new WaitForSeconds(5f);
        state = State.idle;

        StartCoroutine(moving(Vector3.zero));
    }

    IEnumerator outOfZoneDamage()
    {
        playerHealth -= 10f;

        yield return new WaitForSeconds(5f);
        state = State.redzone;
        StartCoroutine(outOfZoneDamage());
    }

    public void wakeUP()
    {
        overallUIScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<OverallUItesting>();
    }
}
