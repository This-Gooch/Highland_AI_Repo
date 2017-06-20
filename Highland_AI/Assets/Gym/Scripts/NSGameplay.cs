namespace NSGameplay {

    namespace Cards
    {
        /// <summary>
        /// Call Phase flags:
        /// 
        ///     This enum is used to call the card's effect during the correct 
        ///     phase. It's set up as flags so a card could be designed to be 
        ///     called on multiple phases. I.E. does an effect at the start and
        ///     end of turn.
        ///     **More can be added.
        /// </summary>
        public enum ECallPhase
        {           
            NONE = 0,
            DEBUG_FIRST = 1,
            PRE_START_TURN = 2,
            CHECK_EXHAUST = 4,
            START_TURN = 8,
            DRAW = 16,
            EXECUTE_ACTIONS = 32,
            PRE_END_TURN = 64,
            END_TURN = 128,
            SET_EXHAUST = 256,
            DEBUG_LAST = 512
        }
        /// <summary>
        /// Tooltip:
        /// Contains all the description for the card's tooltip.
        /// </summary>
        public class Tooltip
        {
            //Path in the resource folder.
            public string image_Path { get; private set; }
            //Title or name of the popup tooltip.
            public string title { get; private set; }
            //Main information displayed.
            public string description { get; private set; }
        }

    }

}