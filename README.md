# FossilFuel
Picks the fossil fuel rate from xml file, and process the data and gives output in xml format

Steps to execute program.

App.config file - Input, Referencedata, Output folder/file paths are available and configurable. you may change the paths, if required.

Sample input xml file is available in the input folder (refer App.config file), the values can be changed for testing purpose.

Build the application, and run the application from MS Visual studio or double click "FossilFuel.exe" (\bin\Debug). Note : If executing program using .exe you may change folder paths using "FossilFuel.exe.config" file (\bin\Debug))

The program is listening to the input folder, once the new input xml file is placed in that input folder.The execution begins and generates output file named as GenerationOutput.xml in configured Output folder path.
