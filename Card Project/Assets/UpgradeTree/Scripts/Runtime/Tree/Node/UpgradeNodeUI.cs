using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class UpgradeNodeUI : MonoBehaviour
{
    public Node nodeData; // Ссылка на данные SO
    public Image icon;
    public TextMeshProUGUI titleText;

    public void Setup(Node data)
    {
        nodeData = data;
        if (icon != null) icon.sprite = data.Icon;
        if (titleText != null) titleText.text = data.Name;


        Vector2 pos = data.position;
        pos.y = -pos.y; // Инверсия, так как в UI Y идет вверх
        GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
