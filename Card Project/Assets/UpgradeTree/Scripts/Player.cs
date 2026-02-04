namespace Eiquif.UpgradeTree.Runtime
{
    using Eiquif.UpgradeTree.Runtime.Tree;
    using Nodee = Runtime.Node.Node;
    using UnityEngine;

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
            var node = (Nodee)data;
            Debug.Log($"HP изменилось: {node.name}");
        }
    }
}



