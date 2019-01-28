using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerModel
{

    public string name;
    public bool isFirst = true;
    public List<LevelModel> levels;


    public PlayerModel(string name)
    {
        this.name = name;
        this.levels = new List<LevelModel>();
    }


    public string ToJSON(PlayerModel player)
    {
        return JsonUtility.ToJson(player);
    }

    public static PlayerModel CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerModel>(jsonString);
    }
}
