using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverUI;
    public GameObject victoryUI;

    void Update()
    {
        if (player == null)
        {
            gameOverUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void Victory()
    {
        victoryUI.SetActive(true);
    }
}
