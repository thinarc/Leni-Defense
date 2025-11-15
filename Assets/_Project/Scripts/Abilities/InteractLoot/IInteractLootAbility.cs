using UnityEngine;

namespace _Project.Scripts.Abilities.InteractLoot
{
    public interface IInteractLootAbility
    {
        public void OnCaughtLoot(GameObject obj);
        public void OnInteract();
        public void OnCancelInteract() { }
    }
}