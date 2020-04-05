using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverallUItesting : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] mainUItesting;
    public headOverUItesting[] headOverUIreference;
    public TextMeshProUGUI[] texts;
    public TextMeshProUGUI[] results;
    public TextMeshProUGUI[] ranks;
    public GameObject[] extraProfiles; // Extras like other elements that needs to be activated after the game is over.
    public List<TextMeshProUGUI> textsList;
    public ScrollRect resultScreen;
    public TextMeshProUGUI killBoard;
    public List<GameObject> playersList;
    public float stopUpdates = 0f;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        mainUItesting = new GameObject[players.Length];
        headOverUIreference = new headOverUItesting[players.Length];


        for(int i = 0; i < players.Length; i++)
        {
            playersList.Add(players[i]);
            mainUItesting[i] = players[i].transform.Find("HeadOverUI").gameObject;
            headOverUIreference[i] = players[i].transform.Find("HeadOverUI").GetComponent<headOverUItesting>();
        }
        for (int i = 0; i < players.Length; i++)
        {
            textsList.Add(texts[i]);
        }
        playersList.Sort((a, b) => {
            return b.GetComponent<Player>().playerHealth.CompareTo(a.GetComponent<Player>().playerHealth);
        });
        for (int i = 0; i < playersList.Count; i++)
        {
            textsList[i].text = (playersList[i].GetComponent<Player>().playerName + " : " + (playersList[i].transform.GetComponent<Player>().kills).ToString()).ToString();
            //print(textsList[i].text);
        }
    }

    private void Update()
    {
        //foreach(TextMeshProUGUI text in texts)
        //{
        //    text.text = players[0].transform.GetComponent<Player>().playerName;
        //}
        
        if(playersList.Count == 1 && stopUpdates != 1f)
        {
            killBoard.text = playersList[0].transform.GetComponent<Player>().playerName + " is the winner";
            for(int i = 0; i < players.Length; i++)
            {
                players[i].gameObject.SetActive(true);
                players[i].transform.GetComponent<Player>().gameOver = 1f;
                DisplayOverAllStats();
            }
        }
    }

    public void removeElementFromList(GameObject element)
    {
        print("Some one got beaten up");
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = ""; // Just so that the deceased players dont show up on the leadboard
        }
        textsList.RemoveAt(textsList.Count - 1);
        playersList.Remove(element);
        sortUpdate();
    }

    public void sortUpdate()
    {
        playersList.Sort((a, b) => {
            return b.GetComponent<Player>().kills.CompareTo(a.GetComponent<Player>().kills);
        });
        leadBoardUpdate();
    }

    void leadBoardUpdate()
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            textsList[i].text = (playersList[i].GetComponent<Player>().playerName + " : " + (playersList[i].transform.GetComponent<Player>().kills).ToString()).ToString();
            //print(textsList[i].text);
        }
    }

    public void potentialFunction(string s)
    {
        killBoard.text = s;
    }

    public void DisplayOverAllStats()
    {
        if (resultScreen.transform.gameObject.activeSelf == false)
        {
            resultScreen.gameObject.SetActive(true);
            foreach(GameObject profile in extraProfiles)
            {
                profile.gameObject.SetActive(true);
            }
        }
        List<GameObject> allplayers = new List<GameObject>();
        for(int i = 0; i < players.Length; i++)
        {
            allplayers.Add(players[i]);
        }
        foreach(GameObject player in allplayers)
        {
            player.transform.GetComponent<MeshRenderer>().enabled = false;
            player.transform.Find("HeadOverUI").gameObject.SetActive(true);
        }

        allplayers.Sort((a, b) => {
            return b.GetComponent<Player>().kills.CompareTo(a.GetComponent<Player>().kills);
        });

        for(int i = 0; i < players.Length; i++)
        {
            results[i].text = allplayers[i].transform.name + " has killed " + (allplayers[i].transform.GetComponent<Player>().kills).ToString() + " players";
        }
        for(int i = 0; i < mainUItesting.Length; i++)
        {
            headOverUIreference[i].DisplayRanks();
        }

        allplayers.Sort((b, a) => {
            return b.GetComponent<Player>().playerRank.CompareTo(a.GetComponent<Player>().playerRank);
        });

        for (int i = 0; i < players.Length; i++)
        {
            ranks[i].text = allplayers[i].transform.name + " is " + (allplayers[i].transform.GetComponent<Player>().playerRank).ToString();
        }

        // <Summary Saving to Desktop (start)>
        string[] s;
        string[] r;

        s = new string[players.Length];
        r = new string[players.Length];
        for(int i = 0; i < players.Length; i++)
        {
            s[i] = results[i].text.ToString().Trim();
        }

        allplayers.Sort((a, b) => {
            return a.GetComponent<Player>().playerRank.CompareTo(b.GetComponent<Player>().playerRank);
        });

        for (int i = 0; i < players.Length; i++)
        {
            r[i] = allplayers[i].gameObject.transform.GetComponent<Player>().playerName.ToString().Trim();
            r[i] += " has finished in ";
            r[i] += allplayers[i].gameObject.transform.GetComponent<Player>().playerRank.ToString().Trim() + " position.";
        }

        staticScript.saveInput(s, r);
        // <Sumary Saving to Desktop (end)>


        stopUpdates = 1f;
        //Time.timeScale = 0f;
    }

    public void gotoPlayer(GameObject targettedPlayer)
    {
        cameraScript mainCameraScript = Camera.main.transform.GetComponent<cameraScript>();
        mainCameraScript.player = targettedPlayer.transform;
    }

    public void gotoPlayer(int index)
    {
        cameraScript mainCameraScript = Camera.main.transform.GetComponent<cameraScript>();
        mainCameraScript.player = players[index].transform;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void gotoMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
/// <Summary>
/// Just remember how much you learned
/// <Summary>