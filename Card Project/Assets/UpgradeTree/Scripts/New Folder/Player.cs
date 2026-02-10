using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

namespace Eiquif.UpgradeTree
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private NodeTree _tree;

        [SerializeField, NodeID(nameof(_tree))]
        private string _startNodeID;
        private void Start()
        {
            ActionRegistry.Subscribe(_startNodeID, Hello);
        }
        private void OnDisable()
        {
            ActionRegistry.Unsubscribe(_startNodeID, Hello);
        }
        void Hello(SkillSO so)
        {
            Debug.Log("Hellooooo");
        }
    }
}