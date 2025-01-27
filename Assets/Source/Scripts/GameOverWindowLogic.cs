using UnityEngine;

public class GameOverWindowLogic : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverObject;
    private void Start()
    {
        Player.OnPlayerDeath += ShowGameOverWindow;
    }

    private void ShowGameOverWindow()
    {
        _gameOverObject.SetActive(true);
    }
}