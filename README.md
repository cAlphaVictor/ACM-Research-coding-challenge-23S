# Vaille ACM Research Coding Challenge 23S Description
## programming language
For this coding challenge I ultimately decided to use the C# programming language instead of Python.

I decided to use C# over Python for a couple of reasons:

- familiarity
  - java is the first language I ever learned and is similar to C#
  - my brother and I program games in C# using the Unity Editor all the time
- computational efficiency
- memory efficiency
- commentability

## first step
The program starts out by creating the appropriate stream reader and csv reader objects necessary in order to read in all of the star data information from the provided csv file. This instantly required me to download, install, and import the C# CsvHelper library into my project, which I admittedly had never used before.

## second step
After reading in all of the necessary data from the provided csv file, the program continues on by compiling the data together based on each entry's star type. This data compilation process is aided by the StarTypeData class that I created which stores an int indicating the star type along with numerous lists that contain all entries of other information (such as all of the temperatures or luminosities) for that specific star type.

## third and final step
Once the program is done compiling the data together by star type it then proceeds to print out the average information for each star type. This printing out process is made incredibly easier by utilizing properties that I created in the StarTypeData class which computes each average piece of information and returns it publicly like a variable. This allows for each star type to have all its average information printed out to the console with a single compact Console.WriteLine(). The top header is printed out first, which specifies that the printing columns are in the order of star type, average temperature, average luminosity, average radius, average absolute magnitude, average star color, and finally average spectral class. The average information for each star type is then printed out to the console below the header using a foreach loop. It's also worth noting that it would be entirely possible for a star type to have an equal likelihood of being one color or another (like star type 0 having the same number of red stars as blue stars), and the same goes for spectral class as well. Thus, in order to show when there is a tie, the tied information will be separated by a " / ", but both pieces of information that could potentially be the average will be printed out.

After printing out the average information for each star type the program is essentially finished running and simply waits for the user to press the Enter key in order to close the console and terminate the program.

## console output screenshot
<img width="832" alt="Screenshot 2023-02-01 184258" src="https://user-images.githubusercontent.com/58710277/216202715-f089562b-a195-4008-8144-aa00662ab77c.png">
