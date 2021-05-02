using System;

namespace Listy
{
    /// <summary>
    /// <para>Statyczna klasa, która zawiera metody pokazujące rózne interaktywne listy, z których użytkownik może wybierać elementy</para>
    /// <para>Sterowanie w liście odbywa się za pomocą WSAD lub strzałek, natomiast potwierdzanie miejsce ENTERem</para>
    /// </summary>
    public abstract class Listy
    {
        /// <value>Błąd, który się pokazuje, gdy nasza tabela nie ma zawartości (ma długość 0)</value>
        public static Exception TableError = new Exception("Podana tabela jest pusta.");
        /// <summary>
        /// Rysuje w konsoli wybraną tabelę wraz z cyframi świadczącymi o kolejności danych pól
        /// </summary>
        /// <param name="table"></param>
        /// <param name="color"></param>
        private static void TableDraw(string[] table, int color)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (i == color)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i + 1}. {table[i]}");
                    Console.ResetColor();
                }
                else
                    Console.WriteLine($"{i + 1}. {table[i]}");
            }
        }

        /// <summary>
        /// Interaktywna lista numerowana pozwalająca wybrać użytkownikowi dane pole ze wskazanej tabeli
        /// </summary>
        /// <param name="Wybory">Tablica, która będzie wyświetlona użytkownikowi z odpowiednio zaznaczonym przez niego polem</param>
        /// <param name="zapetlanieNaKoncach">Dla TRUE pozwala zjechać na sam koniec listy z jego początku jadąc do góry oraz na sam początek z końca jadąc w dół</param>
        /// <param name="dodatkowyAkapit">Ile dodatkowych liń ma być przed pokazaniem tabeli</param>
        /// <exception cref="TableError">Wyrzucane, gdy tabela z wyborami jest pusta</exception>
        /// <returns>Liczbę całkowitą odpowiadającą wybranemu przedmiotowi z tabeli (od 0 do długości-1)</returns>
        public static int ListaPojedyncza(string[] Wybory, bool zapetlanieNaKoncach = false, int dodatkowyAkapit = 0)
        {
            if (Wybory.Length > 1)
            {


                if (dodatkowyAkapit < 0)
                {
                    dodatkowyAkapit = 0;
                }

                bool petla = zapetlanieNaKoncach;

                int Top = Console.CursorTop + dodatkowyAkapit;
                int Miejsce = 0;
                int l = Wybory.Length - 1;

                Console.CursorVisible = false;
                bool exit = false;
                while (!exit)
                {
                    Console.SetCursorPosition(0, Top);
                    TableDraw(Wybory, Miejsce);
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Enter)
                    {
                        exit = true;
                    }
                    else if ((key == ConsoleKey.S || key == ConsoleKey.DownArrow) && Miejsce < l)
                    {
                        Miejsce++;
                    }
                    else if ((key == ConsoleKey.W || key == ConsoleKey.UpArrow) && Miejsce > 0)
                    {
                        Miejsce--;
                    }
                    else if ((key == ConsoleKey.S || key == ConsoleKey.DownArrow) && Miejsce == l && petla == true)
                    {
                        Miejsce = 0;
                    }
                    else if ((key == ConsoleKey.W || key == ConsoleKey.UpArrow) && Miejsce == 0 && petla == true)
                    {
                        Miejsce = l;
                    }
                }
                Console.CursorVisible = true;
                return Miejsce;
            }
            else if (Wybory.Length == 1)
            {
                return 0;
            }
            else
                throw TableError;
        }


        /// <summary>
        /// <para>Podwójna lista numerowana wyświetlająca z tabeli tabel (jagged array[][]) pierwsze wyrazy z każdej (o indeksie 0).
        /// Po zatwierdzeniu jakieś opcji pokazuje resztę wyrazów znajdujących się w danej tabeli (tabeli o wybranym przez użytkownika indeksie).</para>
        /// <para>Sterowanie odbywa się z pomocą 'W' i 'S' (lub strzałek góra/dół). Zatwierdza się wybór ENTEREM. W drugiej liście
        /// steruje się 'A' i 'D' (lub strzałki lewo/prawo), zatwierdza się ENTERem, a anuluje wybór BACKSPACEem.</para>
        /// </summary>
        /// <param name="Wybory">Tablica tabel, których pierwsze elementy (dalej Tematy) zostaną wyświetlone użutkownikowi, a po potwierdzeniu (guzikiem ENTER)
        /// zostaną wyświetlone pozostałe elementy z tej tabeli, której Temat wybrał użytkownik.</param>
        /// <param name="zapetlanieNaKoncach">Dla TRUE pozwala zjechać na sam koniec listy z jego początku jadąc do góry oraz na sam początek z końca jadąc w dół</param>
        /// <param name="dodatkowyAkapit">Ile dodatkowych liń ma być przed pokazaniem tabeli</param>
        /// <returns>Ciąg znaków odpowiadającym wybranym przez użytwkonika indeksom miejsc w tabeli, z czego pierwszym jest indeks Tematu 
        /// (od 0 do ilości tematów) + indeks wybranego przedmiotu (od 1 od ilości przedmiotów).
        /// W przypadku wprowadzenia tabeli tylko z tematem (czyli o długości 1) zostanie zwrócony indeks tematu + "0"</returns>
        ///  <remarks>Np: <c>tab[...] = new string[] {"Temat tabeli", "Przedmiot1", "Przedmiot2",...};</c></remarks>
        public static string ListaPodwojna(string[][] Wybory, bool zapetlanieNaKoncach = false, int dodatkowyAkapit = 0)
        {
            if (Wybory.GetLength(0) == 0)
            {
                throw Listy.TableError;
            }
            else
            {
                int Top = Console.CursorTop + dodatkowyAkapit;

                bool p1 = true;
                Console.CursorVisible = false;
                int miejsce = 1;
                int choice = 0;
                string[] TitlesValues;
                if (Wybory.GetLength(0) == 1 && Wybory[0].Length != 0)
                {
                    //TitlesValues = Wybory[0];
                    string[][] W2 = new string[Wybory[0].Length - 1][];
                    for (int i = 0; i < Wybory[0].Length; i++)
                    {
                        W2[i] = Wybory[0][i];
                    }
                    Wybory = = new string[Wybory[0].Length - 1][];
                    Wybory = W2;
                }
                else if (Wybory.GetLength(0) == 1 && Wybory[0].Length == 0)
                    throw Listy.TableError;
                else
                    TitlesValues = GetFirstValues(Wybory);

                while (p1)
                {
                    for (int i = 0; i < TitlesValues.Length; i++)
                    {
                        Console.SetCursorPosition(0, Top + i);
                        Console.Write(new string(' ', Console.WindowWidth));
                    }
                    Console.SetCursorPosition(0, Top);
                    choice = ListaPojedyncza(TitlesValues, zapetlanieNaKoncach);
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, Top);

                    for (int i = 0; i < TitlesValues.Length; i++)
                    {
                        if (i == choice && Wybory[i].Length > 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write($"{i + 1}. {TitlesValues[i]}\t");
                            Console.ResetColor();


                            int Left = Console.CursorLeft;
                            int Top2 = Console.CursorTop;
                            int l = Wybory[i].Length - 1;

                            bool p2 = true;
                            while (p2)
                            {
                                Console.SetCursorPosition(Left, Top2);

                                for (int j = 1; j < Wybory[i].Length; j++)
                                {
                                    if (j == miejsce)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write($"{j}. {Wybory[i][j]}\t");
                                        Console.ResetColor();

                                    }
                                    else
                                        Console.Write($"{j}. {Wybory[i][j]}\t");
                                }


                                var key = Console.ReadKey(true).Key;
                                if ((key == ConsoleKey.D || key == ConsoleKey.RightArrow) && miejsce < l)
                                {
                                    miejsce++;
                                }
                                else if ((key == ConsoleKey.A || key == ConsoleKey.LeftArrow) && miejsce > 1)
                                {
                                    miejsce--;
                                }
                                else if (key == ConsoleKey.Enter)
                                {
                                    p1 = false; p2 = false;
                                }
                                else if (key == ConsoleKey.Backspace)
                                {
                                    p2 = false;
                                    miejsce = 1;
                                }

                            }
                        }
                        else if (i == choice)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write($"{i + 1}. {TitlesValues[i]}");
                            Console.ResetColor();
                            miejsce = 0;
                            p1 = false;
                        }
                        Console.WriteLine();
                    }



                }

                return choice.ToString() + miejsce.ToString();
            }
        }
        private static string[] GetFirstValues(string[][] tab)
        {
            string[] ret = new string[tab.GetLength(0)];
            for (int i = 0; i < tab.GetLength(0); i++)
            {

                ret[i] = tab[i][0];
            }
            return ret;
        }
    }
}
