using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    public class GameMain
    {

        private GameLoopExecutor gameLoopExecutor;

        public void GameMainBegin()
        {
            gameLoopExecutor = new GameLoopExecutor(GameLoop);
        }

        void GameLoop(double elapsedTimeMs)
        {

        }
    }
}
