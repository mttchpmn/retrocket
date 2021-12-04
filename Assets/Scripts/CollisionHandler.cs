using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private int _activeLevelIndex;
    private int _nextLevelIndex;

    void Start()
    {
        _activeLevelIndex = SceneManager.GetActiveScene().buildIndex;
        _nextLevelIndex = _activeLevelIndex.Equals(SceneManager.sceneCountInBuildSettings - 1)
            ? 0
            : _activeLevelIndex + 1;
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit a friendly object!");
                break;
            case "Fuel":
                Debug.Log("You picked up fuel!");
                break;
            case "Finish":
                Debug.Log("You win!");
                LoadNextLevel();
                break;
            default:
                Debug.Log("You hit an obstacle.");
                ReloadLevel();
                return;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevelIndex);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(_activeLevelIndex);
    }
}