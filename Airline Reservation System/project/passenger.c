#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

#include "passenger.h"
#include "scheduledFlight.h"
#include "reservation.h"

// create poiner to a file
FILE * fpassengers;








/**************************************************************************************************************************/
/********************************************************
 *   Function gets a name for anything,
 *   such as passenger name, surname, country, city, etc.
 ********************************************************/
void get_Name(char arr[], short n, short maxN)
{
    // depending on number provided as argument, choose what to print
    switch (n)
    {

        case 0:
        case 1: puts("------------------------------------------------------------------------\n"
                     "Please enter passenger's name:"
                     "\n------------------------------------------------------------------------");
                break;

        case 2: puts("------------------------------------------------------------------------\n"
                     "Please enter passenger's surname:"
                     "\n------------------------------------------------------------------------");
                break;

        case 3: puts("------------------------------------------------------------------------\n"
                     "Please enter passenger's country of birth:"
                     "\n------------------------------------------------------------------------");
                break;

        case 4: puts("------------------------------------------------------------------------\n"
                     "Please enter passenger's city of birth:"
                     "\n------------------------------------------------------------------------");
                break;

        case 5: puts("------------------------------------------------------------------------\n"
                     "Please enter passenger's home address:"
                     "\n------------------------------------------------------------------------");
                break;

        case 6: puts("------------------------------------------------------------------------\n"
                     "Please enter the name of Airline Company:"
                     "\n------------------------------------------------------------------------");
                break;

        case 7: puts("------------------------------------------------------------------------\n"
                     "Please enter the name of departure city:"
                     "\n------------------------------------------------------------------------");
                break;

        case 8: puts("------------------------------------------------------------------------\n"
                     "Please enter the name of arrival city:"
                     "\n------------------------------------------------------------------------");
                break;

    }

    char ch;         // char for user input
    short i = 0;     // counter for array
    fflush(stdin);   // just to prevent any possible errors

    // while user input is not new line and
    // current counter is less than number of elements allowed in array
    do
    {
        ch = getchar();     // get user input

        if(isalpha(ch) || (ch == ' ' && i > 0))      // check whether input is an alphabetic letter or space
        {
            arr[i] = ch;                   // if yes, put it in array
            i++;                           // increment i for next element in array
        }
        else if(ch != '\n' || (i == 0 && n != 1 && n != 6)) // if input is not a new line or name of passenger or airline company
        {

            while (getchar() != '\n')      //clear input line
                continue;

            // print error
            puts("Invalid input, please use only alphabetic letters:");

            i = 0;              // reset counter for array to zero
            arr[0]= '\n';       // put new line at first array element to repeat the loop
        }

    // while enter is not pressed and counter + 1 is less than number of characters allowed, or if first array element is new line
    }while((ch != '\n' && (i + 1) < maxN) || arr[0] == '\n');

    fflush(stdin);      // empty buffer, to prevent any further errors ...
    arr[i] = '\0';      // add \0 at the end of string
    normalize_name(arr);// normalize name

}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/**************************************************************
 *   Function to normalize a name,
 *   example: lAZAR -> Lazar, or nAtAlIjA -> Natalija
 **************************************************************/
void normalize_name(char arr[])         // Function takes array of name as argument
{
    short i = 0;                        // index for array

    for( i = 0; i < strlen(arr); i++)   // loop trough array
    {
        if(i == 0)                      // if first element
        {
            arr[i] = toupper(arr[i]);   // make a letter capital
        }
        else if (arr[i] == ' ')         // if a element is space
        {
            i++;                        // increment index
            arr[i] = toupper(arr[i]);   // make letter next to space capital
        }
        else arr[i] = tolower(arr[i]);  // else, make a letter small
    }
}

/**************************************************************************************************************************/








/**************************************************************************************************************************/
/**************************************************
 *   Function let us choose a title for us
 *   example: Mr. , Dr. etc
 **************************************************/
void get_Title(char arr[])    // Function takes array as argument
{
    // print options
    puts("------------------------------------------------------------------------\n"
         "Choose title:\n1. Mr.\t2. Ms\t3. Mrs.\t4. Dr.\t5. Prof.");

    short option = get_input(1, 5);

    // depending on number entered, choose wich title to assing to a passenger
    // using strncpy to copy a string (title) to array which holds it
        switch(option)
        {
            case 1: strncpy(arr, "Mr.", 4);   break;
            case 2: strncpy(arr, "Ms.", 4);   break;
            case 3: strncpy(arr, "Mrs.", 5);  break;
            case 4: strncpy(arr, "Dr.", 4);   break;
            case 5: strncpy(arr, "Prof.", 6); break;
        }

}

/**************************************************************************************************************************/








