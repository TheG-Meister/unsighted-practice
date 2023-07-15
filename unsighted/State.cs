using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using dev.gmeister.unsighted.practice.cheats;
using dev.gmeister.unsighted.practice.data;

namespace dev.gmeister.unsighted.practice.unsighted;

[Serializable]
public class State
{

    public PlayerData data;
    public List<SerializableAcidTubeInfo> acidTubesInfo;
    public List<DeceasedEnemyInfo> deceasedEnemiesInfo;
    public List<SerializableDropInfo> dropInfo;

    public State() { }

    public State(PlayerData data, List<SerializableAcidTubeInfo> acidTubesInfo, List<DeceasedEnemyInfo> deceasedEnemiesInfo, List<SerializableDropInfo> dropInfo)
    {
        this.data = data;
        this.acidTubesInfo = acidTubesInfo;
        this.deceasedEnemiesInfo = deceasedEnemiesInfo;
        this.dropInfo = dropInfo;
    }

    public static State Create()
    {
        PlayerData data = State.DeepClone(PseudoSingleton<Helpers>.instance.GetPlayerData());

        State state = new(data, new(), new(), new());

        foreach (AcidTubeInfo tube in LevelController.acidTubesInfo)
        {
            state.acidTubesInfo.Add(new SerializableAcidTubeInfo(tube.sceneName, tube.objectName, tube.numberOfHits));
        }

        foreach (DeceasedEnemyInfo info in LevelController.deceasedEnemiesInfo)
        {
            state.deceasedEnemiesInfo.Add(DeceasedEnemyInfos.Clone(info));
        }

        foreach (DropInfo info in LevelController.dropInfo)
        {
            state.dropInfo.Add(new SerializableDropInfo(info));
        }

        return state;
    }

    public static T DeepClone<T>(T obj) where T : class
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SurrogateSelector surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new Vector2Serializer());
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), new Vector3Serializer());
            binaryFormatter.SurrogateSelector = surrogateSelector;
            binaryFormatter.Serialize(stream, obj);
            stream.Position = 0;
            return (T)binaryFormatter.Deserialize(stream);
        }
    }

}

public sealed class Vector2Serializer : ISerializationSurrogate
{
    // Token: 0x06002E12 RID: 11794 RVA: 0x00124E78 File Offset: 0x00123078
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Vector2 vector = (Vector2)obj;
        info.AddValue("x", vector.x);
        info.AddValue("y", vector.y);
    }

    // Token: 0x06002E13 RID: 11795 RVA: 0x00124EB0 File Offset: 0x001230B0
    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Vector2 vector = (Vector2)obj;
        vector.x = (float)info.GetValue("x", typeof(float));
        vector.y = (float)info.GetValue("y", typeof(float));
        obj = vector;
        return obj;
    }
}

public sealed class Vector3Serializer : ISerializationSurrogate
{
    // Token: 0x06002E0F RID: 11791 RVA: 0x00124DB0 File Offset: 0x00122FB0
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Vector3 vector = (Vector3)obj;
        info.AddValue("x", vector.x);
        info.AddValue("y", vector.y);
        info.AddValue("z", vector.z);
    }

    // Token: 0x06002E10 RID: 11792 RVA: 0x00124DF8 File Offset: 0x00122FF8
    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Vector3 vector = (Vector3)obj;
        vector.x = (float)info.GetValue("x", typeof(float));
        vector.y = (float)info.GetValue("y", typeof(float));
        vector.z = (float)info.GetValue("z", typeof(float));
        obj = vector;
        return obj;
    }
}