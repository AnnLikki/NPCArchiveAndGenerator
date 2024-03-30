﻿namespace Archives
{
    /// <summary>
    /// A part of a Weighted Archive. 
    /// Can represent any object.
    /// </summary>
    public class WeightedElement
    {
        /// <summary>
        /// Whatever is this weighted element represents.
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// Determines the likelyhood of this element getting picked randomly. 
        /// The higher the weight - the better.
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Option to set the element to be gendered. 
        /// If it is, it only can be picked for a character with the same gender.
        /// </summary>
        public Gender Gender { get; set; }

        public WeightedElement(object value, int weight = 1, Gender gender = Gender.Neutral)
        {
            Value = value;
            Gender = gender;
            Weight = weight;
        }
    }
}
