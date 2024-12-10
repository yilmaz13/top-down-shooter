using System.Collections.Generic;
using UnityEngine;

public interface IEnemyListener 
{
    void OnEnemyDead(EnemyController enemyController);
}
