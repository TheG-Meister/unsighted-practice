using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.gmeister.unsighted.practice.data;

[Serializable]
public class SerializableDropInfo
{

    public string enemyNameID;
    public int index;

    public SerializableDropInfo()
    {
    }

    public SerializableDropInfo(string enemyNameID, int index)
    {
        this.enemyNameID = enemyNameID;
        this.index = index;
    }

    public SerializableDropInfo(SerializableDropInfo other)
    {
        this.enemyNameID = other.enemyNameID;
        this.index = other.index;
    }

    public SerializableDropInfo(DropInfo other)
    {
        this.enemyNameID = other.enemyNameID;
        this.index = other.index;
    }

    public DropInfo ToDropInfo()
    {
        return new DropInfo(this.enemyNameID)
        {
            index = this.index,
        };
    }

}
