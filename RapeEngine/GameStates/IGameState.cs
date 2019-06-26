using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    public interface IGameState : IGameObject
    {
        string StateId { get; }
    }
}
