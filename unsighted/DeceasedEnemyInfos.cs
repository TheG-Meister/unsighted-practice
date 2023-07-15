using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.Random;

namespace dev.gmeister.unsighted.practice.unsighted;

public class DeceasedEnemyInfos
{

    public static DeceasedEnemyInfo Clone(DeceasedEnemyInfo info)
    {
        return new DeceasedEnemyInfo()
        {
            sceneName = info.sceneName,
            areaName = info.areaName,
            enemyName = info.enemyName,
            position = info.position,
            height = info.height,
            fellInHole = info.fellInHole,
        };
    }

}
