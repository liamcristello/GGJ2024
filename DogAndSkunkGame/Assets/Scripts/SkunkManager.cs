using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkunkManager : MonoBehaviour
{
    #region "Variables"
    public delegate void SkunkAction();
    public static event SkunkAction StartLooking;
    public static event SkunkAction StopLooking;

    public Sprite SkunkNormal;
    public Sprite SkunkWarn;
    public Sprite SkunkLooking;
    public Sprite SkunkAngry;

    public float phaseTwoThreshold;
    public float phaseThreeThreshold;

    public float phaseOneMinIdleTime;
    public float phaseOneMaxIdleTime;
    public float phaseTwoMinIdleTime;
    public float phaseTwoMaxIdleTime;
    public float phaseThreeMinIdleTime;
    public float phaseThreeMaxIdleTime;

    public float phaseOneLookTime;
    public float phaseTwoLookTime;
    public float phaseThreeLookTime;

    public float minimumWarnTime;
    public float warnPercentageOfIdleTime;

    private float idleTime;
    private float lookTime;
    private float warnTime;
    private Phase currentPhase;

    private enum Phase
    {
        One,
        Two,
        Three
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = Phase.One;
        lookTime = phaseOneLookTime;
        warnTime = lookTime * warnPercentageOfIdleTime;

        StartCoroutine(SkunkLoop());
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
        UIManager.FoodSliderChanged += CheckIfPassedThreshold;
        GameManager.WinGame += OnWinGame;
        GameManager.LoseGame += OnLoseGame;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        UIManager.FoodSliderChanged -= CheckIfPassedThreshold;
        GameManager.WinGame -= OnWinGame;
        GameManager.LoseGame -= OnLoseGame;
    }

    private void EnterIdleMode()
    {
        StopLooking?.Invoke();
        gameObject.GetComponent<SpriteRenderer>().sprite = SkunkNormal;
    }

    private void EnterWarnMode()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SkunkWarn;
    }

    private void EnterLookMode()
    {
        Debug.Log("Looking respectfully?");
        StartLooking?.Invoke();
        gameObject.GetComponent<SpriteRenderer>().sprite = SkunkLooking;
    }

    private void EnterAngryMode()
    {
        Debug.Log("Angy");
        gameObject.GetComponent<SpriteRenderer>().sprite = SkunkAngry;
    }

    private IEnumerator SkunkLoop()
    {
        EnterIdleMode();
        RandomizeIdleTime();
        yield return new WaitForSecondsRealtime(idleTime - warnTime);

        EnterWarnMode();
        yield return new WaitForSecondsRealtime(warnTime);

        EnterLookMode();
        yield return new WaitForSecondsRealtime(lookTime);

        yield return StartCoroutine(SkunkLoop());
    }

    private void RandomizeIdleTime()
    {
        switch(currentPhase)
        {
            case Phase.One:
                idleTime = Random.Range(phaseOneMinIdleTime, phaseOneMaxIdleTime);
                break;
            case Phase.Two:
                idleTime = Random.Range(phaseTwoMinIdleTime, phaseTwoMaxIdleTime);
                break;
            case Phase.Three: 
                idleTime = Random.Range(phaseThreeMinIdleTime, phaseThreeMaxIdleTime);
                break;
            default:
                return;
        }

        warnTime = Mathf.Max(idleTime * warnPercentageOfIdleTime, minimumWarnTime);
    }

    private void CheckIfPassedThreshold(float foodValue)
    {
        if (currentPhase == Phase.One && foodValue <= phaseTwoThreshold)
        {
            currentPhase = Phase.Two;
        }

        else if (currentPhase == Phase.Two && foodValue <= phaseThreeThreshold)
        {
            currentPhase = Phase.Three;
        }
    }

    private void OnLoseGame()
    {
        StopAllCoroutines();
        EnterAngryMode();
    }

    private void OnWinGame()
    {
        StopAllCoroutines();
        EnterIdleMode();
    }
}
