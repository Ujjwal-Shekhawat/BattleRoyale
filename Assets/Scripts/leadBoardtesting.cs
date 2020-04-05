using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class leadBoardtesting : MonoBehaviour
{
    public int index;

    public void Start()
    {
        index = 0;
    }

    public void onClick()
    {
        OverallUItesting mainUI = GameObject.FindGameObjectWithTag("GameController").GetComponent<OverallUItesting>();
        GameObject target = null;

        index = 0;

        foreach(TextMeshProUGUI ui in mainUI.texts)
        {
            if(transform.name == ui.transform.name)
            {
                foreach(GameObject player in mainUI.players)
                {
                    string[] thisName = this.GetComponent<TextMeshProUGUI>().text.ToString().Split(' ');
                    string fullName = thisName[0] + " " + thisName[1];
                    fullName = fullName.Trim();
                    if(thisName[0].Trim() == player.GetComponent<Player>().playerName.ToString().Trim())
                    {
                        target = player;
                        break;
                    }
                    if (fullName == player.GetComponent<Player>().playerName.ToString().Trim())
                    {
                        target = player;
                        break;
                    }
                    index++;
                }
            }
        }

        if(target != null)
        {
            mainUI.gotoPlayer(target);
            //mainUI.gotoPlayer(index);
        }
    }
}
