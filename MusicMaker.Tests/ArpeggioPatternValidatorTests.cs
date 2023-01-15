using MusicMaker.Core.ValueObjects;
using NUnit.Framework;

namespace MusicMaker.Tests
{
    [TestFixture]
    public class ArpeggioPatternValidatorTests
    {
        [TestCase()]
        public void ArpeggioPatternRowValidator__HandleValidData()
        {
            var command = ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1();
            var validator = new ArpeggioPatternRowValidator();
            var result = validator.Validate(command.Pattern.Rows[0]);
            Assert.True(result.IsValid);
        }
    }
}