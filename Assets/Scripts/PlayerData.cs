using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public string playerName;
    public int playerScore;
    public int bestscore;
    public string bestPlayer;
    private void Awake()
    { 
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadInfo();
        } 
    }
    [System.Serializable]
    class SaveInfo
    {
        public string playerName;
        public int bestscore;
        public string bestPlayer;
    }
    public void LogInfo()
    {
        SaveInfo data = new SaveInfo();
        data.playerName = playerName;
        data.bestscore = playerScore;
        data.bestPlayer = bestPlayer;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveInfo data = JsonUtility.FromJson<SaveInfo>(json);

            playerName = data.playerName;
            bestscore = data.bestscore;
            bestPlayer = data.bestPlayer;
        }
    }
    

    
}