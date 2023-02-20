using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using NamesFrequency_Brazil.Models;

namespace NamesFrequency_Test
{
    public class UnitTest1
    {
        string upperPath = @"C:\Projetos VS2022\NamesFrequency_Brazil";
        string fileName = "namesOutput.txt";
        [Fact]
        public void IsTheFileEmpty_FileContent_ReturnsFalseIfEmpty()
        {
            string json = File.ReadAllText(Path.Combine(upperPath, fileName));
            Assert.NotEmpty(json);
        }

        [Fact]
        public void IsTheListNull_NamesStatsMaxList_ReturnsFalseIfNull()
        {
            string json = File.ReadAllText(Path.Combine(upperPath, fileName));
            NomeStatsMax obj = JsonSerializer.Deserialize<NomeStatsMax>(json);
            Assert.NotNull(obj);
        }

        [Fact]
        public void IsTheFirstNameShorterThanThreeCharacters_Nome_FalseIfShorter()
        {
            string json = File.ReadAllText(Path.Combine(upperPath, fileName));
            NomeStatsMax obj = JsonSerializer.Deserialize<NomeStatsMax>(json);
            Assert.False(obj.nome.Length < 3);
        }

        [Fact]
        public void IsThereAnyNameTooSmall_Nomes_FalseIfAnyTooSmall()
        {
            string json = File.ReadAllText(Path.Combine(upperPath, fileName));
            NomeStatsMax obj = JsonSerializer.Deserialize<NomeStatsMax>(json);
            Assert.False(obj.nome.Length < 3 == true);
        }

        [Fact]
        public void IsThereAnyNumberOnNomes_NomesComNumeros_FalseIfNumbersOnNames()
        {
            string json = File.ReadAllText(Path.Combine(upperPath, fileName));
            NomeStatsMax obj = JsonSerializer.Deserialize<NomeStatsMax>(json);
            Assert.False(obj.nome.Any(c => char.IsDigit(c)) == true);
        }
    }
}