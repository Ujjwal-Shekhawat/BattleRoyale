using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class staticScript
{
    public static float spawnSpacingX;
    public static float spawnSpacingY;
    public static float xPower = 5;
    public static float yPower = 5;

    public static void pcOptomaization(string s)
    {
        if(s == "High")
        {
            xPower = 10;
            yPower = 10;
        }
        if(s == "Medium")
        {
            xPower = 5;
            yPower = 5;
        }
        if(s == "Low")
        {
            xPower = 3;
            yPower = 3;
        }
    }

    public static void saveInput(string[] finalResultKills, string[] finalResultRanks)
    {
        string pathToFolder = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "FinalResults");
        if (!System.IO.File.Exists(pathToFolder))
        {
            System.IO.Directory.CreateDirectory(pathToFolder);
        }
        System.IO.File.WriteAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\FinalResults\\KillsResults.txt", finalResultKills);
        System.IO.File.WriteAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\FinalResults\\RanksResults.txt", finalResultRanks);
    }
}
