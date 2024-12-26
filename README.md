# Old Phone Keypad Emulator

![App Screenshot](https://i.sstatic.net/V0lbSC5t.png/468x300?text=App+Screenshot+Here)


## 📖 Overview

The **Old Phone Keypad Emulator** is a Windows Forms
application that simulates an old mobile phone keypad for text input. The
emulator interprets input sequences and converts them into text as per the
rules of multi-tap input on old phones. This project was developed as part of a
coding challenge.


## 🚀 Features


- **Multi-tap Input**: Supports cycling through characters on numeric buttons.


- **Pause Detection**: Implements pause functionality for typing consecutive letters from the same button.


- **Special Characters**:
  - `#` sends the input and displays the output.
  - `*` backspace, Remove character before *.

- **Clear User Interface**: Easy-to-use keypad interface with visual feedback.

- **Direct Input**: Can paste or type input directly into the screen area. 

*Note: One-second wait function for space only works when the mouse clicks on the mobile buttons. Press the space bar if you type using the keyboard.*


 **Output generated when :**
 - Click # button.
 - Click  Call Button (Green).
 - Click  Menu Button (Green).
 - Wait more than one second.


## 🛠️ Implementation Details


The application utilizes the **Windows Forms framework** and
is written in C#. The core logic for handling input sequences resides in the `OldPhonePad()`
method, which processes strings according to the input rules.

### Example Inputs
| Input Sequence          | Output        | Output Breakdown |
|-------------------------|---------------|---------------|
| `33#`                   | E             | Key = 3 , Index = 2         |
| `227*#`                 | B             | 7* -> REMOVED; 22 -> Key = 2, Index = 2        |
| `4433555 555666#`       | HELLO         | 44   -> H, 55   -> E, 555  -> L, 555  -> L, 666  -> O             |
| `8 88777444666*664#`    | TURIOMG         | 8 -> T, 88-> U,  777-> R,  444-> I,  66-> O,  6*   -> REMOVED,  6    -> M,  4-> G;           |

Reference : 
        
         2 → A, B, C | 3 → D, E, F | 4 → G, H, I | 5 → J, K, L | 6 → M, N, O |  7 → P, Q, R, s |  8 → T, U, V



## 🖥️ How to Run


1. **Clone the Repository**
   ```bash
   git clone https://github.com/isanka89/OldPhoneKeypadEmulator.git
   cd OldPhoneKeypadEmulator
 

Open
     the Solution

     Open the .sln file in Visual Studio.

 
Build  and Run

     Build the project and run the application to launch the emulator.



## 📜 Input Processing Logic

 
The application processes input based on the following rules:

**Character Mapping:**

    2 → A, B, C
    3 → D, E, F
    4 → G, H, I
    5 → J, K, L
    6 → M, N, O
    7 → P, Q, R, s
    8 → T, U, V
    9 → W, X, Y, Z

**Backspace Function**
  
  *Backspace > Remove character before `*` and itself.*
        
Backspace possibilities samples:
- **            -> Empty
- **454         -> 454
- 4454*         -> 445 ( Remove `4*` )
- *454**454*    -> 45454 ( Remove `4*` )       


      private static void BackspaceFunction(ref string segment)
      {
          int asteriskCount = segment.Count(c => c == '*');
          while (asteriskCount > 0)
          {
               int asteriskIndex = segment.IndexOf("*");
               if (asteriskIndex > 0)
               {
                    //* is not the first character
                    // Remove the charactor before * and the * it self
                    segment = segment.Remove(asteriskIndex - 1, 2); 

               }
               else if (asteriskIndex == 0)
               {
                    //* is the first character.;
                    segment = segment.Remove(0, 1);// remove the * from the start index
               }

               asteriskCount = segment.Count(c => c == '*');
               if (asteriskCount > 0)// call same function untill all the * removed.
                  BackspaceFunction(ref segment);
          }
      }


**Main Logic**

     public static string OldPhonePad(string input)
     {
          if (string.IsNullOrWhiteSpace(input))
               return "????";

          StringBuilder output = new StringBuilder();
          input = input.Trim();
          if (input.EndsWith('#'))// remove the last # charactor
               input = input.Remove(input.Length - 1, 1);

          string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);// split by space
          foreach (string part in parts)
          {
               var segment = part;
               if (string.IsNullOrWhiteSpace(segment)) 
                    continue;                

               // Backspace function
               if (segment.Contains("*"))
                 BackspaceFunction(ref segment);


               var currentKey = segment[0]; // get the 1st key
               var pressedCount = 1; // set the pressedCount as 1 because part[0] is already 1

               // loop all the charactors in the part to identify the all keys in the current part. starting with 1 idex to skip the 1st item in the part.
               for (int i = 1; i < segment.Length; i++)
               {
                    if (segment[i] == currentKey && (pressedCount + 1) <= ButtonMapper.ButtonMapping[currentKey.ToString()].Length)
                         pressedCount++; // same key pressed and pressed count less than or equel to currentKey's assined letters count. (eg: 6-> MNO=3 , 7-> PQRS = 4)
                    else
                    {
                         // Decode the last charactor (-1 index) when user pressed a different key    
                         output.Append(GetExactLetter(currentKey.ToString(), pressedCount - 1));


                         // Skip invalid charactors, (direct input can include invalid charactors.)
                         if (!Regex.IsMatch(segment[i].ToString(), @"^\d+$"))
                         {
                              output.Append(segment[i].ToString());
                              pressedCount = 0;
                              continue;
                         }

                         // reset to capture the next key
                         currentKey = segment[i];
                         pressedCount = 1;

                         if (i == segment.Length - 1)
                              // DEcode the current letter when is  last charactor of the part. 
                              output.Append(GetExactLetter(currentKey.ToString(), pressedCount - 1));
                    }
               }

               // if user pressed single time or multiple times in same letter but less than 1 seconds delay.. eg : 33 or 8
               if (segment.Length == 1 || pressedCount > 1)
                    output.Append(GetExactLetter(currentKey.ToString(), pressedCount - 1));

          }

          return output.ToString();
     }

**Pause for Same Button:**
Pause for a second when entering consecutive letters from the same button.


**Send Input:** OK Button, Manu Button, # finalizes and displays the text. 
   *Note: When the user waits for more than one-second app will automatically display output for current.*


## 🖌️ User Interface
The application mimics the look and feel of an old phone keypad with the following components:

* Numeric buttons (0-9)
* and # buttons
* A text input display area
* A text output display area


## 📂 Project Structure

    Coding.Challange.IsankaThalagala/
    │
    ├── MobileEmulator.cs          # Main logic for input processing
    ├── MobileEmulator.Designer.cs # UI Design Code
    ├── Resources/                 # Contains images for keypad buttons
    ├── README.md                  # Project documentation
    └── Utility    
         └── ButtonMapper.cs       # Define button mapping > numbers to characters   
         └── TextDecorder.cs       # Text decorder class        
    
    Coding.Challange.IsankaThalagala.Test/
    │
    ├── TextDecoderTests.cs          # Test Cases



## 📂 Unit Testing
Unit test developed using NUnit library.

**Necessary packages**

    <PackageReference Include="Moq" Version="4.20.72" />
    <PackageReference Include="NUnit" Version="4.3.1" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.6.0" />


Sample test cases as below:

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
                        666  -> O 
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


**Running the Unit Tests**

- Build the Solution: In Visual Studio, Build Project > Build Solution or press `Ctrl+Shift+B`.
- Run the Tests: In Visual Studio, go to Test Project > Run All Tests or press `Ctrl+R, A`.

![App Screenshot](https://i.sstatic.net/jtkkpp3F.png/468x300?text=Test+Here)





## 🛡️ License

This project is licensed under the MIT License. See the
LICENSE file for details.

