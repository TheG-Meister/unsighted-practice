using dev.gmeister.unsighted.practice.unsighted;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.gmeister.unsighted.practice.cheats;

public class SaveStates
{

    private string dir;

    public SaveStates(string dir)
    {
        this.dir = dir;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
    }

    public List<string> GetAllStates()
    {
        List<string> states = Directory.GetFiles(this.dir).ToList();
        states.RemoveAll(file => !Path.HasExtension(file) || Path.GetExtension(file) != ".dat");
        states = states.Select(file => Path.GetFileNameWithoutExtension(file)).ToList();
        return states;
    }

    public string GetLocalPath(string name)
    {
        return Path.Combine(this.dir, name + ".dat");
    }

    public string GetFullPath(string name)
    {
        return Path.GetFullPath(this.GetLocalPath(name));
    }

    public void CreateAndWrite(string name)
    {
        State.CreateAndWrite(this.GetLocalPath(name));
    }

    public bool Exists(string name)
    {
        return File.Exists(this.GetLocalPath(name));
    }

    public void ReadAndLoad(string name)
    {
        if (!File.Exists(this.GetLocalPath(name))) throw new ArgumentException($"The state {name} does not exist");
        State.ReadAndLoad(this.GetLocalPath(name));
    }

}
