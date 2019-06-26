using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    public class SplashScreenState: IGameObject
    {
        StateSystemManager stateManager;

        public SplashScreenState(StateSystemManager stateSystemManager)
        {
            stateManager = stateSystemManager;
        }
        public void Update(double elapsedTimeMs)
        {

        }

        public void Render()
        {

        }
    }
}
