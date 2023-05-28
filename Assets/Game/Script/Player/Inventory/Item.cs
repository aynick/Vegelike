using UnityEngine;

namespace Game.Script.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items", order = 0)]
    public class Item : ScriptableObject
    {
        public ItemsType itemsType;
        public Sprite sprite;
    }
}