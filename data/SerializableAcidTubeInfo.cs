using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.gmeister.unsighted.practice.data;

[Serializable]
public class SerializableAcidTubeInfo
{

    public string sceneName;
    public string objectName;
    public int numberOfHits;

    public SerializableAcidTubeInfo() { }

    public SerializableAcidTubeInfo(string sceneName, string objectName, int numberOfHits)
    {
        this.sceneName = sceneName;
        this.objectName = objectName;
        this.numberOfHits = numberOfHits;
    }

    public SerializableAcidTubeInfo(SerializableAcidTubeInfo other)
    {
        this.sceneName = other.sceneName;
        this.objectName = other.objectName;
        this.numberOfHits = other.numberOfHits;
    }

    public SerializableAcidTubeInfo(AcidTubeInfo other)
    {
        this.sceneName = other.sceneName;
        this.objectName = other.objectName;
        this.numberOfHits = other.numberOfHits;
    }

    public AcidTubeInfo ToAcidTubeInfo()
    {
        return new AcidTubeInfo(this.sceneName, this.objectName, this.numberOfHits);
    }

}
