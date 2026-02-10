//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.IO;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public class LocalProgressionService : IProgressionProvider
    {
        private UpgradeTreeSaveData _data = new();
        private readonly string _savePath = Path.Combine(Application.persistentDataPath, "upgrades.json");

        public LocalProgressionService() => Load();

        public bool IsNodeUnlocked(NodeID id)
        {
            return _data.UnlockedNodeIds.Contains(id.Value);
        }

        public void UnlockNode(Node node)
        {
            if (!IsNodeUnlocked(node.ID))
            {
                _data.UnlockedNodeIds.Add(node.ID.Value);
                _data.SavedCurrency -= node.Cost;
                Save();
            }
        }

        public int GetCurrentCurrency() => _data.SavedCurrency;
        public int GetPlayerLevel() => 10;

        public void Save()
        {
            string json = JsonUtility.ToJson(_data);
            File.WriteAllText(_savePath, json);
        }

        public void Load()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                _data = JsonUtility.FromJson<UpgradeTreeSaveData>(json);
            }
        }

        public void ResetProgression()
        {
            _data.UnlockedNodeIds.Clear();

            _data.SavedCurrency = 0;

            if (File.Exists(_savePath))
            {
                File.Delete(_savePath);
            }

            Save();

            Debug.Log("Upgrade Tree Progression has been reset.");
        }
    }
}