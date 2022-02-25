using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomName 
{
    string[] first = new string[] { "Ald", "Alf", "Ash", "Barn", "Blan", "Brack", "Brad", "Brain", "Brom", "Bur", "Cas", "Chelm", "Clere",
            "Cook", "Dart", "Dur", "Edg", "Eg", "El", "Elm", "En", "Farn", "Flit", "Hart", "Horn", "Hors", "Hurst", "Kings", "Leather", "Maiden",
            "Marl", "Mel", "Nort", "Pem", "Pen", "Prest", "Rock", "Shaft", "Shriv", "Sod", "South", "Staf", "Stain", "Stap", "Sud", "Sun", "Walt",
            "Watch", "Wen", "Wet", "Whit", "Win", "Wy", "Wych" };
    string[] doubles = new string[] { "Abb", "Bass", "Booth", "Both", "Burr", "Camb", "Camm", "Cann", "Chedd", "Chill", "Chipp", "Cir",
            "Dribb", "Egg", "Ell", "Emm", "End", "Fald", "Full", "Hamm", "Hamp", "Hann", "Kett", "Mill", "Pend", "Redd", "Ribb", "Roth", "Sir",
            "Skell", "Sodd", "Sudd", "Sund", "Tipp", "Todd", "Warr", "Wolv", "Worr" };
    string[] postdoubles = new string[] { "en", "er", "ing" };
    string[] mid = new string[] { " on ", "bas", "ber", "comp", "den", "en", "mans", "mer", "re", "rens", "ring", "sey", "stan", "-under-" };
    string[] last = new string[] { "avon", "bage", "beck", "borne", "borough", "bourne", "bridge", "brook", "brough", "bury", "by", "castle",
            "cester", "chester", "combe", "den", "ditch", "don", "down", "ey", "field", "ford", "grove", "hall", "ham", "hampton", "head", "lake",
            "ley", "ling", "low", "mere", "moor", "nell", "ney", "over", "port", "shot", "side", "smith", "sted", "stoke", "thorne", "ton", "tree",
            "wang", "well", "wich", "wick", "wold", "wood", "worth" };

    /// <summary>
    /// Gets the next name from the generator.
    /// </summary>
    public string Generate()
    {
        string finished_name = "";
        int pd = 0;

        if (Random.value > 0.4)
        {
            finished_name = finished_name + doubles[Random.Range(0, doubles.Length)];

            if (Random.value > 0.6)
            {
                finished_name = finished_name + postdoubles[Random.Range(0, postdoubles.Length)];
                pd = 1;
            }
            else
            {
                finished_name = finished_name.Substring(0, finished_name.Length - 1);
            }
        }
        else
            finished_name = finished_name + first[Random.Range(0, first.Length)];

        if (Random.value > 0.5 && pd == 0)
        {
            if (finished_name.EndsWith("r") || finished_name.EndsWith("b"))
            {
                if (Random.value > 0.4)
                    finished_name = finished_name + "ble";
                else
                    finished_name = finished_name + "gle";
            }
            else if (finished_name.EndsWith("n") || finished_name.EndsWith("d"))
                finished_name = finished_name + "dle";
            else if (finished_name.EndsWith("s"))
                finished_name = finished_name + "tle";

            if (Random.value > 0.7 && finished_name.EndsWith("le"))
                finished_name = finished_name + "s";
        }
        else if (Random.value > 0.5)
        {
            if (finished_name.EndsWith("n"))
            {
                if (Random.value > 0.5)
                    finished_name = finished_name + "s";
                else
                    finished_name = finished_name + "d";
            }
            else if (finished_name.EndsWith("m"))
                finished_name = finished_name + "s";


            if (Random.value > 0.7)
                finished_name = finished_name + mid[Random.Range(0, mid.Length)];
        }


        finished_name = finished_name + last[Random.Range(0, last.Length)];

        //string[] fix = finished_name.Split(' ');

        //if (fix[1].Equals(" ", System.StringComparison.InvariantCultureIgnoreCase))
        //    finished_name = fix[0] + ' ' + fix[2].ToString().ToUpper();


        //fix = finished_name.Split('-');
        //if (fix[1].Equals("-", System.StringComparison.InvariantCultureIgnoreCase))
        //    finished_name = fix[0] + '-' + fix[2].ToString().ToUpper();

        return finished_name;
    }
}
