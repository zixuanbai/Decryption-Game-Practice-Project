using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameH2A_SO", menuName = "MiniGame Data/GameH2A_SO")]

public class GameH2A_SO : ScriptableObject
{
    [SceneName] public string gameName;
    
    [Header("balls name and image")]
    public List<BallDetails> ballDetails;

    [Header("game logic")]

    public List<Conections> lineConections;

    public List<BallName> startBallOrder;
    public BallDetails GetBallDetails(BallName ballName) => ballDetails.Find(b => b.ballName == ballName);
}

[System.Serializable]
public class BallDetails 
{
    public BallName ballName;

    public Sprite wrongSprite;

    public Sprite rightSprite;

}

[System.Serializable]

public class Conections 
{
    public int from;

    public int to;
}



