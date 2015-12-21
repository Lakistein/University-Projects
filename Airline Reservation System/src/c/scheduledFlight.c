#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "passenger.h"
#include "scheduledFlight.h"
FILE * fscFlight;


/**************************************************************************************************************************/
/********************************************************
 *   Function creates new scheduled flight
 ********************************************************/
void createFlight(struct Flight *scheduledFlight)
{
    short count = 0, i;       // inicializing counter to 0
    short index, filecount;   // declaring variables
    short temp;

    short size = sizeof (struct Flight);   // getting size of struct scheduledFlight

    if(!openfscFlightFile("a+b")) return;  // open a file for editing purpose

    // while there are elements in file, count them
    while (fread(&scheduledFlight[count], size, 1, fscFlight) == 1)
    {
        count++; // increment counter
    }

    // if count is the same as the allowed number of passengers
    if (count == MAXPASSENGERS)
    {
        fputs("The fscFlight.dat file is full.\n", stderr); // show error
        system("pause");
        fclose(fscFlight);    // close file
        return;               // exit function
    }

    filecount = count;  // inicialize filecount to number of counts
    index = count;      // store count to index for later use

    // if there is space for new elements, pro
    puts("Please provide flight details:");
    puts("Press [enter] at the start of a line to stop.");

    // get first name for while loop
    get_Name(scheduledFlight[count].airline, AIRNAME, MAXNAME);
    while (count < MAXPASSENGERS && scheduledFlight[count].airline != NULL && scheduledFlight[count].airline[0] != '\0')
    {

        scheduledFlight[count].flightID = checkID(-5, count, scheduledFlight);  // when input is valid, store it in struct member flightID
        get_Name(scheduledFlight[count].departureCity, FROM, MAXNAME);          // get name from where a flight is taking place
        get_Name(scheduledFlight[count].arrivalCity, TO, MAXNAME);              // get name to where a flight is taking place
        get_date(scheduledFlight[count].departureDate, 2014, 2050, 0);          // get date of flight departure
        scheduledFlight[count].timeDeparture = get_Time(1);                     // get time whene a plane will take off ( 1 is indicator what print, departure)
        scheduledFlight[count].timeArrival = get_Time(0);                       // get time when a plane will land ( 0 is indicator what print, arrival)
        strncpy(scheduledFlight[count].status, "OK", 4);                        // set status of flight to OK(still not cancelled).
        scheduledFlight[count].availableSeats = 50;                             // initialize avaliable seats to 50

        count++;                    // increment counter

        if (count < MAXPASSENGERS)  // if maximum number of allowed flights is not reached
        {
            system("cls");
            puts("Please provide next flight details, or press [ENTER] to finish:");
            get_Name(scheduledFlight[count].airline, AIRNAME, MAXNAME);             // get neext name of airline company
        }
        else
        {
            system("cls");
            puts("\n\n\n---------------------------------------------------------------\n"
                 "File is full, you cannot add more scheduled flights!");
        }
    }

    if (count - filecount > 0)      // if at least 1 new flight is registered
    {
        system("cls");

        if((count - filecount) == 1)                // if only one is made
            puts("Here is new scheduled flight:");
        else                                        // if more than one
            puts("Here are new scheduled flights:");

        // show new scheduled flight(s)
        for (index; index < count; index++)         // index is starting positions of (last flight + 1) in record
            show_FlightInfo(index, scheduledFlight);

        // write them in a file
        fwrite(&scheduledFlight[filecount], size, count - filecount, fscFlight);
    }
    else
        puts("No scheduled flights.\n"); // if no new flights

    fclose(fscFlight);

}
/**************************************************************************************************************************/





/**************************************************************************************************************************/
/******************************************************************************************
 *   Function to to modify chosen passenger, all information or specific one
 *   such as, user can choose only name of passenger to edit or to edit all informations
 ******************************************************************************************/
