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
    public int kills;
    public int deaths;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopTimer = false;
        startTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTimer)
        {
            StartCountdown();
        }
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
}
