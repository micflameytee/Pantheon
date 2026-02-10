using System.Collections.Generic;
using UnityEngine;

namespace PlayerGods
{
    [CreateAssetMenu(fileName = "PlayerList", menuName = "Game/PlayerClass/PlayerList")]
    public class PlayerList : ScriptableObject
    {
        public List<PlayerClassBase> playerList = new List<PlayerClassBase>();
    }
}