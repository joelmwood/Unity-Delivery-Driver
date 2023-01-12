using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 200f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float destroyDelay = 0.5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, moveAmount, 0);

        //if car isn't moving, car can't turn
        if (moveAmount == 0 && steerAmount != 0)
        {
            transform.Rotate(0, 0, 0);
            // Debug.Log("Car can't turn while stationary");
        }
        //if car going forward right turn turns right
        if (moveAmount > 0 && steerAmount != 0)
        {
            transform.Rotate(0, 0, -steerAmount);
        }
        //if car going backward inverts turning so that pressing right turns car to right
        if (moveAmount < 0 && steerAmount != 0)
        {
            transform.Rotate(0, 0, steerAmount);
        }


    }
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collided with something");
        moveSpeed = slowSpeed;
        StartCoroutine(waiter());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Boost"){
            moveSpeed = boostSpeed;
            StartCoroutine(waiter());
            Destroy(other.gameObject, destroyDelay);
        }
    }
    IEnumerator waiter(){
        yield return new WaitForSecondsRealtime(5);
        moveSpeed = 20f;
    }

}