void modifyFlight(struct Flight *scheduledFlight)
{
    short count;                          // inilialize counter to 0
    short optionP, optionE;               // optionP is to choose passenger which to edit, and optionE is to choose what to edit
    short size = sizeof (struct Flight);  // get seize of structure Passenger

    if(!openfscFlightFile("r+b")) return; // open a file for editing

    if((count = listFlights(scheduledFlight)) != -1)// list all flights and store their number in count
    {
        fflush(stdin);  // unespected behaviour prevention

        if((optionP = get_input(-1, count)) != -1)
        {
                system("cls");          // clear screen to be more readable
                puts("Scheduled flight chosen for editing:");
                show_FlightInfo(optionP, scheduledFlight);    // show passenger chosen to be edited

                printf("\nPlease choose detail(s) you want to edit, or -1 to exit:\n0. All\n1. Airline name\n2. Flight ID\n3. Departure city\n"
                       "4. Arrival city\n5. Departure date\n6. Departure Time\n7. Arrival TIme\nChoose option: "); // print options

                if((optionE = get_input(-1, 7)) != -1)
                {
                    // depenting on number entered
                    switch(optionE)
                    {
                        // edit everything
                        case 0: get_Name(scheduledFlight[optionP].airline, AIRNAME, MAXNAME);
                                scheduledFlight[optionP].flightID = checkID(optionP, count, scheduledFlight);
                                get_Name(scheduledFlight[optionP].departureCity, FROM, MAXNAME);
                                get_Name(scheduledFlight[optionP].arrivalCity, TO, MAXNAME);
                                get_date(scheduledFlight[optionP].departureDate, 2014, 2050, 0);
                                scheduledFlight[optionP].timeDeparture = get_Time(1);
                                scheduledFlight[optionP].timeArrival = get_Time(0);
                                break;

                        case 1: get_Name(scheduledFlight[optionP].airline, AIRNAME, MAXNAME);                   break; // edit only airline name
                        case 2: scheduledFlight[optionP].flightID = checkID(optionP, count, scheduledFlight);   break; // edit only flighID
                        case 3: get_Name(scheduledFlight[optionP].departureCity, FROM, MAXNAME);                break; // edit only departure city
                        case 4: get_Name(scheduledFlight[optionP].arrivalCity, TO, MAXNAME);                    break; // edit only arrival city
                        case 5: get_date(scheduledFlight[optionP].departureDate, 2014, 2050, 0);                break; // edit only date of flight departure
                        case 6: scheduledFlight[optionP].timeDeparture = get_Time(1);                           break; // edit only time of departure
                        case 7: scheduledFlight[optionP].timeArrival = get_Time(0);                             break; // edit only time of arrival

                    }

                    fseek(fscFlight, size * optionP, SEEK_SET);             // find position of record with chosen passenger
                    fwrite(&scheduledFlight[optionP], size, 1, fscFlight);  // write there edited data
                    system("cls");                                          // clear screen for to be more readable
                    puts("Here is edited scheduled flight: ");              // print message
                    show_FlightInfo(optionP, scheduledFlight);              // print details of edited passenger

                } // end of inner if
        }   // end of outer if
    }

    fclose(fscFlight);    // when done, close file

}
/**************************************************************************************************************************/





/**************************************************************************************************************************/
/******************************************************************************************
 *   Function to cancel chosen flight
 ******************************************************************************************/
void cancelFlight(struct Flight *scheduledFlight)
{
    short count = 0;                          // inilialize counter to 0
    short optionF, optionE;                   // optionF is to choose which flight to cancel, opetionE is for yes/no confirmation
    if(!openfscFlightFile("r+b")) return;;    // open a file for editing

    if((count = listFlights(scheduledFlight)) != -1)   // list scheduled flights and store their number in count
    {
        fflush(stdin);  // unespected behaviour prevention

        do
        {
            if((optionF = get_input(-1, count)) != -1)                     // if input is not -1
            if(strcmp(scheduledFlight[optionF].status, "CANCELLED") != 0)  // and if chosen flight is not already cancelled
            {
                system("cls");                                             // clear screen to be more readable
                puts("Scheduled flight chosen for cancellation:");
                show_FlightInfo(optionF, scheduledFlight);                 // show flight chosen to be cancelled

                puts("Are you sure you want to cancel this scheduled flight?\n1. Yes\n0. No");

                if((optionE = get_input(0, 1)) == 1)                       // if input is 0
                {
                    strncpy(scheduledFlight[optionF].status, "CANCELLED", STATUS);  // cancel flight

                    short size = sizeof (struct Flight);                   // get seize of structure Passenger

                    fseek(fscFlight, size * optionF, SEEK_SET);            // find position of record with chosen passenger

                    fwrite(&scheduledFlight[optionF], size, 1, fscFlight); // write there edited data
                    system("cls");                                         // clear screen for to be more readable

                    puts("Here is cancelled scheduled flight: ");          // print message
                    show_FlightInfo(optionF, scheduledFlight);             // print details of edited passenger

                    break;
                }
                else break;

            }
            else puts("Flight has been already cancelled. Choose another or -1 to exit.");

        }while(optionF != -1); // while input is not -1
    }

    fclose(fscFlight);     // when done, close file
}
/**************************************************************************************************************************/





/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to display all scheduled flights in the file
 **************************************************************************************/
