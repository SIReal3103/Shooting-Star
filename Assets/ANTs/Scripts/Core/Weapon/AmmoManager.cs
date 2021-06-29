using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class AmmoManager : ProgressablePoolManager<AmmoManager, AmmoPool, WeaponAmmo>
    {

    }

    public class AmmoPool : ANTsPool<AmmoPool, WeaponAmmo>
    {
    }

    
}