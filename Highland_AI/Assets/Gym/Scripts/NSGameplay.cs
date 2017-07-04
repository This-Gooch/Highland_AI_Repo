namespace NSGameplay {

    //Enum of units
    public enum EUnitIDs
    {
        Debug = 0,
        //Units from 1-99 are from class X
        UnitX1 = 1,
        UnitX2 = 2,
        //Units from 100-199 are from class Y
        UnitY1 = 100,
        UnityY2 = 101
        //Etc...
    }

    //Card/Action related namespace.
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
        /// Priority of card.
        ///     Once they called on a phase, what priority are they called in.
        /// </summary>
        public enum ECallPriority
        {
            NONE = 0,
            Lowest = 1,
            Medium = 2,
            High = 3,
            Highest = 4,
            COUNT
        }


        /// <summary>
        /// Types of cards.
        ///     Most types are set in the card's constructor.
        /// </summary>
        public enum ECardType
        {
            NULL,
            DEBUG,
            Instant,
            PassiveEffect,
            Minion,
            Secret,
            ERROR
        }

        /// <summary>
        /// Tooltip:
        /// Contains all the description for the card's tooltip.
        /// </summary>
        public class Tooltip
        {
            //Path in the resource folder.
            public string image_Path { get; set; }
            //Title or name of the popup tooltip.
            public string title { get; set; }
            //Main information displayed.
            public string description { get; set; }
        }

        public enum ECardKeys
        {
            //////////////////////////////////////////////////////////////
            ///Every cards will be listed here with a unique key.       //
            ///Use card name as a naming convention (in Pascal Case).   //
            ///Max value (int32) = 2147483647.                          //
            ///Standard set (Cards available to all classes):           //
            ///     reserved values:                                    //
            ///         0 - 1000                                        //
            /// Class 1:                                                //
            ///     reserved values:                                    //
            ///         2000 - 3000                                     //
            /// Class 2:                                                //
            ///     reserved values:                                    //
            ///         4000 - 5000                                     //
            /// Class 3:                                                //
            ///     reserved values:                                    //
            ///         6000 - 7000                                     //
            /// Class 4:                                                //
            ///     reserved values:                                    //
            ///         8000 - 9000                                     //
            /// Class 5:                                                //
            ///     reserved values:                                    //
            ///         10,000 - 11,000                                 //
            /// Class 6:                                                //
            ///     reserved values:                                    //
            ///         12,000 - 13,000                                 //
            /// Class 7:                                                //
            ///     reserved values:                                    //
            ///         14,000 - 15,000                                 //
            /// Class 8:                                                //
            ///     reserved values:                                    //
            ///         16,000 - 17,000                                 //
            ///                                                         //
            //////////////////////////////////////////////////////////////



            ///********************************************************///
            /// Standard Set                                           ///
            ///Card all units/classes can use in their decks.          ///
            ///Reversed from 0 - 1000                                  ///
            ///********************************************************///

            DirectAttack = 0,
            Guard = 100,

            ShuffleDeck = 200

            ///********************************************************///
            /// Class 1 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 2000 - 3000                               ///
            ///********************************************************///

            ///********************************************************///
            /// Class 2 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 4000 - 5000                               ///
            ///********************************************************///        

            ///********************************************************///
            /// Class 3 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 6000 - 7000                               ///
            ///********************************************************///


            ///********************************************************///
            /// Class 4 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 8000 - 9000                               ///
            ///********************************************************///

            ///********************************************************///
            /// Class 5 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 10000 - 11000                             ///
            ///********************************************************///


            ///********************************************************///
            /// Class 6 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 12000 - 13000                             ///
            ///********************************************************///

            ///********************************************************///
            /// Class 7 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 14000 - 15000                             ///
            ///********************************************************///

            ///********************************************************///
            /// Class 8 Set                                            ///
            ///xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx          ///
            ///Reversed from 16000 - 17000                             ///
            ///********************************************************///
        }


    }

    //State machine related namespace.
    namespace StateMachine
    {
        //State of the FSM.
        public enum State
        {
            DEBUG,
            P1_Exhaust,
            P1_Draw,
            /*Transition*/
            /// <summary>
            /// Draw_To_Play
            /// </summary>
            P1_PlayPhase,
                /*SubPhase*/P1_PlayPhase_Action,//Action executes
            P1_EndTurn,
            P1_Activate,

            P2_Exhaust,
            P2_Draw,
            /*Transition*/
            /// <summary>
            /// Draw_To_Play
            /// </summary>
            P2_PlayPhase,
                /*SubPhase*/P2_PlayPhase_Action,//Action executes
            P2_EndTurn,
            P2_Activate,



            GameOver_P1_Win,
            GameOver_P2_Win,
            GameOver_Draw
           
        }
        //Transition used between states.
        public enum Transition
        {
            //Draw_To_Play
        }
    }


}