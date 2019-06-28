using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    /// <summary>
    /// The standard assortment of game objects in the game world -- handling both logic about their
    /// own state and possessing items which may need to be rendered on screen. One of the 
    /// architectural defaults that the engine and modules will know how to work with.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Tells this game object to update its state in the game. 
        /// </summary>
        /// <param name="elapsedTimeMs">The amount of time that has passed since the last update frame was processed</param>
        void Update(double elapsedTimeMs);

        /// <summary>
        /// Tells this game object to try and render itself, if there's any rendering for it to do.
        /// </summary>
        void Render();
    }
}
