
namespace Archives
{
    public static class Enums
    {
        /// <summary>
        /// Male,
        /// Female,
        /// Neutral
        /// </summary>
        public enum Gender
        {
            Male,
            Female,
            Neutral
        }
        public static Gender Multiply(Gender primaryGender, Gender secondaryGender)
        {
            // same
            if (primaryGender == secondaryGender)
                return primaryGender;
            // one is neutral
            if (primaryGender == Gender.Neutral)
                return secondaryGender;
            if (secondaryGender == Gender.Neutral)
                return primaryGender;
            // both are different non neutral
            return primaryGender;
        }

        /// <summary>
        /// Name,
        /// Occupation,
        /// Character,
        /// Height,
        /// Physique,
        /// Skin,
        /// Hair,
        /// Face,
        /// Eyes,
        /// Clothes,
        /// Features
        /// </summary>
        public enum BundleType
        {
            Name,
            Occupation,
            Character,
            Height,
            Physique,
            Skin,
            Hair,
            Face,
            Eyes,
            Clothes,
            Features
        }
    }
}
