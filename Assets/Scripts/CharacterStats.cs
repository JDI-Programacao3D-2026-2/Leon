using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string characterName;
    public TeamType team = TeamType.Enemy;
    public int kills = 0;
    public int deaths = 0;
    public GameManager gameManager;

    public enum TeamType
    {
        Player,
        Enemy
    }
    
    void Start()
    {
        AddPlayerName();
    }

    public void AddKill()
    {
        kills++;
        if (team == TeamType.Player)
        {
            gameManager.UpdateKills();
        }
    }

    public void AddDeath()
    {
        deaths++;
        if (team == TeamType.Player)
        {
            gameManager.UpdateDeaths();
        }
    }

    public void AddPlayerName()
    {
        if (team == TeamType.Player)
        {
            gameManager.PlayerName(characterName);
        }
    }
}
