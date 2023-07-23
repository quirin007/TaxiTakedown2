using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGrow : MonoBehaviour
{
    public SnakeTail snakeTail;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject, 0.02f);
            snakeTail.AddTail();
        }
    }
}
