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
        warnTime = lookTime * 0.2f;

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

    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        
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
        StartLooking?.Invoke();
        gameObject.GetComponent<SpriteRenderer>().sprite = SkunkLooking;
    }

    // TODO: GetSuspicious event?
    // With sprite change and music? Maybe visual effect?

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
    
    // TODO: interrupt idle and re-roll if player crosses a threshold?

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

        warnTime = Mathf.Max(idleTime * 0.2f, 0.5f);
    }
}
