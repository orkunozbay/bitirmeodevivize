using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //next level start collision player ise
        {
            SceneController.instance.NextLevel(); //sonraki level geç
        }
    }
}
