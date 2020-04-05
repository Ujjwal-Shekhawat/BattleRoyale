using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class headOverUItesting : MonoBehaviour
{
    public Transform parentPlayer;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerKills;
    public TextMeshProUGUI playerRank;
    public Image healthImage;
    public Player player;

    private void Start()
    {
        parentPlayer = transform.parent;
        player = transform.parent.GetComponent<Player>();
        healthImage = transform.Find("HealthBar").GetComponent<Image>();
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, -transform.localScale.z);
    }

    private void Update()
    {
        transform.LookAt(Camera.main.gameObject.transform.position); // Ensuring that all the headOverUI objects liik at the negative camera position

        playerName.text = "Name : " + player.playerName;
        playerKills.text = "Kills : " + player.kills;

        float currentlpayerHealth = parentPlayer.GetComponent<Player>().playerHealth;
        float maximumPlayerHealth = parentPlayer.GetComponent<Player>().playerMaxHealth;
        healthImage.transform.localScale = new Vector3(currentlpayerHealth / maximumPlayerHealth, 1, 1);
    }

    public void DisplayRanks()
    {
        playerRank.text = "Rank : " + parentPlayer.GetComponent<Player>().playerRank;
    }
}
