using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBowlManager : MonoBehaviour
{
    public Sprite FoodBowlPhaseTwo;
    public Sprite FoodBowlPhaseThree;
    public Sprite FoodBowlEmpty;

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
        SkunkManager.ReachedPhaseTwo += OnReachPhaseTwo;
        SkunkManager.ReachedPhaseThree += OnReachPhaseThree;
        GameManager.WinGame += OnWin;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        SkunkManager.ReachedPhaseTwo -= OnReachPhaseTwo;
        SkunkManager.ReachedPhaseThree -= OnReachPhaseThree;
        GameManager.WinGame -= OnWin;
    }

    private void OnReachPhaseTwo()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = FoodBowlPhaseTwo;
    }

    private void OnReachPhaseThree()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = FoodBowlPhaseThree;
    }

    private void OnWin()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = FoodBowlEmpty;
    }
}
