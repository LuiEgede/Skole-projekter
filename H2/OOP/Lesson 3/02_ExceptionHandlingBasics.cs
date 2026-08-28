// ============================================================================
// 02_ExceptionHandlingBasics.cs
//
// Formål: Vise grundlæggende exception handling: try/catch/finally, flere
// catch-blokke i rigtig rækkefølge, almindelige indbyggede exceptions,
// at kaste sine egne exceptions med throw, og en selvlavet exception-klasse.
//
// OBS: Denne fil er tænkt som et selvstændigt eksempel til gennemgang.
// Opret et nyt konsolprojekt og indsæt hele filens indhold i Program.cs
// (eller kald Kør() fra dit eget Main), hvis du vil køre den for dig selv.
// ============================================================================

using System;

namespace Lesson03Examples
{
    // ------------------------------------------------------------------
    // Egen exception-klasse. Navngivningskonvention i C#: navnet slutter
    // altid på "Exception". Klassen arver fra System.Exception, så den
    // "er en" exception og kan bruges alle steder, en exception kan.
    // ------------------------------------------------------------------
    public class InvalidAccountBalanceException : Exception
    {
        // Ekstra data ud over selve fejlbeskeden - her det beløb,
        // brugeren forsøgte at hæve, og den faktiske saldo på kontoen.
        public decimal AttemptedWithdrawal { get; }
        public decimal CurrentBalance { get; }

        public InvalidAccountBalanceException(decimal attemptedWithdrawal, decimal currentBalance)
            : base($"Kan ikke hæve {attemptedWithdrawal:C} - der er kun {currentBalance:C} på kontoen.")
            // base(...) sender den formaterede besked videre til Exception-klassens
            // egen konstruktør, så "Message"-property'en bliver sat korrekt.
        {
            AttemptedWithdrawal = attemptedWithdrawal;
            CurrentBalance = currentBalance;
        }
    }

    // ------------------------------------------------------------------
    // En lille bankkonto-klasse, der bruges til at demonstrere throw.
    // ------------------------------------------------------------------
    public class BankAccount
    {
        public decimal Balance { get; private set; }

        public BankAccount(decimal startingBalance)
        {
            Balance = startingBalance;
        }

        public void Withdraw(decimal amount)
        {
            // Indbygget exception-type genbruges, når den passer præcist til fejlen:
            // "en parameter er ugyldig for denne operation".
            if (amount <= 0)
            {
                throw new ArgumentException("Beløbet, der hæves, skal være positivt.");
            }

            // Her passer ingen indbygget type lige så godt som vores egen -
            // vi kaster derfor vores egen, mere specifikke exception-type.
            if (amount > Balance)
            {
                throw new InvalidAccountBalanceException(amount, Balance);
            }

            Balance -= amount;
        }
    }

    public class ExceptionHandlingBasics
    {
        public static void Main(string[] args)
        {
            ShowTryCatchFinally();
            Console.WriteLine();

            ShowMultipleExceptionTypes();
            Console.WriteLine();

            ShowCustomException();
        }

        // -------------------------------------------------------------
        // Grundstrukturen: try, catch og finally.
        // finally kører ALTID - uanset om der opstod en fejl eller ej.
        // -------------------------------------------------------------
        private static void ShowTryCatchFinally()
        {
            Console.WriteLine("--- try/catch/finally ---");

            Console.Write("Indtast din alder (prøv fx at skrive 'abe'): ");
            string input = Console.ReadLine();

            try
            {
                int age = int.Parse(input); // Kaster FormatException, hvis input ikke er et helt tal
                Console.WriteLine($"Du er {age} år.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Det var ikke et gyldigt tal.");
            }
            finally
            {
                // Kører uanset udfald - typisk brugt til oprydning
                // (fx lukke en fil eller database-forbindelse).
                Console.WriteLine("Forsøget på at læse alderen er afsluttet.");
            }
        }

        // -------------------------------------------------------------
        // Flere catch-blokke: den mest specifikke type skal stå FØRST,
        // og en generel "Exception" (opsamlingsblok) sidst.
        // -------------------------------------------------------------
        private static void ShowMultipleExceptionTypes()
        {
            Console.WriteLine("--- Flere catch-blokke i rigtig rækkefølge ---");

            int[] numbers = { 10, 20, 30 };

            Console.Write("Indtast et indeks i arrayet [0, 1, 2] (prøv fx 9 eller 'x'): ");
            string input = Console.ReadLine();

            try
            {
                int index = int.Parse(input);        // Kan kaste FormatException
                int divisor = numbers[index] - 30;    // Regnet så divisor bliver 0 ved indeks 2
                int result = 100 / divisor;           // Kan kaste DivideByZeroException
                Console.WriteLine($"Resultat: {result}");
            }
            catch (FormatException)
            {
                // Mest specifik: forkert format på selve input-teksten.
                Console.WriteLine("Du skal indtaste et helt tal.");
            }
            catch (IndexOutOfRangeException)
            {
                // Mere specifik end "Exception": indekset findes ikke i arrayet.
                Console.WriteLine("Det indeks findes ikke i arrayet - prøv 0, 1 eller 2.");
            }
            catch (DivideByZeroException)
            {
                // Mere specifik end "Exception": division med nul.
                Console.WriteLine("Den beregning gav division med nul.");
            }
            catch (Exception ex)
            {
                // Opsamlingsblok - fanger alt andet, vi ikke havde forudset.
                // Skal altid stå sidst, ellers giver C# en kompileringsfejl.
                Console.WriteLine($"Der opstod en uventet fejl: {ex.Message}");
            }
        }

        // -------------------------------------------------------------
        // At fange og bruge en selvlavet exception-klasse med ekstra data.
        // -------------------------------------------------------------
        private static void ShowCustomException()
        {
            Console.WriteLine("--- Egen exception-klasse ---");

            BankAccount account = new BankAccount(500m);

            try
            {
                account.Withdraw(1500m); // Vil fejle, da saldoen kun er 500 kr.
            }
            catch (InvalidAccountBalanceException ex)
            {
                // ex.Message er den formaterede besked fra base(...) i konstruktøren.
                Console.WriteLine(ex.Message);

                // De ekstra properties giver adgang til strukturerede data om fejlen,
                // som en almindelig fejlbesked ikke ville kunne give os.
                Console.WriteLine($"Differencen op til det ønskede beløb er {ex.AttemptedWithdrawal - ex.CurrentBalance:C}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ugyldigt beløb: {ex.Message}");
            }

            Console.WriteLine($"Kontoens saldo er stadig {account.Balance:C} - hævningen blev ikke gennemført.");
        }
    }
}
