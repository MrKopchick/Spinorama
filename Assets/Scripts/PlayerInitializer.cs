using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public SpriteRenderer playerRenderer;
    public Skins skinManager;

    private void Start()
    {
        // Завантаження індексу обраного скіна з PlayerPrefs
        int equippedSkinIndex = PlayerPrefs.GetInt("EquippedSkinIndex", 0);

        // Встановлення обраного скіна на гравця
        playerRenderer.sprite = skinManager.skins[equippedSkinIndex];
    }
}
