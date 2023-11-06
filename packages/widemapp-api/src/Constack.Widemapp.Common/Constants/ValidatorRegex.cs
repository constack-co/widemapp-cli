namespace Constack.Widemapp.Common.Constants
{
    public class ValidatorRegex
    {
        public const string Email = @"^(?=[a-zA-Z0-9@._%+-]{6,254}$)[a-zA-Z0-9._%+-]{1,64}@(?:[a-zA-Z0-9-]{1,63}\.){1,8}[a-zA-Z]{2,63}$";
        public const string UserName = @"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$";
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$";
        public const string Time = @"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$";
    }
}
