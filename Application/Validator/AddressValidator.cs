using System.Text.RegularExpressions;

namespace Application.Validator
{
    public static class AddressValidator
    {
        public static bool InputIsValid(this string input) => new Regex(@"^[0-9]*$").IsMatch(input) && input.Length == 8;
    }
}
