using UnityEngine;

namespace Eiquif.UpgradeTree
{
    public class Player : MonoBehaviour
    {
        private UpgradeTreeRuntime runtime;
        void Start()
        {
            runtime = FindFirstObjectByType<UpgradeTreeRuntime>();

            runtime.Subscribe("NewID_1", OnHpChanged);
        }

        void OnDisable()
        {
            runtime.Unsubscribe("NewID_1", OnHpChanged);
        }

        void OnHpChanged(object data)
        {
            var node = (Node)data;
            Debug.Log($"HP изменилось: {node.name}");
        }
    }
}



