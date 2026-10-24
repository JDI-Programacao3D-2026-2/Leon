using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameStartText;
    public float startTime;
    public bool stopTimer;

    [Header("Tab Stats")]
    public GameObject tabPanel;
    public int kills = 0;
    public int deaths = 0;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI playerNameText;
    
    void Start()
    {
        stopTimer = false;
        startTime = 5f;
    }

    void Update()
    {
        if (!stopTimer)
        {
            StartCountdown();
        }
        ScorePanel();
    }

    void StartCountdown()
    {
        if (startTime > 0 && !stopTimer)
        {
            startTime -= Time.deltaTime;
            gameStartText.text = "Game starts in: " + startTime.ToString("0");
        }
        else
        {
            gameStartText.text = "Game Started!";
            Invoke("ClearCountdown", 1f);
        }
    }

    void ClearCountdown()
    {
        gameStartText.text = "";
        stopTimer = true;
        startTime = 5f;
    }

    void ScorePanel()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            tabPanel.SetActive(!tabPanel.activeSelf);
        }
    }

    public void UpdateKills()
    {
        kills++;
        killsText.text = kills.ToString();
    }

    public void UpdateDeaths()
    {
        deaths++;
        deathsText.text = deaths.ToString();
    }

    public void PlayerName(string name)
    {
        playerNameText.text = name;
    }
}