/**************************************************************************************************************************/
/**************************************************************
 *   Function returns integer
 *   depending on how many digits are required
 *   example for: mobile number, passport number
 **************************************************************/
unsigned long long int get_int(short n)     //function takes a number as argument which says how many numbers function must return
{
    //declarations of variables
    unsigned long long int num, numDigits;  // to hold an integer
    short count;                            // to hold a number of digits

    // a number that is passed to n is defined in passenger.h or scheduledFlight.h, PASSDIGITS, MOBDIGITS or
    switch (n)
    {
        case 3: puts("------------------------------------------------------------------------\n"
                     "Please enter flight ID: ");
                break;

        case 9: puts("------------------------------------------------------------------------\n"
                     "Please enter passenger's passport number: ");
                break;

        case 11: puts("------------------------------------------------------------------------\n"
                      "Please enter passenger's mobile number:\nE.g 35699123456,");
                break;

    }

    // loop trough while requirements is not met
    // in other words,
    // do this while number entered does not have required number of digits
    do
    {
        printf("Please provide a number with %hd digits: ", n);

        count = 0;                      // count initialized to 0
        fflush(stdin);

        if(scanf("%llu", &num) != 0)    // get input from user, if input is number, proceed
        {
            numDigits = num;            // store number to temporary variable

            while(numDigits)            // while numDigits is true ( greater than 0)
            {
                numDigits /= 10;        // truncate last digit
                count++;                // increment counter ( number of digits)
            }

            // if counter is the same as number provided as argument
            if(count == n)
                return num;                 // return integer from input

            // else print this error
            printf("%llu does not have %hd digits and it cannot start with 0 nor contain letters\n", num, n);

        }

    }while(1); // will loop trough, wile number's digits from input is not same as required

}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to create new passenger
 **************************************************************************************/
short createNewPassenger(struct Passenger *passenger, short n)
{
    short count = 0, i, noNew = 1; // inicializing counter to 0, and declaring counter i
    short index, filecount;        // declaring variables
    unsigned long long temp;       // declaring temp variable for user input regarding passport number for checking purpose

    short size = sizeof (struct Passenger);   // getting size of struct Passenger

    if(!openPassengerFile("a+b")) return;     // open file of passengers

    // while there are elements in file, count them
    while (fread(&passenger[count], size, 1, fpassengers) == 1)
    {
        count++; // increment counter
    }

    // if count is the same as the allowed number of passengers
    if (count == MAXPASSENGERS)
    {
        fputs("The passenger.dat file is full.\n", stderr); // show error
        system("pause");        // pausing screen to be more readable
        fclose(fpassengers);    // close file
        return;   // exit function

    }

        filecount = count;  // inicialize filecount to number of counts
        index = count;      // store count to index for later use

        // if there is space for new elements, proceed
        puts("Please provide passenger's details:");
        puts("Press [enter] at the start of a line to stop.");

        // get first name for while loop
        get_Name(passenger[count].name, NAME, MAXNAME);
        while (count < MAXPASSENGERS && passenger[count].name != NULL && passenger[count].name[0] != '\0')
        {
            get_Name(passenger[count].surname, SURNAME, MAXNAME);
            get_Title(passenger[count].title);                                   //
            get_Name(passenger[count].country, COUNTRY, MAXNAME);                ////
            get_Name(passenger[count].city, CITY, MAXNAME);                      //////
            get_Name(passenger[count].homeAddress, ADDRESS, MAXADDRESS);  //////////////// getting information from user
            get_date(passenger[count].DOB, 1900, 2012, 1);                       //////
            passenger[count].mobNo = get_int(MOBDIGITS);                         ////
            passenger[count].passportNo = checkPass(-5, count, passenger);       //

            for( i = 0; i < MAXPASSENGERS; i++)         // using MAXPASSENGERS(50) in order to avoid redundancy, to not create new constant that holds the same number
            {
                passenger[count].reservations[i] = -1;  // initialize all elements to -1
            }

            count++;        // increment the counter

            if (count < MAXPASSENGERS && !n)        // if counter is less that allowed passenger in file, and if it is regular creation of passenger i.e. not the one with function for reservation
            {
                system("cls");
                puts("Enter the next name or press [ENTER] to exit:");
                get_Name(passenger[count].name, NAME, MAXNAME);         // ask for new passenger
            }
            else    // if file is full, print message
            {
                system("cls");
                puts("\n\n\n---------------------------------------------------------------\n"
                     "File is full, you cannot add more passengers!");
            }
        }

        if (count - filecount > 0)                  // if passenger/s is created
        {
            system("cls");
            if((count - filecount) == 1)        // just to print correct text
                puts("Here is new passenger:");
            else
                puts("Here are new passengers:");
            for (index; index < count; index++)         // show created passengers
                show_PassengerInfo(index, passenger);
            fwrite(&passenger[filecount], size, count - filecount, fpassengers);    // save them in a file
        }
        else    // if no passengers created
        {
            puts("No new passengers registered.\n");
            noNew = 0;
        }

        fclose(fpassengers);

        return noNew;
}

