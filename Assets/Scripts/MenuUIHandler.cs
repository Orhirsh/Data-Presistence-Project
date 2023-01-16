using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public string playerInput;


    public void LoadGame()
    {
        playerInput = inputField.text;
        PlayerData.Instance.playerName = inputField.text;       
        SceneManager.LoadScene(1);
        
    }
    


}
