using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite DogNormal;
    public Sprite DogScared;
    public Sprite SkunkNormal;
    public Sprite SkunkAngry;

    public GameObject Skunk;
    public GameObject Dog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Dog.GetComponent<SpriteRenderer>().sprite = DogScared;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Dog.GetComponent<SpriteRenderer>().sprite = DogNormal;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skunk.GetComponent<SpriteRenderer>().sprite = SkunkAngry;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Skunk.GetComponent<SpriteRenderer>().sprite = SkunkNormal;
        }
    }
}
