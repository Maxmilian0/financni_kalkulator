using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Program
{
    static string[] menuItems = { "Složené úročení", "Jednoduché úročení", "Výpočet DPH", "Výpočet slevy", "Měnové kurzy", "O programu", "Konec programu" };
    static string programVersion = "1.0.0";


    static void vymazatTerminal()
    {

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.Clear();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Console.Write("\u001b[2J");
            Console.Write("\u001b[H");
        }
    }
    static void udelejNadpis(string nadpisText = "chyba nadpisu")
    {

        vymazatTerminal();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\t{0}\n", nadpisText);
        Console.ForegroundColor = ConsoleColor.White;
    }

    static string urokovaciDobaMenu()
    { //Menu pro zvolení úrokovací doby

        int vyber;
        string[] obdobi = { "rok/let", "pololetí", "čtvrtletí", "měsíc/měsíce", "den/dny/dní" };

    neplatnyVyber:
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Úrokovací období jsou:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\t1)roční\n\t2)pololetní\n\t3)čtvrtletní\n\t4)měsíční\n\t5)denní");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\nVáš výběr: ");
        vyber = Convert.ToInt32(Console.ReadLine());
        if (vyber >= 1 && vyber <= 5)
        {
            return obdobi[vyber - 1];
        }
        else
        {
            vymazatTerminal();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Neplatné číslo, zvolte znovu. <Enter>");
            Console.ReadKey();
            goto neplatnyVyber;
        }
    }
    static void navratDoHlavnihoMenu()
    {

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\npro návrat ztiskněte tlačítko <Enter>");
        Console.ReadKey();
        manager();
    }
    static int hlavniMenu()
    { //Menu pro akce v programu

    errorRepair:
        vymazatTerminal();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t$$ FINANČNÍ KALKULAČKA V{0} $$\n\n", programVersion);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Menu:");

        for (int menuItemNubers = 0; menuItemNubers < menuItems.Length; menuItemNubers++)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("{0}) ", menuItemNubers + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(menuItems[menuItemNubers]);
        }
        Console.Write("\nVáš výběr: ");
        try
        {
            return Array.IndexOf(menuItems, menuItems[Convert.ToInt32(Console.ReadLine()) - 1]);
        }
        catch
        {
            vymazatTerminal();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Neplatné číslo, zvolte znovu. <Enter>");
            Console.ReadKey();
            goto errorRepair;
        }
    }

    static void slozeneUroceni()
    {

        double pocatecniCastka;
        double pocetUrokovObdobi;
        double urokovaSazba;
        double vysledek = 0;
        double uroky;
        string mena;
        string obdobi;

        /*
        Jednoduché úročení je takový způsob úročení,
        při kterém se úrok na konci každého úrokovacího
        období počítá z počátečního kapitálu.
       */
        udelejNadpis("Složené úročení");

        obdobi = urokovaciDobaMenu();

        Console.Write("Zadejte měnovou jednotku (např. kč, $, €)");
        mena = Console.ReadLine();
        Console.Write("zadejte počáteční kapitál (pouze číslo): ");
        pocatecniCastka = Convert.ToDouble(Console.ReadLine().Trim());
        Console.Write("zadejte počet úrokovacích období: ");
        pocetUrokovObdobi = Convert.ToDouble(Console.ReadLine());
        Console.Write("zadejte úrokovou sazbu (bez znaku '%'):");
        urokovaSazba = Convert.ToDouble(Console.ReadLine());
        Console.Write("zúročená částka: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        uroky = pocatecniCastka * (urokovaSazba / 100);
        for (int naviseniCastky = 0; naviseniCastky < pocetUrokovObdobi; naviseniCastky++)
        {

            vysledek = pocatecniCastka + uroky;
            uroky = uroky + vysledek * (urokovaSazba / 100);
        }

        Console.WriteLine("{0,21:N0}{1} za {2} {3}", vysledek, mena, pocetUrokovObdobi, obdobi);

        navratDoHlavnihoMenu();
    }

    static void jednoducheUroceni()
    {

        double pocatecniCastka;
        double pocetUrokovObdobi;
        double urokovaSazba;
        string mena;
        string obdobi;
        /*
        Jednoduché úročení je takový způsob úročení,
        při kterém se úrok na konci každého úrokovacího
        období počítá z počátečního kapitálu.
       */
        udelejNadpis("Jednoduché úročení");

        obdobi = urokovaciDobaMenu();

        Console.Write("Zadejte měnovou jednotku (např. kč, $, €)");
        mena = Console.ReadLine();
        Console.Write("zadejte počáteční kapitál (pouze číslo): ");
        pocatecniCastka = Convert.ToInt32(Console.ReadLine().Trim());
        Console.Write("zadejte počet úrokovacích období: ");
        pocetUrokovObdobi = Convert.ToInt32(Console.ReadLine());
        Console.Write("zadejte úrokovou sazbu (bez znaku '%'):");
        urokovaSazba = Convert.ToInt32(Console.ReadLine());
        Console.Write("zúročená částka: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("{0,21:N0}{1} za {2} {3}", pocatecniCastka + pocetUrokovObdobi * urokovaSazba / 100 * pocatecniCastka, mena, pocetUrokovObdobi, obdobi);

        navratDoHlavnihoMenu();
    }

    static void vypocetDPH()
    {

        double vyseDPH;
        double pocatecniCastka;
        string menaVypocetSlevy;
        double vysledekVypocetDPH;

        udelejNadpis("Výpočet DPH");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("? ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("u čísel s desetinou čárku použijte znak ',' a ne '.'. Např. (3,14; 17,5 atd.)");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Zadejte měnovou jednotku (např. kč, $, €)");
        menaVypocetSlevy = Console.ReadLine();
        Console.Write("Zadejte cenu před zdaněním: ");
        pocatecniCastka = Convert.ToDouble(Console.ReadLine().Trim());
        Console.Write("Zadejte výši daně (bez znaku '%'): ");
        vyseDPH = Convert.ToDouble(Console.ReadLine().Trim());
        vysledekVypocetDPH = pocatecniCastka * (1 + vyseDPH / 100);
        Console.WriteLine("Cena po zdanění: {0,21:N0}{1}", vysledekVypocetDPH, menaVypocetSlevy);

        navratDoHlavnihoMenu();
    }

    static void vypocetSlevy()
    {

        double slevaVypocetSlevy;
        double cenaVypocetSlevy;
        double vysledekVypocetSlevy;
        string menaVypocetSlevy;

        udelejNadpis("Výpočet slevy");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("? ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("u čísel s desetinou čárku použijte znak ',' a ne '.'. Např. (3,14; 17,5 atd.)");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Zadejte měnovou jednotku (např. kč, $, €)");
        menaVypocetSlevy = Console.ReadLine();
        Console.Write("Zadejte cenu před slevou: ");
        cenaVypocetSlevy = Convert.ToDouble(Console.ReadLine().Trim());
        Console.Write("Zadejte slevu (bez znaku %): ");
        slevaVypocetSlevy = Convert.ToDouble(Console.ReadLine().Trim());
        vysledekVypocetSlevy = (1 - slevaVypocetSlevy / 100) * cenaVypocetSlevy;
        Console.WriteLine("Cena po slevě: {0,21:N0}{1}", vysledekVypocetSlevy, menaVypocetSlevy);

        navratDoHlavnihoMenu();
    }

    static void menoveKurzy()
    {

        using (Process process = new Process())
        {
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "https://www.kurzy.cz/kurzy-men";
            process.Start();
        }
        manager();
    }

    static void oProgramu()
    {

        udelejNadpis("O programu");
        Console.WriteLine("verze programu {0}", programVersion);
        Console.WriteLine("vývojář: Maxmilián Moravec <maxmilianmoravec1@gmail.com>");
        Console.WriteLine("datum  vzniku: 2024");
        Console.WriteLine("\n\t\u00A92024 všechna práva vyhrazena");

        navratDoHlavnihoMenu();
    }
    static void konecProgramu()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Opravdu? a/n: ");
        if (Console.ReadLine() == "a")
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Naschledanou!");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }
        else
        {
            manager();
        }
    }

    static void manager()
    { //Hlavní řídící funkce, propojuje menu s jednotlivímy funkcemi

        vymazatTerminal();
        switch (menuItems[hlavniMenu()])
        {
            case "Složené úročení":
                slozeneUroceni();
                break;
            case "Jednoduché úročení":
                jednoducheUroceni();
                break;
            case "Výpočet DPH":
                vypocetDPH();
                break;
            case "Výpočet slevy":
                vypocetSlevy();
                break;
            case "Měnové kurzy":
                menoveKurzy();
                break;
            case "O programu":
                oProgramu();
                break;
            case "Konec programu":
                konecProgramu();
                break;
            default:
                vymazatTerminal();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Na této funkci se ještě pracuje. Prosím, vyzkoušejte jinou. <enter>");
                Console.ReadKey();
                manager();
                break;
        }
    }

    static void Main()
    {
        //Program START
        Console.Title = "Finanční aplikace  V.1.0.0";
        manager();
    }
}
