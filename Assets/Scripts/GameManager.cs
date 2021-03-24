using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isPlayerDead = false;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Game Manager is NULL");

            return _instance;          
        }
    }


    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {

        if (isPlayerDead)
        {
            Invoke("RestartLevel", 2f);
        }
    }

    public void SetDeathBool()
    {
        isPlayerDead = true;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
