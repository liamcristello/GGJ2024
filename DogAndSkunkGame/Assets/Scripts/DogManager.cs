using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogManager : MonoBehaviour
{
    public delegate void DogAction();
    public static event DogAction StartEating;
    public static event DogAction StopEating;

    public Sprite DogNormal;
    public Sprite DogScared;
    public Sprite DogCaught;
    public Sprite DogSprayed;
    public Sprite DogRun;

    private bool wonGame = false;
    private bool disableInput = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!disableInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartToEat();
            }

            else if (Input.GetMouseButtonUp(0))
            {
                EndEat();
            }
        }
        else if (wonGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }
    }

    /// <summary>
    /// Called when object becomes enabled.
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to events
        GameManager.WinGame += OnWinGame;
        GameManager.LoseGame += OnLoseGame;
        GameManager.PlayerGetsSprayed += OnGetSprayed;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        GameManager.WinGame -= OnWinGame;
        GameManager.LoseGame -= OnLoseGame;
        GameManager.PlayerGetsSprayed -= OnGetSprayed;
    }

    private void StartToEat()
    {
        StartEating?.Invoke();
        DogEatingAnim();
    }

    private void EndEat()
    {
        StopEating?.Invoke();
        DogIdleAnim();
    }

    private void DogIdleAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DogNormal;
    }

    private void DogEatingAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DogScared;
    }

    private void DogCaughtAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DogCaught;
    }

    private void DogSprayedAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DogSprayed;
    }

    private void DogRunAnim()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DogRun;
        transform.position = new Vector3(-6.73f, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
    }

    private void OnWinGame()
    {
        disableInput = true;
        wonGame = true;
        EndEat();
        DogRunAnim();
    }

    private void OnLoseGame()
    {
        disableInput = true;
        EndEat();
        DogCaughtAnim();
    }

    private void OnGetSprayed()
    {
        DogSprayedAnim();
    }
}
