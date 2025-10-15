using UnityEngine;

namespace _Project.Scripts.Abilities.LootHandlers
{
    public class EnemyInteractLootAbility : IInteractLootAbility
    {
        private readonly Transform _creator;
        private bool _catchLoot;
        
        public EnemyInteractLootAbility(Transform creator)
        {
            _creator = creator;
        }

        public void OnCaughtLoot(GameObject loot)
        {
            if (_catchLoot) return;
            _catchLoot = true;
            loot.transform.SetParent(_creator);
            loot.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            loot.GetComponent<Collider2D>().enabled = false;
        }

        public void OnInteract()
        {
            Debug.LogWarning("Enemy should not interact with loot yet.");
        }
    }
}