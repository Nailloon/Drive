using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform car;
    private static int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;

    private void Update()
    {
        scoreText.text = ((int)(car.position.z / 2) * (-1)).ToString();
        score = ((int)(car.position.z / 2) * (-1));
        scoreText.text = score.ToString();
        highscoreText.text = "High score:" + PlayerPrefs.GetInt("HighScoreTex");
        if (PlayerPrefs.GetInt("HighScoreTex") <= score)
        {
            PlayerPrefs.SetInt("HighScoreTex", score);
        }
        PlayerPrefs.SetInt("Score", score);
    }
    public static int ReturnScore()
    {
        return PlayerPrefs.GetInt("Score");
    }
}