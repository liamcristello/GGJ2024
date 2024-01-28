using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
    public delegate void DogAction();
    public static event DogAction StartEating;
    public static event DogAction StopEating;

    public Sprite DogNormal;
    public Sprite DogScared;
    public Sprite DogCaught;

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
    }

    /// <summary>
    /// Called when object becomes enabled.
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to events
        GameManager.WinGame += OnWinGame;
        GameManager.LoseGame += OnLoseGame;
    }

    /// <summary>
    /// Called when object is disabled.
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        GameManager.WinGame -= OnWinGame;
        GameManager.LoseGame -= OnLoseGame;
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

    private void OnWinGame()
    {
        disableInput = true;
        EndEat();
    }

    private void OnLoseGame()
    {
        disableInput = true;
        EndEat();
        DogCaughtAnim();
    }
}
