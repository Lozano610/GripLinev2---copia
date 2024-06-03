using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cointext : MonoBehaviour
{
    private TMP_Text CoinsText;

    private void Start()
    {
        CoinsText = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        CoinsText.text = Coins.TotalCoins.ToString();
    }
}
