using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class ProjectileWeaponManager : ProgressablePoolManager<ProjectileWeaponManager, ProjectileWeaponPool, ProjectileWeapon>
    {

    }

    public class ProjectileWeaponPool : ANTsPool<ProjectileWeaponPool, ProjectileWeapon>
    {

    }    
}