using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    V každé scéně musí být Input objekt aby fungovalo ovládání ze stěny. Ten v inspektoru má Input managers, 
    kde je MouseInput a TuioInput (vstup ze stěny).
    Dál se v něm dá nastavit Miss Effect, effect kliknutí do prázdna (ve hrách co jsme zkoušeli to byl třeba takový křížek,
    prostě jen aby bylo vidět že se opravdu kliklo), popřípadě i Hit Sound.


    Tohle je jednoduchý script, který se dá dát na objekt a bude hlásit pozici kliknutí.
    Na objektu musí být i 2D collider (3D nefunguje)
*/

//Include knihovny ovládání - nesnažte se najít zdrojáky, jsou jen zkomplovaný v .dll
using initi.prefabScripts;

//Musíme dědit z BaseHittable
public class UkazkovyScript : BaseHittable
{
   //Override metody Hit - zavolá se pokud se na tento objekt klikne na stěně
   public override void Hit(Vector2 hitPosition){
        //hitPosition jsou globální souřadnice doteku
     //    Debug.Log(hitPosition);
   }


   /*
        Vedle je ještě script BaseInteractive od jednoho z předchozích týmů, pro ukázku jak se dá upravit chování
        BaseHittable.
        Unity zvládne ovládání myší, ze stěny se potom ale předávají informace o dotyku před Tuio,
        takže některé chování se může trochu lišit. Dotyk se tam například nesnímá každý frame, nedá se spoléhat na
        to, že každý Update() bude mít nový input (nebude).
   */

}
