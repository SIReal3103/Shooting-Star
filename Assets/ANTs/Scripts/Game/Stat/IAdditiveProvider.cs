using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Game
{
    public interface IAdditiveProvider
    {
        IEnumerable<float> GetAdditiveBonus();
    }
}