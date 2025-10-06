namespace Zadanie5.Application.Interfaces;

public interface IPeselValidator
{
    (short birthYear, short gender) ParsePesel(string pesel);
}