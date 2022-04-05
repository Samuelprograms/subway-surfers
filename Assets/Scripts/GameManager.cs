using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager instance;
    public Text scoreText;
    public Player player;
    private void Awake()
    {
        instance = this;
    }
    public void IncrementScore()
    {
        score++;
        scoreText.text = "Porritos: "+score.ToString();
        if(score % 20 == 0)
        {
            player.PlayerRunVelocity += 3;
        }
    }
}
