using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Food : MonoBehaviour
{
    public int foodAmount = 3;


    private void OnEnable()
    {
        //foodAmount = 3;
        //World.Instance.GetQueue("Food").AddResource(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (foodAmount <= 0)
        {
            World.Instance.GetQueue("Food").RemoveResource(gameObject);
            gameObject.SetActive(false);
        }
    }
}

