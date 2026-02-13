using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

namespace Eiquif.UpgradeTree
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private NodeTree _tree;
        public TestGameProgression Progression;

        [SerializeField, NodeID(nameof(_tree))]
        private string _fireballNodeID;

        private bool _canShoot;

        private void Start()
        {
            ActionRegistry.Subscribe(_fireballNodeID, EnableFireball);
        }

        private void OnDisable()
        {
            ActionRegistry.Unsubscribe(_fireballNodeID, EnableFireball);
        }

        void EnableFireball(SkillSO so)
        {
            _canShoot = true;
            Debug.Log("Fireball Unlocked!");
        }

        private void Update()
        {
            if (_canShoot && Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            Debug.Log("🔥 Fireball Shot!");
        }
    }
}