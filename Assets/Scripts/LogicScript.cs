using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public GameObject parrot;
    public GameObject tornado;
    public GameObject canvasGameinfos;
    public GameObject canvasMenu;
    private GameObject gameOverScreen;
    private bool gameIsOver = false;

    public void Awake()
    {
        GameManagerScript.instance.subscribeHideMenuCallback(delegate ()
        {
            canvasGameinfos.SetActive(true);
            canvasMenu.SetActive(false);
            Instantiate(tornado, new Vector3(transform.position.x-45, -0.5f, 10), transform.rotation);
            AudioManager.instance.playSFX("Start", 2f);
        });
    }

    public void Start()
    {
        if (GameManagerScript.instance.getShowMenu() == false)
        {
            GameManagerScript.instance.callhideMenuCallbacks();
        }
        gameOverScreen = canvasGameinfos.transform.Find("Game Over Screen").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            // Menu Principal
            if (GameManagerScript.instance.getShowMenu() == true)
            {
                startGame();
            }
            // Menu Game Over
            else if (gameOverScreen.transform.position.y == Screen.height / 2)
            {
                restartGame();
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameOverScreen.activeInHierarchy && gameOverScreen.transform.position.y != Screen.height/2)
        {
            if (gameOverScreen.transform.position.y < (Screen.height / 2)-150) gameOverScreen.transform.position += Vector3.up * 150;
            else gameOverScreen.transform.position = new Vector3(gameOverScreen.transform.position.x, Screen.height / 2, 0);
        }

        // if < s.h/2 alors monte
        // Sinon soit égal à s.h / ou sinon si pas égal, soit égal à s.h
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        if (gameIsOver) return;
        playerScore += scoreToAdd;
        Text scoreText = canvasGameinfos.transform.Find("Score Text").GetComponent<Text>();
        scoreText.text = playerScore.ToString();
        AudioManager.instance.playSFX("Score Up", 1f);
    }

    public void gameOver()
    {
        gameIsOver = true;
        StartCoroutine(showGameOverScreen());
        AudioManager.instance.stopMusic();
        AudioManager.instance.playSFX("Game Over", 3f);
    }

    private IEnumerator showGameOverScreen()
    {
        yield return new WaitForSeconds(1);

        GameObject scoreText = canvasGameinfos.transform.Find("Score Text").gameObject;
        scoreText.SetActive(false);

        Text gameOverScoreText = gameOverScreen.transform.Find("Score Text").GetComponent<Text>();
        gameOverScoreText.text = "Score: " + playerScore.ToString();
        gameOverScreen.SetActive(true);
        AudioManager.instance.playSFX("Game Over Pop", 5f);
    }
    
    public GameObject getParrot()
    {
        return parrot;
    }

    public void startGame()
    {
        GameManagerScript.instance.setShowMenuWithCallbacks(false);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void setIsOnMenu(bool isOnMenu)
    {
        parrot.GetComponent<Animator>().SetBool("isOnMenu", isOnMenu);
    }
}
