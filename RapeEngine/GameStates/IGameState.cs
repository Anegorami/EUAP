using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    /// <summary>
    /// Abstract for a game state. They act like abstracted game objects themselves -- needing to tell
    /// their own member objects to render, and states need to update like anything else -- but 
    /// also own some unique information for state logic and management. 
    /// </summary>
    public interface IGameState : IGameObject
    {
        /// <summary>
        /// The referential ID of the state itself
        /// </summary>
        string StateId { get; }
    }
}
