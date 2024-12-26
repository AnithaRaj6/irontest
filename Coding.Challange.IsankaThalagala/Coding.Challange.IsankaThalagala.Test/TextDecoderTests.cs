using NUnit.Framework;
using Moq;
using System;
using System.Text;
using System.Windows.Forms;
using Coding.Challange.IsankaThalagala.Utility;
using Coding.Challange.IsankaThalagala;

namespace Coding.Challange.IsankaThalagala.Test
{
    [TestFixture]
    public class TextDecoderTests
    {     
        [Test]
        public void TestOldPhonePad_ValidInput_Scenario_01()
        {
            //Arrange
            var input = "33#";
            var expected = "E";

            //Act
            var result = TextDecorder.OldPhonePad(input);

            //Assert
            Assert.That(result, Is.EqualTo(expected));  
        }

        [Test]
        public void TestOldPhonePad_ValidInput_Scenario_02()
        {
            //Arrange
            var input = "227*#";
            /* 
               - After backspace & # removed from the input.
                  input = "227*";
                        22   -> B
                        7*   -> REMOVE                     
            */
            var expected = "B";

            //Act
            var result = TextDecorder.OldPhonePad(input);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestOldPhonePad_ValidInput_Scenario_03()
        {
            //Arrange
            var input = "4433555 555666#";
            /* 
               - After backspace & # removed from the input.
                  input = "4433555 555666";
                        44   -> H
                        55   -> E
                        555  -> L
                        555  -> L
                        666  -> O      
            */
            var expected = "HELLO";

            //Act
            var result = TextDecorder.OldPhonePad(input);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestOldPhonePad_ValidInput_Scenario_04()
        {
            //Arrange
            var input = "8 88777444666*664#";
            /* 
               - After backspace & # removed from the input.
                  input = "8 8877744466664";
                        8    -> T
                        88   -> U
                        777  -> R
                        444  -> I
                        66   -> O 
                        6*   -> REMOVE
                        6    -> M
                        4    -> G
                        #    -> REMOVE             
           */
            var expected = "TURIOMG";

            //Act
            var result = TextDecorder.OldPhonePad(input);           

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void TestOldPhonePad_EmptyInput_Test()
        {
            //Arrange
            var input = " ";
            var expected = "????";

            //Act
            var result = TextDecorder.OldPhonePad(input);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}