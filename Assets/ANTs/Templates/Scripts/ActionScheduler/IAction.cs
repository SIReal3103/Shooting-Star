using System.Collections;
using UnityEngine;

namespace ANTs.Core
{
    public interface IAction
    {
        bool IsActionStart { get; set; }
        void ActionStart();
        void ActionCancel();
    }
}