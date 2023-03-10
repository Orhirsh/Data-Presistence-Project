using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public string bestPlayer;
    

    public Text ScoreText;
    public GameObject GameOverText;
    public Text playerNameText;
    private bool m_Started = false;
    private int m_Points;
    public Text bestScore;
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
            playerNameText.text = "Player: " + PlayerData.Instance.playerName; // setting current player name text on screen

            bestScore.text = $"Best Score : {PlayerData.Instance.bestscore} By : {PlayerData.Instance.bestPlayer}"; // setting best score;


        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        PlayerData.Instance.playerScore = m_Points;
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {

        if (PlayerData.Instance.playerScore > PlayerData.Instance.bestscore)
        {
           
            PlayerData.Instance.bestscore = PlayerData.Instance.playerScore;
            PlayerData.Instance.bestPlayer = PlayerData.Instance.playerName;

        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void Exit()
    {
        GameOver();
       PlayerData.Instance.LogInfo();     
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }
}
