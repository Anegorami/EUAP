using RapeEngine.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    public class StateSystemManager
    {
        private Dictionary<String, IGameObject> stateStore = new Dictionary<string, IGameObject>();
        private IGameObject currentState = null;
        private IGameObject previousState = null;

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

        public void AddState(string stateId, IGameObject state)
        {
            if(!stateStore.ContainsKey(stateId))
            {
                stateStore.Add(stateId, state);
            }
        }

        public void SetState(string stateId)
        {
            //I'll let this throw an exception if they try and change state with a wrong ID.
            //Also temp is here so in case it does throw an exception we don't modify the internal
            //state
            IGameObject temp = currentState;
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
            IGameObject obj = new SplashScreenState(this);

            AddState("splash", obj);

            if(!stateStore.ContainsKey("splash"))
            {
                return false;
            }
            else if(!Exists("splash"))
            {
                return false;
            }

            return true;
        }
        public bool TestStateTransitions()
        {
            IGameObject obj = new SplashScreenState(this);
            IGameObject obj1 = new SplashScreenState(this);
            IGameObject temp;

            AddState("splash", obj);
            AddState("splash2", obj1);

            AddState("splash", obj); //shouldn't do anything
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

            return true;
        }

        #endregion
    }
}
