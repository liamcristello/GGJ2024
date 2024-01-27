using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float drinkDelta;

    public Slider DrinkSlider;
    public Image LoseImage;

    // Start is called before the first frame update
    void Start()
    {
        LoseImage.gameObject.SetActive(false);
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
        GameManager.LoseGame += OnLose;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        GameManager.LoseGame -= OnLose;
    }

    public void OnDrink()
    {
        DrinkSlider.value -= drinkDelta;
    }

    public void OnLose()
    {
        LoseImage.gameObject.SetActive(true);
    }
}
