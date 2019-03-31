# supersidconverter
Supersid timestamp processor

supersid (https://github.com/ericgibert/supersid) is a python based SID recorder. The processed files are CSV files but the timestamp for each line is absent. Instead the header contains the start time and the reading frequency.

This command line program will create a new CSV file which will have the reading timestamp against each line for all CSV files in the director that the program is executed in. This is useful if you are using something like PowerBI to process single or collections of CSV files where it is time consuming to recreate the timestamp against each line. 

# Requirements:
.Net 4.5+

# Instructions for use:
Copy the application executable into the folder that contains the CSV files that need to be processed and run. The updated CSV files will be saved down in a new directory called “Output”.

To uninstall just delete the executable file.

# Compiling from source:
In MS VS create a new vb.net Console Application project and paste the code into Module1.vb (overwriting any exiting content). Build to test it and then publish. Note when testing you will need to put some CSV files into the /bin/Debug/ folder within the project.
