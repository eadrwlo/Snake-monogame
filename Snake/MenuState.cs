using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class MenuState
    {
        private static bool isShowMainMenuScene;
        private static bool isShowGameScene;
        private static bool isShowGameOverScene;

        public static bool IsShowMainMenuScene
        {
            get
            {
                return isShowMainMenuScene;
            }
            set
            {
                if (value == true)
                {
                    isShowGameScene = false;
                    isShowGameOverScene = false;
                }
                isShowMainMenuScene = value;
            }
        }
        public static bool IsShowGameScene
        {
            get => isShowGameScene;
            set
            {
                if (value == true)
                {
                    isShowMainMenuScene = false;
                    isShowGameOverScene = false;
                }
                isShowGameScene = value;
            }
        }
        public static bool IsShowGameOverScene { get { return isShowGameOverScene; } set { if (value == true) { isShowMainMenuScene = false; isShowGameScene = false; } isShowGameOverScene = value; } }
    }
}
