namespace Zadanie5.Helpers;

public class PeselValidator
{
    public (short birthYear, short gender) ParsePesel(string pesel)
    {
        if (string.IsNullOrEmpty(pesel) || pesel.Length != 11)
            throw new ArgumentException("PESEL musi mieć dokładnie 11 znaków");

        var year = int.Parse(pesel.Substring(0, 2));
        var month = int.Parse(pesel.Substring(2, 2));
        var genderDigit = int.Parse(pesel.Substring(9, 1));
        
        short birthYear;
        if (month >= 1 && month <= 12)
            birthYear = (short)(1900 + year);
        else if (month >= 21 && month <= 32)
            birthYear = (short)(2000 + year);
        else if (month >= 41 && month <= 52)
            birthYear = (short)(2100 + year);
        else if (month >= 61 && month <= 72)
            birthYear = (short)(2200 + year);
        else if (month >= 81 && month <= 92)
            birthYear = (short)(1800 + year);
        else
            throw new ArgumentException("Nieprawidłowy miesiąc w PESEL");
        
        if (birthYear > DateTime.Now.Year)
            throw new ArgumentException("Rok urodzenia nie może być w przyszłości");
        
        short gender = (short)(genderDigit % 2);

        return (birthYear, gender);
    }

}