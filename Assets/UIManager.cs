using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    public void UpdateHP(int currentHP, int maxHP)
    {
        hpText.text = "HP: " + currentHP + "/" + maxHP;
    }
}
