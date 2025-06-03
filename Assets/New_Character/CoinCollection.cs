using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;
    public TextMeshProUGUI coinText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin++;
            coinText.text = $"{Coin.ToString()} / 30";
            Debug.Log(Coin);
            Destroy(other.gameObject);
            if (Coin == 30)
            {
                SceneManager.LoadScene("SampleScene1"); // Cambia esto por el nombre real
            }
        }
    }
}
