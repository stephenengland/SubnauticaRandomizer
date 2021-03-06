﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oculus.Newtonsoft.Json;
using SubnauticaRandomizer.Randomizer;
using System.IO;
using System.Linq;

namespace SubnauticaRandomizer.Tests
{
    [TestClass]
    public class RecipeInformationParsingTests
    {
        [TestMethod]
        public void SerializationDoesntMutate()
        {
            var recipes = RecipeInformation.ParseFromCSV("./recipeinformation.csv").ToList();

            Assert.IsNotNull(recipes);
            Assert.IsTrue(recipes.Count > 100);
            var standardTank = recipes.FirstOrDefault(rp => rp.Type == TechType.Tank);
            Assert.IsNotNull(standardTank);
            Assert.AreEqual(standardTank.Category, "Equipment");
            Assert.AreEqual(standardTank.RandomizeDifficulty.Count, 2);
        }

        [TestMethod]
        public void ConvertCsvToJson()
        {
            var recipes = new SerializedRecipesInformation()
            {
                Recipes = RecipeInformation.ParseFromCSV("./recipeinformation.csv").ToList().Select(SerializedRecipeInformation.ConvertFrom).ToList()
            };
            var jsonFilepath = Path.Combine("..", "..", "..","SubnauticaRandomizer", "recipeinformation.json");
            File.WriteAllText(jsonFilepath, JsonConvert.SerializeObject(recipes, Formatting.Indented));
        }
    }
}
