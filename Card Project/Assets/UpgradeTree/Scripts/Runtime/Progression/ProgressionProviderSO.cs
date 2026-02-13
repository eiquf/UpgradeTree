//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.IO;
using UnityEngine;

public abstract class ProgressionProviderSO : ScriptableObject, IProgressionProvider, IProgressionWriter
{
    protected UpgradeTreeSaveData _data = new();
    protected string SavePath => Path.Combine(Application.persistentDataPath, "upgrades.json");

    public abstract bool IsNodeUnlocked(NodeID id);
    public abstract int GetPlayerLevel();
    public abstract void UnlockNode(Node node);
    public abstract int GetCurrentCurrency();
    public abstract void AddCurrency(int amount);
    public abstract void ResetProgression();

    public virtual void Load()
    {
        if (!File.Exists(SavePath))
        {
            _data = new UpgradeTreeSaveData();
            return;
        }

        string json = File.ReadAllText(SavePath);
        _data = JsonUtility.FromJson<UpgradeTreeSaveData>(json);
    }

    public virtual void Save()
    {
        string json = JsonUtility.ToJson(_data, true);
        File.WriteAllText(SavePath, json);
    }
}