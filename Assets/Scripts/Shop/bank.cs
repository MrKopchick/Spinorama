using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bank : MonoBehaviour
{
    public TMP_Text[] text; // ����� ��������� ���� ��� ����������� ������� �����
    public static int count; // �������� ����� ��� ��������� ������� �����

    void Start()
    {
        // �������� ���������� ������� ����� ��� ����� ���
        count = PlayerPrefs.GetInt("amount", 0); // ������ 0 �� �������� �� �������������
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
        // ��������� �� ������� ����
        for (int i = 0; i < text.Length; i++)
        {
            text[i].text = count.ToString();
        }
    }
}