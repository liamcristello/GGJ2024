using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager UIMan;

    public delegate void GameAction();
    public static event GameAction LoseGame;
    public static event GameAction WinGame;
    public static event GameAction PlayerGetsSprayed;

    // Bools to check if the player has been caught
    private bool dogIsEating = false;
    private bool skunkIsLooking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    string currentSceneName = SceneManager.GetActiveScene().name;
        //    SceneManager.LoadScene(currentSceneName);
        //}
    }

    /// <summary>
    /// Called when object becomes enabled.
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to events
        SkunkManager.StartLooking += StartLooking;
        SkunkManager.StopLooking += StopLooking;
        DogManager.StartEating += StartEat;
        DogManager.StopEating += StopEat;
        UIManager.FoodSliderChanged += CheckForWin;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        SkunkManager.StartLooking -= StartLooking;
        SkunkManager.StopLooking -= StopLooking;
        DogManager.StartEating -= StartEat;
        DogManager.StopEating -= StopEat;
        UIManager.FoodSliderChanged -= CheckForWin;
    }

    #region "Eating functions"
    private void StartEat()
    {
        dogIsEating = true;
        CheckIfCaught();
    }

    private void StopEat()
    {
        dogIsEating = false;
    }
    #endregion

    #region "Skunk"
    private void StartLooking()
    {
        skunkIsLooking = true;
        CheckIfCaught();
    }

    private void StopLooking()
    {
        skunkIsLooking = false;
    }
    #endregion

    /// <summary>
    /// Checks if the player has been caught by the skunk.
    /// </summary>
    private void CheckIfCaught()
    {
        if (dogIsEating && skunkIsLooking)
        {
            StartCoroutine(LoseGameSequence());
        }
    }

    private void CheckForWin(float foodValue)
    {
        if (foodValue <= 0.0f)
        {
            WinGame?.Invoke();
        }
    }

    private IEnumerator LoseGameSequence()
    {
        LoseGame?.Invoke();

        yield return new WaitForSecondsRealtime(3.0f);
        PlayerGetsSprayed?.Invoke();

        yield return new WaitForSecondsRealtime(3.0f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
