//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.IO;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public class LocalProgressionService :
        IProgressionProvider,
        IProgressionWriter
    {
        private UpgradeTreeSaveData _data = new();
        private readonly string _savePath =
            Path.Combine(Application.persistentDataPath, "upgrades.json");

        public LocalProgressionService() => Load();

        public bool IsNodeUnlocked(NodeID id) => _data.UnlockedNodeIds.Contains(id.Value);

        public int GetCurrentCurrency() => _data.SavedCurrency;

        public int GetPlayerLevel() => 10;

        public void UnlockNode(Node node)
        {
            if (IsNodeUnlocked(node.ID))
                return;

            _data.UnlockedNodeIds.Add(node.ID.Value);
            _data.SavedCurrency -= node.Cost;

            Save();
        }

        public void AddCurrency(int amount)
        {
            _data.SavedCurrency += amount;
            Save();
        }

        public void ResetProgression()
        {
            _data = new UpgradeTreeSaveData();
            Save();
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_data);
            File.WriteAllText(_savePath, json);
        }

        private void Load()
        {
            if (!File.Exists(_savePath))
            {
                _data = new UpgradeTreeSaveData();
                return;
            }

            string json = File.ReadAllText(_savePath);
            _data = JsonUtility.FromJson<UpgradeTreeSaveData>(json);
        }
    }
}