/**************************************************************************************************************************/








/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to display all passengers in the file
 **************************************************************************************/
void show_existingPassengers(const struct Passenger *passenger)
{

    short count = 0;                         // inilialize counter to 0

    short size = sizeof (struct Passenger);  // get seize of structure passenger

    if(!openPassengerFile("rb")) return;     // open a file

    rewind(fpassengers);                     // go back to the beggining of a file

    while (fread(&passenger[count], size, 1, fpassengers))    // while there is data to be read

    {
            if (count == 0)                                   // display message only once
                puts("Current registered passengers:");
                show_PassengerInfo(count, passenger);         // function which shows passenger info with index count

            count++; // increment counter
    }

    if(count == 0)              // if file is empty, print message
        puts("File is empty.");


    fclose(fpassengers);    // when done, close file
}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to display a particular passenger info
 *   as argument, it takes a number for index of passenger
 **************************************************************************************/
void show_PassengerInfo(short n, const struct Passenger *passenger)
{
    // print info
    printf("\n******************************************************\nPassenger Info:"
                       "\nName and surname: \t%s %s %s\nDate of birth: \t\t%-0.2hd.%-0.2hd.%hd\nPassport No. \t\t%d\n"
                       "Country: \t\t%s\nCity: \t\t\t%s\nHome address: \t\t%s\nMobile number: \t\t+%llu\n"
                       "******************************************************\n\n"
           , passenger[n].title, passenger[n].name
           , passenger[n].surname, passenger[n].DOB[0]
           , passenger[n].DOB[1], passenger[n].DOB[2]
           , passenger[n].passportNo, passenger[n].country
           , passenger[n].city, passenger[n].homeAddress
           , passenger[n].mobNo);

}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to open the file where passangers' details are stored
 **************************************************************************************/
short openPassengerFile(char *mode)
{
    if ((fpassengers = fopen("passengers.dat", mode)) == NULL)     // if cannot open a file
    {
        fputs("Cannot open passengers.dat file.\n",stderr);         // display errorr
        return 0;   // return zero
    }
    else return 1;  // if file can be opened, return one
}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/******************************************************************************************
 *   Function to to modify chosen passenger, all information or specific one
 *   such as, user can choose only name of passenger to edit or to edit all informations
 ******************************************************************************************/
void modifyPassenger(struct Passenger *passenger)
{

    short optionP, optionE, count;               // optionP is to choose passenger which to edit, and optionE is to choose what to edit

    if(!openPassengerFile("r+b")) return;        // open a file for editing

    fflush(stdin);  // unespected behaviour prevention

    if((count = listPassengers(passenger)) != -1 && (optionP = get_input(-1, count)) != -1) // if there are records in a file, and user input is not -1, proceed
    {
        system("cls");          // clear screen to be more readable
        puts("Passenger chosen for editing:");

        show_PassengerInfo(optionP, passenger);    // show passenger chosen to be edited

        printf("\nPlease choose detail(s) you want to edit, or -1 to exit:\n0. All\n1. Name\n2. Surname\n3. Title\n"
                "4. Country\n5. City\n6. Home address\n7. Passport number\n8. Mobile number\n9. Date of birth\nChoose option: "); // print options

        if((optionE = get_input(-1, 9)) != -1)      // if user input is not -1, proceed
        {

            // depending on number entered
            switch(optionE)
            {
                case 0: get_Name(passenger[optionP].name, 0, MAXNAME);                      // edit everything
                        get_Name(passenger[optionP].surname, SURNAME, MAXNAME);
                        get_Title(passenger[optionP].title);
                        get_Name(passenger[optionP].country, COUNTRY, MAXNAME);
                        get_Name(passenger[optionP].city, CITY, MAXNAME);
                        get_Name(passenger[optionP].homeAddress, ADDRESS, MAXADDRESS);
                        get_date(passenger[optionP].DOB, 1900, 2012, 1);
                        passenger[optionP].passportNo = checkPass(optionP, count, passenger);
                        passenger[optionP].mobNo = get_int(MOBDIGITS);
                        break;

                case 1: get_Name(passenger[optionP].name, 0, MAXNAME);                          break; // edit only name
                case 2: get_Name(passenger[optionP].surname, SURNAME, MAXNAME);                 break; // edit only surname
                case 3: get_Title(passenger[optionP].title);                                    break; // edit only title
                case 4: get_Name(passenger[optionP].country, COUNTRY, MAXNAME);                 break; // edit only country
                case 5: get_Name(passenger[optionP].city, CITY, MAXNAME);                       break; // edit only city
                case 6: get_Name(passenger[optionP].homeAddress, ADDRESS, MAXADDRESS);          break; // edit only address
                case 7: passenger[optionP].passportNo = checkPass(optionP, count, passenger);   break; // edit only passport number
                case 8: passenger[optionP].mobNo = get_int(MOBDIGITS);                          break; // edit only mobile number
                case 9: get_date(passenger[optionP].DOB, 1900, 2012, 1);                        break; // edit only date of birth

            }

            short size = sizeof (struct Passenger);              // get seize of structure Passenger
            fseek(fpassengers, size * optionP, SEEK_SET);        // find position of record with chosen passenger
            fwrite(&passenger[optionP], size, 1, fpassengers);   // write there edited data
            system("cls");                                       // clear screen for to be more readable
            puts("Here is edited passenger: ");                  // print message
            show_PassengerInfo(optionP, passenger);              // print details of edited passenger

        }
    }

    fclose(fpassengers);    // when done, close file
}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/******************************************************************************************
 *   Function to check whether a passport number exists or not
 ******************************************************************************************/
