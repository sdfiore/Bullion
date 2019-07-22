using System.Runtime.Serialization;

namespace BullionLibrary
{
    [DataContract]
    public class Account
    {
        #region C# Properties

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "savings")]
        public double Savings { get; set; }

        [DataMember(Name = "cash")]
        public double Cash { get; set; }

        [DataMember(Name = "crypto")]
        public double Crypto { get; set; }

        #endregion

        #region Methods
        public Account()
        {
            Id = 0;
            Savings = 0.0;
            Cash = 0.0;
            Crypto = 0.0;
        }

        #endregion
    }
}
