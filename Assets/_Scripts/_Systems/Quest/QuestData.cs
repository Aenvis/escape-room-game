using Project.Consts;
using UnityEngine;

namespace Project.Systems.Quest
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "New Quest Data", order = 0)]
    public class QuestData : ScriptableObject
    {
        [SerializeField] private ItemName requiredItem;
        [SerializeField][Multiline] private string textWhenUncompleted;

        public bool Completed { get; set; }
        public ItemName GetRequiredItem() => requiredItem;
        public string GetText() => textWhenUncompleted;
    }
}