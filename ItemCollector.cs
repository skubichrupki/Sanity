using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{

    [SerializeField] private TMP_Text expCounter;
    private int exp = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // player (object script is attached to) collides with object with "Banana" tag
        if (collision.gameObject.CompareTag("Banana"))
        {
            Debug.Log("COLLISION!");
            Destroy(collision.gameObject);
            exp++;
            Debug.Log("Bananas: " + exp);

            expCounter.SetText("EXP: " + exp);
        }
    }


}
