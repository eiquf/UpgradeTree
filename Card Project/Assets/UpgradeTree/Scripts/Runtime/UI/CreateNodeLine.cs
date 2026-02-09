//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Eiquif.UpgradeTree.Runtime
{
    public class CreateNodeLine : IElement<List<GameObject>>
    {
        public void Execute(List<GameObject> nodes)
        {
            RectTransform fromRect = nodes[0].GetComponent<RectTransform>();
            RectTransform toRect = nodes[1].GetComponent<RectTransform>();

            GameObject lineObj = new("ConnectLine", typeof(Image));
            RectTransform lineRect = lineObj.GetComponent<RectTransform>();

            lineRect.SetParent(toRect, false);
            lineRect.SetAsFirstSibling();

            Image lineImage = lineObj.GetComponent<Image>();
            lineImage.color = Color.white;

            Vector3 fromWorld = fromRect.TransformPoint(fromRect.rect.center);
            Vector3 toWorld = toRect.TransformPoint(toRect.rect.center);

            Vector2 fromLocal = toRect.InverseTransformPoint(fromWorld);
            Vector2 toLocal = Vector2.zero;

            Vector2 direction = toLocal - fromLocal;
            float distance = direction.magnitude;

            lineRect.anchorMin = lineRect.anchorMax = new Vector2(0.5f, 0.5f);
            lineRect.pivot = new Vector2(0, 0.5f);

            lineRect.anchoredPosition = fromLocal;
            lineRect.sizeDelta = new Vector2(distance, 8f);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            lineRect.localRotation = Quaternion.Euler(0, 0, angle);
        }

    }
}