void show_existingFlights(const struct Flight *scheduledFlight)
{
    short count = 0;                        // inilialize counter to 0

    short size = sizeof (struct Flight);    // get seize of structure passenger

    if(!openfscFlightFile("rb")) return;    // open a file for reading

    rewind(fscFlight);                      // go back to the beggining of a file
    while (fread(&scheduledFlight[count], size, 1, fscFlight))  // while there is data to be read
    {
            if (count == 0)                                     // display message only once
                puts("Current scheduled flights:");
                show_FlightInfo(count, scheduledFlight);        // function which shows passenger info with index count

            count++; // increment counter
    }

    if(count == 0)        // if file is empty, print message
        puts("File is empty.");

    fclose(fscFlight);    // when done, close file
}
/**************************************************************************************************************************/




/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to display a particular scheduled flight info
 *   as argument, it takes an number for index of scheduled flight
 **************************************************************************************/
void show_FlightInfo(short n, const struct Flight *scheduledFlight)
{
    // print info
    printf("\n******************************************************\nScheduled flights info:"
           "\nScheduled flight ID: \t%d\nAirline name: \t\t%s\nFrom: \t\t\t%s\nTo: \t\t\t%s\n"
           "Departure date: \t%-0.2hd/%-0.2hd/%hd\nDeparture time: \t%-0.4hd\nArrival time: \t\t%-0.4hd\n"
           "Available seats: \t%d\nStatus:\t\t\t%s\n******************************************************\n"
           , scheduledFlight[n].flightID, scheduledFlight[n].airline
           , scheduledFlight[n].departureCity, scheduledFlight[n].arrivalCity
           , scheduledFlight[n].departureDate[0], scheduledFlight[n].departureDate[1]
           , scheduledFlight[n].departureDate[2], scheduledFlight[n].timeDeparture
           , scheduledFlight[n].timeArrival, scheduledFlight[n].availableSeats, scheduledFlight[n].status);

}
/**************************************************************************************************************************/




/**************************************************************************************************************************/
/***********************************************************************************************
 *   Function to display all scheduled flights and to return how many are in the file
 ***********************************************************************************************/
short listFlights(const struct Flight *scheduledFlight)
{
    short count = 0;
    short size = sizeof(struct Flight);

    while (fread(&scheduledFlight[count], size, 1, fscFlight) == 1)     // while there is data to be read
    {
        if (count == 0)                                     // display message only once
            puts("Current scheduled flights:\n\nNo. FlightID  Airline \t\tFrom\t\tTo \t\tStatus\t Seats\n");

        printf("%hd.  %-8hd| %-16s|\t%-14s|\t%-14s|\t%-9s| %-2hd|\n", count, scheduledFlight[count].flightID, scheduledFlight[count].airline,
                scheduledFlight[count].departureCity, scheduledFlight[count].arrivalCity
                , scheduledFlight[count].status, scheduledFlight[count].availableSeats);// function which shows passenger info with index count

        count++; // increment counter
    }

    if(count == 0)      // if file is empty
    {
        fclose(fscFlight);    // if empty, close it and print message
        puts("fscFlight.dat file is empty.");
        return -1;                 // and exit from function
    }

    printf("\nPlease provide one number from above, or -1 to exit: ");
    return --count; // return number of scheduled flights

}
/**************************************************************************************************************************/




/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to get time in 24h format
 **************************************************************************************/
short get_Time(short n)
{
    short hour, min;

    if (n)      // just to print correct text
        puts("------------------------------------------------------------------------\n"
             "Please provide time of flight departure:");
    else
        puts("------------------------------------------------------------------------\n"
             "Please provide time of flight arrival:");

    printf("Please provide hours: ");

    hour = get_input(0, 23);    // get hours

    printf("Please provide minutes: ");

    min = get_input(0,59);      // get minuts

    return hour*100 + min;      // return time in format HHMM

}
/**************************************************************************************************************************/




/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to check if flightID already exist
 **************************************************************************************/
short checkID(short flight, short count, const struct Flight *scheduledFlight)
{
    short temp, i;
    // checking whether flight with entered ID already exist or not
    do
    {
        temp = get_int(FLIGHTID);       // getting user input
        for (i = 0; i <= count; i++)
        {
            if(i == flight)             // if index points to chosen flight, skip it
                i++;

            if(scheduledFlight[i].flightID == temp)     // checking
            {
                i = -1; // for purpose to repeat the loop
                printf("Flight with flight ID %d already exists. Please provide another fligh ID:\n", temp);
                break;      // exit for loop
            }

        }

    }while(i == -1);        // if flight ID exist, repeat process

    return temp;            // return new flight ID
}
/**************************************************************************************************************************/





/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to open the file where scheduled flights' informations are stored
 **************************************************************************************/
short openfscFlightFile(char *mode)
{
    if ((fscFlight = fopen("fscFlight.dat", mode)) == NULL)     // if cannot open a file
    {
        fputs("Cannot open fscFlight.dat file.\n",stderr);         // display errorr
        return 0;
    }
    else return 1;
}
/**************************************************************************************************************************/
// CODE NOT FOUND
