namespace Zadanie5.Services.Services;

public interface IPeselValidator
{
    (short birthYear, short gender) ParsePesel(string pesel);
}