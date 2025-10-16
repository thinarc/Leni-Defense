using UnityEngine;

namespace _Project.Scripts.EntryPoints
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform eventSystem;
        [SerializeField] private Object canvas;
        
        private void Start()
        {
            if (eventSystem.parent) eventSystem.SetParent(null);
            DontDestroyOnLoad(eventSystem);
            DontDestroyOnLoad(canvas);
        }
    }
}