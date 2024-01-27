using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public UIManager UIMan;

    public delegate void LoseAction();
    public static event LoseAction LoseGame;

    // Bools to check if the player has been caught
    private bool dogIsDrinking = false;
    private bool skunkIsLooking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called when object becomes enabled.
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to events
        SkunkManager.StartLooking += StartLooking;
        SkunkManager.StopLooking += StopLooking;
        DogManager.StartDrinking += StartDrink;
        DogManager.StopDrinking += StopDrink;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        SkunkManager.StartLooking -= StartLooking;
        SkunkManager.StopLooking -= StopLooking;
    }

    #region "Drink water"
    private void StartDrink()
    {
        dogIsDrinking = true;
        CheckIfCaught();
    }

    private void Drink()
    {
        UIMan.OnDrink();
    }

    private void StopDrink()
    {
        dogIsDrinking = false;
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
        if (dogIsDrinking && skunkIsLooking)
        {
            LoseGame?.Invoke();
        }
    }
}
