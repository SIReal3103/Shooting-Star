﻿using UnityEngine;

using ANTs.Template;

namespace ANTs.Core
{
    public class EnemyPool : ANTsPool<EnemyPool, DieAction>
    {

    }

    public class EnemyData
    {
        public Vector3 spawnPosition;

        public EnemyData(Vector3 position)
        {
            this.spawnPosition = position;
        }
    }
}