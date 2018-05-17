using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImages {

    public static Item GetImage(string name)
    {
        switch (name)
        {
            case "Sword":
                return new Sword();
        }

        return null;
    }
}
