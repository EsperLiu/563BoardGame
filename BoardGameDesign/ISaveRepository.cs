using System;

namespace BoardGameFramework
{
    public interface ISaveRepository
    {
        bool Save(string fileName);

        MoveHistory Load(string fileName);

    }
}