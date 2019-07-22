namespace Bullion.util
{
    public class DataValidation
    {
        // Checks if a double is greater than zero.
        public bool ValidateDouble(double value)
        {
            if (value > 0) { return true; }
            return false;
        }

        // Checks if a string is null.
        public bool ValidateString(string value)
        {
            if (value == "") { return true; }
            return false;
        }

        // Checks if a string contains a space.
        public bool ValidateSpace(string value)
        {
            if (value.Contains(" ")) { return true; }
            return false;
        }
    }
}
