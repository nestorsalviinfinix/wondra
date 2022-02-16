using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scenes.Game.Interfaces
{
    interface IChessPlayerManager
    {
        ChessPlayer GetFirstPlayer();
        ChessPlayer GetNextPlayer();

        void AddPlayer(ChessPlayer player);
    }
}
