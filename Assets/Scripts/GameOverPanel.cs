using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public Player player;
    void Update()
    {
        if (!player.PlayerIsAlive)
        {
            gameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            gameOverMenuUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }

}
