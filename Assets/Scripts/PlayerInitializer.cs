using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public SpriteRenderer playerRenderer;
    public Skins skinManager;

    private void Start()
    {
        // ������������ ������� �������� ���� � PlayerPrefs
        int equippedSkinIndex = PlayerPrefs.GetInt("EquippedSkinIndex", 0);

        // ������������ �������� ���� �� ������
        playerRenderer.sprite = skinManager.skins[equippedSkinIndex];
    }
}