unsigned int checkPass(short chosenP, short count, const struct Passenger *passenger)
{
    unsigned int temp;
    short i;

    // input valitaion, checking whether passenger with unputed passport number exist or not.
    do
    {
        temp = (unsigned int)get_int(PASSDIGITS); // getting user input

        for (i = 0; i < count; i++)
        {
            if(chosenP == i)        // if index point to chosen passenger, skip it
                i++;

            if(passenger[i].passportNo == temp)     // comparting passport number with user input
            {
                i = -1;
                printf("Passenger with passport number %u already exists.\n", temp);
                break;      // if exist, exit loop
            }

        }

    }while(i == -1);        // while entered passport number already exist

    return temp;    // return new passport number
}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/******************************************************************************************
 *   Function to go through passengers, list them and count them
 ******************************************************************************************/
short listPassengers(const struct Passenger *passenger)
{
    short count = 0;
    short size = sizeof(struct Passenger);

    while (fread(&passenger[count], size, 1, fpassengers) == 1)     // while there is data to be read
    {                                                               // read from file and store in struct
            if (count == 0)                                         // display message only once
                puts("Current passengers:\n");
                printf("%d. %s %s %s\n", count, passenger[count].title,
                       passenger[count].name, passenger[count].surname); // show passengers

            count++; // increment counter
    }

    if(count == 0)      // if file is empty
    {
        fclose(fpassengers);    // if empty, close it and print message
        puts("passengers.dat file is empty.");
        return -1;                 // and exit from function
    }

    printf("\nPlease provide one number from above, or -1 to exit: ");
    return --count;  // return number of passengers in a file
}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/********************************************************************************************
 *   Function to get user input, argument indicates the bigest number that user can enter
 ********************************************************************************************/
void get_date(short arr[], short minYear, short maxYear, short n)
{
    short mm[] = {31,28,31,30,31,30,31,31,30,31,30,31};         // initialize months

    if(n)   puts("------------------------------------------------------------------------\n"   // just to print correct text
                 "Please provide passenger's date of birth");

    else    puts("------------------------------------------------------------------------\n"
                 "Please provide date of flight departure");

    printf("Please enter year: ");
    if((arr[2] = get_input(minYear, maxYear)) % 4 == 0) mm[1] = 29;     // if leap year is entered, february will have 29 days

    printf("Please enter month: ");
    arr[1] = get_input(1, 12);      // get input for month

    printf("Please enter day: ");
    arr[0] = get_input(1, (mm[arr[1] - 1]));    // get input for days, boundaries depend on month chosen above

}
/**************************************************************************************************************************/








/**************************************************************************************************************************/
/********************************************************************************************
 *   Function to get user input, argument indicates the bigest number that user can enter
 ********************************************************************************************/
short get_input(short nMIN, short nMAX)
{
    short option;
    do
    {
        if(scanf("%hd", &option) != 0)    // get input from user
        {

            if(option >= nMIN && option <= nMAX)     //if number is from 1 to 5
                break;                               //exit the loop

        }

        // if input is invalid, show these errors depending on numbers provided as argument
        if( nMAX == 0 )   printf("Invalid input, please enter a number 0 or -1 to exit: ");     // if max is 0
        else if ( nMIN == -1 ) printf("Invalid input, please enter a number between 0 and %hd, or -1 to exit: ", nMAX); // if min is -1
        else printf("Invalid input, please enter a number between %hd and %hd: ", nMIN, nMAX); // if mn is not -1 and max is not 0

        fflush(stdin);  // empty buffer

    }while (1); // while input is not in the range

    return option;  // return input number

}
/**************************************************************************************************************************/
// THE NUMBER OF THE BEAST >:)
