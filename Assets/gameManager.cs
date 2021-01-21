using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject pickup;
    public Vector3 spawnValues;
    private int pickupCount = 5;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float countdownWait;
    public float gameTimer;
    public bool gameActive;

    public Text scoreText;
    public Text announcementText;
    public Text countdownText;
    public Text timerText;
    public Text restartText;
    public int score;

    public AudioSource background;
    public AudioSource win;
    public AudioSource lose;
    private bool winPlayed = false;
    private bool losePlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameTimer = 12;
        StartCoroutine(Countdown());
        StartCoroutine(SpawnWaves());
        StartCoroutine(Timer());
    }

    IEnumerator Countdown()
    {
        while (countdownWait > 0)
        {
            countdownText.text = countdownWait.ToString();
            yield return new WaitForSeconds(1f);
            countdownWait--;
        }
        announcementText.text = "";
        countdownText.text = "GO!";
        gameActive = true;
        background.Play();
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds (startWait);
        while (gameTimer >= 0)
        {
            for (int i = 0; i < pickupCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (pickup, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
        }
    }

    IEnumerator Timer()
    {
        while (gameTimer >= 0)
        {
            timerText.text = gameTimer.ToString();
            yield return new WaitForSeconds(1f);
            gameTimer--;
        }
        gameActive = false;
    }
    // Update is called once per frame
    void Update()
    {
        score = GameObject.Find("catcherMonkey").GetComponent<PlayerMovement>().score;

        if (gameActive == false && score >= 5)
        {
            background.Stop();
            if (winPlayed == false)
            {
                win.Play();
                winPlayed = true;
            }
            scoreText.text = "FINAL SCORE: " + score.ToString();
            announcementText.text = "YOU WIN!";
            restartText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("SampleScene");

                }
        }

        if (gameActive == false && score <5)
        {
            background.Stop();
            if(losePlayed == false)
            {
                lose.Play();
                losePlayed = true;
            }
            scoreText.text = "FINAL SCORE: " + score.ToString();
            announcementText.text = "YOU LOSE!";
            restartText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
