using Archives;
using NUnit.Framework;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Tests
{
    internal class BundleTesting
    {
        // Testing bundles and layers
        // Weighted element doesn't need testing
        // Getting and checking data, randomization and statistics
        // Testing edge cases

        [SetUp]
        public void Setup()
        {

        }

        // Layer testing

        [Test]
        public void BasicLayerManipulation()
        {
            Layer layer = new Layer();

            // Adding
            WeightedElement we1 = new WeightedElement("Element1");
            layer.Add(we1);
            layer.Add("Element2", 5, Enums.Gender.Male);
            Assert.That(layer.Count == 2);

            // Getting
            Assert.That(layer.Get(0), Is.EqualTo(we1));
            WeightedElement we2 = layer.Get(1);
            Assert.That(we2.Value.Equals("Element2") && we2.Weight == 5 && we2.Gender == Enums.Gender.Male);

            // Getting by value
            WeightedElement we1_1 = layer.GetByValue("Element1");
            Assert.That(we1, Is.EqualTo(we1_1));
            WeightedElement we2_1 = layer.GetByValue("Element2");
            Assert.That(we2, Is.EqualTo(we2_1));

            // Removing
            layer.Remove(we1);
            Assert.That(layer.Count == 1);
            Assert.That(layer.Get(0), Is.EqualTo(we2));

            // Removing by value
            layer.Add(we1);
            Assert.That(layer.Count == 2);
            layer.RemoveByValue("Element1");
            Assert.That(layer.Count == 1);
            Assert.That(layer.Get(0), Is.EqualTo(we2));

            // Removing by index
            layer.Add(we1);
            Assert.That(layer.Count == 2);
            layer.RemoveAt(0);
            Assert.That(layer.Count == 1);
            Assert.That(layer.Get(0), Is.EqualTo(we1));

            // Clearing
            layer.Add(we2);
            Assert.That(layer.Count == 2);
            layer.Clear();
            Assert.That(layer.Count == 0);

        }

        [Test]
        public void LayerBasicRandomization()
        {
            Layer layer = new Layer();

            layer.Add("E1");
            layer.Add("E2");

            int e1s = 0, e2s = 0;
            int testsNumber = 10000;
            for (int i = 0; i < testsNumber; i++)
            {
                string result = layer.GetRandom();

                if (result == "E1")
                    e1s++;
                else if (result == "E2")
                    e2s++;
                else throw new Exception("Unexpected result");
            }

            Assert.That(((double)e1s) / testsNumber >= 0.45 && ((double)e1s) / testsNumber <= 0.55);
            Assert.That(((double)e2s) / testsNumber >= 0.45 && ((double)e2s) / testsNumber <= 0.55);

        }

        [Test]
        public void LayerRandomizationWithZeroWeights()
        {
            Layer layer = new Layer();

            layer.Add("E0", 0);
            layer.Add("E1");
            layer.Add("E2", 0);
            layer.Add("E3");
            layer.Add("E4", 0);

            int e0s = 0, e1s = 0, e2s = 0, e3s = 0, e4s = 0;
            int testsNumber = 10000;
            for (int i = 0; i < testsNumber; i++)
            {
                string result = layer.GetRandom();

                if (result == "E0")
                    e0s++;
                else if (result == "E1")
                    e1s++;
                else if (result == "E2")
                    e2s++;
                else if (result == "E3")
                    e3s++;
                else if (result == "E4")
                    e4s++;
                else throw new Exception("Unexpected result");
            }

            Assert.That(e0s == 0);
            Assert.That(((double)e1s) / testsNumber >= 0.45 && ((double)e1s) / testsNumber <= 0.55);
            Assert.That(e2s == 0);
            Assert.That(((double)e3s) / testsNumber >= 0.45 && ((double)e3s) / testsNumber <= 0.55);
            Assert.That(e4s == 0);

        }


        // Getting unrestricted randomized weighted results from the layer
        // Checking statistics to see how they correspond with expectations
        [Test]
        public void LayerUnrestrictedRandomization()
        {
            string[] es = { "E0", "E1", "E2", "E3", "E4", "E5" };

            Layer layer = new Layer();

            layer.Add(es[0], 5);
            layer.Add(es[1], 10);
            layer.Add(es[2], 1);
            layer.Add(es[3], 0);
            layer.Add(es[4], 5);
            layer.Add(es[5], 5);

            int[] stats = new int[6];

            int testsNumber = 10000;

            // counting occurances
            for (int i = 0; i < testsNumber; i++)
            {
                string result = layer.GetRandom();

                for (int j = 0; j < es.Length; j++)
                    if (result == es[j])
                    {
                        stats[j]++;
                        break;
                    }
            }

            // sum of weights
            int sum = 5 + 10 + 1 + 0 + 5 + 5;

            double[] chance = { 5.0 / sum,
                10.0 / sum,
            1.0 / sum,
            0.0 / sum,
             5.0 / sum,
            5.0 / sum };

            // checking that each chance is within 3% range of expected probability
            for (int j = 0; j < stats.Length; j++)
            {
                double actual = ((double)stats[j]) / testsNumber;
                Assert.That(actual >= chance[j] - 0.03 && actual <= chance[j] + 0.03);
            }

        }

        // Getting randomized weighted and gendered results from the layer
        // Checking statistics to see how they correspond with expectations
        // Neutral request can only get a neutral result
        // Gendered requests can get a neutral result or a result of the same gender
        [Test]
        public void LayerGenderedRandomization()
        {
            string[] es = { "E0", "E1", "E2", "E3", "E4", "E5" };

            Layer layer = new Layer();

            layer.Add(es[0], 5);
            layer.Add(es[1], 10);
            layer.Add(es[2], 1);
            layer.Add(es[3], 0);
            layer.Add(es[4], 5, Enums.Gender.Male);
            layer.Add(es[5], 5, Enums.Gender.Female);

            int[] statsMale = new int[6];
            int[] statsFemale = new int[6];
            int[] statsNeutral = new int[6];


            int testsNumber = 10000;

            // counting occurances
            for (int i = 0; i < testsNumber; i++)
            {
                string result = layer.GetRandom(Enums.Gender.Male);

                for (int j = 0; j < es.Length; j++)
                    if (result == es[j])
                    {
                        statsMale[j]++;
                        break;
                    }

                result = layer.GetRandom(Enums.Gender.Female);

                for (int j = 0; j < es.Length; j++)
                    if (result == es[j])
                    {
                        statsFemale[j]++;
                        break;
                    }

                result = layer.GetRandom(Enums.Gender.Neutral);

                for (int j = 0; j < es.Length; j++)
                    if (result == es[j])
                    {
                        statsNeutral[j]++;
                        break;
                    }
            }

            // chances

            int sumMale = 5 + 10 + 1 + 0 + 5 + 0;
            double[] chanceMale = { 5.0 / sumMale, 10.0 / sumMale, 1.0 / sumMale, 0.0 / sumMale, 5.0 / sumMale, 0.0 / sumMale };

            int sumFemale = 5 + 10 + 1 + 0 + 0 + 5;
            double[] chanceFemale = { 5.0 / sumFemale, 10.0 / sumFemale, 1.0 / sumFemale, 0.0 / sumFemale, 0.0 / sumFemale, 5.0 / sumFemale };

            int sumNeutral = 5 + 10 + 1 + 0 + 0 + 0;
            double[] chanceNeutral = { 5.0 / sumNeutral, 10.0 / sumNeutral, 1.0 / sumNeutral, 0.0 / sumNeutral, 0.0 / sumNeutral, 0.0 / sumNeutral };

            // checking that each chance is within 5% range of expected probability
            for (int j = 0; j < statsMale.Length; j++)
            {
                double actual = ((double)statsMale[j]) / testsNumber;
                //Console.WriteLine(actual+" "+chanceMale[j]);
                Assert.That(actual >= chanceMale[j] - 0.05 && actual <= chanceMale[j] + 0.05);
            }
            for (int j = 0; j < statsFemale.Length; j++)
            {
                double actual = ((double)statsFemale[j]) / testsNumber;
                //Console.WriteLine(actual + " " + chanceFemale[j]);
                Assert.That(actual >= chanceFemale[j] - 0.05 && actual <= chanceFemale[j] + 0.05);
            }
            for (int j = 0; j < statsNeutral.Length; j++)
            {
                double actual = ((double)statsNeutral[j]) / testsNumber;
                //Console.WriteLine(actual + " " + chanceNeutral[j]);
                Assert.That(actual >= chanceNeutral[j] - 0.05 && actual <= chanceNeutral[j] + 0.05);
            }

        }

        // Edge cases
        [Test]
        public void LayerEdgeCases()
        {
            Layer layer = new Layer();
            Assert.That(layer.Count == 0);

            // Adding the same element is prohibited, doesn't throw an exception
            WeightedElement we1 = new WeightedElement("Element1");
            layer.Add(we1);
            Assert.That(layer.Count == 1);
            layer.Add(we1);
            Assert.That(layer.Count == 1);
            layer.Clear();

            // Adding two different elements with the same value is fine
            layer.Add("E");
            layer.Add("E");
            Assert.That(layer.Count == 2);
            layer.Clear();

            // Adding null element is prohibited, it shouldn't be added, should throw an exception
            Assert.Throws<ArgumentNullException>(() => layer.Add(null));
            Assert.That(layer.Count == 0);
            // Adding null as a value is fine
            layer.Add(null, 1);
            Assert.That(layer.Count == 1);
            layer.Clear();

            // Removing by index out of range
            layer.Add(we1);
            Assert.Throws<ArgumentOutOfRangeException>(() => layer.RemoveAt(1));

            // Getting by index out of range
            Assert.Throws<ArgumentOutOfRangeException>(() => layer.Get(1));

            // Randomizing with only one element
            for (int i = 0; i < 10; i++)
                Assert.That(layer.GetRandom(), Is.EqualTo(we1.Value.ToString()));

            // Randomizing with no elements
            layer.Clear();
            layer.DefaultValue = "Default";
            for (int i = 0; i < 10; i++)
                Assert.That(layer.GetRandom(), Is.EqualTo("Default"));


        }


        // Bundle testing


        [Test]
        public void BasicBundleManipulation()
        {
            Bundle bundle = new Bundle("Bundle");

            Assert.That(bundle.Count == 0);

            // Inserting layers
            bundle.InsertNewLayer(0);
            Assert.That(bundle.Count == 1);
            bundle.InsertNewLayer(0);
            Assert.That(bundle.Count == 2);
            bundle.InsertNewLayer(0);
            Assert.That(bundle.Count == 3);
            bundle.InsertNewLayer(1);
            Assert.That(bundle.Count == 4);
            bundle.InsertNewLayer(2);
            Assert.That(bundle.Count == 5);
            bundle.InsertNewLayer(5);
            Assert.That(bundle.Count == 6);
            Assert.Throws<ArgumentOutOfRangeException>(() => bundle.InsertNewLayer(10));
            Assert.That(bundle.Count == 6);

            // Removing layers
            bundle.RemoveLayerAt(0);
            Assert.That(bundle.Count == 5);
            bundle.RemoveLayerAt(0);
            Assert.That(bundle.Count == 4);
            bundle.RemoveLayerAt(3);
            Assert.That(bundle.Count == 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => bundle.RemoveLayerAt(3));
            Assert.That(bundle.Count == 3);

            // Clearing layers
            bundle.ClearLayers();
            Assert.That(bundle.Count == 0);

            // Getting layers
            bundle.InsertNewLayer(0);
            bundle.InsertNewLayer(0);
            bundle.InsertNewLayer(0);
            Assert.That(bundle.GetLayer(1), Is.EqualTo(bundle.Layers[1]));

            // Adding elements to chosen layer
            WeightedElement we1 = new WeightedElement("Element1");
            WeightedElement we2 = new WeightedElement(123); // elements with any value can be added
            bundle.AddToLayer(0, we1);
            bundle.AddToLayer(0, we2);
            bundle.AddToLayer(0, "Element3"); // but new elements with string value are meant to be added
            Assert.That(bundle.GetLayer(0).Count == 3);

            // Removing/getting elements from layers is only matryoshka
            Assert.That(bundle.GetLayer(0).Get(1), Is.EqualTo(we2));
            bundle.GetLayer(0).RemoveAt(1);
            Assert.That(bundle.GetLayer(0).Count == 2);
        }

        // Bundle with independent layers won't stop generation
        // if one of the layers fails its check to get picked
        [Test]
        public void BundleIndependentLayerChanceTest()
        {
            // independent layers
            Bundle bundle = new("Bundle", true);

            // 80% chance 
            bundle.InsertNewLayer(0, 0.8, "D0");

            // 90% chance 
            bundle.InsertNewLayer(1, 0.9, " D1");

            // 20% chance 
            bundle.InsertNewLayer(2, 0.2, " D2");


            bundle.AddToLayer(0, "E0");
            bundle.AddToLayer(1, "E1");
            bundle.AddToLayer(2, "E2");

            int d0 = 0, d1 = 0, d2 = 0;
            int e0 = 0, e1 = 0, e2 = 0;
            int tests = 10000;
            // counting occurances
            for(int i = 0; i<tests; i++)
            {
                string result = bundle.GetRandom();
                if (result.Contains("D0"))
                    d0++;
                if (result.Contains("D1"))
                    d1++;
                if (result.Contains("D2"))
                    d2++;
                if (result.Contains("E0"))
                    e0++;
                if (result.Contains("E1"))
                    e1++;
                if (result.Contains("E2"))
                    e2++;
                
            }

            Assert.That(((double)d0) / tests >= 0.15 && ((double)d0) / tests <= 0.25);
            Assert.That(((double)d1) / tests >= 0.05 && ((double)d1) / tests <= 0.15);
            Assert.That(((double)d2) / tests >= 0.75 && ((double)d2) / tests <= 0.85);
           
            Assert.That(((double)e0) / tests >= 0.75 && ((double)e0) / tests <= 0.85);
            Assert.That(((double)e1) / tests >= 0.85 && ((double)e1) / tests <= 0.95);
            Assert.That(((double)e2) / tests >= 0.15 && ((double)e2) / tests <= 0.25);

        }

        // Bundle with dependent layers will stop generation
        // if one of the layers fails its check to get picked
        [Test]
        public void BundleDependentLayerChanceTest()
        {
            // dependent layers
            Bundle bundle = new("Bundle", false);

            // 80% chance 
            bundle.InsertNewLayer(0, 0.8, "D0");

            // 90% chance 
            bundle.InsertNewLayer(1, 0.9, " D1");

            // 20% chance 
            bundle.InsertNewLayer(2, 0.2, " D2");


            bundle.AddToLayer(0, "E0");
            bundle.AddToLayer(1, "E1");
            bundle.AddToLayer(2, "E2");

            int d0 = 0, d1 = 0, d2 = 0;
            int e0 = 0, e1 = 0, e2 = 0;
            int tests = 1000;
            // counting occurances
            for (int i = 0; i < tests; i++)
            {
                string result = bundle.GetRandom();
                if (result.Contains("D0"))
                    d0++;
                if (result.Contains("D1"))
                    d1++;
                if (result.Contains("D2"))
                    d2++;
                if (result.Contains("E0"))
                    e0++;
                if (result.Contains("E1"))
                    e1++;
                if (result.Contains("E2"))
                    e2++;
                
            }

            // 20%
            Assert.That(((double)d0) / tests >= 0.15 && ((double)d0) / tests <= 0.25);
            // 8%
            Assert.That(((double)d1) / tests >= 0.03 && ((double)d1) / tests <= 0.13);
            // 57,6%
            Assert.That(((double)d2) / tests >= 0.526 && ((double)d2) / tests <= 0.626);

            // 80%
            Assert.That(((double)e0) / tests >= 0.75 && ((double)e0) / tests <= 0.85);
            // 72%
            Assert.That(((double)e1) / tests >= 0.67 && ((double)e1) / tests <= 0.77);
            // 14,4%
            Assert.That(((double)e2) / tests >= 0.094 && ((double)e2) / tests <= 0.194);

        }

        // If a layer is gendered, it will only get picked if character has matching gender
        // or if layer is gender-neutral. Gender-neutral characters will only generate 
        // gender-neutral results
        // Gender restricted layers that weren't picked won't trigger "dependent" layers end of generation
        [Test]
        public void BundleGenderedLayersRandomization()
        {
            Bundle bundle = new("Bundle");

            bundle.InsertNewLayer(0, 1, "D", Enums.Gender.Neutral);
            bundle.InsertNewLayer(1, 1, "D", Enums.Gender.Female);
            bundle.InsertNewLayer(2, 1, "D", Enums.Gender.Male);

            bundle.AddToLayer(0, "N");
            bundle.AddToLayer(1, "F");
            bundle.AddToLayer(2, "M");

            int testsNumber = 1000;
            for (int i = 0; i < testsNumber; i++)
            {
                string result = bundle.GetRandom(Enums.Gender.Female);

                Assert.That(result, Does.Contain("F"));
                Assert.That(result, Does.Contain("N"));
                Assert.That(result, Does.Not.Contain("M"));
                Assert.That(result, Does.Not.Contain("D"));

                result = bundle.GetRandom(Enums.Gender.Male);

                Assert.That(result, Does.Not.Contain("F"));
                Assert.That(result, Does.Contain("N"));
                Assert.That(result, Does.Contain("M"));
                Assert.That(result, Does.Not.Contain("D"));

                result = bundle.GetRandom(Enums.Gender.Neutral);

                Assert.That(result, Does.Not.Contain("F"));
                Assert.That(result, Does.Contain("N"));
                Assert.That(result, Does.Not.Contain("M"));
                Assert.That(result, Does.Not.Contain("D"));

            }

        }

        // Age-restricted layers can get picked only if character's age
        // is within specified bounds. Edge numbers are within the bounds
        // Age restricted layers that weren't picked won't trigger "dependent" layers end of generation
        [Test]
        public void BundleAgeRestrictedRandomization()
        {
            Random random = new Random();

            Bundle bundle = new("Bundle");

            bundle.InsertNewLayer(0, 1, "D", Enums.Gender.Neutral, 10, 20);
            bundle.InsertNewLayer(1, 1, "D", Enums.Gender.Neutral, 15, 25);
            bundle.InsertNewLayer(2, 1, "D", Enums.Gender.Neutral, 20, 30);

            bundle.AddToLayer(0, "1");
            bundle.AddToLayer(1, "2");
            bundle.AddToLayer(2, "3");

            int testsNumber = 1000;
            for (int i = 0; i < testsNumber; i++)
            {
                int age = random.Next(50);
                string result = bundle.GetRandom(Enums.Gender.Neutral, age);

                if (age >= 10 && age <= 20)
                    Assert.That(result, Does.Contain("1"));
                if (age >= 15 && age <= 25)
                    Assert.That(result, Does.Contain("2"));
                if (age >= 20 && age <= 30)
                    Assert.That(result, Does.Contain("3"));

                Assert.That(result, Does.Not.Contain("D"));
            }
        }


        [Test]
        public void BundleEdgeCases()
        {
            Bundle bundle = new("Bundle");

            // Adding to non-existent layers
            Assert.Throws<ArgumentOutOfRangeException>(() => bundle.AddToLayer(0, "E"));

            // Removing by index out of range
            Assert.Throws<ArgumentOutOfRangeException>(() => bundle.RemoveLayerAt(0));

            // Getting by index out of range
            Assert.Throws<ArgumentOutOfRangeException>(() => bundle.GetLayer(0));

            // Randomizing with no layers
            // Default Value by default is "No suitable layers"
            Assert.That(bundle.GetRandom(), Is.EqualTo("No suitable layers"));

            // Randomizing with no suitable layers
            bundle.InsertNewLayer(0, 1, "", Enums.Gender.Female);
            Assert.That(bundle.GetRandom(Enums.Gender.Male), Is.EqualTo("No suitable layers"));

        }
    
    }
}
