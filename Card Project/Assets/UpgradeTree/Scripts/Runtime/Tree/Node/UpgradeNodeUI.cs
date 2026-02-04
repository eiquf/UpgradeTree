namespace Eiquif.UpgradeTree.Runtime.Tree
{
    using Runtime.Node;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class UpgradeNodeUI : MonoBehaviour
    {
        public Node nodeData;
        public Image icon;
        public TextMeshProUGUI titleText;

        public void Setup(Node data)
        {
            nodeData = data;
            if (icon != null) icon.sprite = data.Icon;
            if (titleText != null) titleText.text = data.Name;

            Vector2 pos = data.position;
            pos.y = -pos.y;
            GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
}