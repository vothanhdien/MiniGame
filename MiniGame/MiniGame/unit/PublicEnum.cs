using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public enum UnitTypeEnum { ZOMBIE, SCORPION, MUMMY, CHARACTER, TREASURE};
    public enum UnitStateEnum { MOVEFORWAR, MOVELEFT, MOVERIGHT, MOVEBACK};

    public enum GameStateEnum { GAME_END, GAME_PAUSE, GAME_PLAYING, GAME_START, GAME_SAVE, GAME_LOAD};
}