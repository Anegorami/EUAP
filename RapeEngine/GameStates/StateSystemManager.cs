using RapeEngine.GameStates;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    public class StateSystemManager
    {
        private Dictionary<String, IGameState> stateStore = new Dictionary<string, IGameState>();
        private IGameState currentState = null;
        private IGameState previousState = null;

        public void Update(double elapsedTimeMs)
        {
            if(currentState != null)
            {
                currentState.Update(elapsedTimeMs);
            }
        }

        public void Render()
        {
            if (currentState != null)
            {
                currentState.Render();
            }
        }

        public bool Exists(string stateId)
        {
            return stateStore.ContainsKey(stateId);
        }

        public void AddState(IGameState state)
        {
            if(!stateStore.ContainsKey(state.StateId))
            {
                stateStore.Add(state.StateId, state);
            }
        }

        public void SetState(string stateId)
        {
            //I'll let this throw an exception if they try and change state with a wrong ID.
            //Also temp is here so in case it does throw an exception we don't modify the internal
            //state
            IGameState temp = currentState;
            currentState = stateStore[stateId];

            previousState = temp;
        }

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
            IGameState obj = new SplashScreenState(this, gl);
            
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
            IGameState obj = new SplashScreenState(this, gl);
            IGameState obj1 = new SplashScreenState(this, gl);
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
