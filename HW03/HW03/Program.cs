using System.Text;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var User = AskUserForData();
        ShowUserInfo(User);
    }

    static (string FName, string LName, int Age, string[] PetNames, string[] FavColors) AskUserForData()
    {
        (string FName, string LName, int Age, string[] PetNames, string[] FavColors) User;
        User.FName = "";
        User.LName = "";
        User.Age = 0;
        User.PetNames = new string[0];
        User.FavColors = new string[0];

        Console.Write(
            "Доброго времени суток! \n" +
            "Далее мы попросим Вас ввести свои данные. \n\n" +
            "Имя: "
        );
        User.FName = Console.ReadLine();

        Console.Write("Фамилия: ");
        User.LName = Console.ReadLine();

        // We'll need these temporary variables later
        bool gotFirstInput = false;
        string input;

        Console.Write("Возраст: ");
        do {
            if (gotFirstInput) {
                Console.Write("Пожалуйста введите положительное число (возраст): ");
            }
            input = Console.ReadLine();
            gotFirstInput = true;
        } while (!CheckNumberInput(input, out User.Age));

        // Couldn't figure out how to read ciryllic input
        // Using console only (without StreamReader)
        Console.Write("Есть ли у Вас питомцы (yes/no)? ");
        gotFirstInput = false;
        bool hasPets = false;
        do
        {
            if (gotFirstInput)
            {
                Console.Write("Пожалуйста введите 'yes' или 'no' (наличие питомцев): ");
            }
            input = Console.ReadLine();
            if (input != null) {
                input = input.ToLower();
            }
            if (input == "yes") {
                // Check for "нет" is redundant here
                // Because loop will end only after "да" or "нет" typed
                hasPets = true;
            }
            gotFirstInput = true;
        } while (input != "yes" && input != "no");

        if (hasPets) {
            int petsCount;
            gotFirstInput = false;
            Console.Write("Сколько их у Вас? ");
            do
            {
                if (gotFirstInput)
                {
                    Console.Write("Пожалуйста введите положительное число (кол-во питомцев): ");
                }
                input = Console.ReadLine();
                gotFirstInput = true;
            } while (!CheckNumberInput(input, out petsCount));
            
            User.PetNames = new string[petsCount];

            Console.WriteLine("Пожалуйста сообщите нам их имена:");
            for (int i = 0; i < petsCount; i++) {
                Console.Write("Кличка питомца №{0} - ", i+1);
                User.PetNames[i] = Console.ReadLine();
            }
        }
        
        Console.Write("Сколько у Вас любимых цветов? ");
        int favColorsCount;
        gotFirstInput = false;
        do
        {
            if (gotFirstInput)
            {
                Console.Write("Пожалуйста введите положительное число (кол-во цветов): ");
            }
            input = Console.ReadLine();
            gotFirstInput = true;
        } while (!CheckNumberInput(input, out favColorsCount));

        User.FavColors = new string[favColorsCount];

        Console.WriteLine("Пожалуйста сообщите нам ваши любимые цвета:");
        for (int i = 0; i < favColorsCount; i++)
        {
            Console.Write("Цвет №{0} - ", i + 1);
            User.FavColors[i] = Console.ReadLine();
        }

        return User;
    }

    static void ShowUserInfo((string FName, string LName, int Age, string[] PetNames, string[] FavColors) User)
    {
        string PetNames = "Клички питомцев: ";
        string FavColors = "Любимые цвета: ";

        if (User.PetNames.Length > 0) {
            for (int i = 0; i < User.PetNames.Length; i++) {
                PetNames += User.PetNames[i];
                PetNames += i < User.PetNames.Length - 1 ? ", " : ".";
            }
        } else {
            PetNames = "Питомцы отсутствуют";
        }

        for (int i = 0; i < User.FavColors.Length; i++)
        {
            FavColors += User.FavColors[i];
            FavColors += i < User.FavColors.Length - 1 ? ", " : ".";
        }

        Console.WriteLine("------------------------------");

        Console.WriteLine(
            "Ваша Анкета: \n" +
            "\tИмя: {0}\n" +
            "\tФамилия: {1}\n" +
            "\tВозрaст: {2}\n" +
            "\t{3}\n" +
            "\t{4}"
            , User.FName, User.LName, User.Age, PetNames, FavColors);
        
        Console.WriteLine("------------------------------");
    }

    static bool CheckNumberInput(string input, out int number)
    {
        bool isNumber = int.TryParse(input, out int parsedInt);
        if (isNumber && parsedInt > 0)
        {
            number = parsedInt;
            return true;
        }
        number = 0;
        return false;
    }
}