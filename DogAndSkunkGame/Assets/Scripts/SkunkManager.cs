using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkunkManager : MonoBehaviour
{
    #region "Variables"
    public delegate void SkunkAction();
    public static event SkunkAction StartLooking;
    public static event SkunkAction StopLooking;

    public Sprite SkunkNormal;
    public Sprite SkunkAngry;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LookAtDog();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            LookAway();
        }
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

    private void ResetSkunkColor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SkunkNormal;
    }

    private void AlterSkunkColor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SkunkAngry;
    }

    private void LookAtDog()
    {
        StartLooking?.Invoke();
        AlterSkunkColor();
    }

    private void LookAway()
    {
        StopLooking?.Invoke();
        ResetSkunkColor();
    }

    // TODO: GetSuspicious event?
    // With sprite change and music? Maybe visual effect?
}
