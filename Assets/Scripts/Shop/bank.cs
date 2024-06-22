using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bank : MonoBehaviour
{
    public TMP_Text[] text; // Масив текстових полів для відображення рахунку монет
    public static int count; // Статична змінна для зберігання рахунку монет

    void Start()
    {
        // Отримуємо збережений рахунок монет при старті гри
        count = PlayerPrefs.GetInt("amount", 0); // Додаємо 0 як значення за замовчуванням
        UpdateCoinText();
    }

    public void AddCoin(int coins)
    {
        count += coins;
        PlayerPrefs.SetInt("amount", count);
        UpdateCoinText();
    }

    public void TakeCoin(int coins)
    {
        count -= coins;
        PlayerPrefs.SetInt("amount", count);
        UpdateCoinText();
    }

    public int GetCount()
    {
        return count;
    }

    void UpdateCoinText()
    {
        // Оновлюємо всі текстові поля
        for (int i = 0; i < text.Length; i++)
        {
            text[i].text = count.ToString();
        }
    }
}