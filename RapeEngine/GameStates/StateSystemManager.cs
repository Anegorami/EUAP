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

    }
}
