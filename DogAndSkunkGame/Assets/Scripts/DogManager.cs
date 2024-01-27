using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
    public delegate void DrinkAction();
    public static event DrinkAction StartEating;
    public static event DrinkAction StopEating;

    public Sprite DogNormal;
    public Sprite DogScared;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartToDrink();
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrink();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrink();
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

    private void StartToDrink()
    {
        StartEating?.Invoke();
        AlterDogColor();
    }

    private void ContinueDrink()
    {

    }

    private void EndDrink()
    {
        StopEating?.Invoke();
        ResetDogColor();
    }

    private void ResetDogColor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DogNormal;
    }

    private void AlterDogColor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DogScared;
    }
}
