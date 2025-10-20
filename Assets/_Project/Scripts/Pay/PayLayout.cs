using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Pay
{
    public class PayLayout : MonoBehaviour
    {
        [SerializeField] private Transform[] columns;
        [SerializeField] private int[] columnLengths;
        [SerializeField, Space(5)] private GameObject slotPrefab;

        private int Price => columnLengths.Sum();

        private void OnValidate()
        {
            if (Price is > 0 and < 21) return;
            columnLengths = new int[columns.Length];
        }
        
        [Button, PropertySpace(5)]
        private void FillColumns()
        {
            if (columns.Length == 0) return;
            ClearColumns();

            for (var i = 0; i < columns.Length; i++)
            {
                for (var j = 0; j < columnLengths[i]; j++)
                {
                    Instantiate(slotPrefab, columns[i]);
                }
            }
        }
        
        [Button]
        private void ClearColumns()
        {
            foreach (var column in columns)
            {
                for (var i = column.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(column.GetChild(i).gameObject);
                }
            }
        }
    }
}
