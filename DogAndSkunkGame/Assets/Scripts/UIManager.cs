using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void SliderAction(float value);
    public static event SliderAction FoodSliderChanged;

    public float foodDelta;

    public Slider FoodSlider;
    public Image LoseImage;
    public Image WinImage;

    // Start is called before the first frame update
    void Start()
    {
        LoseImage.gameObject.SetActive(false);
        WinImage.gameObject.SetActive(false);
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
        GameManager.WinGame += OnWinGame;
        DogManager.StartEating += BeginEating;
        DogManager.StopEating += EndEating;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        GameManager.LoseGame -= OnLose;
        GameManager.WinGame -= OnWinGame;
        DogManager.StartEating -= BeginEating;
        DogManager.StopEating -= EndEating;
    }

    public void BeginEating()
    {
        StartCoroutine(Eat());
    }

    public void EndEating()
    {
        StopAllCoroutines();
    }

    private void OnLose()
    {
        EndEating();
        LoseImage.gameObject.SetActive(true);
    }

    private IEnumerator Eat()
    {
        FoodSlider.value -= foodDelta;
        FoodSliderChanged?.Invoke(FoodSlider.value);

        yield return new WaitForEndOfFrame();
        StartCoroutine(Eat());
    }

    private void OnWinGame()
    {
        EndEating();
        WinImage.gameObject.SetActive(true);
    }
}
