using Eiquif.UpgradeTree;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _goldReward = 10;
    private TestGameProgression _progression;
    private Player _player;
    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _progression = _player.Progression;
    }
    private void OnMouseDown()
    {
        Die();
    }

    void Die()
    {
        _progression.AddCurrency(_goldReward);
        Destroy(gameObject);
    }
}
