using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Character Stats")]
    public string characterName;
    public int kills = 0;
    public int deaths = 0;

    [Header("Character Type")]
    public TeamType team = TeamType.Enemy;
    public CharacterType characterType;
    public EnemyType enemyType;

    [Header("Scripts")]
    public GameManager gameManager;
    public HealthManager healthManager;
    public PlayerMove playerMove;

    public enum TeamType
    {
        Player,
        Enemy
    }

    public enum CharacterType
    {
        ShoujoGirl,
        PlataformerBoy,
        RPGHero
    }

    public enum EnemyType
    {
        Core,
        Minion,
        Boss
    }
    
    void Start()
    {
        AddPlayerName();

        healthManager = GetComponent<HealthManager>();

        switch (team)
        {
            case TeamType.Player:
                playerMove = GetComponent<PlayerMove>();
                PlayerMaxHealth(characterType);
                playerMove.SetCharacterType(characterType);
                gameManager.UpdateKills();
                gameManager.UpdateDeaths();
                break;
            case TeamType.Enemy:
                EnemyMaxHealth(enemyType);
                break;
        }
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

    void PlayerMaxHealth(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.ShoujoGirl:
                healthManager.SetMaxHealth(100);
                break;
            case CharacterType.PlataformerBoy:
                healthManager.SetMaxHealth(150);
                break;
            case CharacterType.RPGHero:
                healthManager.SetMaxHealth(200);
                break;
        }
    }

    void EnemyMaxHealth(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Core:
                healthManager.SetMaxHealth(100);
                break;
            case EnemyType.Minion:
                healthManager.SetMaxHealth(50);
                break;
            case EnemyType.Boss:
                healthManager.SetMaxHealth(200);
                break;
        }
    }
}
