namespace Utilities
{
    public class PeopleGenerator
    {
        static Random random = new Random();
        static List<char> chars = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };
        static int randomIndex;
        static char randomChar;

        public static char RandomChar()
        {
            randomIndex = random.Next(chars.Count);
            return chars[randomIndex];
        }

        public static string GenerateDocument()
        {
            string Document = null;
            for (int i = 0; i < 14; i++)
                Document += random.Next(10);
            return Document;
        }
        public static string GenerateName()
        {
            string name = null;
            for (int i = 0; i < 30; i++)
                name += RandomChar();
            return name;
        }
        public static DateOnly GenerateBirth()
        {
            string birth = null;
            for (int i = 0; i < 8; i++)
            {
                if (i == 2 || i == 5)
                {
                    birth += "/";
                }
                birth += random.Next(10);
            }
            return DateOnly.Parse(birth);
        }
        public static string GenerateTelephone()
        {
            string telephone = null;
            for (int i = 0; i < 9; i++)
                telephone += random.Next(10);
            return telephone;
        }
        public static string GenerateEmail()
        {
            string email = null;
            for (int i = 0; i < 20; i++)
                email += RandomChar();
            return email += "@email.com";
        }

    }
}

