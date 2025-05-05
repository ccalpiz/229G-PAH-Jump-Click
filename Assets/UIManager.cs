using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] hpIcons; // Assign in inspector

    public void UpdateHP(int currentHP, int maxHP)
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].enabled = i < currentHP;
        }
    }
}
