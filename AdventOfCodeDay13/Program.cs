namespace AdventOfCodeDay13;

internal class Program
{
    static void Main(string[] args)
    {
        var alleZeilen = File.ReadAllLines("input.txt").ToList();
        //Console.WriteLine("Part 1: " + Part1(alleZeilen));
        Console.WriteLine("Part 2: " + Part2(alleZeilen));
    }

    //klappt nicht für meinen Input
    private static object Part2(List<string> alleZeilen)
    {
        //weiss nicht warum es für meinen Input nicht funktioniert
        var positionen = new List<((int Spalte, int Zeile), string aktuelleRichtung, int anzahlDrehungen)>();
        var field = FeldErstellen(alleZeilen, positionen);

        do
        {
            positionen = positionen.OrderBy(x => x.Item1.Zeile).ThenBy(x => x.Item1.Spalte).ToList();
            var huhu = 0;
            for (var index = 0; huhu < positionen.Count; index+=0)
            {
                
                if (positionen[index].aktuelleRichtung == ">")
                {
                    if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "v", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "^", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "-")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), ">", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "v", nextValue));
                        }
                        else
                        {
                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "^", ++nextValue)
                                : ((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), ">", ++nextValue));
                        }
                    }
                }
                else if (positionen[index].aktuelleRichtung == "v")
                {
                    if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "<", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), ">", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "|")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "v", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "<", nextValue));
                        }
                        else
                        {
                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), ">", ++nextValue)
                                : ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "v", ++nextValue));
                        }
                    }
                }
                else if (positionen[index].aktuelleRichtung == "<")
                {
                    if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "v", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "^", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "-")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "<", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "^", nextValue));
                        }
                        else
                        { 
                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "v", ++nextValue)
                                : ((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "<", ++nextValue));
                        }
                    }
                }
                else
                {
                    if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "|")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "^", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "<", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), ">", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), ">", nextValue));
                        }
                        else
                        {
                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "<", ++nextValue)
                                : ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "^", ++nextValue));
                        }
                    }
                }
                
                positionen.RemoveAt(0);
                
                for (var zeile = 0; zeile < alleZeilen.Count; zeile++)
                {
                    for (var spalte = 0; spalte < alleZeilen[0].Length; spalte++)
                    {
                        var positionens = positionen.Count(x => x.Item1.Spalte == spalte && x.Item1.Zeile == zeile);
                        if (positionens > 1)
                        {
                            var ergebnis = positionen.Where(x => x.Item1.Spalte == spalte && x.Item1.Zeile == zeile).ToArray();
                            foreach(var element in ergebnis)
                            {
                                positionen.Remove(element);
                            }
                            
                            huhu -= 2;
                        }
                    }
                }
                huhu++;
            }

            if (positionen.Count == 1)
            {
                return positionen[0].Item1;
            }
            
        } while (true);
    }
    //falsche
    //34,79
    //(112, 116)
    //26,37
    //(62, 59)
    // 88,52
    //40,94

    public static void Schreiben(List<((int Spalte, int Zeile), string aktuelleRichtung, int anzahlDrehungen)> posotionen, List<string> alleZeilen, string[,] feld)
    {
        for(var zeile = 0; zeile < alleZeilen.Count; zeile++)
        {
            for(var spalte = 0; spalte < alleZeilen[0].Length; spalte++)
            {
                var test = posotionen.Where(x => x.Item1.Spalte == spalte && x.Item1.Zeile == zeile).ToList();
                if(test.Count != 0)
                {
                    Console.Write(test[0].aktuelleRichtung);
                }
                else
                {
                    Console.Write(feld[zeile,spalte]);   
                }

            }

            Console.WriteLine();
        }
    }
    
    private static object Part1(List<string> alleZeilen)
    {
        var positionen = new List<((int Spalte, int Zeile), string aktuelleRichtung, int anzahlDrehungen)>();
        var field = FeldErstellen(alleZeilen, positionen);

        do
        {
            positionen = positionen.OrderBy(x => x.Item1.Zeile).ThenBy(x => x.Item1.Spalte).ToList();

            for (var index = 0; index < positionen.Count; index++)
            {
                if (positionen[index].aktuelleRichtung == ">")
                {
                    if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "v", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "^", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "-")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), ">", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte + 1] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "v", nextValue));
                        }
                        else
                        {
                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), "^", ++nextValue)
                                : ((positionen[index].Item1.Spalte + 1, positionen[index].Item1.Zeile), ">", ++nextValue));
                        }
                    }
                }
                else if (positionen[index].aktuelleRichtung == "v")
                {
                    if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "<", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), ">", positionen[index].anzahlDrehungen));


                    }
                    else if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "|")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "v", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile + 1, positionen[index].Item1.Spalte] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "<", nextValue));
                        }
                        else
                        {
                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), ">", ++nextValue)
                                : ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile + 1), "v", ++nextValue));
                        }
                    }

                }
                else if (positionen[index].aktuelleRichtung == "<")
                {
                    if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "v", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "^", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "-")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "<", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile, positionen[index].Item1.Spalte - 1] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "^", nextValue));
                        }
                        else
                        {

                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "v", ++nextValue)
                                : ((positionen[index].Item1.Spalte - 1, positionen[index].Item1.Zeile), "<", ++nextValue));
                        }
                    }
                }
                else
                {
                    if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "|")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "^", positionen[index].anzahlDrehungen));
                    }
                    else if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "\\")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "<", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "/")
                    {
                        positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), ">", positionen[index].anzahlDrehungen));

                    }
                    else if (field[positionen[index].Item1.Zeile - 1, positionen[index].Item1.Spalte] == "+")
                    {
                        var nextValue = positionen[index].anzahlDrehungen;

                        if (nextValue == 3)
                        {
                            nextValue = 1;
                            positionen.Add(((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), ">", nextValue));
                        }
                        else
                        {

                            positionen.Add(nextValue == 1
                                ? ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "<", ++nextValue)
                                : ((positionen[index].Item1.Spalte, positionen[index].Item1.Zeile - 1), "^", ++nextValue));
                        }
                    }
                }

                positionen.RemoveAt(0);
                index--;
                for (var zeile = 0; zeile < alleZeilen.Count; zeile++)
                {
                    for (var spalte = 0; spalte < alleZeilen[0].Length; spalte++)
                    {

                        var positionens = positionen.Count(x => x.Item1.Spalte == spalte && x.Item1.Zeile == zeile);
                        if (positionens > 1)
                        {
                            positionen = positionen.OrderBy(x => x.Item1.Zeile).ThenBy(x => x.Item1.Spalte).ToList();
                            var ergebnis = positionen.Where(x => x.Item1.Spalte == spalte && x.Item1.Zeile == zeile).ToArray();
                            return ergebnis[0].Item1;
                        }
                    }
                }
            }
        } while (true);
    }

    private static string[,] FeldErstellen(List<string> alleZeilen, List<((int Spalte, int Zeile), string aktuelleRichtung, int anzahlDrehungen)> positionen)
    {
        var feld = new string[alleZeilen.Count, alleZeilen[0].Length];
        for (var zeile = 0; zeile < alleZeilen.Count; zeile++)
        {
            for (var spalte = 0; spalte < alleZeilen[0].Length; spalte++)
            {
                if (alleZeilen[zeile][spalte] != '-' && alleZeilen[zeile][spalte] != '|' && alleZeilen[zeile][spalte] != '/' && alleZeilen[zeile][spalte] != '\\' && alleZeilen[zeile][spalte] != '+')
                {
                    if (alleZeilen[zeile][spalte] == '>')
                    {
                        feld[zeile, spalte] = "-";
                        positionen.Add(((spalte, zeile), alleZeilen[zeile][spalte].ToString(), 1));
                    }
                    else if (alleZeilen[zeile][spalte] == 'v')
                    {
                        feld[zeile, spalte] = "|";
                        positionen.Add(((spalte, zeile), alleZeilen[zeile][spalte].ToString(), 1));
                    }
                    else if (alleZeilen[zeile][spalte] == '<')
                    {
                        feld[zeile, spalte] = "-";
                        positionen.Add(((spalte, zeile), alleZeilen[zeile][spalte].ToString(), 1));
                    }
                    else if (alleZeilen[zeile][spalte] == '^')
                    {
                        feld[zeile, spalte] = "|";
                        positionen.Add(((spalte, zeile), alleZeilen[zeile][spalte].ToString(), 1));
                    }
                    else
                    {
                        //feld[zeile, spalte] = " ";
                    }
                }
                else
                {
                    feld[zeile, spalte] = alleZeilen[zeile][spalte].ToString();
                }
            }
        }
        return feld;
    }
}