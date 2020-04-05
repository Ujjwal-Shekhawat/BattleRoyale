using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI;
public class SpawnningTestingPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject mainUItesting;
    public GameObject[] headOverUItesting;
    public List<GameObject> players;
    public int spawnSpacingX, spawnSpacingY;
    public List<string> playerDataList;
    public TextAsset textFile;
    public float xPower;
    public float yPower;

    private void Awake()
    {
        StartCoroutine(spawn());
    }

    private void Start()
    {
        //StartCoroutine(awakenUI());
    }

    private void Update()
    {
        Color32 background = new Color32(
                (byte)Random.Range(0, 255),
                (byte)Random.Range(0, 255),
                (byte)Random.Range(0, 255),
                255
            );
        for (int i = 0; i < players.Count - 1; i++)
        {
            //Debug.DrawLine(players[i].transform.position, players[i + 1].transform.position, background, 1f);
        }
    }

    IEnumerator awakenUI()
    {


        yield return new WaitForSeconds(1f);
        foreach(GameObject ui in headOverUItesting)
        {
            ui.SetActive(true);
        }
        mainUItesting.SetActive(true);
        foreach(GameObject player in players)
        {
            player.GetComponent<Player>().wakeUP();
            player.GetComponent<Player>().gameOver = 0f;
            player.transform.Find("HeadOverUI").gameObject.SetActive(true);
        }
    }

    IEnumerator spawn()
    {
        xPower = staticScript.xPower;
        yPower = staticScript.yPower;

        string line = textFile.text;
        string[] lines = line.Split('\n');

        int itreator = 0;
        for (float i = -xPower; i < xPower; i += 1)
        {
            for (float j = -yPower; j < yPower; j += 1)
            {
                yield return new WaitForSeconds(0.01f);
                Vector3 spawnPosition = new Vector3(i * spawnSpacingX, transform.position.y * 0f, j * spawnSpacingY);
                GameObject mainPlayer = Instantiate(player, spawnPosition, Quaternion.identity);
                mainPlayer.transform.Find("HeadOverUI").gameObject.SetActive(true);
                mainPlayer.transform.GetComponent<Player>().gameOver = 1f;
                players.Add(mainPlayer);
                mainPlayer.transform.name = lines[itreator].Trim();
                mainPlayer.transform.GetComponent<Player>().playerName = lines[itreator].Trim();
                mainPlayer.transform.GetComponent<Player>().playerHealth = 100f - Random.Range(10, 90);
                mainPlayer.transform.GetComponent<Player>().playerMaxHealth = 100f;
                mainPlayer.transform.GetComponent<Player>().hitSpeed = Random.Range(1, 3);
                itreator++;
            }
        }
        print(itreator);

        yield return new WaitForSeconds(1f);

        foreach(GameObject player in players)
        {
            player.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(awakenUI());
    }
}
