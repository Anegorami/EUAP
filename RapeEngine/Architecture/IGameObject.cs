using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    public interface IGameObject
    {
        void Update(double elapsedTimeMs);
        void Render();
    }
}
