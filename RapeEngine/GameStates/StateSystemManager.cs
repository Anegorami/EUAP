using RapeEngine.GameStates;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    /// <summary>
    /// Class for managing the game's global set of states, and the main interface for interacting 
    /// with game state logic. 
    /// </summary>
    public class StateSystemManager
    {
        private Dictionary<String, IGameState> stateStore = new Dictionary<string, IGameState>();
        private IGameState currentState = null;
        private IGameState previousState = null;

        /// <summary>
        /// Tells the current game state to run its update sequence.
        /// </summary>
        /// <param name="elapsedTimeMs">Amount of time that has passed since the last update cycle in milliseconds</param>
        public void Update(double elapsedTimeMs)
        {
            if(currentState != null)
            {
                currentState.Update(elapsedTimeMs);
            }
        }

        /// <summary>
        /// Tells the current game state to run its render sequence.
        /// </summary>
        public void Render()
        {
            if (currentState != null)
            {
                currentState.Render();
            }
        }

        /// <summary>
        /// Checks whether or a game state exists in the manager.
        /// </summary>
        /// <param name="stateId">the id of the state</param>
        /// <returns></returns>
        public bool Exists(string stateId)
        {
            return stateStore.ContainsKey(stateId);
        }

        /// <summary>
        /// Adds a state to the manager.
        /// </summary>
        /// <param name="state"></param>
        public void AddState(IGameState state)
        {
            if(!stateStore.ContainsKey(state.StateId))
            {
                stateStore.Add(state.StateId, state);
            }
        }

        /// <summary>
        /// Sets a state to be the current main running state.
        /// 
        /// Will throw an Exception if the state referenced by stateId doesn't exist
        /// </summary>
        /// <param name="stateId"></param>
        public void SetState(string stateId)
        {
            //I'll let this throw an exception if they try and change state with a wrong ID.
            //Also temp is here so in case it does throw an exception we don't modify the internal
            //state
            IGameState temp = currentState;
            currentState = stateStore[stateId];

            previousState = temp;
        }

        /// <summary>
        /// Reverts state manager to its last known state before the most recent Set was called. 
        /// Only works one at a time, IE it only remembers the state right before the last setState call. Can't
        /// be called twice without calling setState again, does nothing otherwise. 
        /// </summary>
        public void RevertSetState()
        {
            //can't be done twice in a row
            if (previousState != null)
            {
                currentState = previousState;
                previousState = null;
            }
        }

        #region Tests

        public bool TestAddState()
        {
            OpenGL gl = new OpenGL();
            Renderer renderer = new Renderer(gl);
            IGameState obj = new SplashScreenState(this, renderer);
            
            AddState(obj);

            if(!stateStore.ContainsKey("splash"))
            {
                return false;
            }
            else if(!Exists("splash"))
            {
                return false;
            }

            stateStore.Clear();
            return true;
        }
        public bool TestStateTransitions()
        {
            OpenGL gl = new OpenGL();
            Renderer renderer = new Renderer(gl);
            IGameState obj = new SplashScreenState(this, renderer);
            IGameState obj1 = new SplashScreenState(this, renderer);
            IGameState temp;

            AddState(obj);
            AddState(obj1);

            AddState(obj); //shouldn't do anything
            if(stateStore.Count > 2)
            {
                return false;
            }

            //should change state
            temp = currentState;
            SetState("splash");

            if(temp == currentState)
            {
                return false;
            }

            //revert shouldn't do anything if only one loaded in, nothing to revert to
            temp = currentState;
            RevertSetState();

            if(temp != currentState)
            {
                return false;
            }

            //should set but then revert state back to the old
            temp = currentState;
            SetState("splash2");
            RevertSetState();
            if(temp != currentState)
            {
                return false;
            }

            //shouldn't revert twice
            temp = currentState;
            RevertSetState();
            if(temp != currentState)
            {
                return false;
            }

            stateStore.Clear();

            return true;
        }

        #endregion
    }
}
