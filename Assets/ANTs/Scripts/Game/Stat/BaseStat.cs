using UnityEngine;

namespace ANTs.Game
{
    public class BaseStat : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField] int level = 1;
        [SerializeField] CharacterClass characterClass = CharacterClass.Player;
        [SerializeField] StatManager statManager;
    }
}