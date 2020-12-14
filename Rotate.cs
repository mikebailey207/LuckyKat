using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rotate : MonoBehaviour
{
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 100;

    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
       // float ver = Input.GetAxis("Vertical");
        transform.Rotate(0, hor * Time.deltaTime * speed, 0);

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
