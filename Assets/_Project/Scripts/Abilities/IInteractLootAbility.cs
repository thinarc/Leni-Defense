using UnityEngine;

namespace _Project.Scripts.Abilities
{
    public interface IInteractLootAbility
    {
        public void ManualUpdate() { }
        public void OnCaughtLoot(GameObject obj);
        public void OnInteract();
        public void OnCancelInteract() { }
    }
}