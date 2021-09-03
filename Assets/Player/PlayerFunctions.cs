using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public static class PlayerFunctions
    {
        public static bool CheckRunInput()
        {
            if(Input.GetKey("left shift"))
            {
                return true;
            } else {
                return false;
            }
        }

        public static int GetDirectionHeld()
        {
            if(Input.GetKey("a"))
            {
                return -1;
            } else if (Input.GetKey("d")){
                return 1;
            } else {
                return 0;
            }
        }
    }
}