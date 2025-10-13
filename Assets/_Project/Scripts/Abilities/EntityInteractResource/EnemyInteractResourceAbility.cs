using UnityEngine;

namespace _Project.Scripts.Abilities.EntityInteractResource
{
    public class EnemyInteractResourceAbility : IInteractResourceAbility
    {
        private readonly Transform _creator;
        private bool _catchRes;
        
        public EnemyInteractResourceAbility(Transform creator)
        {
            _creator = creator;
        }
        
        public void OnResource(GameObject res) => GrabResource(res);
        private void GrabResource(GameObject resource)
        {
            if (_catchRes) return;
            _catchRes = true;
            resource.transform.SetParent(_creator);
            resource.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            resource.GetComponent<Collider2D>().enabled = false;
        }

        public void OnInteract()
        {
            Debug.Log("enemy не отдает ресурсы");
        }
    }
}